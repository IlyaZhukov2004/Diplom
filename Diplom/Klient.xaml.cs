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
using static Diplom.AppData;


namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для Klient.xaml
    /// </summary>
    public partial class Klient : Window
    {
        private static Работники _currentRab;
        private static List<Заявка> _applicationsClient;
        private List<Заявка> currentApplications = new List<Заявка>();

        public Klient(Работники currentRab)
        {
            InitializeComponent();
            Имя_пользователя.Content = currentRab.Имя + " " + currentRab.Фамилия;
            _currentRab = currentRab;
            UpLoad();

            Events();

        }

        public void UpLoad()
        {
            currentApplications = GetContext().Заявка.Where(p => p.ID_клиента == _currentRab.ID).ToList();
            Spisok.ItemsSource = currentApplications;
            Тип_техники.ItemsSource = GetContext().Оборудование.ToList();
            Тип_техники.SelectedIndex = 0;
        }

        private void SwitchLayers(string LayerName)
        {
            List<Grid> layers = new List<Grid>()
            {
                NewZaivka,
                ZaivkaSpisok
            };

            foreach (var layer in layers)
            {
                layer.Visibility = (layer.Name == LayerName) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void Events()
        {
            Заявка.Click += (s, e) =>
            {
                SwitchLayers(nameof(NewZaivka));

            };

            Отпр.Click += (s, e) =>
            {
                SwitchLayers(nameof(ZaivkaSpisok));
                UpLoad();
            };
        }

        private void Выйти_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            Hide();
        }

        private void Отпр_Click(object sender, RoutedEventArgs e)
        {
            if (Опис_проб.Text == "")
            {
                MessageBox.Show("Заполните поле!!!");
            }
            else
            {
                try
                {
                    var item = new Заявка
                    {
                        ID_оборудования = Тип_техники.SelectedIndex + 1,
                        ID_клиента = _currentRab.ID,
                        ID_статус_заявки = 3,
                        Описание_проблемы = Опис_проб.Text,
                        Дата_начала = DateTime.Now,
                    };

                    GetContext().Заявка.Add(item);
                    GetContext().SaveChanges();

                    var item1 = new Отчет
                    {
                        ID_заявки = GetContext().Заявка.Max(point => point.ID),
                        ID_клиента = _currentRab.ID,
                        Дата_начала = DateTime.Now,
                    };
                    MessageBox.Show("Отправлено");

                    Опис_проб.Text = "";
                    Тип_техники.SelectedIndex = 0;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

    }
}
