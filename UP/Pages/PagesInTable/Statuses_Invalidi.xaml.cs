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
    /// Логика взаимодействия для Statuses_Invalidi.xaml
    /// </summary>
    public partial class Statuses_Invalidi : Page
    {
        ClassModules.Statuses_Invalid invalid;
        public Statuses_Invalidi(ClassModules.Statuses_Invalid _invalid)
        {
            InitializeComponent();
            invalid = _invalid;
            if (_invalid.OrderNumber != null)
            {
                prikaz.Text = _invalid.OrderNumber;
                primech.Text = _invalid.StartDate.ToString();
                vidInvalid.Text = _invalid.EndDate.ToString();
                nachStat.Text = _invalid.DisabilityType;
                konStat.Text = _invalid.Note;
            }
        }

        private void Click_Statuses_Invalidi_Redact(object sender, RoutedEventArgs e)
        {
            int id = Login.Login.connection.SetLastId(ClassConnection.Connection.Tables.Statuses_Invalid);
            if (invalid.OrderNumber == null)
            {
                string query = $"Insert Into Departments ([DepartmentID], [DepartmentName]) Values ({id.ToString()}, '{DepName.Text}')";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_Invalid);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_Invalid);
                }
                else MessageBox.Show("Запрос на добавление цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string query = $"Update сeh Departments [DepartmentName] = N'{DepName.Text}' Where [DepartmentID] = {obshaga.DormitoryID}";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_Invalid);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_Invalid);
                }
                else MessageBox.Show("Запрос на изменение цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel_Statuses_Invalidi_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
        }

        private void Click_Remove_Statuses_Invalidi_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_Invalid);
                string query = "Delete Departments Where [DepartmentID] = " + obshaga.DormitoryID.ToString() + "";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_Invalid);
                    Main.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_Invalid);
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