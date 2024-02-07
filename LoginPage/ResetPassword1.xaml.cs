using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using LoginPage.Db;
using LoginPage.Entities;

namespace LoginPage
{

    public partial class ResetPassword1 : Window
    {
        public ResetPassword1()
        {
            InitializeComponent();
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            User u = new User();
            u.Emp_Email = txt_email.Text;
            ManageUser manager = new ManageUser(u);
            if(manager.checkUserEmail())
            {
                string to = txt_email.Text;
                Utilities.FileSender fs = new Utilities.FileSender();
                fs.addReciever(to);
                int randomcode = new Random().Next(999999);
                fs.Subject = "Password Reset Code";
                fs.Body = "Your Reset Code is " + randomcode;
                bool f = fs.sendSync();
                if (f)
                {
                    ResetPassword2 resetPass2 = new ResetPassword2();
                    resetPass2.User = manager.User;
                    resetPass2.randomcode = randomcode;
                    this.Close();
                    resetPass2.Show();
                }
            } else
            {
                MessageBox.Show("Email not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
