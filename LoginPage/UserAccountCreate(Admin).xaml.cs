using LoginPage.Db;
using LoginPage.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoginPage
{
    public partial class UserAccountCreate_Admin_ : UserControl
    {
        public UserAccountCreate_Admin_()
        {
            InitializeComponent();
        }

        private void loadPage(UserControl uc)
        {
            ((Grid)Parent).Children.Insert(1, uc);
            ((Grid)Parent).InvalidateVisual();
            ((Grid)Parent).Children.RemoveAt(((Grid)Parent).Children.IndexOf(this));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txt_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_password.Password))
            {
                txt_error_password.Text = "Password is required.";
                st_conPassword.Visibility = Visibility.Visible;
            }
            else if (txt_password.Password.Length < 8)
            {
                txt_error_password.Text = "Password must be grater than 8 characters";
                st_password.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_password.Text = "";
                st_password.Visibility = Visibility.Hidden;
            }
        }

        private void txt_conPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_conPassword.Password))
            {
                txt_error_conPassword.Text = "Confirm password is required.";
                st_conPassword.Visibility = Visibility.Visible;
            }
            else if (txt_password.Password.Length < 8)
            {
                txt_error_conPassword.Text = "Password must be grater than 8 characters";
                st_password.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_conPassword.Text = "";
                st_conPassword.Visibility = Visibility.Hidden;
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (txt_password.Password.Equals(txt_conPassword.Password))
            {
                if (string.IsNullOrEmpty(cmb_empId.Text))
                {
                    txt_error_empId.Text = "Eployee ID is required.";
                    st_empId.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(txt_uname.Text))
                {
                    txt_error_uname.Text = "username is required.";
                    st_uname.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(txt_password.Password))
                {
                    txt_error_password.Text = "Password is required.";
                    st_conPassword.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(txt_conPassword.Password))
                {
                    txt_error_conPassword.Text = "Confirm password is required.";
                    st_conPassword.Visibility = Visibility.Visible;
                }
                else if (txt_conPassword.Password != txt_password.Password)
                {
                    txt_error_conPassword.Text = "Password does not match.";
                    st_conPassword.Visibility = Visibility.Visible;
                }
                else
                {
                    string[] emp_selected = cmb_empId.SelectedItem.ToString().Split('-');
                    string eid = emp_selected[0];

                    User u = new User();
                    u.Emp_Id = eid;
                    u.Uname = txt_uname.Text;
                    u.Pass = Utilities.Util.encrypt(txt_password.Password);
                    MessageBox.Show(u.Pass);
                    ManageUser manager = new ManageUser(u);
                    if (checkBox_userType.IsChecked.Value)
                    {
                        if (manager.create_Admin_Account())
                        {
                            MessageBox.Show("Admin account created.");
                            resetUI();
                        }
                        else
                        {
                            MessageBox.Show("User account not created.");
                        }
                    }
                    else
                    {
                        if (manager.create_NonAdmin_Account())
                        {
                            MessageBox.Show("Non-admin account created.");
                            resetUI();
                        }
                        else
                        {
                            MessageBox.Show("User account not created.");
                        }
                    }
                }
            }
        }

        private void cmb_empId_DropDownOpened(object sender, System.EventArgs e)
        {
            ManageUser.getUsers(cmb_empId);
        }

        private void resetUI()
        {
            ManageUser.getUsers(cmb_empId);
            txt_uname.Text = string.Empty;
            txt_password.Password = string.Empty;
            txt_conPassword.Password = string.Empty;
            checkBox_userType.IsChecked = false;
            checkBox_showConPass.IsChecked = false;
            checkBox_showPass.IsChecked = false;
        }

        private void btn_discard_Click(object sender, RoutedEventArgs e)
        {
            resetUI();
            loadPage(new Home()); ;
        }

        private void showConPass()
        {
            txt_conPassword.Visibility = Visibility.Hidden;
            txt_conPasswordshow.Visibility = Visibility.Visible;
            txt_conPasswordshow.Text = txt_conPassword.Password;
        }

        private void hideConPass()
        {
            txt_conPassword.Visibility = Visibility.Visible;
            txt_conPasswordshow.Visibility = Visibility.Hidden;
            txt_conPassword.Password = txt_conPasswordshow.Text;
        }

        private void showPass()
        {
            txt_password.Visibility = Visibility.Hidden;
            txt_Passwordshow.Visibility = Visibility.Visible;
            txt_Passwordshow.Text = txt_password.Password;
        }

        private void hidePass()
        {
            txt_password.Visibility = Visibility.Visible;
            txt_Passwordshow.Visibility = Visibility.Hidden;
            txt_password.Password = txt_Passwordshow.Text;
        }

        private void checkBox_showPass_Checked(object sender, RoutedEventArgs e)
        {
            showPass();
        }

        private void checkBox_showPass_Unchecked(object sender, RoutedEventArgs e)
        {
            hidePass();
        }

        private void checkBox_showConPass_Checked(object sender, RoutedEventArgs e)
        {
            showConPass();
        }

        private void checkBox_showConPass_Unchecked(object sender, RoutedEventArgs e)
        {
            hideConPass();
        }

        private void txt_uname_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_uname.Text))
            {
                txt_error_uname.Text = "Username is required.";
                st_uname.Visibility = Visibility.Visible;
            }
            else if (txt_uname.Text.Length > 20)
            {
                txt_error_uname.Text = "Maximum length the username can get is 20 characters";
                st_uname.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_uname.Text = "";
                st_uname.Visibility = Visibility.Hidden;
            }
        }
    }
}
