using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace UP.Classes
{
    public class OwnerContext
    {
        public static void ReportPDF(string FileName)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Отчет по жильцам дома";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            int MarginTop = 20;
            int MarginLeft = 50;
            XFont fontHeader = new XFont("Arial", 16, XFontStyleEx.Bold);
            XFont font = new XFont("Arial", 12);
            gfx.DrawString("Список жильцов дома", fontHeader, XBrushes.Black, new XRect(0, MarginTop, page.Width, 15), XStringFormats.Center);
            gfx.DrawString("по адресу: г. Пермь, ул. Луначарского, д. 24", font, XBrushes.Black, new XRect(0, MarginTop + 30, page.Width, 10), XStringFormats.Center);
            gfx.DrawString("Всего жильцов: " + AllOwners().Count, font, XBrushes.Black, new XRect(MarginLeft, MarginTop + 70, page.Width, 10), XStringFormats.CenterLeft);
            int Width = (Convert.ToInt32(page.Width.Value) - MarginLeft * 2 - 30) / 5;
            gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft, MarginTop + 100, Width, 20);
            gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + Width + 10, MarginTop + 100, Width, 20);
            gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width + 10) * 2, MarginTop + 100, Width, 20);
            gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width + 10) * 3, MarginTop + 100, Width, 20);
            gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width + 10) * 4, MarginTop + 100, Width, 20);
            gfx.DrawString("№ квартиры", font, XBrushes.Black, new XRect(MarginLeft, MarginTop + 100, Width, 20), XStringFormats.Center);
            gfx.DrawString("Фамилия", font, XBrushes.Black, new XRect(MarginLeft + Width + 10, MarginTop + 100, Width, 20), XStringFormats.Center);
            gfx.DrawString("Имя", font, XBrushes.Black, new XRect(MarginLeft + (Width + 10) * 2, MarginTop + 100, Width, 20), XStringFormats.Center);
            gfx.DrawString("Отчество", font, XBrushes.Black, new XRect(MarginLeft + (Width + 10) * 3, MarginTop + 100, Width, 20), XStringFormats.Center);
            gfx.DrawString("Изображение", font, XBrushes.Black, new XRect(MarginLeft + (Width + 10) * 4, MarginTop + 100, Width, 20), XStringFormats.Center);
            PdfPage page2 = document.AddPage();
            XGraphics gfx2 = XGraphics.FromPdfPage(page2);
            gfx2.DrawString("Список собственников", fontHeader, XBrushes.Black, new XRect(0, MarginTop, page2.Width, 15), XStringFormats.Center);
            gfx2.DrawString("по адресу: г. Пермь, ул. Луначарского, д. 24", font, XBrushes.Black, new XRect(0, MarginTop + 30, page.Width, 10), XStringFormats.Center);
            int Width2 = (Convert.ToInt32(page2.Width.Value) - MarginLeft * 2 - 30) / 4;
            gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft, MarginTop + 60, Width2, 20);
            gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + Width2 + 10, MarginTop + 60, Width2, 20);
            gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width2 + 10) * 2, MarginTop + 60, Width2, 20);
            gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width2 + 10) * 3, MarginTop + 60, Width2, 20);
            gfx2.DrawString("№ квартиры", font, XBrushes.Black, new XRect(MarginLeft, MarginTop + 60, Width2, 20), XStringFormats.Center);
            gfx2.DrawString("Фамилия", font, XBrushes.Black, new XRect(MarginLeft + Width2 + 10, MarginTop + 60, Width2, 20), XStringFormats.Center);
            gfx2.DrawString("Имя", font, XBrushes.Black, new XRect(MarginLeft + (Width2 + 10) * 2, MarginTop + 60, Width2, 20), XStringFormats.Center);
            gfx2.DrawString("Отчество", font, XBrushes.Black, new XRect(MarginLeft + (Width2 + 10) * 3, MarginTop + 60, Width2, 20), XStringFormats.Center);
            int temp1 = -1;
            int temp2 = -1;
            int tempOwners = 0;
            for (int i = 0; i < AllOwners().Count; i++)
            {
                gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft, MarginTop + 100 + 25 * (i + 1), Width, 20);
                gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + Width + 10, MarginTop + 100 + 25 * (i + 1), Width, 20);
                gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width + 10) * 2, MarginTop + 100 + 25 * (i + 1), Width, 20);
                gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width + 10) * 3, MarginTop + 100 + 25 * (i + 1), Width, 20);
                gfx.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width + 10) * 4, MarginTop + 100 + 25 * (i + 1), Width, 20);
                if (AllOwners()[i].NumberRoom != temp1)
                {
                    gfx.DrawString(AllOwners()[i].NumberRoom.ToString(), font, XBrushes.Black, new XRect(MarginLeft, MarginTop + 100 + 25 * (i + 1), Width, 20), XStringFormats.Center);
                    temp2 = i + 2;
                }
                else gfx.DrawString("", font, XBrushes.Black, new XRect(MarginLeft, MarginTop + 100 + 25 * (i + 1), Width, 20), XStringFormats.Center);
                temp1 = AllOwners()[i].NumberRoom;
                gfx.DrawString(AllOwners()[i].LastName, font, XBrushes.Black, new XRect(MarginLeft + Width + 10, MarginTop + 100 + 25 * (i + 1), Width, 20), XStringFormats.Center);
                gfx.DrawString(AllOwners()[i].FirstName, font, XBrushes.Black, new XRect(MarginLeft + (Width + 10) * 2, MarginTop + 100 + 25 * (i + 1), Width, 20), XStringFormats.Center);
                gfx.DrawString(AllOwners()[i].SurName, font, XBrushes.Black, new XRect(MarginLeft + (Width + 10) * 3, MarginTop + 100 + 25 * (i + 1), Width, 20), XStringFormats.Center);
                XImage image = XImage.FromFile("C:\\Users\\kiril\\Desktop\\MDK_01_01_PR50\\Images\\owner.png");
                gfx.DrawImage(image, new XRect(MarginLeft + (Width + 10) * 4 + 35, MarginTop + 100 + 25 * (i + 1), 20, 20));
                if (AllOwners()[i].IsOwner == true)
                {
                    gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20);
                    gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + Width2 + 10, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20);
                    gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width2 + 10) * 2, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20);
                    gfx2.DrawRectangle(new XSolidBrush(XColors.LightGray), MarginLeft + (Width2 + 10) * 3, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20);
                    gfx2.DrawString(AllOwners()[i].NumberRoom.ToString(), font, XBrushes.Black, new XRect(MarginLeft, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20), XStringFormats.Center);
                    gfx2.DrawString(AllOwners()[i].LastName, font, XBrushes.Black, new XRect(MarginLeft + Width2 + 10, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20), XStringFormats.Center);
                    gfx2.DrawString(AllOwners()[i].FirstName, font, XBrushes.Black, new XRect(MarginLeft + (Width2 + 10) * 2, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20), XStringFormats.Center);
                    gfx2.DrawString(AllOwners()[i].SurName, font, XBrushes.Black, new XRect(MarginLeft + (Width2 + 10) * 3, MarginTop + 60 + 25 * (tempOwners + 1), Width2, 20), XStringFormats.Center);
                    tempOwners++;
                }
            }
            document.Save(FileName);
            Process.Start(FileName);
        }
    }
}
