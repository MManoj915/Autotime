
SELECT M.*,(ADDPREMIUM+REFUNDPREMIUM)TOTALPREMIUM FROM (SELECT  JV.REF_NO,JV.REFDATE,IMP.ACARDID,To_Char(IMP.MEMBERSTARTDATE,'RRRR') ISSUEDYEAR,
Nvl(POL.POLICYID,IPOL.POLICYID)POLICYID,Nvl(POL.POLICYNAME,IPOL.POLICYNAME)POLICYNAME,
To_Char(IMP.MEMBERSTARTDATE,'DD/MM/RRRR') INCEPTIONDATE,To_Char(Nvl(IMP.MEMBERENDDATE,IMP.POLICYENDDDATE),'DD/MM/RRRR') EXPIRYDATE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POL.POLICYCODE,IPOL.INDIVIDUALPOLICYCODE) AND TYPEE = Decode(JV.POLICYTYPE,0,1,2)) MANAGEDBY,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.ADDPREMIUM),0)),(NVL(ABS(DTL.ADDPREMIUM),0))*-1) ADDPREMIUM,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.REFUNDPREMIUM),0))*-1,(NVL(ABS(DTL.REFUNDPREMIUM),0))) REFUNDPREMIUM,
(SELECT (GETPREMIUMAMOUNT(DTL.POLICYFINANCEPOSTINGCODE,1)) FROM DUAL) IPPREMIUM,
(SELECT (GETPREMIUMAMOUNT(DTL.POLICYFINANCEPOSTINGCODE,2)) FROM DUAL) OPPREMIUM,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR FROM 
IM_NGI_JVPOSTING JV
JOIN IM_POLICYFINANCEPOSTINGDTL DTL ON DTL.POLICYFINANCEPOSTINGCODE = JV.REF_CODE
LEFT JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE = DTL.MEMBERPOLICYCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 0
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 1 
WHERE  JVPREMIUMTYPE = 0
AND REFDATE BETWEEN TO_DATE('01/01/2016','DD/MM/RRRR')
AND TO_DATE('31/01/2016','DD/MM/RRRR') AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE  AND DTL.MEMBERPOLICYCODE = IMP.MEMBERPOLICYCODE
AND NVL(IMP.A_SUBGROUPCODE,IMP.GROUPCODE) =  JV.GROUPCODE  AND   JV.GROUPCODE  IS NOT NULL
UNION ALL
SELECT  JV.REF_NO,JV.REFDATE,Nvl(IMP.ACARDID,'DELMEMBER')ACARDID,To_Char(Nvl(IMP.MEMBERSTARTDATE,JV.REFDATE),'RRRR') ISSUEDYEAR,
Nvl(POL.POLICYID,IPOL.POLICYID)POLICYID,Nvl(POL.POLICYNAME,IPOL.POLICYNAME)POLICYNAME,
To_Char(IMP.MEMBERSTARTDATE,'DD/MM/RRRR') INCEPTIONDATE,To_Char(Nvl(IMP.MEMBERENDDATE,IMP.POLICYENDDDATE),'DD/MM/RRRR') EXPIRYDATE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POL.POLICYCODE,IPOL.INDIVIDUALPOLICYCODE) AND TYPEE = Decode(JV.POLICYTYPE,0,1,2)) MANAGEDBY,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.ADDPREMIUM),0)),(NVL(ABS(DTL.ADDPREMIUM),0))*-1) ADDPREMIUM,
Decode(Nvl(JV.REVERSEJVCODE,0),0,(NVL(ABS(DTL.REFUNDPREMIUM),0))*-1,(NVL(ABS(DTL.REFUNDPREMIUM),0))) REFUNDPREMIUM,
(SELECT (GETPREMIUMAMOUNT(DTL.POLICYFINANCEPOSTINGCODE,1)) FROM DUAL) IPPREMIUM,
(SELECT (GETPREMIUMAMOUNT(DTL.POLICYFINANCEPOSTINGCODE,2)) FROM DUAL) OPPREMIUM,
TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR FROM 
IM_NGI_JVPOSTING JV
JOIN IM_POLICYFINANCEPOSTINGDTL DTL ON DTL.POLICYFINANCEPOSTINGCODE = JV.REF_CODE
LEFT JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE = DTL.MEMBERPOLICYCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 0
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = JV.POLICYCODE AND JV.POLICYTYPE = 1 
WHERE  JVPREMIUMTYPE = 0
AND REFDATE BETWEEN TO_DATE('01/01/2016','DD/MM/RRRR')
AND TO_DATE('31/01/2016','DD/MM/RRRR') AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE
AND JV.GROUPCODE  IS NULL
 )M  WHERE REF_NO IN
(SELECT  REFNO FROM (SELECT (AP-RP) MEMBERPREMIUM,REFNO,MEMOTYPE,AMOUNT FROM
(SELECT SUM(REFUNDPREMIUM) RP,SUM(ADDPREMIUM) AP,Max(MEMOTYPE) MEMOTYPE,Max(AMOUNT) AMOUNT,REF_NO REFNO FROM 
(SELECT (NVL(ABS(DTL.REFUNDPREMIUM),0)) REFUNDPREMIUM, (NVL(ABS(DTL.ADDPREMIUM),0)) ADDPREMIUM,JV.AMOUNT,JV.MEMOTYPE,JV.REF_NO FROM 
IM_NGI_JVPOSTING JV,IM_POLICYFINANCEPOSTINGDTL DTL,IM_MEMBERPOLICY IMP 
WHERE  JVPREMIUMTYPE = 0
AND REFDATE BETWEEN To_Date('01/01/2016','DD/MM/RRRR')
AND To_Date('31/01/2016','DD/MM/RRRR') AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE  AND DTL.MEMBERPOLICYCODE = IMP.MEMBERPOLICYCODE
AND Nvl(IMP.A_SUBGROUPCODE,IMP.GROUPCODE) =  JV.GROUPCODE  AND   JV.GROUPCODE  IS NOT NULL
UNION ALL
SELECT  (NVL(ABS(DTL.REFUNDPREMIUM),0)) REFUNDPREMIUM, (NVL(ABS(DTL.ADDPREMIUM),0)) ADDPREMIUM,JV.AMOUNT,
JV.MEMOTYPE,JV.REF_NO FROM IM_NGI_JVPOSTING JV,IM_POLICYFINANCEPOSTINGDTL DTL  
WHERE  JVPREMIUMTYPE = 0
AND REFDATE BETWEEN To_Date('01/01/2016','DD/MM/RRRR')
AND To_Date('31/01/2016','DD/MM/RRRR') AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE
AND JV.GROUPCODE  IS NULL)
GROUP BY REF_NO)
)WHERE Abs(MEMBERPREMIUM) = Abs(AMOUNT)
) 


SELECT REF_NO,REFDATE,ACARDID,ISSUEDYEAR,POLICYID,POLICYNAME,APD_INCEPTION_DATE,APD_EXPIRY_DATE,MANAGEDBY,
ADDPREMIUM,REFUNDPREMIUM,IPPREMIUM,OPPREMIUM,UWYEAR,PREMIUMAMOUNT  FROM (SELECT POL.*,
SF_GETFMCACTUARYCOMMISSION_FNC(POLICYFINANCEPOSTINGCODE,1,AMOUNT, PREMIUMAMOUNT) AS AGENTCOM,
SF_GETFMCACTUARYCOMMISSION_FNC(POLICYFINANCEPOSTINGCODE,2,AMOUNT,PREMIUMAMOUNT) AS INTROCOM,
ROUND(NVL(PREMIUMAMOUNT,0)*(NGIFEES/100),2)  NGI_FEES
  FROM (SELECT DECODE(MEMOTYPE,1,JVPOS.AMOUNT,(JVPOS.AMOUNT*-1)) AMOUNT,JVPOS.REF_NO,JVPOS.REFDATE,MEMPOL.ACARDID,
TO_CHAR(JVPOS.REFDATE,'RRRR') ISSUEDYEAR,
NVL(MEMPOL.APOLICYID,(SELECT POLICYID FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE)) POLICYID,
NVL(MEMPOL.APOLICYNAME,(SELECT POLICYNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  =  NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE)) POLICYNAME,
NVL(MEMBERSTARTDATE,POLICYSTARTDDATE) APD_INCEPTION_DATE,
NVL(MEMBERENDDATE,POLICYENDDDATE) APD_EXPIRY_DATE,
POLEND.FINANCENO,(NVL(ABS(DET.ADDPREMIUM),0)) ADDPREMIUM,
SF_GETACTUARY_DEBITPREMIUM(NVL(JVPOS.GROUPCODE,NVL(MEMPOL.A_SUBGROUPCODE,MEMPOL.GROUPCODE)),POLEND.POLICYFINANCEPOSTINGCODE,JVPOS.AMOUNT,JVPOS.MEMOTYPE,MEMPOL.MEMBERPOLICYCODE,JVPOS.REF_NO) PREMIUMAMOUNT,
(NVL(ABS(DET.REFUNDPREMIUM),0)) REFUNDPREMIUM,NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) POLICYCODE,
POLEND.POLICYTYPECODE,POLEND.POLICYFINANCEPOSTINGCODE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE) MANAGEDBY,
DECODE(JVPOS.MEMOTYPE,1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*-1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*1)  NGIFEES, 
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,1)) FROM DUAL) IPPREMIUM,
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,2)) FROM DUAL) OPPREMIUM,TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR
FROM (SELECT * FROM IM_POLICYFINANCEPOSTING WHERE POLICYFINANCEPOSTINGCODE <> 100000000000010692) POLEND
JOIN IM_POLICYFINANCEPOSTINGDTL DET ON DET.POLICYFINANCEPOSTINGCODE = POLEND.POLICYFINANCEPOSTINGCODE
LEFT JOIN IM_MEMBERPOLICY MEMPOL ON MEMPOL.MEMBERPOLICYCODE = DET.MEMBERPOLICYCODE
JOIN IM_NGI_JVPOSTING JVPOS ON JVPOS.REF_CODE = POLEND.POLICYFINANCEPOSTINGCODE AND SOURCECODE = 1 AND JVPREMIUMTYPE = 0
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 1
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 2 WHERE JVPOS.MEMOTYPE = 1 AND
NVL(NVL(JVPOS.GROUPCODE,NVL(MEMPOL.A_SUBGROUPCODE,MEMPOL.GROUPCODE)),0) = NVL(NVL(MEMPOL.A_SUBGROUPCODE,MEMPOL.GROUPCODE),0) AND 
(TO_DATE(JVPOS.REFDATE,'DD/MM/RRRR') BETWEEN TO_DATE('01/01/2016','DD/MM/RRRR')  AND TO_DATE('31/01/2016','DD/MM/RRRR'))
UNION
SELECT DECODE(MEMOTYPE,1,JVPOS.AMOUNT,(JVPOS.AMOUNT*-1)) AMOUNT,JVPOS.REF_NO,JVPOS.REFDATE,MEMPOL.ACARDID,
TO_CHAR(JVPOS.REFDATE,'RRRR') ISSUEDYEAR,
NVL(MEMPOL.APOLICYID,(SELECT POLICYID FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE)) POLICYID,
NVL(MEMPOL.APOLICYNAME,(SELECT POLICYNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  =  NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE)) POLICYNAME,
NVL(MEMBERSTARTDATE,POLICYSTARTDDATE) APD_INCEPTION_DATE,
NVL(MEMBERENDDATE,POLICYENDDDATE) APD_EXPIRY_DATE,
POLEND.FINANCENO,(NVL(ABS(DET.ADDPREMIUM),0)) ADDPREMIUM,
SF_GETACTUARY_CREDITPREMIUM(POLEND.POLICYFINANCEPOSTINGCODE,JVPOS.AMOUNT,JVPOS.MEMOTYPE,MEMPOL.MEMBERPOLICYCODE,JVPOS.REF_NO) PREMIUMAMOUNT,
(NVL(ABS(DET.REFUNDPREMIUM),0)) REFUNDPREMIUM,NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) POLICYCODE,
POLEND.POLICYTYPECODE,POLEND.POLICYFINANCEPOSTINGCODE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE) MANAGEDBY,
DECODE(JVPOS.MEMOTYPE,1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*-1,NVL(POL.NGI_FEES,IPOL.NGI_FEES)*1) NGIFEES,
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,1)) FROM DUAL) IPPREMIUM,
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,2)) FROM DUAL) OPPREMIUM,TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR
FROM (SELECT * FROM IM_POLICYFINANCEPOSTING WHERE POLICYFINANCEPOSTINGCODE <> 100000000000010692) POLEND
JOIN IM_POLICYFINANCEPOSTINGDTL DET ON DET.POLICYFINANCEPOSTINGCODE = POLEND.POLICYFINANCEPOSTINGCODE
LEFT JOIN IM_MEMBERPOLICY MEMPOL ON MEMPOL.MEMBERPOLICYCODE = DET.MEMBERPOLICYCODE
JOIN IM_NGI_JVPOSTING JVPOS ON JVPOS.REF_CODE = POLEND.POLICYFINANCEPOSTINGCODE AND SOURCECODE = 1 AND JVPREMIUMTYPE = 0
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 1
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 2 WHERE JVPOS.MEMOTYPE = 0 AND 
(TO_DATE(JVPOS.REFDATE,'DD/MM/RRRR') BETWEEN TO_DATE('01/01/2016','DD/MM/RRRR')  AND TO_DATE('31/01/2016','DD/MM/RRRR'))
)POL)M
WHERE REF_NO IN
(SELECT  REFNO FROM (SELECT (AP-RP) MEMBERPREMIUM,REFNO,MEMOTYPE,AMOUNT FROM
(SELECT SUM(REFUNDPREMIUM) RP,SUM(ADDPREMIUM) AP,Max(MEMOTYPE) MEMOTYPE,Max(AMOUNT) AMOUNT,REF_NO REFNO FROM 
(SELECT (NVL(ABS(DTL.REFUNDPREMIUM),0)) REFUNDPREMIUM, (NVL(ABS(DTL.ADDPREMIUM),0)) ADDPREMIUM,JV.AMOUNT,JV.MEMOTYPE,JV.REF_NO FROM 
IM_NGI_JVPOSTING JV,IM_POLICYFINANCEPOSTINGDTL DTL,IM_MEMBERPOLICY IMP 
WHERE  JVPREMIUMTYPE = 0
AND REFDATE BETWEEN To_Date('01/01/2016','DD/MM/RRRR')
AND To_Date('31/01/2016','DD/MM/RRRR') AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE  AND DTL.MEMBERPOLICYCODE = IMP.MEMBERPOLICYCODE
AND Nvl(IMP.A_SUBGROUPCODE,IMP.GROUPCODE) =  JV.GROUPCODE  AND   JV.GROUPCODE  IS NOT NULL
UNION ALL
SELECT  (NVL(ABS(DTL.REFUNDPREMIUM),0)) REFUNDPREMIUM, (NVL(ABS(DTL.ADDPREMIUM),0)) ADDPREMIUM,JV.AMOUNT,
JV.MEMOTYPE,JV.REF_NO FROM IM_NGI_JVPOSTING JV,IM_POLICYFINANCEPOSTINGDTL DTL  
WHERE  JVPREMIUMTYPE = 0
AND REFDATE BETWEEN To_Date('01/01/2016','DD/MM/RRRR')
AND To_Date('31/01/2016','DD/MM/RRRR') AND JV.REF_CODE = DTL.POLICYFINANCEPOSTINGCODE
AND JV.GROUPCODE  IS NULL)
GROUP BY REF_NO)
)WHERE Abs(MEMBERPREMIUM) <> Abs(AMOUNT)
)