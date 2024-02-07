using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoginPage
{
    public partial class DependentsCustomerReistration : UserControl
    {
        public DependentsCustomerReistration()
        {
            InitializeComponent();
        }

        public bool isKid()
        {
            return ck_isKid.IsChecked.Value;
        }

        private void txt_depFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_depFirstName.Text))
            {
                txt_error_depFirstName.Text = "First name can not be empty";
                st_depFirstName.Visibility = Visibility.Visible;
            }

            else if (txt_depFirstName.Text.Any(char.IsDigit))
            {
                txt_error_depFirstName.Text = "Fist name can not have numbers";
                st_depFirstName.Visibility = Visibility.Visible;

            }

            else if (txt_depFirstName.Text.Any(char.IsSymbol) || txt_depFirstName.Text.Any(char.IsPunctuation))
            {
                txt_error_depFirstName.Text = "First name can not have symbols";
                st_depFirstName.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_depFirstName.Text = "";
                st_depFirstName.Visibility = Visibility.Hidden;
                txt_error_depLastName.Text = "Last name can not be empty";
            }
        }

        private void txt_depLastName_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_depLastName.Text))
            {
                txt_error_depLastName.Text = "Last name can not have numbers";
                st_depLastName.Visibility = Visibility.Visible;
            }

            else if (txt_depLastName.Text.Any(char.IsDigit))
            {
                txt_error_depLastName.Text = "Last name can not be empty";
                st_depLastName.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_depLastName.Text = "";
                st_depLastName.Visibility = Visibility.Hidden;
            }
        }

        private void date_depDob_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(date_depDob.Text))
            {
                txt_error_depdob.Text = "Please select a date";
                st_depdob.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_depdob.Text = "";
                txt_error_depdob.Visibility = Visibility.Hidden;
                st_depdob.Visibility = Visibility.Hidden;
                int age = DateTime.Now.Year - date_depDob.SelectedDate.Value.Year;
                txt_depAge.Text = age.ToString();
                if (age < 12)
                    ck_isKid.IsChecked = true;
                else
                    ck_isKid.IsChecked = false;
            }
        }

        public void validate_gender()
        {
            if (radioBtn_female.IsChecked.Value || radioBtn_male.IsChecked.Value)
            {
                txt_error_depGender.Text = "Please select gender";
                st_depgender.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_depGender.Text = "";
                st_depgender.Visibility = Visibility.Hidden;
            }
        }
    
    }
}
