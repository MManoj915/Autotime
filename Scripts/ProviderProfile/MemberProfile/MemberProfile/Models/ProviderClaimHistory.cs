// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.ProviderClaimHistory
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

namespace MemberProfile.Models
{
  public class ProviderClaimHistory
  {
    public string InvoiceNumber { get; set; }

    public string CardNo { get; set; }

    public string MemberName { get; set; }

    public string TransactionDate { get; set; }

    public double Request_Amount { get; set; }

    public double Approved_Amount { get; set; }

    public string Status { get; set; }
  }
}
