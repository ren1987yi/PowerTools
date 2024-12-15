using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
namespace Vision
{
    /// <summary>
    /// 图像处理
    /// </summary>
    public class ImageProcess
    {
        /// <summary>
        /// 图像二值化：取图片的阈值，亮度比较，低于该值的全都为0，高于该值的全都为255
        /// </summary>
        /// <param name="bmp">输入图像</param>
        /// <param name="iss">阈值</param>
        /// <param name="outputBmp">输出图像</param>
        /// <returns></returns>
        public static void ConvertTo1Bpp(Bitmap bmp, int iss, ref Bitmap outputBmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11) * color.A;
                    //int value = 255 - color.B;
                    int value = gray;
                    Color newColor = value > iss ? Color.FromArgb(255, 255, 255, 255) : Color.FromArgb(0, 0, 0, 0);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            outputBmp = bmp;
        }


        /// <summary>
        /// 提取图像外轮廓
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static List<Vector2> GetImageOutside(Bitmap bmp)
        {

            //实现原理： 
            //1.按x轴逐行扫描，判断x轴上首次出现非透明像素，及x轴上第二次及N次出现透明像素
            //2.按y轴逐行扫描，判断y轴上首次出现非透明像素，及y轴上第二次及N次出现透明像素；

            int w = bmp.Width, h = bmp.Height;//获得原图宽高
            //Bitmap bmp = new Bitmap(SourceImage);//将原图转为位图
            Bitmap output = new Bitmap(bmp.Width, bmp.Height);//用于输出内轮廓的图片

            List<Point> ouput_data = new List<Point>();//计算后输出像素点信息

            //1.按x轴逐行扫描，判断x轴上首次出现非透明像素，及x轴上第二次及N次出现透明像素
            for (int y = 0; y < h; y++)
            {
                //记录沿X轴方向递增最后一次非透明色的位置
                int? start_x = null;
                for (int x = 0; x < w; x++)
                {
                    //如当前像素点是非 apach 通道的透明色则记录
                    if (bmp.GetPixel(x, y).A > 0)
                    {
                        //记录开始位置
                        if (start_x == null)
                        {
                            start_x = x;
                            ouput_data.Add(new Point(x, y));
                        }
                        //找下个开始位置
                        else if (x + 1 < w - 1 && start_x != null)
                        {
                            //开始位置向后递增一个像素如果像素为透明则记录
                            if (bmp.GetPixel(1 + x, y).A == 0)
                            {
                                ouput_data.Add(new Point(x, y));
                                start_x = null;//重置开始x位置
                            }
                        }
                        else if (x + 1 > w - 1)
                        {
                            ouput_data.Add(new Point(x, y));
                        }
                    }
                }
            }

            //2.按y轴逐行扫描，判断y轴上首次出现非透明像素，及y轴上第二次及N次出现透明像素；
            for (int x = 0; x < w; x++)
            {
                //记录沿y轴方向递增最后一次非透明色的位置
                //如当前像素点是非 apach 通道的透明色则记录
                int? start_y = null;
                for (int y = 0; y < h; y++)
                {
                    if (bmp.GetPixel(x, y).A > 0)
                    {
                        //记录开始位置
                        if (start_y == null)
                        {
                            start_y = y;
                            ouput_data.Add(new Point(x, y));
                        }
                        //找下个开始位置
                        else if (y + 1 < h - 1 && start_y != null)
                        {
                            //开始位置向后递增一个像素如果像素为透明则记录
                            if (bmp.GetPixel(x, y + 1).A == 0)
                            {
                                ouput_data.Add(new Point(x, y));
                                start_y = null;//重置开始y位置
                            }
                        }
                        else if (y + 1 > h - 1)
                        {
                            //如在最后边界上则也记录
                            ouput_data.Add(new Point(x, y));
                        }
                    }
                }
            }

            return ouput_data.Select(s => new Vector2(s.X, s.Y)).ToList();
            ouput_data.ForEach(x =>
            {
                output.SetPixel(x.X, x.Y, Color.Blue);//输出图片中写入像素点位置
            });


        }



        /// <summary>
        /// 按点的相近相近关系，把点集连接成多段线 polyline
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<List<Vector2>> PointsToPolylines(List<Vector2> points,double steplength = 3)
        {
            points.Sort(ComparePointByXY);

            var npoints = points.DistinctBy(p => $"{p.X}:{p.Y}").ToList();


            List<List<Vector2>> polylines = new List<List<Vector2>>();


            //double stepLength = 3;

            Vector2 curPoint = new Vector2(int.MinValue);


            do
            {
                var polyline = new List<Vector2>();
                var _bpoints1 = npoints.OrderBy(p => (p - curPoint).Length());

                curPoint = _bpoints1.First();
                npoints.RemoveAt(0);
                polyline.Add(curPoint);

                while (true)
                {



                    var _bpoints = npoints.Where(p => (p - curPoint).Length() <= steplength).OrderBy(p => (p - curPoint).Length());//备选点，距离够
                    if (_bpoints.Count() <= 0)
                    {
                        break;
                    }
                    else
                    {
                        var p = _bpoints.First();
                        npoints.Remove(p);
                        polyline.Add(p);
                        curPoint = p;
                    }

                }


                polyline = polyline.DistinctBy(p => $"{p.X}:{p.Y}").ToList();

                polylines.Add(polyline);

                if (npoints.Count <= 0)
                {
                    break;
                }
            } while (true);


            return polylines;


        }


        /// <summary>
        /// 根据 x , y 的顺序比较
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private static int ComparePointByXY(Vector2 p1, Vector2 p2)
        {
            if (p1.X < p2.X)
            {
                return -1;
            }
            else if (p1.X == p2.X)
            {
                if (p1.Y < p2.Y)
                {
                    return -1;
                }
                else if (p1.Y == p2.Y)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}
