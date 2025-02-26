using ClassModules;
using ClassConnection;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
namespace UP.Classes
{

    public class Report
    {
        public class ReportData
        {
            public string Komnata { get; set; } // Комната
            public string FIO { get; set; } // ФИО
            public string DataRozhdeniya { get; set; } // Дата рождения
            public string Gruppa { get; set; } // Группа
            public string KontaktnyNomer { get; set; } // Контактный номер
            public string KontaktyRoditeley { get; set; } // Контакты родителей
            public string DataPervogoZaseleniya { get; set; } // Дата первого заселения
            public string ObshezhitiePrimechanie { get; set; } // Общежитие (примечание)
            public string Otchislenie { get; set; } // Отчисление
        }
        public static List<ReportData> GetData()
        {
            // Создайте экземпляр класса Connection
            Connection dbConnection = new Connection(); //Создаем экземпляр

            // Подключитесь к базе данных и загрузите данные
            Connection.Connect(); //Вызываем статический метод Connect.  Этот метод устанавливает IsConnected = true
            dbConnection.LoadData(Connection.Tables.Obshaga); // Загрузите данные из таблицы Obshaga
            dbConnection.LoadData(Connection.Tables.Students); // Загрузите данные из таблицы Students
            dbConnection.LoadData(Connection.Tables.Rooms); // Загрузите данные из таблицы Rooms

            // Создайте список для хранения данных отчета
            List<ReportData> reportData = new List<ReportData>();

            // Переберите данные Obshaga и заполните данные отчета
            foreach (var obshaga in Connection.Obshagas)  //Статические свойства остаются статическими!
            {
                // Найдите соответствующего студента
                Students student = Connection.Students.FirstOrDefault(s => s.StudentID == obshaga.StudentID);
                Rooms room = Connection.Rooms.FirstOrDefault(r => r.RoomID == obshaga.RoomNumber);

                if (student != null)
                {
                    reportData.Add(new ReportData
                    {
                        Komnata = (room != null) ? room.RoomName : "N/A", // номер комнаты
                        FIO = $"{student.LastName}\n{student.FirstName}\n{student.MiddleName}",
                        DataRozhdeniya = student.BirthDate.ToString("dd.MM.yyyy"),
                        Gruppa = student.Groups,
                        KontaktnyNomer = student.ContactNumber,
                        KontaktyRoditeley = student.ParentsInfo, // Предполагая, что информация о родителях - это то, что вам нужно для контакта
                        DataPervogoZaseleniya = obshaga.CheckInDate.ToString("dd.MM.yyyy"),
                        ObshezhitiePrimechanie = obshaga.Note,
                        Otchislenie = (obshaga.CheckOutDate != DateTime.MinValue) ? obshaga.CheckOutDate.ToString("dd.MM.yyyy") : ""
                    });
                }
            }

            return reportData;
        }


        public static void GenerateReport(string filePath)
        {
            // 1. Create a Document object
            Document document = new Document(PageSize.A4);

            try
            {
                // 2. Create a PdfWriter object
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                // 3. Open the Document
                document.Open();

                // Define fonts
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); // Replace with your font path
                Font headerFont = new Font(baseFont, 12, Font.BOLD);
                Font bodyFont = new Font(baseFont, 10, Font.NORMAL);

                // Add header information similar to the image
                Paragraph header1 = new Paragraph("Пермский авиационный техникум им. А.Д. Швецова", bodyFont);
                header1.Alignment = Element.ALIGN_LEFT;
                document.Add(header1);

                Paragraph header2 = new Paragraph("База данных по воспитательной работе", headerFont);
                header2.Alignment = Element.ALIGN_LEFT;
                document.Add(header2);

                Paragraph header3 = new Paragraph("Проживающие в общежитии/Фамилия: Ощепков/По алфавиту/на дату: 08.02.2025", bodyFont);
                header3.Alignment = Element.ALIGN_LEFT;
                document.Add(header3);


                // 4. Create a table
                PdfPTable table = new PdfPTable(8);  // 8 columns
                table.WidthPercentage = 100; // Table fills the page width

                // Set column widths (adjust these to match your image)
                float[] widths = new float[] { 0.06f, 0.15f, 0.08f, 0.06f, 0.13f, 0.12f, 0.11f, 0.13f }; // Example widths
                table.SetWidths(widths);



                // Add table headers (adjust font as needed)
                AddTableHeaderCell(table, "Комната", headerFont);
                AddTableHeaderCell(table, "ФИО", headerFont);
                AddTableHeaderCell(table, "Дата\nрождения", headerFont);
                AddTableHeaderCell(table, "Группа", headerFont);
                AddTableHeaderCell(table, "Контактный\nномер", headerFont);
                AddTableHeaderCell(table, "Контакты\nродителей", headerFont);
                AddTableHeaderCell(table, "Дата первого\nзаселения", headerFont);
                AddTableHeaderCell(table, "Общежитие\n(примечание)", headerFont);

                // 5. Fetch your data (replace this with your actual data source)
                List<ReportData> reportData = GetData();

                // 6. Add data rows to the table
                foreach (var rowData in reportData)
                {
                    AddTableCell(table, rowData.Komnata, bodyFont);
                    AddTableCell(table, rowData.FIO, bodyFont);
                    AddTableCell(table, rowData.DataRozhdeniya, bodyFont);
                    AddTableCell(table, rowData.Gruppa, bodyFont);
                    AddTableCell(table, rowData.KontaktnyNomer, bodyFont);
                    AddTableCell(table, rowData.KontaktyRoditeley, bodyFont);
                    AddTableCell(table, rowData.DataPervogoZaseleniya, bodyFont);
                    AddTableCell(table, rowData.ObshezhitiePrimechanie, bodyFont);
                }

                document.Add(table);


                // Add summary information
                Paragraph summary = new Paragraph($"Количество записей: {reportData.Count}", bodyFont);
                summary.Alignment = Element.ALIGN_LEFT;
                document.Add(summary);

                // Add date printed info
                Paragraph datePrinted = new Paragraph($"Дата печати: {DateTime.Now:dd.MM.yyyy}", bodyFont);
                datePrinted.Alignment = Element.ALIGN_RIGHT;
                document.Add(datePrinted);

            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }
            finally
            {
                // 7. Close the Document
                document.Close();
            }
        }


        private static void AddTableHeaderCell(PdfPTable table, string text, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE; // Center text vertically
            table.AddCell(cell);
        }


        private static void AddTableCell(PdfPTable table, string text, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = Element.ALIGN_CENTER; // Default alignment, adjust if needed
            cell.VerticalAlignment = Element.ALIGN_MIDDLE; // Center text vertically
            table.AddCell(cell);
        }
    }
}