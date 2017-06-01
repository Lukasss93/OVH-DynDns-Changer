using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using OVHDD.Classes;

namespace OVHDD.Windows
{
    public partial class Home : Window
    {
        private ObservableCollection<Profile> profiles = new ObservableCollection<Profile>();

        public Home()
        {
            InitializeComponent();

            this.Title = AppConstants.Name;
            this.Width = 320;
            this.Height = 375;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.CanMinimize;

            //load profiles
            this.Loaded += (s, e) => 
            {
                profiles= AppSettings.Get<ObservableCollection<Profile>>("profiles");
                cb_profile.ItemsSource = profiles;
                cb_profile.SelectedIndex = 0;
            };

            //save profiles
            this.Closing += (s, e) =>
            {
                AppSettings.Set("profiles", profiles);
                AppSettings.SaveSettingsFile();
            };

            bt_update.Click += UpdateClick;            
                       
            bt_profile_new.Click += NewProfileClick;
            bt_profile_delete.Click += DeleteProfileClick;            
        }        

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bt_update.IsEnabled = false;

                string response = Tools.ChangeOVHDNS(tb_host.Text, tb_username.Text, tb_password.Password);

                lb_info.Inlines.Clear();
                if(response.Contains("nochg"))
                {
                    string ip = response.Split(' ')[1];

                    lb_info.Inlines.Add(new Run() { Text = "No change.\nCurrent IP: " + ip, Foreground = new SolidColorBrush(Color.FromRgb(150, 130, 25)) });
                }
                else if(response.Contains("good"))
                {
                    string ip = response.Split(' ')[1];

                    lb_info.Inlines.Add(new Run() { Text = "IP changed!\nCurrent IP: " + ip, Foreground = new SolidColorBrush(Colors.Green) });
                }
                else
                {
                    lb_info.Inlines.Add(new Run() { Text = response, Foreground = new SolidColorBrush(Colors.Red) });
                }
            }
            catch(Exception ex)
            {
                lb_info.Inlines.Clear();
                lb_info.Inlines.Add(new Run() { Text = ex.Message, Foreground = new SolidColorBrush(Colors.Red) });
            }
            finally
            {
                bt_update.IsEnabled = true;
            }
        }


        private void NewProfileClick(object sender, RoutedEventArgs e)
        {
            var dialog = new NewProfile();
            if(dialog.ShowDialog() == true)
            {
                profiles.Add(new Profile(dialog.ResponseText,tb_host.Text,tb_username.Text,tb_password.Password));
            }
        }

        private void DeleteProfileClick(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if(messageBoxResult == MessageBoxResult.Yes)
            {
                profiles.RemoveAt(cb_profile.SelectedIndex);
                cb_profile.SelectedIndex = 0;
            }
        }
    }
}
