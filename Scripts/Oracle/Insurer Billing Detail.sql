Insurer Billing Detail :

select Bil.* from(select Bil.*,adm.UserName CreatedUserName,adm1.UserName ModifiedUserName,gen.constantname as AuthorizedStatusName,  
con.COUNTRYNAME CountryName,Place.PLACENAME CityName,State.PROVINCENAME StateName,BANK.CONSTANTNAME BankName,GEC.CURRENCYNAME CurrencyName from IM_GENERAL_COMM_BILLING_DETAIL Bil   
    
join (select constantValue,ConstantName from genconstant where category='AUTHORIZEDSTATUS' and languagecode='en-US')gen on gen.constantValue=Bil.AuthorizedStatus   
left join gencountry con on con.COUNTRYCODE=Bil.COUNTRYCODE  
left join genPlace Place on Place.PLACECODE=Bil.CityCode  
left join genconstant bank on BANK.CONSTANTVALUE=Bil.BANKCODE AND category='SP_BANKS' and languagecode='en-US'  
left join genprovince State on State.PROVINCECODE=Bil.StateCode  
join admuser adm on adm.UserCode=Bil.CREATEDBY  
join admuser adm1 on adm1.UserCode=Bil.LASTMODIFIEDBY  
left join gencurrency gec on GEC.CURRENCYCODE=BIL.CURRENCYCODE  
ORDER BY GENERALCOMMBILLINGCODE DESC)Bil