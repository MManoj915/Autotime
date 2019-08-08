// Decompiled with JetBrains decompiler
// Type: zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace zkemkeeper
{
  [CompilerGenerated]
  [TypeIdentifier("fe9ded34-e159-408e-8490-b720a5e632c7", "zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler")]
  public delegate void _IZKEMEvents_OnAttTransactionExEventHandler(
    [MarshalAs(UnmanagedType.BStr), In] string EnrollNumber,
    [In] int IsInValid,
    [In] int AttState,
    [In] int VerifyMethod,
    [In] int Year,
    [In] int Month,
    [In] int Day,
    [In] int Hour,
    [In] int Minute,
    [In] int Second,
    [In] int WorkCode);
}
