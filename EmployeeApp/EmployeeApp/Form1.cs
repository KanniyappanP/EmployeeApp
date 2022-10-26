using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeApp
{
    public partial class Form1 : Form
    {
        int id = 0;

        public List<Employee> Employees { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.Employees = new List<Employee>();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AllowUserToAddRows = false;
            this.empName.DataPropertyName = "Name";
            this.empAddress.DataPropertyName = "Address";
            this.empLocation.DataPropertyName = "Location";
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.ID = id + 1;
            employee.Name = empNameTextBox.Text;
            employee.Address = empAddressTextBox?.Text;
            employee.Location = empLocationTextBox.Text;
            this.Employees.Add(employee);
            DisplayDataInGrid();
            id = id + 1;
            ResetValues();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var employee = this.dataGridView1.SelectedRows[0]?.DataBoundItem as Employee;
            if (employee != null)
            {
                this.ResetValues();
                this.empNameTextBox.Text = employee.Name;
                this.empAddressTextBox.Text = employee.Address;
                this.empLocationTextBox.Text = employee.Location;
                this.btnSave.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.Employees != null && this.Employees.Count > 0)
            {
                var selectedData = this.dataGridView1.SelectedRows[0].DataBoundItem;
                this.Employees.Remove(selectedData as Employee);
                this.DisplayDataInGrid();
                MessageBox.Show("Deleted the selected employee detail successfully");
            }
            else
            {
                MessageBox.Show("No records found to perform delete operation!!!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            var employee = this.dataGridView1.SelectedRows[0].DataBoundItem as Employee;
            if (employee != null && this.Employees != null && this.Employees.Count > 0)
            {
                Employee updateEmpData = this.Employees.Find(x => x.ID == employee.ID);
                var selectedIndex = this.Employees.IndexOf(employee);
                if (updateEmpData.Name != empNameTextBox.Name)
                {
                    updateEmpData.Name = this.empNameTextBox.Text;
                }
                if (updateEmpData.Address != empAddressTextBox.Text)
                {
                    updateEmpData.Address = this.empAddressTextBox.Text;
                }
                if (updateEmpData.Location != empLocationTextBox.Text)
                {
                    updateEmpData.Location = this.empLocationTextBox.Text;
                }

                this.DisplayDataInGrid();
                this.ResetValues();
                MessageBox.Show("Updated the employee details successfully");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ResetValues();
        }

        private void DisplayDataInGrid()
        {
            var bindingList = new BindingList<Employee>(this.Employees);
            var source = new BindingSource(bindingList, null);
            this.dataGridView1.DataSource = source;
        }

        private void ResetValues()
        {
            this.empNameTextBox.Text = string.Empty;
            this.empAddressTextBox.Text = string.Empty;
            this.empLocationTextBox.Text = string.Empty;
            this.btnSave.Enabled = true;
        }  
    }

    public class Employee
    {
        public int ID { get; set; }
         
        public string Name { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }
    }
}
