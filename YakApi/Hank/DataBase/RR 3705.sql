-- Function: procprocessschedulerdata(text)

-- DROP FUNCTION procprocessschedulerdata(text);

CREATE OR REPLACE FUNCTION procprocessschedulerdata(groupname text DEFAULT NULL::text)
  RETURNS void AS
$BODY$

Declare scheduleId bigint;
Declare startDateTime timestamp;
Declare expirationDateTime timestamp;
Declare frequencyType integer;
Declare lastMainExecuted timestamp;
Declare lastExecuted timestamp;

Declare recurEvery bigint;
Declare repeatTaskFrequency integer;
Declare repeatTaskDuration integer;
Declare forceExecute boolean;


Declare doProcess boolean;
Declare dontUpdateLastTimeProcessedAt boolean;
Declare tmpData timestamp;
Declare tmpId integer;
Declare section integer;
BEGIN

                If(groupName is null)
                then
                                groupName := to_char(current_timestamp, 'dd-mm-yyyy-HH24-MI-ss');
                End If;

                DROP TABLE IF EXISTS temp1;

                CREATE TABLE temp1 (Id BigInt, StartDateTime timestamp, DoProcess boolean, DontUpdateLastTimeProcessedAt boolean, tmpId integer,section integer);

                LOOP
                                dontUpdateLastTimeProcessedAt := null;
                                doProcess := null;

                                Select into forceExecute, scheduleId, startDateTime, expirationDateTime, frequencyType, recurEvery, repeatTaskFrequency, repeatTaskDuration, lastMainExecuted
                                                                "ForceExecute", "Id", "StartDateTime", "ExpirationDateTime", "FrequencyType", "RecurEvery", "RepeatTaskFrequency", "RepeatTaskDuration", "LastTimeProcessedAt"
                                from "TblScheduler" where "IsDeleted" != 't' and "IsDisabled" != 't' and "Id" not in (select Id from temp1)
                                order by "StartDateTime"
                                limit 1    ;

                                if(scheduleId is null) Then
                                                Exit;
                                End If;

                                frequencyType :=  case when frequencyType = '5' then '1' else frequencyType end;
                                recurEvery := case when recurEvery is null or recurEvery = 0 then 1 else recurEvery end;
                                repeatTaskFrequency := case when repeatTaskFrequency is null or repeatTaskFrequency = 0 then 1 else repeatTaskFrequency end;
                                repeatTaskDuration := case when repeatTaskDuration is null or repeatTaskDuration = 0 then 1 else repeatTaskDuration end;


                                lastMainExecuted := COALESCE(lastMainExecuted, current_timestamp);

                                Select into lastExecuted "CreatedOn" from "TblSchedulerHistory" Where "SchedulerId" = scheduleId order by "CreatedOn" desc limit 1;

                                if(frequencyType = '1'
                                                and (expirationDateTime is null or expirationDateTime > current_timestamp)
                                                and startDateTime <= current_timestamp
                                                And (lastExecuted is null or startDateTime > lastExecuted))
                                Then
                                                doProcess := 't';
                                                section:=1;
                                End If;
                                if(frequencyType = '2'
                                                and (expirationDateTime is null or expirationDateTime > current_timestamp)
                                                and startDateTime <= current_timestamp
                                                And (lastExecuted is null or lastMainExecuted + (recurEvery || ' day')::interval <= current_timestamp))
                                Then
                                                doProcess := 't';
                                                section=2;
                                End If;
                                if(frequencyType = '3' or frequencyType = '4')
                                Then
                                                if((expirationDateTime is null or expirationDateTime > current_timestamp)
                                                and startDateTime <= current_timestamp
                                                And (lastExecuted is null or lastMainExecuted + (case when frequencyType = '3' then '7' else '30' end || ' day')::interval <= current_timestamp))
                                Then
                                                doProcess := 't';
                                                section=3;
                                End If;

                                End If;


                                if(repeatTaskFrequency >= '1' and repeatTaskDuration >= '1' ) Then
                                                tmpData := (lastExecuted + (repeatTaskFrequency || ' minute')::interval);

                                                if(lastMainExecuted <= current_timestamp
                                                                and lastExecuted <= current_timestamp
                                                                and tmpData <= current_timestamp
                                                                and tmpData <= (lastMainExecuted + (repeatTaskDuration || ' minute')::interval))
                                                Then
                                                                dontUpdateLastTimeProcessedAt := COALESCE(doProcess, 'f') != 't';
                                                                doProcess := 't';
                                                                section=4;
                                                End if;
                                End if;

                                if(forceExecute = 't' )
                                Then
                                                doProcess := 't';
                                                section=5;
                                End If;

                                insert into temp1 (Id, StartDateTime, DoProcess, DontUpdateLastTimeProcessedAt, tmpId,section) values(scheduleId, startDateTime, doProcess, dontUpdateLastTimeProcessedAt, tmpId,section);
                End loop;

                delete from temp1 where temp1.DoProcess is null;

-- Fill the Test Queue
               insert into "TblTestQueue"("SchedulerId", "SuiteId", "TestId", "GroupName", "Status", "CreatedOn", "ModifiedOn", "IsDeleted","CreatedBy","ModifiedBy")
               Select t2."SchedulerId", t1."SuiteId", t1."TestId", groupName || '-' || t2."SchedulerId", '0', Now(), Now(), 'f',2,2 from "TblLnkSuiteTest" t1
                join "TblLnkSchedulerSuite" t2 on t1."SuiteId" = t2."SuiteId" and t2."IsDeleted" != 't'
                join "TblTest" t4 on t4."Id" = t1."TestId" and t4."IsDeleted" != 't'
                join temp1 t3 on t2."SchedulerId" = t3.Id
                Where t1."IsDeleted" != 't'
                order by t3.StartDateTime;

  -- Update scheduler last executed date time, real one always be from "TblSchedulerHistory"
                update "TblScheduler" set "LastExecuted" = current_timestamp, "ModifiedOn" = current_timestamp
                Where "Id" in (select Id from temp1);

-- Update scheduler LastTimeProcessedAt, will only get updated for real execution not for recurring
                update "TblScheduler" set "LastTimeProcessedAt" = cast(current_timestamp::date || ' ' || "StartDateTime"::time as timestamp), "ModifiedOn" = current_timestamp,"ForceExecute"='f'
                Where "Id" in (select Id from temp1 where COALESCE(temp1.DontUpdateLastTimeProcessedAt, 'f') != 't');

--select current_timestamp

                insert into "TblSchedulerHistory" ("SchedulerId", "GroupName", "Status", "CreatedOn", "ModifiedOn", "IsDeleted","CreatedBy","ModifiedBy", "SettingsJson")
                select t1.Id, groupName || '-' || t1.Id, '0', now(), now(), 'f', 2, 2, t2."SettingsJson" from temp1 t1
                inner join "TblScheduler" t2 on t2."Id" = t1.Id
                order by t1.StartDateTime;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION procprocessschedulerdata(text)
  OWNER TO postgres;
