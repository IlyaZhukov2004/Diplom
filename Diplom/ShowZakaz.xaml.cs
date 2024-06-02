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
using static System.Net.Mime.MediaTypeNames;
using static Diplom.AppData;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для ShowZakaz.xaml
    /// </summary>
    public partial class ShowZakaz : Window
    {
        private static Заказы _currentOrder = new Заказы();

        public ShowZakaz(Заказы current)
        {
            InitializeComponent();
            _currentOrder = current;
            UpLoad();
        }

        public void UpLoad()
        {
            Zakaz.ItemsSource = GetContext().Позиции_заказа.Where(x => x.ID_заказа == _currentOrder.ID).ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
