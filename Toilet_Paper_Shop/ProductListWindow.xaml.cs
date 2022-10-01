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

namespace Toilet_Paper_Shop
{
    /// <summary>
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public static Model.ToiletPaper_dbEntities db = new Model.ToiletPaper_dbEntities();
        public ProductListWindow()
        {
            InitializeComponent();
            PaperLst.ItemsSource = db.Product.ToList();
        }
        private void BLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void BRight_Click(object sender, RoutedEventArgs e)
        {
            if (prod.Count % pageSize == 0)
            {
                if (pageNumber == (prod.Count / pageSize) - 1)
                    return;
            }
            else
            {

                if (pageNumber == (prod.Count / pageSize))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }

        private void CBNumberWrite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageSize = Convert.ToInt32(PaperLst.SelectedItem.ToString());
            RefreshPagination();
            RefreshButtons();
        }
        int pageSize;
        int pageNumber;
        List<Model.Product> prod = db.Product.ToList();
        private void RefreshPagination()
        {
            PaperLst.ItemsSource = null;
            PaperLst.ItemsSource = prod.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
        private void RefreshComboBox()
        {
            PaperLst.Items.Add("10");
            PaperLst.Items.Add("25");
            PaperLst.Items.Add("50");
        }
        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (prod.Count % pageSize == 0)
            {
                for (int i = 0; i < prod.Count / pageSize; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 50;
                    button.Height = 50;
                    button.FontSize = 20;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < prod.Count / pageSize + 1; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 50;
                    button.Height = 50;
                    button.FontSize = 20;
                    WPButtons.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }
    }
}
