using LoginPage.Entities;
using LoginPage.Db;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Linq;

namespace LoginPage
{
    public partial class CustomerRegistration : UserControl
    {
        private int noOfTravellers;
        private int noOfKids;
        private Customer customer;
        private User loggedInUser;

        internal User LoggedInUser { get => loggedInUser; set => loggedInUser = value; }

        public CustomerRegistration()
        {
            InitializeComponent();
            this.customer = new Customer();
            noOfTravellers = 1;
            noOfKids = 0;
        }

        private void loadPage(UserControl uc)
        {
            ((Grid)Parent).Children.Insert(1, uc);
            ((Grid)Parent).InvalidateVisual();
            ((Grid)Parent).Children.RemoveAt(((Grid)Parent).Children.IndexOf(this));
        }

        private void txt_firstName_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_firstName.Text))
            {
                txt_error_firstName.Text = "First name is required.";
                st_firstName.Visibility = Visibility.Visible;
            }
            else if (txt_firstName.Text.Any(char.IsDigit))
            {
                txt_error_firstName.Text = "Fist name can not have numbers.";
                st_firstName.Visibility = Visibility.Visible;
            }
            else if (txt_firstName.Text.Any(char.IsSymbol) || txt_firstName.Text.Any(char.IsPunctuation))
            {
                txt_error_firstName.Text = "First name can not have symbols.";
                st_firstName.Visibility = Visibility.Visible;
            }
            else
            {
                st_firstName.Visibility = Visibility.Hidden;
            }
        }

        private void txt_lastName_KeyUp(object sender, KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_lastName.Text))
            {
                txt_error_lastName.Text = "Last name is required.";
                st_lastName.Visibility = Visibility.Visible;
            }
            else if (txt_lastName.Text.Any(char.IsDigit))
            {
                txt_error_lastName.Text = "Last name can not have numbers.";
                st_lastName.Visibility = Visibility.Visible;
            }
            else if (txt_lastName.Text.Any(char.IsSymbol) || txt_lastName.Text.Any(char.IsPunctuation))
            {
                txt_error_lastName.Text = "Last name can not have symbols.";
                st_lastName.Visibility = Visibility.Visible;
            }
            else
            {
                st_lastName.Visibility = Visibility.Hidden;
            }
        }

        private void txt_email_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_email.Text.Length == 0)
            {
                txt_error_email.Text = "E-mail address is required.";
                st_email.Visibility = Visibility.Visible;
            }
            else if (!Regex.IsMatch(txt_email.Text, "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"))
            {
                txt_error_email.Text = "Invalid email address.(ex:sample@gmail.com)";
                st_email.Visibility = Visibility.Visible;
            }
            else
            {
                st_email.Visibility = Visibility.Hidden;
            }
        }

        private void txt_contactNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_contactNumber.Text.Length == 0)
            {
                txt_error_contactNumber.Text = "Contact number is required.";
                st_contactNumber.Visibility = Visibility.Visible;
            }
            else if (!Regex.IsMatch(txt_contactNumber.Text, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$"))
            {
                txt_error_contactNumber.Text = "Invalid contact number.";
                st_contactNumber.Visibility = Visibility.Visible;
            }
            else
            {
                st_contactNumber.Visibility = Visibility.Hidden;
            }
        }

        private void date_dob_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (date_dob.SelectedDate == null)
            {
                txt_error_dob.Text = "Date of birth is required.";
                st_dob.Visibility = Visibility.Visible;
            }
            else if (date_dob.SelectedDate.Value > DateTime.Today.AddYears(-18))
            {
                txt_error_dob.Text = "Traveller's age should be grater than 18";
                st_dob.Visibility = Visibility.Visible;
            }
            else
            {
                st_dob.Visibility = Visibility.Hidden;
            }
            txt_age.Text = (DateTime.Now.Year - date_dob.SelectedDate.Value.Year).ToString();
        }

        private void cmb_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_country.SelectedItem == null)
            {
                txt_error_country.Text = "Country is required.";
                st_country.Visibility = Visibility.Visible;
            }
            else
            {
                st_country.Visibility = Visibility.Hidden;
            }
        }

        private void radioBtn_male_Checked(object sender, RoutedEventArgs e)
        {
            st_gender.Visibility = Visibility.Hidden;
        }

        private void radioBtn_female_Checked(object sender, RoutedEventArgs e)
        {
            st_gender.Visibility = Visibility.Hidden;
        }

        private void emptyEventHandler(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            if (noOfTravellers > 1)
            {
                noOfTravellers -= 1;
                this.listviewReg.Items.RemoveAt(noOfTravellers);
                txtBlock_noOfTravellers.Text = noOfTravellers.ToString();
            }
            else
            {
                noOfTravellers = 1;
                txtBlock_noOfTravellers.Text = noOfTravellers.ToString();
            }
        }

        private void btn_max_Click(object sender, RoutedEventArgs e)
        {
            noOfTravellers += 1;
            ListViewItem item = new ListViewItem();
            this.listviewReg.Items.Add(new DependentsCustomerReistration());
            txtBlock_noOfTravellers.Text = noOfTravellers.ToString();
        }

        private void cmb_country_DropDownOpened(object sender, EventArgs e)
        {
            ManageLocations.getCountries(cmb_country);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_firstName.Text))
            {
                txt_error_firstName.Text = "First name is required.";
                st_firstName.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrEmpty(txt_lastName.Text))
            {
                txt_error_lastName.Text = "Last name is required.";
                st_lastName.Visibility = Visibility.Visible;
            }
            else if (txt_email.Text.Length == 0)
            {
                txt_error_email.Text = "E-mail address is required.";
                st_email.Visibility = Visibility.Visible;
            }
            else if (txt_contactNumber.Text.Length == 0)
            {
                txt_error_contactNumber.Text = "Contact number is required.";
                st_contactNumber.Visibility = Visibility.Visible;
            }
            else if (date_dob.SelectedDate == null)
            {
                txt_error_dob.Text = "Date of birth is required.";
                st_dob.Visibility = Visibility.Visible;
            }
            else if ((radioBtn_male.IsChecked == false) & (radioBtn_female.IsChecked == false))
            {
                txt_error_gender.Text = "Gender is required.";
                st_gender.Visibility = Visibility.Visible;
            }
            else if (cmb_country.SelectedItem == null)
            {
                txt_error_country.Text = "Country is required.";
                st_country.Visibility = Visibility.Visible;
            }
            else
            {

                int c = listviewReg.Items.Count;
                for (int i = 0; i < c; i++)
                {
                    if (i == 0)
                        continue;
                    noOfKids += ((DependentsCustomerReistration)listviewReg.Items.GetItemAt(i)).isKid() == true ? 1 : 0;
                }

                this.customer.Cus_Id = Utilities.Util.getNextId(ManageCustomer.getLastCustomerId(), 1); ;
                this.customer.Cus_FirstName = txt_firstName.Text;
                this.customer.Cus_LastName = txt_lastName.Text;
                this.customer.Cus_Email = txt_email.Text;
                this.customer.Cus_Phone = txt_contactNumber.Text;
                this.customer.Cus_DOB = date_dob.SelectedDate.Value;
                if (radioBtn_male.IsChecked.Value)
                    this.customer.Cus_Gender = "M";
                if (radioBtn_female.IsChecked.Value)
                    this.customer.Cus_Gender = "F";
                this.customer.Cus_Country = cmb_country.SelectedItem.ToString();
                this.customer.Cus_HandledBy = this.LoggedInUser.Emp_Id;
                Customer customer = this.customer;
                ManageCustomer manager_c = new ManageCustomer(customer);
                if (validate_dependant() && manager_c.save())
                {
                    if (noOfTravellers > 1)
                        save_dependants();

                    MessageBox.Show("data saved.");
                    Booking booking = new Booking();
                    booking.Customer = customer;
                    booking.LoggedInUser = this.LoggedInUser;
                    booking.NoOfTravellers = int.Parse(txtBlock_noOfTravellers.Text);
                    booking.NoOfKids = noOfKids;
                    loadPage(booking);
                }
                else
                {
                    MessageBox.Show("data couldn't be saved.");
                }
            }
        }

        public bool save_dependants()
        {
            bool flag = false;
            if (noOfTravellers > 1)
            {
                int c = listviewReg.Items.Count;
                for (int i = 0; i < c; i++)
                {
                    if (i == 0)
                        continue;
                    DependentsCustomerReistration dep_form = (DependentsCustomerReistration)listviewReg.Items.GetItemAt(i);
                    string dep_id = Utilities.Util.getNextId(ManageDependant.getLastdepenadantId(), 1);
                    string fname = dep_form.txt_depFirstName.Text;
                    string lname = dep_form.txt_depLastName.Text;
                    DateTime dob = dep_form.date_depDob.SelectedDate.Value;
                    string gender = "";
                    if (dep_form.radioBtn_male.IsChecked.Value)
                        gender = "M";
                    if (dep_form.radioBtn_female.IsChecked.Value)
                        gender = "F";
                    Dependant dependant = new Dependant(dep_id, fname, lname, dob, gender, this.customer.Cus_Id);
                    ManageDependant manager_d = new ManageDependant(dependant);
                    flag = manager_d.save();
                }
            }
            return flag;
        }

        public bool validate_dependant()
        {
            bool flag = false;
            if (noOfTravellers > 1)
            {
                int c = listviewReg.Items.Count;
                for (int i = 0; i < c; i++)
                {
                    if (i == 0)
                        continue;
                    DependentsCustomerReistration dep_form = (DependentsCustomerReistration)listviewReg.Items.GetItemAt(i);
                    dep_form.validate_gender();
                    flag = (!string.IsNullOrEmpty(dep_form.txt_depFirstName.Text)
                       && !string.IsNullOrEmpty(dep_form.txt_depLastName.Text)
                       && (dep_form.radioBtn_female.IsChecked.Value || dep_form.radioBtn_male.IsChecked.Value)
                       && date_dob.SelectedDate.Value != null);
                }
            } else
            {
                flag = true;
            }
            return flag;
        }
    }
}
