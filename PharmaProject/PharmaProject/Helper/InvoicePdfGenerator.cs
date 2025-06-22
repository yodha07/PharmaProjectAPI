using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System;
using PharmaProject.Models;

namespace PharmaProject.Helper
{
    public static class InvoicePdfGenerator
    {
        public static string GeneratePurchaseInvoicePdf(List<PurchaseCartDTO2> cartItems, decimal total, string invoiceNo)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PurchaseInvoices");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, $"{invoiceNo}.pdf");

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            
            doc.Add(new Paragraph("Pharma Invoice"));
            doc.Add(new Paragraph($"Invoice No: {invoiceNo}"));
            doc.Add(new Paragraph($"Date: {DateTime.Now.ToString("dd-MM-yyyy")}"));
            doc.Add(new Paragraph(" "));

            
            PdfPTable table = new PdfPTable(5);
            table.AddCell("Medicine");
            table.AddCell("Quantity");
            table.AddCell("Price/Unit");
            table.AddCell("Total");
            table.AddCell("Supplier");

            foreach (var item in cartItems)
            {
                table.AddCell(item.Mname);
                table.AddCell(item.Quantity.ToString());
                table.AddCell(item.Ppu.ToString("0.00"));
                table.AddCell((item.Quantity * item.Ppu).ToString("0.00"));
                table.AddCell(item.Sname);
            }

            table.AddCell("Subtotal");
            table.AddCell("");
            table.AddCell("");
            table.AddCell(total.ToString("0.00"));
            table.AddCell("");

            decimal cgst = total * 0.05m;
            decimal sgst = total * 0.05m;
            decimal grand = total + cgst + sgst;

            table.AddCell("CGST (5%)");
            table.AddCell(""); table.AddCell(""); table.AddCell(cgst.ToString("0.00")); table.AddCell("");

            table.AddCell("SGST (5%)");
            table.AddCell(""); table.AddCell(""); table.AddCell(sgst.ToString("0.00")); table.AddCell("");

            table.AddCell("Grand Total");
            table.AddCell(""); table.AddCell(""); table.AddCell(grand.ToString("0.00")); table.AddCell("");

            doc.Add(table);
            doc.Close();

            return $"/PurchaseInvoices/{invoiceNo}.pdf"; 
        }
    }
}
