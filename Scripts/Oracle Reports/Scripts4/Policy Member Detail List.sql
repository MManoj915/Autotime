select mas.* from (
select to_char(Mem1.FIRST_NAME) MEMBERNAME,         
'Premium' PremiumTypeName,
(nvl(mas.ADDITIONVALUE,0)+nvl(mas.DELTIONVALUE,0) ) PREMIUM_VALUE ,
to_char(gen1.CONSTANTNAME)  GenderName,
CEIL(months_between(sysdate,Mem1.DATE_OF_BIRTH)/12) as Age,
to_char(gen2.CONSTANTNAME) MemberRelationName,
to_char(GEN3.CONSTANTNAME) MaritalStatusName,
mas.POLICYCODE,
to_char(Mem1.MEMBER_ID) MEMBERID,
to_char(CAT.CATEGORY_NAME) CATEGORY_NAME,
to_char(ParMem.Member_ID) PARENTID,
to_char(Mem1.CARDID) CARDID,
to_char(GEN6.CONSTANTNAME) MEMBERSTATUS,
Mem1.DATE_OF_BIRTH,                                                               
nvl(mas.MEMBERSTARTDATE,Mas.POLICYStartddate) INCEPTIONDATE,
nvl(mas.MEMBERENDDATE,Mas.POLICYENDDDATE) MEMBERENDDATE,  
to_char(Mem1.EMIRATES_ID) EMIRATESID,
to_char(gen4.CONSTANTNAME) LOCATIONNAME,
to_char(Mem1.STAFF_ID) A_STAFF_ID,
to_char(gen5.CONSTANTNAME) NATIONALITY,
nvl(mas.ADDITIONVALUE,0) ADDITIONVALUE,
nvl(mas.DELTIONVALUE,0) DELTIONVALUE,
grp.GROUP_NAME,

NVL(MEM1.MOBILE_NUMBER,Mem1.MOBILE_NO) AS MOBILE_NO ,
NVL(Mem1.EMAIL_ID,MEM1.EMAILID) AS  EMAIL_ID,
Mem1.PASSPORT_NO  ,
Mem1.UIDNUMBER MEMBER_UID
from
(select mas.*
,(SELECT SUM (dtl.PREMIUM_VALUE)
FROM IM_MEMBERPOLICYPREMIUMDTL dtl
WHERE dtl.MEMBERPOLICYCODE = mas.MEMBERPOLICYCODE)
ADDITIONVALUE,
(SELECT SUM (dtl.PREMIUM_VALUE * -1)
FROM IM_MEMPOLICYREFUNDPREMDTL dtl
WHERE dtl.MEMBERPOLICYCODE = mas.MEMBERPOLICYCODE)
DELTIONVALUE
from IM_MemberPOLICY mas
where mas.typee=1 and mas.POLICYCODE = Nvl(:policycode,mas.POLICYCODE) 
) mas
join im_members Mem1 on Mem1.MEMBER_CODE=mas.MEMBERCODE and Mem1.CARDID is not null 
left join im_groups Grp on Grp.Group_Code=nvl(Mem1.PARENTGROUPCODE,Mem1.Group_Code)
join im_categories cat on CAT.CATEGORY_CODE=mas.CATEGORYCODE
left join im_members ParMem on ParMem.MEMBER_CODE=mas.APARENT_ID         
left join genconstant gen1 on GEN1.CATEGORY='Gender' and gen1.CONSTANTVALUE=Mem1.GENDER and gen1.languagecode='en-US'
LEFT join genconstant gen2 on gen2.CATEGORY='MEMBERRELATION' and gen2.CONSTANTVALUE=Mem1.RELATION and gen2.languagecode='en-US'
left join genconstant gen3 on gen3.CATEGORY='MARITAL_STATUS' and gen3.CONSTANTVALUE=Mem1.MARITAL_STATUS and gen3.LANGUAGECODE='en-US'
left join genconstant gen4 on gen4.CATEGORY='MEMBERLOCATION' and gen4.CONSTANTVALUE=Mem1.LOCATION and gen4.LANGUAGECODE='en-US'
left join genconstant gen5 on GEN5.CONSTANTVALUE=Mem1.NATIONALITY  and GEN5.CATEGORY='FND_NATIONALITY' and GEN5.LANGUAGECODE='en-US'
left join genconstant gen6 on GEN6.CATEGORY='CUSSTATUS' and GEN6.CONSTANTVALUE=Mem1.Status and GEN6.LANGUAGECODE='en-US'
order by nvl(mas.APARENT_ID,mas.MEMBERCODE ),nvl2(mas.APARENT_ID,0,1),Mem1.MEMBER_ID desc   
) mas
