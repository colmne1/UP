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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace UP.Pages
{
    public partial class Main : Page
    {
        public enum page_main
        {
            Rooms,
            SocialScholarships,
            Statuses_RiskGroup,
            Statuses_Invalid,
            SPPP_Meetings,
            Students,
            Departments,
            Obshaga,
            Statuses_OVZ,
            Statuses_SVO,
            Statuses_Sirots,
            none
        }
        public static page_main page_select;
        public static Main main;
        public Main()
        {
            InitializeComponent();
            main = this;
            page_select = page_main.none;
        }
        public void CreateConnect(bool connectApply)
        {
            if (connectApply == true)
            {
                Login.connection.LoadData(Connection.Tables.Rooms);
                Login.connection.LoadData(Connection.Tables.SocialScholarships);
                Login.connection.LoadData(Connection.Tables.Statuses_RiskGroup);
                Login.connection.LoadData(Connection.Tables.Statuses_Invalid);
                Login.connection.LoadData(Connection.Tables.SPPP_Meetings);
                Login.connection.LoadData(Connection.Tables.Students);
                Login.connection.LoadData(Connection.Tables.Departments);
                Login.connection.LoadData(Connection.Tables.Obshaga);
                Login.connection.LoadData(Connection.Tables.Statuses_OVZ);
                Login.connection.LoadData(Connection.Tables.Statuses_SVO);
                Login.connection.LoadData(Connection.Tables.Statuses_Sirots);
            }
        }
        public void RoleUser()
        {
            WhoAmI.Content = $"Здравствуйте, {Login.UserInfo[0]}! Роль - {Login.UserInfo[1]}";
        }
        public void OpenPageLogin()
        {
            DoubleAnimation opgridAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.6)
            };
            opgridAnimation.Completed += delegate
            {
                MainWindow.init.frame.Navigate(new Login());
                DoubleAnimation opgrisdAnimation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1.2)
                };
                MainWindow.init.frame.BeginAnimation(Frame.OpacityProperty, opgrisdAnimation);
            };
            MainWindow.init.frame.BeginAnimation(Frame.OpacityProperty, opgridAnimation);
        }
        private void LoadRooms()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.Rooms)
                {
                    if (page_select == page_main.Rooms)
                    {
                        parrent.Children.Add(new Elements.Rooms_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Rooms && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Rooms(new ClassModules.Rooms());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Rooms(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Rooms;
            parrent.Children.Clear();
            LoadRooms();
        }

        private void LoadSocialScholarships()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.SocialScholarships)
                {
                    if (page_select == page_main.SocialScholarships)
                    {
                        parrent.Children.Add(new Elements.SocialScholarships_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.SocialScholarships && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.SocialScholarships(new ClassModules.SocialScholarships());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_SocialScholarships(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.SocialScholarships;
            parrent.Children.Clear();
            LoadSocialScholarships();
        }

        private void LoadStatuses_RiskGroup()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.StatusesRiskGroups)
                {
                    if (page_select == page_main.Statuses_RiskGroup)
                    {
                        parrent.Children.Add(new Elements.Statuses_RiskGroup_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Statuses_RiskGroup && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Statuses_RiskGroup(new ClassModules.Statuses_RiskGroup());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Statuses_RiskGroup(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Statuses_RiskGroup;
            parrent.Children.Clear();
            LoadStatuses_RiskGroup();
        }

        private void LoadStatuses_Invalid()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.StatusesInvalids)
                {
                    if (page_select == page_main.Statuses_Invalid)
                    {
                        parrent.Children.Add(new Elements.Statuses_Invalid_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Statuses_Invalid && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Statuses_Invalid(new ClassModules.Statuses_Invalid());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Statuses_Invalid(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Statuses_Invalid;
            parrent.Children.Clear();
            LoadStatuses_Invalid();
        }

        private void LoadSPPP_Meetings()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.SpppMeetings)
                {
                    if (page_select == page_main.SPPP_Meetings)
                    {
                        parrent.Children.Add(new Elements.SPPP_Meetings_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.SPPP_Meetings && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.SPPP_Meetings(new ClassModules.SPPP_Meetings());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_SPPP_Meetings(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.SPPP_Meetings;
            parrent.Children.Clear();
            LoadSPPP_Meetings();
        }
        private void LoadStudents()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.Students)
                {
                    if (page_select == page_main.Students)
                    {
                        parrent.Children.Add(new Elements.Students_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Students && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Students(new ClassModules.Students());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Students(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Students;
            parrent.Children.Clear();
            LoadStudents();
        }
        private void LoadDepartments()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.Departments)
                {
                    if (page_select == page_main.Departments)
                    {
                        parrent.Children.Add(new Elements.Departments_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Departments && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Departments(new ClassModules.Departments());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Departments(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Departments;
            parrent.Children.Clear();
            LoadDepartments();
        }

        private void LoadObshaga()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.Obshagas)
                {
                    if (page_select == page_main.Obshaga)
                    {
                        parrent.Children.Add(new Elements.Obshaga_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Obshaga && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Obshaga(new ClassModules.Obshaga());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Obshaga(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Obshaga;
            parrent.Children.Clear();
            LoadObshaga();
        }

        private void LoadStatuses_OVZ()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.StatusesOvzs)
                {
                    if (page_select == page_main.Statuses_OVZ)
                    {
                        parrent.Children.Add(new Elements.Statuses_OVZ_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Statuses_OVZ && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Statuses_OVZ(new ClassModules.Statuses_OVZ());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Statuses_OVZ(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Statuses_OVZ;
            parrent.Children.Clear();
            LoadStatuses_OVZ();
        }

        private void LoadStatuses_SVO()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.StatusesSvos)
                {
                    if (page_select == page_main.Statuses_SVO)
                    {
                        parrent.Children.Add(new Elements.Statuses_SVO_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Statuses_SVO && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Statuses_SVO(new ClassModules.Statuses_SVO());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Statuses_SVO(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Statuses_SVO;
            parrent.Children.Clear();
            LoadStatuses_SVO();
        }

        private void LoadStatuses_Sirots()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in ClassConnection.Connection.StatusesSirots)
                {
                    if (page_select == page_main.Statuses_Sirots)
                    {
                        parrent.Children.Add(new Elements.Statuses_Sirots_items(item));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.Statuses_Sirots && Login.UserInfo[1] == "admin")
                {
                    var add = new Pages.PagesInTable.Statuses_Sirots(new ClassModules.Statuses_Sirots());
                    parrent.Children.Add(new Elements.Add(add));
                }
            });
        }
        private void Click_Statuses_Sirots(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            page_select = page_main.Statuses_Sirots;
            parrent.Children.Clear();
            LoadStatuses_Sirots();
        }
        private bool isDataLoaded = false;
        private async void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Search.Text) && Search.Text != "Поиск")
            {
                await Task.Delay(100);
                if (page_select == page_main.Rooms)
                {
                    parrent.Children.Clear();
                    var rooms = Connection.Rooms.FindAll(x => x.RoomID.ToString() == Search.Text);
                    foreach (var itemSearch in rooms) parrent.Children.Add(new Elements.Rooms_items(itemSearch));
                }
                else if (page_select == page_main.SocialScholarships)
                {
                    parrent.Children.Clear();
                    var country = Connection.SocialScholarships.FindAll(x => x.oborud.Contains(Search.Text));
                    var countryIds = country.Select(c => c.ScholarshipID).ToList();
                    var locationsByCountry = Connection.SocialScholarships.Where(l => countryIds.Contains(l.ScholarshipID)).ToList();
                    foreach (var itemSearch in locationsByCountry) parrent.Children.Add(new Elements.SocialScholarships_items(itemSearch));
                }
                else if (page_select == page_main.Statuses_RiskGroup)
                {
                    parrent.Children.Clear();
                    var VoditelById = Connection.StatusesRiskGroups.FindAll(x => x.Id_voditel.ToString().Contains(Search.Text));
                    foreach (var itemSearch in VoditelById) parrent.Children.Add(new Elements.Voditel_items(itemSearch));
                }
                else if (page_select == page_main.Statuses_Invalid)
                {
                    parrent.Children.Clear();
                    var techniqueByName = Connection.StatusesInvalids.FindAll(x => x.Name_technique.Contains(Search.Text));
                    foreach (var itemSearch in techniqueByName) parrent.Children.Add(new Elements.Technique_items(itemSearch));
                }
                else if (page_select == page_main.SPPP_Meetings)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.SpppMeetings.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }
                else if (page_select == page_main.Students)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.Students.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }
                else if (page_select == page_main.Departments)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.Departments.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }
                else if (page_select == page_main.Obshaga)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.zapchast.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }
                else if (page_select == page_main.Statuses_OVZ)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.StatusesOvzs.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }
                else if (page_select == page_main.Statuses_SVO)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.StatusesSvos.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }
                else if (page_select == page_main.Statuses_Sirots)
                {
                    parrent.Children.Clear();
                    var typeOfTroopByName = Connection.StatusesSirots.FindAll(x => x.Name_zapchast.Contains(Search.Text));
                    foreach (var itemSearch in typeOfTroopByName) parrent.Children.Add(new Elements.Zapchast_items(itemSearch));
                }

            }
            else
            {
                await Task.Delay(100);
                if (string.IsNullOrWhiteSpace(Search.Text))
                {
                    parrent.Children.Clear();
                    return;
                }
                if (!isDataLoaded || Search.Text == "Поиск")
                {
                    if (page_select == page_main.Garage)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadGarages();
                    }
                    else if (page_select == page_main.ceh)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadCeh();
                    }
                    else if (page_select == page_main.Voditel)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadVoditel();
                    }
                    else if (page_select == page_main.technique)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadTechnique();
                    }
                    else if (page_select == page_main.zapchast)
                    {
                        if (parrent != null) parrent.Children.Clear();
                        LoadZapchast();
                    }

                    isDataLoaded = true;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) => Search.Text = "Поиск";

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => Search.Text = "";

        private void Click_Back(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = false;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            parrent.Children.Clear();
            page_select = page_main.none;
            Login.UserInfo[0] = ""; Login.UserInfo[1] = "";
            OpenPageLogin();
        }

        public void Animation_move(Control control1, Control control2, Frame frame_main = null, Page pages = null, page_main page_restart = page_main.none)
        {
            if (page_restart != page_main.none)
            {
                if (page_restart == page_main.Garage)
                {
                    page_select = page_main.none;
                    Click_Garage(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.ceh)
                {
                    page_select = page_main.none;
                    Click_Ceh(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.Voditel)
                {
                    page_select = page_main.none;
                    Click_Voditel(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.technique)
                {
                    page_select = page_main.none;
                    Click_Technique(new object(), new RoutedEventArgs());
                }
                else if (page_restart == page_main.zapchast)
                {
                    page_select = page_main.none;
                    Click_Zapchast(new object(), new RoutedEventArgs());
                }

            }
            else
            {
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.3);
                opgridAnimation.Completed += delegate
                {
                    if (pages != null)
                    {
                        frame_main.Navigate(pages);
                    }
                    control1.Visibility = Visibility.Hidden;
                    control2.Visibility = Visibility.Visible;
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.4);
                    control2.BeginAnimation(ScrollViewer.OpacityProperty, opgriAnimation);
                };
                control1.BeginAnimation(ScrollViewer.OpacityProperty, opgridAnimation);
            }
        }
    }
}
