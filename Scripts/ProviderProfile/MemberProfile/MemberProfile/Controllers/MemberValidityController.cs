// Decompiled with JetBrains decompiler
// Type: MemberProfile.Controllers.MemberValidityController
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using MemberProfile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MemberProfile.Controllers
{
  public class MemberValidityController : Controller
  {
    private IDbConnection _db;

    public ActionResult Index()
    {
      new FingerPrint().Connect();
      return (ActionResult) this.View((object) new MemberDetails());
    }

    public ActionResult GetMemberDetails(string formData)
    {
      try
      {
        this.get_Session()["ProviderID"].ToString();
        long int64 = Convert.ToInt64(this.get_Session()["ProviderCode"]);
        MemberDetails memberDetails = (MemberDetails) JsonConvert.DeserializeObject<MemberDetails>(formData);
        string str1 = "SELECT  (MP.ACARDID) CARDNO,(M.EMIRATES_ID) EMIRATESID,(MP.AMEMBERNAME) MEMBERNAME,   (To_Char(MP.MEMBERSTARTDATE, 'DD/MM/RRRR')) VALIDFROMDATE,(To_Char(Nvl(MP.MEMBERENDDATE, MP.POLICYENDDDATE), 'DD/MM/RRRR')) VALIDTODATE,   GET_MEMBER_STATUS((MP.MEMBERPOLICYCODE)) STATUS,GET_MEMBER_ELIG((MP.MEMBERPOLICYCODE), " + (object) int64 + ") ELIGIBILITY,  GET_MEMBER_ELIG_CT((M.CARDNO)) CARDTEXT,  CASE WHEN NVL(M.CANCER,'N') = 'Y' THEN 'Yes' WHEN NVL(M.HEPATITIS_C,'N') = 'Y' THEN 'Yes' ELSE 'No' END  BasmaInitiative FROM IM_MEMBERS M  JOIN IM_MEMBERPOLICY MP ON MP.MEMBERCODE = M.MEMBER_CODE  JOIN IM_POLICY POL ON POL.POLICYCODE = MP.POLICYCODE  WHERE TO_DATE(SYSDATE,'DD/MM/RRRR') BETWEEN TO_DATE(MP.MEMBERSTARTDATE,'DD/MM/RRRR') AND TO_DATE(NVL(MP.MEMBERENDDATE,MP.POLICYENDDDATE),'DD/MM/RRRR') AND  POL.OWNERCODE IN(3,13,21) AND M.POLICYTYPE = 1  AND (MP.ACARDID = '" + memberDetails.CardNo + "' OR REPLACE(M.EMIRATES_ID,'-') = REPLACE('" + memberDetails.Emiratesid + "','-'))  UNION  SELECT  (MP.ACARDID) CARDNO,(M.EMIRATES_ID) EMIRATESID,(MP.AMEMBERNAME) MEMBERNAME,    (To_Char(MP.MEMBERSTARTDATE, 'DD/MM/RRRR')) VALIDFROMDATE,(To_Char(Nvl(MP.MEMBERENDDATE, MP.POLICYENDDDATE), 'DD/MM/RRRR')) VALIDTODATE,    GET_MEMBER_STATUS((MP.MEMBERPOLICYCODE)) STATUS,GET_MEMBER_ELIG((MP.MEMBERPOLICYCODE), " + (object) int64 + ") ELIGIBILITY,  GET_MEMBER_ELIG_CT((M.CARDNO)) CARDTEXT,  CASE WHEN NVL(M.CANCER,'N') = 'Y' THEN 'Yes' WHEN NVL(M.HEPATITIS_C,'N') = 'Y' THEN 'Yes' ELSE 'No' END  BasmaInitiative  FROM IM_MEMBERS M  JOIN IM_MEMBERPOLICY MP ON MP.MEMBERCODE = M.MEMBER_CODE  JOIN IM_INDIVIDUALPOLICY POL ON POL.INDIVIDUALPOLICYCODE = MP.POLICYCODE  WHERE TO_DATE(SYSDATE,'DD/MM/RRRR') BETWEEN TO_DATE(MP.MEMBERSTARTDATE,'DD/MM/RRRR') AND TO_DATE(NVL(MP.MEMBERENDDATE,MP.POLICYENDDDATE),'DD/MM/RRRR') AND  POL.OWNERCODE IN(3,13,21) AND M.POLICYTYPE = 2 AND (MP.ACARDID = '" + memberDetails.CardNo + "' OR REPLACE(M.EMIRATES_ID,'-') = REPLACE('" + memberDetails.Emiratesid + "','-')) ";
        string str2 = "SELECT  (M.CARDNO) CARDNO,(M.EMIRATES_ID) EMIRATESID,(MP.AMEMBERNAME) MEMBERNAME,   (To_Char(MP.MEMBERSTARTDATE, 'DD/MM/RRRR')) VALIDFROMDATE,(To_Char(Nvl(MP.MEMBERENDDATE, MP.POLICYENDDDATE), 'DD/MM/RRRR')) VALIDTODATE,   GET_MEMBER_STATUS((MP.MEMBERPOLICYCODE)) STATUS,GET_MEMBER_ELIG((MP.MEMBERPOLICYCODE), " + (object) int64 + ") ELIGIBILITY,  GET_MEMBER_ELIG_CT((M.CARDNO)) CARDTEXT,(MP.MEMBERPOLICYCODE) MEMBERPOLICYCODE FROM IM_MEMBERS M  JOIN IM_MEMBERPOLICY MP ON MP.MEMBERCODE = M.MEMBER_CODE  JOIN IM_POLICY POL ON POL.POLICYCODE = MP.POLICYCODE  WHERE TO_DATE(SYSDATE,'DD/MM/RRRR') BETWEEN TO_DATE(MP.MEMBERSTARTDATE,'DD/MM/RRRR') AND TO_DATE(NVL(MP.MEMBERENDDATE,MP.POLICYENDDDATE),'DD/MM/RRRR') AND  POL.OWNERCODE IN(3,13,21) AND M.POLICYTYPE = 1  AND (MP.ACARDID = '" + memberDetails.CardNo + "' OR REPLACE(M.EMIRATES_ID,'-') = REPLACE('" + memberDetails.Emiratesid + "','-'))  UNION  SELECT  (M.CARDNO) CARDNO,(M.EMIRATES_ID) EMIRATESID,(MP.AMEMBERNAME) MEMBERNAME,    (To_Char(MP.MEMBERSTARTDATE, 'DD/MM/RRRR')) VALIDFROMDATE,(To_Char(Nvl(MP.MEMBERENDDATE, MP.POLICYENDDDATE), 'DD/MM/RRRR')) VALIDTODATE,    GET_MEMBER_STATUS((MP.MEMBERPOLICYCODE)) STATUS,GET_MEMBER_ELIG((MP.MEMBERPOLICYCODE), " + (object) int64 + ") ELIGIBILITY,  GET_MEMBER_ELIG_CT((M.CARDNO)) CARDTEXT,(MP.MEMBERPOLICYCODE) MEMBERPOLICYCODE FROM IM_MEMBERS M  JOIN IM_MEMBERPOLICY MP ON MP.MEMBERCODE = M.MEMBER_CODE  JOIN IM_INDIVIDUALPOLICY POL ON POL.INDIVIDUALPOLICYCODE = MP.POLICYCODE  WHERE TO_DATE(SYSDATE,'DD/MM/RRRR') BETWEEN TO_DATE(MP.MEMBERSTARTDATE,'DD/MM/RRRR') AND TO_DATE(NVL(MP.MEMBERENDDATE,MP.POLICYENDDDATE),'DD/MM/RRRR') AND  POL.OWNERCODE IN(3,13,21) AND M.POLICYTYPE = 2 AND (MP.ACARDID = '" + memberDetails.CardNo + "' OR REPLACE(M.EMIRATES_ID,'-') = REPLACE('" + memberDetails.Emiratesid + "','-')) ";
        List<MemberDetails> source = (List<MemberDetails>) SqlMapper.AsList<MemberDetails>(SqlMapper.Query<MemberDetails>(this._db, str1, (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
        List<MemberPolicyDetails> memberPolicyDetailsList = (List<MemberPolicyDetails>) SqlMapper.AsList<MemberPolicyDetails>(SqlMapper.Query<MemberPolicyDetails>(this._db, str2, (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
        if (memberPolicyDetailsList.Count > 0)
        {
          long num = 0;
          if (!string.IsNullOrEmpty(memberPolicyDetailsList[0].MemberPolicyCode))
            num = Convert.ToInt64(memberPolicyDetailsList[0].MemberPolicyCode);
          SqlMapper.Execute(this._db, "INSERT INTO IM_PROVIDER_ELIG_CHECK SELECT '" + memberDetails.CardNo + "','" + memberDetails.Emiratesid + "'," + (object) num + "," + (object) int64 + ",SYSDATE FROM DUAL", (object) null, (IDbTransaction) null, new int?(), new CommandType?());
          return (ActionResult) this.Json((object) new
          {
            Result = "S",
            MemberResult = source.ToList<MemberDetails>()
          }, (JsonRequestBehavior) 0);
        }
        SqlMapper.Execute(this._db, "INSERT INTO IM_PROVIDER_ELIG_CHECK SELECT '" + memberDetails.CardNo + "','" + memberDetails.Emiratesid + "',null," + (object) int64 + ",SYSDATE FROM DUAL", (object) null, (IDbTransaction) null, new int?(), new CommandType?());
        ((ControllerBase) this).get_TempData().set_Item("MemberValidtyResult", (object) "Incorrect Card No or Emirates Id . Please enter valid one !");
        return (ActionResult) this.Json((object) new
        {
          Result = "Incorrect Card No or Emirates Id . Please enter valid one !",
          MemberResult = source.ToList<MemberDetails>()
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

    public ActionResult GetMemberByFP()
    {
      try
      {
        new FingerPrint().DoEvent();
        if (this.get_Session()["BioUser"] == null)
          return (ActionResult) this.Json((object) new
          {
            Result = "System Cannot find Member FP. Try again !"
          }, (JsonRequestBehavior) 0);
        if (this.get_Session()["BioUser"].ToString() == "65535")
          return (ActionResult) this.Json((object) new
          {
            Result = "Member is not regsitered in Biometric Device!Please contact administrator"
          }, (JsonRequestBehavior) 0);
        string str = this.get_Session()["BioUser"].ToString();
        if (Convert.ToInt64(SqlMapper.ExecuteScalar(this._db, "Select Count(*) From IM_MEMBER_ATT Where EmployeeNumber = '" + str + "'", (object) null, (IDbTransaction) null, new int?(), new CommandType?())) <= 0L)
          return (ActionResult) this.Json((object) new
          {
            Result = "Member is not regsitered in Insurance System!Please contact administrator"
          }, (JsonRequestBehavior) 0);
        long int64 = Convert.ToInt64(SqlMapper.ExecuteScalar(this._db, "Select Count(*) From IM_MEMBER_ATT Where EmployeeNumber = '" + str + "'", (object) null, (IDbTransaction) null, new int?(), new CommandType?()));
        this.get_Session()["ProviderID"].ToString();
        List<MemberDetails> source = (List<MemberDetails>) SqlMapper.AsList<MemberDetails>(SqlMapper.Query<MemberDetails>(this._db, " SELECT (MPOL.ACARDID) CARDNO,(MEM.EMIRATES_ID) EMIRATESID,(MPOL.AMEMBERNAME) MEMBERNAME,  (To_Char(MPOL.MEMBERSTARTDATE, 'DD/MM/RRRR')) VALIDFROMDATE,(To_Char(Nvl(MPOL.MEMBERENDDATE, MPOL.POLICYENDDDATE), 'DD/MM/RRRR')) VALIDTODATE,  GET_MEMBER_STATUS((MPOL.MEMBERPOLICYCODE)) STATUS,GET_MEMBER_ELIG((MPOL.MEMBERPOLICYCODE)," + (object) Convert.ToInt64(this.get_Session()["ProviderCode"]) + ") ELIGIBILITY,  GET_MEMBER_ELIG_CT((MPOL.ACARDID)) CARDTEXT,50000 BENEFITLIMIT FROM IM_MEMBERPOLICY MPOL  JOIN IM_MEMBERS MEM ON MEM.MEMBER_CODE = MPOL.MEMBERCODE  LEFT JOIN GENCONSTANT GEN ON GEN.CATEGORY = 'CUSSTATUS' AND GEN.CONSTANTVALUE = MEM.STATUS AND Upper(GEN.LANGUAGECODE) = 'EN-US'  WHERE MPOL.MEMBERPOLICYCODE  = " + (object) int64, (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
        if (source.Count > 0)
          return (ActionResult) this.Json((object) new
          {
            Result = "S",
            MemberResult = source.ToList<MemberDetails>()
          }, (JsonRequestBehavior) 0);
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          Result = ex.Message
        }, (JsonRequestBehavior) 0);
      }
      return (ActionResult) this.Json((object) new
      {
        Result = "S"
      }, (JsonRequestBehavior) 0);
    }

    public MemberValidityController()
    {
      base.\u002Ector();
    }
  }
}
