using MVC_Template.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MVC_Template.Controller;

namespace MVC_Template.View
{
    public partial class frmMain : Form, AppInterface
    {
        EmployeeController controller = null;

        public frmMain()
        {
            InitializeComponent();
            controller = new EmployeeController(this);
        }

        public string FirstName
        {
            get
            {
                return txtFirstName.Text;
            }

            set
            {
                txtFirstName.Text = value;
            }
        }

        public string Gender
        {
            get
            {
                return cboGender.Text;
            }

            set
            {
                cboGender.Text = value;
            }
        }

        public int ID
        {
            get
            {
                string _id = lblID.Text;
                int result = 0;

                if (int.TryParse(_id, out result))
                    result = Convert.ToInt32(_id);
                else
                    result = 0;

                return result;
            }

            set
            {
                lblID.Text = value.ToString();
            }
        }

        public string LastName
        {
            get
            {
                return txtLastName.Text;
            }

            set
            {
                txtLastName.Text = value;
            }
        }

        public string MiddleName
        {
            get
            {
                return txtMiddleName.Text;
            }

            set
            {
                txtMiddleName.Text = value;
            }
        }

        public bool Status
        {
            get
            {
                return chkStatus.Checked;
            }

            set
            {
                chkStatus.Checked = value;
            }
        }

        public void SetController(EmployeeController controller)
        {
            this.controller = controller;
        }

        public string SearchKey
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            cboGender.SelectedIndex = -1;
            chkStatus.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var confirm = MessageBox.Show(this, "Are you sure you want to save data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (confirm == DialogResult.Yes)
                {
                    if (controller.Save() > 0)
                    {
                        MessageBox.Show(this, "Successfully save", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                    else
                        MessageBox.Show(this, "Failed to save employee", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(this, er.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Employee emp = new Employee();
                emp = controller.Find();

                if(emp.ID > 0)
                {
                    lblID.Text = emp.ID.ToString();
                    txtLastName.Text = emp.LastName;
                    txtFirstName.Text = emp.FirstName;
                    txtMiddleName.Text = emp.MiddleName;
                    cboGender.Text = emp.Gender;
                    chkStatus.Checked = emp.Status;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(this, er.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var confirm = MessageBox.Show(this, $"Are you sure you want to update data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (confirm == DialogResult.Yes)
                {
                    if (controller.Update() > 0)
                    {
                        MessageBox.Show(this, "Record successfully updated.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                    else
                        MessageBox.Show(this, "Failed to update employee", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(this, er.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
