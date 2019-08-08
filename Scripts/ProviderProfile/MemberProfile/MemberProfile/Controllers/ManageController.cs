// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.ManageController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using MemberProfile.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  [Authorize]
  public class ManageController : Controller
  {
    private ApplicationSignInManager _signInManager;
    private ApplicationUserManager _userManager;
    private const string XsrfKey = "XsrfId";

    public ManageController()
    {
      base.\u002Ector();
    }

    public ManageController(
      ApplicationUserManager userManager,
      ApplicationSignInManager signInManager)
    {
      base.\u002Ector();
      this.UserManager = userManager;
      this.SignInManager = signInManager;
    }

    public ApplicationSignInManager SignInManager
    {
      get
      {
        return this._signInManager ?? (ApplicationSignInManager) OwinContextExtensions.Get<ApplicationSignInManager>(HttpContextBaseExtensions.GetOwinContext(this.get_HttpContext()));
      }
      private set
      {
        this._signInManager = value;
      }
    }

    public ApplicationUserManager UserManager
    {
      get
      {
        return this._userManager ?? (ApplicationUserManager) OwinContextExtensions.GetUserManager<ApplicationUserManager>(HttpContextBaseExtensions.GetOwinContext(this.get_HttpContext()));
      }
      private set
      {
        this._userManager = value;
      }
    }

    public async Task<ActionResult> Index(ManageController.ManageMessageId? message)
    {
      // ISSUE: reference to a compiler-generated field
      if (ManageController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ManageController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "StatusMessage", typeof (ManageController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target = ManageController.\u003C\u003Eo__10.\u003C\u003Ep__0.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p0 = ManageController.\u003C\u003Eo__10.\u003C\u003Ep__0;
      object viewBag = ((ControllerBase) this).get_ViewBag();
      ManageController.ManageMessageId? nullable = message;
      ManageController.ManageMessageId manageMessageId1 = ManageController.ManageMessageId.ChangePasswordSuccess;
      string str1;
      if ((nullable.GetValueOrDefault() == manageMessageId1 ? (nullable.HasValue ? 1 : 0) : 0) == 0)
      {
        nullable = message;
        ManageController.ManageMessageId manageMessageId2 = ManageController.ManageMessageId.SetPasswordSuccess;
        if ((nullable.GetValueOrDefault() == manageMessageId2 ? (nullable.HasValue ? 1 : 0) : 0) == 0)
        {
          nullable = message;
          ManageController.ManageMessageId manageMessageId3 = ManageController.ManageMessageId.SetTwoFactorSuccess;
          if ((nullable.GetValueOrDefault() == manageMessageId3 ? (nullable.HasValue ? 1 : 0) : 0) == 0)
          {
            nullable = message;
            ManageController.ManageMessageId manageMessageId4 = ManageController.ManageMessageId.Error;
            if ((nullable.GetValueOrDefault() == manageMessageId4 ? (nullable.HasValue ? 1 : 0) : 0) == 0)
            {
              nullable = message;
              ManageController.ManageMessageId manageMessageId5 = ManageController.ManageMessageId.AddPhoneSuccess;
              if ((nullable.GetValueOrDefault() == manageMessageId5 ? (nullable.HasValue ? 1 : 0) : 0) == 0)
              {
                nullable = message;
                ManageController.ManageMessageId manageMessageId6 = ManageController.ManageMessageId.RemovePhoneSuccess;
                str1 = (nullable.GetValueOrDefault() == manageMessageId6 ? (nullable.HasValue ? 1 : 0) : 0) != 0 ? "Your phone number was removed." : "";
              }
              else
                str1 = "Your phone number was added.";
            }
            else
              str1 = "An error has occurred.";
          }
          else
            str1 = "Your two-factor authentication provider has been set.";
        }
        else
          str1 = "Your password has been set.";
      }
      else
        str1 = "Your password has been changed.";
      object obj = target((CallSite) p0, viewBag, str1);
      string userId = IdentityExtensions.GetUserId(this.get_User().Identity);
      IndexViewModel indexViewModel1 = new IndexViewModel();
      indexViewModel1.HasPassword = this.HasPassword();
      IndexViewModel indexViewModel2 = indexViewModel1;
      string str = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).GetPhoneNumberAsync(userId);
      indexViewModel2.PhoneNumber = str;
      IndexViewModel indexViewModel3 = indexViewModel1;
      bool factorEnabledAsync = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).GetTwoFactorEnabledAsync(userId);
      indexViewModel3.TwoFactor = factorEnabledAsync;
      IndexViewModel indexViewModel4 = indexViewModel1;
      IList<UserLoginInfo> userLoginInfoList = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).GetLoginsAsync(userId);
      indexViewModel4.Logins = userLoginInfoList;
      IndexViewModel indexViewModel5 = indexViewModel1;
      bool flag = await AuthenticationManagerExtensions.TwoFactorBrowserRememberedAsync(this.AuthenticationManager, userId);
      indexViewModel5.BrowserRemembered = flag;
      IndexViewModel model = indexViewModel1;
      indexViewModel2 = (IndexViewModel) null;
      str = (string) null;
      indexViewModel3 = (IndexViewModel) null;
      indexViewModel4 = (IndexViewModel) null;
      userLoginInfoList = (IList<UserLoginInfo>) null;
      indexViewModel5 = (IndexViewModel) null;
      indexViewModel1 = (IndexViewModel) null;
      return (ActionResult) this.View((object) model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> RemoveLogin(
      string loginProvider,
      string providerKey)
    {
      IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).RemoveLoginAsync(IdentityExtensions.GetUserId(this.get_User().Identity), new UserLoginInfo(loginProvider, providerKey));
      ManageController.ManageMessageId? message;
      if (result.get_Succeeded())
      {
        ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
        if (user != null)
          await this.SignInManager.SignInAsync(user, false, false);
        message = new ManageController.ManageMessageId?(ManageController.ManageMessageId.RemoveLoginSuccess);
        user = (ApplicationUser) null;
      }
      else
        message = new ManageController.ManageMessageId?(ManageController.ManageMessageId.Error);
      return (ActionResult) this.RedirectToAction("ManageLogins", (object) new
      {
        Message = message
      });
    }

    public ActionResult AddPhoneNumber()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
    {
      if (!this.get_ModelState().get_IsValid())
        return (ActionResult) this.View((object) model);
      string code = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).GenerateChangePhoneNumberTokenAsync(IdentityExtensions.GetUserId(this.get_User().Identity), model.Number);
      if (((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).get_SmsService() != null)
      {
        IdentityMessage identityMessage = new IdentityMessage();
        identityMessage.set_Destination(model.Number);
        identityMessage.set_Body("Your security code is: " + code);
        IdentityMessage message = identityMessage;
        await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).get_SmsService().SendAsync(message);
        message = (IdentityMessage) null;
      }
      return (ActionResult) this.RedirectToAction("VerifyPhoneNumber", (object) new
      {
        PhoneNumber = model.Number
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EnableTwoFactorAuthentication()
    {
      IdentityResult identityResult = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).SetTwoFactorEnabledAsync(IdentityExtensions.GetUserId(this.get_User().Identity), true);
      ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
      if (user != null)
        await this.SignInManager.SignInAsync(user, false, false);
      return (ActionResult) this.RedirectToAction("Index", "Manage");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DisableTwoFactorAuthentication()
    {
      IdentityResult identityResult = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).SetTwoFactorEnabledAsync(IdentityExtensions.GetUserId(this.get_User().Identity), false);
      ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
      if (user != null)
        await this.SignInManager.SignInAsync(user, false, false);
      return (ActionResult) this.RedirectToAction("Index", "Manage");
    }

    public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
    {
      string code = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).GenerateChangePhoneNumberTokenAsync(IdentityExtensions.GetUserId(this.get_User().Identity), phoneNumber);
      ViewResult viewResult;
      if (phoneNumber != null)
        viewResult = this.View((object) new VerifyPhoneNumberViewModel()
        {
          PhoneNumber = phoneNumber
        });
      else
        viewResult = this.View("Error");
      return (ActionResult) viewResult;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> VerifyPhoneNumber(
      VerifyPhoneNumberViewModel model)
    {
      if (!this.get_ModelState().get_IsValid())
        return (ActionResult) this.View((object) model);
      IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).ChangePhoneNumberAsync(IdentityExtensions.GetUserId(this.get_User().Identity), model.PhoneNumber, model.Code);
      if (result.get_Succeeded())
      {
        ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
        if (user != null)
          await this.SignInManager.SignInAsync(user, false, false);
        return (ActionResult) this.RedirectToAction("Index", (object) new
        {
          Message = ManageController.ManageMessageId.AddPhoneSuccess
        });
      }
      this.get_ModelState().AddModelError("", "Failed to verify phone");
      return (ActionResult) this.View((object) model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> RemovePhoneNumber()
    {
      IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).SetPhoneNumberAsync(IdentityExtensions.GetUserId(this.get_User().Identity), (string) null);
      if (!result.get_Succeeded())
        return (ActionResult) this.RedirectToAction("Index", (object) new
        {
          Message = ManageController.ManageMessageId.Error
        });
      ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
      if (user != null)
        await this.SignInManager.SignInAsync(user, false, false);
      return (ActionResult) this.RedirectToAction("Index", (object) new
      {
        Message = ManageController.ManageMessageId.RemovePhoneSuccess
      });
    }

    public ActionResult ChangePassword()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
    {
      if (!this.get_ModelState().get_IsValid())
        return (ActionResult) this.View((object) model);
      IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).ChangePasswordAsync(IdentityExtensions.GetUserId(this.get_User().Identity), model.OldPassword, model.NewPassword);
      if (result.get_Succeeded())
      {
        ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
        if (user != null)
          await this.SignInManager.SignInAsync(user, false, false);
        return (ActionResult) this.RedirectToAction("Index", (object) new
        {
          Message = ManageController.ManageMessageId.ChangePasswordSuccess
        });
      }
      this.AddErrors(result);
      return (ActionResult) this.View((object) model);
    }

    public ActionResult SetPassword()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
    {
      if (this.get_ModelState().get_IsValid())
      {
        IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).AddPasswordAsync(IdentityExtensions.GetUserId(this.get_User().Identity), model.NewPassword);
        if (result.get_Succeeded())
        {
          ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
          if (user != null)
            await this.SignInManager.SignInAsync(user, false, false);
          return (ActionResult) this.RedirectToAction("Index", (object) new
          {
            Message = ManageController.ManageMessageId.SetPasswordSuccess
          });
        }
        this.AddErrors(result);
        result = (IdentityResult) null;
      }
      return (ActionResult) this.View((object) model);
    }

    public async Task<ActionResult> ManageLogins(
      ManageController.ManageMessageId? message)
    {
      // ISSUE: reference to a compiler-generated field
      if (ManageController.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ManageController.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "StatusMessage", typeof (ManageController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target = ManageController.\u003C\u003Eo__23.\u003C\u003Ep__0.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p0 = ManageController.\u003C\u003Eo__23.\u003C\u003Ep__0;
      object viewBag = ((ControllerBase) this).get_ViewBag();
      ManageController.ManageMessageId? nullable = message;
      ManageController.ManageMessageId manageMessageId1 = ManageController.ManageMessageId.RemoveLoginSuccess;
      string str;
      if ((nullable.GetValueOrDefault() == manageMessageId1 ? (nullable.HasValue ? 1 : 0) : 0) == 0)
      {
        nullable = message;
        ManageController.ManageMessageId manageMessageId2 = ManageController.ManageMessageId.Error;
        str = (nullable.GetValueOrDefault() == manageMessageId2 ? (nullable.HasValue ? 1 : 0) : 0) != 0 ? "An error has occurred." : "";
      }
      else
        str = "The external login was removed.";
      object obj1 = target((CallSite) p0, viewBag, str);
      ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByIdAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
      if (user == null)
        return (ActionResult) this.View("Error");
      IList<UserLoginInfo> userLogins;
      ref IList<UserLoginInfo> local = ref userLogins;
      userLogins = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).GetLoginsAsync(IdentityExtensions.GetUserId(this.get_User().Identity));
      List<AuthenticationDescription> otherLogins = AuthenticationManagerExtensions.GetExternalAuthenticationTypes(this.AuthenticationManager).Where<AuthenticationDescription>((Func<AuthenticationDescription, bool>) (auth => ((IEnumerable<UserLoginInfo>) userLogins).All<UserLoginInfo>((Func<UserLoginInfo, bool>) (ul => auth.get_AuthenticationType() != ul.get_LoginProvider())))).ToList<AuthenticationDescription>();
      // ISSUE: reference to a compiler-generated field
      if (ManageController.\u003C\u003Eo__23.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ManageController.\u003C\u003Eo__23.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowRemoveButton", typeof (ManageController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = ManageController.\u003C\u003Eo__23.\u003C\u003Ep__1.Target((CallSite) ManageController.\u003C\u003Eo__23.\u003C\u003Ep__1, ((ControllerBase) this).get_ViewBag(), ((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) user).get_PasswordHash() != null || ((ICollection<UserLoginInfo>) userLogins).Count > 1);
      return (ActionResult) this.View((object) new ManageLoginsViewModel()
      {
        CurrentLogins = userLogins,
        OtherLogins = (IList<AuthenticationDescription>) otherLogins
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LinkLogin(string provider)
    {
      return (ActionResult) new AccountController.ChallengeResult(provider, this.get_Url().Action("LinkLoginCallback", "Manage"), IdentityExtensions.GetUserId(this.get_User().Identity));
    }

    public async Task<ActionResult> LinkLoginCallback()
    {
      ExternalLoginInfo loginInfo = await AuthenticationManagerExtensions.GetExternalLoginInfoAsync(this.AuthenticationManager, "XsrfId", IdentityExtensions.GetUserId(this.get_User().Identity));
      if (loginInfo == null)
        return (ActionResult) this.RedirectToAction("ManageLogins", (object) new
        {
          Message = ManageController.ManageMessageId.Error
        });
      IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).AddLoginAsync(IdentityExtensions.GetUserId(this.get_User().Identity), loginInfo.get_Login());
      return result.get_Succeeded() ? (ActionResult) this.RedirectToAction("ManageLogins") : (ActionResult) this.RedirectToAction("ManageLogins", (object) new
      {
        Message = ManageController.ManageMessageId.Error
      });
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing && this._userManager != null)
      {
        ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this._userManager).Dispose();
        this._userManager = (ApplicationUserManager) null;
      }
      base.Dispose(disposing);
    }

    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContextBaseExtensions.GetOwinContext(this.get_HttpContext()).get_Authentication();
      }
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (string error in result.get_Errors())
        this.get_ModelState().AddModelError("", error);
    }

    private bool HasPassword()
    {
      ApplicationUser byId = (ApplicationUser) UserManagerExtensions.FindById<ApplicationUser, string>((Microsoft.AspNet.Identity.UserManager<M0, M1>) this.UserManager, (M1) IdentityExtensions.GetUserId(this.get_User().Identity));
      if (byId != null)
        return ((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) byId).get_PasswordHash() != null;
      return false;
    }

    private bool HasPhoneNumber()
    {
      ApplicationUser byId = (ApplicationUser) UserManagerExtensions.FindById<ApplicationUser, string>((Microsoft.AspNet.Identity.UserManager<M0, M1>) this.UserManager, (M1) IdentityExtensions.GetUserId(this.get_User().Identity));
      if (byId != null)
        return ((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) byId).get_PhoneNumber() != null;
      return false;
    }

    public enum ManageMessageId
    {
      AddPhoneSuccess,
      ChangePasswordSuccess,
      SetTwoFactorSuccess,
      SetPasswordSuccess,
      RemoveLoginSuccess,
      RemovePhoneSuccess,
      Error,
    }
  }
}
