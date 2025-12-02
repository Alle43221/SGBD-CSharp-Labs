/*
Problema 3. 1 săptămână
Implementaţi următoarele scenarii pentru baza de date proprie:
- Creaţi o procedură stocată ce inserează date pentru entităţi ce se află într-o relaţie m-n. 
Dacă o operaţie de inserare eşuează, trebuie făcut roll-back pe întreaga procedură 
stocată. (5 puncte)
- Creaţi o procedură stocată ce inserează date pentru entităţi ce se află într-o relaţie m-n. 
Dacă o operaţie de inserare eşuează va trebui să se păstreze cât mai mult posibil din ceea 
ce s-a modificat până în acel moment. De exemplu, dacă se încearcă inserarea unei cărţi 
şi a autorilor acesteia, iar autorii au fost inseraţi cu succes însă apare o problemă la 
inserarea cărţii, atunci să se facă roll-back la inserarea de carte însă autorii acesteia să 
rămână în baza de date. (4 puncte)
Oficiu: 1 punct
Observaţie: Ca notă generală, nu se va transmite niciun ID ca parametru de intrare a unei 
proceduri stocate şi toţi parametrii trebuie să fie validaţi (utilizaţi funcţii acolo unde este nevoie). 
De asemenea, pentru toate scenariile trebuie să stabiliţi un sistem de logare ce vă va permite să 
memoraţi istoricul acţiunilor executate. Pentru detectarea erorilor se recomandă folosirea clauzei 
try-catch.
Pentru prezentarea laboratorului pregătiţi teste ce acoperă scenarii de succes şi cu erori. Pregătiţi 
explicaţii detaliate ale scenariilor şi implementării.


*/

select *
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME='comenzi'

select *
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME='produse_personalizate'

select *
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME='produse'

CREATE TABLE ActiuniLog (
    id INT IDENTITY PRIMARY KEY,
    actiune NVARCHAR(500),
    utilizator NVARCHAR(100),
    data_actiune DATETIME DEFAULT GETDATE(),
    detalii NVARCHAR(1000)
);

GO --DONE 
ALTER FUNCTION validare_produs
(
@denumire nvarchar(200), --not null
@descriere nvarchar (1000), --not null
@pret_de_baza float, --not null
@gramaj float, --not null
@inaltime float, --not null
@latime float --not null
)
RETURNS VARCHAR(1000) AS
BEGIN
    DECLARE @error VARCHAR(1000) = ''

    IF @denumire IS NULL OR LEN(@denumire) = 0 OR LEN(@denumire) > 200
    BEGIN
        SET @error = @error + '@denumire must be not null, non-empty, and <= 200 characters.' + CHAR(13) + CHAR(10)
    END

    IF @descriere IS NULL OR LEN(@descriere) = 0 OR LEN(@descriere) > 1000
    BEGIN
        SET @error = @error + '@descriere must be not null, non-empty, and <= 1000 characters.' + CHAR(13) + CHAR(10)
    END

    IF @pret_de_baza IS NULL OR @pret_de_baza < 0
    BEGIN
        SET @error = @error + '@pret_de_baza must be not null and >= 0.' + CHAR(13) + CHAR(10)
    END

    IF @gramaj IS NULL OR @gramaj <= 0
    BEGIN
        SET @error = @error + '@gramaj must be not null and > 0.' + CHAR(13) + CHAR(10)
    END

    IF @inaltime IS NULL OR @inaltime <= 0
    BEGIN
        SET @error = @error + '@inaltime must be not null and > 0.' + CHAR(13) + CHAR(10)
    END

    IF @latime IS NULL OR @latime <= 0
    BEGIN
        SET @error = @error + '@latime must be not null and > 0.' + CHAR(13) + CHAR(10)
    END

    RETURN @error
END
GO

GO -- DONE
ALTER FUNCTION validare_comanda
(
@total float, --not null
@client int, --not null, fk
@adresa_facturare nvarchar(200),
@adresa_livrare nvarchar(200), --not null
@data_inregistrare DATE, --not null
@livrare int, --not null, fk
@estimare_inceput DATE, --not null
@estimare_sfarsit DATE, --not null
@status_c nvarchar(200),
@magazin int --not null, fk
)
RETURNS VARCHAR(1000) AS
BEGIN
    DECLARE @error VARCHAR(1000) = ''

    IF @total IS NULL OR @total < 0
    BEGIN
        SET @error = @error + '@total must be not null and >= 0.' + CHAR(13) + CHAR(10)
    END

    IF @client IS NULL OR @client < 0
    BEGIN
        SET @error = @error + '@client must be not null and >= 0.' + CHAR(13) + CHAR(10)
    END

    IF NOT EXISTS (SELECT 1 FROM CLIENTI WHERE id = @client)
    BEGIN
        SET @error = @error + '@client must exist in CLIENTI table.' + CHAR(13) + CHAR(10)
    END

    IF @adresa_livrare IS NULL OR LEN(@adresa_livrare) = 0 OR LEN(@adresa_livrare)>200
    BEGIN
        SET @error = @error + '@adresa_livrare must be not null and non-empty, and <= 200 characters' + CHAR(13) + CHAR(10)
    END

	IF @adresa_facturare IS NOT NULL AND( LEN(@adresa_facturare) = 0 OR LEN(@adresa_facturare)>200)
    BEGIN
        SET @error = @error + '@adresa_facturare must have between 0 and 200 characters or be null' + CHAR(13) + CHAR(10)
    END

    IF @livrare < 0 AND @livrare IS NOT NULL
    BEGIN
        SET @error = @error + '@livrare must be >= 0.' + CHAR(13) + CHAR(10)
    END

    IF @livrare > 0 AND NOT EXISTS (SELECT 1 FROM SERVICII_LIVRARE WHERE id = @livrare)
    BEGIN
        SET @error = @error + '@livrare must exist in SERVICII_LIVRARE table.' + CHAR(13) + CHAR(10)
    END

    IF @estimare_inceput IS NULL 
    BEGIN
        SET @error = @error + 'Date @estimare_inceput must be not null.' + CHAR(13) + CHAR(10)
    END

	 IF  @estimare_sfarsit IS NULL 
    BEGIN
        SET @error = @error + 'Date estimare_sfarsit must be not null.' + CHAR(13) + CHAR(10)
    END

	 IF  @data_inregistrare IS NULL
    BEGIN
        SET @error = @error + 'Date  @data_inregistrare must be not null.' + CHAR(13) + CHAR(10)
    END

    IF @estimare_inceput > @estimare_sfarsit
    BEGIN
        SET @error = @error + '@estimare_inceput must be <= @estimare_sfarsit.' + CHAR(13) + CHAR(10)
    END

    IF @data_inregistrare > @estimare_inceput
    BEGIN
        SET @error = @error + '@data_inregistrare must be <= @estimare_inceput.' + CHAR(13) + CHAR(10)
    END

    IF @data_inregistrare > @estimare_sfarsit
    BEGIN
        SET @error = @error + '@data_inregistrare must be <= @estimare_sfarsit.' + CHAR(13) + CHAR(10)
    END

    IF @magazin < 0 AND @magazin IS NOT NULL
    BEGIN
        SET @error = @error + '@magazin must be >= 0.' + CHAR(13) + CHAR(10)
    END

    IF @magazin >= 0  AND NOT EXISTS (SELECT 1 FROM MAGAZINE WHERE id = @magazin)
    BEGIN
        SET @error = @error + '@magazin must exist in MAGAZINE table.' + CHAR(13) + CHAR(10)
    END

	IF @status_c IS NOT NULL AND (LEN(@status_c)=0 OR LEN(@status_c)>200)
	 BEGIN
        SET @error = @error + '@status_c must have between 0 and 200 characters or be null.' + CHAR(13) + CHAR(10)
    END

    RETURN @error
END
GO

GO
ALTER PROCEDURE insereazaProdusComanda
(
    @denumire nvarchar(200),
    @descriere nvarchar(1000),
    @pret_de_baza float,
    @gramaj float,
    @inaltime float,
    @latime float,

    @total float,
    @client_id int,
    @adresa_facturare nvarchar(200),
    @adresa_livrare nvarchar(200),
    @data_inregistrare date,
    @livrare_id int,
    @estimare_inceput date,
    @estimare_sfarsit date,
    @status_c nvarchar(200),
    @magazin_id int,

    @cantitate int
)
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @changed_tables VARCHAR(1000) = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Start procedura insereazaProdusComanda', SYSTEM_USER, 'Pornire procedura');

        -- Validare date produs
        DECLARE @err_produs VARCHAR(1000) = dbo.validare_produs(@denumire, @descriere, @pret_de_baza, @gramaj, @inaltime, @latime);

        IF LEN(@err_produs) > 0
        BEGIN
            RAISERROR(@err_produs, 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Inserare produs
        INSERT INTO Produse (denumire, descriere, pret_de_baza, gramaj, inaltime, latime)
        VALUES (@denumire, @descriere, @pret_de_baza, @gramaj, @inaltime, @latime);

        DECLARE @produs_id INT = SCOPE_IDENTITY(); -- Obținem ID-ul produsului inserat
		SET @changed_tables += 'Produse, '

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Inserare produs', SYSTEM_USER, CONCAT('Produs: ', @denumire, ', ID: ', @produs_id));

        -- Validare date comanda
        DECLARE @err_comanda VARCHAR(1000) = dbo.validare_comanda(@total, @client_id, @adresa_facturare, @adresa_livrare, @data_inregistrare, @livrare_id, @estimare_inceput, @estimare_sfarsit, @status_c, @magazin_id);

        IF LEN(@err_comanda) > 0
        BEGIN
            RAISERROR(@err_comanda, 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Inserare comanda
        INSERT INTO Comenzi (total, client, adresa_facturare, adresa_livrare, data_inregistrare, livrare, estimare_inceput, estimare_sfarsit, status_c, magazin)
        VALUES (@total, @client_id, @adresa_facturare, @adresa_livrare, @data_inregistrare, @livrare_id, @estimare_inceput, @estimare_sfarsit, @status_c, @magazin_id);

        DECLARE @comanda_id INT = SCOPE_IDENTITY(); -- Obținem ID-ul comenzii inserate
		SET @changed_tables += 'Comenzi, '

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Inserare comanda', SYSTEM_USER, CONCAT('Comanda ID: ', @comanda_id, ', Total: ', @total));

        -- Validare legătură PRODUSE_PERSONALIZATE
        DECLARE @err_produs_personalizat VARCHAR(1000) = dbo.validare_produs_personalizat(@comanda_id, @produs_id, @cantitate, @pret_de_baza);

        IF LEN(@err_produs_personalizat) > 0
        BEGIN
            RAISERROR(@err_produs_personalizat, 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Inserare legătură în tabela m-n
        INSERT INTO PRODUSE_PERSONALIZATE (comanda, produs_de_baza, cantitate, pret_pe_bucata)
        VALUES (@comanda_id, @produs_id, @cantitate, @pret_de_baza);

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Inserare in PRODUSE_PERSONALIZATE', SYSTEM_USER, CONCAT('ComandaID: ', @comanda_id, ', ProdusID: ', @produs_id, ', Cantitate: ', @cantitate));
		SET @changed_tables += 'Produse_personalizate, '

        COMMIT TRANSACTION;

        -- Log: finalizare cu succes
        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Finalizare cu succes', SYSTEM_USER, 'Procedura terminata cu succes');
    END TRY
    BEGIN CATCH
        -- Rollback în caz de eroare
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();

        -- Logare eroare
        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Eroare in procedura', SYSTEM_USER, @ErrorMessage);

		IF LEN(@changed_tables) > 0
		BEGIN
			INSERT INTO ActiuniLog (actiune, utilizator, detalii)
			VALUES ('Rolling back changes', SYSTEM_USER, 'Undo the changes made on tables:' + @changed_tables);
		END

        -- Răspândirea erorii
        RAISERROR (@ErrorMessage, 16, 1);
    END CATCH
END
GO

GO
ALTER PROCEDURE insereazaProdusComandaNoRollback
(
    @denumire nvarchar(200),
    @descriere nvarchar(1000),
    @pret_de_baza float,
    @gramaj float,
    @inaltime float,
    @latime float,

    @total float,
    @client_id int,
    @adresa_facturare nvarchar(200),
    @adresa_livrare nvarchar(200),
    @data_inregistrare date,
    @livrare_id int,
    @estimare_inceput date,
    @estimare_sfarsit date,
    @status_c nvarchar(200),
    @magazin_id int,

    @cantitate int
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Start procedura insereazaProdusComanda', SYSTEM_USER, 'Pornire procedura');

        -- Savepoint pentru început
		SAVE TRANSACTION StartTransaction;

        -- Validare date produs
        DECLARE @err_produs VARCHAR(1000) = dbo.validare_produs(@denumire, @descriere, @pret_de_baza, @gramaj, @inaltime, @latime);

        IF LEN(@err_produs) > 0
        BEGIN
            -- Logare eroare produs
            INSERT INTO ActiuniLog (actiune, utilizator, detalii)
            VALUES ('Eroare validare produs', SYSTEM_USER, @err_produs);

			-- Validare date comanda
			DECLARE @err_comanda1 VARCHAR(1000) = dbo.validare_comanda(@total, @client_id, @adresa_facturare, @adresa_livrare, @data_inregistrare, @livrare_id, @estimare_inceput, @estimare_sfarsit, @status_c, @magazin_id);

			IF LEN(@err_comanda1) > 0
			BEGIN
				-- Logare eroare comanda
				INSERT INTO ActiuniLog (actiune, utilizator, detalii)
				VALUES ('Eroare validare comanda', SYSTEM_USER, @err_comanda1);

				-- ROLLBACK doar la comanda
				ROLLBACK TRANSACTION StartTransaction; 

				SET @err_produs +=CHAR(13) + CHAR(10);
				SET @err_produs += @err_produs;
				RAISERROR (@err_comanda1, 16, 1);
				RETURN;
			END

			-- Inserare comanda
			INSERT INTO Comenzi (total, client, adresa_facturare, adresa_livrare, data_inregistrare, livrare, estimare_inceput, estimare_sfarsit, status_c, magazin)
			VALUES (@total, @client_id, @adresa_facturare, @adresa_livrare, @data_inregistrare, @livrare_id, @estimare_inceput, @estimare_sfarsit, @status_c, @magazin_id);

			DECLARE @comanda_id1 INT = SCOPE_IDENTITY(); -- Obținem ID-ul comenzii inserate

			INSERT INTO ActiuniLog (actiune, utilizator, detalii)
			VALUES ('Inserare comanda', SYSTEM_USER, CONCAT('Comanda ID: ', @comanda_id1, ', Total: ', @total));

			RAISERROR (@err_produs, 16, 1);
            RETURN;
        END

        -- Inserare produs
        INSERT INTO Produse (denumire, descriere, pret_de_baza, gramaj, inaltime, latime)
        VALUES (@denumire, @descriere, @pret_de_baza, @gramaj, @inaltime, @latime);

        DECLARE @produs_id INT = SCOPE_IDENTITY(); -- Obținem ID-ul produsului inserat

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Inserare produs', SYSTEM_USER, CONCAT('Produs: ', @denumire, ', ID: ', @produs_id));

		SAVE TRANSACTION ProdusInserat;

        -- Validare date comanda
        DECLARE @err_comanda VARCHAR(1000) = dbo.validare_comanda(@total, @client_id, @adresa_facturare, @adresa_livrare, @data_inregistrare, @livrare_id, @estimare_inceput, @estimare_sfarsit, @status_c, @magazin_id);

        IF LEN(@err_comanda) > 0
        BEGIN
            -- Logare eroare comanda
            INSERT INTO ActiuniLog (actiune, utilizator, detalii)
            VALUES ('Eroare validare comanda', SYSTEM_USER, @err_comanda);

            -- ROLLBACK doar la comanda
            ROLLBACK TRANSACTION ProdusInserat; 

			RAISERROR (@err_comanda, 16, 1);
            RETURN;
        END

        -- Inserare comanda
        INSERT INTO Comenzi (total, client, adresa_facturare, adresa_livrare, data_inregistrare, livrare, estimare_inceput, estimare_sfarsit, status_c, magazin)
        VALUES (@total, @client_id, @adresa_facturare, @adresa_livrare, @data_inregistrare, @livrare_id, @estimare_inceput, @estimare_sfarsit, @status_c, @magazin_id);

        DECLARE @comanda_id INT = SCOPE_IDENTITY(); -- Obținem ID-ul comenzii inserate

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Inserare comanda', SYSTEM_USER, CONCAT('Comanda ID: ', @comanda_id, ', Total: ', @total));

		SAVE TRANSACTION ComandaInserata;

        -- Validare legătură PRODUSE_PERSONALIZATE
        DECLARE @err_produs_personalizat VARCHAR(1000) = dbo.validare_produs_personalizat(@comanda_id, @produs_id, @cantitate, @pret_de_baza);

        IF LEN(@err_produs_personalizat) > 0
        BEGIN
            -- Logare eroare produs personalizat
            INSERT INTO ActiuniLog (actiune, utilizator, detalii)
            VALUES ('Eroare validare produs personalizat', SYSTEM_USER, @err_produs_personalizat);

            -- ROLLBACK doar la produsul personalizat
            ROLLBACK TRANSACTION ComandaInserata; 

			RAISERROR (@err_produs_personalizat, 16, 1);
            RETURN;
        END

        -- Inserare legătură în tabela m-n
        INSERT INTO PRODUSE_PERSONALIZATE (comanda, produs_de_baza, cantitate, pret_pe_bucata)
        VALUES (@comanda_id, @produs_id, @cantitate, @pret_de_baza);

        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Inserare in PRODUSE_PERSONALIZATE', SYSTEM_USER, CONCAT('ComandaID: ', @comanda_id, ', ProdusID: ', @produs_id, ', Cantitate: ', @cantitate));

		COMMIT TRANSACTION;

        -- Log: finalizare cu succes
        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Finalizare cu succes', SYSTEM_USER, 'Procedura terminata cu succes');
    END TRY
    BEGIN CATCH

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();

        -- Logare eroare
        INSERT INTO ActiuniLog (actiune, utilizator, detalii)
        VALUES ('Eroare in procedura', SYSTEM_USER, @ErrorMessage);

        -- Răspândirea erorii
        RAISERROR (@ErrorMessage, 16, 1);

		COMMIT TRAN
    END CATCH
END
GO


--- INSERARE CU SUCCES

EXEC insereazaProdusComanda
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 37692,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 1,
	@cantitate = 1;

--- client inexistent

EXEC insereazaProdusComanda
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 1,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 1,
	@cantitate = 1;

--- serviciu de livrare inexistent

EXEC insereazaProdusComanda
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 37692,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 1,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 1,
	@cantitate = 1;

--- magazin inexistent

EXEC insereazaProdusComanda
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 37692,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 100,
	@cantitate = 1;

--- date gresite de intrare pentru campuri, dar id-uri existente

EXEC insereazaProdusComanda
    @denumire = N'',
    @descriere = N'',
    @pret_de_baza = -25.5,
    @gramaj = -300,
    @inaltime = -2.5,
    @latime = -30,
    @total = -25.5,
    @client_id = 37692,
    @adresa_facturare = N'',
    @adresa_livrare = N'',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2024-03-13',
    @estimare_sfarsit = '2024-03-01',
    @status_c = N'',
    @magazin_id = 1,
	@cantitate = -1;

select * from comenzi order by id desc;
select * from PRODUSE order by id desc;

select count(id) from produse;

select * from ActiuniLog order by id desc;

delete from ActiuniLog

Select * from clienti order by id desc;

--- INSERARE CU SUCCES

EXEC insereazaProdusComandaNoRollback
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 37692,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 1,
	@cantitate = 1;

--- client inexistent

EXEC insereazaProdusComandaNoRollback
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 1,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 1,
	@cantitate = 1;

--- serviciu de livrare inexistent

EXEC insereazaProdusComandaNoRollback
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 37692,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 1,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 1,
	@cantitate = 1;

--- magazin inexistent

EXEC insereazaProdusComandaNoRollback
    @denumire = N'Farfurie pizza',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = 25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 37692,
    @adresa_facturare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'În procesare',
    @magazin_id = 100,
	@cantitate = 1;

--- date gresite de intrare pentru campuri, dar id-uri existente

EXEC insereazaProdusComandaNoRollback
    @denumire = N'',
    @descriere = N'',
    @pret_de_baza = -25.5,
    @gramaj = -300,
    @inaltime = -2.5,
    @latime = -30,
    @total = -25.5,
    @client_id = 37692,
    @adresa_facturare = N'',
    @adresa_livrare = N'',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2024-03-13',
    @estimare_sfarsit = '2024-03-01',
    @status_c = N'',
    @magazin_id = 1,
	@cantitate = -1;

--- produs invalid
EXEC insereazaProdusComandaNoRollback
    @denumire = N'Farfurie pizzaOK',
    @descriere = N'Farfurie plata cu model de pizza, cu margini ridicate',
    @pret_de_baza = -25.5,
    @gramaj = 300,
    @inaltime = 2.5,
    @latime = 30,
    @total = 25.5,
    @client_id = 37692,
    @adresa_facturare = N' Cluj-Napoca',
    @adresa_livrare = N'Str. Mihai Eminescu nr 1, Cluj-Napoca',
    @data_inregistrare = '2025-03-11',
    @livrare_id = 2,
    @estimare_inceput = '2025-03-13',
    @estimare_sfarsit = '2025-03-16',
    @status_c = N'Livrata',
    @magazin_id = 1,
	@cantitate = 1;

--- client inexistent

WHILE @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;

Print @@TRANCOUNT