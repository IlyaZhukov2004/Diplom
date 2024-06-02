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
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {

        private static Работники _currentRab;
        private static Исполнители _currentEmployee;
        private static Пользователи _currentPol;
        private static Роли _currentRole;
        private static List<Заявка> _applicationsClient;
        private List<Заявка> currentApplications = new List<Заявка>();
        private List<Работники> currentRab = new List<Работники>();
        private List<Пользователи> currentPol = new List<Пользователи>();


        public Administrator(Исполнители currentEmployee)
        {
            InitializeComponent();
            Имя_пользователя.Content = currentEmployee.Имя + " " + currentEmployee.Фамилия;
            _currentEmployee = currentEmployee;
            UpLoad();

            Events();
        }

        public void UpLoad()
        {
            //RegUseSpisok.ItemsSource = GetContext().Исполнители.ToList();
            //RegUseSpisok.ItemsSource = GetContext().Работники.ToList();
            RegUseSpisok.ItemsSource = GetContext().Пользователи.ToList();
            Роль.ItemsSource = GetContext().Роли.ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) //поиск
        {
            try
            {
                RegUseSpisok.ItemsSource = GetContext().Пользователи.Where(Item => Item.Логин.Contains(Поиск.Text) || Item.Пароль.Contains(Поиск.Text) || Item.Роли.Роль.Contains(Поиск.Text)).ToList();
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
                RegUseLayer,
                NewUseLayer,
            };

            foreach (var layer in layers)
            {
                layer.Visibility = (layer.Name == LayerName) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void Events()
        {
            RegUse.Click += (s, e) =>
            {
                SwitchLayers(nameof(RegUseLayer));
            };

            NewUse.Click += (s, e) =>
            {
                SwitchLayers(nameof(NewUseLayer));
            };
        }

        private void Отпр_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item2 = new Пользователи
                {
                    ID = GetContext().Пользователи.ToList().Max(point => point.ID),
                    Логин = Логин.Text,
                    Пароль = Пароль.Text,
                    ID_роли = Роль.SelectedIndex + 1,
                    //ID_роли = 3
                };
                GetContext().Пользователи.Add(item2);
                GetContext().SaveChanges();
                UpLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                var item = new Работники
                {
                    ID = GetContext().Работники.ToList().Max(point => point.ID),
                    Фамилия = Фамилия.Text,
                    Имя = Имя.Text,
                    Отчество = Отчетсво.Text,
                    Телефон = Телефон.Text,
                    Почта = Почта.Text,
                    ID_пользователя = GetContext().Пользователи.ToList().Max(point => point.ID),
                };


                GetContext().Работники.Add(item);
                GetContext().SaveChanges();
                MessageBox.Show("Отправлено");

                Фамилия.Text = "";
                Имя.Text = "";
                Отчетсво.Text = "";
                Телефон.Text = "";
                Почта.Text = "";
                Логин.Text = "";
                Пароль.Text = "";
                Роль.SelectedIndex = 0;
                UpLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Удалить_Click(object sender, RoutedEventArgs e)
        {

            //if (RegUseSpisok.SelectedItem is Исполнители current)
            //{
            //    var RemovingRab = (sender as Button)?.DataContext as Исполнители;
            //    var userId = GetContext().Пользователи.Where(u => u.ID == RemovingRab.ID_пользователя).FirstOrDefault();

            //    if (MessageBox.Show($"Вы точно хотите удалить пользователя?", "Внимание",
            //    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //    {
            //        try
            //        {
            //            GetContext().Исполнители.Remove(RemovingRab);
            //            GetContext().SaveChanges();

            //            UpLoad();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message.ToString());
            //        }

            //        try
            //        {
            //            GetContext().Пользователи.Remove(userId);
            //            GetContext().SaveChanges();
            //            MessageBox.Show("Данные удалены");
            //            UpLoad();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message.ToString());
            //        }

            //    }
            //}
        }

    }

}

