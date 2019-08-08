// Decompiled with JetBrains decompiler
// Type: MemberProfile.BundleConfig
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using System.Web.Optimization;

namespace MemberProfile
{
  public class BundleConfig
  {
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(((Bundle) new ScriptBundle("~/bundles/jquery")).Include("~/Scripts/jquery-{version}.js", new IItemTransform[0]));
      bundles.Add(((Bundle) new ScriptBundle("~/bundles/jqueryval")).Include("~/Scripts/jquery.validate*", new IItemTransform[0]));
      bundles.Add(((Bundle) new ScriptBundle("~/bundles/modernizr")).Include("~/Scripts/modernizr-*", new IItemTransform[0]));
      bundles.Add(((Bundle) new ScriptBundle("~/bundles/bootstrap")).Include(new string[2]
      {
        "~/Scripts/bootstrap.js",
        "~/Scripts/respond.js"
      }));
      bundles.Add(((Bundle) new StyleBundle("~/Content/css")).Include(new string[2]
      {
        "~/Content/bootstrap.css",
        "~/Content/site.css"
      }));
    }
  }
}
