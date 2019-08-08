// Decompiled with JetBrains decompiler
// Type: MemberProfile.ApplicationUserManager
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using MemberProfile.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Data.Entity;

namespace MemberProfile
{
  public class ApplicationUserManager : UserManager<ApplicationUser>
  {
    public ApplicationUserManager(IUserStore<ApplicationUser> store)
    {
      base.\u002Ector(store);
    }

    public static ApplicationUserManager Create(
      IdentityFactoryOptions<ApplicationUserManager> options,
      IOwinContext context)
    {
      ApplicationUserManager applicationUserManager1 = new ApplicationUserManager((IUserStore<ApplicationUser>) new UserStore<ApplicationUser>((DbContext) OwinContextExtensions.Get<ApplicationDbContext>(context)));
      ApplicationUserManager applicationUserManager2 = applicationUserManager1;
      UserValidator<ApplicationUser> userValidator1 = new UserValidator<ApplicationUser>((UserManager<ApplicationUser, string>) applicationUserManager1);
      ((UserValidator<ApplicationUser, string>) userValidator1).set_AllowOnlyAlphanumericUserNames(false);
      ((UserValidator<ApplicationUser, string>) userValidator1).set_RequireUniqueEmail(true);
      UserValidator<ApplicationUser> userValidator2 = userValidator1;
      ((UserManager<ApplicationUser, string>) applicationUserManager2).set_UserValidator((IIdentityValidator<ApplicationUser>) userValidator2);
      ApplicationUserManager applicationUserManager3 = applicationUserManager1;
      PasswordValidator passwordValidator1 = new PasswordValidator();
      passwordValidator1.set_RequiredLength(6);
      passwordValidator1.set_RequireNonLetterOrDigit(true);
      passwordValidator1.set_RequireDigit(true);
      passwordValidator1.set_RequireLowercase(true);
      passwordValidator1.set_RequireUppercase(true);
      PasswordValidator passwordValidator2 = passwordValidator1;
      ((UserManager<ApplicationUser, string>) applicationUserManager3).set_PasswordValidator((IIdentityValidator<string>) passwordValidator2);
      ((UserManager<ApplicationUser, string>) applicationUserManager1).set_UserLockoutEnabledByDefault(true);
      ((UserManager<ApplicationUser, string>) applicationUserManager1).set_DefaultAccountLockoutTimeSpan(TimeSpan.FromMinutes(5.0));
      ((UserManager<ApplicationUser, string>) applicationUserManager1).set_MaxFailedAccessAttemptsBeforeLockout(5);
      ApplicationUserManager applicationUserManager4 = applicationUserManager1;
      PhoneNumberTokenProvider<ApplicationUser> numberTokenProvider1 = new PhoneNumberTokenProvider<ApplicationUser>();
      ((PhoneNumberTokenProvider<ApplicationUser, string>) numberTokenProvider1).set_MessageFormat("Your security code is {0}");
      PhoneNumberTokenProvider<ApplicationUser> numberTokenProvider2 = numberTokenProvider1;
      ((UserManager<ApplicationUser, string>) applicationUserManager4).RegisterTwoFactorProvider("Phone Code", (IUserTokenProvider<ApplicationUser, string>) numberTokenProvider2);
      ApplicationUserManager applicationUserManager5 = applicationUserManager1;
      EmailTokenProvider<ApplicationUser> emailTokenProvider1 = new EmailTokenProvider<ApplicationUser>();
      ((EmailTokenProvider<ApplicationUser, string>) emailTokenProvider1).set_Subject("Security Code");
      ((EmailTokenProvider<ApplicationUser, string>) emailTokenProvider1).set_BodyFormat("Your security code is {0}");
      EmailTokenProvider<ApplicationUser> emailTokenProvider2 = emailTokenProvider1;
      ((UserManager<ApplicationUser, string>) applicationUserManager5).RegisterTwoFactorProvider("Email Code", (IUserTokenProvider<ApplicationUser, string>) emailTokenProvider2);
      ((UserManager<ApplicationUser, string>) applicationUserManager1).set_EmailService((IIdentityMessageService) new EmailService());
      ((UserManager<ApplicationUser, string>) applicationUserManager1).set_SmsService((IIdentityMessageService) new SmsService());
      IDataProtectionProvider protectionProvider = options.get_DataProtectionProvider();
      if (protectionProvider != null)
        ((UserManager<ApplicationUser, string>) applicationUserManager1).set_UserTokenProvider((IUserTokenProvider<ApplicationUser, string>) new DataProtectorTokenProvider<ApplicationUser>(protectionProvider.Create(new string[1]
        {
          "ASP.NET Identity"
        })));
      return applicationUserManager1;
    }
  }
}
