using Project_1C_;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // Added as requested
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // runs when the form first opens
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please enter username and password!");
                return;
            }

            // Temporary debug code
            using (var connTemp = Database.GetConnection())
            {
                connTemp.Open();
                using (var cmdTemp = new SQLiteCommand("PRAGMA table_info(users)", connTemp))
                using (var readerTemp = cmdTemp.ExecuteReader())
                {
                    string msg = "";
                    while (readerTemp.Read())
                    {
                        msg += readerTemp["name"].ToString() + "";
                    }
                    MessageBox.Show("Columns:" + msg);
                }
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM users WHERE username=@u AND password=@p";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    // Force trimming before checking login
                    cmd.Parameters.AddWithValue("@u", user.Trim());
                    cmd.Parameters.AddWithValue("@p", pass.Trim());

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("Login Successful!");
                            this.Hide();
                            new MainForm().Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password!");
                        }
                    }
                }
            }
        }
    }
}

