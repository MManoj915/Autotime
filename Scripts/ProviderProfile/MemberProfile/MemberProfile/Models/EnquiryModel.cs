// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.EnquiryModel
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Web;

namespace MemberProfile.Models
{
  public class EnquiryModel
  {
    public string GroupName { get; set; }

    public string Mobilenumber { get; set; }

    public string EmailId { get; set; }

    public int ComplaintType { get; set; }

    public string Description { get; set; }

    public string Comments { get; set; }

    public HttpPostedFileBase File { get; set; }
  }
}
