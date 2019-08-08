// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.HomeController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    public ActionResult About()
    {
      // ISSUE: reference to a compiler-generated field
      if (HomeController.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HomeController.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (HomeController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = HomeController.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) HomeController.\u003C\u003Eo__1.\u003C\u003Ep__0, ((ControllerBase) this).get_ViewBag(), "Your application description page.");
      return (ActionResult) this.View();
    }

    public ActionResult Contact()
    {
      // ISSUE: reference to a compiler-generated field
      if (HomeController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        HomeController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (HomeController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = HomeController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) HomeController.\u003C\u003Eo__2.\u003C\u003Ep__0, ((ControllerBase) this).get_ViewBag(), "Your contact page.");
      return (ActionResult) this.View();
    }

    public HomeController()
    {
      base.\u002Ector();
    }
  }
}
