using Project_1C_;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string user = txtRegUsername.Text.Trim();
            string pass = txtRegPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO users (username, password) VALUES (@u, @p)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    // Force trimming before saving
                    cmd.Parameters.AddWithValue("@u", user.Trim());
                    cmd.Parameters.AddWithValue("@p", pass.Trim());

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account created!");

                        new LoginForm().Show();
                        this.Hide();
                    }
                    catch (SQLiteException ex)
                    {
                        if (ex.ErrorCode == (int)SQLiteErrorCode.Constraint)
                            MessageBox.Show("Username already exists!");
                        else
                            MessageBox.Show("Database error: " + ex.Message);
                    }
                }
            }
        }
    }
}

