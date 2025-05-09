CREATE TABLE [dbo].[Movies_2025] (
	id INT IDENTITY(1,1) PRIMARY KEY,
    [url] NVARCHAR(MAX) NULL,
    primaryTitle NVARCHAR(255) NOT NULL UNIQUE,
    [description] NVARCHAR(MAX) NULL,
    primaryImage NVARCHAR(MAX) NOT NULL,
    [year] INT NOT NULL,
    releaseDate DATE NOT NULL,
    [language] NVARCHAR(50) NOT NULL,
    budget DECIMAL(18,2) NOT NULL,
    grossWorldwide DECIMAL(18,2) NOT NULL DEFAULT 0,
    genres NVARCHAR(255) NULL,
    isAdult BIT NOT NULL DEFAULT 0,
    runtimeMinutes INT NOT NULL,
    averageRating DECIMAL(3,1) NOT NULL DEFAULT 0,
    numVotes INT NOT NULL DEFAULT 0,
	priceToRent INT NOT NULL DEFAULT (FLOOR(RAND() * 21) + 10),
    rentalCount INT NOT NULL DEFAULT 0,
	deletedAt DATE NULL
);

CREATE TYPE dbo.MovieListType AS TABLE (
    [url] NVARCHAR(MAX),
    primaryTitle NVARCHAR(255),
    [description] NVARCHAR(MAX),
    primaryImage NVARCHAR(MAX),
    [year] INT,
    releaseDate DATE,
    [language] NVARCHAR(50),
    budget DECIMAL(18,2),
    grossWorldwide DECIMAL(18,2),
    genres NVARCHAR(255),
    isAdult BIT,
    runtimeMinutes INT,
    averageRating DECIMAL(3,1),
    numVotes INT
);

ALTER TABLE [dbo].[Movies_2025]
ADD CONSTRAINT Unique_primaryTitle UNIQUE(primaryTitle);