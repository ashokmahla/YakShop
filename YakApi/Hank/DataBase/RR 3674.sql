
-- RR #3674

-- Table: "TblTransformationCategory"

-- DROP TABLE "TblTransformationCategory";

CREATE TABLE "TblTransformationCategory"
(
  "Id" bigserial NOT NULL,
  "IsDeleted" boolean,
  "CreatedOn" timestamp without time zone,
  "CreatedBy" bigint,
  "ModifiedBy" bigint,
  "ModifiedOn" timestamp without time zone,
  "Name" character varying,
  "WebsiteId" bigint,
  CONSTRAINT "TblTransformationCategory_pkey" PRIMARY KEY ("Id"),
  CONSTRAINT "TransformationCategory_Website_Fkey" FOREIGN KEY ("WebsiteId")
      REFERENCES "TblWebsite" ("Id") MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "TblTransformationCategory"
  OWNER TO postgres;

  -- RR #3674

-- Index: "fki_TransformationCategory_Website_Fkey"

-- DROP INDEX "fki_TransformationCategory_Website_Fkey";

CREATE INDEX "fki_TransformationCategory_Website_Fkey"
  ON "TblTransformationCategory"
  USING btree
  ("WebsiteId");




  -- Table: "TblTransformation"

  -- DROP TABLE "TblTransformation";

  CREATE TABLE "TblTransformation"
  (
    "Id" bigint NOT NULL DEFAULT nextval('"TblTransformationDefination_Id_seq"'::regclass),
    "IsDeleted" boolean,
    "CreatedOn" timestamp without time zone,
    "CreatedBy" bigint,
    "ModifiedBy" bigint,
    "ModifiedOn" timestamp without time zone,
    "TransformationCategoryId" bigint,
    "Value" character varying,
    "TransformedValue" character varying,
    CONSTRAINT "TblTransformationDefination_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "TblTransformationDefination_TblTransformationCategory" FOREIGN KEY ("TransformationCategoryId")
        REFERENCES "TblTransformationCategory" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE NO ACTION
  )
  WITH (
    OIDS=FALSE
  );
  ALTER TABLE "TblTransformation"
    OWNER TO postgres;

  -- Index: "fki_TblTransformationDefination_TblTransformationCategory"

  -- DROP INDEX "fki_TblTransformationDefination_TblTransformationCategory";

  CREATE INDEX "fki_TblTransformationDefination_TblTransformationCategory"
    ON "TblTransformation"
    USING btree
    ("TransformationCategoryId");

