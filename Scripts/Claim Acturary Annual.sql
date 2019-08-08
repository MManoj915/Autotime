INSERT INTO IM_CLAIM_ACTUARY   SELECT * FROM
(SELECT  MEMBERPOLICYCODE,PROVIDER_CODE,SOURCEPROVIDER,TREATMENT_TYPE,
POLICYCODE,REQUEST_TYPE,CLAIM_STATUS,ISNEXTCARE,CDCODE,LASTMODIFIEDON,BENEFICARYSHARE,ENCOUNTER_END_DATE,
ACR_FORM_NO,CLAIM_CODE,CARDNO,AMEMBERNAME,POLICYNAME,POLICYHOLDER,UNIQ_CLAIM_ID,
REPORTED_DATE,OCCURRENCE,PAY_DATE,REQUEST_AMOUNT,
Decode(ISREVERSEDFORDEBIT,0,RECOVERYAMOUNT,1,0)RECOVERYAMOUNT,
GROSSAMOUNT,DISCOUNTPERCENT,NETWORKTYPE,MANAGEDBY,POLICYID,DECODE(SUBSTR(PAYMENTREFNO,0,2),'01',ABS(NETAMOUNT)*-1,(NETAMOUNT))NETAMOUNT,PAYMENTREFNO,UWYEAR FROM(
SELECT  MEMBERPOLICYCODE,PROVIDER_CODE,SOURCEPROVIDER,TREATMENT_TYPE,
POLICYCODE,REQUEST_TYPE,CLAIM_STATUS,ISNEXTCARE,CDCODE,LASTMODIFIEDON,BENEFICARYSHARE,ENCOUNTER_END_DATE,
ACR_FORM_NO,CLAIM_CODE,CARDNO,AMEMBERNAME,POLICYNAME,POLICYHOLDER,UNIQ_CLAIM_ID,REPORTED_DATE,OCCURRENCE,PAY_DATE,REVERSEREFDATE,REQUEST_AMOUNT,Decode(ISREVERSEDFORDEBIT,0,RECOVERYAMOUNT,1,0)RECOVERYAMOUNT,
GROSSAMOUNT,DISCOUNTPERCENT,NETWORKTYPE,MANAGEDBY,POLICYID,Decode(ISREVERSEDFORDEBIT,0,NETAMOUNT,1,(RECOVERYAMOUNT*-1)) NETAMOUNT,PAYMENTREFNO,ISREVERSEDFORDEBIT,UWYEAR FROM(
SELECT MEMBERPOLICYCODE,PROVIDER_CODE,SOURCEPROVIDER,TREATMENT_TYPE,
POLICYCODE,REQUEST_TYPE,CLAIM_STATUS,ISNEXTCARE,CDCODE,LASTMODIFIEDON,BENEFICARYSHARE,ENCOUNTER_END_DATE,
ACR_FORM_NO,CLAIM_CODE,CARDNO,AMEMBERNAME,POLICYNAME,POLICYHOLDER,UNIQ_CLAIM_ID,REPORTED_DATE,OCCURRENCE,PAY_DATE,REQUEST_AMOUNT,RECOVERYAMOUNT,GROSSAMOUNT,DISCOUNTPERCENT,NETWORKTYPE,
MANAGEDBY,POLICYID,REVERSEREFDATE,NETAMOUNT,PAYMENTREFNO,ISREVERSEDFORDEBIT,UWYEAR
FROM (SELECT HDR.*,GROSSAMOUNT - GROSSAMOUNT / 100 * DISCOUNTPERCENT NETAMOUNT FROM (SELECT 
IMP.MEMBERPOLICYCODE,HDR.PROVIDER_CODE,HDR.SOURCEPROVIDER,HDR.TREATMENT_TYPE,
HDR.POLICYCODE,HDR.REQUEST_TYPE,HDR.CLAIM_STATUS,HDR.ISNEXTCARE,HDR.CDCODE,HDR.LASTMODIFIEDON,
(Nvl(HDR.CO_INS_VALUE,0) + Nvl(HDR.DEDUCTABLEVALUE,0)) BENEFICARYSHARE,HDR.ENCOUNTER_END_DATE,
HDR.ACR_FORM_NO,HDR.CLAIM_CODE,
Nvl(GRP.GROUP_NAME,Nvl(POL.POLICYNAME,IPOL.POLICYNAME)) POLICYHOLDER,Nvl(POL.POLICYNAME,IPOL.POLICYNAME) POLICYNAME,
Nvl(HDR.CARDNO,HDR.FMCCARDNO) CARDNO,
Nvl(IMP.AMEMBERNAME,HDR.FMCMEMBERNAME) AMEMBERNAME,
HDR.INVOICENUMBER UNIQ_CLAIM_ID,TO_DATE(HDR.TRANSACTIONDATE, 'DD/MM/RRRR')REPORTED_DATE,
TO_DATE(HDR.ENCOUNTER_START_DATE, 'DD/MM/RRRR')OCCURRENCE,TO_DATE(HDR.PAYMENTREFDATE, 'DD/MM/RRRR') PAY_DATE,
TO_CHAR(HDR.REVERSEREFDATE, 'DD/MM/RRRR') REVERSEREFDATE,
HDR.REQUEST_AMOUNT,DECODE(HDR.RFP,1,HDR.DENAILVALUE,NVL((SELECT SUM (DENIEDAMOUNT)FROM IM_RECOVERYAMOUNT_VW DEC
WHERE DEC.CLAIM_CODE = HDR.CLAIM_CODE),0))RECOVERYAMOUNT,
(ROUND (HDR.APPROVED_AMOUNT, 2)+ (SELECT NVL (SUM (NVL (DENIEDAMOUNT, 0)), 0) DENIEDAMOUNT
FROM IM_DECLINEAMOUNT_VW VW WHERE VW.CLAIM_CODE = HDR.CLAIM_CODE AND HDR.CLAIM_CODE NOT IN(SELECT CLAIM_CODE FROM REJDEC)AND REQUEST_TYPE IN (1, 2)))
GROSSAMOUNT,NVL (HDR.DISCOUNTPERCENTAGE, 0) DISCOUNTPERCENT,DECODE (ISNEXTCARE,
1, 'In net',
(SELECT L_INNET
FROM TABLE (
SF_GETCLAIMSNWTYPE_FNC (
HDR.POLICYCODE,
HDR.CATEGORY_CODE,
HDR.PROVIDER_CODE,
1))))
NETWORKTYPE,NVL (GEN8.CONSTANTNAME, 'NGI') MANAGEDBY,NVL (POL.POLICYID, IPOL.POLICYID) POLICYID,HDR.MEMOTYPE,HDR.PAYMENTREFNO,
Nvl(HDR.ISREVERSEDFORDEBIT,0) ISREVERSEDFORDEBIT,To_Char(Nvl(POL.STARTDATE,IPOL.STARTDATE),'RRRR') UWYEAR FROM 
(SELECT * FROM IM_CLAIM_PROCESS_HEADER WHERE CLAIM_CODE NOT IN(100000000002543131)
AND PAYMENTREFNO NOT IN(SELECT * FROM IM_NOV_REVERSEDOC)
UNION ALL
SELECT * FROM IM_CLAIM_PROCESS_REV_JV)HDR
LEFT JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE = HDR.MEMBERPOLICYCODE
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE = NVL (IMP.POLICYCODE, HDR.POLICYCODE) --AND IMP.TYPEE = 1
LEFT JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = NVL (IMP.POLICYCODE, HDR.POLICYCODE) --AND IMP.TYPEE = 2
LEFT JOIN IM_GROUPS GRP ON GRP.GROUP_CODE = POL.GROUPCODE
LEFT JOIN GENCONSTANT GEN8 ON GEN8.CONSTANTVALUE = NVL (POL.OWNERCODE, IPOL.OWNERCODE) AND GEN8.CATEGORY = 'NGIQUOTATIONTYPE' AND UPPER (GEN8.LANGUAGECODE) = 'EN-US'
WHERE CLAIM_CODE NOT IN (SELECT CLAIMCODE FROM IM_ENDPOSTCLAIMDTL WHERE ENDORESMENTCODE IN (SELECT CLAIMREFCODE FROM JAN12011 WHERE REVERSEJVCODE =
100000000000007308))) HDR))CHDR) WHERE 
(TO_DATE(PAY_DATE,'DD/MM/RRRR') BETWEEN    To_Date('01/01/2016','DD/MM/RRRR') AND To_Date('21/12/2016','DD/MM/RRRR')  OR 
TO_DATE(REVERSEREFDATE,'DD/MM/RRRR') BETWEEN    To_Date('01/01/2016','DD/MM/RRRR') AND To_Date('21/12/2016','DD/MM/RRRR'))) 