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
    /// Логика взаимодействия для Statuses_RiskGroup.xaml
    /// </summary>
    public partial class Statuses_RiskGroup : Page
    {
        ClassModules.Statuses_RiskGroup risk;
        public Statuses_RiskGroup(ClassModules.Statuses_RiskGroup _risk)
        {
            InitializeComponent();
            risk = _risk;
            if (_risk.RiskGroupType != null)
            {
                tip.Text = _risk.RiskGroupType;
                nachUch.Text = _risk.DateStart.ToString();
                konUch.Text = _risk.DateEnd.ToString();
                osnUch.Text = _risk.OsnPost;
                osnSnatUch.Text = _risk.OsnSnat;
                prichUch.Text = _risk.PrichinaPost;
                prichSnUch.Text = _risk.PrichinaSnat;
                primech.Text = _risk.Note;
            }
        }

        private void Click_Statuses_RiskGroup_Redact(object sender, RoutedEventArgs e)
        {
            int id = Login.Login.connection.SetLastId(ClassConnection.Connection.Tables.Statuses_RiskGroup);
            if (risk.RiskGroupType == null)
            {
                string query = $"Insert Into Departments ([DepartmentID], [DepartmentName]) Values ({id.ToString()}, '{DepName.Text}')";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_RiskGroup);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_RiskGroup);
                }
                else MessageBox.Show("Запрос на добавление цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string query = $"Update сeh Departments [DepartmentName] = N'{DepName.Text}' Where [DepartmentID] = {obshaga.DormitoryID}";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_RiskGroup);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_RiskGroup);
                }
                else MessageBox.Show("Запрос на изменение цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel_Statuses_RiskGroup_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
        }

        private void Click_Remove_Statuses_RiskGroup_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_RiskGroup);
                string query = "Delete Departments Where [DepartmentID] = " + obshaga.DormitoryID.ToString() + "";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Statuses_RiskGroup);
                    Main.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Statuses_RiskGroup);
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
