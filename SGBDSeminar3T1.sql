-- Dirty Reads T1

BEGIN TRAN;
UPDATE clienti Set nume='client modificat' where cod_c=2;
Waitfor delay '00:00:06';
Rollback tran;
Select * from clienti;

-- cum evitam dirty read? ->  Set transaction isolation level read commited;

-- Unrepeatable Reads T1
Set transaction isolation level read committed;
-- solutie: Set transaction isolation level repeatable read;
Begin TRAN;
Select * from Clienti;
Waitfor delay '00:00:06';
Select * from Clienti;
commit tran;

-- Phantom reads T1
set transaction isolation level repeatable read;
begin tran;
select * from conturi;
Waitfor delay '00:00:06';
select * from Conturi;
commit tran;
-- solutie: set transaction isolation level serializable

-- deadlock T1
set transaction isolation level serializable
begin tran;
Update clienti set nume='Clienti T1' where cod_c=1;
Waitfor delay '00:00:06';
Update Conturi set detalii='Cont T1' where cod_cont=1;
commit tran;


