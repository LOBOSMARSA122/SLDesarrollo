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
    public class INFORME_CUESTIONARIO_ISTAS_2
    {
        private static void RunFile(string filePdf)
        {
            Process proceso = Process.Start(filePdf);
            proceso.WaitForExit();
            proceso.Close();
        }
        
        #region CUESTONARIO_ISTAS
        public static void CreateReportIstas(PacientList filiationData, ServiceList DataService,
            List<ServiceComponentList> serviceComponent,
            organizationDto infoEmpresa,
            PacientList datosPac,
            string filePDF)
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 45f, 41f);

            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            document.Open();

            #region Declaration Tables
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            string[] columnValues = null;
            string[] columnHeaders = null;
            PdfPTable header2 = new PdfPTable(6);
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.WidthPercentage = 100;
            float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            header2.SetWidths(widths1);
            PdfPTable companyData = new PdfPTable(6);
            companyData.HorizontalAlignment = Element.ALIGN_CENTER;
            companyData.WidthPercentage = 100;
            float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            companyData.SetWidths(widthscolumnsCompanyData);
            PdfPTable filiationWorker = new PdfPTable(4);
            PdfPTable table = null;
            PdfPCell cell = null;
            document.Add(new Paragraph("\r\n"));
            #endregion

            #region Fonts
            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            var tamaño_celda = 13f;
            var tamaño_celda2 = 26f;
            #endregion

           
            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresa.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit = new List<PdfPCell>()
                { 
                    new PdfPCell(new Phrase("CUESTIONARIO DE RIESGOS PSICOSOCIALES -  ISTAS CoPsoQ   "+ "\n\r (Hoja de Respuestas)", fontTitle1)) { BackgroundColor= BaseColor.WHITE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region DATOS PERSONALES
            //VALORES FILIACION
            string[] fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            string[] empresaSplit = filiationData.v_FullWorkingOrganizationName.Split('/');
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("1. DATOS PERSONALES", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.WHITE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },       
                new PdfPCell(new Phrase("Apellidos Y Nombres:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Sexo:", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase(datosPac.Genero, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString()+" años", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                
                new PdfPCell(new Phrase("Empresa:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(empresaSplit[0], fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },    
                new PdfPCell(new Phrase("Puesto de trabajo:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation , fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  }, 
                
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("ELIGE UNA SOLA RESPUESTA PARA CADA UNA DE LAS SIGUIENTES PREGUNTAS:", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region VARIABLES
            ServiceComponentList IstasQ = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CUESTIONARIO_ISTAS_2);

                #region Campos 1
                var PREGUNTA_1 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_1) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_1).v_Value1;
                string _1_MUCHAS = "", _1_UNPOCO = "", _1_ALGUNAS = "", _1_SOLO = "", _1_NUNCA = "";
                if (PREGUNTA_1 == "4") _1_MUCHAS = "( X )"; else if (PREGUNTA_1 == "3") _1_UNPOCO = "( X )"; else if (PREGUNTA_1 == "2") _1_ALGUNAS = "( X )"; else if (PREGUNTA_1 == "1") _1_SOLO = "( X )"; else if (PREGUNTA_1 == "0") _1_NUNCA = "( X )";

                var PREGUNTA_2 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_2) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_2).v_Value1;
                string _2_MUCHAS = "", _2_UNPOCO = "", _2_ALGUNAS = "", _2_SOLO = "", _2_NUNCA = "";
                if (PREGUNTA_2 == "4") _2_MUCHAS = "( X )"; else if (PREGUNTA_2 == "3") _2_UNPOCO = "( X )"; else if (PREGUNTA_2 == "2") _2_ALGUNAS = "( X )"; else if (PREGUNTA_2 == "1") _2_SOLO = "( X )"; else if (PREGUNTA_2 == "0") _2_NUNCA = "( X )";

                var PREGUNTA_3 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_3) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_3).v_Value1;
                string _3_MUCHAS = "", _3_UNPOCO = "", _3_ALGUNAS = "", _3_SOLO = "", _3_NUNCA = "";
                if (PREGUNTA_3 == "4") _3_MUCHAS = "( X )"; else if (PREGUNTA_3 == "3") _3_UNPOCO = "( X )"; else if (PREGUNTA_3 == "2") _3_ALGUNAS = "( X )"; else if (PREGUNTA_3 == "1") _3_SOLO = "( X )"; else if (PREGUNTA_3 == "0") _3_NUNCA = "( X )";

                var PREGUNTA_4 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_4) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_4).v_Value1;
                string _4_MUCHAS = "", _4_UNPOCO = "", _4_ALGUNAS = "", _4_SOLO = "", _4_NUNCA = "";
                if (PREGUNTA_4 == "4") _4_MUCHAS = "( X )"; else if (PREGUNTA_4 == "3") _4_UNPOCO = "( X )"; else if (PREGUNTA_4 == "2") _4_ALGUNAS = "( X )"; else if (PREGUNTA_4 == "1") _4_SOLO = "( X )"; else if (PREGUNTA_4 == "0") _4_NUNCA = "( X )";

                var PREGUNTA_5 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_5) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_5).v_Value1;
                string _5_MUCHAS = "", _5_UNPOCO = "", _5_ALGUNAS = "", _5_SOLO = "", _5_NUNCA = "";
                if (PREGUNTA_5 == "4") _5_MUCHAS = "( X )"; else if (PREGUNTA_5 == "3") _5_UNPOCO = "( X )"; else if (PREGUNTA_5 == "2") _5_ALGUNAS = "( X )"; else if (PREGUNTA_5 == "1") _5_SOLO = "( X )"; else if (PREGUNTA_5 == "0") _5_NUNCA = "( X )";

                var PREGUNTA_6 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_6) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_6).v_Value1;
                string _6_MUCHAS = "", _6_UNPOCO = "", _6_ALGUNAS = "", _6_SOLO = "", _6_NUNCA = "";
                if (PREGUNTA_6 == "4") _6_MUCHAS = "( X )"; else if (PREGUNTA_6 == "3") _6_UNPOCO = "( X )"; else if (PREGUNTA_6 == "2") _6_ALGUNAS = "( X )"; else if (PREGUNTA_6 == "1") _6_SOLO = "( X )"; else if (PREGUNTA_6 == "0") _6_NUNCA = "( X )";

                int valorApartado1 = int.Parse(PREGUNTA_1) + int.Parse(PREGUNTA_2) + int.Parse(PREGUNTA_3) + int.Parse(PREGUNTA_4) + int.Parse(PREGUNTA_5) + int.Parse(PREGUNTA_6);
                #endregion

                #region Campos 2
                var PREGUNTA_7 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_7) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_7).v_Value1;
                string _7_MUCHAS = "", _7_UNPOCO = "", _7_ALGUNAS = "", _7_SOLO = "", _7_NUNCA = "";
                if (PREGUNTA_7 == "4") _7_MUCHAS = "( X )"; else if (PREGUNTA_7 == "3") _7_UNPOCO = "( X )"; else if (PREGUNTA_7 == "2") _7_ALGUNAS = "( X )"; else if (PREGUNTA_7 == "1") _7_SOLO = "( X )"; else if (PREGUNTA_7 == "0") _7_NUNCA = "( X )";
                var PREGUNTA_8 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_8) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_8).v_Value1;
                string _8_MUCHAS = "", _8_UNPOCO = "", _8_ALGUNAS = "", _8_SOLO = "", _8_NUNCA = "";
                if (PREGUNTA_8 == "4") _8_MUCHAS = "( X )"; else if (PREGUNTA_8 == "3") _8_UNPOCO = "( X )"; else if (PREGUNTA_8 == "2") _8_ALGUNAS = "( X )"; else if (PREGUNTA_8 == "1") _8_SOLO = "( X )"; else if (PREGUNTA_8 == "0") _8_NUNCA = "( X )";
                var PREGUNTA_9 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_9) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_9).v_Value1;
                string _9_MUCHAS = "", _9_UNPOCO = "", _9_ALGUNAS = "", _9_SOLO = "", _9_NUNCA = "";
                if (PREGUNTA_9 == "4") _9_MUCHAS = "( X )"; else if (PREGUNTA_9 == "3") _9_UNPOCO = "( X )"; else if (PREGUNTA_9 == "2") _9_ALGUNAS = "( X )"; else if (PREGUNTA_9 == "1") _9_SOLO = "( X )"; else if (PREGUNTA_9 == "0") _9_NUNCA = "( X )";
                var PREGUNTA_10 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_10) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_10).v_Value1;
                string _10_MUCHAS = "", _10_UNPOCO = "", _10_ALGUNAS = "", _10_SOLO = "", _10_NUNCA = "";
                if (PREGUNTA_10 == "4") _10_MUCHAS = "( X )"; else if (PREGUNTA_10 == "3") _10_UNPOCO = "( X )"; else if (PREGUNTA_10 == "2") _10_ALGUNAS = "( X )"; else if (PREGUNTA_10 == "1") _10_SOLO = "( X )"; else if (PREGUNTA_10 == "0") _10_NUNCA = "( X )";
                var PREGUNTA_11 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_11) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_11).v_Value1;
                string _11_MUCHAS = "", _11_UNPOCO = "", _11_ALGUNAS = "", _11_SOLO = "", _11_NUNCA = "";
                if (PREGUNTA_11 == "4") _11_MUCHAS = "( X )"; else if (PREGUNTA_11 == "3") _11_UNPOCO = "( X )"; else if (PREGUNTA_11 == "2") _11_ALGUNAS = "( X )"; else if (PREGUNTA_11 == "1") _11_SOLO = "( X )"; else if (PREGUNTA_11 == "0") _11_NUNCA = "( X )";
                var PREGUNTA_12 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_12) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_12).v_Value1;
                string _12_MUCHAS = "", _12_UNPOCO = "", _12_ALGUNAS = "", _12_SOLO = "", _12_NUNCA = "";
                if (PREGUNTA_12 == "4") _12_MUCHAS = "( X )"; else if (PREGUNTA_12 == "3") _12_UNPOCO = "( X )"; else if (PREGUNTA_12 == "2") _12_ALGUNAS = "( X )"; else if (PREGUNTA_12 == "1") _12_SOLO = "( X )"; else if (PREGUNTA_12 == "0") _12_NUNCA = "( X )";
                var PREGUNTA_13 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_13) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_13).v_Value1;
                string _13_MUCHAS = "", _13_UNPOCO = "", _13_ALGUNAS = "", _13_SOLO = "", _13_NUNCA = "";
                if (PREGUNTA_13 == "4") _13_MUCHAS = "( X )"; else if (PREGUNTA_13 == "3") _13_UNPOCO = "( X )"; else if (PREGUNTA_13 == "2") _13_ALGUNAS = "( X )"; else if (PREGUNTA_13 == "1") _13_SOLO = "( X )"; else if (PREGUNTA_13 == "0") _13_NUNCA = "( X )";
                var PREGUNTA_14 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_14) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_14).v_Value1;
                string _14_MUCHAS = "", _14_UNPOCO = "", _14_ALGUNAS = "", _14_SOLO = "", _14_NUNCA = "";
                if (PREGUNTA_14 == "4") _14_MUCHAS = "( X )"; else if (PREGUNTA_14 == "3") _14_UNPOCO = "( X )"; else if (PREGUNTA_14 == "2") _14_ALGUNAS = "( X )"; else if (PREGUNTA_14 == "1") _14_SOLO = "( X )"; else if (PREGUNTA_14 == "0") _14_NUNCA = "( X )";
                var PREGUNTA_15 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_15) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_15).v_Value1;
                string _15_MUCHAS = "", _15_UNPOCO = "", _15_ALGUNAS = "", _15_SOLO = "", _15_NUNCA = "";
                if (PREGUNTA_15 == "4") _15_MUCHAS = "( X )"; else if (PREGUNTA_15 == "3") _15_UNPOCO = "( X )"; else if (PREGUNTA_15 == "2") _15_ALGUNAS = "( X )"; else if (PREGUNTA_15 == "1") _15_SOLO = "( X )"; else if (PREGUNTA_15 == "0") _15_NUNCA = "( X )";
                var PREGUNTA_16 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_16) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_16).v_Value1;
                string _16_MUCHAS = "", _16_UNPOCO = "", _16_ALGUNAS = "", _16_SOLO = "", _16_NUNCA = "";
                if (PREGUNTA_16 == "4") _16_MUCHAS = "( X )"; else if (PREGUNTA_16 == "3") _16_UNPOCO = "( X )"; else if (PREGUNTA_16 == "2") _16_ALGUNAS = "( X )"; else if (PREGUNTA_16 == "1") _16_SOLO = "( X )"; else if (PREGUNTA_16 == "0") _16_NUNCA = "( X )";
                int valorApartado2 = int.Parse(PREGUNTA_7) + int.Parse(PREGUNTA_8) + int.Parse(PREGUNTA_9) + int.Parse(PREGUNTA_10) + int.Parse(PREGUNTA_11) + int.Parse(PREGUNTA_12) + int.Parse(PREGUNTA_13) + int.Parse(PREGUNTA_14) + int.Parse(PREGUNTA_15) + int.Parse(PREGUNTA_16);
                #endregion

                #region Campos 3
                var PREGUNTA_17 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_17) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_17).v_Value1;
                string _17_MUCHAS = "", _17_UNPOCO = "", _17_ALGUNAS = "", _17_SOLO = "", _17_NUNCA = "";
                if (PREGUNTA_17 == "4") _17_MUCHAS = "( X )"; else if (PREGUNTA_17 == "3") _17_UNPOCO = "( X )"; else if (PREGUNTA_17 == "2") _17_ALGUNAS = "( X )"; else if (PREGUNTA_17 == "1") _17_SOLO = "( X )"; else if (PREGUNTA_17 == "0") _17_NUNCA = "( X )";

                var PREGUNTA_18 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_18) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_18).v_Value1;
                string _18_MUCHAS = "", _18_UNPOCO = "", _18_ALGUNAS = "", _18_SOLO = "", _18_NUNCA = "";
                if (PREGUNTA_18 == "4") _18_MUCHAS = "( X )"; else if (PREGUNTA_18 == "3") _18_UNPOCO = "( X )"; else if (PREGUNTA_18 == "2") _18_ALGUNAS = "( X )"; else if (PREGUNTA_18 == "1") _18_SOLO = "( X )"; else if (PREGUNTA_18 == "0") _18_NUNCA = "( X )";

                var PREGUNTA_19 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_19) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_19).v_Value1;
                string _19_MUCHAS = "", _19_UNPOCO = "", _19_ALGUNAS = "", _19_SOLO = "", _19_NUNCA = "";
                if (PREGUNTA_19 == "4") _19_MUCHAS = "( X )"; else if (PREGUNTA_19 == "3") _19_UNPOCO = "( X )"; else if (PREGUNTA_19 == "2") _19_ALGUNAS = "( X )"; else if (PREGUNTA_19 == "1") _19_SOLO = "( X )"; else if (PREGUNTA_19 == "0") _19_NUNCA = "( X )";

                var PREGUNTA_20 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_20) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_20).v_Value1;
                string _20_MUCHAS = "", _20_UNPOCO = "", _20_ALGUNAS = "", _20_SOLO = "", _20_NUNCA = "";
                if (PREGUNTA_20 == "4") _20_MUCHAS = "( X )"; else if (PREGUNTA_20 == "3") _20_UNPOCO = "( X )"; else if (PREGUNTA_20 == "2") _20_ALGUNAS = "( X )"; else if (PREGUNTA_20 == "1") _20_SOLO = "( X )"; else if (PREGUNTA_20 == "0") _20_NUNCA = "( X )";

                int valorApartado3 = int.Parse(PREGUNTA_17) + int.Parse(PREGUNTA_18) + int.Parse(PREGUNTA_19) + int.Parse(PREGUNTA_20);

                #endregion

                #region Campos 4
                var PREGUNTA_21 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_21) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_21).v_Value1;
                string _21_MUCHAS = "", _21_UNPOCO = "", _21_ALGUNAS = "", _21_SOLO = "", _21_NUNCA = "";
                if (PREGUNTA_21 == "4") _21_MUCHAS = "( X )"; else if (PREGUNTA_21 == "3") _21_UNPOCO = "( X )"; else if (PREGUNTA_21 == "2") _21_ALGUNAS = "( X )"; else if (PREGUNTA_21 == "1") _21_SOLO = "( X )"; else if (PREGUNTA_21 == "0") _21_NUNCA = "( X )";

                var PREGUNTA_22 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_22) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_22).v_Value1;
                string _22_MUCHAS = "", _22_UNPOCO = "", _22_ALGUNAS = "", _22_SOLO = "", _22_NUNCA = "";
                if (PREGUNTA_22 == "4") _22_MUCHAS = "( X )"; else if (PREGUNTA_22 == "3") _22_UNPOCO = "( X )"; else if (PREGUNTA_22 == "2") _22_ALGUNAS = "( X )"; else if (PREGUNTA_22 == "1") _22_SOLO = "( X )"; else if (PREGUNTA_22 == "0") _22_NUNCA = "( X )";

                var PREGUNTA_23 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_23) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_23).v_Value1;
                string _23_MUCHAS = "", _23_UNPOCO = "", _23_ALGUNAS = "", _23_SOLO = "", _23_NUNCA = "";
                if (PREGUNTA_23 == "4") _23_MUCHAS = "( X )"; else if (PREGUNTA_23 == "3") _23_UNPOCO = "( X )"; else if (PREGUNTA_23 == "2") _23_ALGUNAS = "( X )"; else if (PREGUNTA_23 == "1") _23_SOLO = "( X )"; else if (PREGUNTA_23 == "0") _23_NUNCA = "( X )";

                var PREGUNTA_24 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_24) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_24).v_Value1;
                string _24_MUCHAS = "", _24_UNPOCO = "", _24_ALGUNAS = "", _24_SOLO = "", _24_NUNCA = "";
                if (PREGUNTA_24 == "4") _24_MUCHAS = "( X )"; else if (PREGUNTA_24 == "3") _24_UNPOCO = "( X )"; else if (PREGUNTA_24 == "2") _24_ALGUNAS = "( X )"; else if (PREGUNTA_24 == "1") _24_SOLO = "( X )"; else if (PREGUNTA_24 == "0") _24_NUNCA = "( X )";

                var PREGUNTA_25 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_25) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_25).v_Value1;
                string _25_MUCHAS = "", _25_UNPOCO = "", _25_ALGUNAS = "", _25_SOLO = "", _25_NUNCA = "";
                if (PREGUNTA_25 == "4") _25_MUCHAS = "( X )"; else if (PREGUNTA_25 == "3") _25_UNPOCO = "( X )"; else if (PREGUNTA_25 == "2") _25_ALGUNAS = "( X )"; else if (PREGUNTA_25 == "1") _25_SOLO = "( X )"; else if (PREGUNTA_25 == "0") _25_NUNCA = "( X )";

                var PREGUNTA_26 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_26) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_26).v_Value1;
                string _26_MUCHAS = "", _26_UNPOCO = "", _26_ALGUNAS = "", _26_SOLO = "", _26_NUNCA = "";
                if (PREGUNTA_26 == "4") _26_MUCHAS = "( X )"; else if (PREGUNTA_26 == "3") _26_UNPOCO = "( X )"; else if (PREGUNTA_26 == "2") _26_ALGUNAS = "( X )"; else if (PREGUNTA_26 == "1") _26_SOLO = "( X )"; else if (PREGUNTA_26 == "0") _26_NUNCA = "( X )";

                var PREGUNTA_27 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_27) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_27).v_Value1;
                string _27_MUCHAS = "", _27_UNPOCO = "", _27_ALGUNAS = "", _27_SOLO = "", _27_NUNCA = "";
                if (PREGUNTA_27 == "4") _27_MUCHAS = "( X )"; else if (PREGUNTA_27 == "3") _27_UNPOCO = "( X )"; else if (PREGUNTA_27 == "2") _27_ALGUNAS = "( X )"; else if (PREGUNTA_27 == "1") _27_SOLO = "( X )"; else if (PREGUNTA_27 == "0") _27_NUNCA = "( X )";

                var PREGUNTA_28 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_28) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_28).v_Value1;
                string _28_MUCHAS = "", _28_UNPOCO = "", _28_ALGUNAS = "", _28_SOLO = "", _28_NUNCA = "";
                if (PREGUNTA_28 == "4") _28_MUCHAS = "( X )"; else if (PREGUNTA_28 == "3") _28_UNPOCO = "( X )"; else if (PREGUNTA_28 == "2") _28_ALGUNAS = "( X )"; else if (PREGUNTA_28 == "1") _28_SOLO = "( X )"; else if (PREGUNTA_28 == "0") _28_NUNCA = "( X )";

                var PREGUNTA_29 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_29) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_29).v_Value1;
                string _29_MUCHAS = "", _29_UNPOCO = "", _29_ALGUNAS = "", _29_SOLO = "", _29_NUNCA = "";
                if (PREGUNTA_29 == "4") _29_MUCHAS = "( X )"; else if (PREGUNTA_29 == "3") _29_UNPOCO = "( X )"; else if (PREGUNTA_29 == "2") _29_ALGUNAS = "( X )"; else if (PREGUNTA_29 == "1") _29_SOLO = "( X )"; else if (PREGUNTA_29 == "0") _29_NUNCA = "( X )";

                var PREGUNTA_30 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_30) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_30).v_Value1;
                string _30_MUCHAS = "", _30_UNPOCO = "", _30_ALGUNAS = "", _30_SOLO = "", _30_NUNCA = "";
                if (PREGUNTA_30 == "4") _30_MUCHAS = "( X )"; else if (PREGUNTA_30 == "3") _30_UNPOCO = "( X )"; else if (PREGUNTA_30 == "2") _30_ALGUNAS = "( X )"; else if (PREGUNTA_30 == "1") _30_SOLO = "( X )"; else if (PREGUNTA_30 == "0") _30_NUNCA = "( X )";

                int valorApartado4 = int.Parse(PREGUNTA_21) + int.Parse(PREGUNTA_22) + int.Parse(PREGUNTA_23) + int.Parse(PREGUNTA_24) + int.Parse(PREGUNTA_25) + int.Parse(PREGUNTA_26) + int.Parse(PREGUNTA_27) + int.Parse(PREGUNTA_28) + int.Parse(PREGUNTA_29) + int.Parse(PREGUNTA_30);

                #endregion

                #region Campos 5

                var PREGUNTA_31 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_31) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_31).v_Value1;
                string _31_A_ = ""; string _31_B_ = ""; string _31_C_ = "";string _31_D_ = ""; string _31_E_ = ""; 

                var PREGUNTA_32 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_32) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_32).v_Value1;
                string _32_MUCHAS = "", _32_UNPOCO = "", _32_ALGUNAS = "", _32_SOLO = "", _32_NUNCA = "";
                

                var PREGUNTA_33 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_33) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_33).v_Value1;
                string _33_MUCHAS = "", _33_UNPOCO = "", _33_ALGUNAS = "", _33_SOLO = "", _33_NUNCA = "";
                

                var PREGUNTA_34 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_34) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_34).v_Value1;
                string _34_MUCHAS = "", _34_UNPOCO = "", _34_ALGUNAS = "", _34_SOLO = "", _34_NUNCA = "";
                

                string valorApartado5="VIVE SOLO";
                var _5_SINO = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants._5_SINO) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants._5_SINO).v_Value1;
                if (_5_SINO == "1")
                {
                    if (PREGUNTA_31 == "4") _31_A_ = "( X )";
                    if (PREGUNTA_31 == "3") _31_B_ = "( X )";
                    if (PREGUNTA_31 == "2") _31_C_ = "( X )";
                    if (PREGUNTA_31 == "1") _31_D_ = "( X )";
                    if (PREGUNTA_31 == "0") _31_E_ = "( X )";
                    if (PREGUNTA_32 == "4") _32_MUCHAS = "( X )"; else if (PREGUNTA_32 == "3") _32_UNPOCO = "( X )"; else if (PREGUNTA_32 == "2") _32_ALGUNAS = "( X )"; else if (PREGUNTA_32 == "1") _32_SOLO = "( X )"; else if (PREGUNTA_32 == "0") _32_NUNCA = "( X )";
                    if (PREGUNTA_33 == "4") _33_MUCHAS = "( X )"; else if (PREGUNTA_33 == "3") _33_UNPOCO = "( X )"; else if (PREGUNTA_33 == "2") _33_ALGUNAS = "( X )"; else if (PREGUNTA_33 == "1") _33_SOLO = "( X )"; else if (PREGUNTA_33 == "0") _33_NUNCA = "( X )";
                    if (PREGUNTA_34 == "4") _34_MUCHAS = "( X )"; else if (PREGUNTA_34 == "3") _34_UNPOCO = "( X )"; else if (PREGUNTA_34 == "2") _34_ALGUNAS = "( X )"; else if (PREGUNTA_34 == "1") _34_SOLO = "( X )"; else if (PREGUNTA_34 == "0") _34_NUNCA = "( X )";
                    valorApartado5 = (int.Parse(PREGUNTA_31) + int.Parse(PREGUNTA_32) + int.Parse(PREGUNTA_33) + int.Parse(PREGUNTA_34)).ToString();
                }

                #endregion

                #region Campos 6
                var PREGUNTA_35 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_35) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_35).v_Value1;
                string _35_MUCHAS = "", _35_UNPOCO = "", _35_ALGUNAS = "", _35_SOLO = "", _35_NUNCA = "";
                if (PREGUNTA_35 == "4") _35_MUCHAS = "( X )"; else if (PREGUNTA_35 == "3") _35_UNPOCO = "( X )"; else if (PREGUNTA_35 == "2") _35_ALGUNAS = "( X )"; else if (PREGUNTA_35 == "1") _35_SOLO = "( X )"; else if (PREGUNTA_35 == "0") _35_NUNCA = "( X )";

                var PREGUNTA_36 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_36) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_36).v_Value1;
                string _36_MUCHAS = "", _36_UNPOCO = "", _36_ALGUNAS = "", _36_SOLO = "", _36_NUNCA = "";
                if (PREGUNTA_36 == "4") _36_MUCHAS = "( X )"; else if (PREGUNTA_36 == "3") _36_UNPOCO = "( X )"; else if (PREGUNTA_36 == "2") _36_ALGUNAS = "( X )"; else if (PREGUNTA_36 == "1") _36_SOLO = "( X )"; else if (PREGUNTA_36 == "0") _36_NUNCA = "( X )";

                var PREGUNTA_37 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_37) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_37).v_Value1;
                string _37_MUCHAS = "", _37_UNPOCO = "", _37_ALGUNAS = "", _37_SOLO = "", _37_NUNCA = "";
                if (PREGUNTA_37 == "4") _37_MUCHAS = "( X )"; else if (PREGUNTA_37 == "3") _37_UNPOCO = "( X )"; else if (PREGUNTA_37 == "2") _37_ALGUNAS = "( X )"; else if (PREGUNTA_37 == "1") _37_SOLO = "( X )"; else if (PREGUNTA_37 == "0") _37_NUNCA = "( X )";

                var PREGUNTA_38 = IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_38) == null ? "" : IstasQ.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PREGUNTA_38).v_Value1;
                string _38_MUCHAS = "", _38_UNPOCO = "", _38_ALGUNAS = "", _38_SOLO = "", _38_NUNCA = "";
                if (PREGUNTA_38 == "4") _38_MUCHAS = "( X )"; else if (PREGUNTA_38 == "3") _38_UNPOCO = "( X )"; else if (PREGUNTA_38 == "2") _38_ALGUNAS = "( X )"; else if (PREGUNTA_38 == "1") _38_SOLO = "( X )"; else if (PREGUNTA_38 == "0") _38_NUNCA = "( X )";

                int valorApartado6 = int.Parse(PREGUNTA_35) + int.Parse(PREGUNTA_36) + int.Parse(PREGUNTA_37) + int.Parse(PREGUNTA_38);

                
                #endregion
         
            #endregion

            #region Interpretacion
                #region Interpretacion Pregunta 6
                var verde6 =
                    valorApartado6 >= 13 && valorApartado6 <= 16
                        ? "( X )"
                        : "";
                var amarillo6 =
                    valorApartado6 >= 11 && valorApartado6 <= 12
                        ? "( X )"
                        : "";
                var rojo6 =
                    valorApartado6 >= 0 && valorApartado6 <= 10
                        ? "( X )"
                        : "";
                #endregion

                #region Interpretacion pregunta 5

                string verde5 = "", amarillo5 = "", rojo5 = "";

                if (_5_SINO == "1")
                {
                 verde5 =
                    int.Parse(valorApartado5) >= 0 && int.Parse(valorApartado5) <= 3
                        ? "( X )"
                        : "";
                 amarillo5 =
                     int.Parse(valorApartado5) >= 4 && int.Parse(valorApartado5) <= 6
                        ? "( X )"
                        : "";
                 rojo5 =
                     int.Parse(valorApartado5) >= 7 && int.Parse(valorApartado5) <= 16
                        ? "( X )"
                        : "";
                }

                #endregion

                #region  Interpretacion pregunta 4
                var verde4 =
                    valorApartado4 >= 29 && valorApartado4 <= 40
                        ? "( X )"
                        : "";
                var amarillo4 =
                    valorApartado4 >= 24 && valorApartado4 <= 28
                        ? "( X )"
                        : "";
                var rojo4 =
                    valorApartado4 >= 0 && valorApartado4 <= 23
                        ? "( X )"
                        : "";
                #endregion

                #region Interpretacion 3era pregunta
                var verde3 =
                    valorApartado3 >= 0 && valorApartado3 <= 1
                        ? "( X )"
                        : "";
                var amarillo3 =
                    valorApartado3 >= 2 && valorApartado3 <= 5
                        ? "( X )"
                        : "";
                var rojo3 =
                    valorApartado3 >= 6 && valorApartado3 <= 20
                        ? "( X )"
                        : "";
                #endregion

                #region Interpretacion 2da pregunta
                var verde2 =
                    valorApartado2 >= 26 && valorApartado2 <= 40
                        ? "( X )"
                        : "";
                var amarillo2 =
                    valorApartado2 >= 21 && valorApartado2 <= 25
                        ? "( X )"
                        : "";
                var rojo2 =
                    valorApartado2 >= 0 && valorApartado2 <= 20
                        ? "( X )"
                        : "";
                #endregion


                #region Interpretacion 1era pregunta
                var verde1 =
                    valorApartado1 >= 0 && valorApartado1 <= 7
                        ? "( X )"
                        : "";
                var amarillo1 =
                    valorApartado1 >= 8 && valorApartado1 <= 10
                        ? "( X )"
                        : "";
                var rojo1 =
                    valorApartado1 >= 11 && valorApartado1 <= 24
                        ? "( X )"
                        : "";
                #endregion

                #endregion

                #region APARTADO 1
                cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("APARTADO 1", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Muchas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Un poco", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Solo"+"\r algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Nunca", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("1.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tienes que trabajar muy rápido", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_1_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_1_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_1_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_1_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_1_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("2.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("La distribución de tareas es irregular y provoca que se te acumule el trabajo", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_2_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_2_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_2_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_2_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_2_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("3.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tienes tiempo de llevar al día tu trabajo ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_3_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_3_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_3_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_3_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_3_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("4.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Te cuesta olvidar los problemas del trabajo ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_4_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_4_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_4_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_4_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_4_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("5.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tu trabajo en general es desgastante emocionalmente ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_5_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_5_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_5_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_5_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_5_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("6.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tu trabajo requiere que escondas tus emociones ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_6_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_6_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_6_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_6_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_6_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Valor: "+valorApartado1.ToString(), fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region APARTADO 2
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("APARTADO 2", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Muchas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Un poco", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Solo"+"\r algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Nunca", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("7.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tienes influencia sobre la cantidad de trabajo que se te asigna ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_7_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_7_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_7_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_7_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_7_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("8.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Se tiene en cuenta tu opinión cuando se te asignan las tareas", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_8_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_8_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_8_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_8_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_8_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("9.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tienes influencia sobre el orden en que realizas tus tareas ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_9_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_9_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_9_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_9_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_9_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("10.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Puedes decidir cuando hacer un descanso ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_10_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_10_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_10_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_10_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_10_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("11.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Si tienes algún asunto familiar o personal ¿puedes dejar tu puesto de trabajo al menos una hora, sin pedir un permiso especial?", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_11_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_11_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_11_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_11_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_11_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("12.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tu trabajo requiere que tengas iniciativa ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_12_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_12_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_12_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_12_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_12_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("13.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tu trabajo te permite aprender nuevas cosas", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_13_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_13_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_13_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_13_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_13_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("14.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Te sientes comprometido con tu profesión ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_14_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_14_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_14_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_14_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_14_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("15.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tienen sentido tus tareas", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_15_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_15_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_15_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_15_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_15_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("16.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Hablas con entusiasmo de tu empresa a otras personas", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_16_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_16_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_16_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_16_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_16_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Valor: "+valorApartado2.ToString(), fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region APARTADO 3
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("APARTADO 3", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Muchas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Un poco", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Solo"+"\r algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Nunca", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("17.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Estás preocupado por lo difícil que sería encontrar otro trabajo, si es que quedaras desempleado", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_17_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_17_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_17_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_17_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_17_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("18.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Estas preocupado por si te cambian tareas en contra de tu voluntad ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_18_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_18_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_18_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_18_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_18_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("19.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Estas preocupado por si te varían el salario (que no te actualicen, que te lo bajen, que lo introduzcan en un salario variable, etc)", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_19_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_19_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_19_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_19_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_19_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("20.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Estas preocupado por si te cambian el horario (turno, días de la semana, horarios) contra tu voluntad", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_20_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_20_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_20_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_20_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_20_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Valor: "+valorApartado3.ToString(), fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region APARTADO 4
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("APARTADO 4", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Muchas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Un poco", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Solo"+"\r algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Nunca", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("21.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Sabes exactamente que margen de autonomía tienes en tu trabajo ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_21_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_21_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_21_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_21_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_21_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("22.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Sabes exactamente que tareas son de tu responsabilidad", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_22_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_22_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_22_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_22_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_22_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("23.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("En tu empresa se te informa con suficiente antelación de los cambios que pueden afectar tu futuro", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_23_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_23_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_23_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_23_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_23_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("24.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Recibes toda la información que necesitas para realizar tu trabajo ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_24_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_24_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_24_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_24_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_24_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("25.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("ecibes ayuda y apoyo de tus compañeros de trabajo ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_25_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_25_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_25_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_25_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_25_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("26.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Recibes ayuda de tu inmediato superior  ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_26_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_26_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_26_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_26_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_26_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("27.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tu puesto de trabajo se encuentra aislado de tus compañeros de trabajo", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_27_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_27_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_27_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_27_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_27_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("28.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("En el trabajo sientes que formas parte de un equipo de trabajo ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_28_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_28_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_28_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_28_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_28_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("29.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tus actuales jefes planifican bien el trabajo ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_29_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_29_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_29_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_29_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_29_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("30.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Tus actuales jefes se comunican bien con los trabajadores ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_30_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_30_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_30_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_30_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_30_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Valor: "+valorApartado4.ToString(), fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region Salto de hoja
            //salto de pagina
            document.NewPage();
            #endregion
            
            #region TÍTULO

            cells = new List<PdfPCell>();

            if (infoEmpresa.b_Image != null)
            {
                iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(HandlingItextSharp.GetImage(infoEmpresa.b_Image));
                imagenEmpresa.ScalePercent(25);
                imagenEmpresa.SetAbsolutePosition(40, 790);
                document.Add(imagenEmpresa);
            }
            //iTextSharp.text.Image imagenMinsa = iTextSharp.text.Image.GetInstance("C:/Banner/Minsa.png");
            //imagenMinsa.ScalePercent(10);
            //imagenMinsa.SetAbsolutePosition(400, 785);
            //document.Add(imagenMinsa);

            var cellsTit1 = new List<PdfPCell>()
            { 
                new PdfPCell(new Phrase("CUESTIONARIO DE RIESGOS PSICOSOCIALES -  ISTAS CoPsoQ   "+ "\n\r (Hoja de Respuestas)", fontTitle1)) { BackgroundColor= BaseColor.WHITE, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 15f, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };
            columnWidths = new float[] { 100f };
            table = HandlingItextSharp.GenerateTableFromCells(cellsTit1, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion
            
            #region DATOS PERSONALES 2
            //VALORES FILIACION
            fechaServicio = datosPac.FechaServicio.ToString().Split(' ');
            empresaSplit = filiationData.v_FullWorkingOrganizationName.Split('/');
            cells = new List<PdfPCell>()
            {          
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValueBold)) { Colspan = 20,HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, BackgroundColor = BaseColor.WHITE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },       
                new PdfPCell(new Phrase("Apellidos Y Nombres:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(datosPac.v_FirstLastName + " " + datosPac.v_SecondLastName + " " + datosPac.v_FirstName, fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                new PdfPCell(new Phrase("Sexo:", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase(datosPac.Genero, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase("Edad:", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase(datosPac.Edad.ToString()+" años", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                
                new PdfPCell(new Phrase("Empresa:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },    
                new PdfPCell(new Phrase(empresaSplit[0], fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  },    
                new PdfPCell(new Phrase("Puesto de trabajo:", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE }, 
                new PdfPCell(new Phrase(datosPac.v_CurrentOccupation , fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE  }, 
                
                
            };
            columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region Subtitulo
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("ESTE APARTADO ESTA DISEÑADO PARA PERSONAS QUE CONVIVAN CON ALGUIEN (PAREJA, HIJOS, PADRES…) SI VIVEN SOLOS PASAR AL APARTADO 6", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);
            #endregion

            #region APARTADO 5 QUE CONVIVAN CON ALGUIEN
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(_5_SINO=="0"?"  APARTADO 5 (VIVE SOLO)":"APARTADO 5", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase("31.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("¿Qué parte del trabajo familiar y doméstico haces tú?", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase(" ", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Soy el principal responsable y hago la mayor parte de las tareas", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_31_A_, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase(" ", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Hago aproximadamente la mitad de las tareas domesticas", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_31_B_, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase(" ", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Hago más o menos la cuarta parte de las tareas domésticas ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_31_C_, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase(" ", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Solo hago tareas muy puntuales ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_31_D_, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                new PdfPCell(new Phrase(" ", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("No hago ninguna o casi ninguna de las tareas ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_31_E_, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region APARTADO 5
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase(_5_SINO=="0"?"  APARTADO 5 (VIVE SOLO)":"APARTADO 5", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Muchas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Un poco", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Solo"+"\r algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Nunca", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("32.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Si faltas algún día a la casa, las tareas domésticas quedan sin hacer ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_32_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_32_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_32_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_32_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_32_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("33.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Cuando estas en la empresa piensas en las tareas domésticas ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_33_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_33_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_33_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_33_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_33_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("34.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Hay momentos en los que necesitas estar en la empresa y en la casa también", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_34_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_34_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_34_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_34_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_34_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Valor: "+valorApartado5, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region APARTADO 6
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("APARTADO 6", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Muchas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Un poco", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Solo"+"\r algunas"+"\r veces", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Nunca", fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda2,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("35.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Mis superiores me dan el reconocimiento que merezco ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_35_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_35_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_35_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_35_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_35_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("36.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("En las situaciones difíciles en el trabajo recibo el apoyo necesario ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_36_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_36_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_36_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_36_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_36_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("37.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("En mi trabajo me tratan injustamente ", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_37_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_37_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_37_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_37_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_37_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase("38.-", fontSubTitle)) { Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Si pienso en todo el trabajo y esfuerzo que he realizado, el reconocimiento que recibo en mi trabajo es el adecuado", fontColumnValue)) { Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 "+_38_MUCHAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("3 "+_38_UNPOCO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 "+_38_ALGUNAS, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("1 "+_38_SOLO, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 "+_38_NUNCA, fontColumnValue)) { Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Valor: "+valorApartado6, fontColumnValue)) { Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},

            };

            columnWidths = new float[] { 3f, 9f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region INTERPRETACION
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase(" ", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                new PdfPCell(new Phrase("INTERPRETACION", fontColumnValue)) { Colspan = 20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
     
                new PdfPCell(new Phrase("Apartado", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Exigencias psicosológicas", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Puntaje  ", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Verde", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Amarillo ", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Rojo", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase("1", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Dimensiones psicosociales", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(valorApartado1.ToString(), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 - 7 "+verde1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("8 - 10 "+amarillo1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("11 - 24 "+rojo1, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                
                new PdfPCell(new Phrase("2", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Trabajo activo y desarrollo", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(valorApartado2.ToString(), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("40 - 26 "+verde2, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("25 - 21 "+amarillo2, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("20 - 0 "+rojo2, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                

                new PdfPCell(new Phrase("3", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Inseguridad", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(valorApartado3.ToString(), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 - 1 "+verde3, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("2 - 5 "+amarillo3, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("6 - 20 "+rojo3, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase("4", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Apoyo social y calidad de  liderazgo", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(valorApartado4.ToString(), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("40 - 29 "+verde4, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("28 - 24 "+amarillo4, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("23 - 0 "+rojo4, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                new PdfPCell(new Phrase("5", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Doble presencia ", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(valorApartado5.ToString(), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("0 - 3 "+verde5, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("4 - 6 "+amarillo5, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("7 - 16 "+rojo5, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
                
                new PdfPCell(new Phrase("6", fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("Estima ", fontColumnValue)) { Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase(valorApartado6.ToString(), fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("16 - 13 "+verde6, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("12 - 11 "+amarillo6, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                new PdfPCell(new Phrase("10 - 0 "+rojo6, fontColumnValue)) { Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, FixedHeight = tamaño_celda,  MinimumHeight = tamaño_celda, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},
                
            };

            columnWidths = new float[] { 5f, 5f, 5f, 6f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
            document.Add(table);



            #endregion

            #region Firma

            ////#region Creando celdas de tipo Imagen y validando nulls

            ////// Firma del trabajador ***************************************************
            ////PdfPCell cellFirmaTrabajador = null;
            //////DirectoryInfo rutaFirma = null;
            //////rutaFirma = new DirectoryInfo(WebConfigurationManager.AppSettings["FirmaHuella"].ToString());
            //////iTextSharp.text.Image Firmajpg = iTextSharp.text.Image.GetInstance(rutaFirma +DataService.v_DocNumber + "_Firma.jpg");


            ////if (filiationData.FirmaTrabajador != null)
            ////    cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, null, null, 70, 30));
            ////else

            ////    cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));
            //////cellFirmaTrabajador = new PdfPCell(Firmajpg);

            ////// Huella del trabajador **************************************************
            ////PdfPCell cellHuellaTrabajador = null;

            //////DirectoryInfo rutaHuella = null;
            //////rutaHuella = new DirectoryInfo(WebConfigurationManager.AppSettings["FirmaHuella"].ToString());
            //////iTextSharp.text.Image Huellajpg = iTextSharp.text.Image.GetInstance(rutaHuella + DataService.v_DocNumber + "_Huella.jpg");

            ////if (filiationData.HuellaTrabajador != null)
            ////    cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, null, null, 30, 30));
            ////else
            ////    cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));
            //////cellHuellaTrabajador = new PdfPCell(Huellajpg);

            ////// Firma del doctor Auditor **************************************************

            ////PdfPCell cellFirma = null;

            ////if (DataService.FirmaMedicoMedicina != null)
            ////    cellFirma = null; //new PdfPCell(HandlingItextSharp.GetImage(DataService.FirmaMedicoMedicina, null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            ////else
            ////    cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));

            ////#endregion

            ////#region Crear tablas en duro (para la Firma y huella del trabajador)

            ////cells = new List<PdfPCell>();

            ////cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            ////cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            ////cellFirmaTrabajador.Border = PdfPCell.NO_BORDER;
            ////cellFirmaTrabajador.FixedHeight = 40F;
            ////cells.Add(cellFirmaTrabajador);
            ////cells.Add(new PdfPCell(new Phrase("FIRMA DEL EXAMINADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            ////columnWidths = new float[] { 100f };

            ////var tableFirmaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            //////***********************************************

            ////cells = new List<PdfPCell>();

            ////cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
            ////cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
            ////cellFirmaTrabajador.Border = PdfPCell.NO_BORDER;
            ////cellHuellaTrabajador.FixedHeight = 40F;
            ////cells.Add(cellHuellaTrabajador);
            ////cells.Add(new PdfPCell(new Phrase("HUELLA DEL EXAMINADO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

            ////columnWidths = new float[] { 100f };

            ////var tableHuellaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            ////#endregion

            ////cells = new List<PdfPCell>();

            ////// 1 celda vacia              
            ////cells.Add(new PdfPCell(tableFirmaTrabajador));

            ////// 1 celda vacia
            ////cells.Add(new PdfPCell(tableHuellaTrabajador));

            ////// 2 celda
            ////cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue)) { Rowspan = 2 };
            ////cell.HorizontalAlignment = Element.ALIGN_CENTER;
            ////cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            ////cells.Add(cell);

            ////// 3 celda (Imagen)
            ////cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
            ////cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
            ////cellFirma.FixedHeight = 40F;
            ////cells.Add(cellFirma);

            ////cells.Add(new PdfPCell(new Phrase("CON LA CUAL DECLARA QUE LA INFORMACIÓN DECLARADA ES VERAZ", fontColumnValue)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE });
            ////cells.Add(new PdfPCell(new Phrase("", fontColumnValue)) { Colspan = 2, UseVariableBorders = true, BorderColorLeft = BaseColor.BLACK, BorderColorRight = BaseColor.BLACK, BorderColorBottom = BaseColor.BLACK, BorderColorTop = BaseColor.WHITE });

            ////columnWidths = new float[] { 35f, 35f, 30f, 40F };
            ////table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);

            ////document.Add(table);

            #endregion

            document.Close();
            writer.Close();
            writer.Dispose();
        }
        #endregion
    }
}
