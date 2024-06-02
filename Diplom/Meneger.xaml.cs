using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
using static Diplom.AppData;
using System.IO;
using System.Data;


namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для Meneger.xaml
    /// </summary>
    public partial class Meneger : Window
    {

        private static Исполнители _currentEmployee;
        private static Пользователи _currentPol;
        //private static Роль _currentRole;
        private static List<Заявка> _applications;
        private static List<Заказы> _order;
        private Заявка currentApplications = new Заявка();
        private List<Заказы> _currentorder = new List<Заказы>();
        private List<Работники> currentRab = new List<Работники>();
        private static Работники _currentRab;
        //private List<Исполнители> _Executors = new List<Исполнители>();
        private List<Приоритет_заявки> _Priorities = new List<Приоритет_заявки>();
        private List<Статус_заявки> _Statuses = new List<Статус_заявки>();
        private List<Пользователи> currentPol = new List<Пользователи>();
        private List<Вид_оборудования> vidObor = new List<Вид_оборудования>();
        private static Материалы _currentDetail = new Материалы();



        public Meneger(Исполнители currentEmployee)
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
            _order = GetContext().Заказы.ToList();
            Spisok.ItemsSource = _applications;
            HistorySpisok.ItemsSource = _order;

            DataContext = currentApplications;
            Кто_выполняет.ItemsSource = GetContext().Исполнители.Where(x => x.Пользователи.ID_роли == 2).ToList();
            Приоритет.ItemsSource = GetContext().Приоритет_заявки.ToList();
            Статус_заявки.ItemsSource = GetContext().Статус_заявки.ToList();
            RegUseSpisok.ItemsSource = GetContext().Исполнители.ToList();
            Роль.ItemsSource = GetContext().Роли.ToList();

            Zakazi.ItemsSource = GetContext().Заказы.Where(x => x.ID_заявки == currentApplications.ID && x.Количество_деталей != 0 && x.Статус_заказа == "На рассмотрении").ToList();
            Тип_техники.ItemsSource = GetContext().Оборудование.ToList();
        }

        private void Events()
        {
            Назад.Click += (s, e) =>
            {
                SwitchLayers(nameof(UsersRequestLayer));
                Обновить.Visibility = Visibility.Visible;
                Выгрузить.Visibility = Visibility.Hidden;
                wrapDate.Visibility = Visibility.Visible;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
                UpLoad();
            };

            Список_заявок.Click += (s, e) =>
            {
                SwitchLayers(nameof(UsersRequestLayer));
                Обновить.Visibility = Visibility.Visible;
                Выгрузить.Visibility = Visibility.Hidden;
                wrapDate.Visibility = Visibility.Visible;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
                UpLoad();
            };

            Новая_техника.Click += (s, e) =>
            {
                SwitchLayers(nameof(NewTechinc));
                Обновить.Visibility = Visibility.Hidden;
                Выгрузить.Visibility = Visibility.Hidden;
                wrapDate.Visibility = Visibility.Hidden;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
                UpLoad();
            };

            RegUse.Click += (s, e) =>
            {
                SwitchLayers(nameof(RegUseLayer));
                Обновить.Visibility = Visibility.Visible;
                Выгрузить.Visibility = Visibility.Hidden;
                wrapDate.Visibility = Visibility.Hidden;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
            };

            NewUse.Click += (s, e) =>
            {
                SwitchLayers(nameof(NewUseLayer));
                Обновить.Visibility = Visibility.Hidden;
                Выгрузить.Visibility = Visibility.Hidden;
                wrapDate.Visibility = Visibility.Hidden;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
            };

            History.Click += (s, e) =>
            {
                SwitchLayers(nameof(HistoryLayer)); 
                Обновить.Visibility = Visibility.Visible;
                Выгрузить.Visibility = Visibility.Visible;
                wrapDate.Visibility = Visibility.Visible;
                Поиск.Visibility = Visibility.Visible;
                Поиск.Clear();
                DpkStart.SelectedDate = null;
                DpkEnd.SelectedDate = null;
            };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) //поиск
        {
            try
            {
                Spisok.ItemsSource = GetContext().Заявка.Where(Item => Item.Описание_проблемы.Contains(Поиск.Text)
                    || Item.Исполнители.Фамилия.Contains(Поиск.Text) || Item.Исполнители.Имя.Contains(Поиск.Text) || Item.Исполнители.Отчество.Contains(Поиск.Text)
                    || Item.Работники.Фамилия.Contains(Поиск.Text) || Item.Работники.Имя.Contains(Поиск.Text) || Item.Работники.Отчество.Contains(Поиск.Text)
                    || Item.Оборудование.Вид_оборудования.Вид.Contains(Поиск.Text)).ToList();
                RegUseSpisok.ItemsSource = GetContext().Пользователи.Where(Item => Item.Логин.Contains(Поиск.Text) 
                    || Item.Пароль.Contains(Поиск.Text) || Item.Роли.Роль.Contains(Поиск.Text)).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput3(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-z][-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w]+");
            e.Handled = regex.IsMatch(e.Text);
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
                RegUseLayer,
                NewUseLayer,
                NewTechinc,
                HistoryLayer
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
            Выгрузить.Visibility = Visibility.Hidden;
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
            //SwitchLayers(nameof(History));
        }

        private void Отпр_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var req = currentApplications;
                req.ID_приоритет = Приоритет.SelectedIndex + 1;
                req.ID_статус_заявки = Статус_заявки.SelectedIndex + 1;
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

        private void Добавить_Click(object sender, RoutedEventArgs e)
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
                var item = new Исполнители
                {
                    ID = GetContext().Работники.ToList().Max(point => point.ID),
                    Фамилия = Фамилия.Text,
                    Имя = Имя.Text,
                    Отчество = Отчетсво.Text,
                    Телефон = Телефон.Text,
                    Почта = Почта.Text,
                    ID_пользователя = GetContext().Пользователи.ToList().Max(point => point.ID),
                };


                GetContext().Исполнители.Add(item);
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

            if (RegUseSpisok.SelectedItem is Исполнители current)
            {
                var RemovingRab = (sender as Button)?.DataContext as Исполнители;
                var userId = GetContext().Пользователи.Where(u => u.ID == RemovingRab.ID_пользователя).FirstOrDefault();

                if (MessageBox.Show($"Вы точно хотите удалить пользователя?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        GetContext().Исполнители.Remove(RemovingRab);
                        GetContext().SaveChanges();

                        UpLoad();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    try
                    {
                        GetContext().Пользователи.Remove(userId);
                        GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        UpLoad();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                }
            }
        }

        private void Сохр_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item2 = new Оборудование
                {
                    ID = GetContext().Пользователи.ToList().Max(point => point.ID),
                    Индивидуальный_номер = Индивидуальный.Text,
                    ID_вид_оборудования = Тип_техники.SelectedIndex + 1,
                    //Описание_обородования = Опис_тех.Text,
                };
                GetContext().Оборудование.Add(item2);
                GetContext().SaveChanges();
                MessageBox.Show("Отправлено");

                Индивидуальный.Text = "";
                Тип_техники.SelectedIndex = 0;
                UpLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Заказы current = (sender as Button)?.DataContext as Заказы;
            if (current != null)
            {
                current.Статус_заказа = "Одобрен";
                GetContext().SaveChanges();
                MessageBox.Show("Заказ одобрен");
                UpLoad();
            }
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            Заказы current = (sender as Button)?.DataContext as Заказы;
            if (current != null)
            {
                current.Статус_заказа = "Отклонен";
                MessageBox.Show("Заказ отклонен");
                GetContext().SaveChanges();
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
                _order = _order.Where(p => p.Дата_заказа >= DpkStart.SelectedDate.Value && p.Дата_заказа <= DpkEnd.SelectedDate.Value).ToList();

                Spisok.ItemsSource = _applications;
                HistorySpisok.ItemsSource = _order;
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
        }

        private void Обновить_Click(object sender, RoutedEventArgs e)
        {
            UpLoad();
        }

        private void Выгрузить_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "Text documents (.pdf)|*.pdf";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));

                string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font1 = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);

                document.Open();

                document.AddTitle("ОТЧЕТ");
                document.NewPage();
                document.Add(new iTextSharp.text.Paragraph("ОТЧЕТ", font1));

                float[] widths = new float[] { 4f, 4f, 4f };


                PdfPCell cell = new PdfPCell(new Phrase("Заказы"));
                var items = GetContext().Заказы.Where(x => x.Дата_заказа == DpkStart.SelectedDate).ToList();

                foreach (var item in items)
                {
                    string zNum = "Номер: " + item.ID;
                    string zDate = $"Дата: {item.Дата_заказа:dd.MM.yyyy}";
                    string zAmount = "Количество деталей: " + item.Количество_деталей;

                    document.Add(new iTextSharp.text.Paragraph(zNum, font1));
                    document.Add(new iTextSharp.text.Paragraph(zDate, font1));
                    document.Add(new iTextSharp.text.Paragraph(zAmount, font1));

                    PdfPTable table = new PdfPTable(3);
                    table.SetWidths(widths);
                    table.WidthPercentage = 100;
                    table.AddCell(new Phrase("Деталь", font1));
                    table.AddCell(new Phrase("Описание", font1));
                    table.AddCell(new Phrase("Количество", font1));
                    var items1 = GetContext().Позиции_заказа.Where(x => x.ID_заказа == item.ID).ToList();

                    foreach (var item1 in items1)
                    {
                        _currentDetail = GetContext().Материалы.Where(x => x.ID == item1.ID_материала).First();
                        table.AddCell(new Phrase(_currentDetail.Деталь, font1));
                        table.AddCell(new Phrase(_currentDetail.Описание, font1));
                        table.AddCell(new Phrase(item1.Количество.ToString(), font1));
                    }
                    document.Add(table);
                }

                document.Close();
                MessageBox.Show("Документ записан");
            }
        }
    }
}

