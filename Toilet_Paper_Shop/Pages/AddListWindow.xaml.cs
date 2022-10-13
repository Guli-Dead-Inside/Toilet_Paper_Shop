using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для AddListWindow.xaml
    /// </summary>
    public partial class AddListWindow : Window
    {
        public static ToiletPaper_dbEntities db = new Model.ToiletPaper_dbEntities();
        OpenFileDialog ofdImage = new OpenFileDialog();
        public AddListWindow()
        {
            InitializeComponent();
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
            if (NameTB.Text == "" || PriceTB.Text == "" || TypeTB.Text == "" || MaterialTB.Text == "" || ArticleTB.Text == "")
            {
                MessageBox.Show("Введите ваши данные!");
            }
            else
            {
                Product prod = new Product();
                Material mat = new Material();
                TypeProd type = new TypeProd();
                prod.Name = NameTB.Text;
                PriceTB.Text = Convert.ToString(prod.MinCostForAgent);
                mat.Name = MaterialTB.Text;
                ArticleTB.Text = Convert.ToString(prod.Id_Prod);
                type.NameType = TypeTB.Text;
                ProductListWindow.db.Product.Add(prod);
                ProductListWindow.db.Material.Add(mat);
                ProductListWindow.db.TypeProd.Add(type);
                try
                {
                    ProductListWindow.db.SaveChanges();
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

        
    }
}
