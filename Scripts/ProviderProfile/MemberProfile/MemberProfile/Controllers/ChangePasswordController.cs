// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.ChangePasswordController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MemberProfile.Models;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class ChangePasswordController : Controller
  {
    private IDbConnection _db;

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult ChangePassword(ChangePassword model)
    {
      if (string.IsNullOrEmpty(model.NewPassword))
      {
        ((ControllerBase) this).get_TempData().set_Item("NewPasswordError", (object) "Please enter a valid password");
        return (ActionResult) this.RedirectToAction("Index", "Login");
      }
      if (string.IsNullOrEmpty(model.ConfirmPassword))
      {
        ((ControllerBase) this).get_TempData().set_Item("NewPasswordError", (object) "Please enter a valid new Confirmation password");
        return (ActionResult) this.RedirectToAction("Index", "Login");
      }
      if (string.IsNullOrEmpty(model.ConfirmPassword))
      {
        ((ControllerBase) this).get_TempData().set_Item("NewPasswordError", (object) "Current Password Supplied is invalid");
        return (ActionResult) this.RedirectToAction("Index", nameof (ChangePassword));
      }
      string pattern = "^[A-Za-z0-9\\[\\]/!$%^&*()\\-_+{};:£@#.?]*$";
      if (!Regex.IsMatch(model.NewPassword, pattern) || !Regex.IsMatch(model.ConfirmPassword, pattern))
      {
        if (!Regex.IsMatch(model.NewPassword, pattern))
          ((ControllerBase) this).get_TempData().set_Item("NewPasswordError", (object) "Please enter a valid new password");
        if (!Regex.IsMatch(model.ConfirmPassword, pattern))
          ((ControllerBase) this).get_TempData().set_Item("NewPasswordError", (object) "Please enter a valid new Confirmation password");
        return (ActionResult) this.RedirectToAction("Index", nameof (ChangePassword));
      }
      if (model.NewPassword != model.ConfirmPassword)
      {
        ((ControllerBase) this).get_TempData().set_Item("NewPasswordError", (object) "The New Password and Confirmation Password fields must match");
        return (ActionResult) this.RedirectToAction("Index", nameof (ChangePassword));
      }
      if (model.OldPassword != this.get_Request().Cookies["Password"].Value)
      {
        ((ControllerBase) this).get_TempData().set_Item("NewPasswordError", (object) "Current Password Supplied is invalid");
        return (ActionResult) this.RedirectToAction("Index", nameof (ChangePassword));
      }
      long int64 = Convert.ToInt64(this.get_Session()["ProviderCode"]);
      if (int64 == 0L)
      {
        LoginController loginController = new LoginController();
        if (loginController.CheckCookie() == null)
          return (ActionResult) this.RedirectToAction("Index", "Login");
        loginController.CheckProviderCredential();
      }
      SqlMapper.Execute(this._db, "Update IM_Providers Set Password = '" + model.NewPassword + "' Where ProviderCode = " + (object) int64, (object) null, (IDbTransaction) null, new int?(), new CommandType?());
      this.get_Request().Cookies["Password"].Value = model.NewPassword;
      return (ActionResult) this.RedirectToAction("Index", "Provider");
    }

    public ChangePasswordController()
    {
      base.\u002Ector();
    }
  }
}
