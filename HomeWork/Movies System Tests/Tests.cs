using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies_System.Controllers;
using Movies_System.DAL;
using Movies_System.Models;

namespace Movies_System_Tests
{
    public sealed class Tests : IDisposable
    {
        #region Setup
        private readonly DBservices _db;
        private readonly UsersController _controller;

        public Tests()
        {
            _controller = new UsersController();
            DbConfig.EnableTestMode();
            _db = new DBservices();
        }
        #endregion

        #region User Class Tests
        [Fact]
        public void Read_WhenCalled_ReturnsAllUsers()
        {
            // Arrange
            var users = User.Read();
            users.Clear();
            var user1 = new User(1, "User1", "user1@test.com", "pass", true);
            var user2 = new User(2, "User2", "user2@test.com", "pass", true);
            users.Add(user1);
            users.Add(user2);

            // Act
            var result = User.Read();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(user1, result);
            Assert.Contains(user2, result);
        }


        [Fact]
        public void GetByEmail_ExistingEmail_ReturnsUser()
        {
            // Arrange
            var users = User.Read();
            users.Clear();
            var expectedUser = new User(1, "Test", "test@test.com", "pass", true);
            users.Add(expectedUser);

            // Act
            var result = User.GetByEmail("test@test.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Email, result.Email);
        }

        [Fact]
        public void GetByEmail_NonExistentEmail_ReturnsNull()
        {
            // Arrange
            User.Read().Clear();

            // Act
            var result = User.GetByEmail("nonexistent@test.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsUserInfo()
        {
            // Arrange
            var users = User.Read();
            users.Clear();
            string password = "password";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 15);
            var user = new User(1, "Test User", "test@example.com", hashedPassword, true);
            users.Add(user);

            // Act
            var result = User.Login("test@example.com", "password");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
            Assert.True(result.Active);
        }

        [Fact]
        public void Login_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var users = User.Read();
            users.Clear();
            string password = "password";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 15);
            var user = new User(1, "Test User", "test@example.com", hashedPassword, true);
            users.Add(user);

            // Act
            var result = User.Login("test@example.com", "wrongpassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Login_NonExistentEmail_ReturnsNull()
        {
            // Arrange
            User.Read().Clear();

            // Act
            var result = User.Login("nonexistent@example.com", "password");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Register_ValidUser_InsertsIntoDatabase()
        {
            // Arrange
            var user = new User(1, "Test User", "test@example.com", "password", true);

            // Act
            bool result = User.Register(user);

            // Assert
            Assert.True(result);
            using (var connection = _db.connect("myProjDB"))
            {
                var cmd = new SqlCommand("SELECT * FROM Users_2025_Test WHERE Email = 'test@example.com'", connection);
                var reader = cmd.ExecuteReader();
                Assert.True(reader.HasRows);
                reader.Close();
            }
        }

        [Fact]
        public void Register_AlreadyExistingUser_ReturnsFalse()
        {
            //Arrange
            var user = new User(1, "Test User", "test@example.com", "password", true);
            User.Register(user);

            //Act
            bool result = User.Register(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteUserById_ExistingUser_ReturnTrue()
        {
            // Arrange
            var user = new User(1, "Test User", "test@example.com", "password", true);
            User.Register(user);
            int userId = user.Id;

            // Act
            bool result = User.DeleteUserById(userId);
            Assert.True(result);
            using (var connection = _db.connect("myProjDB"))
            {
                var cmd = new SqlCommand("SELECT * FROM Users_2025_Test WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", userId);
                var reader = cmd.ExecuteReader();
                Assert.False(reader.HasRows);
                reader.Close();
            }
        }

        [Fact]
        public void DeleteUserById_NonExistingUser_ReturnFalse()
        {
            // Arrange
            int userId = 999;
            // Act
            bool result = User.DeleteUserById(userId);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateUser_ExistingUser_ReturnsTrue()
        {
            // Arrange
            var user = new User(1, "Test User", "test@example.com", "password", true);
            User.Register(user);
            user.Name = "Updated User";

            // Act
            bool result = User.UpdateUser(user);
            Assert.True(result);
            using (var connection = _db.connect("myProjDB"))
            {
                var cmd = new SqlCommand("SELECT * FROM Users_2025_Test WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                var reader = cmd.ExecuteReader();
                Assert.True(reader.HasRows);
                while (reader.Read())
                {
                    Assert.Equal("Updated User", reader["Name"].ToString());
                }
                reader.Close();
            }
        }

        [Fact]
        public void UpdateUser_InvalidUser_ReturnsFalse()
        {
            // Act
            bool result = User.UpdateUser(new User());
            // Assert
            Assert.False(result);
        }
        #endregion

        #region UsersController Tests
        [Fact]
        public void Get_ReturnsAllUsers()
        {
            // Arrange
            User.Register(new User());
            User.Register(new User());

            // Act
            var result = _controller.Get();


            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Register_ValidUser_ReturnsOk()
        {
            // Arrange
            var user = new User(1, "Test User", "test@example.com", "password", true);

            // Act
            var result = _controller.Register(user);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult.Value);

            var response = okResult.Value as RegisterResponse;
            Assert.Equal("User registered successfully", response.Message);
            Assert.True(response.Success);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsOk()
        {
            User.Register(new User(1, "Test User", "valid@example.com", "valid", true));
            // Arrange
            var request = new LoginRequest { Email = "valid@example.com", Password = "valid" };

            // Act
            var result = _controller.Login(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_InValidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest { Email = "Invalid@example.com", Password = "Invalid" };

            // Act
            var result = _controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public void Delete_ValidId_ReturnsTrue()
        {
            // Arrange
            int validId = 1;

            // Act
            var result = _controller.Delete(validId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Put_ValidUser_ReturnsTrue()
        {
            // Arrange
            var user = new User { Id = 1, Email = "updated@example.com" };

            // Act
            var result = _controller.Put(user);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region Movie Class Tests

        #endregion

        #region MoviesController Tests
        [Fact]
        public void Insert_NewMovie_ReturnTrue()
        {
            // Arrange
            var movie = CreateTestMovie(-1);
            // Act
            var result = Movie.Insert(movie);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Insert_ExistingMovie_ReturnFalse()
        {
            // Arrange
            var movie = CreateTestMovie(-1);
            // Act
            var result1 = Movie.Insert(movie);
            var result2 = Movie.Insert(movie);
            // Assert
            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void DeleteMovieById_ValidId_ReturnsTrue()
        {
            // Arrange
            var movie = CreateTestMovie(-1);
            Movie.Insert(movie);
            var insertedId = movie.Id;

            // Act
            var result = Movie.DeleteMovieById(insertedId);

            // Assert
            Assert.True(result);
            using (var connection = _db.connect("myProjDB"))
            {
                var cmd = new SqlCommand("SELECT * FROM Movies_2025_Test WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", insertedId);
                var reader = cmd.ExecuteReader();
                Assert.False(reader.HasRows);
                reader.Close();
            }
        }

        [Fact]
        public void DeleteMovieById_InvalidId_ReturnsFalse()
        {
            // Act
            var result = Movie.DeleteMovieById(-999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateMovie_ValidMovie_ReturnsTrue()
        {
            // Arrange
            var movie = CreateTestMovie(-1);
            Movie.Insert(movie);
            movie.PrimaryTitle = "Updated Title";

            // Act
            var result = Movie.UpdateMovie(0, movie);

            // Assert
            Assert.True(result);
            using (var connection = _db.connect("myProjDB"))
            {
                var cmd = new SqlCommand("SELECT * FROM Movies_2025_Test WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", movie.Id);
                var reader = cmd.ExecuteReader();
                Assert.True(reader.HasRows);
                while (reader.Read())
                {
                    Assert.Equal("Updated Title", reader["PrimaryTitle"].ToString());
                }
                reader.Close();
            }
        }

        [Fact]
        public void UpdateMovie_InvalidMovie_ReturnsFalse()
        {
            // Arrange
            var movie = new Movie { Id = -999 }; // Non-existent movie

            // Act
            var result = Movie.UpdateMovie(999, movie);

            // Assert
            Assert.False(result);
        }

        #region Movies Test Helpers
        private Movie CreateTestMovie(int id) => new Movie(
            id: id,
            url: "http://test.com",
            primaryTitle: "Integration Test Movie",
            description: "Test Description",
            primaryImage: "test.jpg",
            year: 2023,
            releaseDate: DateTime.Now,
            language: "English",
            budget: 1000000,
            grossWorldwide: 5000000,
            genres: "Action",
            isAdult: false,
            runtimeMinutes: 120,
            averageRating: 8.5f,
            numVotes: 1000
        );
        #endregion

        #endregion

        #region Dispose
        public void Dispose()
        {
            // Cleanup: Delete all data after each test
            using (var connection = _db.connect("myProjDB"))
            {
                var cmd = new SqlCommand("DELETE FROM Users_2025_Test", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM Movies_2025_Test", connection);
                cmd.ExecuteNonQuery();
            }

        }
        #endregion
    }
}
