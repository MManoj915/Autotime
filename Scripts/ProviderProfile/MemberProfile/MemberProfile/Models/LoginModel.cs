// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.LoginModel
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.ComponentModel;

namespace MemberProfile.Models
{
  public class LoginModel
  {
    public long MemberCode { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    [DisplayName("Remember Me")]
    public bool Remember { get; set; }
  }
}
