select G.*,G1.SHORTNAME FROMCURRENCYNAME,G2.SHORTNAME TOCURRENCYNAME FROM GENEXCHANGERATE G
JOIN GENCURRENCY G1 ON G1.CURRENCYCODE = G.FROMCURRENCYCODE
JOIN GENCURRENCY G2 ON G2.CURRENCYCODE = G.TOCURRENCYCODE