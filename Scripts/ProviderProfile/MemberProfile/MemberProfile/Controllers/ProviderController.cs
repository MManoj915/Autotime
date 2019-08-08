// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.ProviderController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MemberProfile.Models;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Security;
using System.Threading;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class ProviderController : Controller
  {
    private IDbConnection _db;

    public ActionResult Index()
    {
      if (Convert.ToInt64(this.get_Session()["ProviderCode"]) == 0L)
      {
        LoginController loginController = new LoginController();
        if (loginController.CheckCookie() == null)
          return (ActionResult) this.RedirectToAction(nameof (Index), "Login");
        loginController.CheckProviderCredential();
      }
      return (ActionResult) this.View((object) (Provider) this.get_Session()["ProviderDetails"]);
    }

    public ActionResult UpdateProviderInfo(string formData)
    {
      long int64 = Convert.ToInt64(this.get_Session()["ProviderCode"]);
      if (int64 == 0L)
      {
        LoginController loginController = new LoginController();
        if (loginController.CheckCookie() == null)
          return (ActionResult) this.RedirectToAction("Index", "Login");
        loginController.CheckProviderCredential();
      }
      ProviderModel providerModel = (ProviderModel) JsonConvert.DeserializeObject<ProviderModel>(formData);
      Provider provider = (Provider) this.get_Session()["ProviderDetails"];
      string empty = string.Empty;
      SqlMapper.Execute(this._db, "Update IM_Providers Set PROVIDERMAINPHONE = '" + (string.IsNullOrEmpty(providerModel.ProviderMainPhone) ? provider.ProviderMainPhone : providerModel.ProviderMainPhone) + "',PROVIDERMAINEMAIL='" + (string.IsNullOrEmpty(providerModel.EMail) ? provider.EMail : providerModel.EMail) + "',ADDRESS='" + (string.IsNullOrEmpty(providerModel.Address) ? provider.Address : providerModel.Address) + "',PROVIDERAREA='" + (string.IsNullOrEmpty(providerModel.ProviderArea) ? provider.ProviderArea : providerModel.ProviderArea) + "' Where ProviderCode = " + (object) int64, (object) null, (IDbTransaction) null, new int?(), new CommandType?());
      try
      {
        MimeMessage mimeMessage1 = new MimeMessage();
        mimeMessage1.get_From().Add((InternetAddress) new MailboxAddress("Provider Portal", "Providerportal@ngiuae.com"));
        mimeMessage1.get_To().Add((InternetAddress) new MailboxAddress("Zeeshan", "zeeshan@ngiuae.com"));
        mimeMessage1.set_Subject("Provider Detail Change");
        MimeMessage mimeMessage2 = mimeMessage1;
        TextPart textPart1 = new TextPart("html");
        textPart1.set_Text(" Hello NGI team,<br><br>Below Provider changed his details. <br><br><b>Provider Id: </b>" + this.get_Session()["ProviderID"].ToString() + " <br> <b>Provider Name: </b>" + this.get_Session()["ProviderName"].ToString());
        TextPart textPart2 = textPart1;
        mimeMessage2.set_Body((MimeEntity) textPart2);
        using (SmtpClient smtpClient = new SmtpClient())
        {
          ((MailService) smtpClient).set_ServerCertificateValidationCallback((RemoteCertificateValidationCallback) ((sender, certificate, chain, sslPolicyErrors) => true));
          ((MailService) smtpClient).Connect("148.0.0.67", 25, (SecureSocketOptions) 1, new CancellationToken());
          ((MailService) smtpClient).Authenticate("providerportal", "pro1234*", new CancellationToken());
          ((MailTransport) smtpClient).Send(mimeMessage1, new CancellationToken(), (ITransferProgress) null);
          ((MailService) smtpClient).Disconnect(true, new CancellationToken());
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
      return (ActionResult) this.Json((object) new
      {
        Result = "S"
      }, (JsonRequestBehavior) 0);
    }

    public ProviderController()
    {
      base.\u002Ector();
    }
  }
}
