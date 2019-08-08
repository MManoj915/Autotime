// Decompiled with JetBrains decompiler
// Type: MemberProfile.EmailService
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace MemberProfile
{
  public class EmailService : IIdentityMessageService
  {
    public Task SendAsync(IdentityMessage message)
    {
      return (Task) Task.FromResult<int>(0);
    }
  }
}
