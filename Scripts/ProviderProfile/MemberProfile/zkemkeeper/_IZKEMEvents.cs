// Decompiled with JetBrains decompiler
// Type: zkemkeeper._IZKEMEvents
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace zkemkeeper
{
  [CompilerGenerated]
  [Guid("CF83B580-5D32-4C65-B44E-BEDC750CDFA8")]
  [InterfaceType(2)]
  [TypeIdentifier]
  [ComImport]
  public interface _IZKEMEvents
  {
    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_8();

    [DispId(9)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void OnVerify([In] int UserID);

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap2_7();

    [DispId(17)]
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void OnAttTransactionEx(
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
}
