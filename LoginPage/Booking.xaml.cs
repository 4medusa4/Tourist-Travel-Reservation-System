using LoginPage.Db;
using LoginPage.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LoginPage
{
    public partial class Booking : UserControl
    {
        private Customer customer;
        private Package package;
        private User loggedInUser;
        private int noOfTravellers;
        private int noOfKids;
        int noOfSingleRooms;
        int noOfDoubleRooms;
        int noOfFamilyRooms;
        private double roomCharges;
        private double packageCharges;
        private double total;

        internal Customer Customer { get => customer; set => customer = value; }
        public int NoOfTravellers { get => noOfTravellers; set => noOfTravellers = value; }
        public int NoOfKids { get => noOfKids; set => noOfKids = value; }
        internal User LoggedInUser { get => loggedInUser; set => loggedInUser = value; }

        public Booking()
        {
            InitializeComponent();
            noOfSingleRooms = 0;
            noOfDoubleRooms = 0;
            noOfFamilyRooms = 0;
            noOfTravellers = 0;
            noOfKids = 0;
            roomCharges = 0.0;
            packageCharges = 0.0;
            total = 0.0;
        }

        private void cmb_package_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_package.SelectedItem == null)
            {
                txt_error_package.Visibility = Visibility.Visible;
            }
            else
            {
                txt_error_package.Visibility = Visibility.Hidden;
                // gets pachae id from combo box selected item
                string[] pack_selected = cmb_package.SelectedItem.ToString().Split('-');
                string pid = pack_selected[1];

                // create new package with the selected package id and gets its data from db.
                Package p = new Package();
                p.Pack_Id = pid;
                ManagePackage manager = new ManagePackage(p);
                this.package = manager.getPackage();
                calculateCharges();
            }
        }

        public void calculateCharges()
        {
            double kid_charge = package.Pack_PriceKids * noOfKids;
            double adult_charge = package.Pack_PriceAdults * (noOfTravellers - noOfKids);
            int no_singleRoom = (!string.IsNullOrEmpty(txt_noSingleRooms.Text) && !Utilities.Validation.isAlpha(txt_noSingleRooms.Text)) == true ? int.Parse(txt_noSingleRooms.Text) : 0;
            int no_doubleRoom = (!string.IsNullOrEmpty(txt_noDoubleRooms.Text) && !Utilities.Validation.isAlpha(txt_noDoubleRooms.Text)) == true ? int.Parse(txt_noDoubleRooms.Text) : 0;
            int no_familyRoom = (!string.IsNullOrEmpty(txt_noFamilyRooms.Text) && !Utilities.Validation.isAlpha(txt_noFamilyRooms.Text)) == true ? int.Parse(txt_noFamilyRooms.Text) : 0;

            double single_room_charges = package.Pack_SingleRoomCharge * no_singleRoom;
            double double_room_charges = package.Pack_DoubleRoomCharge * no_doubleRoom;
            double family_room_charges = package.Pack_FamilyRoomCharge * no_familyRoom;
            this.noOfSingleRooms = no_singleRoom;
            this.noOfDoubleRooms = no_doubleRoom;
            this.noOfFamilyRooms = no_familyRoom;
            this.roomCharges = single_room_charges + double_room_charges + family_room_charges;
            packageCharges = kid_charge + adult_charge;

            total = packageCharges + roomCharges;
            txt_totalPackageCharge.Text = "" + packageCharges;
            txt_noOfAdults.Text = "adult price: " + package.Pack_PriceAdults + " x no: " + noOfKids + " -> " + adult_charge;
            txt_noOfKids.Text = "kid price: " + package.Pack_PriceKids + " x no: " + (noOfTravellers - noOfKids) + " -> " + kid_charge;

            txt_totalRoomCharge.Text = "" + roomCharges;
            txt_singleRoomCharge.Text = "single room(s): " + package.Pack_SingleRoomCharge + " x no: " + no_singleRoom + " -> " + single_room_charges;
            txt_doubleRoomCharge.Text = "double room(s): " +  package.Pack_DoubleRoomCharge + " x no: " + no_doubleRoom + " -> " + double_room_charges;
            txt_familyRoomCharge.Text = "family room(s): " + package.Pack_FamilyRoomCharge + " x no: " + no_familyRoom + " -> " + family_room_charges;
            txt_btoTalAmount.Text = "" + total;
        }

        private void cmb_package_DropDownOpened(object sender, EventArgs e)
        {
            ManagePackage.getPackages(cmb_package);
        }

        private void uc_booking_Loaded(object sender, RoutedEventArgs e)
        {
            txt_name.Text = this.customer.Cus_FirstName + " " + this.customer.Cus_LastName;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            // creates invoice
            Invoice invoice = new Invoice();
            invoice.Inv_Id = Utilities.Util.getNextId(ManageInvoice.getLastInvoiceId(), 1);
            invoice.Cid = this.customer.Cus_Id;
            invoice.Inv_Date = DateTime.Now;
            invoice.Inv_Total = total;

            // creates reservation
            string res_Id = Utilities.Util.getNextId(ManageReservation.getLastReservationId(), 1);
            DateTime res_Date = DateTime.Now;
            int not = this.NoOfTravellers;
            DateTime res_StartDate = date_startDate.SelectedDate.Value;
            DateTime res_EndtDate = date_enddate.SelectedDate.Value;
            int res_SingleRoom = this.noOfSingleRooms;
            int res_DoubleRoom = this.noOfDoubleRooms;
            int res_FamilyRoom = this.noOfFamilyRooms;
            string pid = this.package.Pack_Id;
            string iid = invoice.Inv_Id;
            string cid = Customer.Cus_Id;

            Reservation reservation = new Reservation(res_Id, not, res_Date, res_StartDate, res_EndtDate, 
                                            res_SingleRoom, res_DoubleRoom, res_FamilyRoom, pid, iid, cid);

            ManageInvoice manage_i = new ManageInvoice(invoice);
            ManageReservation manage_r = new ManageReservation(reservation);
            if (manage_i.save() && manage_r.save())
            {
                MessageBox.Show("Booking saved.");
            } else
            {
                MessageBox.Show("Booking not saved.");
            }
        }

        private void txt_noSingleRooms_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            calculateCharges();
        }

        private void txt_noDoubleRooms_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            calculateCharges();
        }

        private void txt_noFamilyRooms_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            calculateCharges();
        }
    }
}
