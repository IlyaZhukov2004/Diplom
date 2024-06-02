using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
    /// Логика взаимодействия для Texnik.xaml
    /// </summary>
    public partial class Texnik : Window
    {

        private Заявка currentApplications = new Заявка();

        private static Исполнители _currentEmployee;
        private static Оборудование _currentEquipment;
        private static List<Заявка> _applicationsClient;
        private static List<Заявка> _applications = new List<Заявка>();
        private static List<Отчет> _reports = new List<Отчет>();


        public Texnik(Исполнители currentEmployee)
        {
            InitializeComponent();
            Имя_пользователя.Content = currentEmployee.Имя + " " + currentEmployee.Фамилия;
            _currentEmployee = currentEmployee;
            UpLoad();

            Events();
        }

        public void UpLoad()
        {
            _applications = GetContext().Заявка.ToList();
            _reports = GetContext().Отчет.ToList();
            _applicationsClient = GetContext().Заявка.Where(p => p.ID_исполнителя == _currentEmployee.ID).ToList();

            YouSpisok.ItemsSource = _applicationsClient;
            Spisok.ItemsSource = _applications;
            TechSpisok.ItemsSource = GetContext().Оборудование.ToList();
            Zakazi.ItemsSource = GetContext().Заказы.Where(x => x.ID_заявки == currentApplications.ID).ToList();

            //YouSpisok.ItemsSource = GetContext().Заявка.Where(x => x.ID_исполнителя == _currentEmployee.ID).ToList();
            DataContext = currentApplications;
            Кто_выполняет.ItemsSource = GetContext().Исполнители.Where(x => x.Пользователи.ID_роли == 2).ToList();
            //Приоритет.ItemsSource = GetContext().Приоритет_заявки.ToList();
            Статус_заявки.ItemsSource = GetContext().Статус_заявки.ToList();

        }


        public void UpDate(Оборудование current)
        {
            HistoryTech.ItemsSource = _reports.Where(p => p.ID == current.ID).ToList();
        }

        private void Events()
        {
            Назад.Click += (s, e) =>
            {
                SwitchLayers(nameof(UsersRequestLayer));
                Обновить.Visibility = Visibility.Visible;
                wrapDate.Visibility = Visibility.Visible;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
                UpLoad();
            };

            Заявки.Click += (s, e) =>
            {
                SwitchLayers(nameof(UsersRequestLayer));
                Обновить.Visibility = Visibility.Visible;
                wrapDate.Visibility = Visibility.Visible;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
                UpLoad();
            };

            Техника.Click += (s, e) =>
            {
                SwitchLayers(nameof(TechincLayer));
                wrapDate.Visibility = Visibility.Hidden;
                Обновить.Visibility = Visibility.Hidden;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
                UpLoad();
            };

            Твои_заявки.Click += (s, e) =>
            {
                SwitchLayers(nameof(YourRequests));
                Обновить.Visibility = Visibility.Visible;
                wrapDate.Visibility = Visibility.Visible;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
                UpLoad();
            };


        }


        public void Удалить_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Removing = (sender as Button)?.DataContext as Заказы;
                var positions = GetContext().Позиции_заказа.Where(u => u.ID_заказа == Removing.ID).ToList();

                try
                {
                    GetContext().Позиции_заказа.RemoveRange(positions);
                    GetContext().SaveChanges();
                    GetContext().Заказы.Remove(Removing);
                    GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    UpLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) //поиск
        {
            try
            {
                Spisok.ItemsSource = GetContext().Заявка.Where(Item => Item.Описание_проблемы.Contains(Поиск.Text)
                || Item.Исполнители.Фамилия.Contains(Поиск.Text) || Item.Исполнители.Имя.Contains(Поиск.Text) || Item.Исполнители.Отчество.Contains(Поиск.Text)
                || Item.Работники.Фамилия.Contains(Поиск.Text) || Item.Работники.Имя.Contains(Поиск.Text) || Item.Работники.Отчество.Contains(Поиск.Text)
                || Item.Оборудование.Вид_оборудования.Вид.Contains(Поиск.Text)).ToList();
                YouSpisok.ItemsSource = GetContext().Заявка.Where(Item => Item.Описание_проблемы.Contains(Поиск.Text)
                || Item.Исполнители.Фамилия.Contains(Поиск.Text) || Item.Исполнители.Имя.Contains(Поиск.Text) || Item.Исполнители.Отчество.Contains(Поиск.Text)
                || Item.Работники.Фамилия.Contains(Поиск.Text) || Item.Работники.Имя.Contains(Поиск.Text) || Item.Работники.Отчество.Contains(Поиск.Text)
                || Item.Оборудование.Вид_оборудования.Вид.Contains(Поиск.Text)).ToList();
                TechSpisok.ItemsSource = GetContext().Оборудование.Where(Item => Item.Индивидуальный_номер.Contains(Поиск.Text) || Item.Вид_оборудования.Вид.Contains(Поиск.Text)).ToList();
                HistoryTech.ItemsSource = _reports.Where(Item => Item.Заявка.Работники.Фамилия.Contains(Поиск.Text)
                || Item.Заявка.Работники.Имя.Contains(Поиск.Text)
                || Item.Заявка.Работники.Отчество.Contains(Поиск.Text)
                || Item.Заявка.Исполнители.Фамилия.Contains(Поиск.Text)
                || Item.Заявка.Исполнители.Имя.Contains(Поиск.Text)
                || Item.Заявка.Исполнители.Отчество.Contains(Поиск.Text)
                || Item.Заявка.Описание_проблемы.Contains(Поиск.Text)
                || Item.Результат_работы.Contains(Поиск.Text)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void Выйти_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            Hide();
        }


        private void SwitchLayers(string LayerName)
        {
            List<Grid> layers = new List<Grid>()
            {
                UsersRequestLayer,
                RegZaivka,
                TechincLayer,
                History,
                YourRequests
            };

            foreach (var layer in layers)
            {
                layer.Visibility = (layer.Name == LayerName) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            SwitchLayers(nameof(RegZaivka));
            Обновить.Visibility = Visibility.Hidden;
            wrapDate.Visibility = Visibility.Hidden;
            Поиск.Visibility = Visibility.Hidden;
            Поиск.Clear();
            DpkStart.SelectedDate = null;
            DpkEnd.SelectedDate = null;

            Заявка current = (sender as Button)?.DataContext as Заявка;
            if (current != null)
            {
                currentApplications = current;
                UpLoad();
            }

        }

        private void Edit2(object sender, RoutedEventArgs e)
        {
            SwitchLayers(nameof(History));
            wrapDate.Visibility = Visibility.Visible;
            Поиск.Visibility = Visibility.Visible;
            Поиск.Clear();
            DpkStart.SelectedDate = null;
            DpkEnd.SelectedDate = null;

            Оборудование otch = (sender as Button)?.DataContext as Оборудование;
            _currentEquipment = otch;
            if (otch != null)
            {
                UpDate(_currentEquipment);
            }
        }

        private void Отпр_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var req = currentApplications;
                req.Дата_окончания = (DateTime)Дата_оконачания.SelectedDate;
                //req.Отчет.П = Причина_поломки.Text;
                req.ID_статус_заявки = Статус_заявки.SelectedIndex + 1;
                req.ID_приоритет = 3;
                req.ID_исполнителя = Кто_выполняет.SelectedIndex + 1;



                GetContext().SaveChanges();
                UpLoad();
                MessageBox.Show("Отправлено");
                SwitchLayers(nameof(UsersRequestLayer));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Заказать_Click(object sender, RoutedEventArgs e)
        {
            Material material = new Material(currentApplications);
            material.ShowDialog();
            UpLoad();
        }

        private void Отчет_Click(object sender, RoutedEventArgs e)
        {
            Заявка appl = (sender as Button)?.DataContext as Заявка;
            if (appl != null)
            {
                Otchet otchet = new Otchet(appl);
                otchet.Show();
                UpLoad();
            }
        }

        private void Развернуть_Click(object sender, RoutedEventArgs e)
        {
            Заказы current = (sender as Button)?.DataContext as Заказы;
            if (current != null)
            {
                ShowZakaz wnd = new ShowZakaz(current);
                wnd.Show();
            }
        }

        private void Применить_Click(object sender, RoutedEventArgs e)
        {
            UpLoad();
            if (DpkStart.SelectedDate != null && DpkEnd.SelectedDate != null
                && DpkEnd.SelectedDate.Value >= DpkStart.SelectedDate.Value)
            {
                _applications = _applications.Where(p => p.Дата_начала >= DpkStart.SelectedDate.Value && p.Дата_начала <= DpkEnd.SelectedDate.Value).ToList();
                _applicationsClient = _applicationsClient.Where(p => p.Дата_начала >= DpkStart.SelectedDate.Value && p.Дата_начала <= DpkEnd.SelectedDate.Value).ToList();
                _reports = _reports.Where(p => p.Дата_начала >= DpkStart.SelectedDate.Value && p.Дата_начала <= DpkEnd.SelectedDate.Value).ToList();

                Spisok.ItemsSource = _applications;
                YouSpisok.ItemsSource = _applicationsClient;
                HistoryTech.ItemsSource = _reports;
            }
            else
            {
                MessageBox.Show("Необходимо выбрать корректный диапазон дат!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void Очистить_Click(object sender, RoutedEventArgs e)
        {
            Поиск.Clear();
            DpkStart.SelectedDate = null;
            DpkEnd.SelectedDate = null;
            UpLoad();
            if (_currentEquipment != null)
            {
                UpDate(_currentEquipment);
            }
        }

        private void Обновить_Click(object sender, RoutedEventArgs e)
        {
            UpLoad();
        }
    }
}

