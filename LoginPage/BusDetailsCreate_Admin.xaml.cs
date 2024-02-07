using System.Windows;
using System.Windows.Controls;

namespace LoginPage
{
    public partial class BusDetailsCreate_Admin : UserControl
    {
        private Entities.Bus bus;
        public BusDetailsCreate_Admin()
        {
            InitializeComponent();
            this.bus = new Entities.Bus();
        }

        private void setData()
        {
            this.bus.Bus_Id = txtBlock_busId.Text;
            this.bus.Bus_Number = txt_capacity.Text;
            this.bus.Bus_Capacity = int.Parse(txt_capacity.Text);
        }

        public void save()
        {
            setData();
            Db.ManageBus manager_b = new Db.ManageBus(this.bus);
            if(manager_b.save())
            {
                MessageBox.Show("Bus deatals saved.");
                loadPage(new Home());
            } else
            {
                MessageBox.Show("Bus deatals not saved.");
            }
        }

        public void update()
        {
            setData();
            Db.ManageBus manager_b = new Db.ManageBus(this.bus);
            if (manager_b.update())
            {
                MessageBox.Show("Bus deatals updated.");
                loadPage(new Home());
            }
            else
            {
                MessageBox.Show("Bus deatals not updated.");
            }
        }

        private void loadPage(UserControl uc)
        {
            ((Grid)Parent).Children.Insert(1, uc);
            ((Grid)Parent).InvalidateVisual();
            ((Grid)Parent).Children.RemoveAt(((Grid)Parent).Children.IndexOf(this));
        }

        internal Entities.Bus Bus { get => bus; set => bus = value; }
    }
}
