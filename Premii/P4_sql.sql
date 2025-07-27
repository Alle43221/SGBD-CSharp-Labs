create database Premii_SGBD;
use Premii_SGBD;

CREATE TABLE Tipuri_premii
(
cod INT PRIMARY KEY IDENTITY,
nume VARCHAR(200),
stat VARCHAR(20) CHECK (stat IN ('activa', 'inactiva')),
anual BIT,
numar_castigatori INT
);

CREATE TABLE Premii
(
cod INT PRIMARY KEY IDENTITY,
nume VARCHAR(200),
sponsor VARCHAR(200),
an INT,
nume_castigator VARCHAR(1000),
varsta INT,
cod_tip INT FOREIGN KEY REFERENCES Tipuri_premii(cod)
ON UPDATE CASCADE ON DELETE CASCADE
);

insert into Tipuri_premii(nume,stat,anual,numar_castigatori)values
('Tip1','activa', 1, 3),
('Tip2','activa', 0, 6),
('Tip3','inactiva', 1, 5),
('Tip4','inactiva',0, 7),
('Tip5','inactiva',1, 3);

INSERT INTO Premii (nume, sponsor, an, nume_castigator, varsta, cod_tip) VALUES
-- Type 1 Awards
('Premiul Excelenței', 'TechCorp', 2022, 'Andrei Popescu', 35, 1),
('Premiul Inovației', 'TechCorp', 2023, 'Ioana Ionescu', 29, 1),
('Premiul Cercetării', 'TechCorp', 2024, 'Mihai Georgescu', 41, 1),

-- Type 2 Awards
('Premiul Creativității', 'ArtVision', 2021, 'Elena Marin', 33, 2),
('Premiul Designului', 'ArtVision', 2022, 'Cristian Vasile', 27, 2),
('Premiul Culturii', 'ArtVision', 2023, 'Simona Dinu', 38, 2),

-- Type 3 Awards
('Premiul Umanitar', 'HealthAid', 2022, 'Alin Neagu', 45, 3),
('Premiul pentru Voluntariat', 'HealthAid', 2023, 'Raluca Tomescu', 31, 3),
('Premiul Speranței', 'HealthAid', 2024, 'Bogdan Iacob', 36, 3),

-- Type 4 Awards
('Premiul Ecologic', 'GreenFuture', 2021, 'Irina Lupu', 28, 4),
('Premiul Verde', 'GreenFuture', 2022, 'Dan Popa', 34, 4),
('Premiul Planetei', 'GreenFuture', 2023, 'Claudia Radu', 30, 4),

-- Type 5 Awards
('Premiul Tehnologiei', 'InnovaTech', 2022, 'Sorin Matei', 39, 5),
('Premiul Software', 'InnovaTech', 2023, 'Bianca Nedelcu', 26, 5),
('Premiul AI', 'InnovaTech', 2024, 'Victor Enache', 32, 5);

/*

//scrieti un sql care returneaza toate tipurile de premii pentru care exista minim 3 castigatori

// scrieti un sql care returneaza varsta medie a castigatorilor sub varsta de 18 ani

// creati un index si pentru una dintre interogarile de mai sus
*/

select T.cod, T.nume from Tipuri_premii AS T, Premii as P
Where P.cod_tip= T.cod
group by T.cod, T.nume
having count(P.cod)>=3;

select AVG(varsta) from premii where varsta<18;

create nonclustered index i_varsta on Premii(varsta);
