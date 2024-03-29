SELECT REPLACE(MEMPOL.APOLICYID,'-','/') POLICYNO,MEMPOL.ACARDID CARDNO,MEMPOL.APOLICYNAME CLIENTNAME,
TO_CHAR(MEMPOL.POLICYSTARTDDATE,'DD/MM/RRRR') POLICYEFFECTIVEDATE,
TO_CHAR(MEMPOL.POLICYENDDDATE,'DD/MM/RRRR') POLICYEXPIRYDATE,
CAT.CATEGORY_NAME,MEMPOL.AMEMBERNAME,
DECODE (UPPER (GEN2.CONSTANTNAME),'SELF', 'PRINCIPAL','WIFE', 'SPOUSE','SON', 'CHILD','DAUGHTER', 'CHILD','DTR', 'CHILD','SPOUSE', 'SPOUSE')RELATION,
TO_CHAR(MEMPOL.MEMBERSTARTDATE,'DD/MM/RRRR') MEMBEREFFECTIVEDATE,
NVL(TO_CHAR(MEMPOL.MEMBERENDDATE,'DD/MM/RRRR'),TO_CHAR(Nvl(MEMPOL.MEMBERENDDATE,MEMPOL.POLICYENDDDATE),'DD/MM/RRRR')) MEMBEREXPIRYDATE,
TO_CHAR(MEM.MEMBERSINCE,'DD/MM/RRRR') MEMBERORGINALEFFECTIVEDATE,
DECODE(MEMPOL.TYPEE,1,'GROUP',2,'INDIVIDUAL') POLICYTYPE,'NON DUBAI' EMIRATE
FROM IM_MEMBERPOLICY MEMPOL
JOIN IM_MEMBERS MEM ON MEM.MEMBER_CODE = MEMPOL.MEMBERCODE                 
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 1 
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 2
LEFT JOIN IM_CATEGORIES CAT ON CAT.CATEGORY_CODE = MEMPOL.CATEGORYCODE
LEFT JOIN GENCONSTANT GEN2  ON  GEN2.CONSTANTVALUE = MEMPOL.ARELATION AND GEN2.CATEGORY = 'MEMBERRELATION'  AND UPPER (GEN2.LANGUAGECODE) = 'EN-US'
WHERE  SYSDATE BETWEEN MEMPOL.MEMBERSTARTDATE AND Nvl(MEMPOL.MEMBERENDDATE,MEMPOL.POLICYENDDDATE)
AND (POL.OWNERCODE = 3 OR IPOL.OWNERCODE = 3) AND 
(CAT.CATEGORY_NAME LIKE '%AUH%' OR IPOL.INDIVIDUALPOLICYCODE IN(
SELECT INDIVIDUALPOLICYCODE FROM IM_NGIINDPOLICYCATEGORY WHERE PLANCODE IN
(SELECT PLAN_CODE FROM IM_PLANEMIRATES WHERE EMIRATECODE=100000000000001989)))
UNION ALL
SELECT REPLACE(MEMPOL.APOLICYID,'-','/') POLICYNO,MEMPOL.ACARDID CARDNO,MEMPOL.APOLICYNAME CLIENTNAME,
TO_CHAR(MEMPOL.POLICYSTARTDDATE,'DD/MM/RRRR') POLICYEFFECTIVEDATE,
TO_CHAR(MEMPOL.POLICYENDDDATE,'DD/MM/RRRR') POLICYEXPIRYDATE,
CAT.CATEGORY_NAME,MEMPOL.AMEMBERNAME,
DECODE (UPPER (GEN2.CONSTANTNAME),'SELF', 'PRINCIPAL','WIFE', 'SPOUSE','SON', 'CHILD','DAUGHTER', 'CHILD','DTR', 'CHILD','SPOUSE', 'SPOUSE')RELATION,
TO_CHAR(MEMPOL.MEMBERSTARTDATE,'DD/MM/RRRR') MEMBEREFFECTIVEDATE,
NVL(TO_CHAR(MEMPOL.MEMBERENDDATE,'DD/MM/RRRR'),TO_CHAR(Nvl(MEMPOL.MEMBERENDDATE,MEMPOL.POLICYENDDDATE),'DD/MM/RRRR')) MEMBEREXPIRYDATE,
TO_CHAR(MEM.MEMBERSINCE,'DD/MM/RRRR') MEMBERORGINALEFFECTIVEDATE,
DECODE(MEMPOL.TYPEE,1,'GROUP',2,'INDIVIDUAL') POLICYTYPE,'DUBAI' EMIRATES
FROM IM_MEMBERPOLICY MEMPOL
JOIN IM_MEMBERS MEM ON MEM.MEMBER_CODE = MEMPOL.MEMBERCODE                 
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 1 
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = MEMPOL.POLICYCODE AND MEMPOL.TYPEE = 2
LEFT JOIN IM_CATEGORIES CAT ON CAT.CATEGORY_CODE = MEMPOL.CATEGORYCODE
LEFT JOIN GENCONSTANT GEN2  ON  GEN2.CONSTANTVALUE = MEMPOL.ARELATION AND GEN2.CATEGORY = 'MEMBERRELATION'  AND UPPER (GEN2.LANGUAGECODE) = 'EN-US'
WHERE  SYSDATE BETWEEN MEMPOL.MEMBERSTARTDATE AND Nvl(MEMPOL.MEMBERENDDATE,MEMPOL.POLICYENDDDATE)
AND (POL.OWNERCODE = 3 OR IPOL.OWNERCODE = 3) AND 
CAT.CATEGORY_NAME NOT LIKE '%AUH%' AND NVL(POL.POLICYCODE,IPOL.INDIVIDUALPOLICYCODE) NOT IN(
SELECT INDIVIDUALPOLICYCODE FROM IM_NGIINDPOLICYCATEGORY WHERE PLANCODE IN
(SELECT PLAN_CODE FROM IM_PLANEMIRATES WHERE EMIRATECODE=100000000000001989)
UNION ALL
SELECT POLICYCODE FROM IM_POLICY_CATEGORYDTL WHERE PLANCODE IN
(SELECT PLAN_CODE FROM IM_PLANEMIRATES WHERE EMIRATECODE=100000000000001989))