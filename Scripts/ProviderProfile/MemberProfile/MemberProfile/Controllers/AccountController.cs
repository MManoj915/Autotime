// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.AccountController
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
  public class AccountController : Controller
  {
    private ApplicationSignInManager _signInManager;
    private ApplicationUserManager _userManager;
    private const string XsrfKey = "XsrfId";

    public AccountController()
    {
      base.\u002Ector();
    }

    public AccountController(
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

    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReturnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__10.\u003C\u003Ep__0, ((ControllerBase) this).get_ViewBag(), returnUrl);
      return (ActionResult) this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
      if (!this.get_ModelState().get_IsValid())
        return (ActionResult) this.View((object) model);
      SignInStatus result = await this.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
      switch ((int) result)
      {
        case 0:
          return this.RedirectToLocal(returnUrl);
        case 1:
          return (ActionResult) this.View("Lockout");
        case 2:
          return (ActionResult) this.RedirectToAction("SendCode", (object) new
          {
            ReturnUrl = returnUrl,
            RememberMe = model.RememberMe
          });
        default:
          this.get_ModelState().AddModelError("", "Invalid login attempt.");
          return (ActionResult) this.View((object) model);
      }
    }

    [AllowAnonymous]
    public async Task<ActionResult> VerifyCode(
      string provider,
      string returnUrl,
      bool rememberMe)
    {
      bool flag = await this.SignInManager.HasBeenVerifiedAsync();
      if (!flag)
        return (ActionResult) this.View("Error");
      return (ActionResult) this.View((object) new VerifyCodeViewModel()
      {
        Provider = provider,
        ReturnUrl = returnUrl,
        RememberMe = rememberMe
      });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
    {
      if (!this.get_ModelState().get_IsValid())
        return (ActionResult) this.View((object) model);
      SignInStatus result = await this.SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
      switch ((int) result)
      {
        case 0:
          return this.RedirectToLocal(model.ReturnUrl);
        case 1:
          return (ActionResult) this.View("Lockout");
        default:
          this.get_ModelState().AddModelError("", "Invalid code.");
          return (ActionResult) this.View((object) model);
      }
    }

    [AllowAnonymous]
    public ActionResult Register()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      if (this.get_ModelState().get_IsValid())
      {
        ApplicationUser applicationUser = new ApplicationUser();
        ((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) applicationUser).set_UserName(model.Email);
        ((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) applicationUser).set_Email(model.Email);
        ApplicationUser user = applicationUser;
        IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).CreateAsync(user, model.Password);
        if (result.get_Succeeded())
        {
          await this.SignInManager.SignInAsync(user, false, false);
          return (ActionResult) this.RedirectToAction("Index", "Home");
        }
        this.AddErrors(result);
        user = (ApplicationUser) null;
        result = (IdentityResult) null;
      }
      return (ActionResult) this.View((object) model);
    }

    [AllowAnonymous]
    public async Task<ActionResult> ConfirmEmail(string userId, string code)
    {
      if (userId == null || code == null)
        return (ActionResult) this.View("Error");
      IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).ConfirmEmailAsync(userId, code);
      return (ActionResult) this.View(result.get_Succeeded() ? nameof (ConfirmEmail) : "Error");
    }

    [AllowAnonymous]
    public ActionResult ForgotPassword()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
      if (this.get_ModelState().get_IsValid())
      {
        ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByNameAsync(model.Email);
        bool flag1 = user == null;
        if (!flag1)
        {
          bool flag2 = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).IsEmailConfirmedAsync(((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) user).get_Id());
          flag1 = !flag2;
        }
        if (flag1)
          return (ActionResult) this.View("ForgotPasswordConfirmation");
        user = (ApplicationUser) null;
      }
      return (ActionResult) this.View((object) model);
    }

    [AllowAnonymous]
    public ActionResult ForgotPasswordConfirmation()
    {
      return (ActionResult) this.View();
    }

    [AllowAnonymous]
    public ActionResult ResetPassword(string code)
    {
      return code == null ? (ActionResult) this.View("Error") : (ActionResult) this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
    {
      if (!this.get_ModelState().get_IsValid())
        return (ActionResult) this.View((object) model);
      ApplicationUser user = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).FindByNameAsync(model.Email);
      if (user == null)
        return (ActionResult) this.RedirectToAction("ResetPasswordConfirmation", "Account");
      IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).ResetPasswordAsync(((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) user).get_Id(), model.Code, model.Password);
      if (result.get_Succeeded())
        return (ActionResult) this.RedirectToAction("ResetPasswordConfirmation", "Account");
      this.AddErrors(result);
      return (ActionResult) this.View();
    }

    [AllowAnonymous]
    public ActionResult ResetPasswordConfirmation()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      return (ActionResult) new AccountController.ChallengeResult(provider, this.get_Url().Action("ExternalLoginCallback", "Account", (object) new
      {
        ReturnUrl = returnUrl
      }));
    }

    [AllowAnonymous]
    public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
    {
      string userId = await this.SignInManager.GetVerifiedUserIdAsync();
      if (userId == null)
        return (ActionResult) this.View("Error");
      IList<string> userFactors = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).GetValidTwoFactorProvidersAsync(userId);
      List<SelectListItem> factorOptions = userFactors.Select<string, SelectListItem>((Func<string, SelectListItem>) (purpose =>
      {
        SelectListItem selectListItem = new SelectListItem();
        selectListItem.set_Text(purpose);
        selectListItem.set_Value(purpose);
        return selectListItem;
      })).ToList<SelectListItem>();
      return (ActionResult) this.View((object) new SendCodeViewModel()
      {
        Providers = (ICollection<SelectListItem>) factorOptions,
        ReturnUrl = returnUrl,
        RememberMe = rememberMe
      });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> SendCode(SendCodeViewModel model)
    {
      if (!this.get_ModelState().get_IsValid())
        return (ActionResult) this.View();
      bool flag = await this.SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider);
      return flag ? (ActionResult) this.RedirectToAction("VerifyCode", (object) new
      {
        Provider = model.SelectedProvider,
        ReturnUrl = model.ReturnUrl,
        RememberMe = model.RememberMe
      }) : (ActionResult) this.View("Error");
    }

    [AllowAnonymous]
    public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
    {
      ExternalLoginInfo loginInfo = await AuthenticationManagerExtensions.GetExternalLoginInfoAsync(this.AuthenticationManager);
      if (loginInfo == null)
        return (ActionResult) this.RedirectToAction("Login");
      SignInStatus result = await this.SignInManager.ExternalSignInAsync(loginInfo, false);
      switch ((int) result)
      {
        case 0:
          return this.RedirectToLocal(returnUrl);
        case 1:
          return (ActionResult) this.View("Lockout");
        case 2:
          return (ActionResult) this.RedirectToAction("SendCode", (object) new
          {
            ReturnUrl = returnUrl,
            RememberMe = false
          });
        default:
          // ISSUE: reference to a compiler-generated field
          if (AccountController.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountController.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReturnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = AccountController.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__26.\u003C\u003Ep__0, ((ControllerBase) this).get_ViewBag(), returnUrl);
          // ISSUE: reference to a compiler-generated field
          if (AccountController.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountController.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LoginProvider", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = AccountController.\u003C\u003Eo__26.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__26.\u003C\u003Ep__1, ((ControllerBase) this).get_ViewBag(), loginInfo.get_Login().get_LoginProvider());
          return (ActionResult) this.View("ExternalLoginConfirmation", (object) new ExternalLoginConfirmationViewModel()
          {
            Email = loginInfo.get_Email()
          });
      }
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ExternalLoginConfirmation(
      ExternalLoginConfirmationViewModel model,
      string returnUrl)
    {
      if (this.get_User().Identity.IsAuthenticated)
        return (ActionResult) this.RedirectToAction("Index", "Manage");
      if (this.get_ModelState().get_IsValid())
      {
        ExternalLoginInfo info = await AuthenticationManagerExtensions.GetExternalLoginInfoAsync(this.AuthenticationManager);
        if (info == null)
          return (ActionResult) this.View("ExternalLoginFailure");
        ApplicationUser applicationUser = new ApplicationUser();
        ((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) applicationUser).set_UserName(model.Email);
        ((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) applicationUser).set_Email(model.Email);
        ApplicationUser user = applicationUser;
        IdentityResult result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).CreateAsync(user);
        if (result.get_Succeeded())
        {
          result = await ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this.UserManager).AddLoginAsync(((IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>) user).get_Id(), info.get_Login());
          if (result.get_Succeeded())
          {
            await this.SignInManager.SignInAsync(user, false, false);
            return this.RedirectToLocal(returnUrl);
          }
        }
        this.AddErrors(result);
        info = (ExternalLoginInfo) null;
        user = (ApplicationUser) null;
        result = (IdentityResult) null;
      }
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__27.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__27.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReturnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__27.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__27.\u003C\u003Ep__0, ((ControllerBase) this).get_ViewBag(), returnUrl);
      return (ActionResult) this.View((object) model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LogOff()
    {
      this.AuthenticationManager.SignOut(new string[1]
      {
        "ApplicationCookie"
      });
      return (ActionResult) this.RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public ActionResult ExternalLoginFailure()
    {
      return (ActionResult) this.View();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this._userManager != null)
        {
          ((Microsoft.AspNet.Identity.UserManager<ApplicationUser, string>) this._userManager).Dispose();
          this._userManager = (ApplicationUserManager) null;
        }
        if (this._signInManager != null)
        {
          this._signInManager.Dispose();
          this._signInManager = (ApplicationSignInManager) null;
        }
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

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (this.get_Url().IsLocalUrl(returnUrl))
        return (ActionResult) this.Redirect(returnUrl);
      return (ActionResult) this.RedirectToAction("Index", "Home");
    }

    internal class ChallengeResult : HttpUnauthorizedResult
    {
      public ChallengeResult(string provider, string redirectUri)
        : this(provider, redirectUri, (string) null)
      {
      }

      public ChallengeResult(string provider, string redirectUri, string userId)
      {
        this.\u002Ector();
        this.LoginProvider = provider;
        this.RedirectUri = redirectUri;
        this.UserId = userId;
      }

      public string LoginProvider { get; set; }

      public string RedirectUri { get; set; }

      public string UserId { get; set; }

      public virtual void ExecuteResult(ControllerContext context)
      {
        AuthenticationProperties authenticationProperties1 = new AuthenticationProperties();
        authenticationProperties1.set_RedirectUri(this.RedirectUri);
        AuthenticationProperties authenticationProperties2 = authenticationProperties1;
        if (this.UserId != null)
          authenticationProperties2.get_Dictionary()["XsrfId"] = this.UserId;
        HttpContextBaseExtensions.GetOwinContext(context.get_HttpContext()).get_Authentication().Challenge(authenticationProperties2, new string[1]
        {
          this.LoginProvider
        });
      }
    }
  }
}
