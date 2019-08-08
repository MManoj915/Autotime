using Dapper;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using zkemkeeper;

namespace ZKService
{
    public partial class Service1 : ServiceBase
    {
        private Thread _thread;
        private int ThreadTimeLimit = 1;
        private static IDbConnection _db = new OracleConnection(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
        public static zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
        public Service1()
        {
            string TimeLimit = string.Empty; 
            int HourLimit = Convert.ToInt32(ConfigurationSettings.AppSettings["HOURLIMIT"].ToString());
            if (string.IsNullOrEmpty(TimeLimit))
                TimeLimit = "1";
            ThreadTimeLimit = (Convert.ToInt16(TimeLimit) * HourLimit); 
            InitializeComponent(); 
            //Execute();
        }

        protected override void OnStart(string[] args)
        {
            this._thread = new Thread(new ThreadStart(this.Execute));
            this._thread.Start();
            EventLog.WriteEntry("ZK Start", "Service Started with Time Limit : " + ThreadTimeLimit);
        }

        private void Execute()
        {
            string path = ConfigurationSettings.AppSettings["SQLPath"].ToString();
           
            List<string> Errors = new List<string>();

            bool doprocess = false;
            bool serviceintervaltime = false;
            try
            {
                while (!doprocess)
                {
                    EventLog.WriteEntry("ZK Services", "Pulling Data from Device");
                    PullLogFromDevice();
                    EventLog.WriteEntry("ZK Services", "Finish Pulling Data");
                    Thread.Sleep(ThreadTimeLimit);
                }
            }
            catch (Exception EX)
            {

            }
        }

        protected override void OnStop()
        {
            if (this._thread != null)
            {
                this._thread.Abort();
                this._thread.Join();
            }
            EventLog.WriteEntry("ZK Stop", "Service Stoped");
        } 

        public static bool ConnectMe(string DeviceIP, int DevicePort, int DeviceType, ref List<string> Errors)
        {
            switch (DeviceType)
            {
                case 1:
                    {
                        bool resault = false;
                        bool bIsConnected = false;
                        int iMachineNumber;
                        try
                        {
                            int idwErrorCode = 0;
                            try { bIsConnected = axCZKEM1.Connect_Net(DeviceIP, DevicePort); }
                            catch { bIsConnected = axCZKEM1.Connect_Net(DeviceIP, DevicePort); }
                            if (bIsConnected == true)
                            {
                                //In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                                iMachineNumber = 1;
                                //Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                                axCZKEM1.RegEvent(iMachineNumber, 65535);
                            }
                            else
                            {
                                //ConnectMe(DeviceIP, DevicePort, DeviceType, ref Errors);
                                axCZKEM1.GetLastError(ref idwErrorCode);
                                Errors.Add(idwErrorCode.ToString());
                                // throw new Exception("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString()+ "Error");
                            }
                            resault = bIsConnected;
                            return resault;
                        }
                        catch (Exception x)
                        {
                            //ConnectMe(DeviceIP, DevicePort, DeviceType, ref Errors); 
                            return resault;
                        } //throw new Exception(x.Message); }
                    }
                case 2:// Synel Device;
                    {
                        break;
                        // return false;
                    }
            }
            return false;
        }

        public static bool DisConnectMe(string DeviceIP, int DevicePort, bool ClearData, ref List<string> Errors, int DeviceType)
        {

            switch (DeviceType)
            {
                case 1:
                    {
                        //CountTries = 0;
                        int idwMN = 1; int idwErrorCode = 1;
                        try
                        {
                            if (!ClearData)
                            {
                                axCZKEM1.Disconnect();
                                return true;
                            }
                            else
                            {

                                axCZKEM1.EnableDevice(idwMN, false);//disable the device
                                try
                                {
                                    if (axCZKEM1.ClearGLog(idwMN))
                                    {

                                        axCZKEM1.RefreshData(idwMN);//the data in the device should be refreshed
                                    }
                                    else
                                    {
                                        axCZKEM1.GetLastError(ref idwErrorCode);
                                        Errors.Add("Error During Clearing G Log Device type 1: Operation failed,ErrorCode=" + idwErrorCode.ToString());
                                    }
                                }
                                catch
                                {
                                    axCZKEM1.EnableDevice(idwMN, true);//enable the device
                                }

                                try
                                {
                                    if (axCZKEM1.ClearSLog(idwMN))
                                    {

                                        axCZKEM1.RefreshData(idwMN);//the data in the device should be refreshed
                                    }
                                    else
                                    {
                                        axCZKEM1.GetLastError(ref idwErrorCode);
                                        Errors.Add("Error During Clearing S Log Devicy Type 1: Operation failed,ErrorCode=" + idwErrorCode.ToString());
                                    }
                                }
                                catch
                                {
                                    axCZKEM1.EnableDevice(idwMN, true);//enable the device
                                }


                                axCZKEM1.EnableDevice(idwMN, true);//enable the device
                            }
                            //axCZKEM1.Disconnect();
                            return true;
                        }
                        catch (Exception x)
                        {
                            axCZKEM1.EnableDevice(idwMN, true);//enable the device
                            Errors.Add("Error during disconnecting device type 0" + x.Message); return true;
                        }
                    }
                case 2:
                    {
                        return true;
                    }
            }
            return true;
        }

        public static bool PullLogFromDevice()
        {
            try
            {
                List<string> Errors = new List<string>();
                string path = ConfigurationSettings.AppSettings["SQLPath"].ToString();
                EventLog.WriteEntry("ZK Services", "Connecting to Database");
                List<AttendanceDevice> ADL = _db.Query<AttendanceDevice>("SELECT Device_ID DeviceCode,DEVICE_IP DEVICEIP,PORT,Device_Name DEVICENAME,Device_Type DeviceType FROM HR_TA_DEVICES_SETUP").ToList();
                EventLog.WriteEntry("ZK Services", "Database Connected");
                foreach (AttendanceDevice CurDevice in ADL)
                {
                    EventLog.WriteEntry("ZK Services", "Pulling Data from "+ CurDevice.DeviceIP);
                    if (ConnectMe(CurDevice.DeviceIP, CurDevice.Port, CurDevice.DeviceType, ref Errors))
                    {
                        EventLog.WriteEntry("ZK Services", "Device Connected");
                        int idwVerifyMode = 0;
                        int idwInOutMode = 0;
                        int idwYear = 0;
                        int idwMonth = 0;
                        int idwDay = 0;
                        int idwHour = 0;
                        int idwMinute = 0;
                        int idwSecond = 0;
                        int idwWorkCode = 0;
                        int iMachineNumber = 1;
                        string idwEnrollNumbers = string.Empty;
                        axCZKEM1.EnableDevice(iMachineNumber, false);
                        List<string> StrList = new List<string>();
                        if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                        {
                            while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber,
                                                       out idwEnrollNumbers, out idwVerifyMode, out idwInOutMode,
                                                            out idwYear, out idwMonth, out idwDay,
                                                            out idwHour, out idwMinute, out idwSecond,
                                                            ref idwWorkCode))
                            {
                                if (string.IsNullOrEmpty(idwEnrollNumbers))
                                    continue;
                                string Sql = "Insert into HRMS_IN_OUT(InOutCode,InOutSerialNo,Location_ID,CreatedIn,CreatedOn,CreatedBy," +
                                    " EmployeeCode,CardNo,IDWenRollNumber,TransactionDate,AttendanceDeviceCode," +
                                    " IDWInoutMode,IDWVerifyMode,IDWMonth,IDWDay,IDWYear,IDWHour,IDWMinute,IDWSecond,IDWMachineNumber,DeviceType," +
                                    " PRocessed,Org_ID,IPProcessed,ProcessedUPD,Status) Values " +
                                    " ((Select Max(InOutCode) + 1 From Hrms_In_Out),(Select Max(InOutSerialNo) + 1 From Hrms_In_Out),1," +
                                    " 1,SysDate,100000000000000002,Null,Null,'" + idwEnrollNumbers + "'," +
                                    " To_Date('" + idwDay + "/" + idwMonth + "/" + idwYear + " " + idwHour + ":" + idwMinute + ":" + idwSecond + "','DD/MM/RRRR HH24:MI:SS')," +
                                    " " + CurDevice.DeviceCode + "," + 
                                    " " + idwInOutMode + "," + idwVerifyMode + "," + idwMonth + "," + idwDay + "," + idwYear + "," + idwHour + "," + idwMinute + "," + idwSecond + ",1,1,110,1,0,NULL,NULL)";
                                StrList.Add(Sql);
                            }
                           
                            try
                            {

                                EventLog.WriteEntry("ZK Services", "Data Count " + StrList.Count);
                                for (int i = 0; i < StrList.Count; i++)
                                {
                                    if (!File.Exists(path))
                                    {
                                        File.Create(path).Dispose();
                                        File.AppendAllLines(path, new[] { StrList[i] + ";" });
                                    }
                                    else
                                    {
                                        File.AppendAllLines(path, new[] { StrList[i] + ";" });
                                    }
                                    _db.Execute(StrList[i]);
                                }
                                EventLog.WriteEntry("ZK Services", "Finish insert into table");
                                DisConnectMe(CurDevice.DeviceIP, CurDevice.Port, true, ref Errors, CurDevice.DeviceType);
                                axCZKEM1.EnableDevice(iMachineNumber, true);
                            }
                            catch
                            {
                                DisConnectMe(CurDevice.DeviceIP, CurDevice.Port, false, ref Errors, CurDevice.DeviceType);
                                Errors.Add(" Error During Saving Attendance to DB -Error 154");
                            }

                        }

                    }
                    else
                    { 
                    }
                   
                }

            }
            catch (Exception Exp)
            {
                string path = ConfigurationSettings.AppSettings["ErrorLogFile"].ToString();
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    File.AppendAllLines(path, new[] { Exp.Message + Exp.InnerException.ToString() });
                }
                else
                {
                    File.AppendAllLines(path, new[] { Exp.Message + Exp.InnerException.ToString() });
                }
            }

            return true;
        }
    }
}
