SELECT *  FROM (SELECT DECODE(MEMOTYPE,1,JVPOS.AMOUNT,(JVPOS.AMOUNT*-1)) AMOUNT,JVPOS.REF_NO,JVPOS.REFDATE,MEMPOL.ACARDID,
TO_CHAR(JVPOS.REFDATE,'RRRR') ISSUEDYEAR,
NVL(MEMPOL.APOLICYID,(SELECT POLICYID FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE)) POLICYID,
NVL(MEMPOL.APOLICYNAME,(SELECT POLICYNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  =  NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE)) POLICYNAME,
NVL(MEMBERSTARTDATE,POLICYSTARTDDATE) APD_INCEPTION_DATE,
NVL(MEMBERENDDATE,POLICYENDDDATE) APD_EXPIRY_DATE,
POLEND.FINANCENO,(NVL(ABS(DET.ADDPREMIUM),0)) ADDPREMIUM,
SF_GETACTUARY_DEBITPREMIUM(NVL(JVPOS.GROUPCODE,NVL(MEMPOL.A_SUBGROUPCODE,MEMPOL.GROUPCODE)),POLEND.POLICYFINANCEPOSTINGCODE,JVPOS.AMOUNT,JVPOS.MEMOTYPE,MEMPOL.MEMBERPOLICYCODE,JVPOS.REF_NO) PREMIUMAMOUNT,
(NVL(ABS(DET.REFUNDPREMIUM),0)) REFUNDPREMIUM,NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) POLICYCODE,
POLEND.POLICYTYPECODE,POLEND.POLICYFINANCEPOSTINGCODE,
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE) MANAGEDBY,NVL(POL.NGI_FEES,IPOL.NGI_FEES) NGIFEES,
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,1)) FROM DUAL) IPPREMIUM,
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,2)) FROM DUAL) OPPREMIUM,TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR
FROM (SELECT * FROM IM_POLICYFINANCEPOSTING WHERE POLICYFINANCEPOSTINGCODE <> 100000000000010692) POLEND
JOIN IM_POLICYFINANCEPOSTINGDTL DET ON DET.POLICYFINANCEPOSTINGCODE = POLEND.POLICYFINANCEPOSTINGCODE
LEFT JOIN IM_MEMBERPOLICY MEMPOL ON MEMPOL.MEMBERPOLICYCODE = DET.MEMBERPOLICYCODE
JOIN IM_NGI_JVPOSTING JVPOS ON JVPOS.REF_CODE = POLEND.POLICYFINANCEPOSTINGCODE AND SOURCECODE = 1 AND JVPREMIUMTYPE = 0
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 1
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 2 WHERE JVPOS.MEMOTYPE = 1 AND
NVL(NVL(JVPOS.GROUPCODE,NVL(MEMPOL.A_SUBGROUPCODE,MEMPOL.GROUPCODE)),0) = NVL(NVL(MEMPOL.A_SUBGROUPCODE,MEMPOL.GROUPCODE),0) AND 
(TO_DATE(JVPOS.REFDATE,'DD/MM/RRRR') BETWEEN TO_DATE('01/02/2016','DD/MM/RRRR')  AND TO_DATE('29/02/2016','DD/MM/RRRR'))
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
(SELECT OWNERNAME FROM GETPOLICYNAME_VW WHERE POLICYCODE  = NVL(POLEND.POLICYCODE,POLEND.INDIVIDUALPOLICYCODE) AND TYPEE = POLEND.POLICYTYPECODE) MANAGEDBY,NVL(POL.NGI_FEES,IPOL.NGI_FEES) NGIFEES,
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,1)) FROM DUAL) IPPREMIUM,
(SELECT (GETPREMIUMAMOUNT(POLEND.POLICYFINANCEPOSTINGCODE,2)) FROM DUAL) OPPREMIUM,TO_CHAR(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR
FROM (SELECT * FROM IM_POLICYFINANCEPOSTING WHERE POLICYFINANCEPOSTINGCODE <> 100000000000010692) POLEND
JOIN IM_POLICYFINANCEPOSTINGDTL DET ON DET.POLICYFINANCEPOSTINGCODE = POLEND.POLICYFINANCEPOSTINGCODE
LEFT JOIN IM_MEMBERPOLICY MEMPOL ON MEMPOL.MEMBERPOLICYCODE = DET.MEMBERPOLICYCODE
JOIN IM_NGI_JVPOSTING JVPOS ON JVPOS.REF_CODE = POLEND.POLICYFINANCEPOSTINGCODE AND SOURCECODE = 1 AND JVPREMIUMTYPE = 0
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 1
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 2 WHERE JVPOS.MEMOTYPE = 0 AND 
(TO_DATE(JVPOS.REFDATE,'DD/MM/RRRR') BETWEEN TO_DATE('01/02/2016','DD/MM/RRRR')  AND TO_DATE('29/02/2016','DD/MM/RRRR'))
)