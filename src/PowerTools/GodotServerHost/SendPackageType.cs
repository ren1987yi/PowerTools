using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotServerHost
{
    public enum SendPackageType
    {
        None = 0,
        UpdateData = 1,
        CameraCommand = 2,
        ChangeScene = 3,
        TrackPos = 4
    }
}
