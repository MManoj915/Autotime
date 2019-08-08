// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.ClaimsModel
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Web.Mvc;

namespace MemberProfile.Models
{
  public class ClaimsModel
  {
    public string CardNo { get; set; }

    public string MemberName { get; set; }

    public string InvoiceNumber { get; set; }

    public long CDCode { get; set; }

    public double Request_Amount { get; set; }

    public double PatientShare { get; set; }

    public double Net_Amount { get; set; }

    public long TypeCode { get; set; }

    public string TypeName { get; set; }

    public long DetailCode { get; set; }

    public long TreatmentTypeCode { get; set; }

    public string TreatmentTypeName { get; set; }

    public string Code { get; set; }

    public string CDName { get; set; }

    public string RequestTypecode { get; set; }

    public string RequestTypeName { get; set; }

    public SelectList ClinicianList { get; set; }

    public SelectList RequestTypeList { get; set; }

    public SelectList DiagnosisTypeList { get; set; }

    public SelectList TypeList { get; set; }

    public SelectList DiagnosisVersionList { get; set; }

    public SelectList VersionList { get; set; }

    public SelectList TreatmentTypeList { get; set; }
  }
}
