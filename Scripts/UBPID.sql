UBPID MASTER:

select * from IM_UBIP

UBPID DETAIL :

select UBI.*,BEF.BENEFIT_ID BenefitID, BEF.BENEFIT_NAME BenefitName,GEN.CONSTANTNAME AnnualLimitTypeName,GEN1.CONSTANTNAME InpatientName,GEN2.CONSTANTNAME OutpatientName from IM_UBPIDDETAIL UBI left join IM_BENEFIT_CODES bef on BEF.BENEFIT_CODE=UBI.BENEFITCODE left join genconstant gen on GEN.CONSTANTVALUE=UBI.ANNUALLIMITTYPE and GEN.CATEGORY='CALCMETHOD' and GEN.LANGUAGECODE='en-US' left join genconstant gen1 on GEN1.CONSTANTVALUE=UBI.INPATIENT and GEN1.CATEGORY='CALCMETHOD' and GEN1.LANGUAGECODE='en-US' left join genconstant gen2 on GEN2.CONSTANTVALUE=UBI.OUTPATIENT and  GEN2.CATEGORY='CALCMETHOD' and GEN2.LANGUAGECODE='en-US' where UBI.Ubipcode=10000000000000001