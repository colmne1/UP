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
        private void LoadCeh()
        {
            Dispatcher.InvokeAsync(async () =>
            {
                foreach (ClassModules.Ceh ceh_items in ClassConnection.Connection.ceh)
                {
                    if (page_select == page_main.ceh)
                    {
                        parrent.Children.Add(new Elements.Ceh_items(ceh_items));
                        await Task.Delay(90);
                    }
                }
                if (page_select == page_main.ceh)
                {
                    if (Login.UserInfo[1] == "admin")
                    {
                        var add = new Pages.PagesInTable.Ceh(new ClassModules.Ceh());
                        parrent.Children.Add(new Elements.Add(add));
                    }
                }
            });
        }

        private void Click_Ceh(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = true;
            garage.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            ceh_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            Voditel_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            technique_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            zapchast_itms.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2C2C2C"));
            if (frame_main.Visibility == Visibility.Visible) MainWindow.main.Animation_move(MainWindow.main.frame_main, MainWindow.main.scroll_main);
            if (page_select != page_main.ceh)
            {
                page_select = page_main.ceh;
                DoubleAnimation opgridAnimation = new DoubleAnimation();
                opgridAnimation.From = 1;
                opgridAnimation.To = 0;
                opgridAnimation.Duration = TimeSpan.FromSeconds(0.2);
                opgridAnimation.Completed += delegate
                {
                    parrent.Children.Clear();
                    DoubleAnimation opgriAnimation = new DoubleAnimation();
                    opgriAnimation.From = 0;
                    opgriAnimation.To = 1;
                    opgriAnimation.Duration = TimeSpan.FromSeconds(0.2);
                    opgriAnimation.Completed += delegate
                    {
                        LoadCeh();
                    };
                    parrent.BeginAnimation(StackPanel.OpacityProperty, opgriAnimation);
                };
                parrent.BeginAnimation(StackPanel.OpacityProperty, opgridAnimation);
            }
        }
    }
}
