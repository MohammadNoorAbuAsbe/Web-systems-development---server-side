CREATE TABLE dbo.RentedMovies_2025_Test (
    userId SMALLINT NOT NULL,
    movieId SMALLINT NOT NULL,
    rentStart DATE NOT NULL,
    rentEnd DATE NOT NULL,
    totalPrice DECIMAL(10,2) NOT NULL,
    deletedAt DATE DEFAULT NULL,
    
    PRIMARY KEY (userId, movieId, rentStart),
    
    FOREIGN KEY (userId) 
        REFERENCES dbo.Users_2025_Test(id)
        ON DELETE CASCADE,
        
    FOREIGN KEY (movieId) 
        REFERENCES dbo.Movies_2025_Test(id)
        ON DELETE CASCADE,

	CONSTRAINT CHK_ValidRentalPeriod_Test CHECK (rentEnd >= rentStart)
);


CREATE TRIGGER UpdateMovieRentalStats_Test
ON dbo.RentedMovies_2025_Test
AFTER INSERT
AS
BEGIN
    UPDATE dbo.Movies_2025_Test
    SET rentalCount = rentalCount + 1,
		grossWorldwide = grossWorldwide + totalPrice
    FROM inserted
    WHERE dbo.Movies_2025_Test.id = inserted.movieId;
END;

CREATE TRIGGER CalculateRentalPrice_Test
ON dbo.RentedMovies_2025_Test
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO dbo.RentedMovies_2025_Test 
        (userId, movieId, rentStart, rentEnd, totalPrice, deletedAt)
    SELECT
        i.userId,
        i.movieId,
        i.rentStart,
        i.rentEnd,
        DATEDIFF(DAY, i.rentStart, i.rentEnd) * m.priceToRent AS calculatedPrice,
        i.deletedAt
    FROM inserted i
    INNER JOIN dbo.Movies_2025_Test m
        ON i.movieId = m.id;
END;