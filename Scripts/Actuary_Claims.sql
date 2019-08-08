SELECT   UWYEAR,POLICYID,POLICYNAME,NETWORKNAME,POLICYSTARTDATE,POLICYENDDATE,ANNUALLIMIT,POLICYHOLDER,IPLIMIT,IPDEDUCTIBLE,IPCOINSURANCE,
OPLIMIT,OPDEDUCTIBLE,OPCOINSURANCE,DENTALLIMIT,DENTALDEDUCTIBLE,DENTALCOINSURANCE,OPTICALLIMIT,OPTICALDEDUCTIBLE,OPTICALCOINSURANCE,
MATLIMIT,MATDEDUCTIBLE,MATCOINSURANCE,CARDNO,AMEMBERNAME,RELATION,GENDER,NATIONALITY,DOB,INCEPTIONDATE,EXPIRYDATE,
UNIQ_CLAIM_ID,OCCURRENCE,PAY_DATE,CLAIMSTATUS,REQUEST_AMOUNT,NETAMOUNT FROM IM_UW_ACTUARY_CLAIM MEM
LEFT JOIN IM_EFORM_CLAIM_PRC_LIMIT LIM ON LIM.POLICYCODE = MEM.ANNUALPOLICYCODE AND LIM.POLICYTYPE = MEM.ANNUALPOLICYTYPE     
AND LIM.CATEGORYCODE = MEM.CATEGORYCODE WHERE MEM.CATEGORYCODE IS NOT NULL
UNION  
SELECT   UWYEAR,POLICYID,POLICYNAME,NETWORKNAME,POLICYSTARTDATE,POLICYENDDATE,ANNUALLIMIT,POLICYHOLDER,IPLIMIT,IPDEDUCTIBLE,IPCOINSURANCE,
OPLIMIT,OPDEDUCTIBLE,OPCOINSURANCE,DENTALLIMIT,DENTALDEDUCTIBLE,DENTALCOINSURANCE,OPTICALLIMIT,OPTICALDEDUCTIBLE,OPTICALCOINSURANCE,
MATLIMIT,MATDEDUCTIBLE,MATCOINSURANCE,CARDNO,AMEMBERNAME,RELATION,GENDER,NATIONALITY,DOB,INCEPTIONDATE,EXPIRYDATE,
UNIQ_CLAIM_ID,OCCURRENCE,PAY_DATE,CLAIMSTATUS,REQUEST_AMOUNT,NETAMOUNT FROM IM_UW_ACTUARY_CLAIM MEM
LEFT JOIN IM_EFORM_CLAIM_PRC_LIMIT LIM ON LIM.POLICYCODE = MEM.ANNUALPOLICYCODE AND LIM.POLICYTYPE = MEM.ANNUALPOLICYTYPE     
WHERE MEM.CATEGORYCODE IS NULL