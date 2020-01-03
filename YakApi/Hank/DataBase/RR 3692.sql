--3692 	Halt if no data found

-- Function: procgetreportlinkdata(timestamp without time zone, bigint)

-- DROP FUNCTION procgetreportlinkdata(timestamp without time zone, bigint);

CREATE OR REPLACE FUNCTION procgetreportlinkdata(
    IN daytillpastdate timestamp without time zone,
    IN testid bigint)
  RETURNS TABLE("Value" json, "ReportDataId" bigint, "TestId" bigint) AS
$BODY$


BEGIN

Return Query

SELECT
cast("RD"."Value"::json->>'VariableStateContainer' as json),
cast ("RD"."Id" as bigint),
cast ("TQ"."TestId" as bigint)
FROM "TblReportData" as "RD"
Inner Join "TblTestQueue" as "TQ" on "RD"."TestQueueId"="TQ"."Id"
Left Join "TblReportExecutionLinkData" as "REL" on "REL"."TestId"="TQ"."TestId" AND "REL"."ReportDataId"="RD"."Id"

Where

"TQ"."TestId"=testid AND
"RD"."Status"=8 AND
"REL"."Id" IS NULL AND
"RD"."CreatedOn"::date >= daytillpastdate::date
order By  "RD"."Id" desc limit 1 ;



END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION procgetreportlinkdata(timestamp without time zone, bigint)
  OWNER TO postgres;
