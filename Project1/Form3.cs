using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;

namespace Project1
{
    public partial class formContactList : Form
    {
        public formContactList()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseContacts_Click(object sender, EventArgs e)
        {
            string sqlString = "select * from Phonebook";
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\pezhe\Phonebook.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    List<Contact> myContactList = new List<Contact>();

                    myContactList = db.Query<Contact>(sqlString).ToList();
                    dataGridView1.DataSource = myContactList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                string sqlString = "insert into Phonebook values (@Firstname, @Lastname, @Address, @City, @State, @Zipcode, @CellPhone, @WorkPhone, @Notes)";
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\pezhe\Phonebook.mdf;Integrated Security=True;Connect Timeout=30";
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Firstname", txtFirstName.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Lastname", txtLastName.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Address", txtAddress.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@City", txtCity.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@State", txtState.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Zipcode", txtZipCode.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@CellPhone", txtHomePhone.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@WorkPhone", txtWorkPhone.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Notes", richTextBox1.Text.Trim(), DbType.String, ParameterDirection.Input);

                    rowsAffected = db.Execute(sqlString, parameters);
                    MessageBox.Show($"{rowsAffected} has been inserted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                string sqlString = "delete phonebook where Person_ID = @Person_ID";
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\pezhe\Phonebook.mdf;Integrated Security=True;Connect Timeout=30";
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Person_ID", txtPersonID.Text.Trim(), DbType.String, ParameterDirection.Input);

                    rowsAffected = db.Execute(sqlString, parameters);
                    MessageBox.Show($"{rowsAffected} has been deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = 0;
                string sqlString = "update Phonebook " +
                         " set Firstname = @Firstname, " +
                         "     Lastname =  @Lastname, " +
                         "     Address = @Address, " +
                         "     City = @City, " +
                         "     State = @State, " +
                         "     Zipcode = @Zipcode, " +
                         "     CellPhone = @CellPhone, " +
                         "     WorkPhone = @WorkPhone, " +
                         "     Notes = @Notes " +
                         "where Person_ID = @Person_ID";
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\pezhe\Phonebook.mdf;Integrated Security=True;Connect Timeout=30";
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Person_ID", txtPersonID.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Firstname", txtFirstName.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Lastname", txtLastName.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Address", txtAddress.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@City", txtCity.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@State", txtState.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Zipcode", txtZipCode.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@CellPhone", txtHomePhone.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@WorkPhone", txtWorkPhone.Text.Trim(), DbType.String, ParameterDirection.Input);
                    parameters.Add("@Notes", richTextBox1.Text.Trim(), DbType.String, ParameterDirection.Input);

                    rowsAffected = db.Execute(sqlString, parameters);
                    MessageBox.Show($"{rowsAffected} record has been updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            try
            {
                string sqlString = "select * from Phonebook where Lastname = @Lastname";
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=c:\users\pezhe\Phonebook.mdf;Integrated Security=True;Connect Timeout=30";
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Lastname", toolStripTextBox1.Text.Trim(), DbType.String, ParameterDirection.Input);
                    List<Contact> myContactList = db.Query<Contact>(sqlString, parameters).ToList();
                    if (myContactList.Count == 0)
                    {
                        MessageBox.Show($"No records found for {toolStripTextBox1.Text.Trim()}.");
                        return;
                    }
                    //ContactList = db.Query<Contact>(sqlString).ToList();
                    dataGridView1.DataSource = myContactList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtPersonID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtFirstName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtLastName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtAddress.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtCity.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtState.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txtZipCode.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                txtHomePhone.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                txtWorkPhone.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                if(dataGridView1.SelectedRows[0].Cells[9].Value != null)
                {
                    richTextBox1.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                }
                else
                {
                    richTextBox1.Text = "";
                }

            }
        }
    }
}
