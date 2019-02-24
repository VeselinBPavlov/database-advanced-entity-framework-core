namespace HelperClasses.Configurations
{
    using System.Collections.Generic;

    public class DbCommand
    {
        // 01. Initial Setup
        public const string InitialSetupCreateDB = @"CREATE DATABASE MinionsDB";
        // Database configuration -> ON PRIMARY (NAME = N'MinionsDB_Data', FILENAME = N'D:\Courses\Data\MinionsDB_Data.mdf', SIZE = 167872KB, MAXSIZE = UNLIMITED, FILEGROWTH = 16384KB) LOG ON (NAME = N'MinionsDB_Log', FILENAME = N'D:\Courses\Data\MinionsDB_Log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 16384KB ) COLLATE SQL_Latin1_General_CP1_CI_AS;

        public IReadOnlyCollection<string> InitialSetupCreateTables = new string[]
        {
            "CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))",
            "CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))",
            "CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))",
            "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))",
            "CREATE TABLE Villains(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))",
            "CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id), CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId))"
        };

        public IReadOnlyCollection<string> InitialSetupInsert = new string[]
        {
            "INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')",
            "INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)",
            "INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)",
            "INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')",
            "INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)",
            "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)"
        };

        // 02. Villain Names
        public const string VillainNames = "SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId GROUP BY v.Id, v.Name HAVING COUNT(mv.VillainId) > 3 ORDER BY COUNT(mv.VillainId)";

        // 03. Minion Names
        public const string MinionNamesVillainName = "SELECT Name FROM Villains WHERE Id = @Id";
        public const string MinionNames = "SELECT ROW_NUMBER() OVER(ORDER BY m.Name) as RowNum, m.Name, m.Age FROM MinionsVillains AS mv JOIN Minions As m ON mv.MinionId = m.Id WHERE mv.VillainId = @Id ORDER BY m.Name";
        
        // 04. Add Minion
        public const string SelectVillainId = "SELECT Id FROM Villains WHERE Name = @Name";
        public const string SelectMinionId = "SELECT Id FROM Minions WHERE Name = @Name";
        public const string InsertMinnionVillian = "INSERT INTO MinionsVillains(MinionId, VillainId) VALUES(@villainId, @minionId)";
        public const string InsertVillain = "INSERT INTO Villains(Name, EvilnessFactorId)  VALUES(@villainName, 4)";
        public const string InsertMinion = "INSERT INTO Minions(Name, Age, TownId) VALUES(@name, @age, @townId)";
        public const string InsertTown = "INSERT INTO Towns(Name) VALUES(@townName)";
        public const string SelectTownId = "SELECT Id FROM Towns WHERE Name = @townName";

        // 05. Change Town Names Casing
        public const string ChangeTownName = "UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";
        public const string SelectTownsByCountry = "SELECT t.Name FROM Towns as t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @countryName";

        // 06. *Remove Villain 
        public const string SelectVillainById = "SELECT Name FROM Villains WHERE Id = @villainId";
        public const string DeleteMinionVllainById = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
        public const string DeleteVillainById = "DELETE FROM Villains WHERE Id = @villainId";

        // 07. Print All Minion Names
        public const string PrintAllNames = "SELECT Name FROM Minions";

        // 08. Increase Minion Age
        public const string MinionIncreaseAge = "UPDATE Minions SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1 WHERE Id = @Id SELECT Name, Age FROM Minions";
        public const string SelectMinionNameAge = "SELECT Name, Age FROM Minions";

        // 09. Increase Age Stored Procedure 
        public const string IncreaseAgeStoredProcedure = "GO CREATE PROC usp_GetOlder @id INT AS UPDATE Minions SET Age += 1 WHERE Id = @id";
        public const string ExecuteIncreaseAgeProcedure = "EXEC usp_GetOlder @id";
        public const string SelectMinionNameAgeById = "SELECT Name, Age FROM Minions WHERE Id = @Id";
    }
}
