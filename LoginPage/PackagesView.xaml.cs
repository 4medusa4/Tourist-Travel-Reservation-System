using System.Windows.Controls;

namespace LoginPage
{
    public partial class PackagesView : UserControl
    {
        public PackagesView()

        {
            InitializeComponent();
        }

        public void loadPage(UserControl uc)
        {
            ((Grid)Parent).Children.Insert(1, uc);
            ((Grid)Parent).InvalidateVisual();
            ((Grid)Parent).Children.RemoveAt(((Grid)Parent).Children.IndexOf(this));
        }
    }
}
