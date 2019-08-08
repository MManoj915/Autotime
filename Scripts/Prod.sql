fn_generateprod_rpt

NextCare
========

Group Report
select * from table(SF_NEXTCAREPRODUCTIONGROUPPOL('YYYNNNN','EN-US','ALL','68bba06c-622e-4302-9c01-6b21dda0b7ff',to_date('01-Jan-2016 00:00:00','DD-MON-YYYY HH24:MI:SS'),to_date('07-Dec-2016 00:00:00','DD-MON-YYYY HH24:MI:SS'),0,0,'ALL','ALL','ALL','2016'))
                                                                                                                                                                           
Individual Report
select * from table(SF_PRODUNCTIONINDIVNEXTCARE('EN-US','YYNNNN','ALL',to_date('01-Jan-2016 00:00:00','DD-MON-YYYY HH24:MI:SS'),to_date('01-Nov-2016 00:00:00','DD-MON-YYYY HH24:MI:SS'),0,0,'ALL','ALL','ALL','68bba06c-622e-4302-9c01-6b21dda0b7ff','2016'))

FMC
===

Group Report
SELECT *   FROM TABLE (SF_NEXTCAREPRODUCTIONGROUPPOL ('YYYNNNN','EN-US','ALL','e4a42118-ea21-4256-a68d-e7b66b437fa0',
TO_DATE ('01-Jan-2016 00:00:00', 'DD-MON-YYYY HH24:MI:SS'),TO_DATE ('30-Nov-2016 00:00:00', 'DD-MON-YYYY HH24:MI:SS'),
0,0,'ALL','ALL','ALL','2016'))
                                                                                                                                                                           
Individual Report
select * from table(SF_PRODUNCTIONINDIVNEXTCARE('EN-US','YYNNNN','ALL',to_date('01-Jan-2016 00:00:00','DD-MON-YYYY HH24:MI:SS'),
to_date('01-Nov-2016 00:00:00','DD-MON-YYYY HH24:MI:SS'),0,0,'ALL','ALL','ALL','e4a42118-ea21-4256-a68d-e7b66b437fa0','2016'))