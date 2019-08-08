// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.ClaimsController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MemberProfile.Models;
using System;
using System.Collections;
using System.Data;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class ClaimsController : Controller
  {
    private IDbConnection _db;
    private LocationContext Loc;

    public ActionResult Index()
    {
      long int64 = Convert.ToInt64(this.get_Session()["ProviderCode"]);
      return (ActionResult) this.View((object) new ClaimsModel()
      {
        ClinicianList = new SelectList((IEnumerable) this.Loc.GetClinicianList(int64), "CDCode", "CDName"),
        TypeList = new SelectList((IEnumerable) this.Loc.GetTypeList(), "TypeCode", "TypeName"),
        DiagnosisTypeList = new SelectList((IEnumerable) this.Loc.GetDiagnosisTypeList(), "TypeCode", "TypeName"),
        VersionList = new SelectList((IEnumerable) this.Loc.GetVersionDetails(), "DetailCode", "Code"),
        DiagnosisVersionList = new SelectList((IEnumerable) this.Loc.GetDiagnosisVersionDetails(), "DetailCode", "Code"),
        TreatmentTypeList = new SelectList((IEnumerable) this.Loc.GetTreatmentTypeList(), "TreatmentTypeCode", "TreatmentTypeName"),
        RequestTypeList = new SelectList((IEnumerable) this.Loc.GetClaimType(), "RequestTypecode", "RequestTypeName")
      });
    }

    public ActionResult GetMemberName(string CardNo)
    {
      try
      {
        return (ActionResult) this.Json((object) new
        {
          Result = "S",
          MemberResult = SqlMapper.ExecuteScalar(this._db, "Select AMemberName from IM_MEMBERPOLICY_NAME Where Upper(ACardID) = Upper('" + CardNo + "')", (object) null, (IDbTransaction) null, new int?(), new CommandType?()).ToString()
        }, (JsonRequestBehavior) 0);
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          Result = ex.Message
        }, (JsonRequestBehavior) 0);
      }
    }

    public ClaimsController()
    {
      base.\u002Ector();
    }
  }
}
