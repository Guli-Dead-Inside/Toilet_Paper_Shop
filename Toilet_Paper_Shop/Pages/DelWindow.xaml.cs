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
using Toilet_Paper_Shop.Model;

namespace Toilet_Paper_Shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для DelWindow.xaml
    /// </summary>
    public partial class DelWindow : Window
    {
        public static ToiletPaperShop_dbEntities db = new ToiletPaperShop_dbEntities();
        public DelWindow()
        {
            InitializeComponent();
            PaperLst.ItemsSource = db.Product.ToList();
        }

        private void DelBTN_Click(object sender, RoutedEventArgs e)
        {
            var q = PaperLst.SelectedItem as Product;
            if (q == null)
            {
                MessageBox.Show("Ничего не выбрано!");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить строку?", "Удалить?", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.Product.Remove(q);
                    db.SaveChanges();
                    PaperLst.ItemsSource = db.Product.ToList();
                    MessageBox.Show("Выполнено!");
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }
    }
}
