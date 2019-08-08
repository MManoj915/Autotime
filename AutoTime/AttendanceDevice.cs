using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZKService
{
    class AttendanceDevice
    {
        public string DeviceIP
        {
            get;
            set;
        }

        public string DeviceName
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public long DeviceCode
        {
            get;
            set;
        }

        public int DeviceType
        {
            get;
            set;
        }
    }
}
