CREATE TABLE dbo.RentedMovies_2025 (
    userId SMALLINT NOT NULL,
    movieId SMALLINT NOT NULL,
    rentStart DATE NOT NULL,
    rentEnd DATE NOT NULL,
    totalPrice DECIMAL(10,2) NOT NULL,
    deletedAt DATE DEFAULT NULL,
    
    PRIMARY KEY (userId, movieId, rentStart),
    
    FOREIGN KEY (userId) 
        REFERENCES dbo.Users_2025(id)
        ON DELETE CASCADE,
        
    FOREIGN KEY (movieId) 
        REFERENCES dbo.Movies_2025(id)
        ON DELETE CASCADE,

	CONSTRAINT CHK_ValidRentalPeriod CHECK (rentEnd >= rentStart)
);


CREATE TRIGGER UpdateMovieRentalStats
ON dbo.RentedMovies_2025
AFTER INSERT
AS
BEGIN
    UPDATE dbo.Movies_2025
    SET rentalCount = rentalCount + 1,
		grossWorldwide = grossWorldwide + totalPrice
    FROM inserted
    WHERE dbo.Movies_2025.id = inserted.movieId;
END;

CREATE TRIGGER CalculateRentalPrice
ON dbo.RentedMovies_2025
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO dbo.RentedMovies_2025 
        (userId, movieId, rentStart, rentEnd, totalPrice, deletedAt)
    SELECT
        i.userId,
        i.movieId,
        i.rentStart,
        i.rentEnd,
        DATEDIFF(DAY, i.rentStart, i.rentEnd) * m.priceToRent AS calculatedPrice,
        i.deletedAt
    FROM inserted i
    INNER JOIN dbo.Movies_2025 m
        ON i.movieId = m.id;
END;