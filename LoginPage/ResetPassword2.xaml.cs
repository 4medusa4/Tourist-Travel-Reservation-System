using LoginPage.Entities;
using System.Windows;

namespace LoginPage
{
    public partial class ResetPassword2 : Window
    {
        private User user;

        internal User User { get => user; set => user = value; }

        public ResetPassword2()
        {
            InitializeComponent();
        }
        public int randomcode { private get; set; }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_verifyCode.Text))
                {
                    MessageBox.Show("Verification code must be provided.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                else if (randomcode == int.Parse(txt_verifyCode.Text))
                {
                    MessageBox.Show("Code incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                else
                {
                    ResetPassword3 resetPass3 = new ResetPassword3();
                    resetPass3.User = this.User;
                    resetPass3.Show();
                    this.Close();
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Code incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Code incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rslt = MessageBox.Show("The action would send you back to the window where you were prevoiusl was.\nConfirm ?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (rslt == MessageBoxResult.Yes)
            {
                this.Close();
                new ResetPassword1().Show();
            }
        }
    }
}
