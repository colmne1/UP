using System;
using System.Collections.Generic;
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
using ClassConnection;

namespace UP.Pages.PagesInTable
{
    /// <summary>
    /// Логика взаимодействия для Statuses_Invalid.xaml
    /// </summary>
    public partial class Statuses_OVZ : Page
    {
        ClassModules.Statuses_OVZ ovz;
        public Statuses_OVZ(ClassModules.Statuses_OVZ _ovz)
        {
            InitializeComponent();
            ovz = _ovz;
            if (_ovz.Prikaz != null)
            {
                prikaz.Text = _ovz.Prikaz;
                primech.Text = _ovz.Note;
                nachStat.Text = _ovz.StartDate.ToString();
                konStat.Text = _ovz.EndDate.ToString();
            }
        }

        private void Click_Statuses_OVZ_Redact(object sender, RoutedEventArgs e)
        {
            int id = Login.Login.connection.SetLastId(ClassConnection.Connection.Tables.Statuses_OVZ);
            if (invalid.OrderNumber == null)
            {
                string query = $"Insert Into Departments ([DepartmentID], [DepartmentName]) Values ({id.ToString()}, '{DepName.Text}')";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_OVZ);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_OVZ);
                }
                else MessageBox.Show("Запрос на добавление цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string query = $"Update сeh Departments [DepartmentName] = N'{DepName.Text}' Where [DepartmentID] = {obshaga.DormitoryID}";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_OVZ);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_OVZ);
                }
                else MessageBox.Show("Запрос на изменение цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel_Statuses_OVZ_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
        }

        private void Click_Remove_Statuses_OVZ_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_OVZ);
                string query = "Delete Departments Where [DepartmentID] = " + obshaga.DormitoryID.ToString() + "";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_OVZ);
                    Main.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_OVZ);
                }
                else MessageBox.Show("Запрос на удаление цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
