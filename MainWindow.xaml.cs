using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapterEmpl;
        SqlDataAdapter adapterDep;
        DataTable dtEmployees;
        DataTable dtDepartments;

        public MainWindow()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connection = new SqlConnection(connectionString);
            adapterEmpl = new SqlDataAdapter("SELECT * FROM Employees", connection);
            var builderEmpl = new SqlCommandBuilder(adapterEmpl);
            adapterEmpl.InsertCommand = builderEmpl.GetInsertCommand();
            adapterEmpl.UpdateCommand = builderEmpl.GetUpdateCommand();
            adapterEmpl.DeleteCommand = builderEmpl.GetDeleteCommand();
            dtEmployees = new DataTable();
            adapterEmpl.Fill(dtEmployees);

            adapterDep = new SqlDataAdapter("SELECT * FROM Departments", connection);
            var builderDep = new SqlCommandBuilder(adapterDep);
            adapterDep.InsertCommand = builderDep.GetInsertCommand();
            adapterDep.UpdateCommand = builderDep.GetUpdateCommand();
            adapterDep.DeleteCommand = builderDep.GetDeleteCommand();
            dtDepartments = new DataTable();
            adapterDep.Fill(dtDepartments);



            InitializeComponent();

            dgEmpl.DataContext = dtEmployees;
            dgEmpl.ItemsSource = dtEmployees.DefaultView;
            DataGridComboBoxColumn dgvCmb = new DataGridComboBoxColumn();
            dgvCmb.ItemsSource = dtDepartments.DefaultView;
            dgvCmb.DisplayMemberPath = "Name";
            dgvCmb.SelectedValuePath = "ID";
            //dgvCmb.SelectedItemBinding = new Binding("DepartmentID");
           // dgvCmb.TextBinding = new Binding("DepartmentID");
           
            


            dgvCmb.Width = 90;
            dgvCmb.Header = "Department";
            dgEmpl.Columns.Add(dgvCmb);

            dgDep.DataContext = dtDepartments;
            dgDep.ItemsSource = dtDepartments.DefaultView;

        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            adapterEmpl.Update(dtEmployees);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            adapterDep.Update(dtDepartments);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)dgEmpl.SelectedItem;

            newRow.Row.Delete();
            adapterEmpl.Update(dtEmployees);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)dgDep.SelectedItem;

            newRow.Row.Delete();
            adapterDep.Update(dtDepartments);
        }
    }
}
