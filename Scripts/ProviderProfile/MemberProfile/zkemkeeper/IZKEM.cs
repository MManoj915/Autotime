// Decompiled with JetBrains decompiler
// Type: zkemkeeper.IZKEM
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace zkemkeeper
{
  [CompilerGenerated]
  [Guid("102F4206-E43D-4FC9-BAB0-331CFFE4D25B")]
  [TypeIdentifier]
  [ComImport]
  public interface IZKEM
  {
    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_39();

    [DispId(39)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    bool Connect_Net([MarshalAs(UnmanagedType.BStr), In] string IPAdd, [In] int Port);

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap2_133();

    [DispId(171)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    bool RegEvent([In] int dwMachineNumber, [In] int EventMask);
  }
}
