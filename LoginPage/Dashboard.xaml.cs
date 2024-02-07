using LoginPage.Db;
using LoginPage.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LoginPage
{
    public partial class Dashboard : Window
    {
        private User loggedInUser;

        internal User LoggedInUser { get => loggedInUser; set => loggedInUser = value; }

        public Dashboard()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private static void item_dim(ListViewItem item)
        {
            item.Background = Brushes.Gray;

        }
        private static void item_norm(ListViewItem item)
        {
            item.Background = Brushes.Transparent;

        }
        internal void LoadPage(UserControl uc)
        {
            page_loader.Children.Clear();
            page_loader.Children.Insert(0, uc);
            page_loader.InvalidateVisual();
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void btn_max_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }

            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void ToolTip_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btn_close_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_close.BorderBrush = Brushes.Gray;
        }

        private void btn_close_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_close.BorderBrush = Brushes.Transparent;
        }

        private void btn_max_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_max.BorderBrush = Brushes.Gray;
        }

        private void btn_max_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_max.BorderBrush = Brushes.Transparent;
        }

        private void btn_min_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_min.BorderBrush = Brushes.Gray;
        }

        private void btn_min_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_min.BorderBrush = Brushes.Transparent;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }

        private void lv_home_Selected(object sender, RoutedEventArgs e)
        {
            if (page_loader.Children.GetType() != new Home().GetType())
            {
                Home home = new Home();
                home.LoggedInUser = this.loggedInUser;
                LoadPage(home);
                tg_btn.IsChecked = true;
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want exit?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
            {
                this.Show();
            }
        }



        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                page_loader.Opacity = 1;
                page_loader.IsEnabled = true;
                top.IsEnabled = true;
            }
            catch
            {

            }

        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            page_loader.Opacity = 0.5;
            page_loader.IsEnabled = false;
            top.IsEnabled = false;

        }


        private void lv_logout_Selected(object sender, RoutedEventArgs e)
        {
            tg_btn.IsChecked = true;
            new MainWindow().Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }

        private void page_loader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tg_btn.IsChecked = true;
        }

        private void lv_dashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            item_dim(lv_dashboard);
        }

        private void lv_dashboard_MouseLeave(object sender, MouseEventArgs e)
        {
            item_norm(lv_dashboard);
        }

        private void lv_home_MouseEnter(object sender, MouseEventArgs e)
        {
            item_dim(lv_home);
        }

        private void lv_home_MouseLeave(object sender, MouseEventArgs e)
        {
            item_norm(lv_home);
        }

        private void lv_gmail_MouseEnter(object sender, MouseEventArgs e)
        {
            item_dim(lv_gmail);
        }

        private void lv_gmail_MouseLeave(object sender, MouseEventArgs e)
        {
            item_norm(lv_gmail);
        }

        private void lv_account_MouseEnter(object sender, MouseEventArgs e)
        {
            item_dim(lv_account);
        }

        private void lv_account_MouseLeave(object sender, MouseEventArgs e)
        {
            item_norm(lv_account);
        }

        private void lv_logout_MouseEnter(object sender, MouseEventArgs e)
        {
            item_dim(lv_logout);
        }

        private void lv_logout_MouseLeave(object sender, MouseEventArgs e)
        {
            item_norm(lv_logout);
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            string to = txt_emailaddress.Text;
            string outPutDirectory = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            string file_folder = new Uri(Path.Combine(outPutDirectory, "Documents", "Pack Files")).LocalPath;
            Utilities.FileSender fs = new Utilities.FileSender();
            List<Package> p = new List<Package>();
            p = ManagePackage.getPackages();

            // sets reciever
            if (!string.IsNullOrEmpty(to))
                fs.addReciever(to);

            // sets attachments
            p.ForEach(x =>
            {
                string file_path = new Uri(Path.Combine(file_folder, x.Pack_FileName)).LocalPath;
                if (File.Exists(file_path))
                    fs.addAttachment(file_path);
                else
                    err += "\'" + x.Pack_FileName + "\' of Package \'" + x.Pack_Id + "\' not found.\n";
            });
            // displaying an error if occurs
            if (!string.IsNullOrEmpty(err))
                MessageBox.Show(err, "File missing.", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);

            fs.Subject = "TRAVEL THE WORLD!";

            fs.Body = "Hi dear traveller," +
                      "\nWe offer you the BIGGEST value for your money so come and enjoy the breeze of the nature by travelling around the globe. Packages with affordable prices are made just for you." +
                      "\nFind the attached package details." +
                      "\nEnjoy your day!" +

                     "\n\n\nThank you." +
                     "\nCeylon Travellers," +
                     "\nSri Lanka.";
            fs.sendAsync();
        }

        private void lv_dashboard_Selected(object sender, RoutedEventArgs e)
        {
            foreach (Window win in Application.Current.Windows)
            {
                
            }
            Dashboard dashboard = new Dashboard();
            dashboard.LoggedInUser = this.loggedInUser;
            dashboard.Show();
            this.Close();
            tg_btn.IsChecked = true;
        }

    }
}
    
