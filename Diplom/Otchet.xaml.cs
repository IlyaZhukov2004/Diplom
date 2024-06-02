using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Otchet.xaml
    /// </summary>
    public partial class Otchet : Window
    {
        private static Заявка _currentAppl = new Заявка();
        private static Работники _currentClient = new Работники();
        private static Исполнители _currentEmpl = new Исполнители();
        private static Материалы _currentDetail = new Материалы();
        private static Отчет _currentReport = new Отчет();

        public Otchet(Заявка current)
        {
            InitializeComponent();
            _currentAppl = current;
            UpLoad();
        }

        public void UpLoad()
        {
            Zakazy.ItemsSource = GetContext().Заказы.Where(x => x.ID_заявки == _currentAppl.ID).ToList();
            _currentReport = GetContext().Отчет.Where(x => x.ID_заявки == _currentAppl.ID).First();
            _currentClient = GetContext().Работники.Where(x => x.ID == _currentReport.ID_клиента).First();
            _currentEmpl = GetContext().Исполнители.Where(x => x.ID == _currentReport.ID_исполнителя).First();
            DataContext = _currentReport;
            клиент.Text = _currentClient.Фамилия + " " + _currentClient.Имя + " " + _currentClient.Отчество;
            исполнитель.Text = _currentEmpl.Фамилия + " " + _currentEmpl.Имя + " " + _currentEmpl.Отчество;
        }

        private void Отправить_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item2 = new Отчет
                {
                    Результат_работы = результат.Text,
                    Дата_начала = (DateTime)начало.SelectedDate,
                    Дата_окончания = (DateTime)завершение.SelectedDate,
                };
                GetContext().SaveChanges();
                MessageBox.Show("Сохранено");
                UpLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Отмена_Click(object sender, RoutedEventArgs e)
        {
            Hide();
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

        private void Выгрузка_Click(object sender, RoutedEventArgs e)
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

                string reportID = "Код отчета: " + кодОтчета.Text;
                string applicationID = "Код заявки: " + кодЗаявки.Text;
                string status = "Статус: " + статус.Text;
                string oborudovanie = "Оборудование: " + оборудоваание.Text;
                string serialNumber = "Серийный номер: " + серийныйНомер.Text;
                string problem = "Описание проблемы: " + описаниеПроблемы.Text;
                string client = "Клиент: " + клиент.Text;
                string executor = "Исполнитель: " + исполнитель.Text;
                string start = "Дата начала: " + начало.Text;
                string finish = "Дата завершения: " + завершение.Text;
                string jobResult = "Реузультат работы: " + результат.Text;


                document.Add(new iTextSharp.text.Paragraph(reportID, font1));
                document.Add(new iTextSharp.text.Paragraph(applicationID, font1));
                document.Add(new iTextSharp.text.Paragraph(client, font1));
                document.Add(new iTextSharp.text.Paragraph(executor, font1));
                document.Add(new iTextSharp.text.Paragraph(start, font1));
                document.Add(new iTextSharp.text.Paragraph(finish, font1));
                document.Add(new iTextSharp.text.Paragraph(status, font1));
                document.Add(new iTextSharp.text.Paragraph(oborudovanie, font1));
                document.Add(new iTextSharp.text.Paragraph(serialNumber, font1));
                document.Add(new iTextSharp.text.Paragraph(problem, font1));
                document.Add(new iTextSharp.text.Paragraph(jobResult, font1));


                float[] widths = new float[] { 4f, 4f, 4f };


                PdfPCell cell = new PdfPCell(new Phrase("Заказы"));
                var items = GetContext().Заказы.Where(x => x.ID_заявки == _currentAppl.ID).ToList();

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
