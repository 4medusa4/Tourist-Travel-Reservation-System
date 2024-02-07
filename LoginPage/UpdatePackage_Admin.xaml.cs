using LoginPage.Db;
using LoginPage.Entities;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LoginPage
{
    public partial class UpdatePackage_Admin : UserControl
    {
        private Package package;
        private string outPutDirectory;
        private Uri selected_image_uri;
        private Uri selected_file_uri;
        private string folder_main;
        private string folder_subImage;
        private string folder_subFile;

        public UpdatePackage_Admin()
        {
            InitializeComponent();
            outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            folder_main = "Documents";
            folder_subImage = "Pack Images";
            folder_subFile = "Pack Files";
        }

        internal Package Package { get => package; set => package = value; }

        private void loadPage(UserControl uc)
        {
            ((Grid)Parent).Children.Insert(1, uc);
            ((Grid)Parent).InvalidateVisual();
            ((Grid)Parent).Children.RemoveAt(((Grid)Parent).Children.IndexOf(this));
        }

        public void setData()
        {
            txtBlock_packId.Text = package.Pack_Id;
            txt_packageName.Text = package.Pack_Name;
            txt_priceAdults.Text = package.Pack_PriceAdults.ToString();
            txt_priceKids.Text = package.Pack_PriceKids.ToString();
            txt_packageDurartion.Text = package.Pack_Duration.ToString();
            txt_singleRoom.Text = package.Pack_SingleRoomCharge.ToString();
            txt_doubleRoom.Text = package.Pack_DoubleRoomCharge.ToString();
            txt_familyRoom.Text = package.Pack_FamilyRoomCharge.ToString();
            string err_msg = "";
            string pack_image_path = new Uri(Path.Combine(outPutDirectory, folder_main, folder_subImage, package.Pack_image)).LocalPath;
            string pack_file_path = new Uri(Path.Combine(outPutDirectory, folder_main, folder_subFile, package.Pack_FileName)).LocalPath;
            if (File.Exists(pack_image_path))
            {
                packageImage.Source = new BitmapImage(new Uri(Path.Combine(pack_image_path)));
            } else
            {
                err_msg = "Package image not found.";
            }
            if (File.Exists(pack_file_path))
            {
                filePath.Text = Path.Combine(pack_file_path, package.Pack_FileName);
            } else
            {
                err_msg = "\nPackage file not found.";
            }
            if (!string.IsNullOrEmpty(err_msg))
                MessageBox.Show(err_msg);
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
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
            if (manager_p.update())
            {
                update_documents();
                MessageBox.Show("Package updated.");
            }
            else
            {
                MessageBox.Show("data not updated!");
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            loadPage(new Home());
        }

        public void update_documents()
        {
            string status = "";
            string pack_image_folder_path = new Uri(Path.Combine(outPutDirectory, folder_main, folder_subImage)).LocalPath;
            string pack_file_folder_path = new Uri(Path.Combine(outPutDirectory, folder_main, folder_subFile)).LocalPath;
            if (!Directory.Exists(pack_image_folder_path))
            {
                Directory.CreateDirectory(pack_image_folder_path);
            }
            if (!Directory.Exists(pack_file_folder_path))
            {
                Directory.CreateDirectory(pack_file_folder_path);
            }
            string image_destination = new Uri(Path.Combine(pack_image_folder_path, this.package.Pack_image)).LocalPath;
            string file_destination = new Uri(Path.Combine(pack_file_folder_path, this.package.Pack_FileName)).LocalPath;
            if (!File.Exists(image_destination))
            {
                File.Copy(selected_image_uri.LocalPath, image_destination, true);
            } else
            {
                status += "image slready exists in the same name.";
            }
            if (!File.Exists(file_destination))
            {
                File.Copy(selected_file_uri.LocalPath, file_destination, true);
            } else
            {
                status += "\n\n file slready exists in the same name.";
            }
            if (!string.IsNullOrEmpty(status))
                MessageBox.Show("status: " + status);
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
                MessageBox.Show("The file format is not supported. Please select a file in .jpg /.png /.jpeg formats.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "PDF Files (*.pdf)|*.pdf";
                if (openFile.ShowDialog().Value)
                {
                    string fileName = openFile.FileName;
                    selected_file_uri = new Uri(openFile.FileName);
                    this.package.Pack_FileName = openFile.SafeFileName;
                    filePath.Text = fileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Select a file in pdf format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    
    }
}
