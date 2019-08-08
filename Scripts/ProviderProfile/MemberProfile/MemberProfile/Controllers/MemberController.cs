// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.MemberController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MemberProfile.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class MemberController : Controller
  {
    private IDbConnection _db;
    private LocationContext Loc;

    public ActionResult Index()
    {
      List<Member> memberList = (List<Member>) SqlMapper.AsList<Member>(SqlMapper.Query<Member>(this._db, "SELECT MEM.* FROM IM_MEMBERPROFILE_VW MEM Where  MEM.ID =" + (object) Convert.ToInt64(this.get_Session()["MemberCode"]), (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
      if (memberList.Count > 0)
        this.get_Session()["MemberDetails"] = (object) memberList[0];
      Member member = (Member) this.get_Session()["MemberDetails"];
      member.ResidentLocationList = new SelectList((IEnumerable) this.Loc.GetMobileList(), "ResidentialLocationCode", "ResidentialLocationName");
      member.WorkLocationList = new SelectList((IEnumerable) this.Loc.GetMobileList(), "ResidentialLocationCode", "ResidentialLocationName");
      return (ActionResult) this.View((object) member);
    }

    public ActionResult UpdateMemberInfo(string formData)
    {
      long int64 = Convert.ToInt64(this.get_Session()["MemberCode"]);
      MemberModel memberModel = (MemberModel) JsonConvert.DeserializeObject<MemberModel>(formData);
      SqlMapper.Execute(this._db, "Update IM_Members Set UserName = '" + memberModel.UserName + "',Password='" + memberModel.Password + "',Mobile_Number='" + memberModel.Mobilenumber + "',Emirates_ID='" + memberModel.Emiratesid + "',EmailID='" + memberModel.EmailId + "',WorkLocation=" + (object) memberModel.WorkLocationCode + ",ResidentLocation=" + (object) memberModel.ResidentialLocationCode + " Where Member_Code = " + (object) int64, (object) null, (IDbTransaction) null, new int?(), new CommandType?());
      return (ActionResult) this.Json((object) new
      {
        Result = "S"
      }, (JsonRequestBehavior) 0);
    }

    public ActionResult ClaimHistoryView()
    {
      return (ActionResult) this.Json((object) new
      {
        Result = "S",
        ClaimResult = ((IEnumerable<ClaimHistory>) SqlMapper.AsList<ClaimHistory>(SqlMapper.Query<ClaimHistory>(this._db, "SELECT MEM.* FROM IM_MEMBERPROFILE_CH_VW MEM Where  MEM.MEMBER_CODE =" + (object) Convert.ToInt64(this.get_Session()["MemberCode"]), (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()))).ToList<ClaimHistory>()
      }, (JsonRequestBehavior) 0);
    }

    public ActionResult ProviderNetworkView()
    {
      Convert.ToInt64(this.get_Session()["MemberCode"]);
      return (ActionResult) this.Json((object) new
      {
        Result = "S",
        ProviderNetworkResult = ((IEnumerable<ProviderNetwork>) SqlMapper.AsList<ProviderNetwork>(SqlMapper.Query<ProviderNetwork>(this._db, " SELECT NVL(VW.PROVIDERID,'') PROVIDERID,NVL(VW.PROVIDERNAME,'')PROVIDERNAME,NVL(GEN.CONSTANTNAME,'') PROVIDERTYPE, NVL(Nvl(PROVIDERMAINPHONE, PROVIDERMOBILE),'') PROVIDERMAINPHONE,NVL(VW.ADDRESS,'') ADDRESS, NVL(VW.CITYNAME,'') CITYNAME,NVL(VW.PROVIDERAREA,'') PROVIDERAREA FROM IM_PROVIDERNETWORK_VW VW LEFT JOIN GENCONSTANT GEN ON GEN.CONSTANTVALUE = VW.PROVIDERTYPE  AND GEN.CATEGORY = 'PROVIDERTYPE' AND Upper(GEN.LANGUAGECODE) = 'EN-US'  LEFT JOIN GENCONSTANT GEN1 ON GEN1.CONSTANTVALUE = VW.SPECIALITY  AND GEN1.CATEGORY = 'Speciality' AND Upper(GEN1.LANGUAGECODE) = 'EN-US'  WHERE NVL(VW.PROVIDERSTATUS,0)=0 AND VW.MEMBERPOLICYCODE = " + (object) Convert.ToInt64(this.get_Session()["MemberPolicyCode"]), (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()))).ToList<ProviderNetwork>()
      }, (JsonRequestBehavior) 0);
    }

    public ActionResult AuthorizationView_Test()
    {
      ((IEnumerable<string>) this.get_Request().Form.GetValues("draw")).FirstOrDefault<string>();
      string str1 = ((IEnumerable<string>) this.get_Request().Form.GetValues("start")).FirstOrDefault<string>();
      string str2 = ((IEnumerable<string>) this.get_Request().Form.GetValues("length")).FirstOrDefault<string>();
      string str3 = ((IEnumerable<string>) this.get_Request().Form.GetValues("columns[" + ((IEnumerable<string>) this.get_Request().Form.GetValues("order[0][column]")).FirstOrDefault<string>() + "][name]")).FirstOrDefault<string>();
      string str4 = ((IEnumerable<string>) this.get_Request().Form.GetValues("order[0][dir]")).FirstOrDefault<string>();
      string str5 = ((IEnumerable<string>) this.get_Request().Form.GetValues("search[value]")).FirstOrDefault<string>();
      int num1 = str2 != null ? Convert.ToInt32(str2) : 0;
      int num2 = str1 != null ? Convert.ToInt32(str1) : 0;
      if (string.IsNullOrEmpty(str5))
        ;
      long int64 = Convert.ToInt64(this.get_Session()["MemberCode"]);
      Convert.ToInt64(this.get_Session()["MemberPolicyCode"]);
      List<Authorization> source = (List<Authorization>) SqlMapper.AsList<Authorization>(SqlMapper.Query<Authorization>(this._db, " SELECT * FROM (SELECT  TO_DATE(HDR.TRANSACTIONDATE,'DD/MM/RRRR HH24:MI:SS') REQUESTDATE,  Decode(CLAIM_STATUS, 1, NULL, TO_DATE(HDR.LASTMODIFIEDON, 'DD/MM/RRRR HH24:MI:SS')) APPROVEDDATE,  PRIOR_AUTHORIZATION_ID,PROVIDERNAME,Decode(AUTHORIZEDSTATUS, 0, 'Pending', 1, 'Approved', 2, 'Rejected') Status  FROM IM_AUTHORIZATION_PROCESS_HDR HDR  LEFT JOIN IM_PROVIDERS P ON P.PROVIDERCODE = HDR.PROVIDER_CODE WHERE HDR.MEMBER_CODE = " + (object) int64 + ") ORDER BY " + str3 + " " + str4, (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
      long num3 = (long) source.Count<Authorization>();
      return (ActionResult) this.Json((object) new
      {
        Result = "S",
        data = source.ToList<Authorization>(),
        recordsFiltered = num3,
        recordsTotal = num3
      }, (JsonRequestBehavior) 0);
    }

    public ActionResult AuthorizationView()
    {
      long int64 = Convert.ToInt64(this.get_Session()["MemberCode"]);
      Convert.ToInt64(this.get_Session()["MemberPolicyCode"]);
      return (ActionResult) this.Json((object) new
      {
        Result = "S",
        AuthorizationResult = ((IEnumerable<Authorization>) SqlMapper.AsList<Authorization>(SqlMapper.Query<Authorization>(this._db, " SELECT   TO_CHAR(HDR.TRANSACTIONDATE,'DD/MM/RRRR HH24:MI') REQUESTDATE,  Decode(CLAIM_STATUS, 1, NULL, TO_CHAR(HDR.LASTMODIFIEDON, 'DD/MM/RRRR HH24:MI')) APPROVEDDATE,  PRIOR_AUTHORIZATION_ID,PROVIDERNAME,Decode(AUTHORIZEDSTATUS, 0, 'Pending', 1, 'Approved', 2, 'Rejected') Status  FROM IM_AUTHORIZATION_PROCESS_HDR HDR  LEFT JOIN IM_PROVIDERS P ON P.PROVIDERCODE = HDR.PROVIDER_CODE WHERE HDR.MEMBER_CODE = " + (object) int64 + " ORDER BY HDR.TRANSACTIONDATE DESC", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()))).ToList<Authorization>()
      }, (JsonRequestBehavior) 0);
    }

    public ActionResult Enquiry()
    {
      return (ActionResult) this.RedirectToAction("Index", nameof (Enquiry));
    }

    public MemberController()
    {
      base.\u002Ector();
    }
  }
}
