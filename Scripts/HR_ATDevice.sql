CREATE TABLE HR_TA_DEVICES_SETUP
(
  DEVICE_ID           NUMBER(19)                NOT NULL,
  DEVICE_NAME         NVARCHAR2(70)             NOT NULL,
  COMMUNICATION_TYPE  NUMBER(3)                 NOT NULL,
  COMMUNICATION_KEY   NVARCHAR2(20),
  PORT                NUMBER(10)                NOT NULL,
  DEVICE_NUMBER       NUMBER(4)                 NOT NULL,
  BAUD_RATE           NUMBER(3),
  DEVICE_IP           NVARCHAR2(19),
  DEVICE_SERIALNO     NUMBER(13)                NOT NULL,
  LOCATION_ID         NUMBER(19)                DEFAULT 1,
  SHORT_NAME          NVARCHAR2(100),
  ACTIVE              NUMBER(1)                 DEFAULT 0                     NOT NULL,
  CREATION_DATE       DATE                      DEFAULT sysdate,
  LAST_UPDATED_DATE   DATE                      DEFAULT sysdate,
  LAST_UPDATED_BY     NUMBER(19),
  CREATED_BY          NUMBER(19),
  DEVICE_TYPE         NUMBER(3),
  DEPARTMENT_CODE     NUMBER(19),
  VERSION_CODE        NUMBER(3),
  FB_VERSION_CODE     NUMBER(3),
  ORGANIZATIONS_CODE  NUMBER(19),
  ORG_ID              NUMBER
);



