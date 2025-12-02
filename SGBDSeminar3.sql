Create database SGBDSeminar3
GO
Use SGBDSeminar3

CREATE TABLE Clienti 
(cod_c INT PRIMARY KEY IDENTITY,
nume VARCHAR(200),
email VARCHAR(100) UNIQUE,
parola VARCHAR(200),
data_nasterii DATE
);
CREATE TABLE Conturi
(cod_cont INT PRIMARY KEY IDENTITY,
detalii VARCHAR(300),
suma DECIMAL,
moneda VARCHAR(20),
data_creare DATE,
cod_c INT FOREIGN KEY REFERENCES Clienti(cod_c)
);
--tranzactie autocommit
INSERT INTO Clienti (nume, email, parola, data_nasterii) VALUES
('Client1','client1@gmail.com','qawsfgrhy2','1999-02-02'),
('Client2','client2@gmail.com','asdfg2142534yega','1987-09-09');
--verificarea numarului de tranzactii deschise pe conexiunea curenta
PRINT @@TRANCOUNT;
--tranzactie explicita
BEGIN TRAN;
PRINT @@TRANCOUNT;
SELECT * FROM Clienti;
INSERT INTO Clienti (nume, email, parola, data_nasterii) VALUES
('Client3','client3@gmail.com','asd132453tyrad','1990-09-20');
SELECT * FROM Clienti;
INSERT INTO Conturi (detalii,suma,moneda,data_creare,cod_c) VALUES 
('IR64234567878976543',1000.32,'CHF','2025-04-03',3);
SELECT * FROM Conturi;
COMMIT TRAN;
PRINT @@TRANCOUNT;
SELECT * FROM Clienti;
SELECT * FROM Conturi;
--tranzactii implicite
SET IMPLICIT_TRANSACTIONS ON;
PRINT @@TRANCOUNT;
SELECT * FROM Conturi;
PRINT @@TRANCOUNT;
INSERT INTO Conturi (detalii,suma,moneda,data_creare,cod_c) VALUES
('IR451235465768776',400.56,'MXN','2025-04-03',2);
SELECT * FROM Conturi;
COMMIT TRAN;
PRINT @@TRANCOUNT;
SELECT * FROM Clienti;
PRINT @@TRANCOUNT;
INSERT INTO Clienti (nume, email, parola, data_nasterii) VALUES
('Client temporar','clienttemporar@gmail.com','25qewasfdgfhtrq','2000-02-02');
SELECT * FROM Clienti;
ROLLBACK TRAN;
PRINT @@TRANCOUNT;
SET IMPLICIT_TRANSACTIONS OFF;
SELECT * FROM Clienti;
PRINT @@TRANCOUNT;