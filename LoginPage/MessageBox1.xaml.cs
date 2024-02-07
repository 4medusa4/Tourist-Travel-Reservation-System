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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginPage
{
    /// <summary>
    /// Interaction logic for MessageBox1.xaml
    /// </summary>
    public partial class MessageBox1 : Window
    {
        public MessageBox1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void title_TextChanged(object sender, TextChangedEventArgs e)
        {


        }
        public void error()
        {
            title.Text = "Error";
        }
    }
}
