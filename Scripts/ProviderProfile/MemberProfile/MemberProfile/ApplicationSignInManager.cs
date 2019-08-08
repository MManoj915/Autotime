// Decompiled with JetBrains decompiler
// Type: MemberProfile.ApplicationSignInManager
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using MemberProfile.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MemberProfile
{
  public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
  {
    public ApplicationSignInManager(
      ApplicationUserManager userManager,
      IAuthenticationManager authenticationManager)
    {
      this.\u002Ector((UserManager<ApplicationUser, string>) userManager, authenticationManager);
    }

    public virtual Task<ClaimsIdentity> CreateUserIdentityAsync(
      ApplicationUser user)
    {
      return user.GenerateUserIdentityAsync((UserManager<ApplicationUser>) this.get_UserManager());
    }

    public static ApplicationSignInManager Create(
      IdentityFactoryOptions<ApplicationSignInManager> options,
      IOwinContext context)
    {
      return new ApplicationSignInManager((ApplicationUserManager) OwinContextExtensions.GetUserManager<ApplicationUserManager>(context), context.get_Authentication());
    }
  }
}
