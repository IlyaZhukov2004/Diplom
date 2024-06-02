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
using static System.Net.Mime.MediaTypeNames;


namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для Material.xaml
    /// </summary>
    public partial class Material : Window
    {
        //private static Работники _currentEmployee;
        //private static Заявка _currentAppl;
        //private static List<Заявка> _applicationsClient;
        private static Материалы _currentMat;
        private static List<Материалы> _applicationsMat;
        private static Заявка _current = new Заявка();
        private static Заказы _currentOrder = new Заказы();
        private static Позиции_заказа _detailToEdit = new Позиции_заказа();
        private static List<Позиции_заказа> listCart = new List<Позиции_заказа>();
        public int amount = 0;
        public int ID_Amount;

        public Material(Заявка current)
        {
            InitializeComponent();
            _current = current;
            UpLoad();
            CreateOrder();
        }

        public void UpLoad()
        {
            Деталь.ItemsSource = GetContext().Материалы.ToList();
            dgPartCosts.ItemsSource = GetContext().Позиции_заказа.Where(x => x.ID_заказа == _currentOrder.ID).ToList();
            listCart = GetContext().Позиции_заказа.Where(x => x.ID_заказа == _currentOrder.ID).ToList();
            lbAmount.Content = $"{amount}";
        }

        public void CreateOrder()
        {
            _currentOrder.ID_заявки = _current.ID;
            _currentOrder.Дата_заказа = DateTime.Now;
            _currentOrder.Количество_деталей = amount;

            GetContext().Заказы.Add(_currentOrder);
            GetContext().SaveChanges();
        }

        private void Отмена_Click(object sender, RoutedEventArgs e)
        {
            var listToRemove = GetContext().Позиции_заказа.Where(u => u.ID_заказа == _currentOrder.ID).ToList();

            try
            {
                GetContext().Позиции_заказа.RemoveRange(listToRemove);
                GetContext().SaveChanges();

                GetContext().Заказы.Remove(_currentOrder);
                GetContext().SaveChanges();
                MessageBox.Show("Заказ отменен");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Hide();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Позиции_заказа order = (sender as Button)?.DataContext as Позиции_заказа;
            if (order != null)
            {
                listCart.Remove(order);
                GetContext().Позиции_заказа.Remove(order);
                GetContext().SaveChanges();
                UpLoad();
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Позиции_заказа order = (sender as Button)?.DataContext as Позиции_заказа;
                if (order != null)
                {
                    order.Количество++;
                    amount++;
                    GetContext().SaveChanges();
                    UpLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Позиции_заказа order = (sender as Button)?.DataContext as Позиции_заказа;
                if (order != null)
                {
                    if (order.Количество > 1)
                    {
                        order.Количество--;
                        GetContext().SaveChanges();
                        amount--;
                        UpLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Заказать_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(listCart.Count() < 1)
                {
                    MessageBox.Show("Нельзя оформить пустой заказ");
                }
                else
                {
                    _currentOrder.Количество_деталей = listCart.Count();
                    _currentOrder.Дата_заказа = DateTime.Now;
                    _currentOrder.Статус_заказа = "На рассмотрении";

                    GetContext().SaveChanges();
                    MessageBox.Show("Заказ оформлен");
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Добавить_Click(object sender, RoutedEventArgs e)
        {
            if (Количество.Text != "")
            {
                if (Деталь.Text != "")
                {
                    try
                    {
                        Позиции_заказа _detailToAdd = new Позиции_заказа();
                        _detailToAdd.ID_материала = Деталь.SelectedIndex + 1;
                        _detailToAdd.ID_заказа = _currentOrder.ID;
                        _detailToAdd.Количество = Convert.ToInt32(Количество.Text);


                        ID_Amount = listCart.Where(x => x.ID_материала == _detailToAdd.ID_материала && x.ID_заказа == _currentOrder.ID).Count();
                        if (ID_Amount == 0)
                        {
                            listCart.Add(_detailToAdd);
                            GetContext().Позиции_заказа.Add(_detailToAdd);
                            GetContext().SaveChanges();
                        }
                        else
                        {
                            _detailToEdit = listCart.Where(x => x.ID_материала == _detailToAdd.ID_материала && x.ID_заказа == _currentOrder.ID).First();
                            _detailToEdit.Количество++;
                            GetContext().SaveChanges();
                        }
                        amount++;
                        UpLoad();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Выберите деталь!!!!!!!");
                }
            }
            else
            {
                MessageBox.Show("Заполните количество!!!!!!!");
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // крестик окна
        {
            var listToRemove = GetContext().Позиции_заказа.Where(u => u.ID_заказа == _currentOrder.ID).ToList();

            try
            {
                GetContext().Позиции_заказа.RemoveRange(listToRemove);
                GetContext().SaveChanges();

                GetContext().Заказы.Remove(_currentOrder);
                GetContext().SaveChanges();
                MessageBox.Show("Заказ отменен");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Hide();
        }
    }
}
