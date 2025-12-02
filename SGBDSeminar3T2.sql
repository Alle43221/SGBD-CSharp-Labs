-- Dirty Reads T2
SET Transaction isolation level Read Uncommitted;
Begin TRAN;
Select * from clienti;
commit tran;

-- Unrepeatable Reads T
Begin TRAN;
Update Clienti set nume='client modificat' where cod_c=1;
commit tran;

-- Phantom reads T2
begin tran;
insert into  Conturi (detalii, suma, moneda, data_creare, cod_c) values
('gfds', 500, 'EUR', '2000-12-3', 1);
commit tran;

-- deadlock T2
set transaction isolation level serializable
begin tran;
Update Conturi set detalii='Cont T1' where cod_cont=1;
Waitfor delay '00:00:06';
Update clienti set nume='Clienti T1' where cod_c=1;
commit tran;