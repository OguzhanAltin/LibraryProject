using LibraryProject.DAL.ORM.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryProject.View.Shared.LoginView
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        ProjectContext db = new ProjectContext();

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (db.AppUsers.Any(x=> x.UserName==txtUserName.Text && x.Password==txtPassword.Text &&
            x.Status==DAL.ORM.Enum.Status.Active && x.Role==DAL.ORM.Enum.Role.Admin))
            {

            }
            else if (db.AppUsers.Any(y=> y.UserName==txtUserName.Text && y.Password==txtPassword.Text &&
            y.Status==DAL.ORM.Enum.Status.Active && y.Role==DAL.ORM.Enum.Role.Member))
            {

            }
            else
            {
                MessageBox.Show("You have entered username or password incorrectly.");
            }
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }
    }
}
