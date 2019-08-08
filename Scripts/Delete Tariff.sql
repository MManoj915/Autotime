DELETE FROM IM_PROVIDER_TARIFF_DETAILS
WHERE TARIFFCODE IN(SELECT TARIFFCODE FROM IM_PROVIDER_TARIFF WHERE PROVIDERCODE IN
(SELECT PROVIDERCODE FROM IM_PROVIDERS WHERE PROVIDERID = 'M408')
AND STANDARDCODE IN(SELECT DETAILCODE FROM IM_VERSION_DETALIS WHERE VERSIONCODE IN
(SELECT VERSIONCODE FROM IM_ACTIVITY_DETAILS WHERE TYPECODE IN
(SELECT TYPECODE FROM IM_ACTIVITY_TYPES WHERE VALUE IN (-2,3,8)))))
AND EFFECTIVEFROM > TO_DATE('20/05/2014','DD/MM/YYYY')




DELETE FROM IM_PROVIDER_TARIFF_DETAILS
WHERE TARIFFCODE IN(SELECT TARIFFCODE FROM IM_PROVIDER_TARIFF WHERE PROVIDERCODE IN
(SELECT PROVIDERCODE FROM IM_PROVIDERS WHERE PROVIDERID = 'M207'))
AND To_Date(EFFECTIVEFROM,'DD/MM/RRRR') = TO_DATE('01/03/2018','DD/MM/YYYY')