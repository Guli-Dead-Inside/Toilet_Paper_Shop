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
        public static ToiletPaper_dbEntities1 db = new Model.ToiletPaper_dbEntities1();
        //public static ToiletPaper_dbEntities db = new Model.ToiletPaper_dbEntities();
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
                PriceTB.Text = Convert.ToString(prod.MinCostForAgent);
                prod.Id_Material = Convert.ToInt32(MaterialTB.Text);
                ArticleTB.Text = Convert.ToString(prod.Id_Prod);
            
                prod.Picture = File.ReadAllBytes(ofdImage.FileName);
                db.Product.Add(prod);
              
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Такие данные уже существует!");
                }
                finally
                {
                    MessageBox.Show("Complete!");
                }
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
            var typeName = ((TypeProd)TypeCB.SelectedItem).NameType;
            var type = ProductListWindow.db.TypeProd.Where(x => x.NameType == typeName).FirstOrDefault();
        }
    }
}
