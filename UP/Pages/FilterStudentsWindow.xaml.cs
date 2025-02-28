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
using System.Windows.Shapes;
using ClassConnection;
using ClassModules;

namespace UP.Pages
{
    /// <summary>
    /// Логика взаимодействия для FilterStudentsWindow.xaml
    /// </summary>
    public partial class FilterStudentsWindow : Window
    {
        private List<Students> originalStudentList;
        public List<Students> FilteredStudents { get; set; }


        public FilterStudentsWindow(List<Students> students)
        {
            InitializeComponent();

            originalStudentList = students;
            FilteredStudents = new List<Students>(students);

            // Заполнение ComboBox данными об отделении
            OtdelenieComboBox.Items.Add(new ComboBoxItem { Content = "Не выбрано" });
            foreach (var department in Connection.Departments)
            {
                OtdelenieComboBox.Items.Add(new ComboBoxItem { Content = department.DepartmentName });
            }

            // Заполнение ComboBox данными о комнатах
            KomnataComboBox.Items.Add(new ComboBoxItem { Content = "Не выбрано" });
            foreach (var obshaga in ClassConnection.Connection.Obshagas)
            {
                KomnataComboBox.Items.Add(new ComboBoxItem { Content = obshaga.RoomNumber });
            }
        }

        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            FilteredStudents = new List<Students>(originalStudentList); // Start with a fresh copy

            // Apply filters based on UI selections
            ApplyHostelFilter();
            ApplyOrphansFilter();
            ApplySPPPFilter();
            ApplyDisabilityFilter();
            ApplySocialScholarshipFilter();
            ApplyRiskGroupFilter();
            ApplySurnameFilter();
            ApplyMajorityFilter();
            ApplyGroupFilter();
            ApplyVziskanieFilter();
            ApplyOVZFilter();
            ApplyUchachiesyaFilter();
            ApplyOtdelenieFilter();
            ApplyKomnataFilter();
            ApplyPolFilter();

            //Set result
            DialogResult = true;
            Close();

        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            // Reset UI elements to their default states
            HostelComboBox.SelectedIndex = 0;
            OrphansComboBox.SelectedIndex = 0;
            SPPPCheckBox.IsChecked = false;
            DisabilityComboBox.SelectedIndex = 0;
            SocialScholarshipComboBox.SelectedIndex = 0;
            COPComboBox.SelectedIndex = 0;
            RiskGroupComboBox.SelectedIndex = 0;
            SurnameTextBox.Text = "";
            MajorityComboBox.SelectedIndex = 0;
            GroupTextBox.Text = "";
            VziskanieCheckBox.IsChecked = false;
            OVZComboBox.SelectedIndex = 0;
            UchaschiesaComboBox.SelectedIndex = 0;
            OtdelenieComboBox.SelectedIndex = 0;
            KomnataComboBox.SelectedIndex = 0;
            PolComboBox.SelectedIndex = 0;

            FilteredStudents = new List<Students>(originalStudentList);
        }

        private void ApplyHostelFilter()
        {
            if (HostelComboBox.SelectedIndex == 1) // Yes
            {
                FilteredStudents = (from student in FilteredStudents
                                    join obshaga in Connection.Obshagas
                                    on student.StudentID equals obshaga.StudentID
                                    select student).ToList();
            }
            else if (HostelComboBox.SelectedIndex == 2) // No
            {
                FilteredStudents = (from student in FilteredStudents
                                    where !Connection.Obshagas.Any(obshaga => obshaga.StudentID == student.StudentID)
                                    select student).ToList();
            }
        }

        private void ApplyOrphansFilter()
        {
            if (OrphansComboBox.SelectedIndex == 1) // Yes
            {
                FilteredStudents = (from student in FilteredStudents
                                    join sirots in Connection.StatusesSirots
                                    on student.StudentID equals sirots.StudentID
                                    select student).ToList();
            }
            else if (OrphansComboBox.SelectedIndex == 2) // No
            {
                FilteredStudents = (from student in FilteredStudents
                                    where !Connection.StatusesSirots.Any(sirots => sirots.StudentID == student.StudentID)
                                    select student).ToList();
            }
        }

        private void ApplySPPPFilter()
        {
            if (SPPPCheckBox.IsChecked == true)
            {
                FilteredStudents = (from student in FilteredStudents
                                    join sppp in Connection.SpppMeetings
                                    on student.StudentID equals sppp.StudentID
                                    select student).ToList();
            }
        }

        private void ApplyDisabilityFilter()
        {
            if (DisabilityComboBox.SelectedIndex == 1) // Yes
            {
                FilteredStudents = (from student in FilteredStudents
                                    join invalid in Connection.StatusesInvalids
                                    on student.StudentID equals invalid.StudentID
                                    select student).ToList();
            }
            else if (DisabilityComboBox.SelectedIndex == 2) // No
            {
                FilteredStudents = (from student in FilteredStudents
                                    where !Connection.StatusesInvalids.Any(invalid => invalid.StudentID == student.StudentID)
                                    select student).ToList();
            }
        }

        private void ApplySocialScholarshipFilter()
        {
            if (SocialScholarshipComboBox.SelectedIndex == 1) // Yes
            {
                FilteredStudents = (from student in FilteredStudents
                                    join scholarship in Connection.SocialScholarships
                                    on student.StudentID equals scholarship.StudentID
                                    select student).ToList();
            }
            else if (SocialScholarshipComboBox.SelectedIndex == 2) // No
            {
                FilteredStudents = (from student in FilteredStudents
                                    where !Connection.SocialScholarships.Any(scholarship => scholarship.StudentID == student.StudentID)
                                    select student).ToList();
            }
        }

        private void ApplyRiskGroupFilter()
        {
            if (RiskGroupComboBox.SelectedIndex == 1) // Yes
            {
                FilteredStudents = (from student in FilteredStudents
                                    join riskGroup in Connection.StatusesRiskGroups
                                    on student.StudentID equals riskGroup.StudentID
                                    select student).ToList();
            }
            else if (RiskGroupComboBox.SelectedIndex == 2) // No
            {
                FilteredStudents = (from student in FilteredStudents
                                    where !Connection.StatusesRiskGroups.Any(riskGroup => riskGroup.StudentID == student.StudentID)
                                    select student).ToList();
            }
        }
        private void ApplySurnameFilter()
        {
            if (!string.IsNullOrEmpty(SurnameTextBox.Text))
            {
                FilteredStudents = FilteredStudents.Where(s => s.LastName != null && s.LastName.Contains(SurnameTextBox.Text)).ToList(); // Null check
            }
        }

        private void ApplyMajorityFilter()
        {
            DateTime majorityDate = DateTime.Now.AddYears(-18);

            if (MajorityComboBox.SelectedIndex == 1) // Yes
            {
                FilteredStudents = FilteredStudents.Where(s => s.BirthDate <= majorityDate).ToList();
            }
            else if (MajorityComboBox.SelectedIndex == 2) // No
            {
                FilteredStudents = FilteredStudents.Where(s => s.BirthDate > majorityDate).ToList();
            }
        }

        private void ApplyGroupFilter()
        {
            if (!string.IsNullOrEmpty(GroupTextBox.Text))
            {
                FilteredStudents = FilteredStudents.Where(s => s.Groups != null && s.Groups.Contains(GroupTextBox.Text)).ToList();
            }
        }

        private void ApplyVziskanieFilter()
        {
            if (VziskanieCheckBox.IsChecked == true)
            {
                FilteredStudents = FilteredStudents.Where(s => s.Vziskanie != null).ToList();
            }
        }

        private void ApplyOVZFilter()
        {
            if (OVZComboBox.SelectedIndex == 1) // Yes
            {
                FilteredStudents = (from student in FilteredStudents
                                    join ovz in Connection.StatusesOvzs
                                    on student.StudentID equals ovz.StudentID
                                    select student).ToList();
            }
            else if (OVZComboBox.SelectedIndex == 2) // No
            {
                FilteredStudents = (from student in FilteredStudents
                                    where !Connection.StatusesOvzs.Any(ovz => ovz.StudentID == student.StudentID)
                                    select student).ToList();
            }
        }

        private void ApplyUchachiesyaFilter()
        {
            //I couldn't figure out what data you must compare with, so i had to remove this one.
        }

        private void ApplyOtdelenieFilter()
        {
            if (OtdelenieComboBox.SelectedIndex > 0)
            {
                string selectedDepartmentName = ((ComboBoxItem)OtdelenieComboBox.SelectedItem).Content.ToString();
                FilteredStudents = FilteredStudents.Where(s => s != null && s.Otdelenie == ClassConnection.Connection.Departments.FirstOrDefault(x => x.DepartmentName == selectedDepartmentName)?.DepartmentID).ToList();
            }
        }

        private void ApplyKomnataFilter()
        {
            if (KomnataComboBox.SelectedIndex > 0)
            {
                int selectedKomnata = Convert.ToInt32(((ComboBoxItem)KomnataComboBox.SelectedItem).Content);
                FilteredStudents = (from student in FilteredStudents
                                    join obshaga in Connection.Obshagas
                                    on student.StudentID equals obshaga.StudentID
                                    where obshaga.RoomNumber == selectedKomnata
                                    select student).ToList();  //
            }
        }

        private void ApplyPolFilter()
        {
            if (PolComboBox.SelectedIndex > 0)
            {
                string selectedPol = ((ComboBoxItem)PolComboBox.SelectedItem).Content.ToString();
                FilteredStudents = FilteredStudents.Where(s => s.Gender == selectedPol).ToList();  //
            }
        }
    }
}