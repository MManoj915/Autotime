// Decompiled with JetBrains decompiler
// Type: MemberProfile.Models.LocationContext
// Assembly: MemberProfile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C5AF095-404D-4EC8-89E1-AB3C7D31869E
// Assembly location: C:\Users\Manoj\Desktop\MemberProfile.dll

using Dapper;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace MemberProfile.Models
{
  public class LocationContext
  {
    private IDbConnection _db = (IDbConnection) new OracleConnection(ConfigurationSettings.AppSettings["Connection"].ToString());

    public IEnumerable<Member> GetMobileList()
    {
      return (IEnumerable<Member>) SqlMapper.AsList<Member>(SqlMapper.Query<Member>(this._db, "Select Value ResidentialLocationCode,Name ResidentialLocationName From IM_MEMBERLOCATION_VW", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }

    public IEnumerable<ClaimsModel> GetClinicianList(long ProviderCode)
    {
      return (IEnumerable<ClaimsModel>) SqlMapper.AsList<ClaimsModel>(SqlMapper.Query<ClaimsModel>(this._db, "Select CDCode,LicenseID CDName From IM_Clinicians  WHERE CDCODE IN  (SELECT CLINICIANCODE FROM IM_PROVIDER_CLINICIAN_LINK WHERE PROVIDERCODE = " + (object) ProviderCode + ")", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }

    public IEnumerable<ClaimsModel> GetTypeList()
    {
      return (IEnumerable<ClaimsModel>) SqlMapper.AsList<ClaimsModel>(SqlMapper.Query<ClaimsModel>(this._db, "Select TypeCode,TypeName from IM_ACTIVITY_TYPES WHERE TYPECODE <> 100000000000000001", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }

    public IEnumerable<ClaimsModel> GetDiagnosisTypeList()
    {
      return (IEnumerable<ClaimsModel>) SqlMapper.AsList<ClaimsModel>(SqlMapper.Query<ClaimsModel>(this._db, "Select TypeCode,TypeName from IM_ACTIVITY_TYPES WHERE TYPECODE = 100000000000000001", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }

    public IEnumerable<ClaimsModel> GetClaimType()
    {
      return (IEnumerable<ClaimsModel>) SqlMapper.AsList<ClaimsModel>(SqlMapper.Query<ClaimsModel>(this._db, "SELECT 1 RequestTypecode,'Claims' RequestTypeName FROM DUAL UNION  SELECT 2 RequestTypecode,'Approvals' RequestTypeName FROM DUAL", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }

    public IEnumerable<ClaimsModel> GetDiagnosisVersionDetails()
    {
      return (IEnumerable<ClaimsModel>) SqlMapper.AsList<ClaimsModel>(SqlMapper.Query<ClaimsModel>(this._db, "Select DetailCode,Code  FROM IM_VERSION_DETALIS  WHERE VERSIONCODE IN(SELECT VERSIONCODE FROM IM_ACTIVITY_DETAILS WHERE TYPECODE = 100000000000000001) AND ROWNUM < 101", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }

    public IEnumerable<ClaimsModel> GetVersionDetails()
    {
      return (IEnumerable<ClaimsModel>) SqlMapper.AsList<ClaimsModel>(SqlMapper.Query<ClaimsModel>(this._db, "Select DetailCode,Code  FROM IM_VERSION_DETALIS  WHERE VERSIONCODE IN(SELECT VERSIONCODE FROM IM_ACTIVITY_DETAILS WHERE TYPECODE <> 100000000000000001) AND ROWNUM < 101", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }

    public IEnumerable<ClaimsModel> GetTreatmentTypeList()
    {
      return (IEnumerable<ClaimsModel>) SqlMapper.AsList<ClaimsModel>(SqlMapper.Query<ClaimsModel>(this._db, "SELECT ConstantValue TreatmentTypeCode,ConstantName TreatmentTypeName FROM GENCONSTANT WHERE CATEGORY='APPLICABLE' AND LANGUAGECODE='en-US'", (object) null, (IDbTransaction) null, true, new int?(), new CommandType?()));
    }
  }
}
