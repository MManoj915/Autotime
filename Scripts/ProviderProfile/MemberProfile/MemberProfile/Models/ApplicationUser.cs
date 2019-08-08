// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.ApplicationUser
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MemberProfile.Models
{
  public class ApplicationUser : IdentityUser
  {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
      UserManager<ApplicationUser> manager)
    {
      ClaimsIdentity userIdentity = await ((UserManager<ApplicationUser, string>) manager).CreateIdentityAsync(this, "ApplicationCookie");
      return userIdentity;
    }

    public ApplicationUser()
    {
      base.\u002Ector();
    }
  }
}
