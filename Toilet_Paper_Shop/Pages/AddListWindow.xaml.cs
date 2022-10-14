using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Toilet_Paper_Shop.Model;

namespace Toilet_Paper_Shop.Pages
{
    public partial class AddListWindow : Window
    {
        public static ToiletPaper_dbEntities3 db = new Model.ToiletPaper_dbEntities3();
        OpenFileDialog ofdImage = new OpenFileDialog();
        public AddListWindow()
        {
            InitializeComponent();
            foreach (var serv in ProductListWindow.db.TypeProd)
            {
                TypeCB.ItemsSource = db.TypeProd.ToList();
            }
        }

        private void btn_Image_Click(object sender, RoutedEventArgs e)
        {
            ofdImage.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            ofdImage.FilterIndex = 1;
            if (ofdImage.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ofdImage.FileName);
                image.EndInit();
                playim.Source = image;
            }
        }

        private void PriceTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_ImageDel_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();
            image.Freeze();
            playim.Source = image;
        }

        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text == "" || PriceTB.Text == "" || TypeCB.Text == "" || MaterialTB.Text == "" || ArticleTB.Text == "")
            {
                MessageBox.Show("Введите ваши данные!");
            }
            else
            {
                    Product prod = new Product();

                    prod.Name = NameTB.Text;
                    prod.MinCostForAgent = Convert.ToInt32(PriceTB.Text);
                    prod.Id_Material = Convert.ToInt32(MaterialTB.Text);
                    prod.Id_Prod = Convert.ToInt32(ArticleTB.Text);
                    var TypeName = TypeCB.SelectedItem;
                    var temp = ((TypeProd)TypeName).Id;

                    prod.Id_Type = temp;

                    prod.Picture = File.ReadAllBytes(ofdImage.FileName);
                    db.Product.Add(prod);
                    db.SaveChanges();
                    MessageBox.Show("Complete");

            }
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            NameTB.Clear();
            PriceTB.Clear();
            MaterialTB.Clear();
            ArticleTB.Clear();
            BitmapImage image = new BitmapImage();
            image.Freeze();
            playim.Source = image;
        }

        private void TypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var typeName = ((TypeProd)TypeCB.SelectedItem).Id;
            var type = ProductListWindow.db.TypeProd.Where(x => x.Id == typeName).FirstOrDefault();
        }
    }
}
