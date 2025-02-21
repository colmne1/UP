using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            ClassModules.Students Id_student_temp;
            Id_student_temp = ClassConnection.Connection.Students.Find(x => x.StudentID == Convert.ToInt32(((ComboBoxItem)student.SelectedItem).Tag));
            int id = Login.Login.connection.SetLastId(ClassConnection.Connection.Tables.Statuses_OVZ);
            if (ovz.Prikaz == null) 
            {
                string query = $"Insert Into Statuses_OVZ ([OVZStatusID], [StudentID], [Prikaz], [StartDate], [EndDate], [Note]) Values ({id.ToString()}, '{Id_student_temp.StudentID.ToString()}', '{prikaz.Text}', '{nachStat.Text}', '{konStat.Text}', '{primech.Text}')";
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
                string query = $"Update Statuses_Invalid [StudentID] = N'{Id_student_temp.StudentID.ToString()}', [Prikaz] = N'{prikaz.Text}', [StartDate] = N'{nachStat.Text}', [EndDate] = N'{konStat.Text}', [Note] = N'{primech.Text}' Where [MeetingID] = {ovz.OVZStatusID}";
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
                string query = "Delete Statuses_OVZ Where [OVZStatusID] = " + ovz.OVZStatusID.ToString() + "";
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
        private void TextBox_Data(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^\d{4}(-)\d{2}\1\d{2}$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
