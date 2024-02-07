using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using LoginPage.Db;
using LoginPage.Entities;
using MaterialDesignThemes.Wpf;

namespace LoginPage
{
    public partial class Home : UserControl
    {
        private User loggedInUser;
        private View customer_view;
        private View package_view;
        private BusDetailsCreate_Admin bus_data;

        internal User LoggedInUser { get => loggedInUser; set => loggedInUser = value; }
        public View Package_view { get => package_view; set => package_view = value; }

        public Home()
        {
            InitializeComponent();
        }

        private void loadPage(UserControl uc)
        {
            ((Grid)Parent).Children.Insert(1, uc);
            ((Grid)Parent).InvalidateVisual();
            ((Grid)Parent).Children.RemoveAt(((Grid)Parent).Children.IndexOf(this));
        }

        private static void card_dimm(Card card)
        {
            card.Background = Brushes.LightGray;
            card.Width = 305;
            card.Height = 205;
        }

        private static void card_norm(Card card)
        {
            card.Background = Brushes.White;
            card.Width = 300;
            card.Height = 200;
        }

        private void card1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_dimm(card1);
        }

        private void card1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_norm(card1);
        }

        private void card2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_dimm(card2);
        }

        private void card2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_norm(card2);
        }

        private void card3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_dimm(card3);
        }

        private void card3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_norm(card3);
        }

        private void btn_cusReg_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CustomerRegistration reg = new CustomerRegistration();
            reg.LoggedInUser = this.LoggedInUser;
            loadPage(reg);
        }

        private void card4_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_dimm(card4);
        }

        private void card4_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_norm(card4);
        }

        private void card5_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_dimm(card5);
        }

        private void card5_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_norm(card5);
        }

        private void card6_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_dimm(card6);
        }

        private void card6_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            card_norm(card6);
        }

        private void btn_packages_Click(object sender, RoutedEventArgs e)
        {
            Package package = new Package();
            package.Pack_Id = Utilities.Util.getNextId(ManagePackage.getLastPackageId(), 1);
            PackageCreate_Admin_ create_package = new PackageCreate_Admin_();
            create_package.Package = package;
            loadPage(create_package);
        }

        private void btn_booking_Click(object sender, RoutedEventArgs e)
        {
            loadPage(new BusDetailsCreate_Admin());
        }

        private void btn_createAccount_Click(object sender, RoutedEventArgs e)
        {
            loadPage(new UserAccountCreate_Admin_());
        }

        private void btn_packagesView_Click(object sender, RoutedEventArgs e)
        {
            Package_view = new View();
            Package_view.table_name.Text = "Packages";
            bindColumn(Package_view.data_view, "pack_Id", "package Id");
            bindColumn(Package_view.data_view, "pack_Name", "name");
            bindColumn(Package_view.data_view, "pack_PriceAdults", "price(adults)");
            bindColumn(Package_view.data_view, "pack_PriceKids", "price(kids)");
            bindColumn(Package_view.data_view, "pack_Duration", "duration(days)");
            bindColumn(Package_view.data_view, "pack_Created", "date created");
            bindColumn(Package_view.data_view, "pack_SingleRoomCharge", "room chrg(S)");
            bindColumn(Package_view.data_view, "pack_DoubleRoomCharge", "room chrg(D)");
            bindColumn(Package_view.data_view, "pack_FamilyRoomCharge", "room chrg(F)");
            bindColumn(Package_view.data_view, "pack_image", "image");
            bindColumn(Package_view.data_view, "pack_FileName", "pdf");
            Package_view.txt_search.KeyUp += txt_package_search;
            Package_view.data_view.MouseDoubleClick += data_view_package_MouseDoubleClick;
            try
            {
                Package_view.data_view.ItemsSource = ManagePackage.getPackages("").DefaultView;
            }
            catch (Exception)
            { }
            loadPage(Package_view);
        }

        private void data_view_package_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdatePackage_Admin packUpdate = new UpdatePackage_Admin();
            DataRowView rowSelected = package_view.data_view.SelectedItem as DataRowView;
            if (rowSelected != null)
            {
                Package p = new Package();
                p.Pack_Id = rowSelected["pack_Id"].ToString();
                ManagePackage manager = new ManagePackage(p);
                manager.getPackage();
                packUpdate.Package = manager.Package;
                packUpdate.setData();
                package_view.loadPage(packUpdate);
            }
        }

        private void txt_customer_search(Object sender, KeyEventArgs e)
        {
            customer_view.data_view.ItemsSource = ManagePackage.getPackages(customer_view.txt_search.Text).DefaultView;

        }

        private void txt_package_search(Object sender, KeyEventArgs e)
        {
            Package_view.data_view.ItemsSource = ManagePackage.getPackages(Package_view.txt_search.Text).DefaultView;

        }

        private void card7_MouseEnter(object sender, MouseEventArgs e)
        {
            card_dimm(card7);
        }

        private void card7_MouseLeave(object sender, MouseEventArgs e)
        {
            card_norm(card7);
        }

        private void bindColumn(DataGrid data_grid, string db_column, string grid_column)
        {
            MaterialDesignThemes.Wpf.DataGridTextColumn col = new MaterialDesignThemes.Wpf.DataGridTextColumn();
            col.Header = grid_column;
            col.Binding = new Binding(db_column);
            data_grid.Columns.Add(col);
        }

        private void btn_view_customer_Click(object sender, RoutedEventArgs e)
        {
            customer_view = new View();
            customer_view.table_name.Text = "Customers";
            bindColumn(customer_view.data_view, "cus_Id", "cus. Id");
            bindColumn(customer_view.data_view, "cus_FirstName", "first name");
            bindColumn(customer_view.data_view, "cus_LastName", "last name");
            bindColumn(customer_view.data_view, "cus_Email", "email");
            bindColumn(customer_view.data_view, "cus_Phone", "phone");
            bindColumn(customer_view.data_view, "cus_DOB", "dob");
            bindColumn(customer_view.data_view, "cus_Gender", "gender");
            bindColumn(customer_view.data_view, "cus_Country", "country");
            bindColumn(customer_view.data_view, "NUID", "handle by");
            customer_view.txt_search.KeyUp += txt_customer_search;
            try
            {
                customer_view.data_view.ItemsSource = ManageCustomer.getCustomers("").DefaultView;
            }
            catch (Exception)
            { }
            loadPage(customer_view);
        }

        private void btn_view_booking_Click(object sender, RoutedEventArgs e)
        {
            View view = new View();
            view.table_name.Text = "Booking";
            bindColumn(view.data_view, "res_Id", "Booking Id");
            bindColumn(view.data_view, "res_Date", "Booking Datee");
            bindColumn(view.data_view, "res_NoOfTravellers", "No of Travellers");
            bindColumn(view.data_view, "res_StartDate", "Start date");
            bindColumn(view.data_view, "res_EndDate", "End date");
            bindColumn(view.data_view, "res_SingleRoomCount", "no of single rooms");
            bindColumn(view.data_view, "res_DoubleRoomCount", "no of double roomsr");
            bindColumn(view.data_view, "res_FamilyRoomCount", "no of family roomsy");
            bindColumn(view.data_view, "PID", "handle by");
            bindColumn(view.data_view, "IID", "handle by");
            bindColumn(view.data_view, "CID", "Customer");
            string search_text = "";
            try
            {
                view.data_view.ItemsSource = ManageReservation.getReservation(search_text).DefaultView;
            }
            catch (Exception)
            { }
            loadPage(view);
        }

        private void btn_addBus_Click(object sender, RoutedEventArgs e)
        {
            bus_data = new BusDetailsCreate_Admin();
            Bus bus = new Bus();
            bus.Bus_Id = Utilities.Util.getNextId(ManageBus.getLastBusId(), 1);
            bus_data.Bus = bus;
            bus_data.txtBlock_busId.Text = bus.Bus_Id;
            bus_data.btn_save.Click += btn_bus_save_Click;
            loadPage(bus_data);
        }

        private void btn_bus_save_Click(Object sender, RoutedEventArgs e)
        {
            bus_data.save();
        }

        private void btn_bus_update_Click(Object sender, RoutedEventArgs e)
        {
            bus_data.update();
        }
    }
}
