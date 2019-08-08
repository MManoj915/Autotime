// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.SendCodeViewModel
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Collections.Generic;
using System.Web.Mvc;

namespace MemberProfile.Models
{
  public class SendCodeViewModel
  {
    public string SelectedProvider { get; set; }

    public ICollection<SelectListItem> Providers { get; set; }

    public string ReturnUrl { get; set; }

    public bool RememberMe { get; set; }
  }
}
