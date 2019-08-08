using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Services;
using System.Data.OracleClient;
using System.Web.Configuration;
using System.IO;
using Dapper;
using System.Configuration;

namespace SponsorcaseService
{
    /// <summary>
    /// Summary description for UploadPicture
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UploadPicture : System.Web.Services.WebService
    {
        private IDbConnection _db = new OracleConnection(ConfigurationSettings.AppSettings["connection"].ToString());
        static string connStr = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        static OracleConnection Conn;
        OracleCommand InsertCommand = new OracleCommand();
        OracleCommand UpdateCommand = new OracleCommand();
        OracleCommand CaseCommand = new OracleCommand();
        OracleCommand SelectCommand = new OracleCommand();

        [WebMethod]
        public string UploadSponsorFiles(int UploadType, string FileName, byte[] ImageBytes, string CaseCode)
        {
            string StatusMessage = CreateCaseImageFolder(UploadType, FileName, ImageBytes, CaseCode);
            return StatusMessage;
        }

        [WebMethod]
        public string UploadAidRequestFiles(int UploadType, string FileName, byte[] ImageBytes, string CaseCode)
        {
            string StatusMessage = CreateAidRequestImageFolder(UploadType, FileName, ImageBytes, CaseCode);
            return StatusMessage;
        }

        [WebMethod]
        public string UploadProjectFiles(int UploadType, string FileName, byte[] ImageBytes, string ProjectCode)
        {
            string StatusMessage = CreateProjectImageFolder(UploadType, FileName, ImageBytes, ProjectCode);
            return StatusMessage;
        }


        public string CreateCaseImageFolder(int UploadType, string FileName, byte[] ImageBytes, string CaseCode)
        {
            string Status = "S";
            Conn = new OracleConnection(connStr);            
            long Case_Code = 0;
            int affected = 0; 
            string DMLCaseQuery = "Select CASE_ID from SP_CASES Where Upper(Trim(CASECODE)) = Upper(Trim('" + CaseCode + "'))";
            CaseCommand.CommandText = DMLCaseQuery;
            CaseCommand.Connection = Conn;
            Conn.Open();
            Case_Code = Convert.ToInt64(CaseCommand.ExecuteScalar());
            Conn.Close();
            string DMLInsertQuery = string.Empty;
            if (UploadType == 1)
            {
                DMLInsertQuery = "Update SP_CASES set ImageFileName = '" + CaseCode + ".jpg' Where Case_Code = " + Case_Code;
            }
            if (UploadType == 2)
            {
                DMLInsertQuery = " Insert into BMCASESFILES " +
                                " (FILE_CODE, CASE_CODE, FILETITLE, FILENAME, COMPANYCODE, CREATEDON, LASTMODIFIEDON, LOCATIONCODE, SERIALNO) " +
                                " Values " +
                                "  ((SELECT Nvl(MAX(FILE_CODE),100000000000000000) + 1 FROM BMCASESFILES), " + Case_Code + ", '" + CaseCode + ".jpg', '" + CaseCode + ".jpg', 1, " +
                                "  Sysdate,Sysdate, 1, (SELECT Nvl(MAX(SERIALNO),0) + 1 FROM BMCASESFILES)) ";
            }

            InsertCommand.CommandText = DMLInsertQuery;
            InsertCommand.Connection = Conn;
            Conn.Open();
            affected = InsertCommand.ExecuteNonQuery();
            Conn.Close();
            string ImagePath = string.Empty;
            if (UploadType == 1)
            {
                ImagePath = WebConfigurationManager.AppSettings["CaseImagePath"].ToString();
                ImagePath = ImagePath + Case_Code;
                if (!System.IO.File.Exists(ImagePath))
                {
                    System.IO.Directory.CreateDirectory(ImagePath);
                    File.WriteAllBytes(ImagePath + "\\" + CaseCode + "", ImageBytes);
                }
            }
            if (UploadType == 2)
            {
                ImagePath = WebConfigurationManager.AppSettings["CaseFilePath"].ToString();
                ImagePath = ImagePath + Case_Code;
                if (!System.IO.File.Exists(ImagePath))
                {
                    System.IO.Directory.CreateDirectory(ImagePath);
                    File.WriteAllBytes(ImagePath + "\\" + FileName + "", ImageBytes);
                }
            }
           
            if (affected == 1)
            {                
                return "S";
            }
            else
            {
                if (Case_Code == 0)
                {
                    return CaseCode;
                }
                else
                {
                    Status = "Not Updated !";
                    return "S";
                }
                
            }
            return "S";
        }

        public string CreateAidRequestImageFolder(int UploadType, string FileName, byte[] ImageBytes, string CaseCode)
        {
            string Status = "S";
            Conn = new OracleConnection(connStr);
            long Case_Code = 0;
            int affected = 0;
            string DMLCaseQuery = "Select Case_Code from C_Aid_Requests Where Upper(Trim(Case_No)) = Upper(Trim('" + CaseCode + "'))";
            CaseCommand.CommandText = DMLCaseQuery;
            CaseCommand.Connection = Conn;
            Conn.Open();
            Case_Code = Convert.ToInt64(CaseCommand.ExecuteScalar());
            Conn.Close();
            string DMLInsertQuery = string.Empty;
            /*if (UploadType == 1)
            {
                DMLInsertQuery = "Update BMCases set ImageFileName = '" + CaseCode + ".jpg' Where Case_Code = " + Case_Code;
            }*/
            if (UploadType == 2)
            {
                DMLInsertQuery = " Insert into C_RECEIVED_PAPERS " +
                            " (REQ_PAPER_CODE, COMPANYCODE, CREATEDON, LASTMODIFIEDON, LOCATIONCODE, SERIALNO, CASE_CODE, DOCUMENT_NAME) " +
                            " Values " +
                            " ((SELECT Nvl(MAX(REQ_PAPER_CODE),100000000000000000) + 1 FROM C_RECEIVED_PAPERS), 1,SYSDATE,SYSDATE,1, (SELECT Nvl(MAX(SERIALNO),0) + 1 FROM C_RECEIVED_PAPERS), " +
                            " " + Case_Code + ", '" + FileName + "')";
            }

            InsertCommand.CommandText = DMLInsertQuery;
            InsertCommand.Connection = Conn;
            Conn.Open();
            affected = InsertCommand.ExecuteNonQuery();
            Conn.Close();
            string ImagePath = string.Empty;
            if (UploadType == 1)
            {
                ImagePath = WebConfigurationManager.AppSettings["CaseImagePath"].ToString();
                ImagePath = ImagePath + Case_Code;
                if (!System.IO.File.Exists(ImagePath))
                {
                    System.IO.Directory.CreateDirectory(ImagePath);
                    File.WriteAllBytes(ImagePath + "\\" + CaseCode + "", ImageBytes);
                }
            }
            if (UploadType == 2)
            {
                ImagePath = WebConfigurationManager.AppSettings["AidRequestFilePath"].ToString();
                ImagePath = ImagePath + Case_Code;
                if (!System.IO.File.Exists(ImagePath))
                {
                    System.IO.Directory.CreateDirectory(ImagePath);
                    File.WriteAllBytes(ImagePath + "\\" + FileName + "", ImageBytes);
                }
            }

            if (affected == 1)
            {
                return "S";
            }
            else
            {
                if (Case_Code == 0)
                {
                    return CaseCode;
                }
                else
                {
                    Status = "Not Updated !";
                    return "S";
                }

            }
            return "S";
        }

        public string CreateProjectImageFolder(int UploadType, string FileName, byte[] ImageBytes, string ProjectCode)
        {
            string Status = "S";
            try
            {
                Conn = new OracleConnection(connStr);
                long Case_Code = 0;
                int affected = 0;
                string DMLCaseQuery = "Select Project_Code from SC_PROJECT  Where Upper(Trim(REPLACE(Project_Number,'/',''))) = Upper(Trim('" + ProjectCode + "'))";
                CaseCommand.CommandText = DMLCaseQuery;
                CaseCommand.Connection = Conn;
                Conn.Open();
                Case_Code = Convert.ToInt64(CaseCommand.ExecuteScalar());
                Conn.Close();
                string DMLInsertQuery = string.Empty;
                /*if (UploadType == 1)
                {
                    DMLInsertQuery = "Update BMCases set ImageFileName = '" + CaseCode + ".jpg' Where Case_Code = " + Case_Code;
                }*/
                if (UploadType == 2)
                {
                    DMLInsertQuery = " INSERT INTO SC_PROJECT_DETAIL(FILE_CODE,SERIALNO,LOCATIONCODE,PROJECT_CODE,CREATEDON,LASTMODIFIEDON,FILENAME) " +
                                    " VALUES " +
                                    " ((SELECT Nvl(MAX(FILE_CODE), 100000000000000000) + 1 FROM SC_PROJECT_DETAIL)," +
                                    " (SELECT Nvl(MAX(SERIALNO), 0) + 1 FROM SC_PROJECT_DETAIL),1," + Case_Code + ",SYSDATE,SYSDATE,'" + FileName + "') ";
                }
                Status = DMLInsertQuery;
                InsertCommand.CommandText = DMLInsertQuery;
                InsertCommand.Connection = Conn;
                Conn.Open();
                affected = InsertCommand.ExecuteNonQuery();
                Conn.Close();
                string ImagePath = string.Empty;
                if (UploadType == 2)
                {
                    ImagePath = WebConfigurationManager.AppSettings["ProjectFilePath"].ToString();
                    ImagePath = ImagePath + Case_Code;
                    if (!System.IO.File.Exists(ImagePath))
                    {
                        System.IO.Directory.CreateDirectory(ImagePath);
                        File.WriteAllBytes(ImagePath + "\\" + FileName + "", ImageBytes);
                    }
                }

                if (affected == 1)
                {
                    return "S";
                }
                else
                {
                    if (Case_Code == 0)
                    {
                        return ProjectCode;
                    }
                    else
                    {
                        Status = "Not Updated !";
                        return "S";
                    }

                }
            }
            catch (Exception e)
            {
                return Status;
            }
            
            return "S";
        }

        [WebMethod]
        public DataSet GetDataSet()
        {
            Conn = new OracleConnection(connStr);
            Conn.Open();
            string DMLQuery = "SELECT * FROM SP_RPTFILES_VW";
            SelectCommand.CommandText = DMLQuery;
            SelectCommand.Connection = Conn; 
            DataSet ds = new DataSet();
            OracleDataAdapter _da = new OracleDataAdapter();
            _da.SelectCommand = SelectCommand;
            _da.Fill(ds);
            Conn.Close();
            return ds;
        }

        [WebMethod]
        public byte[] GetImageBytes(long ReportCode,string FileName)
        {
            byte[] ImageBytes; 
            string ImagePath = WebConfigurationManager.AppSettings["ImagePath"].ToString();
            if(FileName.IndexOf('.') >= 0)
            {
                ImageBytes = File.ReadAllBytes(ImagePath + "\\" + ReportCode + "\\" + FileName);
            }
            else
            {
                try
                {
                    ImageBytes = File.ReadAllBytes(ImagePath + "\\" + ReportCode + "\\" + FileName + ".jpg");
                }
                catch
                {
                    ImageBytes = File.ReadAllBytes(ImagePath + "\\" + ReportCode + "\\" + FileName + ".png");
                }
            }
             
            return ImageBytes;
        }

    }


}
