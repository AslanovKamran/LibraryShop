using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdoNet_Exam.Storage
{
    public class DataStorage
    {
        private SqlConnection GetConnection()
        {
            var connection_String = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return new SqlConnection(connection_String);
        }
        public void InsertNewUser(User new_user)
        {
            var user_FirstName = new_user.FirstName;
            var user_LastName = new_user.LastName;
            var user_Login = new_user.Login;
            var user_Password = new_user.Password;
            var query = @"INSERT INTO Users VALUES(@user_FirstName,@user_LastName,@user_Login ,@user_Password,0)";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter { ParameterName = "@user_FirstName", Value = user_FirstName });
                command.Parameters.Add(new SqlParameter { ParameterName = "@user_LastName", Value = user_LastName });
                command.Parameters.Add(new SqlParameter { ParameterName = "@user_Login", Value = user_Login });
                command.Parameters.Add(new SqlParameter { ParameterName = "@user_Password", Value = user_Password });
                int result;
                try
                {
                    result = command.ExecuteNonQuery();
                    MessageBox.Show($"Sign up has ended Succesfully");
                }
                catch (Exception)
                {
                    MessageBox.Show("This login is already in use");
                }
            }
        }

        #region Get Data

        public ObservableCollection<User> GetUsers()
        {
            var users_list = new ObservableCollection<User>();

            var query = @"SELECT * FROM Users";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_user = new User();
                            new_user.Id = reader.GetInt32(0);
                            new_user.FirstName = reader.GetString(1);
                            new_user.LastName = reader.GetString(2);
                            new_user.Login = reader.GetString(3);
                            new_user.Password = reader.GetString(4);
                            new_user.IsAdmin = reader.GetBoolean(5);
                            users_list.Add(new_user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return users_list;
            }
        }

        public ObservableCollection<Book> GetBooks()
        {
            var books_list = new ObservableCollection<Book>();

            var query = @"SELECT Books.[Id], Books.[Name], Books.[PublisherId],Books.[AmountOfPages], Books.[Year],
                          Books.[PrimeCost], Books.[DiscountPercent], Books.[Price], Books.[AmountOfBook], Books.[IsSequel],
                          (Authors.[FirstName] + ' ' + Authors.[LastName])  AS [Author],
                          Genres.GenreType FROM Books
                          JOIN AuthorBooks ON AuthorBooks.BookdId = Books.Id
                          JOIN Authors ON AuthorBooks.AuthorId = Authors.Id
                          JOIN GenresBooks ON GenresBooks.BookdId = Books.Id
                          JOIN Genres ON GenresBooks.GenreId = Genres.Id
                          ORDER BY Books.Name";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_book = new Book();
                            new_book.Id = reader.GetInt32(0);
                            new_book.Name = reader.GetString(1);
                            new_book.PublisherId = reader.GetInt32(2);
                            new_book.AmountOfPages = reader.GetInt32(3);
                            new_book.Year = reader.GetDateTime(4);
                            new_book.PrimeCost = Math.Round(reader.GetDecimal(5), 2);
                            new_book.DiscountPercent = reader.GetInt32(6);
                            new_book.Price = Math.Round(reader.GetDecimal(7), 2);
                            new_book.AmountOfBook = reader.GetInt32(8);
                            new_book.IsSequel = reader.GetBoolean(9);
                            new_book.BookAuthor = reader.GetString(10);
                            new_book.BookGenre = reader.GetString(11);
                            books_list.Add(new_book);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return books_list;
            }
        }

        public ObservableCollection<Order> GetOrdersOfCurrentUser(int userId)
        {
            var order_list = new ObservableCollection<Order>();

            var query = $@"SELECT Orders.Id, Books.Name, Orders.OrderDate From Orders
                           JOIN Books ON Orders.BookId = Books.Id
                           WHERE UserId = @userId";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@userId", Value = userId, SqlDbType = System.Data.SqlDbType.Int });
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_order = new Order();
                            new_order.Id = reader.GetInt32(0);
                            new_order.BookName = reader.GetString(1);
                            new_order.OrderDate= reader.GetDateTime(2);
                            order_list.Add(new_order);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return order_list;
            }
        }

        public ObservableCollection<Publisher> GetPublishers()
        {
            var publisher_list = new ObservableCollection<Publisher>();

            var query = @"SELECT * FROM Publishers 
                          ORDER BY Id";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_publisher = new Publisher();
                            new_publisher.Id = reader.GetInt32(0);
                            new_publisher.Name = reader.GetString(1);
                            publisher_list.Add(new_publisher);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return publisher_list;
            }
        }

        public ObservableCollection<Author> GetAuthors()
        {
            var author_list = new ObservableCollection<Author>();

            var query = @"SELECT * FROM Authors 
                          ORDER BY Id";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_author = new Author();
                            new_author.Id = reader.GetInt32(0);
                            new_author.FirstName = reader.GetString(1);
                            new_author.LastName = reader.GetString(2);
                            author_list.Add(new_author);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return author_list;
            }
        }

        public ObservableCollection<Genre> GetGenres()
        {

            var genre_list = new ObservableCollection<Genre>();

            var query = @"SELECT * FROM Genres
                        ORDER BY Id";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_genre = new Genre();
                            new_genre.Id = reader.GetInt32(0);
                            new_genre.GenreType = reader.GetString(1);
                            genre_list.Add(new_genre);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return genre_list;
            }
        }

        public ObservableCollection<Stock> GetStocks()
        {
            var stocks_list = new ObservableCollection<Stock>();

            var query = @"SELECT Stocks.Id, Books.Name, Books.PrimeCost, Books.DiscountPercent , Books.Price FROM Stocks
                          JOIN Books ON Stocks.BookId = Books.Id";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_stock = new Stock();
                            new_stock.Id = reader.GetInt32(0);
                            new_stock.BookName = reader.GetString(1);
                            new_stock.PrimeCost = reader.GetDecimal(2);
                            new_stock.DiscountPercent = reader.GetInt32(3);
                            new_stock.Price = reader.GetDecimal(4);
                            stocks_list.Add(new_stock);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return stocks_list;
            }

        }

        public ObservableCollection<Order> GetOrders()
        {
            var orders_list = new ObservableCollection<Order>();

            var query = @" SELECT Orders.Id, Books.Id, Users.Id, Books.Name, Users.Login, Orders.OrderDate FROM Orders
                          JOIN Users ON Orders.UserId = Users.Id
                          JOIN Books ON Orders.BookId = Books.Id";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var new_order = new Order();
                            new_order.Id = reader.GetInt32(0);
                            new_order.BookId = reader.GetInt32(1);
                            new_order.UserId = reader.GetInt32(2);
                            new_order.BookName = reader.GetString(3);
                            new_order.UserLogin = reader.GetString(4);
                            new_order.OrderDate = reader.GetDateTime(5);
                            orders_list.Add(new_order);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return orders_list;
            }
        }
        #endregion

        #region DataDeleting
        public void RemoveBook(Book _selectedBook)
        {
            var query = @$"DELETE FROM Books WHERE Id LIKE @id";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = _selectedBook.Id, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void RemovePublisher(Publisher _selectedPublisher)
        {
            var query = @$"DELETE FROM Publishers WHERE Id LIKE @id";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = _selectedPublisher.Id, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void RemoveAuthor(Author _selectedAuthor)
        {
            var query = @$"DELETE FROM Authors WHERE Id LIKE @id";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = _selectedAuthor.Id, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void RemoveGenre(Genre _selectedGenre)
        {
            var query = @$"DELETE FROM Genres WHERE Id LIKE @id";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = _selectedGenre.Id, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void RemoveStock(Stock _selectedStock)
        {
            var query = @$"DELETE FROM Stocks WHERE Id LIKE @id";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = _selectedStock.Id, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void RemoveOrder(Order _selectedOrder)
        {
            var query = @$"DELETE FROM Orders WHERE Id LIKE @id";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = _selectedOrder.Id, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        #region Add Data 

        public void AddAuthor(Author _newAuthor)
        {
            var query = @$"INSERT INTO Authors VALUES(@fname, @lname)";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@fname", Value = _newAuthor.FirstName, SqlDbType = System.Data.SqlDbType.NVarChar });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@lname", Value = _newAuthor.LastName, SqlDbType = System.Data.SqlDbType.NVarChar });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void AddGenre(Genre _newGenre)
        {
            var query = @$"INSERT INTO Genres VALUES (@Type)";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Type", Value = _newGenre.GenreType, SqlDbType = System.Data.SqlDbType.NVarChar });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void AddPublisher(Publisher _newPublisher)
        {
            var query = @$"INSERT INTO Publishers VALUES (@Name)";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Name", Value = _newPublisher.Name, SqlDbType = System.Data.SqlDbType.NVarChar });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void AddNewStock(Stock _newStock, Book _selectedBook, int _discountPercent)
        {
            int _bookId = _selectedBook.Id;
            var query = @$"INSERT INTO Stocks VALUES (@_bookId)";


            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_bookId", Value = _bookId, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

            var query2 = @$"UPDATE Books SET DiscountPercent = {_discountPercent} WHERE Books.Id = {_bookId}";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query2, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_bookId", Value = _bookId, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }
        public void AddNewBook(Book _newBook, int _publisherId)
        {
            var query = @$"INSERT INTO Books VALUES (@Name,@PublisherId,@AmountOfPages,@Year,@PrimeCost,@DiscountPercent,0,@AmountOfBooks,@IsSequel)";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Name", Value = _newBook.Name, SqlDbType = System.Data.SqlDbType.NVarChar });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@PublisherId", Value = _publisherId, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@AmountOfPages", Value = _newBook.AmountOfPages, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Year", Value = _newBook.Year, SqlDbType = System.Data.SqlDbType.Date });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@PrimeCost", Value = _newBook.PrimeCost, SqlDbType = System.Data.SqlDbType.Decimal });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@DiscountPercent", Value = _newBook.DiscountPercent, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@AmountOfBooks", Value = _newBook.AmountOfBook, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@IsSequel", Value = _newBook.IsSequel, SqlDbType = System.Data.SqlDbType.Bit });
                    res = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void AddGenresToNewBook(string _newBookName, int _genreId)
        {
            var query = $@"DECLARE @Id INT 
                           SET @Id = (SELECT Id FROM Books WHERE Books.Name LIKE N'{_newBookName}')
                           INSERT INTO GenresBooks VALUES(@Id,@_genreID)";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_genreID", Value = _genreId, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }
        public void AddAuthorsToNewBook(string _newBookName, int _authorId) 
        {
            var query = $@"DECLARE @Id INT 
                           SET @Id = (SELECT Id FROM Books WHERE Books.Name LIKE N'{_newBookName}')
                           INSERT INTO AuthorBooks VALUES (@Id,@_authorId)";

            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_authorId", Value = _authorId, SqlDbType = System.Data.SqlDbType.Int });
                    res = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void AddNewOrder(Book _selectedBook, int _userId, DateTime _orderDate ) 
        {
            int _bookId = _selectedBook.Id;
            var query = @$"INSERT INTO Orders VALUES (@_bookId, @_userId, @_orderDate)";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_bookId", Value = _bookId, SqlDbType = System.Data.SqlDbType.Int});
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_userId", Value = _userId, SqlDbType = System.Data.SqlDbType.Int});
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_orderDate", Value = _orderDate, SqlDbType = System.Data.SqlDbType.Date});
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Input Propper Date And Select A Book", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #region Update Data 

        public void UpdateGenre(Genre _selectedGenre, string _newType)
        {
            int id = _selectedGenre.Id;
            var query = $@"UPDATE Genres SET GenreType = @Type WHERE Id = {id}";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Type", Value = _newType, SqlDbType = System.Data.SqlDbType.NVarChar });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void UpdatePublisher(Publisher _selectedPublisher, string _newName)
        {
            int id = _selectedPublisher.Id;
            var query = $@"UPDATE Publishers SET Name = @Name WHERE Id = {id}";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Name", Value = _newName, SqlDbType = System.Data.SqlDbType.NVarChar });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void UpdateAuthors(Author _selectedAuthor, string _newName, string _newSurname)
        {
            var id = _selectedAuthor.Id;
            var query = @$"UPDATE Authors SET FirstName = @fname, LastName = @lname WHERE Id  = {id}";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@fname", Value = _newName, SqlDbType = System.Data.SqlDbType.NVarChar });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@lname", Value = _newSurname, SqlDbType = System.Data.SqlDbType.NVarChar });
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void UpdateBooks(Book _selectedBook, string _bookName, int _publisherId, int _amountOfPages, DateTime _year, decimal _primeCost, int _discountPercent, int _amountOfBook, bool _isSequel) 
        {
            int id = _selectedBook.Id;
            var query = @$"UPDATE Books SET Name = @_bookName, PublisherId = @_publisherId, AmountOfPages = @_amountOfPages, Year = @_year, PrimeCost = @_primeCost, DiscountPercent = @_discountPercent, AmountOfBook = @_amountOfBook, IsSequel = @_isSequel WHERE Id = { id }";
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int res;
                try
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_bookName", Value = _bookName, SqlDbType = System.Data.SqlDbType.NVarChar });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_publisherId", Value = _publisherId, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_amountOfPages", Value = _amountOfPages, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_year", Value = _year, SqlDbType = System.Data.SqlDbType.Date });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_primeCost", Value = _primeCost, SqlDbType = System.Data.SqlDbType.Decimal });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_discountPercent", Value = _discountPercent, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_amountOfBook", Value = _amountOfBook, SqlDbType = System.Data.SqlDbType.Int });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@_isSequel", Value = _isSequel, SqlDbType = System.Data.SqlDbType.Bit });
                    res = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

    }
}
