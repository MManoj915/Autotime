Header :

SELECT HDR.CLAIM_CODE,HDR.REQUEST_NUMBER,HDR.CPPREFIX,HDR.CPNO,HDR.CPSUFFIX,HDR.CPFULLNO,
HDR.AUTHORIZEDSTATUS,HDR.TRANSACTIONDATE,HDR.REQUEST_SOURCE,HDR.REQUEST_TYPE,HDR.REQUEST_STATUS,
HDR.PROVIDER_CODE,HDR.IS_BLOCKED,HDR.IS_FASTTRACE_PAYMENT,HDR.MEMBER_CODE,HDR.AUTHORIZATOIN_CODE,
HDR.CREATEDIN,HDR.CREATEDON,HDR.CREATEDBY,HDR.LASTMODIFIEDON,HDR.LASTMODIFIEDBY,HDR.LEGALENTITYCODE,HDR.CUSTOMERCODE,HDR.LOCATIONCODE,HDR.COMPANYCODE,HDR.BENEFIT_CODE,HDR.REQUEST_AMOUNT,HDR.APPROVED_DATE,HDR.APPROVED_AMOUNT,HDR.TREATMENT_TYPE,HDR.ENCOUNTER_START_DATE,HDR.TYPE,HDR.CLAIM_STATUS,HDR.DENIAL_REASON,HDR.CASESUMMARY,HDR.SERIALNO,HDR.DENAILVALUE,HDR.PICTURE,HDR.INVOICENUMBER,HDR.DENIALREASON,HDR.REASON,HDR.TYPECODE,HDR.ACTIVITYCODE,HDR.MAP_VALUE,HDR.BATCH_NUMBER,HDR.ATHORIZATOIN_CODE,HDR.ENCOUNTER_END_DATE,HDR.DURATION,HDR.REQUEST_DATE,HDR.RESUBMISSION_TYPE,HDR.BATCHCODE,HDR.PARENTCLAIMCODE,HDR.CURRENCYCODE,HDR.NETWORKCODE,HDR.POLICYCODE,HDR.CATEGORY_CODE,HDR.ISEMERGENCY,HDR.PROVINCECODE,HDR.CDCODE,HDR.UPLOADSTATUS,HDR.DISCOUNTTOTAL,HDR.ACTIVITYPRICE,HDR.CO_INS_VALUE,HDR.DEDUCTABLEVALUE,HDR.INVOICEDATE,HDR.ACR_FORM_NO,HDR.ACR_FORM_TYPE,HDR.CLAIM_TYPE,HDR.NETAMOUNT,HDR.MEMBERPOLICYCODE,HDR.RESUBMISSION,HDR.ISRESUBMISSION,HDR.CLAIM_REMARKS,HDR.ENCOUNTER_START_TYPE,HDR.ENCOUNTER_END_TYPE,HDR.ENCOUNTER_FACILITY_TYPE,HDR.RFP,HDR.SUBMITTORA,HDR.FINANCE,HDR.ORGINALPATIENTSHARE,HDR.RATE,HDR.REIMBURSEMENTAMT,HDR.SOURCE_TYPE,HDR.SOURCE_CODE,HDR.ACR_FORM_SEQ_NO,HDR.ACR_PRODUCT_CODE,HDR.ACR_SERIAL_NO,HDR.ACR_PRODUCT_CATEGORY,HDR.ACR_MEMBER_NO,HDR.ACR_MEMBER_REFERENCE_NO,HDR.ACR_PROVIDER_CODE,HDR.ACR_PHYSICIAN_CODE,HDR.ISPOSTED,HDR.PAYMENTREFNO,HDR.SOURCEPROVIDER,HDR.SCREENINGDONE,HDR.ACR_UW_YEAR,HDR.OUTOFNETWORKPROVIDERNAME,HDR.CARDNO,HDR.ISNEXTCARE,HDR.POLICYTYPECODE,HDR.NETWORKTYPE,HDR.ADMINISTRATEDBY,HDR.RESUBMISSION_COMMENTS,HDR.RESUBMISSION_ATTACHMENT,HDR.UBYMANOJ,HDR.PROCESSEDDATE,HDR.TOBEDELETECLAIMS,HDR.ALREADYSCREENED,HDR.TOBEUPDATED,HDR.ACR_SEQUENCE_NO,HDR.CEEDSTATUS,HDR.REVIEWEDBYDOCTOR,HDR.DECLINEAMOUNT,HDR.HNMAMOUNT,HDR.MDEDUCT,HDR.MCO_INS,HDR.OLDCLAIMS,HDR.AUTOREJECT,HDR.ISDECLINEDPOSTED,HDR.ISFRAUD,HDR.ISAUTOMATED,HDR.LCNT,HDR.IBNRMONTH,HDR.MEDPOSTINGDONE,HDR.ISPROCESSED,HDR.CRTCLAIM,HDR.ORIGINALDEDUCTABLEAMOUNT,HDR.ORIGINALREQUESTAMOUNT,HDR.AUTOPROCESSED,HDR.CLAIMMEMBERBATCHCODE,HDR.ACTCHECKHDR,HDR.ISREVERSED,HDR.PAYMENTTYPECODE,HDR.REIMBURSEMENT_FORM_NO,HDR.FORMTYPECODE,HDR.BRATE,HDR.MEMOTYPE,HDR.PRICEUPDATE,HDR.MEDREFDATE,HDR.ACR_SEQ_NO,HDR.ACR_INV_NO,HDR.ISCARDRESUBMISSION,HDR.ISREVERSEDFORDEBIT,HDR.ISREVERSEDFORCREDIT,HDR.PAYMENTREFDATE,HDR.DISCOUNTPERCENTAGE,HDR.REVERSEREFNO,HDR.REVERSEREFDATE,HDR.NEXTCAREBATCHNO,HDR.TPACLAIMHDRCODE,HDR.REINSMEMBERNAME,HDR.TEMPORGINALPATIENTSHARE,HDR.NEXTCARECLAIMHDRCODE,HDR.FMCMEMBERNAME,HDR.FMCDIAGNOSIS,HDR.NEXTCAREDENIEDFOR,HDR.PRIOR_AUTHORIZATION_ID,HDR.ISPRIORAUTHORIZATION,HDR.FMCCARDNO,HDR.FMCUNIQCLAIMID,HDR.FOB,HDR.RAUPLOADDATE,HDR.AUTHORIZATIONTYPECODE,
MPOL.AMEMBERNAME MEMBERNAME,MPOL.ACARDID,MPOL.MEMBERSTARTDATE,NVL(MPOL.MEMBERENDDATE,MPOL.POLICYENDDDATE) ENDDATE,
MPOL.POLICYSTARTDDATE,MPOL.POLICYENDDDATE,MPOL.APOLICYID POLICYID,MPOL.APOLICYNAME POLICYNAME,GEN.CONSTANTNAME GENDERNAME,
MPOL.ADATE_OF_BIRTH DATEOFBIRTH,MONTHS_BETWEEN(TRUNC(SYSDATE),TO_DATE(MPOL.ADATE_OF_BIRTH,'DD/MM/RRRR'))/12 AGE,
GEN1.CONSTANTNAME AUTHORIZEDSTATUSNAME,GEN8.CONSTANTNAME STATUSNAME,PRO.PROVIDERID,PRO.PROVIDERNAME,
IMVRDD.CODE,ADM.USERNAME,GEN2.CONSTANTNAME AUTHORIZATIONTYPENAME,HDR.DATEORDERED,GEN4.CONSTANTNAME ENCOUNTERTYPENAME,
HDR.REFCODE,CAT.CATEGORY_ID CATEGORYNAME,HDR.MEMBER_CODE MEMBERCODE,HDR.EMAILID,HDR.MOBILENO
FROM IM_AUTHORIZATION_PROCESS_HDR HDR
LEFT JOIN IM_MEMBERPOLICY MPOL ON MPOL.MEMBERPOLICYCODE = HDR.MEMBERPOLICYCODE
LEFT JOIN GENCONSTANT GEN ON GEN.CATEGORY = 'Gender' AND GEN.LANGUAGECODE = 'en-US' AND GEN.CONSTANTVALUE = MPOL.AGENDER
LEFT JOIN GENCONSTANT GEN1  ON GEN1.CONSTANTVALUE = HDR.AUTHORIZEDSTATUS AND GEN1.CATEGORY = 'AUTHORIZEDSTATUS' AND UPPER(GEN1.LANGUAGECODE) = 'EN-US'
LEFT JOIN GENCONSTANT GEN8 ON GEN8.CONSTANTVALUE = HDR.CLAIM_STATUS AND GEN8.CATEGORY = 'CLAIMREQSTATUS' AND UPPER (GEN8.LANGUAGECODE) = 'EN-US'
LEFT JOIN GENCONSTANT GEN2 ON GEN2.CONSTANTVALUE = HDR.AUTHORIZATIONTYPECODE AND GEN2.CATEGORY = 'AUTHORIZATIONTYPE' AND UPPER (GEN2.LANGUAGECODE) = 'EN-US'
LEFT JOIN IM_PROVIDERS PRO ON PRO.PROVIDERCODE = HDR.PROVIDER_CODE
LEFT JOIN IM_CATEGORIES CAT ON CAT.CATEGORY_CODE = MPOL.CATEGORYCODE
LEFT JOIN GENCONSTANT GEN4 ON GEN4.CONSTANTVALUE=HDR.ENCOUNTER_FACILITY_TYPE  AND GEN4.CATEGORY='ENCOUNTERFACILITYTYPE' AND UPPER (GEN4.LANGUAGECODE) = 'EN-US'
LEFT JOIN IM_VERSION_DETALIS IMVRDD ON IMVRDD.DETAILCODE = HDR.DENIAL_REASON
JOIN ADMUSER ADM  ON ADM.USERCODE = HDR.LASTMODIFIEDBY
WHERE AUTHORIZEDSTATUS = 0 AND MAP_VALUE IS NULL

Diagnosis :

Select Dtl.*,Gen.ConstantName TypeName,IVD.Code DiagnosisName,IVD.ShortDesc from IM_Authorization_Process_Dtl Dtl  
left join IM_Version_Detalis IVD on IVD.DetailCode = Dtl.VersionDetailCode  
left join GenConstant Gen on Upper(Gen.ConstantName) = upper(Dtl.Type) and Gen.Category='IMDIAGNOSISTYPE' AND
 Gen.LanguageCode = 'en-US' Where Nvl(Dtl.Type,0) <> 0 and Dtl.Claim_Code=1


Activity Detail :

Select Dtl.*,ACTTYP.TypeName,  VerDet.Code VersionDtlName,VerDet.ShortDesc,IMVRDD.Code RecoveryReason,IBC.Benefit_ID BenefitID
from IM_Authorization_Process_Dtl Dtl  
LEFT JOIN IM_ACTIVITY_TYPES ACTTYP ON ACTTYP.TYPECODE = Dtl.ACTIVITY_CODE  
Left Join IM_Benefit_Codes IBC on IBC.Benefit_Code = Dtl.Benefit_Code  
LEFT JOIN IM_VERSION_DETALIS VERDET ON VERDET.DETAILCODE = Dtl.VERSIONDETAILCODE 
LEFT JOIN IM_VERSION_DETALIS IMVRDD  ON (IMVRDD.DETAILCODE) = Dtl.DENIAL_REASON 
Where Nvl(Dtl.Type,0) = 0  and Dtl.Claim_Code=1
