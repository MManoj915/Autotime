INSERT INTO IM_PREMIUM_ACTUARY
SELECT * FROM(SELECT ACC.*,
CASE WHEN TOTALPREMIUM > 0 THEN
ABS(ROUND(NVL(TOTALPREMIUM,0)*(NGIFEESPERCENTAGE/100),2))*-1
ELSE
ABS(ROUND(NVL(TOTALPREMIUM,0)*(NGIFEESPERCENTAGE/100),2)) END  NGI_FEES FROM(
SELECT M.*,Decode(AMOUNT,0,0,(ADDPREMIUM+REFUNDPREMIUM)) TOTALPREMIUM FROM (SELECT  JV.REF_NO,JV.REFDATE,IMP.ACARDID,
IMP.AMEMBERNAME,To_Char(IMP.MEMBERSTARTDATE,'RRRR') ISSUEDYEAR,
Nvl(POL.POLICYID,IPOL.POLICYID)POLICYID,Nvl(POL.POLICYNAME,IPOL.POLICYNAME)POLICYNAME,
To_Char(IMP.MEMBERSTARTDATE,'DD/MM/RRRR') INCEPTIONDATE,To_Char(Nvl(IMP.MEMBERENDDATE,IMP.POLICYENDDDATE),'DD/MM/RRRR') EXPIRYDATE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POL.POLICYCODE,IPOL.INDIVIDUALPOLICYCODE) AND TYPEE = Decode(JV.POLICYTYPE,0,1,2)) MANAGEDBY,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.ADDPREMIUM),0)),(NVL(ABS(DTL.ADDPREMIUM),0))*-1) ADDPREMIUM,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.REFUNDPREMIUM),0))*-1,(NVL(ABS(DTL.REFUNDPREMIUM),0))) REFUNDPREMIUM,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR,
DECODE(JV.MEMOTYPE,1,NVL(POL.NGI_FEES,IPOL.NGI_FEES),NVL(POL.NGI_FEES,IPOL.NGI_FEES)*1)  NGIFEESPERCENTAGE,
JV.AMOUNT,DTL.POLICYFINANCEPOSTINGCODE,DTL.MEMBERPOLICYCODE,IMP.TYPEE ANNUALPOLICYTYPE,IMP.POLICYCODE ANNUALPOLICYCODE,IMP.CATEGORYCODE,
GEN.CONSTANTNAME NATIONALITY,GEN1.CONSTANTNAME GENDER,GEN2.CONSTANTNAME RELATION,TO_CHAR(IM.DATE_OF_BIRTH,'DD/MM/RRRR') DOB,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'DD/MM/RRRR') POLICYSTARTDATE,TO_CHAR(Nvl(POL.ENDDATE,IPOL.ENDDATE),'DD/MM/RRRR') POLICYENDDATE FROM
IM_NGI_JVPOSTING JV
JOIN IM_POLICYFINANCEPOSTINGDTL DTL ON DTL.POLICYFINANCEPOSTINGCODE = JV.REF_CODE
LEFT JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE = DTL.MEMBERPOLICYCODE
JOIN IM_MEMBERS IM ON IM.MEMBER_CODE = IMP.MEMBERCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 0
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 1
LEFT JOIN GENCONSTANT GEN ON  GEN.CONSTANTVALUE = IM.NATIONALITY AND GEN.CATEGORY = 'FND_NATIONALITY' AND Upper(GEN.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN1 ON  GEN1.CONSTANTVALUE = IM.GENDER AND GEN1.CATEGORY = 'GENDER' AND Upper(GEN1.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN2 ON  GEN2.CONSTANTVALUE = IM.RELATION AND GEN2.CATEGORY = 'MEMBERRELATION' AND Upper(GEN2.LANGUAGECODE) = Upper('en-US')
WHERE  JV.REF_NO NOT IN(SELECT REF_NO FROM IM_UW_REVERSE_JV) AND   JVPREMIUMTYPE = 0
AND TO_DATE(REFDATE,'DD/MM/RRRR') > TO_DATE('01/01/2016','DD/MM/RRRR')   AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE  AND DTL.MEMBERPOLICYCODE = IMP.MEMBERPOLICYCODE
AND NVL(IM.PARENTGROUPCODE,IM.GROUP_CODE) =  JV.GROUPCODE  AND   JV.GROUPCODE  IS NOT NULL
UNION ALL
SELECT  JV.REF_NO,JV.REFDATE,IMP.ACARDID,
IMP.AMEMBERNAME,To_Char(IMP.MEMBERSTARTDATE,'RRRR') ISSUEDYEAR,
Nvl(POL.POLICYID,IPOL.POLICYID)POLICYID,Nvl(POL.POLICYNAME,IPOL.POLICYNAME)POLICYNAME,
To_Char(IMP.MEMBERSTARTDATE,'DD/MM/RRRR') INCEPTIONDATE,To_Char(Nvl(IMP.MEMBERENDDATE,IMP.POLICYENDDDATE),'DD/MM/RRRR') EXPIRYDATE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POL.POLICYCODE,IPOL.INDIVIDUALPOLICYCODE) AND TYPEE = Decode(JV.POLICYTYPE,0,1,2)) MANAGEDBY,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.ADDPREMIUM),0)),(NVL(ABS(DTL.ADDPREMIUM),0))*-1) ADDPREMIUM,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.REFUNDPREMIUM),0))*-1,(NVL(ABS(DTL.REFUNDPREMIUM),0))) REFUNDPREMIUM,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR,
DECODE(JV.MEMOTYPE,1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*-1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*1)  NGIFEESPERCENTAGE,
JV.AMOUNT,DTL.POLICYFINANCEPOSTINGCODE,DTL.MEMBERPOLICYCODE,IMP.TYPEE ANNUALPOLICYTYPE,IMP.POLICYCODE ANNUALPOLICYCODE,IMP.CATEGORYCODE,
GEN.CONSTANTNAME NATIONALITY,GEN1.CONSTANTNAME GENDER,GEN2.CONSTANTNAME RELATION,TO_CHAR(IM.DATE_OF_BIRTH,'DD/MM/RRRR') DOB,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'DD/MM/RRRR') POLICYSTARTDATE,TO_CHAR(Nvl(POL.ENDDATE,IPOL.ENDDATE),'DD/MM/RRRR') POLICYENDDATE FROM
IM_NGI_JVPOSTING JV
JOIN IM_POLICYFINANCEPOSTINGDTL DTL ON DTL.POLICYFINANCEPOSTINGCODE = JV.REF_CODE
LEFT JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE = DTL.MEMBERPOLICYCODE
JOIN IM_REINS_MEMBERS IM ON IM.MEMBER_CODE = IMP.REINSMEMBERCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 0
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 1
LEFT JOIN GENCONSTANT GEN ON  GEN.CONSTANTVALUE = IM.NATIONALITY AND GEN.CATEGORY = 'FND_NATIONALITY' AND Upper(GEN.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN1 ON  GEN1.CONSTANTVALUE = IM.GENDER AND GEN1.CATEGORY = 'GENDER' AND Upper(GEN1.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN2 ON  GEN2.CONSTANTVALUE = IM.RELATION AND GEN2.CATEGORY = 'MEMBERRELATION' AND Upper(GEN2.LANGUAGECODE) = Upper('en-US')
WHERE JV.REF_NO NOT IN(SELECT REF_NO FROM IM_UW_REVERSE_JV) AND JVPREMIUMTYPE = 0
AND TO_DATE(REFDATE,'DD/MM/RRRR') > TO_DATE('01/01/2016','DD/MM/RRRR')   AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE  AND DTL.MEMBERPOLICYCODE = IMP.MEMBERPOLICYCODE
AND NVL(IM.PARENTGROUPCODE,IM.GROUP_CODE) =  JV.GROUPCODE  AND   JV.GROUPCODE  IS NOT NULL
UNION ALL
SELECT  JV.REF_NO,JV.REFDATE,Nvl(IMP.ACARDID,'DELMEMBER')ACARDID,IMP.AMEMBERNAME,To_Char(Nvl(IMP.MEMBERSTARTDATE,JV.REFDATE),'RRRR') ISSUEDYEAR,
Nvl(POL.POLICYID,IPOL.POLICYID)POLICYID,Nvl(POL.POLICYNAME,IPOL.POLICYNAME)POLICYNAME,
To_Char(IMP.MEMBERSTARTDATE,'DD/MM/RRRR') INCEPTIONDATE,To_Char(Nvl(IMP.MEMBERENDDATE,IMP.POLICYENDDDATE),'DD/MM/RRRR') EXPIRYDATE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POL.POLICYCODE,IPOL.INDIVIDUALPOLICYCODE) AND TYPEE = Decode(JV.POLICYTYPE,0,1,2)) MANAGEDBY,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.ADDPREMIUM),0)),(NVL(ABS(DTL.ADDPREMIUM),0))*-1) ADDPREMIUM,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.REFUNDPREMIUM),0))*-1,(NVL(ABS(DTL.REFUNDPREMIUM),0))) REFUNDPREMIUM,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR,
DECODE(JV.MEMOTYPE,1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*-1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*1)  NGIFEESPERCENTAGE,
JV.AMOUNT,DTL.POLICYFINANCEPOSTINGCODE,DTL.MEMBERPOLICYCODE,IMP.TYPEE ANNUALPOLICYTYPE,IMP.POLICYCODE ANNUALPOLICYCODE,IMP.CATEGORYCODE,
GEN.CONSTANTNAME NATIONALITY,GEN1.CONSTANTNAME GENDER,GEN2.CONSTANTNAME RELATION,TO_CHAR(IMP.ADATE_OF_BIRTH,'DD/MM/RRRR') DOB,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'DD/MM/RRRR') POLICYSTARTDATE,TO_CHAR(Nvl(POL.ENDDATE,IPOL.ENDDATE),'DD/MM/RRRR') POLICYENDDATE FROM
IM_NGI_JVPOSTING JV
JOIN IM_POLICYFINANCEPOSTINGDTL DTL ON DTL.POLICYFINANCEPOSTINGCODE = JV.REF_CODE
LEFT JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE = DTL.MEMBERPOLICYCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 0
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 1
LEFT JOIN GENCONSTANT GEN ON  GEN.CONSTANTVALUE = IMP.A_NATIONALITY AND GEN.CATEGORY = 'FND_NATIONALITY' AND Upper(GEN.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN1 ON  GEN1.CONSTANTVALUE = IMP.AGENDER AND GEN1.CATEGORY = 'GENDER' AND Upper(GEN1.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN2 ON  GEN2.CONSTANTVALUE = IMP.ARELATION AND GEN2.CATEGORY = 'MEMBERRELATION' AND Upper(GEN2.LANGUAGECODE) = Upper('en-US')
WHERE  JV.REF_NO NOT IN(SELECT REF_NO FROM IM_UW_REVERSE_JV) AND JVPREMIUMTYPE = 0
AND TO_DATE(REFDATE,'DD/MM/RRRR') > TO_DATE('01/01/2016','DD/MM/RRRR')  
AND JV.GROUPCODE  IS NULL
UNION ALL
SELECT  JV.REF_NO,JV.REFDATE,Nvl(IMP.ACARDID,'DELMEMBER')ACARDID,IMP.AMEMBERNAME,To_Char(Nvl(IMP.MEMBERSTARTDATE,JV.REFDATE),'RRRR') ISSUEDYEAR,
Nvl(POL.POLICYID,IPOL.POLICYID)POLICYID,Nvl(POL.POLICYNAME,IPOL.POLICYNAME)POLICYNAME,
To_Char(IMP.MEMBERSTARTDATE,'DD/MM/RRRR') INCEPTIONDATE,To_Char(Nvl(IMP.MEMBERENDDATE,IMP.POLICYENDDDATE),'DD/MM/RRRR') EXPIRYDATE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POL.POLICYCODE,IPOL.INDIVIDUALPOLICYCODE) AND TYPEE = Decode(JV.POLICYTYPE,0,1,2)) MANAGEDBY,
Decode(SubStr(JV.REF_NO,0,2),'01',(NVL(ABS(JV.AMOUNT),0)),0) ADDPREMIUM,
Decode(SubStr(JV.REF_NO,0,2),'02',(NVL(ABS(JV.AMOUNT),0))*-1,0) REFUNDPREMIUM,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR,
DECODE(JV.MEMOTYPE,1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*-1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*1)  NGIFEESPERCENTAGE,
JV.AMOUNT,DTL.POLICYFINANCEPOSTINGCODE,DTL.MEMBERPOLICYCODE,IMP.TYPEE ANNUALPOLICYTYPE,IMP.POLICYCODE ANNUALPOLICYCODE,IMP.CATEGORYCODE,
GEN.CONSTANTNAME NATIONALITY,GEN1.CONSTANTNAME GENDER,GEN2.CONSTANTNAME RELATION,TO_CHAR(IMP.ADATE_OF_BIRTH,'DD/MM/RRRR') DOB,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'DD/MM/RRRR') POLICYSTARTDATE,TO_CHAR(Nvl(POL.ENDDATE,IPOL.ENDDATE),'DD/MM/RRRR') POLICYENDDATE FROM
IM_NGI_JVPOSTING JV
LEFT JOIN (SELECT 0 MEMBERPOLICYCODE,0 POLICYFINANCEPOSTINGCODE,REFUNDPREMIUM,ADDPREMIUM FROM IM_POLICYFINANCEPOSTINGDTL) DTL ON DTL.POLICYFINANCEPOSTINGCODE = JV.REF_CODE
LEFT JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE = DTL.MEMBERPOLICYCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 0
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 1
LEFT JOIN GENCONSTANT GEN ON  GEN.CONSTANTVALUE = IMP.A_NATIONALITY AND GEN.CATEGORY = 'FND_NATIONALITY' AND Upper(GEN.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN1 ON  GEN1.CONSTANTVALUE = IMP.AGENDER AND GEN1.CATEGORY = 'GENDER' AND Upper(GEN1.LANGUAGECODE) = Upper('en-US')
LEFT JOIN GENCONSTANT GEN2 ON  GEN2.CONSTANTVALUE = IMP.ARELATION AND GEN2.CATEGORY = 'MEMBERRELATION' AND Upper(GEN2.LANGUAGECODE) = Upper('en-US')
WHERE  JV.REF_NO   IN(SELECT REF_NO FROM IM_UW_REVERSE_JV) AND JVPREMIUMTYPE = 0
AND TO_DATE(REFDATE,'DD/MM/RRRR') > TO_DATE('01/01/2016','DD/MM/RRRR') 
)M)ACC) WHERE REF_NO NOT IN(SELECT A.REF_NO FROM IM_PREMIUM_ACTUARY A) ;
COMMIT;