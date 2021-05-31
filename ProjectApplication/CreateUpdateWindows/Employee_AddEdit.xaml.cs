﻿using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectApplication
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class Employee_AddEdit : Window
    {

        public Employee SelectedEmployee { get; set; }

        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

        // Add employee
        public Employee_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Employee> employees)
        {
            Ctx = ctx;
            Employees = employees;
            InitializeComponent();

        }

        public Employee_AddEdit(ProjectApplicationContext ctx, Employee selectedEmployee, ObservableCollection<Employee> employees)
        {
            Ctx = ctx;
            Employees = employees;
            SelectedEmployee = selectedEmployee;
            this.DataContext = selectedEmployee;
            InitializeComponent();

           
          /*  tbEmployeeId.Text = selectedEmployee.EmployeeId.ToString();
           
            tbUserAccount.Text = selectedEmployee.UserAccount.UserName;
            
            tbSalary.Text = selectedEmployee.Salary.ToString();*/
      
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedEmployee == null)
            {
                Employee newEmployee = new Employee()
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    DOB = (DateTime)dpDOB.SelectedDate,
                    PhoneNumber = tbPhoneNumber.Text,
                    Street = tbStreet.Text,
                    PostCode = tbStreet.Text,
                    Number = tbNumber.Text,
                    Email = tbNumber.Text,
                    City = tbNumber.Text,
                    StartDate = (DateTime)dpStartDate.SelectedDate,
                    Salary = Convert.ToDouble(tbSalary.Text),
                    Role = (Role)cbRole.SelectedItem

                };
                Ctx.Employees.Add(newEmployee);
                Ctx.SaveChanges();
            }
            else
            {

                SelectedEmployee.FirstName = tbFirstName.Text;
                SelectedEmployee.LastName = tbLastName.Text;
                SelectedEmployee.DOB = (DateTime)dpDOB.SelectedDate;
                SelectedEmployee.PhoneNumber = tbPhoneNumber.Text;
                SelectedEmployee.Street = tbStreet.Text;
                SelectedEmployee.PostCode = tbStreet.Text;
                SelectedEmployee.Number = tbNumber.Text;
                SelectedEmployee.Email = tbNumber.Text;
                SelectedEmployee.City = tbNumber.Text;
                SelectedEmployee.StartDate = (DateTime)dpStartDate.SelectedDate;
                SelectedEmployee.Salary = Convert.ToDouble(tbSalary.Text);
                SelectedEmployee.Role = (Role)cbRole.SelectedItem;


                CollectionViewSource.GetDefaultView(Employees).Refresh();
                Ctx.SaveChanges();

            }

        }
    }
}
