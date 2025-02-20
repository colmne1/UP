using ClassConnection;
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

namespace UP.Pages.PagesInTable
{
    /// <summary>
    /// Логика взаимодействия для Obshaga.xaml
    /// </summary>
    public partial class Obshaga : Page
    {
        ClassModules.Obshaga obshaga;
        public Obshaga(ClassModules.Obshaga _obshaga)
        {
            InitializeComponent();
            obshaga = _obshaga;
            foreach (var item in Connection.Rooms)
            {
                ComboBoxItem cb_room = new ComboBoxItem();
                cb_room.Tag = item.RoomID;
                cb_room.Content = "Вид техники: " + item.RoomName;
                RentStart.Text = _obshaga.CheckInDate.ToString();
                RentOut.Text = _obshaga.CheckOutDate.ToString();
                primech.Text = _obshaga.Note;
                room.Items.Add(cb_room);
            }
        }

        private void Click_Obshaga_Redact(object sender, RoutedEventArgs e)
        {
            int id = Login.Login.connection.SetLastId(ClassConnection.Connection.Tables.Departments);
            if (obshaga.CheckInDate == null)
            {
                string query = $"Insert Into Departments ([DepartmentID], [DepartmentName]) Values ({id.ToString()}, '{DepName.Text}')";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Departments);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Obshaga);
                }
                else MessageBox.Show("Запрос на добавление цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string query = $"Update сeh Departments [DepartmentName] = N'{DepName.Text}' Where [DepartmentID] = {obshaga.DormitoryID}";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Departments);
                    MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Obshaga);
                }
                else MessageBox.Show("Запрос на изменение цеха не был обработан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Click_Cancel_Obshaga_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
        }

        private void Click_Remove_Obshaga_Redact(object sender, RoutedEventArgs e)
        {
            try
            {
                Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Departments);
                string query = "Delete Departments Where [DepartmentID] = " + obshaga.DormitoryID.ToString() + "";
                var query_apply = Login.Login.connection.ExecuteQuery(query);
                if (query_apply != null)
                {
                    Login.Login.connection.LoadData(ClassConnection.Connection.Tables.Departments);
                    Main.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main, null, null, Main.page_main.Obshaga);
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
