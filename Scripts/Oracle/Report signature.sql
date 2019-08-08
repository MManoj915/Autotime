Master :

SELECT CPH.CLAIM_CODE,CPH.ENCOUNTER_START_DATE TREATMENTDATE,PRO.PROVIDERID,PRO.PROVIDERNAME,
IMC.CARDNO,MEM.AMEMBERNAME,NVL(POL.POLICYID,IPOL.POLICYID) POLICYID,
NVL(POL.POLICYNAME,IPOL.POLICYNAME) POLICYNAME,CPH.REQUEST_AMOUNT,
(SELECT LISTAGG (IMVD.SHORTDESC, ',') WITHIN GROUP (ORDER BY IMVD.SHORTDESC) FROM IM_CLAIM_PROCESS_DETAIL DTL
 LEFT JOIN IM_ACTIVITY_TYPES  ACTTYP ON ACTTYP.TYPECODE=DTL.ACTIVITY_CODE
JOIN IM_VERSION_DETALIS IMVD ON IMVD.DETAILCODE = DTL.VERSIONDETAILCODE
WHERE ACTTYP.VALUE = -1 AND DTL.CLAIM_CODE = CPH.CLAIM_CODE GROUP BY DTL.CLAIM_CODE ) ICDCODES,
CPH.ORGINALPATIENTSHARE PATIENTSHARE,CPH.ACR_FORM_NO FORMNO,1 DIAGNOSISDTL,2 ACTIVITYDTL
FROM IM_CLAIM_PROCESS_HEADER CPH
LEFT JOIN IM_PROVIDERS  PRO ON PRO.PROVIDERCODE=CPH.PROVIDER_CODE
LEFT JOIN IM_MEMBERS_MAT_VW MEM ON MEM.MEMBERCODE=CPH.MEMBER_CODE AND MEM.POLICYCODE = CPH.POLICYCODE
LEFT JOIN IM_CORDPRINT IMC ON IMC.MEMBERCODE = MEM.MEMBERCODE AND IMC.POLICYCODE = MEM.POLICYCODE AND MEM.CATEGORYCODE = IMC.CATEGORYCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE=CPH.POLICYCODE  AND POL.POLICYCODE = MEM.POLICYCODE AND MEM.TYPEE = 1
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE=CPH.POLICYCODE  AND IPOL.INDIVIDUALPOLICYCODE = MEM.POLICYCODE AND MEM.TYPEE = 2
WHERE CPH.CLAIM_CODE = APRIKEY


Activity Detail :

SELECT IVD.CODE,IVD.SHORTDESC DESCRIPTION,DTL.ACTIVITYSTARTDATE TREATMENTDATE,
DTL.REQUESTAMOUNT,DTL.REQUESTQTY,DTL.DEDUCTIBLE_AMOUNT,DTL.COINSURANCE_AMOUNT,DTL.DISCOUNT_AMOUNT,
DTL.DENIAL_VALUE,DTL.TOTAL,IMC.NAME CLINICIANNAME,DTL.CLAIM_CODE FROM
(SELECT * FROM IM_CLAIM_PROCESS_DETAIL WHERE NVL(TYPE,0) = 0)  DTL
JOIN IM_VERSION_DETALIS IVD ON IVD.DETAILCODE = DTL.VERSIONDETAILCODE
JOIN IM_CLINICIANS IMC ON IMC.CDCODE = DTL.CLINICIAN_CODE
WHERE DTL.CLAIM_CODE = APRIKEY


Diagnosis Detail :

SELECT IVD.CODE,IVD.SHORTDESC DESCRIPTION,GEN.CONSTANTNAME TYPENAME,DTL.CLAIM_CODE FROM
(SELECT * FROM IM_CLAIM_PROCESS_DETAIL WHERE TYPE IN (1,2)) DTL
JOIN IM_VERSION_DETALIS IVD ON IVD.DETAILCODE = DTL.VERSIONDETAILCODE
LEFT JOIN GENCONSTANT GEN ON UPPER(GEN.CONSTANTVALUE) = UPPER(DTL.TYPE) AND GEN.CATEGORY='IMDIAGNOSISTYPE' AND GEN.LANGUAGECODE = 'en-US'
WHERE DTL.CLAIM_CODE = APRIKEY