using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using LoginPage.Db;
using LoginPage.Entities;
using Microsoft.Win32;

namespace LoginPage
{
    public partial class PackageCreate_Admin_ : UserControl
    {
        private Package package;
        private string outPutDirectory;
        private Uri selected_image_uri;
        private Uri selected_file_uri;
        internal Package Package { get => package; set => package = value; }

        public PackageCreate_Admin_()
        {
            InitializeComponent();
            package = new Package();
            outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        }

        private void btn_uploadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openImage = new OpenFileDialog();
                openImage.Filter = "PEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
                if (openImage.ShowDialog().Value)
                {
                    Uri imageUri = new Uri(openImage.FileName);
                    selected_image_uri = imageUri;
                    packageImage.Source = new BitmapImage(imageUri);
                    this.package.Pack_image = openImage.SafeFileName;
                }
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("The file format is not supported. Please select file like .jpg /.png /.jpge", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("The file is not supported.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btn_uploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog().Value)
            {
                string fileName = openFile.FileName;
                selected_file_uri = new Uri(openFile.FileName);
                this.package.Pack_FileName = openFile.SafeFileName;
                filePath.Text = fileName;
            }
        }

        private void btn_discard_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to discard changes", "Discard", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

            }
        }

        private void uc_CreatePackage_Loaded(object sender, RoutedEventArgs e)
        {
            txtBlock_packId.Text = this.package.Pack_Id;

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Package pack = new Package();
            pack.Pack_Id = this.package.Pack_Id;
            pack.Pack_Name = txt_packageName.Text;
            pack.Pack_PriceAdults = double.Parse(txt_priceAdults.Text);
            pack.Pack_PriceKids = double.Parse(txt_priceKids.Text);
            pack.Pack_Duration = int.Parse(txt_packageDurartion.Text);
            pack.Pack_Created = DateTime.Now;
            pack.Pack_SingleRoomCharge = double.Parse(txt_singleRoom.Text);
            pack.Pack_DoubleRoomCharge = double.Parse(txt_doubleRoom.Text);
            pack.Pack_FamilyRoomCharge = double.Parse(txt_familyRoom.Text);
            pack.Pack_image = this.package.Pack_image;
            pack.Pack_FileName = this.package.Pack_FileName;

            ManagePackage manager_p = new ManagePackage(pack);
            if (manager_p.save())
            {
                save_documents();
                MessageBox.Show("Package saved.");
            }
            else
            {
                MessageBox.Show("data not saved!");
            }
        }

        public void save_documents()
        {
            string status = "";
            string pack_image_folder_path = new Uri(Path.Combine(outPutDirectory, "Documents", "Pack Images")).LocalPath;
            string pack_file_folder_path = new Uri(Path.Combine(outPutDirectory, "Documents", "Pack Files")).LocalPath;
            if (!Directory.Exists(pack_image_folder_path))
            {
                Directory.CreateDirectory(pack_image_folder_path);
                status = "\'" + pack_image_folder_path + "\'" + " folder created.";
            }
            if (!Directory.Exists(pack_file_folder_path))
            {
                Directory.CreateDirectory(pack_file_folder_path);
                status += "\n\n\'" + pack_file_folder_path + "\'" + " folder created.";
            }
            string image_destination = new Uri(Path.Combine(pack_image_folder_path, this.package.Pack_image)).LocalPath;
            string file_destination = new Uri(Path.Combine(pack_file_folder_path, this.package.Pack_FileName)).LocalPath;

            if (!File.Exists(image_destination))
            {
                File.Copy(selected_image_uri.LocalPath, image_destination, true);
            }
            else
            {
                status += "\n\n image slready exists.";
            }
            if (!File.Exists(file_destination))
            {
                File.Copy(selected_file_uri.LocalPath, file_destination, true);
            }
            else
            {
                status += "\n\n file slready exists.";
            }
            if (!string.IsNullOrEmpty(status))
                MessageBox.Show("status: " + status);
        }

        private void txt_priceAdults_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(string.IsNullOrEmpty(txt_priceAdults.Text))
            {
                txt_errorPackageCharge.Text = "Package Charges for Adults is reqired.";
                st_packageCharge.Visibility = Visibility.Visible;
            }
            else if(!Regex.IsMatch(txt_priceAdults.Text, "^[0-9]{1,4}([.]{1}[0-9]{0,2})?$"))
            {
                txt_errorPackageCharge.Text = "Invalid entry.";
                st_packageCharge.Visibility = Visibility.Visible;
            }

            else
            {
                st_packageCharge.Visibility = Visibility.Hidden;
            }
        }

        private void txt_priceKids_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_priceKids.Text))
            {
                txt_errorPackageCharge.Text = "Package Charges for Kids is reqired.";
                st_packageCharge.Visibility = Visibility.Visible;
            }
            else if (!Regex.IsMatch(txt_priceKids.Text, "^[0-9]{1,4}([.]{1}[0-9]{0,2})?$"))
            {
                txt_errorPackageCharge.Text = "Invalid entry.";
                st_packageCharge.Visibility = Visibility.Visible;
            }
             else
            {
                st_packageCharge.Visibility = Visibility.Hidden;
            }
        }

        private void txt_singleRoom_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_singleRoom.Text))
            {
                txt_errorRoomCharge.Text = "Single room charge is reqired.";
                st_roomCharge.Visibility = Visibility.Visible;
            }
            else if (!Regex.IsMatch(txt_singleRoom.Text, "^[0-9]{1,4}([.]{1}[0-9]{0,2})?$"))
            {
                txt_errorRoomCharge.Text = "Invalid Entry.";
                st_roomCharge.Visibility = Visibility.Visible;
            }
            else
            {
                st_roomCharge.Visibility = Visibility.Hidden;
            }
        }

        private void txt_doubleRoom_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_doubleRoom.Text))
            {
                txt_errorRoomCharge.Text = "Double room charge is reqired.";
                st_roomCharge.Visibility = Visibility.Visible;
            }
            else if (!Regex.IsMatch(txt_doubleRoom.Text, "^[0-9]{1,4}([.]{1}[0-9]{0,2})?$"))
            {
                txt_errorRoomCharge.Text = "Invalid Entry.";
                st_roomCharge.Visibility = Visibility.Visible;
            }
            else
            {
                st_roomCharge.Visibility = Visibility.Hidden;
            }
        }

        private void txt_familyRoom_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_familyRoom.Text))
            {
                txt_errorRoomCharge.Text = "Family room charge is reqired.";
                st_roomCharge.Visibility = Visibility.Visible;
            }
            else if (!Regex.IsMatch(txt_familyRoom.Text, "^[0-9]{1,4}([.]{1}[0-9]{0,2})?$"))
            {
                txt_errorRoomCharge.Text = "Invalid entry.";
                st_roomCharge.Visibility = Visibility.Visible;
            }
           
            else
            {
                st_roomCharge.Visibility = Visibility.Hidden;
            }
        }

        private void txt_packageName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_packageName.Text))
            {
                txt_errorPackageName.Text = "Family room charge is reqired.";
                st_packageName.Visibility = Visibility.Visible;
            }
            else
            {
                st_packageName.Visibility = Visibility.Hidden;
            }
        }

        private void txt_packageDurartion_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_packageDurartion.Text))
            {
                txt_errorDuration.Text = "Family room charge is reqired.";
                st_duration.Visibility = Visibility.Visible;
            }
            else
            {
                st_duration.Visibility = Visibility.Hidden;
            }
        }

        private void cmb_package_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmb_package_DropDownOpened(object sender, EventArgs e)
        {

        }
    }
}
