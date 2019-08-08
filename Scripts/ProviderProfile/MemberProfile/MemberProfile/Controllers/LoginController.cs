// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.LoginController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MemberProfile.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class LoginController : Controller
  {
    private IDbConnection _db;

    public ActionResult Index()
    {
      LoginModel loginModel = this.CheckCookie();
      if (loginModel == null)
        return (ActionResult) this.View((object) loginModel);
      return this.CheckProviderCredential();
    }

    [HttpPost]
    public ActionResult ValidateCredential(LoginModel model)
    {
      if (this.get_ModelState().get_IsValid())
      {
        if (string.IsNullOrEmpty(model.Password))
        {
          ((ControllerBase) this).get_TempData().set_Item("PasswordError", (object) "Please enter a valid password");
          return (ActionResult) this.RedirectToAction("Index", "Login");
        }
        if (string.IsNullOrEmpty(model.UserName))
        {
          ((ControllerBase) this).get_TempData().set_Item("UserNameError", (object) "Please enter a valid username");
          return (ActionResult) this.RedirectToAction("Index", "Login");
        }
        string pattern = "^[A-Za-z0-9\\[\\]/!$%^&*()\\-_+{};:£@#.?]*$";
        if (!Regex.IsMatch(model.Password, pattern) || !Regex.IsMatch(model.UserName, pattern))
        {
          if (!Regex.IsMatch(model.Password, pattern))
            ((ControllerBase) this).get_TempData().set_Item("PasswordError", (object) "Please enter a valid password");
          if (!Regex.IsMatch(model.UserName, pattern))
            ((ControllerBase) this).get_TempData().set_Item("UserNameError", (object) "Please enter a valid username");
          return (ActionResult) this.RedirectToAction("Index", "Login");
        }
        if (model.Remember)
        {
          HttpCookie cookie1 = new HttpCookie("UserName");
          HttpCookie httpCookie1 = cookie1;
          DateTime now = DateTime.Now;
          DateTime dateTime1 = now.AddYears(1);
          httpCookie1.Expires = dateTime1;
          cookie1.Value = model.UserName;
          this.get_Response().Cookies.Add(cookie1);
          HttpCookie cookie2 = new HttpCookie("Password");
          HttpCookie httpCookie2 = cookie2;
          now = DateTime.Now;
          DateTime dateTime2 = now.AddYears(1);
          httpCookie2.Expires = dateTime2;
          cookie2.Value = model.Password;
          this.get_Response().Cookies.Add(cookie2);
        }
        else
        {
          HttpCookie cookie1 = new HttpCookie("UserName");
          HttpCookie httpCookie1 = cookie1;
          DateTime now = DateTime.Now;
          DateTime dateTime1 = now.AddMinutes(20.0);
          httpCookie1.Expires = dateTime1;
          cookie1.Value = model.UserName;
          this.get_Response().Cookies.Add(cookie1);
          HttpCookie cookie2 = new HttpCookie("Password");
          HttpCookie httpCookie2 = cookie2;
          now = DateTime.Now;
          DateTime dateTime2 = now.AddMinutes(20.0);
          httpCookie2.Expires = dateTime2;
          cookie2.Value = model.Password;
          this.get_Response().Cookies.Add(cookie2);
        }
        List<Provider> providerList = (List<Provider>) SqlMapper.AsList<Provider>(SqlMapper.Query<Provider>(this._db, " SELECT PROVIDERCODE ID,PROVIDERID,PROVIDERNAME,  PROVIDERMAINPHONE,PRO.PROVIDERMAINEMAIL EMAIL,PRO.ADDRESS,GEN.COUNTRYNAME,  GEN1.PROVINCENAME,PRO.PROVIDERAREA FROM IM_PROVIDERS PRO  LEFT JOIN  GENCOUNTRY GEN ON GEN.COUNTRYCODE = PRO.COUNTRYCODE  LEFT JOIN GENPROVINCE GEN1 ON GEN1.PROVINCECODE = PRO.CITYCODE Where  PRO.USERNAME = '" + model.UserName + "' and  PRO.PASSWORD ='" + model.Password + "' ", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
        if (providerList.Count > 0)
        {
          this.get_Session()["ProviderID"] = (object) providerList[0].ProviderID;
          this.get_Session()["ProviderName"] = (object) providerList[0].ProviderName;
          this.get_Session()["ProviderCode"] = (object) providerList[0].ID;
          this.get_Session()["ProviderDetails"] = (object) providerList[0];
          return (ActionResult) this.RedirectToAction("Index", "Provider");
        }
      }
      return (ActionResult) this.RedirectToAction("Index", "Login");
    }

    public ActionResult CheckProviderCredential()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (this.get_Request().Cookies["UserName"] != null && this.get_Request().Cookies["UserName"].Value != null)
        empty1 = this.get_Request().Cookies["UserName"].Value.ToString();
      if (this.get_Request().Cookies["Password"] != null && this.get_Request().Cookies["Password"].Value != null)
        empty2 = this.get_Request().Cookies["Password"].Value.ToString();
      List<Provider> providerList = (List<Provider>) SqlMapper.AsList<Provider>(SqlMapper.Query<Provider>(this._db, " SELECT PROVIDERCODE ID,PROVIDERID,PROVIDERNAME,  PROVIDERMAINPHONE,PRO.PROVIDERMAINEMAIL EMAIL,PRO.ADDRESS,GEN.COUNTRYNAME,  GEN1.PROVINCENAME,PRO.PROVIDERAREA FROM IM_PROVIDERS PRO  LEFT JOIN  GENCOUNTRY GEN ON GEN.COUNTRYCODE = PRO.COUNTRYCODE  LEFT JOIN GENPROVINCE GEN1 ON GEN1.PROVINCECODE = PRO.CITYCODE Where  PRO.USERNAME = '" + empty1 + "' and  PRO.PASSWORD ='" + empty2 + "' ", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
      if (providerList.Count <= 0)
        return (ActionResult) this.RedirectToAction("Index", "Provider");
      this.get_Session()["ProviderCode"] = (object) providerList[0].ID;
      this.get_Session()["ProviderDetails"] = (object) providerList[0];
      return (ActionResult) this.RedirectToAction("Index", "Provider");
    }

    public bool VerifyAccount(string UserName, string Password)
    {
      return ((List<Provider>) SqlMapper.AsList<Provider>(SqlMapper.Query<Provider>(this._db, " SELECT PROVIDERCODE ID,PROVIDERID,PROVIDERNAME,  PROVIDERMAINPHONE,PRO.PROVIDERMAINEMAIL EMAIL,PRO.ADDRESS,GEN.COUNTRYNAME,  GEN1.PROVINCENAME,PRO.PROVIDERAREA FROM IM_PROVIDERS PRO  LEFT JOIN  GENCOUNTRY GEN ON GEN.COUNTRYCODE = PRO.COUNTRYCODE  LEFT JOIN GENPROVINCE GEN1 ON GEN1.PROVINCECODE = PRO.CITYCODE Where  PRO.USERNAME = '" + UserName + "' and  PRO.PASSWORD ='" + Password + "' ", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()))).Count > 0;
    }

    public ActionResult LogOut()
    {
      this.get_Session().Abandon();
      if (this.get_Response().Cookies["UserName"] != null)
        this.get_Response().Cookies.Add(new HttpCookie("UserName")
        {
          Value = (string) null,
          Expires = DateTime.Now.AddYears(-1)
        });
      if (this.get_Response().Cookies["Password"] != null)
        this.get_Response().Cookies.Add(new HttpCookie("Password")
        {
          Value = (string) null,
          Expires = DateTime.Now.AddYears(-1)
        });
      return (ActionResult) this.RedirectToAction("Index", "Login");
    }

    public LoginModel CheckCookie()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      LoginModel loginModel = (LoginModel) null;
      if (this.get_Request().Cookies["UserName"] != null && this.get_Request().Cookies["UserName"].Value != null)
        empty1 = this.get_Request().Cookies["UserName"].Value.ToString();
      if (this.get_Request().Cookies["Password"] != null && this.get_Request().Cookies["Password"].Value != null)
        empty2 = this.get_Request().Cookies["Password"].Value.ToString();
      if (!string.IsNullOrEmpty(empty1) && !string.IsNullOrEmpty(empty2) && this.VerifyAccount(empty1, empty2))
        loginModel = new LoginModel()
        {
          UserName = empty1,
          Password = empty2
        };
      return loginModel;
    }

    public LoginController()
    {
      base.\u002Ector();
    }
  }
}
