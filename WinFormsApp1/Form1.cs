using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConnectToBooksSystem();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=ASUS\SQLEXPRESS;Database=BooksSystem;Trusted_Connection=true";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string insertQuery = @"Insert Into Book (BookName,Price,InStock)
                            Values (@bookName,@price,1)";
            SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@bookName", txtBookName.Text);
            sqlCommand.Parameters.AddWithValue("@price", txtPrice.Text);
            int rowAffect = sqlCommand.ExecuteNonQuery();

            MessageBox.Show($"{rowAffect} row affected");


            sqlConnection.Close();
            ConnectToBooksSystem();

        }




        public void ConnectToBooksSystem()
        {
            string connectionString = @"Server=ASUS\SQLEXPRESS;Database=BooksSystem;Trusted_Connection=true";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string sqlQuery = @"Select Id Id,
                               BookName Book_Name,
                               Price Book_Price 
                               from Book";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            var data = sqlCommand.ExecuteReader();

            List<Book> books = new List<Book>();
            while (data.Read())
            {
                Book book = new Book
                {
                    Id = (int)data["Id"],
                    Book_Name = data["Book_Name"].ToString(),
                    Book_Price = (int)data["Book_Price"]
                };
                books.Add(book);
            }
            dataGridView1.DataSource = books;
            sqlConnection.Close();
        }
    }
}
