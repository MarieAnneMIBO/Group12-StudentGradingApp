using Project_1C_;
using System;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Database.Initialize();

            Application.Run(new LoginForm());
        }
    }
}
