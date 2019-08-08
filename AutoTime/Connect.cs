using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Dapper;
using System.Data;
using System.Configuration;

namespace ZKService
{
    public static class Connect
    {
       /* private static IDbConnection _db = new OracleConnection(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
        public static zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();

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
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    File.AppendAllLines(path, new[] { "Attendance Count : " + 0 });
                }
                else
                {
                    File.AppendAllLines(path, new[] { "Attendance Count : " + 0 });
                }
                List<AttendanceDevice> ADL = _db.Query<AttendanceDevice>("SELECT MACHINE_ID DEVICECODE,IP_ADRESS DEVICEIP,PORT,MACHINE_NAME DEVICENAME,1 DeviceType FROM HR_MACHINE_MAPPING_HDR").ToList();
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    File.AppendAllLines(path, new[] { "Attendance Count : " + ADL.Count });
                }
                else
                {
                    File.AppendAllLines(path, new[] { "Attendance Count : " + ADL.Count });
                }
                foreach (AttendanceDevice CurDevice in ADL)
                {
                    if (!File.Exists(path))
                    {
                        File.Create(path).Dispose();
                        File.AppendAllLines(path, new[] { "Machine ID : " + CurDevice.DeviceCode });
                    }
                    else
                    {
                        File.AppendAllLines(path, new[] { "Machine ID : " + CurDevice.DeviceCode });
                    }
                    if (ConnectMe(CurDevice.DeviceIP, CurDevice.Port, CurDevice.DeviceType, ref Errors))
                    {
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
                                string Sql = "Insert into HRMS_ATT_DATA(DeviceCode,IDWenRollNumber,TransactionDate,TRANSACTIONTIME,IDInoutMode,IDWVerifyMode,IDWMonth,IDWDay,IDWYear,IDWHour,IDWMinute,IDWSecond,PRocessed) Values " +
                                    " (" + CurDevice.DeviceCode + ",'" + idwEnrollNumbers + "',To_Date('" + idwDay + "/" + idwMonth + "/" + idwYear + " " + idwHour + ":" + idwMinute + ":" + idwSecond + "','DD/MM/RRRR HH24:MI:SS')," +
                                    " To_Date('" + idwHour + ":" + idwMinute + ":" + idwSecond + "','HH24:MI:SS')," +
                                    " " + idwInOutMode + "," + idwVerifyMode + "," + idwMonth + "," + idwDay + "," + idwYear + "," + idwHour + "," + idwMinute + "," + idwSecond + ",'N')";
                                StrList.Add(Sql);
                            }
                            DisConnectMe(CurDevice.DeviceIP, CurDevice.Port, false, ref Errors, CurDevice.DeviceType);
                            axCZKEM1.EnableDevice(iMachineNumber, true);
                            try
                            {


                                for (int i = 0; i < StrList.Count; i++)
                                {
                                    if (!File.Exists(path))
                                    {
                                        File.Create(path).Dispose();
                                        File.AppendAllLines(path, new[] { StrList[i] });
                                    }
                                    else
                                    {
                                        File.AppendAllLines(path, new[] { StrList[i] });
                                    }
                                    _db.Execute(StrList[i]);
                                }
                            }
                            catch
                            {

                                Errors.Add(" Error During Saving Attendance to DB -Error 154");
                            }

                        }

                    }
                }

            }
            catch (Exception Exp)
            {
                string path = ConfigurationSettings.AppSettings["SQLPath"].ToString();
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
        }*/


    }
}
