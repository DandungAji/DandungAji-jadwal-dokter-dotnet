IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DoctorDb')
BEGIN
    CREATE DATABASE DoctorDb;
END
GO
USE DoctorDb;
GO

IF OBJECT_ID('JadwalDokter', 'U') IS NULL
BEGIN
    CREATE TABLE JadwalDokter (
        Id INT PRIMARY KEY IDENTITY(1,1),
        NamaDokter NVARCHAR(100),
        Spesialis NVARCHAR(100),
        Hari NVARCHAR(50),
        Jam NVARCHAR(50)
    );
END
GO

IF NOT EXISTS (SELECT * FROM JadwalDokter)
BEGIN
    INSERT INTO JadwalDokter (NamaDokter, Spesialis, Hari, Jam) VALUES
    ('dr. Ricki Rajagukguk, Sp. A', 'Spesialis Anak', 'Senin', '08.00-11.00'),
    ('dr. Yogie Setyabudi, Sp. PD', 'Spesialis Penyakit Dalam', 'Selasa', '08.00-10.00'),
    ('dr. Andi, Sp. B', 'Spesialis Bedah', 'Rabu', '13.30-15.00'),
    ('dr. Adrian Narene, Sp. OG', 'Spesialis Obgyn', 'Rabu', '12.00-18.00'),
    ('dr. Aih Cahyani, Sp. S(K)', 'Spesialis Saraf', 'Sabtu', '17.00-19.00'),
    ('dr. Irma, Sp. DVE', 'Spesialis Kulit dan Kelamin', 'Jumat', '15.30-19.00');
END
GO

IF OBJECT_ID('GetJadwalDokter', 'P') IS NOT NULL
    DROP PROCEDURE GetJadwalDokter;
GO

CREATE PROCEDURE GetJadwalDokter
AS
BEGIN
    WITH DokterCTE AS (
        SELECT Id, NamaDokter, Spesialis, Hari, Jam
        FROM JadwalDokter
    )
    SELECT * FROM DokterCTE;
END;
GO
