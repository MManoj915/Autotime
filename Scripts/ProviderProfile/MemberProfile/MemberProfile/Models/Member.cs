// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.Member
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Web.Mvc;

namespace MemberProfile.Models
{
  public class Member
  {
    public long MemberPolicyCode { get; set; }

    public long ID { get; set; }

    public string CardId { get; set; }

    public string PolicyId { get; set; }

    public string CoverdFromDate { get; set; }

    public string CoveredEndDate { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string Emiratesid { get; set; }

    public string Mobilenumber { get; set; }

    public string EmailId { get; set; }

    public long ResidentialLocationCode { get; set; }

    public long WorkLocationCode { get; set; }

    public string Memberid { get; set; }

    public string LastName { get; set; }

    public string GroupName { get; set; }

    public string ResidentialLocationName { get; set; }

    public SelectList ResidentLocationList { get; set; }

    public SelectList WorkLocationList { get; set; }
  }
}
