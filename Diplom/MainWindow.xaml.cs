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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Diplom.AppData;


namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Пользователи currentUser = new Пользователи();
        private static Работники currentRab = new Работники();
        private static Исполнители currentEmployee = new Исполнители();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Вход_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentUser = GetContext().Пользователи.Where(x => x.Логин == tBoxLogin.Text && x.Пароль == pBoxPassword.Password).FirstOrDefault();
                if (currentUser == null)
                {
                    MessageBox.Show("Такого пользователя нет", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (currentUser.ID_роли)
                    {
                        case 3:
                            currentEmployee = GetContext().Исполнители.Where(x => x.ID_пользователя == currentUser.ID).FirstOrDefault();
                            var managersList = new Meneger(currentEmployee);
                            managersList.Owner = this;
                            managersList.Title = currentEmployee.Имя + " " + currentEmployee.Фамилия;
                            managersList.Show();
                            Hide();
                            break;
                        case 2:
                            currentEmployee = GetContext().Исполнители.Where(x => x.ID_пользователя == currentUser.ID).FirstOrDefault();
                            var employeeWindow = new Texnik(currentEmployee);
                            employeeWindow.Owner = this;
                            employeeWindow.Title = currentEmployee.Имя + " " + currentEmployee.Фамилия;
                            employeeWindow.Show();
                            Hide();
                            break;
                        case 1:
                            currentRab = GetContext().Работники.Where(x => x.ID_пользователя == currentUser.ID).FirstOrDefault();
                            var clientWindow = new Klient(currentRab);
                            clientWindow.Owner = this;
                            clientWindow.Title = currentRab.Имя + " " + currentRab.Фамилия;
                            clientWindow.Show();
                            Hide();
                            break;
                        case 4:
                            currentEmployee = GetContext().Исполнители.Where(x => x.ID_пользователя == currentUser.ID).FirstOrDefault();
                            var adminWindow = new Administrator(currentEmployee);
                            adminWindow.Owner = this;
                            adminWindow.Title = currentEmployee.Имя + " " + currentEmployee.Фамилия;
                            adminWindow.Show();
                            Hide();
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString() + "Критическая работа приложения", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
