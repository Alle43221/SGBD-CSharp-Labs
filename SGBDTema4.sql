-- TEMA 4

-- DIRTY READS - citirea de date necomise

-- T1:
BEGIN TRAN
UPDATE SERVICII SET pret = 5 where id=3;
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Actualizare servicii',
    SYSTEM_USER,
    CONCAT('Id: 3', ', Pret: 5')
);
WAITFOR DELAY '00:00:07'
ROLLBACK TRAN

-- T2:
-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED --<------- solutia (poate lipsi, acesta este comportamentul normal)
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
BEGIN TRAN;
DECLARE @result INT;
SELECT @result = pret FROM SERVICII WHERE id = 3;
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Selectare servicii',
    SYSTEM_USER,
    CONCAT('Id: 3', ', Pret: ', @result)
);
COMMIT TRAN;

-- NON-REPEATABLE READS - o inregistrare citita se schimba in cadrul unei tranzactii

-- T1:
-- SET TRANSACTION ISOLATION LEVEL REPEATABLE READ   --<------- solutia
BEGIN TRAN
SELECT * from SERVICII
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Selectare servicii',
    SYSTEM_USER,
    'all entries'
);
WAITFOR DELAY '00:00:07'
SELECT * from SERVICII
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Selectare servicii',
    SYSTEM_USER,
    'all entries'
);
COMMIT TRAN;

-- T2:
BEGIN TRAN
UPDATE SERVICII SET pret = 8 where id=3;
WAITFOR DELAY '00:00:03'
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Actualizare servicii',
    SYSTEM_USER,
    CONCAT('Id: 3', ', Pret: 8')
);
COMMIT TRAN

-- PHANTOM READS - sunt adaugate noi inregistrari si apar in cadrul unei tranzactii

-- T1:
-- SET TRANSACTION ISOLATION LEVEL SERIALIZABLE -- <------- solutia
BEGIN TRAN
SELECT * FROM MAGAZINE 
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Selectare magazine',
    SYSTEM_USER,
    'all entries'
);
WAITFOR DELAY '00:00:07'
SELECT * FROM MAGAZINE
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Selectare magazine',
    SYSTEM_USER,
    'all entries'
);
COMMIT TRAN

-- T2:
BEGIN TRAN
WAITFOR DELAY '00:00:03'
INSERT INTO MAGAZINE(adresa, telefon, data_deschidere) VALUES
('Str. Splaiul Independentei Bucuresti, nr 85', '0774678502', GETDATE())
DECLARE @magazin_id INT = SCOPE_IDENTITY(); 
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'Inserare magazin',
    SYSTEM_USER,
    Concat('Id: ',  @magazin_id)
);
COMMIT TRAN


-- DEADLOCK

-- T1:
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRAN;
UPDATE Magazine SET telefon='0774888888' WHERE id=1;
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'modificare magazin',
    SYSTEM_USER,
    Concat('Id: ', 1, ', telefon: 0774888888')
);
WAITFOR DELAY '00:00:05';
UPDATE SERVICII SET pret=4 WHERE id=3;
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'modificare serviciu',
    SYSTEM_USER,
    Concat('Id: ', 1, ', pret: 4')
);
COMMIT TRAN;

-- T2:

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRAN;
UPDATE SERVICII SET pret=7 WHERE id=3;
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'modificare serviciu',
    SYSTEM_USER,
    Concat('Id: ', 1, ', pret: 7')
);
WAITFOR DELAY '00:00:05';
UPDATE Magazine SET telefon='0774999999' WHERE id=1;
INSERT INTO ActiuniLog (actiune, utilizator, detalii)
VALUES (
    'modificare magazin',
    SYSTEM_USER,
    Concat('Id: ', 1, ', telefon: 0774999999')
);

COMMIT TRAN;

-- solutie -> setarea de DEADLOCK_PRIORITY daca una dintre tranzactii este mai importanta si am dori sa se execute 
--         -> executarea modificarilor pe tabele in aceeasi ordine (blocarea resurselor in aceeasi ordine)

select * from magazine
select * from servicii
select * from ActiuniLog order by id desc;