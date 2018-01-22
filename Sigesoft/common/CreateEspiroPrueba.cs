using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Sigesoft.Node.WinClient.BE;


namespace NetPdf
{
    public class CreateEspiroPrueba
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }

        public static void CreateInformeEspiroPrueba(string filePDF, List<ServiceComponentFieldValuesList> Datos)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage page = new pdfPage();

            writer.PageEvent = page;

            document.Open();

            #region Declaration Tables

            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            //string[] columnValues = null;
            string[] columnHeaders = null;


            PdfPTable filiationWorker = new PdfPTable(8);

            PdfPTable table = null;

            PdfPCell cell = null;

            #endregion

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueAntecedentesOcupacionales = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableAntecedentesOcupacionales = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueNegrita = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontAptitud = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            //Font fontTitleTableNegro = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            #region Title
            //Tabla con 2 columnas
            columnWidths = new float[] { 10f,90f };
            var cellsTit = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("PRUEBA PDF", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase("SUB TITULO", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT }
            };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            //Tabla con 3 columnas
            columnWidths = new float[] { 25f, 25f,25f, 25f};
            var cellsTit_1 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("CVF")) { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                new PdfPCell(new Phrase(Datos.Find(p => p.v_ComponentFieldId== Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF) == null ?"" :Datos.Find(p => p.v_ComponentFieldId== Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).v_Value1)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                new PdfPCell(new Phrase("Descripción", fontTitle1)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
                new PdfPCell(new Phrase(Datos.Find(p => p.v_ComponentFieldId== Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_DESCRIPCION_CVF) == null ?"" :Datos.Find(p => p.v_ComponentFieldId== Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_DESCRIPCION_CVF).v_Value1)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT },
            };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit_1, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);
            document.Close();
            writer.Close();
            writer.Dispose();
            #endregion

        }
    }
}
