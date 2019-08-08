Category Master :

SELECT IC.*,gen.constantname emiratename FROM IM_CATEGORIES IC 
left join genconstant gen on gen.category='MEMBERLOCATION' and gen.constantvalue=IC.emirate and gen.languagecode='en-US'   
order by IC.CATEGORY_CODE desc