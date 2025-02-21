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
            ClassModules.Students Id_student_temp;
            Id_student_temp = ClassConnection.Connection.Students.Find(x => x.StudentID == Convert.ToInt32(((ComboBoxItem)student.SelectedItem).Tag));
            int id = Login.Login.connection.SetLastId(ClassConnection.Connection.Tables.Statuses_Invalid);
            if (invalid.OrderNumber == null)
            {
                string query = $"Insert Into Statuses_Invalid ([DisabilityStatusID], [StudentID], [OrderNumber], [StartDate], [EndDate], [DisabilityType], [Note]) Values ({id.ToString()}, '{Id_student_temp.StudentID.ToString()}', '{prikaz.Text}', '{nachStat.Text}', '{konStat.Text}', '{vidInvalid.Text}', '{primech.Text}')";
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
                string query = $"Update Statuses_Invalid [StudentID] = N'{Id_student_temp.StudentID.ToString()}', [OrderNumber] = N'{prikaz.Text}', [StartDate] = N'{nachStat.Text}', [EndDate] = N'{konStat.Text}', [DisabilityType] = N'{konStat.Text}', [Note] = N'{primech.Text}' Where [MeetingID] = {invalid.DisabilityStatusID}";
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
                string query = "Delete Statuses_Invalid Where [DisabilityStatusID] = " + invalid.DisabilityStatusID.ToString() + "";
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