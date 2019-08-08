// Decompiled with JetBrains decompiler
// Type: MemberProfile.Startup
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using MemberProfile.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MemberProfile
{
  public class Startup
  {
    public void ConfigureAuth(IAppBuilder app)
    {
      AppBuilderExtensions.CreatePerOwinContext<ApplicationDbContext>(app, (Func<M0>) new Func<ApplicationDbContext>(ApplicationDbContext.Create));
      AppBuilderExtensions.CreatePerOwinContext<ApplicationUserManager>(app, (Func<IdentityFactoryOptions<M0>, IOwinContext, M0>) new Func<IdentityFactoryOptions<ApplicationUserManager>, IOwinContext, ApplicationUserManager>(ApplicationUserManager.Create));
      AppBuilderExtensions.CreatePerOwinContext<ApplicationSignInManager>(app, (Func<IdentityFactoryOptions<M0>, IOwinContext, M0>) new Func<IdentityFactoryOptions<ApplicationSignInManager>, IOwinContext, ApplicationSignInManager>(ApplicationSignInManager.Create));
      IAppBuilder iappBuilder = app;
      CookieAuthenticationOptions authenticationOptions1 = new CookieAuthenticationOptions();
      ((AuthenticationOptions) authenticationOptions1).set_AuthenticationType("ApplicationCookie");
      authenticationOptions1.set_LoginPath(new PathString("/Account/Login"));
      CookieAuthenticationOptions authenticationOptions2 = authenticationOptions1;
      CookieAuthenticationProvider authenticationProvider1 = new CookieAuthenticationProvider();
      authenticationProvider1.set_OnValidateIdentity(SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(TimeSpan.FromMinutes(30.0), (Func<M0, M1, Task<ClaimsIdentity>>) ((manager, user) => user.GenerateUserIdentityAsync((UserManager<ApplicationUser>) manager))));
      CookieAuthenticationProvider authenticationProvider2 = authenticationProvider1;
      authenticationOptions2.set_Provider((ICookieAuthenticationProvider) authenticationProvider2);
      CookieAuthenticationOptions authenticationOptions3 = authenticationOptions1;
      CookieAuthenticationExtensions.UseCookieAuthentication(iappBuilder, authenticationOptions3);
      AppBuilderExtensions.UseExternalSignInCookie(app, "ExternalCookie");
      AppBuilderExtensions.UseTwoFactorSignInCookie(app, "TwoFactorCookie", TimeSpan.FromMinutes(5.0));
      AppBuilderExtensions.UseTwoFactorRememberBrowserCookie(app, "TwoFactorRememberBrowser");
    }

    public void Configuration(IAppBuilder app)
    {
      this.ConfigureAuth(app);
    }
  }
}
