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
    /// Логика взаимодействия для SocialScholarships.xaml
    /// </summary>
    public partial class SocialScholarships : Page
    {
        ClassModules.SocialScholarships social;
        public SocialScholarships(ClassModules.SocialScholarships _social)
        {
            InitializeComponent();
            social = _social;
            if (_social.DocumentReason != null)
            {
                docOsn.Text = _social.DocumentReason;
                nachVipl.Text = _social.StartDate.ToString();
                konVipl.Text = _social.EndDate.ToString();
            }
        }

        private void Click_SocialScholarships_Redact(object sender, RoutedEventArgs e)
        {
            int id = Login.Login.connection.SetLastId(ClassConnection.Connection.Tables.SocialScholarships);
            if (social.DocumentReason == null)
            {
                string query = $"Insert Into Departments ([DepartmentID], [DepartmentName]) Values ({id.ToString()}, '{DepName.Text}')";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.SocialScholarships);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.SocialScholarships);
                }
                else MessageBox.Show("Запрос на добавление цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string query = $"Update сeh Departments [DepartmentName] = N'{DepName.Text}' Where [DepartmentID] = {obshaga.DormitoryID}";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.SocialScholarships);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.SocialScholarships);
                }
                else MessageBox.Show("Запрос на изменение цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel_SocialScholarships_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
        }

        private void Click_Remove_SocialScholarships_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login.Login.connection.LoadData(ClassConnection.Connection.Tables.SocialScholarships);
                string query = "Delete Departments Where [DepartmentID] = " + obshaga.DormitoryID.ToString() + "";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.SocialScholarships);
                    Main.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.SocialScholarships);
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
