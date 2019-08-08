// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.ProviderClaimHistoryController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MemberProfile.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class ProviderClaimHistoryController : Controller
  {
    private IDbConnection _db;

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    public ActionResult ProviderClaimHistoryView()
    {
      return (ActionResult) this.Json((object) new
      {
        Result = "S",
        ProviderClaimHistoryResult = ((IEnumerable<ProviderClaimHistory>) SqlMapper.AsList<ProviderClaimHistory>(SqlMapper.Query<ProviderClaimHistory>(this._db, " SELECT  InvoiceNumber,ACardID CardNo,AMemberName MemberName,TransactionDate,Request_Amount,Approved_Amount,Decode(Hdr.Claim_Status,1,'Pending',2,'Processed') Status From IM_Claim_Process_Header Hdr,IM_MemberPolicy IM Where Hdr.MemberPolicyCode = IM.MemberPolicyCode And Hdr.Provider_Code = " + (object) Convert.ToInt64(this.get_Session()["ProviderCode"]), (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()))).ToList<ProviderClaimHistory>()
      }, (JsonRequestBehavior) 0);
    }

    public ProviderClaimHistoryController()
    {
      base.\u002Ector();
    }
  }
}
