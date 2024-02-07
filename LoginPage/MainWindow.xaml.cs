using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LoginPage.Db;
using LoginPage.Entities;


namespace LoginPage
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void btn_close_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_close.Background = Brushes.Gray;
        }

        private void btn_close_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_close.Background = Brushes.Transparent;
        }

        private void btn_login_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_login.Background = Brushes.Red;
        }

        private void btn_login_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_login.Background = Brushes.Goldenrod;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_foggotenPass.Foreground = Brushes.White;
        }

        private void btn_foggotenPass_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_foggotenPass.Foreground = Brushes.Moccasin;
        }

        private void btn_passVisible_MouseEnter_1(object sender, MouseEventArgs e)
        {
            btn_passVisible.Background = Brushes.Gray;
        }

        private void btn_passVisible_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_passVisible.Background = Brushes.Transparent;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_username.Text))
            {
                txt_error_username.Text = "User name can not be empty";
                txt_error_password.Clear();
                txt_username.Focus();
            } else if(string.IsNullOrEmpty(txt_password.Password))
            {
                txt_error_password.Text = "Password can not be emply";
                txt_error_username.Clear();
                txt_password.Focus();
            } else
            {
                txt_error_username.Text = "";
                txt_error_password.Text = "";
                User user = new User(txt_username.Text, txt_password.Password);
                ManageUser manager = new ManageUser(user);
                int i = manager.login();
                if (i > 0)
                {   // admin
                    Dashboard d = new Dashboard();
                    manager.getUser();
                    d.LoggedInUser = manager.User;
                    this.Close();
                    d.Show();
                }
                else if (i == 0)
                {   // non admin
                    Dashboard d = new Dashboard();
                    manager.getUser();
                    d.LoggedInUser = manager.User;
                    this.Close();
                    d.Show();
                }
                else
                {
                    MessageBox.Show("Please check the User Name and Password again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_username.Clear();
                    txt_password.Clear();
                }


            }
        }

        private void txt_username_KeyUp(object sender, KeyEventArgs e)
        {
            if(txt_username.Text.Length >= 10)
            {
                txt_error_username.Text = "User name should be less than 10";
            }
            else
            {
                txt_error_username.Clear();
            }
        }

        private void txt_error_username_KeyUp(object sender, KeyEventArgs e)
        {
            if(txt_password.Password.Length >= 8)
            {
                txt_error_password.Text = "Password should be 8 characters";
            }
            else
            {
                txt_error_password.Clear();
            }
        }

        private void btn_foggotenPass_Click(object sender, RoutedEventArgs e)
        {
            ResetPassword1 resetPass1 = new ResetPassword1();
            this.Close();
            resetPass1.Show();
        }

        private void btn_passVisible_Checked(object sender, RoutedEventArgs e)
        {
            showPass();
        }

        private void btn_passVisible_Unchecked(object sender, RoutedEventArgs e)
        {
            hidePass();
        }

        private void showPass()
        {
            txt_password.Visibility = Visibility.Hidden;
            txt_passwordVissible.Visibility = Visibility.Visible;
            txt_passwordVissible.Text = txt_password.Password;
        }

        private void hidePass()
        {
            txt_password.Visibility = Visibility.Visible;
            txt_passwordVissible.Visibility = Visibility.Hidden;
            txt_password.Password = txt_passwordVissible.Text;
        }
    }
}