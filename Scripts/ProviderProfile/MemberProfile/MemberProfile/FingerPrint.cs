// Decompiled with JetBrains decompiler
// Type: MemberProfile.FingerPrint
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;
using zkemkeeper;

namespace MemberProfile
{
  public class FingerPrint
  {
    public void Connect()
    {
    }

    public void DoEvent()
    {
      // ISSUE: variable of a compiler-generated type
      CZKEM instance = (CZKEM) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00853A19-BD51-419B-9269-2DABE57EB61F")));
      // ISSUE: reference to a compiler-generated method
      instance.Connect_Net("192.168.2.99", 4370);
      // ISSUE: reference to a compiler-generated method
      if (!instance.RegEvent(1, (int) ushort.MaxValue))
        return;
      // ISSUE: method pointer
      // ISSUE: object of a compiler-generated type is created
      new ComAwareEventInfo(typeof (_IZKEMEvents_Event), "OnVerify").AddEventHandler((object) instance, (Delegate) new _IZKEMEvents_OnVerifyEventHandler((object) this, (UIntPtr) __methodptr(axCZKEM1_OnVerify)));
      // ISSUE: method pointer
      // ISSUE: object of a compiler-generated type is created
      new ComAwareEventInfo(typeof (_IZKEMEvents_Event), "OnAttTransactionEx").AddEventHandler((object) instance, (Delegate) new _IZKEMEvents_OnAttTransactionExEventHandler((object) this, (UIntPtr) __methodptr(axCZKEM1_OnAttTransactionEx)));
      Application.DoEvents();
    }

    private void axCZKEM1_OnVerify(int iUserID)
    {
      HttpContext.Current.Session["BioUser"] = (object) iUserID;
    }

    private void axCZKEM1_OnConnected()
    {
    }

    private void axCZKEM1_OnAttTransactionEx(
      string sEnrollNumber,
      int iIsInValid,
      int iAttState,
      int iVerifyMethod,
      int iYear,
      int iMonth,
      int iDay,
      int iHour,
      int iMinute,
      int iSecond,
      int iWorkCode)
    {
      HttpContext.Current.Session["BioUser"] = (object) sEnrollNumber;
    }
  }
}
