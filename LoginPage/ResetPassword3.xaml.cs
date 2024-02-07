using LoginPage.Db;
using LoginPage.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace LoginPage
{
    public partial class ResetPassword3 : Window
    {
        private User user;

        internal User User { get => user; set => user = value; }

        public ResetPassword3()
        {
            InitializeComponent();
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            if (txt_newpass.Password == txt_confirmPass.Password)
            {
                byte[] enc = null;
                StringBuilder builder = new StringBuilder();
                using (SHA256 sha256hash = SHA256.Create())
                {
                    enc = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(txt_newpass.Password));
                    for (int j = 0; j < enc.Length; j++)
                    {
                        builder.Append(enc[j].ToString("x2"));
                    }
                }

                User u = this.User;
                u.Pass = builder.ToString();
                ManageUser manager = new ManageUser(u);
                MessageBox.Show("Password Successfully Changed");
            }
            else
            {
                MessageBox.Show("Sorry your New Password and Confirm password is not Match");
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rslt = MessageBox.Show("This would result in going back to login window.\nConfirm ?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (rslt == MessageBoxResult.Yes)
            {
                new MainWindow().Show();
                this.Close();
            }
        }
    }
}
