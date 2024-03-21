using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class StudentManagementForm : Form
    {
        public StudentManagementForm()
        {
            InitializeComponent();
        }

        private void StudentManagementForm_Load(object sender, EventArgs e)
        {
            // Calculate the center position of the screen
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            int left = (screenWidth - formWidth) / 2;
            int top = (screenHeight - formHeight) / 2;

            // Set the form's location to center screen
            this.Location = new Point(left, top);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            LoadData();

        }


        public void LoadData()
        {
            // Create columns
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "ID";
            column1.Name = "ID";

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.HeaderText = "FULLNAME";
            column2.Name = "FULLNAME";

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.HeaderText = "CODE";
            column3.Name = "CODE";


            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.HeaderText = "EMAIL";
            column4.Name = "EMAIL";

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.HeaderText = "PHONE";
            column5.Name = "PHONE";

            // Add columns to the DataGridView control
            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);


            dataGridView1.Columns["ID"].DataPropertyName = "ID";
            dataGridView1.Columns["CODE"].DataPropertyName = "CODE";
            dataGridView1.Columns["FULLNAME"].DataPropertyName = "FULLNAME";
            dataGridView1.Columns["EMAIL"].DataPropertyName = "EMAIL";
            dataGridView1.Columns["PHONE"].DataPropertyName = "PHONE";
          

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            GetData();
        }


        private void GetData()
        {
            string connectionString = "Server=DONGVANDINH;Database=FPTU;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM student_table";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    // Create a new instance of the DataGridView
                    dataGridView1.DataSource = dataTable;

                
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text;
            string code = txtCode.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            string connectionString = "Server=DONGVANDINH;Database=FPTU;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO student_table (FULLNAME, CODE, PHONE, EMAIL) " +
                                     "VALUES (@FullName, @Code, @Phone, @Email)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Set parameter values
                    command.Parameters.AddWithValue("@FullName", fullname);
                    command.Parameters.AddWithValue("@Code",code);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Data inserted successfully.");
            Console.ReadLine();

            GetData();


        }
    }
}
