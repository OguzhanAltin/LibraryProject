using LibraryProject.DAL.ORM.Context;
using LibraryProject.DAL.ORM.Entity;
using LibraryProject.DAL.ORM.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryProject.View.AdminArea.AppUserView
{
    public partial class AppUserPage : Form
    {
        public AppUserPage()
        {
            InitializeComponent();
        }

        ProjectContext db = new ProjectContext();

        public void TextBoxCleaner()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in groupBox4.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }

       public void AppUserTakeList()
        {
            dataGridView1.DataSource = db.AppUsers.Where
                (x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Updated).ToList();
        }

        public void EnumList()
        {
            cmbUserAddRole.Items.AddRange(Enum.GetNames(typeof(Role)));
            cmbUserAddRole.SelectedIndex = 1;

            cmbUserAddGender.Items.AddRange(Enum.GetNames(typeof(Gender)));
            cmbUserAddGender.SelectedIndex = 1;

            cmbUpdateRole.Items.AddRange(Enum.GetNames(typeof(Role)));
            cmbUpdateRole.SelectedIndex = 1;

            cmbGenderUpdate.Items.AddRange(Enum.GetNames(typeof(Gender)));
            cmbGenderUpdate.SelectedIndex = 1;
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            AppUser user = new AppUser();

            user.FirstName = txtFirstNameAdd.Text;
            user.LastName = txtLastNameAdd.Text;
            user.Email = txtEmailAdd.Text;
            user.UserName = txtUserNameAdd.Text;
            user.Password = txtPasswordAdd.Text;
            user.Role = (Role)Enum.Parse(typeof(Role), cmbUserAddRole.Text);
            user.Gender = (Gender)Enum.Parse(typeof(Role), cmbUserAddGender.Text);

            db.AppUsers.Add(user);
            db.SaveChanges();

            AppUserTakeList();
            TextBoxCleaner();
        }

        private void AppUserPage_Load(object sender, EventArgs e)
        {
            AppUserTakeList();
            EnumList();
        }

        int id;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFirstNameUpdate.Text = dataGridView1.CurrentRow.Cells["FirstName"].Value.ToString();
            txtLastNameUpdate.Text = dataGridView1.CurrentRow.Cells["LastName"].Value.ToString();
            txtEmailUpdate.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
            txtUserNameUpdate.Text = dataGridView1.CurrentRow.Cells["UserName"].Value.ToString();
            txtPasswordUpdate.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
            id = int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            txtUserDelete.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AppUser user = db.AppUsers.FirstOrDefault(x => x.ID == id);
            user.FirstName = txtFirstNameUpdate.Text;
            user.LastName = txtLastNameUpdate.Text;
            user.Email = txtEmailUpdate.Text;
            user.UserName = txtUserNameUpdate.Text;
            user.Password = txtPasswordUpdate.Text;
            user.Status = DAL.ORM.Enum.Status.Updated;

            db.SaveChanges();

            AppUserTakeList();
            TextBoxCleaner();
            
            
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            AppUser user = db.AppUsers.FirstOrDefault(x => x.ID == id);
            user.Status = DAL.ORM.Enum.Status.Deleted;

            db.SaveChanges();

            AppUserTakeList();
            TextBoxCleaner();
        }
    }
}
