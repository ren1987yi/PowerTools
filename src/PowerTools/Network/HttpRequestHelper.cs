using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Network
{
    public static class HttpRequestHelper
    {
        public static bool GetString(string url,out string content)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var response = client.GetAsync(url).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return true;
                    }
                    else
                    {
                        content = response.StatusCode.ToString();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
                return false;
                
            }
        }

        public static bool GetJson<T>(string url,out T content, out string error) 
        {
            if(GetString(url ,out var txt))
            {
                //content = new JsonSerializer.Deserialize<T>(txt);
                try
                {
                    error = string.Empty;
                    content = JsonSerializer.Deserialize<T>(txt);
                    return true;
                }catch (Exception ex)
                {
                    error = ex.Message;
                    content = default(T);
                    return false;

                }

            }
            else
            {
                error = txt;
                content = default(T);
                return false;
            }
        }
    }
}
