// Decompiled with JetBrains decompiler
// Type: MemberProfile.RouteConfig
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Web.Mvc;
using System.Web.Routing;

namespace MemberProfile
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      RouteCollectionExtensions.IgnoreRoute(routes, "{resource}.axd/{*pathInfo}");
      RouteCollectionExtensions.MapRoute(routes, "Default", "{controller}/{action}/{id}", (object) new
      {
        controller = "Login",
        action = "Index",
        id = (UrlParameter) UrlParameter.Optional
      });
    }
  }
}
