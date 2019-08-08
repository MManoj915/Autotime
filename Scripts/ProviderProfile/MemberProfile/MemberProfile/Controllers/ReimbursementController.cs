// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.ReimbursementController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MemberProfile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class ReimbursementController : Controller
  {
    private IDbConnection _db;

    public ActionResult Index()
    {
      return (ActionResult) this.View((object) ((List<EnquiryModel>) SqlMapper.AsList<EnquiryModel>(SqlMapper.Query<EnquiryModel>(this._db, "SELECT MEM.GroupName,MobileNumber,EmailID,0 ComplaintType,'' Description,'' Comments From IM_MEMBERPROFILE_VW MEM Where  MEM.ID =" + (object) Convert.ToInt64(this.get_Session()["MemberCode"]), (object) null, (IDbTransaction) null, true, new int?(), new CommandType?())))[0]);
    }

    [HttpPost]
    public ActionResult Index(EnquiryModel Mod, IEnumerable<HttpPostedFileBase> files)
    {
      try
      {
        string str1 = DateTime.Now.ToString("yyyy-MM-dd");
        Member member = (Member) this.get_Session()["MemberDetails"];
        string str2 = string.Empty;
        if (!string.IsNullOrEmpty(Mod.Description))
          str2 = Mod.Description;
        string str3 = string.Empty;
        if (!string.IsNullOrEmpty(Mod.Comments))
          str3 = Mod.Comments;
        MailMessage message = new MailMessage();
        message.IsBodyHtml = true;
        List<string> stringList = new List<string>();
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        message.From = new MailAddress("ngicorporation2015@gmail.com");
        string appSetting = ConfigurationManager.AppSettings["ReimbursementClaimID"];
        message.Subject = "Reimbursement Claims Submission";
        message.Body = " Hello NGI team,<br><br> <b>Member Id: </b>" + member.Memberid + " <br> <b>Member Name: </b>" + member.FirstName + " " + member.LastName + "<br> <b>Email-ID:</b> " + member.EmailId + "<br> <b>Date:</b> " + str1 + " <br><br> Collect Reimbursement Claims Submission documents! ";
        message.To.Add(appSetting);
        foreach (HttpPostedFileBase file in files)
        {
          if (file != null && file.ContentLength > 0)
          {
            string fileName = Path.GetFileName(file.FileName);
            Attachment attachment = new Attachment(file.InputStream, fileName);
            message.Attachments.Add(attachment);
          }
        }
        smtpClient.Port = 587;
        smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential("ngicorporation2015@gmail.com", "@ngiadmin#");
        smtpClient.EnableSsl = true;
        smtpClient.Send(message);
        ((ControllerBase) this).get_TempData().set_Item("ReimbursementResult", (object) "Reimbursement Claim Submission Submitted Successfully !");
      }
      catch (Exception ex)
      {
        ((ControllerBase) this).get_TempData().set_Item("ReimbursementResult", (object) "Error While submitting Reimbursement Claim Submission !");
      }
      return (ActionResult) this.RedirectToAction(nameof (Index));
    }

    public ReimbursementController()
    {
      base.\u002Ector();
    }
  }
}
