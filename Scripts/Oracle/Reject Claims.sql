IM_RJTCLAIMWITHDCODE_ORA(P_CLAIM_CODE NVARCHAR2,USERCODE NUMBER,P_DENIALCODE NUMBER) - REJECT WITH DENIAL REASON
IM_RJTCLAIMWITHDREASON_ORA(P_CLAIM_CODE NVARCHAR2,USERCODE NUMBER) - REJECT CLAIMS
AV_PROCESS_CLAIMS.AV_PROCESS_CLAIMS(P_CLAIMCODELIST VARCHAR2,P_SPLITER VARCHAR2,P_USERCODE NUMBER,P_RESULT OUT NUMBER) - PROCESS CLAIMS

select icdm.*,den.code detailname,den.longdesc,gen3.constantname recoveryfromname from im_claim_denialmapping icdm  
left join im_version_detalis den on den.detailcode=icdm.denailcode  
left join genconstant gen3 on gen3.constantvalue=icdm.recoveryfrom and gen3.category='deniedfor' and gen3.languagecode ='en-us'
