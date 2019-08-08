// Decompiled with JetBrains decompiler
// Type: zkemkeeper._IZKEMEvents_Event
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace zkemkeeper
{
  [CompilerGenerated]
  [ComEventInterface(typeof (_IZKEMEvents), typeof (_IZKEMEvents))]
  [TypeIdentifier("fe9ded34-e159-408e-8490-b720a5e632c7", "zkemkeeper._IZKEMEvents_Event")]
  [ComImport]
  public interface _IZKEMEvents_Event
  {
    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_16();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void add_OnVerify(_IZKEMEvents_OnVerifyEventHandler value);

    event _IZKEMEvents_OnVerifyEventHandler OnVerify;

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void remove_OnVerify(_IZKEMEvents_OnVerifyEventHandler value);

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap2_14();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void add_OnAttTransactionEx(_IZKEMEvents_OnAttTransactionExEventHandler value);

    event _IZKEMEvents_OnAttTransactionExEventHandler OnAttTransactionEx;

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void remove_OnAttTransactionEx(_IZKEMEvents_OnAttTransactionExEventHandler value);
  }
}
