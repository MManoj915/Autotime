SELECT * FROM(SELECT  MAX((DECODE(MEMOTYPE,0,JV.AMOUNT,JV.AMOUNT*-1))) JAMOUNT,SUM(TOTALPREMIUM) AAMOUNT,
JV.REF_NO FROM IM_NGI_JVPOSTING JV
LEFT JOIN IM_UW_ACTUARY_MEM MEM ON JV.REF_NO = MEM.REF_NO
WHERE JV.REFDATE BETWEEN TO_DATE('01/01/2016','DD/MM/RRRR') AND TO_DATE('31/12/2016','DD/MM/RRRR')
AND JV.REF_CODE IS NOT NULL AND JV.JVPREMIUMTYPE = 0  
AND JV.JVPOSTINGCODE NOT IN(SELECT * FROM IM_REVERSE_JV)
GROUP BY JV.REF_NO) WHERE ABS(NVL(JAMOUNT,0)) <> ABS(NVL(AAMOUNT,0))