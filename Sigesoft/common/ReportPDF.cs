﻿using System;
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
    public class ReportPDF
    {         

        #region Medical Report EPS

        public static void CreateMedicalReportEPS(PacientList filiationData, List<PersonMedicalHistoryList> personMedicalHistory, List<NoxiousHabitsList> noxiousHabit, List<FamilyMedicalAntecedentsList> familyMedicalAntecedent, ServiceList anamnesis, List<ServiceComponentList> serviceComponent, List<DiagnosticRepositoryList> diagnosticRepository, string filePDF)
        {
            // step 1: creation of a document-object
            Document document = new Document();
            //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

            try
            {
                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

                //create an instance of your PDFpage class. This is the class we generated above.
                pdfPage page = new pdfPage();
                page.Dato = "Anexo-312/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
                writer.PageEvent = page;

                // step 3: we open the document
                document.Open();
                // step 4: we Add content to the document
                // we define some fonts

                #region Fonts

                Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));

                Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));


                #endregion

                #region Title
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));
                document.Add(new Paragraph("\r\n"));

                Paragraph cTitle = new Paragraph("INFORME MÉDICO", fontTitle1);
                Paragraph cTitle2 = new Paragraph("Rimac EPS", fontTitle2);
                cTitle.Alignment = Element.ALIGN_CENTER;
                cTitle2.Alignment = Element.ALIGN_CENTER;

                document.Add(cTitle);
                document.Add(cTitle2);

                #endregion

                #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
                string include = string.Empty;
                List<PdfPCell> cells = null;
                float[] columnWidths = null;
                string[] columnValues = null;
                string[] columnHeaders = null;

                //PdfPTable header1 = new PdfPTable(2);
                //header1.HorizontalAlignment = Element.ALIGN_CENTER;
                //header1.WidthPercentage = 100;
                ////header1.TotalWidth = 500;
                ////header1.LockedWidth = true;    // Esto funciona con TotalWidth
                //float[] widths = new float[] { 150f, 200f};
                //header1.SetWidths(widths);


                //Rectangle rec = document.PageSize;
                PdfPTable header2 = new PdfPTable(6);
                header2.HorizontalAlignment = Element.ALIGN_CENTER;
                header2.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                header2.SetWidths(widths1);
                //header2.SetWidthPercentage(widths1, rec);

                PdfPTable companyData = new PdfPTable(6);
                companyData.HorizontalAlignment = Element.ALIGN_CENTER;
                companyData.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                companyData.SetWidths(widthscolumnsCompanyData);

                PdfPTable filiationWorker = new PdfPTable(4);

                PdfPTable table = null;

                PdfPCell cell = null;

                #endregion

                // Salto de linea
                document.Add(new Paragraph("\r\n"));

                #region Filiación del trabajador

                PdfPCell cellPhoto = null;

                if (filiationData.b_Photo != null)
                    cellPhoto = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 15F));
                else
                    cellPhoto = new PdfPCell(new Phrase(" ", fontColumnValue));

                cellPhoto.Rowspan = 5;
                cellPhoto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPhoto.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("NOMBRES: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("APELLIDOS: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName , fontColumnValue)),                   
                    new PdfPCell(new Phrase("FOTO:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT }, cellPhoto,
                                                                                        
                    new PdfPCell(new Phrase("EDAD: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.i_Age.ToString(), fontColumnValue)),                   
                    new PdfPCell(new Phrase("SEGURO: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_TypeOfInsuranceName, fontColumnValue)),                   
                    new PdfPCell(new Phrase(" ", fontColumnValue)), 

                    new PdfPCell(new Phrase("EMPRESA: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("PARENTESCO: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_RelationshipName, fontColumnValue)),                   
                    new PdfPCell(new Phrase(" ", fontColumnValue)),      
            
                    new PdfPCell(new Phrase("NOMBRE DEL TITULAR: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_OwnerName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("CENTRO MÉDICO: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_OwnerOrganizationName, fontColumnValue)),                   
                    new PdfPCell(new Phrase(" ", fontColumnValue)),                  

                    new PdfPCell(new Phrase("MÉDICO: ", fontColumnValue)), new PdfPCell(new Phrase("Dr(a)." + filiationData.v_DoctorPhysicalExamName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("FECHA AT: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)),                   
                    new PdfPCell(new Phrase(" ", fontColumnValue)),                  
                                                   
                };

                columnWidths = new float[] { 20.6f, 40.6f, 16.6f, 34.6f, 6.6f, 14.6f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, "I. DATOS DE FILIACIÓN", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                #region Antecedentes Medicos Personales

                if (personMedicalHistory.Count == 0)
                {
                    personMedicalHistory.Add(new PersonMedicalHistoryList { v_DiseasesName = "NO REFIERE ANTECEDENTES MÉDICO PERSONALES." });
                    columnWidths = new float[] { 100f };
                    include = "v_DiseasesName";
                }
                else
                {
                    columnWidths = new float[] { 3f, 97f };
                    include = "i_Item,v_DiseasesName";
                }

                var antMedicoPer = HandlingItextSharp.GenerateTableFromList(personMedicalHistory, columnWidths, include, fontColumnValue, "II. ANTECEDENTES MÉDICO PERSONALES", fontTitleTableNegro);

                document.Add(antMedicoPer);

                #endregion

                #region Habitos Nocivos

                if (noxiousHabit.Count == 0)
                {
                    noxiousHabit.Add(new NoxiousHabitsList { v_NoxiousHabitsName = "NO REFIERE HÁBITOS NOCIVOS." });
                    columnWidths = new float[] { 100f };
                    include = "v_NoxiousHabitsName";
                }
                else
                {
                    columnWidths = new float[] { 10f, 45f, 45f };
                    include = "i_Item,v_NoxiousHabitsName,v_Frequency";
                }


                var habitoNocivo = HandlingItextSharp.GenerateTableFromList(noxiousHabit, columnWidths, include, fontColumnValue, "III. HÁBITOS NOCIVOS", fontTitleTableNegro);

                document.Add(habitoNocivo);

                #endregion

                #region Antecedentes Patológicos Familiares

                if (familyMedicalAntecedent.Count == 0)
                {
                    familyMedicalAntecedent.Add(new FamilyMedicalAntecedentsList { v_FullAntecedentName = "NO REFIERE ANTECEDENTES PATOLÓGICOS FAMILIARES." });
                    columnWidths = new float[] { 100f };
                    include = "v_FullAntecedentName";
                }
                else
                {
                    columnWidths = new float[] { 0.7f, 23.6f };
                    include = "i_Item,v_FullAntecedentName";
                }

                var pathologicalFamilyHistory = HandlingItextSharp.GenerateTableFromList(familyMedicalAntecedent, columnWidths, include, fontColumnValue, "IV. ANTECEDENTES PATOLÓGICOS FAMILIARES", fontTitleTableNegro);

                document.Add(pathologicalFamilyHistory);

                #endregion

                #region Evaluación Médica

                #region Anamnesis

                var rpta = anamnesis.i_HasSymptomId == null ? "NO" : "SI";
                var sinto = anamnesis.v_MainSymptom == null ? "NO REFIERE" : anamnesis.v_MainSymptom + " / " + anamnesis.i_TimeOfDisease + "días";
                var relato = anamnesis.v_Story == null ? "PACIENTE ASINTOMÁTICO" : anamnesis.v_Story;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ANAMNESIS: ", fontSubTitle)) { Colspan = 2 ,
                                                                            BackgroundColor = new BaseColor(System.Drawing.Color.Gray),
                                                                            HorizontalAlignment = Element.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("¿PRESENTA SÍNTOMAS?: " + rpta, fontColumnValue)),                   
                    new PdfPCell(new Phrase("SÍNTOMAS PRINCIPALES: " + sinto, fontColumnValue)),                               
                    new PdfPCell(new Phrase("RELATO: " + relato, fontColumnValue)) { Colspan = 2 },                        
                                                   
                };

                columnWidths = new float[] { 20.6f, 40.6f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "V. EVALUACIÓN MÉDICA", fontTitleTableNegro);

                document.Add(table);

                #endregion

                string[] orderPrint = new string[]
                { 
                    Sigesoft.Common.Constants.ANTROPOMETRIA_ID, 
                    Sigesoft.Common.Constants.FUNCIONES_VITALES_ID,
                    Sigesoft.Common.Constants.EXAMEN_FISICO_ID
                };

                ReportBuilder(serviceComponent, orderPrint, fontTitleTable, fontSubTitle, fontColumnValue, subTitleBackGroundColor, document);

                #endregion

                #region Hallazgos y recomendaciones

                cells = new List<PdfPCell>();

                if (diagnosticRepository != null && diagnosticRepository.Count > 0)
                {
                    columnWidths = new float[] { 0.7f, 23.6f };
                    include = "i_Item,v_RecommendationName";

                    foreach (var item in diagnosticRepository)
                    {
                        cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                        // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                        table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue);
                        cell = new PdfPCell(table);
                        cells.Add(cell);
                    }

                    columnWidths = new float[] { 20.6f, 40.6f };
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    columnWidths = new float[] { 100 };
                }

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "VI. HALLAZGOS Y RECOMENDACIONES", fontTitleTableNegro);

                document.Add(table);

                #endregion

                // Salto de linea
                document.Add(new Paragraph("\r\n"));

                #region Firma y sello Médico

                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.WidthPercentage = 20;

                columnWidths = new float[] { 10f, 10f };
                table.SetWidths(columnWidths);

                cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_RIGHT };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(" ", fontColumnValue));
                table.AddCell(cell);

                document.Add(table);

                #endregion

                // Salto de linea
                document.Add(new Paragraph("\r\n"));

                #region EVALUACION GINECOLOGICA

                var find = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GINECOLOGIA_ID);

                if (find != null)
                {
                    #region Antecedentes

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("I. ANTECEDENTES:", fontSubTitle))
                    {
                        Colspan = 6,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Gray),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************               
                    var dateFur = anamnesis.d_Fur == null ? string.Empty : anamnesis.d_Fur.Value.ToShortDateString();
                    var datePap = anamnesis.d_PAP == null ? string.Empty : anamnesis.d_PAP.Value.ToShortDateString();
                    var dateMamografia = anamnesis.d_Mamografia == null ? string.Empty : anamnesis.d_Mamografia.Value.ToShortDateString();

                    // fila 1
                    cells.Add(new PdfPCell(new Phrase("Menarquia:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_Menarquia, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Régimen Catamenial:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_CatemenialRegime, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("FUR:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(dateFur, fontColumnValue)));

                    // fila 2
                    cells.Add(new PdfPCell(new Phrase("GESTAPARA:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_Gestapara, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Fecha de último PAP:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(datePap, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Fecha de última mamografía:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(dateMamografia, fontColumnValue)));

                    // fila 3
                    cells.Add(new PdfPCell(new Phrase("Cirugía Geinecológica:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_CiruGine, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Método anticonceptivo:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_Mac, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));

                    columnWidths = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, find.v_ComponentName.ToUpper(), fontTitleTableNegro);
                    document.Add(table);

                    #endregion

                    #region Sintomas

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("II. SÍNTOMAS:", fontSubTitle))
                    {
                        Colspan = 6,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Gray),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************               

                    // fila 1
                    cells.Add(new PdfPCell(new Phrase("Flujo Vaginal: " + "Si", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Dispareunia:" + "No", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Incontinencia Urinaria:" + "No", fontColumnValue)));

                    // fila 2
                    cells.Add(new PdfPCell(new Phrase("Otros:" + " ", fontColumnValue)) { Colspan = 3 });

                    columnWidths = new float[] { 16.6f, 16.6f, 16.6f };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    document.Add(table);

                    #endregion

                    #region Examen Clinico

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("III. EXAMEN CLÍNICO:", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Gray),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************               

                    // fila 1
                    cells.Add(new PdfPCell(new Phrase("Hallazgos: ", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Sin alteración", fontColumnValue)));

                    columnWidths = new float[] { 16f, 84f };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    document.Add(table);

                    #endregion

                    #region Aparato genital

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("IV. APARATO GENITAL:", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Gray),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************               

                    // fila 1
                    cells.Add(new PdfPCell(new Phrase("Hallazgos: ", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Sin alteración", fontColumnValue)));

                    columnWidths = new float[] { 16f, 84f };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    document.Add(table);

                    #endregion

                    #region Exámenes auxiliares

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("V. EXÁMENES AUXILIARES:", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = new BaseColor(System.Drawing.Color.Gray),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************               

                    // fila 1
                    cells.Add(new PdfPCell(new Phrase("Papanicolau: ", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Sin presencia de celulas anormales", fontColumnValue)));

                    columnWidths = new float[] { 16f, 84f };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    document.Add(table);

                    #endregion

                }

                #endregion

                // step 5: we close the document
                document.Close();

                RunFile(filePDF);

            }
            catch (DocumentException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }

        }

        private static void ReportBuilder(List<ServiceComponentList> serviceComponent, string[] order, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor, Document document)
        {
            if (order != null)
            {
                var sortEntity = GetSortEntity(order, serviceComponent);

                if (sortEntity != null)
                {
                    foreach (var ent in sortEntity)
                    {
                        var table = TableBuilder(ent, fontTitle, fontSubTitle, fontColumnValue, SubtitleBackgroundColor);

                        if (table != null)
                            document.Add(table);
                    }
                }
            }
        }

        private static PdfPTable TableBuilder(ServiceComponentList serviceComponent, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor)
        {
            PdfPTable table = null;
            List<PdfPCell> cells = null;
            PdfPCell cell = null;
            float[] columnWidths = null;

            switch (serviceComponent.v_ComponentId)
            {
                case Sigesoft.Common.Constants.ANTROPOMETRIA_ID:

                    #region ANTROPOMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var talla = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID);
                        var peso = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID);
                        var imc = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);
                        var periCintura = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID);

                        cells.Add(new PdfPCell(new Phrase("TALLA: " + talla.v_Value1 + " " + talla.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("PESO: " + peso.v_Value1 + " " + peso.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("IMC: " + imc.v_Value1 + " " + imc.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("PERÍMETRO CINTURA: " + periCintura.v_Value1 + " " + periCintura.v_MeasurementUnitName, fontColumnValue)));

                        columnWidths = new float[] { 20.6f, 40.6f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.FUNCIONES_VITALES_ID:

                    #region FUNCIONES VITALES

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var pas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID);
                        var pad = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID);
                        var fecCardiaca = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID);
                        var fecRespiratoria = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID);
                        var satO2 = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID);

                        cells.Add(new PdfPCell(new Phrase("P.A.S: " + pas.v_Value1 + " " + pas.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("P.A.D: " + pad.v_Value1 + " " + pad.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("FRECUENCIA CARDÍACA: " + fecCardiaca.v_Value1 + " " + fecCardiaca.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("FRECUENCIA RESPIRATORIA: " + fecRespiratoria.v_Value1 + " " + fecRespiratoria.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("SAT. O2: " + satO2.v_Value1 + " " + satO2.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));

                        columnWidths = new float[] { 20.6f, 40.6f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_FISICO_ID:

                    #region EXAMEN FÍSICO

                    cells = new List<PdfPCell>();

                    //Subtitulo
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var componentField = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_HALLAZGOS_ID);

                        if (componentField != null)
                        {
                            var split = componentField.v_Value1.Split('\r', '\n').Where(p => p != string.Empty).ToArray();

                            foreach (var str in split)
                            {
                                cells.Add(new PdfPCell(new Phrase(str, fontColumnValue)));
                            }
                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                        }
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100 };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.GINECOLOGIA_ID:

                    break;
                default:
                    // Ejm: Oftalmología, Rx, etc

                    cells = new List<PdfPCell>();

                    //Subtitulo
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var find = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CONCLUSIONES);

                        if (find != null)
                        {
                            var text = find.v_ConclusionAndDiagnostic == null ? "NO SE HAN REGISTRADO DATOS." : find.v_ConclusionAndDiagnostic;

                            cells.Add(new PdfPCell(new Phrase(text, fontColumnValue)));
                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                        }
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));

                    }

                    columnWidths = new float[] { 100 };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    break;
            }

            return table;

        }


        #endregion
    
        #region Report For The Worker

        public static void CreateMedicalReportForTheWorker(PacientList filiationData, List<PersonMedicalHistoryList> personMedicalHistory, List<NoxiousHabitsList> noxiousHabit, List<FamilyMedicalAntecedentsList> familyMedicalAntecedent, ServiceList anamnesis, List<ServiceComponentList> serviceComponent, List<DiagnosticRepositoryList> diagnosticRepository, string customerOrganizationName, organizationDto infoEmpresaPropietaria,string filePDF)
        {
            // step 1: creation of a document-object
            Document document = new Document();
            //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

            try
            {
                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

                //create an instance of your PDFpage class. This is the class we generated above.
                pdfPage page = new pdfPage();
                page.Dato = "FMT/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
                writer.PageEvent = page;

                // step 3: we open the document
                document.Open();
                // step 4: we Add content to the document
                // we define some fonts

                #region Fonts

                Font fontTitle1 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));


                #endregion


     #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
                string include = string.Empty;
                //List<PdfPCell> cells = null;
                //float[] columnWidths = null;
                string[] columnValues = null;
                string[] columnHeaders = null;

                //PdfPTable header1 = new PdfPTable(2);
                //header1.HorizontalAlignment = Element.ALIGN_CENTER;
                //header1.WidthPercentage = 100;
                ////header1.TotalWidth = 500;
                ////header1.LockedWidth = true;    // Esto funciona con TotalWidth
                //float[] widths = new float[] { 150f, 200f};
                //header1.SetWidths(widths);


                //Rectangle rec = document.PageSize;
                PdfPTable header2 = new PdfPTable(6);
                header2.HorizontalAlignment = Element.ALIGN_CENTER;
                header2.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                header2.SetWidths(widths1);
                //header2.SetWidthPercentage(widths1, rec);

                PdfPTable companyData = new PdfPTable(6);
                companyData.HorizontalAlignment = Element.ALIGN_CENTER;
                companyData.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                companyData.SetWidths(widthscolumnsCompanyData);

                PdfPTable filiationWorker = new PdfPTable(4);

                //PdfPTable table = null;

                //PdfPCell cell = null;

                #endregion
      

                #region Title
                List<PdfPCell> cells = null;
                PdfPCell CellLogo = null;
                cells = new List<PdfPCell>();
                float[] columnWidths = null;
                PdfPCell cellPhoto1 = null;
                PdfPTable table = null;
                if (filiationData.b_Photo != null)
                    cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                else
                    cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

                if (infoEmpresaPropietaria.b_Image != null)
                {
                    CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                }
                else
                {
                    CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
                }

                columnWidths = new float[] { 100f };

                var cellsTit = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("INFORME MÉDICO", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
            };

                table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

                cells.Add(CellLogo);
                cells.Add(new PdfPCell(table));
                cells.Add(cellPhoto1);

                columnWidths = new float[] { 20f, 60f, 20f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
                document.Add(table);

                #endregion
                
                #region Filiación del trabajador

                PdfPCell cellPhoto = null;
                PdfPCell cell = null;


                if (filiationData.b_Photo != null)
                    cellPhoto = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 15F));
                else
                    cellPhoto = new PdfPCell(new Phrase("Sin Foto", fontColumnValue));

                cellPhoto.Rowspan = 5;
                cellPhoto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPhoto.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("NOMBRES: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("APELLIDOS: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName , fontColumnValue)),  
                    new PdfPCell(new Phrase("EDAD: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.i_Age.ToString(), fontColumnValue)),      
             
                    new PdfPCell(new Phrase("PUESTO DE TRABAJO: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_CurrentOccupation, fontColumnValue)),
                    new PdfPCell(new Phrase("EMPRESA: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)) {Colspan=3},    
               

                    new PdfPCell(new Phrase("CENTRO MÉDICO: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_OwnerOrganizationName, fontColumnValue)){Colspan=5},                   
              
                                         
                    new PdfPCell(new Phrase("MÉDICO: ", fontColumnValue)), new PdfPCell(new Phrase("Dr(a)." + filiationData.v_DoctorPhysicalExamName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("FECHA ATENCIÓN: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)){Colspan=3},                 
                           
                                                   
                };

                columnWidths = new float[] { 20.6f, 40.6f, 16.6f, 34.6f, 8f, 14.6f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, "I. DATOS DE FILIACIÓN", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                #region Antecedentes Medicos Personales

                if (personMedicalHistory.Count == 0)
                {
                    personMedicalHistory.Add(new PersonMedicalHistoryList { v_DiseasesName = "No Refiere Antecedentes Médico Personales." });
                    columnWidths = new float[] { 100f };
                    include = "v_DiseasesName";
                }
                else
                {
                    columnWidths = new float[] { 3f, 97f };
                    include = "i_Item,v_DiseasesName";
                }

                var antMedicoPer = HandlingItextSharp.GenerateTableFromList(personMedicalHistory, columnWidths, include, fontColumnValue, "II. ANTECEDENTES MÉDICO PERSONALES", fontTitleTableNegro);

                document.Add(antMedicoPer);

                #endregion

                //#region Habitos Nocivos

                //if (noxiousHabit.Count == 0)
                //{
                //    noxiousHabit.Add(new NoxiousHabitsList { v_NoxiousHabitsName = "No Aplica Hábitos Nocivos a la Atención." });
                //    columnWidths = new float[] { 100f };
                //    include = "v_NoxiousHabitsName";
                //}
                //else
                //{
                //    columnWidths = new float[] { 3f, 32f, 65f };
                //    include = "i_Item,v_NoxiousHabitsName,v_Frequency";
                //}

                //var habitoNocivo = HandlingItextSharp.GenerateTableFromList(noxiousHabit, columnWidths, include, fontColumnValue, "III. HÁBITOS NOCIVOS", fontTitleTableNegro);

                //document.Add(habitoNocivo);

                //#endregion

                //#region Antecedentes Patológicos Familiares

                //if (familyMedicalAntecedent.Count == 0)
                //{
                //    familyMedicalAntecedent.Add(new FamilyMedicalAntecedentsList { v_FullAntecedentName = "No Refiere Antecedentes Patológicos Familiares." });
                //    columnWidths = new float[] { 100f };
                //    include = "v_FullAntecedentName";
                //}
                //else
                //{
                //    columnWidths = new float[] { 0.7f, 23.6f };
                //    include = "i_Item,v_FullAntecedentName";
                //}

                //var pathologicalFamilyHistory = HandlingItextSharp.GenerateTableFromList(familyMedicalAntecedent, columnWidths, include, fontColumnValue, "IV. ANTECEDENTES PATOLÓGICOS FAMILIARES", fontTitleTableNegro);

                //document.Add(pathologicalFamilyHistory);

                //#endregion

                #region Evaluación Médica

                #region Anamnesis

                var rpta = anamnesis.i_HasSymptomId == null || anamnesis.i_HasSymptomId == 0 ? "No" : "Si";
                var sinto = anamnesis.v_MainSymptom == null ? "NO REFIERE" : anamnesis.v_MainSymptom + " / " + anamnesis.i_TimeOfDisease + "días";
                var relato = anamnesis.v_Story == null ? "PACIENTE ASINTOMÁTICO" : anamnesis.v_Story;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ANAMNESIS: ", fontSubTitleNegroNegrita)) { Colspan = 3 ,HorizontalAlignment = Element.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("¿PRESENTA SÍNTOMAS?: " + rpta, fontColumnValue)),                   
                    new PdfPCell(new Phrase("SÍNTOMAS PRINCIPALES: " + sinto, fontColumnValue)){ Colspan = 2 ,HorizontalAlignment = Element.ALIGN_LEFT },                               
                    new PdfPCell(new Phrase("RELATO:" + relato, fontColumnValue)) { Colspan = 3 ,HorizontalAlignment = Element.ALIGN_LEFT },                        
                                                   
                };

                columnWidths = new float[] { 33f, 33f,33f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "III. EVALUACIÓN MÉDICA", fontTitleTableNegro);

                document.Add(table);

                #endregion

                string[] orderPrint = new string[]
                { 
                    Sigesoft.Common.Constants.ANTROPOMETRIA_ID, 
                    Sigesoft.Common.Constants.FUNCIONES_VITALES_ID,
                    Sigesoft.Common.Constants.EXAMEN_FISICO_ID,
                    Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID,
                    Sigesoft.Common.Constants.INFORME_LABORATORIO_ID,
                    Sigesoft.Common.Constants.GLUCOSA_ID,
                    Sigesoft.Common.Constants.PERFIL_LIPIDICO,
                    Sigesoft.Common.Constants.COLESTEROL_ID,
                    Sigesoft.Common.Constants.TRIGLICERIDOS_ID,
                    Sigesoft.Common.Constants.TGO_ID,
                    Sigesoft.Common.Constants.TGP_ID,
                    "N009-ME000000045",
                    "N009-ME000000046",
                    Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID
                };


                ReportBuilderReportForTheWorker(serviceComponent, orderPrint, fontTitleTable, fontSubTitleNegroNegrita, fontColumnValue, subTitleBackGroundColor, document, diagnosticRepository);

                #endregion

                ////Salto de Pàgina
                //document.NewPage();
                #region Hallazgos y recomendaciones

                cells = new List<PdfPCell>();

                var filterDiagnosticRepository = diagnosticRepository.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);

                if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
                {
                    columnWidths = new float[] { 0.7f, 23.6f};
                    include = "i_Item,Valor1";

                    foreach (var item in filterDiagnosticRepository)
                    {
                        if (item.v_DiseasesId == "N009-DD000000029")
                        {
                            cell = new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            cells.Add(cell);
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            cells.Add(cell);
                        }

                        ListaComun oListaComun = null;
                        List<ListaComun> Listacomun = new List<ListaComun>();

                        if (item.Recomendations.Count > 0)
                        {
                            oListaComun = new ListaComun();
                            oListaComun.Valor1 = "--------RECOMENDACIONES------";
                            oListaComun.i_Item = "";
                            Listacomun.Add(oListaComun);
                        }
                      

                        int Contador = 1;
                        foreach (var Reco in item.Recomendations)
                        {
                            oListaComun = new ListaComun();

                            oListaComun.Valor1 = Reco.v_RecommendationName;
                            oListaComun.i_Item = Contador.ToString(); 
                            Listacomun.Add(oListaComun);
                            Contador++;
                        }

                        if (item.Restrictions.Count > 0)
                        {
                            oListaComun = new ListaComun();
                            oListaComun.Valor1 = "--------RESTRICCIONES------";
                            oListaComun.i_Item = "";
                            Listacomun.Add(oListaComun);

                        }
                        int Contador1 = 1;
                        foreach (var Rest in item.Restrictions)
                        {
                            oListaComun = new ListaComun();
                            oListaComun.Valor1 = Rest.v_RestrictionName;
                            oListaComun.i_Item = Contador1.ToString(); 
                            Listacomun.Add(oListaComun);
                            Contador1++;
                        }

                        // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                        table = HandlingItextSharp.GenerateTableFromList(Listacomun, columnWidths, include, fontColumnValue);
                        cell = new PdfPCell(table);

                        cells.Add(cell);
                    }

                    columnWidths = new float[] { 20.6f, 40.6f };
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    columnWidths = new float[] { 100 };
                }

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "IV. CONCLUSIONES", fontTitleTableNegro);

                document.Add(table);

                #endregion

                //// Salto de linea
                //document.Add(new Paragraph("\r\n"));

           

                #region Firma y sello Médico
               
                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.WidthPercentage = 40;

                columnWidths = new float[] { 15f, 25f };
                table.SetWidths(columnWidths);

                PdfPCell cellFirma = null;

                if (filiationData.FirmaDoctorAuditor != null)
                    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaDoctorAuditor, null, null, 120, 45));
                else
                    cellFirma = new PdfPCell(new Phrase(" ", fontColumnValue));
                                        
                cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.FixedHeight = 50F;

                cell = new PdfPCell(new Phrase("FIRMA Y SELLO MÉDICO", fontColumnValue));                                                  
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                                  
                table.AddCell(cell);             
                table.AddCell(cellFirma);

                document.Add(table);

                #endregion

                #region EVALUACION GINECOLOGICA

                var findGineco = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GINECOLOGIA_ID);

                if (findGineco != null)
                {
                    // Nueva Hoja               
                    document.NewPage();

                    #region Antecedentes

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("I. ANTECEDENTES:", fontSubTitleNegroNegrita))
                    {
                        Colspan = 6,
                        BackgroundColor = new BaseColor(252, 252, 252),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************               
                    var dateFur = anamnesis.d_Fur == null ? string.Empty : anamnesis.d_Fur.Value.ToShortDateString();
                    //var datePap = anamnesis.d_PAP == null ? string.Empty : anamnesis.d_PAP.Value.ToShortDateString();
                    //var dateMamografia = anamnesis.d_Mamografia == null ? string.Empty : anamnesis.d_Mamografia.Value.ToShortDateString();
                    var datePap = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_ANTECEDENTES_PERSONALES_FECHA_ULTIMO_PAP);
                    var dateMamografia = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTECEDENTES_PERSONALES_FECHA_ULTIMA_MAMOGRAFIA);
                    var antPersonalesGineco = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_ANTECEDENTES_PERSONALES_ANTECEDENTES);

                    // fila 1
                    cells.Add(new PdfPCell(new Phrase("Menarquia:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_Menarquia, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Régimen Catamenial:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_CatemenialRegime, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("FUR:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(dateFur, fontColumnValue)));

                    // fila 2
                    cells.Add(new PdfPCell(new Phrase("GESTAPARA:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_Gestapara, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Fecha último PAP:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(datePap == null ? string.Empty : datePap.v_Value1, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Fecha última mamografía:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(dateMamografia == null ? string.Empty : dateMamografia.v_Value1, fontColumnValue)));

                    // fila 3
                    cells.Add(new PdfPCell(new Phrase("Cirugía Geinecológica:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_CiruGine, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase("Método anticonceptivo:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(anamnesis.v_Mac, fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(" ", fontColumnValue)));

                    // fila 4
                    cells.Add(new PdfPCell(new Phrase("Antecedentes Personales:", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(antPersonalesGineco == null ? string.Empty : antPersonalesGineco.v_Value1, fontColumnValue)) { Colspan = 5 });

                    columnWidths = new float[] { 19.6f, 16.6f, 19.6f, 16.6f, 19.6f, 12.6f, };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, findGineco.v_ComponentName.ToUpper(), fontTitleTableNegro);
                    document.Add(table);

                    #endregion

                    #region Sintomas

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("II. SÍNTOMAS:", fontSubTitleNegroNegrita))
                    {
                        Colspan = 6,
                        BackgroundColor = new BaseColor(252, 252, 252),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************    

                    // Definir variables para la Xs <por defecto en blanco>
                    string leucorreaSi = string.Empty, dispareuniaSi = string.Empty, incontinenciaUrinariaSi = string.Empty, otrosSi = string.Empty;
                    string leucorreaNo = string.Empty, dispareuniaNo = string.Empty, incontinenciaUrinariaNo = string.Empty, otrosNo = string.Empty;

                    string leucorreaComentario = string.Empty, dispareuniaComentario = string.Empty, incontinenciaUrinariaComentario = string.Empty, otrosComentario = string.Empty;

                    if (findGineco.ServiceComponentFields.Count > 0)
                    {
                        var leucorrea = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_LEUCORREA);
                        var dispareunia = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_DISPAREUNIA);
                        var incontinenciaUrinaria = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_INCONTINENCIA_URINARIA);
                        var otros = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_OTROS);

                        // Descripciones *****************************************
                        leucorreaComentario = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_LEUCORREA_COMENTARIO).v_Value1;
                        dispareuniaComentario = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_DISPAREUNIA_COMENTARIO).v_Value1;
                        incontinenciaUrinariaComentario = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_INCONTINENCIA_URINARIA_COMENTARIO).v_Value1;
                        otrosComentario = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_SINTOMAS_OTROS_COMENTARIO).v_Value1;
                        //***********************************************************

                        #region Eval valores X

                        // 
                        if (leucorrea.v_Value1 == "1")
                            leucorreaSi = "X";
                        else
                            leucorreaNo = "X";

                        if (dispareunia.v_Value1 == "1")
                            dispareuniaSi = "X";
                        else
                            dispareuniaNo = "X";

                        if (incontinenciaUrinaria.v_Value1 == "1")
                            incontinenciaUrinariaSi = "X";
                        else
                            incontinenciaUrinariaNo = "X";

                        if (otros.v_Value1 == "1")
                            otrosSi = "X";
                        else
                            otrosNo = "X";

                        #endregion

                    }

                    #region Campos

                    // fila 1
                    cells.Add(new PdfPCell(new Phrase("Leucorrea:", fontColumnValue)) { Border = PdfPCell.LEFT_BORDER | PdfPCell.TOP_BORDER });
                    cells.Add(new PdfPCell(new Phrase("Si", fontColumnValue)) { Border = PdfPCell.TOP_BORDER });
                    cells.Add(new PdfPCell(new Phrase(leucorreaSi, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("NO", fontColumnValue)) { Border = PdfPCell.TOP_BORDER });
                    cells.Add(new PdfPCell(new Phrase(leucorreaNo, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("Descripción: " + leucorreaComentario, fontColumnValue)));
                    // fila 2
                    cells.Add(new PdfPCell(new Phrase("Dispareunia:", fontColumnValue)) { Border = PdfPCell.LEFT_BORDER });
                    cells.Add(new PdfPCell(new Phrase("Si", fontColumnValue)) { Border = PdfPCell.NO_BORDER });
                    cells.Add(new PdfPCell(new Phrase(dispareuniaSi, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("NO", fontColumnValue)) { Border = PdfPCell.NO_BORDER });
                    cells.Add(new PdfPCell(new Phrase(dispareuniaNo, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("Descripción: " + dispareuniaComentario, fontColumnValue)));
                    // fila 3
                    cells.Add(new PdfPCell(new Phrase("Incontinencia Urinaria: ", fontColumnValue)) { Border = PdfPCell.LEFT_BORDER });
                    cells.Add(new PdfPCell(new Phrase("Si", fontColumnValue)) { Border = PdfPCell.NO_BORDER });
                    cells.Add(new PdfPCell(new Phrase(incontinenciaUrinariaSi, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("NO", fontColumnValue)) { Border = PdfPCell.NO_BORDER });
                    cells.Add(new PdfPCell(new Phrase(incontinenciaUrinariaNo, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("Descripción: " + incontinenciaUrinariaComentario, fontColumnValue)));

                    // fila 4
                    cells.Add(new PdfPCell(new Phrase("Otros:", fontColumnValue)) { Border = PdfPCell.LEFT_BORDER });
                    cells.Add(new PdfPCell(new Phrase("Si", fontColumnValue)) { Border = PdfPCell.NO_BORDER });
                    cells.Add(new PdfPCell(new Phrase(otrosSi, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("NO", fontColumnValue)) { Border = PdfPCell.NO_BORDER });
                    cells.Add(new PdfPCell(new Phrase(otrosNo, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                    cells.Add(new PdfPCell(new Phrase("Descripción: " + otrosComentario, fontColumnValue)));

                    #endregion

                    columnWidths = new float[] { 17f, 3.5f, 2.5f, 3.5f, 2.5f, 71f };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    document.Add(table);

                    #endregion

                    #region Evaluación Ginecológica

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("III. EVALUACIÓN GINECOLÓGICA:", fontSubTitleNegroNegrita))
                    {
                        Colspan = 2,
                        BackgroundColor = new BaseColor(252, 252, 252),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************    

                    if (findGineco.ServiceComponentFields.Count > 0)
                    {
                        var hallazgos = findGineco.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_HALLAZGOS_HALLAZGOS);

                        if (hallazgos != null)
                        {
                            cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(hallazgos.v_Value1) ? "No se han registrado datos." : hallazgos.v_Value1, fontColumnValue)));
                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        }
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    document.Add(table);

                    #endregion

                    #region Examen de Mama

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("IV. EXAMEN DE MAMA:", fontSubTitleNegroNegrita))
                    {
                        Colspan = 2,
                        BackgroundColor = new BaseColor(252, 252, 252),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //*****************************************    

                    var findExamenMama = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_MAMA_ID);

                    if (findExamenMama != null)
                    {
                        if (findExamenMama.ServiceComponentFields.Count > 0)
                        {
                            var exMamaHallazgos = findExamenMama.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GINECOLOGIA_EX_MAMA_HALLAZGOS_HALLAZGOS);

                            if (exMamaHallazgos != null)
                            {
                                cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(exMamaHallazgos.v_Value1) ? "No se han registrado datos." : exMamaHallazgos.v_Value1, fontColumnValue)));
                            }
                            else
                            {
                                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                            }
                        }
                        else
                        {
                            cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        }

                        columnWidths = new float[] { 100f };

                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                        document.Add(table);
                    }

                    #endregion

                    #region Exámenes auxiliares

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("V. EXÁMENES AUXILIARES:", fontSubTitleNegroNegrita))
                    {
                        Colspan = 2,
                        BackgroundColor = new BaseColor(252, 252, 252),
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    cells.Add(cell);

                    //Resultados de PAP *****************************************               
                    string papRsultEvalX = "No se han registrado datos.";
                    string mamografiaResultEvalX = "No se han registrado datos.";

                    var findPapaNicolau = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PAPANICOLAU_ID);

                    if (findPapaNicolau != null)
                    {
                        if (findPapaNicolau.ServiceComponentFields.Count > 0)
                        {
                            var papRsultEval = findPapaNicolau.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PAPANICOLAU_HALLAZGOS);
                            var mamografiaResultEval = findPapaNicolau.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PAPANICOLAU_RADIOGRAFIA_HALLAZGOS);

                            if (papRsultEval != null)
                                if (!string.IsNullOrEmpty(papRsultEval.v_Value1))
                                    papRsultEvalX = papRsultEval.v_Value1;

                            if (mamografiaResultEval != null)
                                if (!string.IsNullOrEmpty(mamografiaResultEval.v_Value1))
                                    mamografiaResultEvalX = mamografiaResultEval.v_Value1;
                        }                                                    

                    }

                    // Fila
                    cells.Add(new PdfPCell(new Phrase("Resultados de PAP: ", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(papRsultEvalX, fontColumnValue)));

                    // Fila
                    cells.Add(new PdfPCell(new Phrase("Resultados de Radiografía: ", fontColumnValue)));
                    cells.Add(new PdfPCell(new Phrase(mamografiaResultEvalX, fontColumnValue)));

                    columnWidths = new float[] { 25f, 75f };

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                    document.Add(table);
                              
                    #endregion

                    #region Hallazgos y recomendaciones

                    cells = new List<PdfPCell>();

                    var diagnosticRepositoryGineco = diagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_MAMA_ID);

                    if (diagnosticRepositoryGineco != null && diagnosticRepositoryGineco.Count > 0)
                    {
                        columnWidths = new float[] { 0.7f, 23.6f };
                        include = "i_Item,v_RecommendationName";

                        foreach (var item in diagnosticRepositoryGineco)
                        {
                            cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            cells.Add(cell);
                            // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                            table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue);
                            cell = new PdfPCell(table);
                            cells.Add(cell);
                        }

                        columnWidths = new float[] { 20.6f, 40.6f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100 };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "VI. HALLAZGOS Y RECOMENDACIONES", fontTitleTableNegro);

                    document.Add(table);

                    #endregion

                    // Salto de linea
                    document.Add(new Paragraph("\r\n"));

                    #region Firma y sello Médico

                    table = new PdfPTable(2);
                    table.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.WidthPercentage = 30;

                    columnWidths = new float[] { 15f, 25f };
                    table.SetWidths(columnWidths);

                    PdfPCell cellFirmaGineco = null;

                    if (findGineco.FirmaMedico != null)
                        cellFirmaGineco = new PdfPCell(HandlingItextSharp.GetImage(findGineco.FirmaMedico, 40F));
                    else
                        cellFirmaGineco = new PdfPCell(new Phrase("Sin Firma", fontColumnValue));

                    cellFirmaGineco.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellFirmaGineco.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cellFirmaGineco.FixedHeight = 30F;

                    cell = new PdfPCell(new Phrase("Firma y Sello Médico", fontColumnValue));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                    table.AddCell(cell);
                    table.AddCell(cellFirmaGineco);

                    document.Add(table);

                    #endregion

                }
                                            
                #endregion

                // step 5: we close the document
                document.Close();
                writer.Close();
                writer.Dispose();
                //RunFile(filePDF);

            }
            catch (DocumentException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }

        }

        private static void ReportBuilderReportForTheWorker(List<ServiceComponentList> serviceComponent, string[] order, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor, Document document, List<DiagnosticRepositoryList> diagnosticRepository)
        {
            if (order != null)
            {
                var sortEntity = GetSortEntity(order, serviceComponent);

                if (sortEntity != null)
                {
                    foreach (var ent in sortEntity)
                    {
                        var table = TableBuilderReportForTheWorker(ent, fontTitle, fontSubTitle, fontColumnValue, SubtitleBackgroundColor, diagnosticRepository);

                        if (table != null)
                            document.Add(table);
                    }
                }
            }
        }

        private static PdfPTable TableBuilderReportForTheWorker(ServiceComponentList serviceComponent, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor, List<DiagnosticRepositoryList> diagnosticRepository)
        {
            PdfPTable table = null;
            List<PdfPCell> cells = null;
            PdfPCell cell = null;
            float[] columnWidths = null;

            switch (serviceComponent.v_ComponentId)
            {
                case Sigesoft.Common.Constants.ANTROPOMETRIA_ID:

                    #region ANTROPOMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var talla = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID);
                        var peso = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID);
                        var imc = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);
                        var periCintura = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID);

                        cells.Add(new PdfPCell(new Phrase("TALLA: " + talla.v_Value1 + " " + talla.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("PESO: " + peso.v_Value1 + " " + peso.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("IMC: " + imc.v_Value1 + " " + imc.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("PERÍMETRO ABDOMINAL: " + periCintura.v_Value1 + " " + periCintura.v_MeasurementUnitName, fontColumnValue)));

                        columnWidths = new float[] { 25f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.FUNCIONES_VITALES_ID:

                    #region FUNCIONES VITALES

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 5,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var pas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID);
                        var pad = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID);
                        var fecCardiaca = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID);
                        var fecRespiratoria = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID);
                        var satO2 = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID);

                        cells.Add(new PdfPCell(new Phrase("P.A.S: " + pas.v_Value1 + " " + pas.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("P.A.D: " + pad.v_Value1 + " " + pad.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("FRECUENCIA CARDÍACA: " + fecCardiaca.v_Value1 + " " + fecCardiaca.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("FRECUENCIA RESPIRATORIA:" + fecRespiratoria.v_Value1 + " " + fecRespiratoria.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("SAT. O2: " + satO2.v_Value1 + " " + satO2.v_MeasurementUnitName, fontColumnValue)));

                        columnWidths = new float[] { 12f, 10f, 22f, 25f, 10f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_FISICO_ID:

                    #region EXAMEN FÍSICO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.AUDIOMETRIA_ID:

                    #region AUDIOMETRIA

                    cells = new List<PdfPCell>();
                     
                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var concat = string.Join(", ", query.Select(p => p.v_DiseasesName));
                        var conclusion = string.Join(", ", serviceComponent.DiagnosticRepository.Select(p => p.v_DiseasesName));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(conclusion) ? "NO SE HAN REGISTRADO DATOS." : conclusion, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID:

                    #region ELECTROCARDIOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID);
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1, fontColumnValue)));
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.ESPIROMETRIA_ID:

                    #region ESPIROMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var resultados = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_RESULTADO_ID);
                        //var obs = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_OBSERVACION_ID);
                        //cells.Add(new PdfPCell(new Phrase(resultados.v_Value1Name + ", " + obs.v_Value1, fontColumnValue)));
                        var dxEspi = serviceComponent.DiagnosticRepository;  //     .Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);

                        var join = string.Join(", ", dxEspi.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "NO SE HAN REGISTRADO DATOS." : join, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_ID:

                    #region EVALUACION_ERGONOMICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var conclusion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_CONCLUSION_ID);
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVALUACION_ERGONOMICA_DESCRIPCION_ID);
                        cells.Add(new PdfPCell(new Phrase(conclusion.v_Value1Name + ", " + descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID:

                    #region ALTURA_ESTRUCTURAL

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var apto = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_APTO_ID);
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID);
                        cells.Add(new PdfPCell(new Phrase(apto.v_Value1Name + ", " + descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_ID:

                    #region ALTURA_GEOGRAFICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var apto = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_APTO_ID);
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ALTURA_GEOGRAFICA_DESCRIPCION_ID);
                        cells.Add(new PdfPCell(new Phrase(apto.v_Value1Name + ", " + descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1:

                    #region OSTEO_MUSCULAR

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var apto = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.APTITUD);
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.DESCRIPCION);
                        cells.Add(new PdfPCell(new Phrase(apto.v_Value1Name + ", " + descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.OFTALMOLOGIA_ID:

                    #region OFTALMOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {

                        var dxOft = diagnosticRepository.FindAll(p => p.i_CategoryId == 14);
                        //var dxOft = serviceComponent.DiagnosticRepository;
                        var join = string.Join(", ", dxOft.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "NO SE HAN REGISTRADO DATOS." : join, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID:

                    #region PRUEBA_ESFUERZO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PRUEBA_ESFUERZO_DESCRIPCION_ID);

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "No se han registrado datos." : descripcion.v_Value1, fontColumnValue)));
                        var dxPE = serviceComponent.DiagnosticRepository;//          .Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);

                        var join = string.Join(", ", dxPE.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "NO SE HAN REGISTRADO DATOS." : join, fontColumnValue)));


                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_ID:

                    #region TAMIZAJE_DERMATOLOGIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EXAMEN_FISICO_7C_ID:

                    #region EXAMEN_FISICO_7C

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_HALLAZGOS_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
       
                case Sigesoft.Common.Constants.INFORME_LABORATORIO_ID:

                    #region LABORATORIO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_HALLAZGO_PATOLOGICO_LABORATORIO_ID);

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1, fontColumnValue)));

                        var dxLab = diagnosticRepository.FindAll(p => p.i_CategoryId==1);//          .Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);

                        var join = string.Join(", ", dxLab.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "NO SE HAN REGISTRADO DATOS." : join, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.GLUCOSA_ID:

                    #region GLUCOSA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DESCRIPCION);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1 + " (mg/dl)", fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 20f,80f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.PERFIL_LIPIDICO:
                       #region COLESTEROL Y/O TRIGLICERIDOS

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcionColesterol = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcionColesterol.v_Value1) ? "" : "COLESTEROL: "+ descripcionColesterol.v_Value1 + " (mg/dl)", fontColumnValue)));

                        var descripcionTriglicerido = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcionTriglicerido.v_Value1) ? "" : "TRIGLICERIDOS: " + descripcionTriglicerido.v_Value1 + " (mg/dl)", fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 20f, 40f, 40f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion
                    break;

                case Sigesoft.Common.Constants.COLESTEROL_ID:

                    #region COLESTEROL

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_COLESTEROL_TOTAL_ID || p.v_ComponentFieldsId == Sigesoft.Common.Constants.COLESTEROL_TOTAL);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1 + " (mg/dl)", fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 20f, 80f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.TRIGLICERIDOS_ID:

                    #region TRIGLICERIDOS

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS || p.v_ComponentFieldsId == Sigesoft.Common.Constants.TRIGLICERIDOS);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1 + " (mg/dl)", fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 20f, 80f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;


                case Sigesoft.Common.Constants.TGO_ID:
                    #region TGO_ID
                     cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {


                        var tgo = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGO_BIOQUIMICA_TGO);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(tgo.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : tgo.v_Value1 + " (U/l)", fontColumnValue)));
                        

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 20f, 80f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);


                    #endregion
                    break;
                case Sigesoft.Common.Constants.TGP_ID:
                    #region TGP_ID

                     cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {

                        var tgp = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TGP_BIOQUIMICA_TGP);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(tgp.v_Value1) ? "NO SE HAN REGISTRADO DATOS." :  tgp.v_Value1 + " (U/l)", fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 20f, 80f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion
                    break;

                case Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID:

                    #region HEMOGLOBINA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1 + " (%)", fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 15f,85f  };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion


                    break;

                case Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID:

                    #region GRUPO_Y_FACTOR_SANGUINEO

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var grupo = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                        var factor = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(grupo.v_Value1Name) ? "NO SE HAN REGISTRADO DATOS." : "GRUPO SANGUÍNEO: " + grupo.v_Value1Name, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(grupo.v_Value1Name) ? "NO SE HAN REGISTRADO DATOS." : "FACTOR RH: " + factor.v_Value1Name, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 50f, 50f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

             
                case Sigesoft.Common.Constants.ODONTOGRAMA_ID:

                    #region ODONTOGRAMA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var dxAudio = serviceComponent.DiagnosticRepository;//          .Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);

                        var join = string.Join(", ", dxAudio.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "NO SE HAN REGISTRADO DATOS." : join, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.PSICOLOGIA_ID:

                    #region PSICOLOGIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var areaCognitiva = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID);
                        //var areaEmocional = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_EMOCIONAL_ID);
                        //var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_CONCLUSIONES_ID);
                        cells.Add(new PdfPCell(new Phrase(areaCognitiva.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.RX_TORAX_ID:

                    #region RX

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        //var conclusionRxDes = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID);
                        ////var conclusionOitDes = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID);
                        //var exPolvoDes = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_EXPOSICION_POLVO_DESCRIPCION_ID);

                        //string valueA = string.Empty;
                        ////string valueB = string.Empty;
                        //string valueC = string.Empty;
                        //string finalValue = string.Empty;

                        //if (conclusionRxDes != null)
                        //    valueA = conclusionRxDes.v_Value1;                      

                        ////if (conclusionOitDes != null)
                        ////    valueB = conclusionOitDes.v_Value1;

                        //if (exPolvoDes != null)
                        //    valueC = exPolvoDes.v_Value1;

                        //if (string.IsNullOrEmpty(valueA) && string.IsNullOrEmpty(valueC)) // && string.IsNullOrEmpty(valueC))
                        //    finalValue = "NO SE HAN REGISTRADO DATOS.";
                        //else
                        //    finalValue = string.Format("{0}, {1}", valueA, valueC); //, valueC);

                        //cells.Add(new PdfPCell(new Phrase(finalValue, fontColumnValue)));

                        var dxRx = serviceComponent.DiagnosticRepository;//          .Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);

                        var join = string.Join(", ", dxRx.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "NO SE HAN REGISTRADO DATOS." : join, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                case Sigesoft.Common.Constants.OIT_ID:

                    #region OIT

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {

                        //var ConclusionesOITDescripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID);
                        //var ConclusionesOIT = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_ID);

                        //var xConcluOIT = string.Empty;

                        //if (!string.IsNullOrEmpty(ConclusionesOIT.v_Value1Name))
                        //{
                        //    xConcluOIT = ConclusionesOIT.v_Value1Name;
                        //}

                        //if (!string.IsNullOrEmpty(ConclusionesOITDescripcion.v_Value1))
                        //{
                        //    xConcluOIT += ", " + ConclusionesOITDescripcion.v_Value1;
                        //}

                        //cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(xConcluOIT) ? "NO SE HAN REGISTRADO DATOS." : xConcluOIT, fontColumnValue)));

                        var dxOit = serviceComponent.DiagnosticRepository;//          .Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ODONTOGRAMA_CONCLUSIONES_DESCRIPCION_ID);

                        var join = string.Join(", ", dxOit.Select(p => p.v_DiseasesName));

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(join) ? "NO SE HAN REGISTRADO DATOS." : join, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.MAMOGRAFIA_ID:

                    #region MAMOGRAFIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var mamaDerecha = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MAMOGRAFIA_DERECHA_ID);
                        var mamaIzquierda = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MAMOGRAFIA_IZQUIERDA_ID);

                        string valueA = string.Empty;
                        string valueB = string.Empty;
                        string finalValue = string.Empty;

                        if (mamaDerecha != null)
                            valueA = mamaDerecha.v_Value1;

                        if (mamaIzquierda != null)
                            valueB = mamaIzquierda.v_Value1;

                        if (string.IsNullOrEmpty(valueA) && string.IsNullOrEmpty(valueB))
                            finalValue = "NO SE HAN REGISTRADO DATOS.";
                        else
                            finalValue = string.Format("{0}, {1}", valueA, valueB);

                        cells.Add(new PdfPCell(new Phrase(finalValue, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.EVAL_NEUROLOGICA_ID:

                    #region EVAL_NEUROLOGICA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EVAL_NEUROLOGICA_DESCRIPCION_ID);

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.TEST_ROMBERG_ID:

                    #region TEST_ROMBERG

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var hallazgos = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ROMBERG_HALLAZGOS_ID);
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ROMBERG_DESCRIPCION_ID);

                        string valueA = string.Empty;
                        string valueB = string.Empty;
                        string finalValue = string.Empty;

                        if (hallazgos != null)
                            valueA = hallazgos.v_Value1;

                        if (descripcion != null)
                            valueB = descripcion.v_Value1;

                        if (string.IsNullOrEmpty(valueA) && string.IsNullOrEmpty(valueB))
                            finalValue = "No se han registrado datos.";
                        else
                            finalValue = string.Format("{0}, {1}", valueA, valueB);

                        cells.Add(new PdfPCell(new Phrase(finalValue, fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 100f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;



                case "N009-ME000000045":

                    #region HEMOGLOBINA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("HEMOGLOBINA" + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001282");

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1 + " gr. %", fontColumnValue)));
                                             
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }
                                       
                    columnWidths = new float[] { 15f, 85f, };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);



              

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("HEMATOCRITO" + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {

                        var descripcion1 = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001280");

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion1.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion1.v_Value1 + " %", fontColumnValue)));
                                             
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }
                                       
                    columnWidths = new float[] { 15f, 85f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);








                    #endregion
                    
                    break;      


                case "N009-ME000000046":

                    #region LEUCOCITOS

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase("LEUCOCITOS EN ORINA" + ": ", fontSubTitle))
                    {
                        //Colspan = 2,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var descripcion = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == "N009-MF000001061");

                        cells.Add(new PdfPCell(new Phrase(string.IsNullOrEmpty(descripcion.v_Value1) ? "NO SE HAN REGISTRADO DATOS." : descripcion.v_Value1 + " X Campo", fontColumnValue)));

                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                    }

                    columnWidths = new float[] { 25f, 75f };
                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;

                default:
                    break;
            }

            return table;

        }

        #endregion

        #region Anexo7C

        public static void CreateAnexo7C(ServiceList DataService,
                                         PacientList filiationData,
                                        List<ServiceComponentList> serviceComponent, 
                                        List<PersonMedicalHistoryList> listaPersonMedicalHistory, 
                                        List<FamilyMedicalAntecedentsList> listaPatologicosFamiliares, 
                                        List<NoxiousHabitsList> listaHabitoNocivos, 
                                        byte[] CuadroVacio, 
                                        byte[] CuadroCheck,
                                        byte[] Pulmones,
                                        string PiezasCaries, 
                                        string PiezasAusentes, 
                                        List<ServiceComponentFieldValuesList> Audiometria,
                                        List<DiagnosticRepositoryList> diagnosticRepository,
                                        organizationDto infoEmpresaPropietaria,
                                        string filePDF)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
            writer.PageEvent = page;

            // step 3: we open the document
            document.Open();
            // step 4: we Add content to the document
            // we define some fonts

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            #endregion

            //#region Logo
            //PdfPCell CellLogo = null;
            //if (infoEmpresaPropietaria.b_Image != null)
            //{
            //    CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F));
            //}
            //else
            //{
            //    CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue));
            //}
            ////Image logo = HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 20F);
            //PdfPTable headerTbl = new PdfPTable(1);
            //headerTbl.TotalWidth = writer.PageSize.Width;
            //PdfPCell cellLogo = new PdfPCell(CellLogo);

            //cellLogo.VerticalAlignment = Element.ALIGN_TOP;
            //cellLogo.HorizontalAlignment = Element.ALIGN_CENTER;

            //cellLogo.Border = PdfPCell.NO_BORDER;
            //headerTbl.AddCell(cellLogo);
            //document.Add(headerTbl);

            //#endregion
            
            //#region Title

            //Paragraph cTitle = new Paragraph("ANEXO 16", fontTitle1);
            //cTitle.Alignment = Element.ALIGN_CENTER;
            
            //document.Add(cTitle);

            //#endregion
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
            #region Title

            PdfPCell CellLogo = null;
            cells = new List<PdfPCell>();
            PdfPCell cellPhoto1 = null;

            if (filiationData.b_Photo != null)
                cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            else
                cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

            var cellsTit = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("ANEXO 16", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            cells.Add(cellPhoto1);

            columnWidths = new float[] { 20f, 60f, 20f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

     

            document.Add(new Paragraph("\r\n"));

            #region Cabecera Reporte

            PdfPCell cellConCheck = null;          
            cellConCheck = new PdfPCell(HandlingItextSharp.GetImage(CuadroCheck));

            PdfPCell cellSinCheck = null;
            cellSinCheck = new PdfPCell(HandlingItextSharp.GetImage(CuadroVacio));
                   

            PdfPCell PreOcupacional = cellSinCheck, Periodica = cellSinCheck, Retiro = cellSinCheck, Otros = cellSinCheck;
            string Empresa = "", Contratista="";
            if (DataService !=null)
            {
                if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PreOcupacional)
                {
                    PreOcupacional = cellConCheck;
                }
                else if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.PeriodicoAnual)
                {
                    Periodica = cellConCheck;
                }
                else if (DataService.i_EsoTypeId == (int)Sigesoft.Common.TypeESO.Retiro)
                {
                    Retiro = cellConCheck;
                }
                else
                {
                    Otros = cellConCheck;
                }

                if (DataService.EmpresaTrabajo == DataService.EmpresaEmpleadora)
                {
                    Empresa = "";
                    Contratista = DataService.EmpresaEmpleadora;
                }
                else
                {
                    Empresa = DataService.EmpresaTrabajo;
                    Contratista = DataService.EmpresaEmpleadora;
                }
            }
          


            cells = new List<PdfPCell>()
                {
                    //fila
                    new PdfPCell(new Phrase("EMPRESA:", fontColumnValue)){Border = PdfPCell.LEFT_BORDER},                                   
                    new PdfPCell(new Phrase( DataService.v_CustomerOrganizationName,fontColumnValue)){Colspan=3, Border = PdfPCell.NO_BORDER},        
                    
                    new PdfPCell(new Phrase("EXAMEN MÉDICO", fontColumnValue)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment=PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER},  

                    //fila
                    new PdfPCell(new Phrase("CONTRATISTA:", fontColumnValue)){Border = PdfPCell.LEFT_BORDER},                                   
                    new PdfPCell(new Phrase(DataService.EmpresaEmpleadora, fontColumnValue)){Colspan=3,Border = PdfPCell.NO_BORDER},  
                    new PdfPCell(new Phrase("PRE-OCUPACIONAL", fontColumnValue)){Border = PdfPCell.NO_BORDER},  
                    new PdfPCell(PreOcupacional){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    
                     //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {Border = PdfPCell.LEFT_BORDER, Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(new Phrase("ANUAL", fontColumnValue)){Border = PdfPCell.NO_BORDER},   
                    new PdfPCell(Periodica){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_CENTER },

                     //fila
                    new PdfPCell(new Phrase("", fontColumnValue)) {Border = PdfPCell.LEFT_BORDER, Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(new Phrase("RETIRO", fontColumnValue)){Border = PdfPCell.NO_BORDER}, 
                    new PdfPCell(Retiro){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_CENTER },

                     //fila
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.LEFT_BORDER, Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},    
                    new PdfPCell(new Phrase("REUBICACIÓN", fontColumnValue)){Border = PdfPCell.NO_BORDER},  
                    new PdfPCell(Otros){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_CENTER },


                     //fila
                    new PdfPCell(new Phrase("APELLIDOS Y NOMBRES", fontColumnValueBold)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT  },                
                    new PdfPCell(new Phrase(DataService==null ? "" :DataService.v_Pacient, fontColumnValue)) ,                                        
                    new PdfPCell(new Phrase("N° DE FICHA", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},   
                    new PdfPCell(new Phrase(DataService==null ? "" :DataService.v_ServiceId, fontColumnValue))
                     { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                      //fila
                    new PdfPCell(new Phrase("FECHA DEL EXAMEN", fontColumnValueBold)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},              
                    new PdfPCell(new Phrase(DataService==null ? "" :DataService.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)) ,                                          
                    new PdfPCell(new Phrase("MINERALES EXPLOTADOS O PROCESADOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                           
                    new PdfPCell(new Phrase(DataService==null ? "" :DataService.v_ExploitedMineral, fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                };
            columnWidths = new float[] { 15f, 5f, 30f, 30f, 23, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Datos Persona 1

            //Foto del Trabajador
            PdfPCell cellFirmaTrabajador = null;
            PdfPCell cellFirmaDoctor = null;
            PdfPCell cellHuellaTrabajador = null;
            if (DataService !=null)
            {
                if (DataService.FirmaTrabajador != null)
                    cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(DataService.FirmaTrabajador, null, null, 120, 45));
                else
                    cellFirmaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

                cellFirmaTrabajador.Colspan = 2;
                cellFirmaTrabajador.Rowspan = 8;
                cellFirmaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellFirmaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
               

                //Foto del Doctor

                if (DataService.FirmaMedicoMedicina != null)
                    cellFirmaDoctor = new PdfPCell(HandlingItextSharp.GetImage(DataService.FirmaMedicoMedicina, null, null, 120, 45));
                else
                    cellFirmaDoctor = new PdfPCell(new Phrase(" ", fontColumnValue));
                cellFirmaDoctor.Colspan = 6;
                cellFirmaDoctor.Rowspan = 3;
                cellFirmaDoctor.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellFirmaDoctor.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;


                //Huella 
                
                if (DataService.HuellaTrabajador != null)
                    cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(DataService.HuellaTrabajador, null, null, 20, 40));
                else
                    cellHuellaTrabajador = new PdfPCell(new Phrase(" ", fontColumnValue));

                cellHuellaTrabajador.Colspan = 2;
                cellHuellaTrabajador.Rowspan = 4;
                cellHuellaTrabajador.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellHuellaTrabajador.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            }
           

            //Pulmones 
            //PdfPCell cellPulmones = null;

            PdfPCell cellPulmones = null;
            cellPulmones = new PdfPCell(HandlingItextSharp.GetImage(Pulmones,15f));
           
            cellPulmones.Colspan = 2;
            cellPulmones.Rowspan = 4;
            cellPulmones.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cellPulmones.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;


            PdfPCell Superficie = cellSinCheck, Concentradora = cellSinCheck, SubSuelo = cellSinCheck;
            PdfPCell Debajo2500 = cellSinCheck, Entre3001a3500 = cellSinCheck,
                  Entre3501a4000 = cellSinCheck, Entre2501a3000 = cellSinCheck,
                  Entre4001a4500 = cellSinCheck, Mas4501 = cellSinCheck;

            if (DataService !=null)
            {
                if (DataService.i_PlaceWorkId == (int)Sigesoft.Common.LugarTrabajo.Superfice)
                {
                    Superficie = cellConCheck;
                }
                else if (DataService.i_PlaceWorkId == (int)Sigesoft.Common.LugarTrabajo.Concentradora)
                {
                    Concentradora = cellConCheck;
                }
                else if (DataService.i_PlaceWorkId == (int)Sigesoft.Common.LugarTrabajo.Subsuelo)
                {
                    SubSuelo = cellConCheck;
                }


                if (DataService.i_AltitudeWorkId == (int)Sigesoft.Common.Altitud.Debajo2500)
                {
                    Debajo2500 = cellConCheck;
                }
                else if (DataService.i_AltitudeWorkId == (int)Sigesoft.Common.Altitud.Entre2501a3000)
                {
                    Entre2501a3000 = cellConCheck;
                }
                else if (DataService.i_AltitudeWorkId == (int)Sigesoft.Common.Altitud.Entre3001a3500)
                {
                    Entre3001a3500 = cellConCheck;
                }
                else if (DataService.i_AltitudeWorkId == (int)Sigesoft.Common.Altitud.Entre3501a4000)
                {
                    Entre3501a4000 = cellConCheck;
                }
                else if (DataService.i_AltitudeWorkId == (int)Sigesoft.Common.Altitud.Entre4001a4500)
                {
                    Entre4001a4500 = cellConCheck;
                }
                else if (DataService.i_AltitudeWorkId == (int)Sigesoft.Common.Altitud.Mas4501)
                {
                    Mas4501 = cellConCheck;
                }
            }


            cells = new List<PdfPCell>()
                   {

                    //fila
                    new PdfPCell(new Phrase("LUGAR Y FECHA NACIMIENTO", fontColumnValue))
                                        { HorizontalAlignment = PdfPCell.ALIGN_CENTER},              
                    new PdfPCell(new Phrase("DOMICILIO ACTUAL", fontColumnValue)),  
                    new PdfPCell(new Phrase("LUGAR DE LABOR", fontColumnValue))
                                            { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("ALTITUD DE LABOR", fontColumnValue))
                                    { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //fila
                    new PdfPCell(new Phrase(DataService.v_BirthPlace + " - " + DataService.d_BirthDate.Value.ToShortDateString(), fontColumnValue))
                                     { Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER},              
                    new PdfPCell(new Phrase(DataService.v_AdressLocation, fontColumnValue))
                                     { Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase("SUPERFICIE", fontColumnValue)),
                    new PdfPCell(Superficie){Border = PdfPCell.NO_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("DEBAJO DE 2500 m", fontColumnValue)),
                    new PdfPCell(Debajo2500){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("3501 a 4000 m", fontColumnValue)),
                    new PdfPCell(Entre3501a4000){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
            
                    
                    //fila                 
                    new PdfPCell(new Phrase("CONCENTRADORA", fontColumnValue)),
                     new PdfPCell(Concentradora){Border = PdfPCell.NO_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("2501 a 3000 m", fontColumnValue)),
                    new PdfPCell(Entre2501a3000){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("4001 a 4500 m", fontColumnValue)),
                    new PdfPCell(Entre4001a4500){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },

                    
                    //fila                   
                    new PdfPCell(new Phrase("SUBSUELO", fontColumnValue)),
                    new PdfPCell(SubSuelo){Border = PdfPCell.NO_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("3001 a 3500 m", fontColumnValue)),
                    new PdfPCell(Entre3001a3500){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("más de 4501 m", fontColumnValue)),
                    new PdfPCell(Mas4501){Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment=PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                                  
                   };

            columnWidths = new float[] { 10f, 10f, 10f, 5f, 10f, 5f, 10f, 5f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Datos Persona 2

            PdfPCell Masculino = cellSinCheck, Femenino = cellSinCheck,
                    Soltero = cellSinCheck, Conviviente = cellSinCheck, Viudo = cellSinCheck, Casado = cellSinCheck, Divorciado = cellSinCheck,
                    Analfabeto = cellSinCheck, PrimariaCompleta = cellSinCheck, SecundariaCompleta = cellSinCheck, Tecnico = cellSinCheck, PrimariaImcompleta = cellSinCheck, SecundariaIncompleta = cellSinCheck, Universitario = cellSinCheck;
            
            //Genero
            if (DataService.i_SexTypeId == (int)Sigesoft.Common.Gender.MASCULINO)
            {
                Masculino = cellConCheck;
            }
            else if (DataService.i_SexTypeId == (int)Sigesoft.Common.Gender.FEMENINO)
            {
                Femenino = cellConCheck;
            }


            //Estado Civil
            if (DataService.i_MaritalStatusId == (int)Sigesoft.Common.EstadoCivil.Soltero)
            {
                Soltero = cellConCheck;
            }
            else if (DataService.i_MaritalStatusId == (int)Sigesoft.Common.EstadoCivil.Casado)
            {
                Casado = cellConCheck;
            }
            else if (DataService.i_MaritalStatusId == (int)Sigesoft.Common.EstadoCivil.Viudo)
            {
                Viudo = cellConCheck;
            }
            else if (DataService.i_MaritalStatusId == (int)Sigesoft.Common.EstadoCivil.Divorciado)
            {
                Divorciado = cellConCheck;
            }
            else if (DataService.i_MaritalStatusId == (int)Sigesoft.Common.EstadoCivil.Conviviente)
            {
                Conviviente = cellConCheck;
            }


            //Nivel Educación
            if (DataService.i_LevelOfId == (int)Sigesoft.Common.NivelEducacion.Analfabeto)
            {
                Analfabeto = cellConCheck;
            }
            else if (DataService.i_LevelOfId == (int)Sigesoft.Common.NivelEducacion.PIncompleta)
            {
                PrimariaImcompleta = cellConCheck;
            }
            else if (DataService.i_LevelOfId == (int)Sigesoft.Common.NivelEducacion.PCompleta)
            {
                PrimariaCompleta = cellConCheck;
            }
            else if (DataService.i_LevelOfId == (int)Sigesoft.Common.NivelEducacion.SIncompleta)
            {
                SecundariaIncompleta = cellConCheck;
            }
            else if (DataService.i_LevelOfId == (int)Sigesoft.Common.NivelEducacion.SCompleta)
            {
                SecundariaCompleta = cellConCheck;
            }
            else if (DataService.i_LevelOfId == (int)Sigesoft.Common.NivelEducacion.Tecnico)
            {
                Tecnico = cellConCheck;
            }
            else if (DataService.i_LevelOfId == (int)Sigesoft.Common.NivelEducacion.Universitario)
            {
                Universitario = cellConCheck;
            }

            cells = new List<PdfPCell>()
                  {
                    //fila 1
                    new PdfPCell(new Phrase("EDAD", fontColumnValue)){ Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},              
                    new PdfPCell(new Phrase("GÉNERO", fontColumnValue)){ Border = PdfPCell.LEFT_BORDER, Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
                    new PdfPCell(new Phrase("DNI", fontColumnValue)){ Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},                                            
                    new PdfPCell(new Phrase("ESTADO CIVIL", fontColumnValue)){  Border = PdfPCell.LEFT_BORDER,Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("GRADO DE INSTRUCCIÓN", fontColumnValue)){Border = PdfPCell.LEFT_BORDER, Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("", fontColumnValue)){ Border = PdfPCell.RIGHT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    
                    //fila 2
                    new PdfPCell(new Phrase(DataService.i_Edad.ToString() + " Años", fontColumnValue)){ Border = PdfPCell.LEFT_BORDER,  Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER},              
                    new PdfPCell(new Phrase("M", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(Masculino){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase(DataService.v_DocNumber, fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},                                            
                    new PdfPCell(new Phrase("SOLTERO", fontTitleTable)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(Soltero){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("CONVIVIENTE", fontTitleTable)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(Conviviente){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },        
                    new PdfPCell(new Phrase("ANALFABETO", fontTitleTable)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(Analfabeto){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("", fontColumnValue)){ Border = PdfPCell.NO_BORDER , Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("", fontColumnValue)){ Border = PdfPCell.RIGHT_BORDER , HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    
                    //fila   3    
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan=2, Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, //mf
                    new PdfPCell(new Phrase("TELÉFONO", fontColumnValue)){Border = PdfPCell.LEFT_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("VIUDO", fontTitleTable)){ Colspan=3,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(Viudo){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("PRIM. COMP", fontTitleTable)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(PrimariaCompleta){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("SEC. COMP", fontTitleTable)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(SecundariaCompleta){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("TÉCNICO", fontTitleTable)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(Tecnico){ Border = PdfPCell.RIGHT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },

                    //fila  4                            
                    new PdfPCell(new Phrase("F", fontColumnValue)){Rowspan=2, Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
                    new PdfPCell(Femenino){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase(DataService.Telefono, fontColumnValue)){Rowspan=2, Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
                    new PdfPCell(new Phrase("CASADO", fontTitleTable)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(Casado){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("DIVORCIADO", fontTitleTable)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},  
                    new PdfPCell(Divorciado){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("PRIM. INCOMP", fontTitleTable)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(PrimariaImcompleta){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("SEC INCOMP", fontTitleTable)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(SecundariaIncompleta){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(new Phrase("UNIVERSITARIO", fontTitleTable)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(Universitario){Border = PdfPCell.RIGHT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },

                  };

            columnWidths = new float[] { 5f, 2.5f, 2.5f, 10f, 7f, 2.5f, 7f, 2.5f, 7f, 3f, 7f, 3f, 7f, 5f }; //14

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region EPPS

            PdfPCell Ruido = cellSinCheck, Cancerigenos = cellSinCheck, Temperaturas = cellSinCheck, Cargas = cellSinCheck,
                    Polvo = cellSinCheck, Mutagenicos = cellSinCheck, Biologicos = cellSinCheck, MovRepet = cellSinCheck,
                    VidSegmentaria = cellSinCheck, Solventes = cellSinCheck, Posturas = cellSinCheck, PVD = cellSinCheck,
                    VidTotal = cellSinCheck, MetalesPesados = cellSinCheck, Turnos = cellSinCheck, OtrosEPPS = cellSinCheck;

         
            string Describir = "";

            string ValorCabeza = "", ValorCuello = "", ValorBoca="", ValorReflejosPupilares="" , ValorNariz="";

        

            #region Examen Fisco (7C)
            ServiceComponentList find7C = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_ID);
            PdfPCell ValorPulmonesNormal = cellSinCheck, ValorPulmonesAnormal = cellSinCheck;           
            PdfPCell ValorTactoRectalNormal = cellSinCheck, ValorTactoRectalAnormal = cellSinCheck, ValorTactoRectalSinRealizar = cellSinCheck;
            string ValorPulmonDescripcion = "", ValorTactoRectalDescripcion = "";
            
            string ValorMiembrosInferiores = "", ValorMiembrosSuperiores="", ValorReflejosOsteoTendinosos="", ValorMarcha="", ValorColumna="", ValorAbdomen="",
                    ValorAnilloInguinales="", ValorHernias="", ValorVarice="", ValorGenitales="", ValorGangleos="";


            if (find7C != null)
            {

                var TactoRectalNormal = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_NORMAL);
                if (TactoRectalNormal != null)
                {
                    if (TactoRectalNormal.v_Value1 == "1")
                    {
                        ValorTactoRectalNormal = cellConCheck;
                    }
                }

                var TactoRectalAnormal = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_ANORMAL);
                if (TactoRectalAnormal != null)
                {
                    if (TactoRectalAnormal.v_Value1 == "1")
                    {
                        ValorTactoRectalAnormal = cellConCheck;
                    }
                }


                var TactoRectalSinRealizar = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_NO_RELIZADO);
                if (TactoRectalSinRealizar != null)
                {
                    if (TactoRectalSinRealizar.v_Value1 == "1")
                    {
                        ValorTactoRectalSinRealizar = cellConCheck;
                    }
                }


                var TactoRectalDescripcion = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_DESCRIPCION);
                if (TactoRectalDescripcion != null)
                {
                    ValorTactoRectalDescripcion = TactoRectalDescripcion.v_Value1;
                }



                var PulmonesNormal = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_NORMAL);
                if (PulmonesNormal != null)
                {
                    if (PulmonesNormal.v_Value1 == "1")
                    {
                        ValorPulmonesNormal = cellConCheck;
                    }
                }

                var PulmonesAnormal = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_ANORMAL);
                if (PulmonesAnormal != null)
                {
                    if (PulmonesAnormal.v_Value1 == "1")
                    {
                        ValorPulmonesAnormal = cellConCheck;
                    }
                }

                var PulmonDescripcion = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_DESCRIPCION);
                if (PulmonDescripcion != null)
                {
                    ValorPulmonDescripcion = PulmonDescripcion.v_Value1;
                }

                var Gangleos = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION);
                if (Gangleos != null)
                {
                    ValorGangleos = Gangleos.v_Value1;
                }

                var Genitales = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION);
                if (Genitales != null)
                {
                    ValorGenitales = Genitales.v_Value1;
                }

                var Varice = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION);
                if (Varice != null)
                {
                    ValorVarice = Varice.v_Value1;
                }

                var Hernias = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION);
                if (Hernias != null)
                {
                    ValorHernias = Hernias.v_Value1;
                }

                var AnilloInguinales = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION);
                if (AnilloInguinales != null)
                {
                    ValorAnilloInguinales = AnilloInguinales.v_Value1;
                }
                
                var MiembrosInferiores = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION);
                if (MiembrosInferiores != null)
                {
                    ValorMiembrosInferiores = MiembrosInferiores.v_Value1;
                }

                var MiembrosSuperiores = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION);
                if (MiembrosSuperiores != null)
                {
                    ValorMiembrosSuperiores = MiembrosSuperiores.v_Value1;
                }

                var ReflejosOsteoTendinosos = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION);
                if (ReflejosOsteoTendinosos != null)
                {
                    ValorReflejosOsteoTendinosos = ReflejosOsteoTendinosos.v_Value1;
                }

                var Marcha = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION);
                if (Marcha != null)
                {
                    ValorMarcha = Marcha.v_Value1;
                }

                var Columna = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION);
                if (Columna != null)
                {
                    ValorColumna = Columna.v_Value1;
                }

                var Abdomen = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION);
                if (Abdomen != null)
                {
                    ValorAbdomen = Abdomen.v_Value1;
                }



                var ValorRuido = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_RUIDO_ID);
                if (ValorRuido != null)
                {
                    if (ValorRuido.v_Value1 == "1")
                    {
                        Ruido = cellConCheck;
                    }
                }

                var ValorCancerigeno = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_CANCERIGENOS_ID);
                if (ValorCancerigeno != null)
                {
                    if (ValorCancerigeno.v_Value1 == "1")
                    {
                        Cancerigenos = cellConCheck;
                    }
                }

                var ValorTemperaturas = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_TEMPERATURA_ID);
                if (ValorTemperaturas != null)
                {
                    if (ValorTemperaturas.v_Value1 == "1")
                    {
                        Temperaturas = cellConCheck;
                    }
                }

                var ValorCargas = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_CARGAS_ID);
                if (ValorCargas != null)
                {
                    if (ValorCargas.v_Value1 == "1")
                    {
                        Cargas = cellConCheck;
                    }
                }

                var ValorPolvo = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_POLVO_ID);
                if (ValorPolvo != null)
                {
                    if (ValorPolvo.v_Value1 == "1")
                    {
                        Polvo = cellConCheck;
                    }
                }


                var ValorMutagenicos = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_MUTAGENICOS_ID);
                if (ValorMutagenicos != null)
                {
                    if (ValorMutagenicos.v_Value1 == "1")
                    {
                        Mutagenicos = cellConCheck;
                    }
                }


                var ValorBiologicos = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_BIOLOGICOS_ID);
                if (ValorBiologicos != null)
                {
                    if (ValorBiologicos.v_Value1 == "1")
                    {
                        Biologicos = cellConCheck;
                    }
                }

                var ValorMovRepet = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_MOV_REPETITIVOS_ID);
                if (ValorMovRepet != null)
                {
                    if (ValorMovRepet.v_Value1 == "1")
                    {
                        MovRepet = cellConCheck;
                    }
                }

                var ValorVidSegmentaria = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_VIG_SEGMENTARIA_ID);
                if (ValorVidSegmentaria != null)
                {
                    if (ValorVidSegmentaria.v_Value1 == "1")
                    {
                        VidSegmentaria = cellConCheck;
                    }
                }

                var ValorSolventes = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_SOLVENTES_ID);
                if (ValorSolventes != null)
                {
                    if (ValorSolventes.v_Value1 == "1")
                    {
                        Solventes = cellConCheck;
                    }
                }

                var ValorPosturas = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_POSTURAS_ID);
                if (ValorPosturas != null)
                {
                    if (ValorPosturas.v_Value1 == "1")
                    {
                        Posturas = cellConCheck;
                    }
                }

                var ValorPVD = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_PVD_ID);
                if (ValorPVD != null)
                {
                    if (ValorPVD.v_Value1 == "1")
                    {
                        PVD = cellConCheck;
                    }
                }

                var ValorVidTotal = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_Vid_Total_ID);
                if (ValorVidTotal != null)
                {
                    if (ValorVidTotal.v_Value1 == "1")
                    {
                        VidTotal = cellConCheck;
                    }
                }


                var ValorMetalesPesados = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_METAL_PESADO_ID);
                if (ValorMetalesPesados != null)
                {
                    if (ValorMetalesPesados.v_Value1 == "1")
                    {
                        MetalesPesados = cellConCheck;
                    }
                }

                var ValorTurnos = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_TURNOS_ID);
                if (ValorTurnos != null)
                {
                    if (ValorTurnos.v_Value1 == "1")
                    {
                        Turnos = cellConCheck;
                    }
                }

                var ValorOtros = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_OTROS_ID);
                if (ValorOtros != null)
                {
                    if (ValorOtros.v_Value1 == "1")
                    {
                        Otros = cellConCheck;
                    }
                }

                var ValorDescribir = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_DESCRIBIR_ID);
                if (ValorDescribir != null)
                {
                    Describir = ValorDescribir.v_Value1;
                }

                var Cabeza = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION);
                if (Cabeza != null)
                {
                    ValorCabeza = Cabeza.v_Value1;
                }

                var Cuello = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION);
                if (Cuello != null)
                {
                    ValorCuello = Cuello.v_Value1;
                }

                var Nariz = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION);
                if (Nariz != null)
                {
                    ValorNariz = Nariz.v_Value1;
                }

                var Boca = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION);
                if (Boca != null)
                {
                    ValorBoca = Boca.v_Value1;
                }

                var Reflejos = find7C.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION);
                if (Reflejos != null)
                {
                    ValorReflejosPupilares = Reflejos.v_Value1;
                }  
            #endregion
           
                           
            }
          
            cells = new List<PdfPCell>()
                  {
                    //filaMobogenie3.0,Released Now!
                    new PdfPCell(new Phrase("RUIDO", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(Ruido){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("CANCERÍGENOS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},    
                    new PdfPCell(Cancerigenos){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("TEMPERATURA", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},    
                    new PdfPCell(Temperaturas){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("CARGAS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},    
                    new PdfPCell(Cargas){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },    
                    new PdfPCell(new Phrase(Describir, fontColumnValue)){ Rowspan = 4, Colspan = 2, Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},     
                    //new PdfPCell(new Phrase("", fontColumnValue)){ Rowspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase("PUESTO ACTUAL / AL QUE POSTULA", fontColumnValue)), 

                    //fila
                    new PdfPCell(new Phrase("POLVO", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                       
                    new PdfPCell(Polvo){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("MUTAGÉNICOS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(Mutagenicos){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("BIOLÓGICOS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(Biologicos){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("MOV. REPET.", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(MovRepet){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    //new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(new Phrase(DataService.v_CurrentOccupation, fontColumnValue)){ Rowspan=3, HorizontalAlignment = PdfPCell.ALIGN_CENTER},   

                    //fila
                    new PdfPCell(new Phrase("VIB SEGMENTARIA", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                       
                    new PdfPCell(VidSegmentaria){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },   
                    new PdfPCell(new Phrase("SOLVENTES", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(Solventes){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("POSTURAS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(Posturas){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("PVD", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(PVD){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    //new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  

                    //fila
                    new PdfPCell(new Phrase("VIB TOTAL", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                      
                    new PdfPCell(VidTotal){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("METALES PESADOS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(MetalesPesados){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("TURNOS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(Turnos){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    new PdfPCell(new Phrase("OTROS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(Otros) { Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM }, 
                    //new PdfPCell(new Phrase("", fontColumnValue)) { Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                  };
            columnWidths = new float[] { 15f, 3f, 15f, 3f, 12f, 3f, 10f, 3f, 7f, 7f, 25f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Antecedentes Ocupacionales
            cells = new List<PdfPCell>()
                 {
                      new PdfPCell(new Phrase("(VER ADJUNTO HISTORIA OCUPACIONAL)", fontColumnValue)),
                 };
            columnWidths = new float[] { 100f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "ANTECEDENTES OCUPACIONALES", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Antecedentes Personales
            cells = new List<PdfPCell>();

            if (listaPersonMedicalHistory != null && listaPersonMedicalHistory.Count > 0)
            {
                foreach (var item in listaPersonMedicalHistory)
                {
                    //Columna Diagnóstico
                    cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna Fecha Inicio
                    cell = new PdfPCell(new Phrase(item.d_StartDate.Value.ToShortDateString(), fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna Tipo Dx
                    cell = new PdfPCell(new Phrase(item.v_TypeDiagnosticName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                }
                columnWidths = new float[] { 50f, 20f, 30f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO REFIERE ANTECEDENTES.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 50f, 20f, 30f };

            }
            columnHeaders = new string[] { "DIAGNÓSTICO", "FECHA DE INICIO", "TIPO DIAGNÓSTICO" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "III. ANTECEDENTES PERSONALES", fontTitleTable, columnHeaders);

            document.Add(table);

            #endregion

            #region Antecedentes Familiares
            cells = new List<PdfPCell>();

            if (listaPatologicosFamiliares != null && listaPatologicosFamiliares.Count > 0)
            {
                foreach (var item in listaPatologicosFamiliares)
                {
                    //Columna Diagnóstico
                    cell = new PdfPCell(new Phrase(item.v_DiseaseName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna Fecha Inicio
                    cell = new PdfPCell(new Phrase(item.v_TypeFamilyName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);

                    //Columna Tipo Dx
                    cell = new PdfPCell(new Phrase(item.v_Comment, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                }
                columnWidths = new float[] { 50f, 20f, 30f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NO REFIERE ANTECEDENTES.", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] { 50f, 20f, 30f };

            }
            columnHeaders = new string[] { "DIAGNÓSTICO", "GRUPO FAMILIAR", "COMENTARIO" };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "IV. ANTECEDENTES FAMILIARES", fontTitleTable, columnHeaders);

            document.Add(table);

            #endregion

            #region NÚMERO DE HIJOS

            cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("VIVOS", fontColumnValue)),
                    new PdfPCell(new Phrase(DataService.HijosVivos.ToString(), fontColumnValue)),
                    new PdfPCell(new Phrase("FALLECIDOS", fontColumnValue)),
                    new PdfPCell(new Phrase(DataService.HijosDependientes.ToString(), fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                 };
            columnWidths = new float[] { 15f, 5f, 15f, 5f, 60f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "NÚMERO DE HIJOS", fontTitleTable);

            document.Add(filiationWorker);


            #endregion

            #region HÁBITOS

            #region Antropometria
            ServiceComponentList findAntropometria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID);
            string ValorTalla = "", ValorPeso = "", ValorIMC = "", ValorCintura = "", ValorCadera = "", ValorICC = "";
            if (findAntropometria != null)
            {
                var Talla = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID);
                if (Talla != null)
                {
                    if (Talla.v_Value1 != null) ValorTalla = Talla.v_Value1;
                }


                var Peso = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID);
                if (Peso != null)
                {
                    if (Peso.v_Value1 != null) ValorPeso = Peso.v_Value1;
                }


                var IMC = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);
                if (IMC != null)
                {
                    if (IMC.v_Value1 != null) ValorIMC = IMC.v_Value1;
                }

                var Cintura = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID);
                if (Cintura != null)
                {
                    if (Cintura.v_Value1 != null) ValorCintura = Cintura.v_Value1;
                }

                var Cadera = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID);
                if (Cadera != null)
                {
                    if (Cadera.v_Value1 != null) ValorCadera = Cadera.v_Value1;
                }

                var ICC = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_INDICE_CINTURA_ID);
                if (ICC != null)
                {
                    if (ICC.v_Value1 != null) ValorICC = ICC.v_Value1;
                }

                //var Temperatura = findAntropometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_TEMPERATURA_ID);
                //if (Talla != null)
                //{
                //    if (Talla.v_Value1 != null) ValorTalla = Talla.v_Value1;
                //}
            }
            #endregion

            #region Funciones Vitales
            
              ServiceComponentList findFuncionesVitales = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FUNCIONES_VITALES_ID);
              string ValorTemperatura = "", ValorFRespiratoria="", ValorFCardiaca="", ValorSatO2="", ValorPAS="", ValorPAD="";
              if (findFuncionesVitales != null)
              {                 
                  var Temperatura = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID);
                  if (Temperatura != null)
                  {
                      if (Temperatura.v_Value1 != null) ValorTemperatura = Temperatura.v_Value1;
                  }

                  var FRespiratoria = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID);
                  if (FRespiratoria != null)
                  {
                      if (FRespiratoria.v_Value1 != null) ValorFRespiratoria = FRespiratoria.v_Value1;
                  }

                  var FCardiaca = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID);
                  if (FCardiaca != null)
                  {
                      if (FCardiaca.v_Value1 != null) ValorFCardiaca = FCardiaca.v_Value1;
                  }

                  var SatO2 = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID);
                  if (SatO2 != null)
                  {
                      if (SatO2.v_Value1 != null) ValorSatO2 = SatO2.v_Value1;
                  }

                  var PAS = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID);
                  if (PAS != null)
                  {
                      if (PAS.v_Value1 != null) ValorPAS = PAS.v_Value1;
                  }

                  var PAD = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID);
                  if (PAD != null)
                  {
                      if (PAD.v_Value1 != null) ValorPAD = PAD.v_Value1;
                  }


              }
            #endregion

            #region Espirometria


            string ValorCVF = "", ValorFEV1 = "", ValorFEV1_FVC = "", ValorFEF25_75 = "", ValorResultadoABS = "",ValorObservacionABS = "",ValorConclusionABS = "";

            ServiceComponentList findEspirometria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
         
            if (findEspirometria != null)
            {
                var CVF = findEspirometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF);
                if (CVF != null)
                {
                    if (CVF.v_Value1 != null) ValorCVF = CVF.v_Value1;
                }

                var FEV1 = findEspirometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1);
                if (FEV1 != null)
                {
                    if (FEV1.v_Value1 != null) ValorFEV1 = FEV1.v_Value1;
                }

                var FEV1_FVC = findEspirometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1_CVF);
                if (FEV1_FVC != null)
                {
                    if (FEV1_FVC.v_Value1 != null) ValorFEV1_FVC = FEV1_FVC.v_Value1;
                }

                var FEF25_75 = findEspirometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_FEF_25_75);
                if (FEF25_75 != null)
                {
                    if (FEF25_75.v_Value1 != null) ValorFEF25_75 = FEF25_75.v_Value1;
                }

                var ResultadoABS = findEspirometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_FUNCIÓN_RESPIRATORIA_ABS_RESULTADOS);
                if (ResultadoABS != null)
                {
                    if (ResultadoABS.v_Value1 != null) ValorResultadoABS = ResultadoABS.v_Value1Name;
                }


                var ListaEspirometriaDx = diagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);
                string DiagnosticoEspirometria = "";

                foreach (var item in ListaEspirometriaDx)
                {
                    DiagnosticoEspirometria = item.v_DiseasesName + ";";
                }

                //var ObsABS = findEspirometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ESPIROMETRIA_FUNCIÓN_RESPIRATORIA_ABS_OBSERVACION);
                //if (ObsABS != null)
                //{
                //    if (ObsABS.v_Value1 != null) ValorObservacionABS = ObsABS.v_Value1;
                //}

                ValorConclusionABS = DiagnosticoEspirometria;


            }

            if (findFuncionesVitales != null)
            {
                var Temperatura = findFuncionesVitales.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_TEMPERATURA_ID);
                if (Temperatura != null)
                {
                    if (Temperatura.v_Value1 != null) ValorTemperatura = Temperatura.v_Value1;
                }
            }
            
            #endregion

            #region Habitos Noscivos

            PdfPCell TabacoNada = cellSinCheck, TabacoPoco = cellSinCheck, TabacoHabitual = cellSinCheck, TabacoExcesivo = cellSinCheck,
                   AlcoholNada = cellSinCheck, AlcoholPoco = cellSinCheck, AlcoholHabitual = cellSinCheck, AlcoholExcesivo = cellSinCheck,
                   DrogasNada = cellSinCheck, DrogasPoco = cellSinCheck, DrogasHabitual = cellSinCheck, DrogasExcesivo = cellSinCheck,
                  ActividadFisicaNada = cellSinCheck, ActividadFisicaPoco = cellSinCheck, ActividadFisicaHabitual = cellSinCheck, ActividadFisicaExcesivo = cellSinCheck;

            string ActividadFisicaDes = string.Empty;

            foreach (var item in listaHabitoNocivos)
            {

                if (item.i_TypeHabitsId ==(int)Sigesoft.Common.TypeHabit.Alcohol)
                {
                    if (item.v_FrecuenciaHabito.Trim().ToUpper() == "NUNCA" || item.v_FrecuenciaHabito.Trim().ToUpper() == "NADA" || item.v_FrecuenciaHabito.Trim().ToUpper() == "NO")
                    {
                        AlcoholNada = cellConCheck;
                        
                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "POCO")
                    {
                        AlcoholPoco = cellConCheck;
                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "HABITUAL")
                    {
                        AlcoholHabitual = cellConCheck;
                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "EXCESIVO")
                    {
                        AlcoholExcesivo = cellConCheck;
                    }
                }

                if (item.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Tabaco)
                {
                    if (item.v_FrecuenciaHabito.Trim().ToUpper() == "NUNCA" || item.v_FrecuenciaHabito.Trim().ToUpper() == "NADA" || item.v_FrecuenciaHabito.Trim().ToUpper() == "NO")
                    {
                        TabacoNada = cellConCheck;

                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "POCO")
                    {
                        TabacoPoco = cellConCheck;
                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "HABITUAL")
                    {
                        TabacoHabitual = cellConCheck;
                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "EXCESIVO")
                    {
                        TabacoExcesivo = cellConCheck;
                    }
                }

                if (item.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.Drogas)
                {
                    if (item.v_FrecuenciaHabito.Trim().ToUpper() == "NUNCA" || item.v_FrecuenciaHabito.Trim().ToUpper() == "NADA" || item.v_FrecuenciaHabito.Trim().ToUpper() == "NO")
                    {
                        DrogasNada = cellConCheck;

                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "POCO")
                    {
                        DrogasPoco = cellConCheck;
                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "HABITUAL")
                    {
                        DrogasHabitual = cellConCheck;
                    }
                    else if (item.v_FrecuenciaHabito.Trim().ToUpper() == "EXCESIVO")
                    {
                        DrogasExcesivo = cellConCheck;
                    }
                }

                if (item.i_TypeHabitsId == (int)Sigesoft.Common.TypeHabit.ActividadFisica)
                {
                    ActividadFisicaDes = item.v_FrecuenciaHabito;                
                }

            }

            string dxIMC = string.Empty;
            var antropometria = diagnosticRepository.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ANTROPOMETRIA_ID 
                                                            && p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);

            if (antropometria != null)
            {
                dxIMC = antropometria.v_DiseasesName;
            }
            
            #endregion
            
          
            cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("HÁBITOS", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("TABACO", fontColumnValue)){Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("ALCOHOL", fontColumnValue)){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("DROGAS", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_CENTER},                 
                    new PdfPCell(new Phrase("TALLA", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("PESO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("FUNCIÓN RESPIRATORIA ABS %", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("TEMPERATURA", fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //Linea
                    new PdfPCell(new Phrase("NADA", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(TabacoNada){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(AlcoholNada){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(DrogasNada){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },                 
                    new PdfPCell(new Phrase(ValorTalla, fontColumnValue)) { Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase(ValorPeso, fontColumnValue)){ Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_MIDDLE},
                    new PdfPCell(new Phrase("FVC", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorCVF, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(ValorTemperatura == "0.00" || ValorTemperatura == "0,00" || string.IsNullOrEmpty(ValorTemperatura) ? "Afebril" : double.Parse(ValorTemperatura).ToString("#.#"), fontColumnValue))
                                                        { Rowspan=2, Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //Linea
                    new PdfPCell(new Phrase("POCO", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(TabacoPoco){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(AlcoholPoco){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(DrogasPoco){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },               
                    new PdfPCell(new Phrase("FEV1", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorFEV1, fontColumnValue)),

                    //Linea
                    new PdfPCell(new Phrase("HABITUAL", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(TabacoHabitual){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(AlcoholHabitual){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },
                    new PdfPCell(DrogasHabitual){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_BOTTOM },               
                    new PdfPCell(new Phrase("IMC", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},        
                    new PdfPCell(new Phrase("FEV1/FVC", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorFEV1_FVC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("CINTURA", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorCintura, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                    //Linea
                    new PdfPCell(new Phrase("EXCESIVO", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,Rowspan=0, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(TabacoExcesivo){Rowspan=0,Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },
                    new PdfPCell(AlcoholExcesivo){Rowspan=0,Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },
                    new PdfPCell(DrogasExcesivo){Rowspan=0,Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },              
                    new PdfPCell(new Phrase(ValorIMC, fontColumnValue)){Rowspan=0,Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase("FEF 25-75%", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorFEF25_75, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("CADERA", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorCadera, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 
                    //   //Linea
                    // Alejandro                 
                    new PdfPCell(new Phrase("ACTIVIDAD FÍSICA:" + ActividadFisicaDes, fontColumnValue)) { Colspan = 4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //new PdfPCell(new Phrase("hola2", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    //new PdfPCell(new Phrase("hola3", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},                                       
                    //new PdfPCell(new Phrase("hola4", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(dxIMC, fontColumnValue)) { Colspan = 2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    //new PdfPCell(new Phrase("hola6", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("CONCLUSIÓN", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorConclusionABS, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("ICC", fontColumnValueBold)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorICC, fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                 };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 20f, 8f, 8f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Cabeza
            cells = new List<PdfPCell>()
                 {
                      new PdfPCell(new Phrase("CABEZA", fontColumnValue)),
                       new PdfPCell(new Phrase(ValorCabeza, fontColumnValue))
                 };
            columnWidths = new float[] { 15f,85f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Cuella y Nariz
             cells = new List<PdfPCell>()
                 {
                      new PdfPCell(new Phrase("CUELLO", fontColumnValue)),
                       new PdfPCell(new Phrase(ValorCuello, fontColumnValue)),
                        new PdfPCell(new Phrase("NARIZ", fontColumnValue)),
                       new PdfPCell(new Phrase(ValorNariz, fontColumnValue))
                 };
            columnWidths = new float[] { 15f,35f,10f,40f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Boca, Amigdalas
            cells = new List<PdfPCell>()
                 {
                      new PdfPCell(new Phrase("BOCA, AMÍGDALAS, FARINGE, LARINGE", fontColumnValue)),
                       new PdfPCell(new Phrase("PIEZAS EN MAL ESTADO", fontColumnValue)),                       
                       new PdfPCell(new Phrase(PiezasCaries, fontColumnValue)),

                       //lINEa
                        new PdfPCell(new Phrase(ValorBoca, fontColumnValue)),
                       new PdfPCell(new Phrase("PIEZAS QUE FALTAN", fontColumnValue)),                       
                       new PdfPCell(new Phrase(PiezasAusentes, fontColumnValue))

                 };
            columnWidths = new float[] { 65f, 25f, 10f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region OJOS


            ServiceComponentList findOftalmologia = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
            var TestIshihara = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TEST_ISHIHARA_ID);

            var DxCateodiaOftalmologia = diagnosticRepository.FindAll(p => p.i_CategoryId == 14);
            string ValorDxOftalmologia = "";
            string ValorOD_VC_SC = "", ValorOI_VC_SC = "", ValorOD_VC_CC = "", ValorOI_VC_CC = "";
            string ValorOD_VL_SC = "", ValorOI_VL_SC = "", ValorOD_VL_CC = "", ValorOI_VL_CC = "";
            string ValorDiscromatopsia = "";
            if (findOftalmologia != null)
            {


                var OD_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID);
                if (OD_VC_SC != null)
                {
                    if (OD_VC_SC.v_Value1 != null) ValorOD_VC_SC = OD_VC_SC.v_Value1;
                }

                var OI_VC_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO);
                if (OI_VC_SC != null)
                {
                    if (OI_VC_SC.v_Value1 != null) ValorOI_VC_SC = OI_VC_SC.v_Value1;
                }

                var OD_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO);
                if (OD_VC_CC != null)
                {
                    if (OD_VC_CC.v_Value1 != null) ValorOD_VC_CC = OD_VC_CC.v_Value1;
                }

                var OI_VC_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID);
                if (OI_VC_CC != null)
                {
                    if (OI_VC_CC.v_Value1 != null) ValorOI_VC_CC = OI_VC_CC.v_Value1;
                }
                                
                var OD_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID);
                if (OD_VL_SC != null)
                {
                    if (OD_VL_SC.v_Value1 != null) ValorOD_VL_SC = OD_VL_SC.v_Value1;
                }

                var OI_VL_SC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID);
                if (OI_VL_SC != null)
                {
                    if (OI_VL_SC.v_Value1 != null) ValorOI_VL_SC = OI_VL_SC.v_Value1;
                }

                var OD_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID);
                if (OD_VL_CC != null)
                {
                    if (OD_VL_CC.v_Value1 != null) ValorOD_VL_CC = OD_VL_CC.v_Value1;
                }
                
                var OI_VL_CC = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID);
                if (OI_VL_CC != null)
                {
                    if (OI_VL_CC.v_Value1 != null) ValorOI_VL_CC = OI_VL_CC.v_Value1;
                }

                if (DxCateodiaOftalmologia != null)
                {
                    ValorDxOftalmologia = string.Join(", ", DxCateodiaOftalmologia.Select(p => p.v_DiseasesName));   
               
                }
                
                //if (findOftalmologia.DiagnosticRepository != null)
                //{
                //    ValorDxOftalmologia = string.Join(", ", findOftalmologia.DiagnosticRepository.Select(p => p.v_DiseasesName));

                //}

                //var Discromatopsia = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DICROMATOPSIA_ID);
                //var NormalAnormal = findOftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL);

                //if (Discromatopsia != null || NormalAnormal != null)
                //{
                //    var a = NormalAnormal.v_Value1.ToString() == "1" ? "NORMAL" : "ANORMAL";
                //    var b = Discromatopsia.v_Value1Name;

                //    ValorDiscromatopsia = a + " / " + b;
                //}

                //TEST DE ISHIHARA: Anormal, Discromatopsia: No definida.


                if (TestIshihara != null)
                {
                    string TestIshiharaNormal = TestIshihara.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL) == null ? "" : TestIshihara.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL).v_Value1;// TestIshihara.Count() == 0 || ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL)).v_Value1;
                    string TestIshiharaAnormal = TestIshihara.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL) == null ? "" : TestIshihara.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL).v_Value1;// TestIshihara.Count() == 0 || ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL)).v_Value1;
                    string Dicromatopsia = TestIshihara.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC) == null ? "" : TestIshihara.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC).v_Value1Name;// TestIshihara.Count() == 0 || ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)) == null ? string.Empty : ((ServiceComponentFieldValuesList)TestIshihara.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.TEST_ISHIHARA_DESC)).v_Value1Name;


                    if (TestIshiharaNormal == "1")
                    {
                        ValorDiscromatopsia = "NORMAL";
                    }
                    else if (TestIshiharaAnormal == "1")
                    {

                        ValorDiscromatopsia = " ANORMAL" + " DISCROMATOPSIA: " + Dicromatopsia;
                    }
                }
                else
                {
                    ValorDiscromatopsia = "NO APLICA";
                }
               


            }

            cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("OJOS", fontColumnValue)){Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("SIN CORREGIR", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("CORREGIDA", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("ENFERMEDADES OCULARES", fontColumnValue)),

                    //Linea
                    //linea en blanco
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.D", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("O.I", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorDxOftalmologia, fontColumnValue)){Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE LEJOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorOD_VC_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VC_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOD_VC_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VC_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    
                    //Linea
                    new PdfPCell(new Phrase("VISIÓN DE CERCA", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase(ValorOD_VL_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VL_SC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOD_VL_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorOI_VL_CC, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("REFLEJOS PUPILARES", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                      //Linea
                    new PdfPCell(new Phrase("VISIÓN DE COLORES", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                    new PdfPCell(new Phrase(ValorDiscromatopsia, fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(ValorReflejosPupilares, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                 };
            columnWidths = new float[] { 15f, 10f, 10f, 10f, 10f, 55f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);


            #endregion

            #region Audiometria

            #region Audiometria
            //ServiceComponentList findAudiometria = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
            //var xxx = findAudiometria.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.UserControlAudimetria);
           
            string ValorOtoscopiaOI = "", ValorOtoscopiaOD ="";

            string ValorVA_OD_500 = "", ValorVA_OD_1000 = "", ValorVA_OD_2000 = "", ValorVA_OD_3000 = "", ValorVA_OD_4000 = "", ValorVA_OD_6000 = "", ValorVA_OD_8000 = "",
                    ValorVA_OI_500 = "", ValorVA_OI_1000 = "", ValorVA_OI_2000 = "", ValorVA_OI_3000 = "", ValorVA_OI_4000 = "", ValorVA_OI_6000 = "", ValorVA_OI_8000 = "";

          

            var VA_OD_500 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500);
            if (VA_OD_500 != null)
            {
                if (VA_OD_500.v_Value1 != null) ValorVA_OD_500 = VA_OD_500.v_Value1;
            }

            var VA_OD_1000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000);
            if (VA_OD_1000 != null)
            {
                if (VA_OD_1000.v_Value1 != null) ValorVA_OD_1000 = VA_OD_1000.v_Value1;
            }

            var VA_OD_2000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000);
            if (VA_OD_2000 != null)
            {
                if (VA_OD_2000.v_Value1 != null) ValorVA_OD_2000 = VA_OD_2000.v_Value1;
            }

            var VA_OD_3000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000);
            if (VA_OD_3000 != null)
            {
                if (VA_OD_3000.v_Value1 != null) ValorVA_OD_3000 = VA_OD_3000.v_Value1;
            }

            var VA_OD_4000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000);
            if (VA_OD_4000 != null)
            {
                if (VA_OD_4000.v_Value1 != null) ValorVA_OD_4000 = VA_OD_4000.v_Value1;
            }

            var VA_OD_6000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000);
            if (VA_OD_6000 != null)
            {
                if (VA_OD_6000.v_Value1 != null) ValorVA_OD_6000 = VA_OD_6000.v_Value1;
            }

            var VA_OD_8000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000);
            if (VA_OD_8000 != null)
            {
                if (VA_OD_8000.v_Value1 != null) ValorVA_OD_8000 = VA_OD_8000.v_Value1;
            }


            //-------------------------------------------------------------------------------------


            var VA_OI_500 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500);
            if (VA_OI_500 != null)
            {
                if (VA_OI_500.v_Value1 != null) ValorVA_OI_500 = VA_OI_500.v_Value1;
            }

            var VA_OI_1000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000);
            if (VA_OI_1000 != null)
            {
                if (VA_OI_1000.v_Value1 != null) ValorVA_OI_1000 = VA_OI_1000.v_Value1;
            }

            var VA_OI_2000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000);
            if (VA_OI_2000 != null)
            {
                if (VA_OI_2000.v_Value1 != null) ValorVA_OI_2000 = VA_OI_2000.v_Value1;
            }

            var VA_OI_3000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000);
            if (VA_OI_3000 != null)
            {
                if (VA_OI_3000.v_Value1 != null) ValorVA_OI_3000 = VA_OI_3000.v_Value1;
            }

            var VA_OI_4000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000);
            if (VA_OI_4000 != null)
            {
                if (VA_OI_4000.v_Value1 != null) ValorVA_OI_4000 = VA_OI_4000.v_Value1;
            }

            var VA_OI_6000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000);
            if (VA_OI_6000 != null)
            {
                if (VA_OI_6000.v_Value1 != null) ValorVA_OI_6000 = VA_OI_6000.v_Value1;
            }

            var VA_OI_8000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000);
            if (VA_OI_8000 != null)
            {
                if (VA_OI_8000.v_Value1 != null) ValorVA_OI_8000 = VA_OI_8000.v_Value1;
            }
            
            var OtoscopiaOD = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.AUDIOMETRIA_OTOSCOPIA_OD);
            if (OtoscopiaOD != null)
            {
                if (OtoscopiaOD.v_Value1 != null) ValorOtoscopiaOD = OtoscopiaOD.v_Value1;
            }

            var OtoscopiaOI = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.AUDIOMETRIA_OTOSCOPIA_OI);
            if (OtoscopiaOI != null)
            {
                if (OtoscopiaOI.v_Value1 != null) ValorOtoscopiaOI = OtoscopiaOI.v_Value1;
            }





            string ValorVO_OD_500 = "", ValorVO_OD_1000 = "", ValorVO_OD_2000 = "", ValorVO_OD_3000 = "", ValorVO_OD_4000 = "", ValorVO_OD_6000 = "", ValorVO_OD_8000 = "",
          ValorVO_OI_500 = "", ValorVO_OI_1000 = "", ValorVO_OI_2000 = "", ValorVO_OI_3000 = "", ValorVO_OI_4000 = "", ValorVO_OI_6000 = "", ValorVO_OI_8000 = "";


            var VO_OD_500 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_500);
            if (VO_OD_500 != null)
            {
                if (VO_OD_500.v_Value1 != null) ValorVO_OD_500 = VO_OD_500.v_Value1;
            }

            var VO_OD_1000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_1000);
            if (VO_OD_1000 != null)
            {
                if (VO_OD_1000.v_Value1 != null) ValorVO_OD_1000 = VO_OD_1000.v_Value1;
            }

            var VO_OD_2000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_2000);
            if (VO_OD_2000 != null)
            {
                if (VO_OD_2000.v_Value1 != null) ValorVO_OD_2000 = VO_OD_2000.v_Value1;
            }

            var VO_OD_3000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_3000);
            if (VO_OD_3000 != null)
            {
                if (VO_OD_3000.v_Value1 != null) ValorVO_OD_3000 = VO_OD_3000.v_Value1;
            }

            var VO_OD_4000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_4000);
            if (VO_OD_4000 != null)
            {
                if (VO_OD_4000.v_Value1 != null) ValorVO_OD_4000 = VO_OD_4000.v_Value1;
            }

            var VO_OD_6000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_6000);
            if (VO_OD_6000 != null)
            {
                if (VO_OD_6000.v_Value1 != null) ValorVO_OD_6000 = VO_OD_6000.v_Value1;
            }

            var VO_OD_8000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_8000);
            if (VO_OD_8000 != null)
            {
                if (VO_OD_8000.v_Value1 != null) ValorVO_OD_8000 = VO_OD_8000.v_Value1;
            }


            //-------------------------------------------------------------------------------------


            var VO_OI_500 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_500);
            if (VO_OI_500 != null)
            {
                if (VO_OI_500.v_Value1 != null) ValorVO_OI_500 = VO_OI_500.v_Value1;
            }

            var VO_OI_1000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_1000);
            if (VO_OI_1000 != null)
            {
                if (VO_OI_1000.v_Value1 != null) ValorVO_OI_1000 = VO_OI_1000.v_Value1;
            }

            var VO_OI_2000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_2000);
            if (VO_OI_2000 != null)
            {
                if (VO_OI_2000.v_Value1 != null) ValorVO_OI_2000 = VO_OI_2000.v_Value1;
            }

            var VO_OI_3000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_3000);
            if (VO_OI_3000 != null)
            {
                if (VO_OI_3000.v_Value1 != null) ValorVO_OI_3000 = VO_OI_3000.v_Value1;
            }

            var VO_OI_4000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_4000);
            if (VO_OI_4000 != null)
            {
                if (VO_OI_4000.v_Value1 != null) ValorVO_OI_4000 = VO_OI_4000.v_Value1;
            }

            var VO_OI_6000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_6000);
            if (VO_OI_6000 != null)
            {
                if (VO_OI_6000.v_Value1 != null) ValorVO_OI_6000 = VO_OI_6000.v_Value1;
            }

            var VO_OI_8000 = Audiometria.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_8000);
            if (VO_OI_8000 != null)
            {
                if (VO_OI_8000.v_Value1 != null) ValorVO_OI_8000 = VO_OI_8000.v_Value1;
            }

           

            #endregion

            document.NewPage();
            #region Title

             CellLogo = null;
            cells = new List<PdfPCell>();
             cellPhoto1 = null;

            //if (filiationData.b_Photo != null)
            //    cellPhoto1 = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 23F)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
            //else
                cellPhoto1 = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };

            if (infoEmpresaPropietaria.b_Image != null)
            {
                CellLogo = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }
            else
            {
                CellLogo = new PdfPCell(new Phrase(" ", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT };
            }

            columnWidths = new float[] { 100f };

             cellsTit = new List<PdfPCell>()
            { 

                new PdfPCell(new Phrase("ANEXO 16", fontTitle1))
                                { HorizontalAlignment = PdfPCell.ALIGN_CENTER },
                
            };

            table = HandlingItextSharp.GenerateTableFromCells(cellsTit, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);

            cells.Add(CellLogo);
            cells.Add(new PdfPCell(table));
            cells.Add(cellPhoto1);

            columnWidths = new float[] { 20f, 60f, 20f };

            table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, null, fontTitleTable);
            document.Add(table);

            #endregion

            cells = new List<PdfPCell>()
                 {                   
                    //Linea
                    new PdfPCell(new Phrase("OIDOS", fontColumnValue)){Colspan=2,Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("Audición derecha 500 1000 2000 3000 8000", fontColumnValue)){Colspan=8, Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=2, Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("Audición izquierda 500 1000 2000 3000 8000", fontColumnValue)){Colspan=10, Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},                        
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                    //Linea
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.BOTTOM_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    //Linea
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("Hz", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("500", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("1000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("2000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("3000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("4000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("6000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("8000", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("Hz", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("500", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("1000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("2000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("3000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("4000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("6000", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("8000", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},

                    //linea                     
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                    //linea                     
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("dB(A)", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OD_500, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OD_1000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OD_2000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OD_3000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OD_4000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OD_6000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OD_8000, fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("dB(A)", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OI_500, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OI_1000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OI_2000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OI_3000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OI_4000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OI_6000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVA_OI_8000, fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                     //linea                     
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                    //linea                     
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("dB(O)", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OD_500, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OD_1000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OD_2000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OD_3000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OD_4000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OD_6000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OD_8000, fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("dB(O)", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OI_500, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OI_1000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OI_2000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OI_3000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OI_4000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OI_6000, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVO_OI_8000, fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    

                    //linea                     
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.TOP_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                    
                    //linea                     
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=3, Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},     
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=2 , Border= PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan=3,Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.LEFT_BORDER},  
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=4, Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    
                    //linea                     
                    new PdfPCell(new Phrase("OTOSCOPIA", fontColumnValue)){Colspan=2, Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                    new PdfPCell(new Phrase("OD", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorOtoscopiaOD, fontColumnValue)){Colspan=8 , Border= PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("F. RESPIRATORIA", fontColumnValue)){ Colspan=3,Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.LEFT_BORDER},  
                    new PdfPCell(new Phrase(ValorFRespiratoria, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" x min", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.LEFT_BORDER}, 
                    new PdfPCell(new Phrase("PRESIÓN ARTERIAL SISTÉMICA", fontColumnValue)){Colspan=5,HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                    //linea                     
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=2, Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},    
                    new PdfPCell(new Phrase("OI", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorOtoscopiaOI, fontColumnValue)){Colspan=8 , Border= PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT},  
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("F. CARDIACA", fontColumnValue)){ Colspan=3,Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.LEFT_BORDER},  
                    new PdfPCell(new Phrase(ValorFCardiaca, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" x min", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("SISTÓLICA", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase( ValorPAS + " mmHg", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                    //linea                     
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=3, Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},     
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("", fontColumnValue)){Colspan=2 , Border= PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},  
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("Sat. O2", fontColumnValue)){ Colspan=3,Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.LEFT_BORDER},  
                    new PdfPCell(new Phrase(ValorSatO2, fontColumnValue)){Border = PdfPCell.NO_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase("%", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("DIASTÓLICA", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(ValorPAD + "mmHg", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Border = PdfPCell.RIGHT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 

                 };
            columnWidths = new float[] { 4.5f, 6f, 4.5f, 4.5f, 4.5f, 4.5f, 4.5f, 4.5f, 4.5f, 3.5f, 3.5f, 3.5f, 6f, 4.5f, 4.5f, 4.5f, 5.5f, 4.5f, 4.5f, 4.5f, 4.5f, 6.5f, 4.5f };


            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Pulmones
            cells = new List<PdfPCell>()
                 {
                    new PdfPCell(new Phrase("PULMONES", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("NORMAL", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    //new PdfPCell(new Phrase("X", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(ValorPulmonesNormal){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(new Phrase("ANORMAL", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    //new PdfPCell(new Phrase("X", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 
                    new PdfPCell(ValorPulmonesAnormal){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
              
                    new PdfPCell(new Phrase("DESCRIPCIÓN", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(ValorPulmonDescripcion, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //Linea
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=7, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //Linea
                    new PdfPCell(new Phrase("MIEMBROS SUPERIORES", fontColumnValueBold)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT}  ,
                    new PdfPCell(new Phrase(ValorMiembrosSuperiores, fontColumnValue)){Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT}    , 
 
                    //Linea
                    new PdfPCell(new Phrase("MIEMBROS INFERIORES", fontColumnValueBold)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT}  ,
                    new PdfPCell(new Phrase(ValorMiembrosInferiores, fontColumnValue)){Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT}  ,    

                    //Linea
                    new PdfPCell(new Phrase("REFLEJOS OSTEO-TENDINOSOS", fontColumnValueBold)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT}  ,
                    new PdfPCell(new Phrase(ValorReflejosOsteoTendinosos, fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT} , 
                    new PdfPCell(new Phrase("MARCHA", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT} , 
                    new PdfPCell(new Phrase(ValorMarcha, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT} , 

                    //Linea
                    new PdfPCell(new Phrase("COLUMNA VERTEBRAL", fontColumnValueBold)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT}  ,
                    new PdfPCell(new Phrase( ValorColumna, fontColumnValue)){Colspan=5, HorizontalAlignment = PdfPCell.ALIGN_LEFT}    , 
                 };
            columnWidths = new float[] { 15f, 10f, 5f,10f,5f,10f,45f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Abdomen

            cells = new List<PdfPCell>()
                 {
                    //Linea
                    new PdfPCell(new Phrase("ABDOMEN", fontColumnValueBold)){ Rowspan =3 ,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorAbdomen, fontColumnValue)){Rowspan =3 ,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("TACTO RECTAL", fontColumnValue)) { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_CENTER}, 

                    //Linea
                    
                    new PdfPCell(new Phrase("DIFERIDO", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    //new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(ValorTactoRectalSinRealizar){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(new Phrase("ANORMAL", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    //new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(ValorTactoRectalAnormal){HorizontalAlignment = PdfPCell.ALIGN_LEFT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE }, 

                    //Linea
                    
                    new PdfPCell(new Phrase("NORMAL", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    //new PdfPCell(new Phrase("", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(ValorTactoRectalNormal){Border = PdfPCell.NO_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(new Phrase("OBS.", fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(ValorTactoRectalDescripcion, fontColumnValue)) {HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                 };
            columnWidths = new float[] { 15f, 30f, 10f, 5f, 10f, 15f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region Anillos

            #region Psicologia
            string ValorAreaCognitiva = "" ,ValorAreaEmocional="", ValorConclusionPsicologia="";
            string ConcatenadoPsicologia = "";

            ServiceComponentList findPsicologia = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PSICOLOGIA_ID);
            if (findPsicologia !=null)
            {
                var AreaCognitiva = findPsicologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_COGNITIVA_ID);
                if (AreaCognitiva != null)
                {
                    ValorAreaCognitiva = AreaCognitiva.v_Value1Name + ", ";
                }

                var AreaEmocional = findPsicologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_EMOCIONAL_ID);
                if (AreaEmocional != null)
                {
                    ValorAreaEmocional = AreaEmocional.v_Value1Name + ", ";
                }

                var ConclusionPsicologia = findPsicologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.PSICOLOGIA_AREA_CONCLUSIONES_ID);
                if (ConclusionPsicologia != null)
                {
                    ValorConclusionPsicologia = ConclusionPsicologia.v_Value1 + ", ";
                }


                ConcatenadoPsicologia = ValorAreaCognitiva + ValorAreaEmocional + ValorConclusionPsicologia;
                if (ConcatenadoPsicologia != string.Empty)
                {
                    ConcatenadoPsicologia.Substring(0, ConcatenadoPsicologia.Length - 3);
                }
            }
            
           
            
            #endregion
            cells = new List<PdfPCell>()
                 {
                    //Linea
                    new PdfPCell(new Phrase("ANILLO INGINALES", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorAnilloInguinales, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("HERNIAS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorHernias, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("VARICES", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorVarice, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //Linea
                    new PdfPCell(new Phrase("ÓRGANOS GENITALES", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorGenitales, fontColumnValue)){ Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase("GANGLIOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                    new PdfPCell(new Phrase(ValorGangleos, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //Linea
                    new PdfPCell(new Phrase("LENGUAJE, ATENCIÓN, MEMORIA, ORIENTACIÓN, INTELIGENCIA, AFECTIVIDAD", fontColumnValue)){Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    new PdfPCell(new Phrase(ConcatenadoPsicologia, fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                  

                 };
            columnWidths = new float[] { 10f,20f,10f,20f,10f,20f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);
            #endregion

            #region Imagen

            #region Rayos X

            ServiceComponentList findRayosX = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.RX_TORAX_ID);

            var findOIT = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID);

         

            string ValorVertices = "", ValorCamposPulmonares="", ValorHilos="", ValorDiafragmaticos="", ValorCardiofrenicos="", ValorMediastinos=""
                    , ValorSiluetaCardiaca="", ValorConclusionesRx="",
                    ValorNroRx = "", ValorFechaToma = "", ValorCalidad = "", NroPlacaRx ="";

            PdfPCell Cero = cellSinCheck, UnoCero = cellSinCheck, Uno = cellSinCheck, Dos = cellSinCheck, Tres = cellSinCheck, Cuatro = cellSinCheck;
            PdfPCell ABC = cellSinCheck;

            PdfPCell Sin_Neumoconiosis = cellSinCheck;
            PdfPCell Con_Hallazgos = cellSinCheck;
            PdfPCell Con_Neumoconiosis = cellSinCheck;

            PdfPCell Apto = cellSinCheck, NoApto = cellSinCheck, AptoConRestricciones = cellSinCheck, AptoObs = cellSinCheck;

            if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.Apto)
            {
                Apto = cellConCheck;
            }
            else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.NoApto)
            {
                NoApto = cellConCheck;
            }
            else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.AptRestriccion)
            {
                AptoConRestricciones = cellConCheck;
            }
            else if (DataService.i_AptitudeStatusId == (int)Sigesoft.Common.AptitudeStatus.AptoObs)
            {
                AptoObs = cellConCheck;
            }
            // Alejandro
            //string RX_CONCLUSIONES_OIT_DESCRIPCION_ID = "";
            var ConclusionesOITDescripcionSinNeumoconiosis = string.Empty;
            var ConclusionesOITDescripcionConNeumoconiosis = string.Empty;
            string ExposicionPolvoDescripcion = string.Empty;

            if (findOIT != null)
            {
                var CONCLUSIONES_OIT_DESCRIPCION_ID = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID);
               

                //if (CONCLUSIONES_OIT_DESCRIPCION_ID != null)
                //{
                //    RX_CONCLUSIONES_OIT_DESCRIPCION_ID = CONCLUSIONES_OIT_DESCRIPCION_ID.v_Value1;
                //}

                var ValorRX_NEUMOCONIOSIS_CHECK = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_NEUMOCONIOSIS_CHECK);
                
                if (ValorRX_NEUMOCONIOSIS_CHECK != null)
                {
                    if (ValorRX_NEUMOCONIOSIS_CHECK.v_Value1 == "1")
                    {
                        Sin_Neumoconiosis = cellConCheck;

                        //if (CONCLUSIONES_OIT_DESCRIPCION_ID != null)
                        //{
                        //    ConclusionesOITDescripcionSinNeumoconiosis = CONCLUSIONES_OIT_DESCRIPCION_ID.v_Value1;
                        //}
                    }
                    else
                    {
                        Con_Neumoconiosis = cellConCheck;
                    }

                }


                var Vertices = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.VERTICES);
                if (Vertices != null)
                {
                    ValorVertices = Vertices.v_Value1;
                }

                var CamposPulmonares = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.CAMPOS_PULMONARES);
                if (CamposPulmonares != null)
                {
                    ValorCamposPulmonares = CamposPulmonares.v_Value1;
                }

                var Hilos = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HILOS);
                if (Hilos != null)
                {
                    ValorHilos = Hilos.v_Value1;
                }

                var Diafragmaticos = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SENOS);
                if (Diafragmaticos != null)
                {
                    ValorDiafragmaticos = Diafragmaticos.v_Value1;
                }


                var Mediastinos = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.MEDIASTINOS);
                if (Mediastinos != null)
                {
                    ValorMediastinos = Mediastinos.v_Value1;
                }

                var SiluetaCardiaca = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.SILUETA_CARDIOVASCULAR);
                if (SiluetaCardiaca != null)
                {
                    ValorSiluetaCardiaca = SiluetaCardiaca.v_Value1;
                }



                //var Valor_Con_Neumoconiosis = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_OIT_ID);
                
                //if (Valor_Con_Neumoconiosis != null)
                //{
                //    if (Valor_Con_Neumoconiosis.v_Value1 == "2")
                //    {
                //        Con_Neumoconiosis = cellConCheck;

                //        if (CONCLUSIONES_OIT_DESCRIPCION_ID != null)
                //        {
                //            ConclusionesOITDescripcionConNeumoconiosis = CONCLUSIONES_OIT_DESCRIPCION_ID.v_Value1;
                //        }
                //    }
                //}

                var Valor_Con_Hallazgos = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_EXPOSICION_POLVO_ID);
                var Exposicion_Polvo_Descripcion = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_EXPOSICION_POLVO_DESCRIPCION_ID);

                if (Exposicion_Polvo_Descripcion != null)
                {
                    ExposicionPolvoDescripcion = Exposicion_Polvo_Descripcion.v_Value1;
                }

                if (Valor_Con_Hallazgos != null)
                {
                    if (Valor_Con_Hallazgos.v_Value1 == "1")
                    {
                        Con_Hallazgos = cellConCheck;
                    }
                }

                var ValorCero = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_0_0_ID);
                if (ValorCero != null)
                {
                    if (ValorCero.v_Value1 == "1")
                    {
                        Cero = cellConCheck;
                    }
                }

                var ValorUnoCero = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_1_0_ID);
                if (ValorUnoCero != null)
                {
                    if (ValorUnoCero.v_Value1 == "1")
                    {
                        UnoCero = cellConCheck;
                    }
                }
                //--------
                var ValorUnoUno = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_1_1_ID);
                if (ValorUnoUno != null)
                {
                    if (ValorUnoUno.v_Value1 == "1")
                    {
                        Uno = cellConCheck;
                    }
                }

                var ValorUnoDos = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_1_2_ID);
                if (ValorUnoDos != null)
                {
                    if (ValorUnoDos.v_Value1 == "1")
                    {
                        Uno = cellConCheck;
                    }
                }
                //--------
                var ValorDosUno = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_2_1_ID);
                if (ValorDosUno != null)
                {
                    if (ValorDosUno.v_Value1 == "1")
                    {
                        Dos = cellConCheck;
                    }
                }

                var ValorDosDos = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_2_2_ID);
                if (ValorDosDos != null)
                {
                    if (ValorDosDos.v_Value1 == "1")
                    {
                        Dos = cellConCheck;
                    }
                }

                var ValorDosTres = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_2_3_ID);
                if (ValorDosTres != null)
                {
                    if (ValorDosTres.v_Value1 == "1")
                    {
                        Dos = cellConCheck;
                    }
                }
                //--------
                var ValorTresDos = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_3_2_ID);
                if (ValorTresDos != null)
                {
                    if (ValorTresDos.v_Value1 == "1")
                    {
                        Tres = cellConCheck;
                    }
                }
                var ValorTresTres = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_3_3_ID);
                if (ValorTresTres != null)
                {
                    if (ValorTresTres.v_Value1 == "1")
                    {
                        Tres = cellConCheck;
                    }
                }
                var ValorTresMas = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_3_MAS_ID);
                if (ValorTresMas != null)
                {
                    if (ValorTresMas.v_Value1 == "1")
                    {
                        Tres = cellConCheck;
                    }
                }


                var NroRx = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_NRO_PLACA_ID);
                if (NroRx != null)
                {
                    ValorNroRx = NroRx.v_Value1;
                }

                var FechaToma = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_FECHA_TOMA_ID);
                if (FechaToma != null)
                {
                    ValorFechaToma = FechaToma.v_Value1;
                }

                var Calidad = findOIT.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CALIDAD_ID);
                if (Calidad != null)
                {
                    ValorCalidad = Calidad.v_Value1Name;
                }

            }

            if (findRayosX != null)
            {

                var NroPLacaA = findRayosX.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CODIGO_PLACA_ID);
                if (NroPLacaA != null)
                {
                    NroPlacaRx = NroPLacaA.v_Value1;
                }


                var ValorA = findRayosX.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_A_ID);
                if (ValorA != null)
                {
                    if (ValorA.v_Value1 == "1")
                    {
                        ABC = cellConCheck;
                    }
                }

                var ValorB = findRayosX.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_B_ID);
                if (ValorB != null)
                {
                    if (ValorB.v_Value1 == "1")
                    {
                        ABC = cellConCheck;
                    }
                }

                var ValorC = findRayosX.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_C_ID);
                if (ValorC != null)
                {
                    if (ValorC.v_Value1 == "1")
                    {
                        ABC = cellConCheck;
                    }
                }



                var Cardiofrenicos = findRayosX.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_SENOS_CARDIOFRENICOS_DESCRIPCION_ID);
                if (Cardiofrenicos != null)
                {
                    ValorCardiofrenicos = Cardiofrenicos.v_Value1;
                }


               
            }
            var DxRx = diagnosticRepository.FindAll(p => p.i_CategoryId == 6);
            //var ConclusionesRx = findRayosX.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID);
            var concatRx = string.Join(", ", DxRx.Select(p => p.v_DiseasesName));
            if (concatRx != "")
            {
                ValorConclusionesRx = concatRx.ToString(); //var concat = string.Join(", ", query.Select(p => p.v_DiseasesName));
            }
            #endregion

            #region Laboratorio
            ServiceComponentList findLaboratorio = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.VDRL_ID);
            ServiceComponentList findLaboratorioGrupoSanguineo = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.GRUPO_Y_FACTOR_SANGUINEO_ID);

            ServiceComponentList findLaboratorioHemoglobina = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID);
            ServiceComponentList oHEMOGRAMA_COMPLETO_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_ID);


            ServiceComponentList oLABORATORIO_HEMOGLOBINA_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMOGLOBINA_ID);
            ServiceComponentList oLABORATORIO_HEMATOCRITO_ID = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LABORATORIO_HEMATOCRITO_ID);


            ServiceComponentList HEMOGRAMA_COMPLETO_HEMATOCRITO = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO);
            ServiceComponentList HEMATOCRITO_HEMOGRAMA_HEMATOCRITO = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HEMATOCRITO_HEMOGRAMA_HEMATOCRITO);



            //ServiceComponentList findLaboratorioHematocrito = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);

            PdfPCell ReaccionPositivo = cellSinCheck, ReaccionNegativo = cellSinCheck;
            string VDRLValor = "";
            PdfPCell SangreO = cellSinCheck, SangreA = cellSinCheck, SangreB = cellSinCheck, SangreAB = cellSinCheck, SangreRHPositivo = cellSinCheck, SangreRHNegativo = cellSinCheck;
            PdfPCell rhPositivo = cellSinCheck, rhNegativo = cellSinCheck;

            string ValorHemoglobina1 = "", ValorHematocrito1 = "";
            string ValorHemoglobina2 = "", ValorHematocrito2 = "";
            var Resultado_HEMOGRAMA_COMPLETO_HEMOGLOBINA = "";
            var Resultado_HEMOGRAMA_COMPLETO_HEMATOCRITO = "";
      
               
                if (oHEMOGRAMA_COMPLETO_ID != null)
                {
                    var Value_HEMOGRAMA_COMPLETO_HEMOGLOBINA = oHEMOGRAMA_COMPLETO_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA);
                   
                        if (Value_HEMOGRAMA_COMPLETO_HEMOGLOBINA != null)
                        {
                            Resultado_HEMOGRAMA_COMPLETO_HEMOGLOBINA = Value_HEMOGRAMA_COMPLETO_HEMOGLOBINA.v_Value1;
                        }

                        var Value_HEMOGRAMA_COMPLETO_HEMATOCRITO = oHEMOGRAMA_COMPLETO_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO);

                        if (Value_HEMOGRAMA_COMPLETO_HEMATOCRITO != null)
                        {
                            Resultado_HEMOGRAMA_COMPLETO_HEMATOCRITO = Value_HEMOGRAMA_COMPLETO_HEMATOCRITO.v_Value1;
                        }


                        ValorHemoglobina1 = Resultado_HEMOGRAMA_COMPLETO_HEMOGLOBINA + " / " + Resultado_HEMOGRAMA_COMPLETO_HEMATOCRITO;
                }


                if (oLABORATORIO_HEMOGLOBINA_ID != null || oLABORATORIO_HEMATOCRITO_ID != null)
                {

                    var Value_HEMOGLOBINA_ID = oLABORATORIO_HEMOGLOBINA_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGLOBINA_ID);
                    var Value_HEMATOCRITO_ID = oLABORATORIO_HEMATOCRITO_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_ID);



                    ValorHemoglobina2 = Value_HEMOGLOBINA_ID.v_Value1 + " / " + Value_HEMATOCRITO_ID.v_Value1;

                }
         

























            //if (findLaboratorioHematocrito != null)
            //{
            //    var Hematocrito = findLaboratorioHematocrito.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMATOCRITO_ID);
            //    if (Hematocrito != null)
            //    {

            //        // HEMOGRAMA_COMPLETO_HEMATOCRITO
            //        if (HEMOGRAMA_COMPLETO_ID != null)
            //        {
            //            var Value_HEMOGRAMA_COMPLETO_HEMATOCRITO = HEMOGRAMA_COMPLETO_ID.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.HEMOGRAMA_COMPLETO_HEMATOCRITO);

            //            if (Value_HEMOGRAMA_COMPLETO_HEMATOCRITO != null)
            //            {
            //                Resultado_HEMOGRAMA_COMPLETO_HEMATOCRITO = Value_HEMOGRAMA_COMPLETO_HEMATOCRITO.v_Value1;
            //            }
            //        }
                  


            //        ValorHematocrito1 = Hematocrito.v_Value1 + " " + Resultado_HEMOGRAMA_COMPLETO_HEMATOCRITO;
            //    }
            //}


              if (findLaboratorioGrupoSanguineo !=null)
              {
                  var ValorSangreO = findLaboratorioGrupoSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                  if (ValorSangreO != null)
                  {
                      if (ValorSangreO.v_Value1 == "1")
                      {
                          SangreO = cellConCheck;
                      }

                  }

                  var ValorSangreA = findLaboratorioGrupoSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                  if (ValorSangreA != null)
                  {
                      if (ValorSangreA.v_Value1 == "2")
                      {
                          SangreA = cellConCheck;
                      }

                  }

                  var ValorSangreB = findLaboratorioGrupoSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                  if (ValorSangreB != null)
                  {
                      if (ValorSangreB.v_Value1 == "3")
                      {
                          SangreB = cellConCheck;
                      }

                  }

                  var ValorSangreAB = findLaboratorioGrupoSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.GRUPO_SANGUINEO_ID);
                  if (ValorSangreAB != null)
                  {
                      if (ValorSangreAB.v_Value1 == "4")
                      {
                          SangreAB = cellConCheck;
                      }

                  }

                  var Factor_rh_Positivo = findLaboratorioGrupoSanguineo.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FACTOR_SANGUINEO_ID);
                  if (Factor_rh_Positivo != null)
                  {
                      if (Factor_rh_Positivo.v_Value1 == "1")
                      {
                          rhPositivo  = cellConCheck;
                      }
                      else if (Factor_rh_Positivo.v_Value1 == "2")
                      {
                          rhNegativo = cellConCheck;
                      }


                  }



              }
              if (findLaboratorio != null)
              {
                  var ValorVDRL = findLaboratorio.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.LABORATORIO_VDRL_ID);
                  if (ValorVDRL != null)
                  {

                      VDRLValor = ValorVDRL.v_Value1;
                      //if (ValorVDRL.v_Value1 == "1")
                      //{
                      //    ReaccionPositivo = cellConCheck;
                      //}
                      //else if (ValorVDRL.v_Value1 == "2")
                      //{
                      //    ReaccionNegativo= cellConCheck;
                      //}
                  }
                  else
                  {
                      VDRLValor = "NO REALIZADO";
                  }

              }
              else
              {
                  VDRLValor = "NO APLICA";
              }

            #endregion

            cells = new List<PdfPCell>()
                 {
                     //Linea              

                      new PdfPCell(new Phrase("", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_RIGHT}, 
                       new PdfPCell(new Phrase("", fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                      new PdfPCell(cellPulmones),
                      new PdfPCell(new Phrase("VÉRTICES", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorVertices, fontColumnValue)){ Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                     
                      //Linea            
                      new PdfPCell(new Phrase("CAMPOS PULMONARES", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorCamposPulmonares, fontColumnValue)){Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                           
                      //Linea                     
                      new PdfPCell(new Phrase("HILIOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorHilos, fontColumnValue)){Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 
                       //Linea                
                      new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                      //Linea   
                      new PdfPCell(new Phrase("N° Rx", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      new PdfPCell(new Phrase(ValorNroRx, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      new PdfPCell(new Phrase("SENOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorDiafragmaticos, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      new PdfPCell(new Phrase("MEDIASTINOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorMediastinos, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                     //Linea   
                      new PdfPCell(new Phrase("FECHA", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      new PdfPCell(new Phrase(ValorFechaToma, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      new PdfPCell(new Phrase("CONCLUSIONES RADIOGRÁFICAS", fontColumnValueBold)){Rowspan=2, HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorConclusionesRx, fontColumnValue)){Rowspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                      new PdfPCell(new Phrase("SILUETA CARDIOVASCULAR", fontColumnValueBold)){Rowspan=2,HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorSiluetaCardiaca, fontColumnValue)){Rowspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                        //Linea   
                      new PdfPCell(new Phrase("CALIDAD", fontColumnValueBold)){ HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase(ValorCalidad, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                      //Linea   
                      new PdfPCell(new Phrase("SÍMBOLOS", fontColumnValueBold)){HorizontalAlignment = PdfPCell.ALIGN_RIGHT},
                      new PdfPCell(new Phrase("", fontColumnValue)){Colspan=5,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                 };
            columnWidths = new float[] { 10f,10f,20f,20f,20f,20f };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion

            #region CERO

            #region Dx y Recomendaciones

            cells = new List<PdfPCell>();

            if (diagnosticRepository != null && diagnosticRepository.Count > 0)
            {
                //PdfPCell cellDx = null;
                
                columnWidths = new float[] { 25f };
                include = "v_RecommendationName";

                var ListaFinal = diagnosticRepository.FindAll(p => p.i_FinalQualificationId != 4);
                foreach (var item in ListaFinal)
                {
                    if (item.v_DiseasesId == "N009-DD000000029")
                    {
                        cell = new PdfPCell(new Phrase("")) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        cells.Add(cell);
                    }
                  
                    // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                    var tableDx = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue, 0);
                    cell = new PdfPCell(tableDx);
                    cells.Add(cell);
                }
                columnWidths = new float[] { 18f, 54f };
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                columnWidths = new float[] { 100f };
            }

            var GrillaDx = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, 0, "", fontTitleTableNegro);
            //document.Add(table);

            #endregion

            #region Restriccion

            cells = new List<PdfPCell>();

            var ent = diagnosticRepository.SelectMany(p => p.Restrictions).ToList();
            if (ent != null && ent.Count > 0)
            {
                foreach (var item in ent)
                {
                    //Columna Restricciones
                    cell = new PdfPCell(new Phrase(item.v_RestrictionName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    cells.Add(cell);
                   
                }
                columnWidths = new float[] { 100f};
            }
            else
            {
                cells.Add(new PdfPCell(new Phrase("NINGUNA", fontColumnValue)) { Colspan = 8, HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                columnWidths = new float[] {100f};

            }
            columnHeaders = new string[] { "" };

            var GrillaRestricciones = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "RESTRICCIONES", fontTitleTable, columnHeaders);

            //document.Add(table);

            #endregion

            cells = new List<PdfPCell>()
                 {
                    //Linea
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("REACCIONES SEROLÓGICAS", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                    //Linea
                    new PdfPCell(new Phrase("0/0", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("1/0", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("1/1, 1/2", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("2/1, 2/2, 2/3", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("3/2, 3/3, 3+", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("A,B,C", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("", fontColumnValue)),
                    new PdfPCell(new Phrase("A LÚES", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                    //Linea
                    new PdfPCell(new Phrase("CERO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("1/0", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("UNO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("DOS", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("TRES", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("CUATRO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Rowspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(VDRLValor, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                   
                    //Linea        
                    new PdfPCell(Cero){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(UnoCero){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(Uno){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(Dos){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(Tres){ Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(ABC){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },  
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                  

                    //Linea
                    new PdfPCell(new Phrase("SIN NEUMOCONIOSIS", fontColumnValue)){ Colspan=2,Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("IMAGEN RADIOGRÁFICA DE EXPOSICIÓN A POLVO", fontColumnValue)){Colspan=3,Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("CON NEUMOCONIOSIS", fontColumnValue)){Colspan=3, Border = PdfPCell.LEFT_BORDER,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                       
                    //Linea
                    new PdfPCell(Sin_Neumoconiosis){Colspan=2,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },  
                    new PdfPCell(Con_Hallazgos){Colspan=3,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE },                      
                    new PdfPCell(Con_Neumoconiosis){Colspan=3, Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(new Phrase("", fontColumnValue)){ Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //Linea
                    //Linea   NORMAL
                    // Alejandro
                    new PdfPCell(new Phrase(ConclusionesOITDescripcionSinNeumoconiosis, fontColumnValue)){Colspan=2,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ExposicionPolvoDescripcion, fontColumnValue)){Colspan=3,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                     
                    new PdfPCell(new Phrase(ConclusionesOITDescripcionConNeumoconiosis, fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,Colspan=3,HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase(" ", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_LEFT},


                    //Linea
                    new PdfPCell(new Phrase("GRUPO SANGUÍNEO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, Colspan = 4},
                    //new PdfPCell(new Phrase(" ", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(" ", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase(" ", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("FACTOR RH", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER, Colspan = 2},
                    //new PdfPCell(new Phrase(" ", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("HB/HTO", fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},       
                    new PdfPCell(cellFirmaTrabajador),

                    //Linea
                    new PdfPCell(new Phrase("O", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("A", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("B", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("AB", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("Rh (+)", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("Rh (-)", fontColumnValue)){Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ValorHemoglobina1 + " / " + ValorHematocrito1, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //Linea
                    new PdfPCell(SangreO){Rowspan=2, Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },                      
                    new PdfPCell(SangreA){Rowspan=2,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },                      
                    new PdfPCell(SangreB){Rowspan=2,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },                      
                    new PdfPCell(SangreAB){Rowspan=2,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },   
                    new PdfPCell(rhPositivo){Rowspan=2,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP },   
                    new PdfPCell(rhNegativo){Rowspan=2,Border = PdfPCell.LEFT_BORDER ,HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_TOP }, 
                    new PdfPCell(new Phrase(ValorHemoglobina2, fontColumnValue)){Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //Linea
                    new PdfPCell(new Phrase(" ", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                       
                    //Linea
                    new PdfPCell(new Phrase("APTO PARA TRABAJAR", fontColumnValue)){ Colspan=2,HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase("MÉDICO :" + DataService.NombreDoctor + " COLEGIATURA N° " + DataService.CMP, fontColumnValue)){ Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                       
                    //Linea
                    new PdfPCell(new Phrase("APTO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(Apto){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    new PdfPCell(cellFirmaDoctor),

                    //Linea
                    new PdfPCell(new Phrase("NO APTO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(NoApto){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 

                     //Linea
                    new PdfPCell(new Phrase("CON RESTRICCIÓN", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(AptoConRestricciones){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 
                    
                    //  //Linea
                    //new PdfPCell(new Phrase("OBSERVADO", fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(AptoObs){HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment=PdfPCell.ALIGN_MIDDLE }, 

                    //Linea
                     new PdfPCell(new Phrase("HALLAZGOS", fontColumnValue)){Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                     new PdfPCell(new Phrase("RECOMENDACIONES", fontColumnValue)){ Colspan=6, HorizontalAlignment = PdfPCell.ALIGN_CENTER},                  
                     new PdfPCell(new Phrase("", fontColumnValue)){ Colspan=2,  HorizontalAlignment = PdfPCell.ALIGN_CENTER},

                    //Linea
                    new PdfPCell(GrillaDx){Rowspan=5, Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(cellHuellaTrabajador){ FixedHeight = 55F},
                                                
                    // //Linea
                    new PdfPCell(new Phrase("HUELLA DIGITAL ÍNDICE DERECHO", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                    //Linea
                    new PdfPCell(GrillaRestricciones){ Colspan=8, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                    new PdfPCell(new Phrase("DECLARO QUE TODA LA INFORMACIÓN ES VERDADERA", fontColumnValue)){ Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},

                 };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, };

            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

            document.Add(filiationWorker);

            #endregion
    
            document.Close();

            //RunFile(filePDF);
        }

        #endregion

        #region Examen Clínico

        public static void CreateMedicalReportForExamenClinico(PacientList filiationData, List<PersonMedicalHistoryList> personMedicalHistory, List<NoxiousHabitsList> noxiousHabit, List<FamilyMedicalAntecedentsList> familyMedicalAntecedent, ServiceList anamnesis, List<ServiceComponentList> serviceComponent, List<DiagnosticRepositoryList> diagnosticRepository, string customerOrganizationName, organizationDto infoEmpresaPropietaria, string filePDF, ServiceList doctoPhisicalExam)
        {
            // step 1: creation of a document-object
            Document document = new Document();
            //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

            try
            {
                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

                //create an instance of your PDFpage class. This is the class we generated above.
                pdfPage page = new pdfPage();
                page.Dato = "IC/" + filiationData.v_FirstName + " " + filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName;
                //set the PageEvent of the pdfWriter instance to the instance of our PDFPage class
                writer.PageEvent = page;

                // step 3: we open the document
                document.Open();
                // step 4: we Add content to the document
                // we define some fonts

                #region Fonts

                Font fontTitle1 = FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));


                #endregion

                #region Logo

                Image logo = HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, 30F);
                PdfPTable headerTbl = new PdfPTable(1);
                headerTbl.TotalWidth = writer.PageSize.Width;
                PdfPCell cellLogo = new PdfPCell(logo);

                cellLogo.VerticalAlignment = Element.ALIGN_TOP;
                cellLogo.HorizontalAlignment = Element.ALIGN_CENTER;

                cellLogo.Border = PdfPCell.NO_BORDER;
                headerTbl.AddCell(cellLogo);
                document.Add(headerTbl);

                #endregion

                #region Title

                Paragraph cTitle = new Paragraph("Examen Clínico", fontTitle1);
                Paragraph cTitle2 = new Paragraph("Historia Clínica: " + anamnesis.v_ServiceId, fontTitle2);
                cTitle.Alignment = Element.ALIGN_CENTER;
                cTitle2.Alignment = Element.ALIGN_CENTER;

                document.Add(cTitle);
                document.Add(cTitle2);

                #endregion

                #region Declaration Tables
                var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.White);
                string include = string.Empty;
                List<PdfPCell> cells = null;
                float[] columnWidths = null;
                string[] columnValues = null;
                string[] columnHeaders = null;

                //PdfPTable header1 = new PdfPTable(2);
                //header1.HorizontalAlignment = Element.ALIGN_CENTER;
                //header1.WidthPercentage = 100;
                ////header1.TotalWidth = 500;
                ////header1.LockedWidth = true;    // Esto funciona con TotalWidth
                //float[] widths = new float[] { 150f, 200f};
                //header1.SetWidths(widths);


                //Rectangle rec = document.PageSize;
                PdfPTable header2 = new PdfPTable(6);
                header2.HorizontalAlignment = Element.ALIGN_CENTER;
                header2.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                header2.SetWidths(widths1);
                //header2.SetWidthPercentage(widths1, rec);

                PdfPTable companyData = new PdfPTable(6);
                companyData.HorizontalAlignment = Element.ALIGN_CENTER;
                companyData.WidthPercentage = 100;
                //header1.TotalWidth = 500;
                //header1.LockedWidth = true;    // Esto funciona con TotalWidth
                float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
                companyData.SetWidths(widthscolumnsCompanyData);

                PdfPTable filiationWorker = new PdfPTable(4);

                PdfPTable table = null;

                PdfPCell cell = null;

                #endregion

                // Salto de linea
                document.Add(new Paragraph("\r\n"));

                #region Filiación del trabajador

                PdfPCell cellPhoto = null;

                if (filiationData.b_Photo != null)
                    cellPhoto = new PdfPCell(HandlingItextSharp.GetImage(filiationData.b_Photo, 15F));
                else
                    cellPhoto = new PdfPCell(new Phrase("Sin Foto", fontColumnValue));

                cellPhoto.Rowspan = 5;
                cellPhoto.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cellPhoto.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("Nombres: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("Apellidos: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FirstLastName + " " + filiationData.v_SecondLastName , fontColumnValue)),                   
                    new PdfPCell(new Phrase("Foto:", fontColumnValue)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT }, cellPhoto,
                                                                                        
                    new PdfPCell(new Phrase("Edad: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.i_Age.ToString(), fontColumnValue)),                   
                    new PdfPCell(new Phrase("Seguro: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_TypeOfInsuranceName, fontColumnValue)),                   
                    new PdfPCell(new Phrase(" ", fontColumnValue)), 

                    new PdfPCell(new Phrase("Empresa: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_FullWorkingOrganizationName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("Centro Médico: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.v_OwnerOrganizationName, fontColumnValue)),                   
                    new PdfPCell(new Phrase(" ", fontColumnValue)),      
                                         
                    new PdfPCell(new Phrase("Médico: ", fontColumnValue)), new PdfPCell(new Phrase("Dr(a)." + filiationData.v_DoctorPhysicalExamName, fontColumnValue)),                   
                    new PdfPCell(new Phrase("Fecha Atención: ", fontColumnValue)), new PdfPCell(new Phrase(filiationData.d_ServiceDate.Value.ToShortDateString(), fontColumnValue)),                   
                    new PdfPCell(new Phrase(" ", fontColumnValue)),                  
                                                   
                };

                columnWidths = new float[] { 20.6f, 40.6f, 16.6f, 34.6f, 6.6f, 14.6f };

                filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, PdfPCell.NO_BORDER, "I. DATOS DE FILIACIÓN", fontTitleTableNegro, null);

                document.Add(filiationWorker);

                #endregion

                #region Antecedentes Medicos Personales

                if (personMedicalHistory.Count == 0)
                {
                    personMedicalHistory.Add(new PersonMedicalHistoryList { v_DiseasesName = "No Refiere Antecedentes Médico Personales." });
                    columnWidths = new float[] { 100f };
                    include = "v_DiseasesName";
                }
                else
                {
                    columnWidths = new float[] { 3f, 97f };
                    include = "i_Item,v_DiseasesName";
                }

                var antMedicoPer = HandlingItextSharp.GenerateTableFromList(personMedicalHistory, columnWidths, include, fontColumnValue, "II. ANTECEDENTES MÉDICO PERSONALES", fontTitleTableNegro);

                document.Add(antMedicoPer);

                #endregion

                #region Habitos Nocivos

                cells = new List<PdfPCell>();                                           

                if (noxiousHabit.Count == 0)
                {                  
                    cells.Add(new PdfPCell(new Phrase("No Aplica Hábitos Nocivos a la Atención.")));
                    columnWidths = new float[] { 100f };
                }
                else
                {
                    foreach (var habito in noxiousHabit)
                    {
                        cells.Add(new PdfPCell(new Phrase(habito.v_NoxiousHabitsName + ": " + habito.v_Frequency, fontColumnValue))); 
                    }

                    columnWidths = new float[noxiousHabit.Count];

                    for (int i = 0; i < noxiousHabit.Count; i++)
                    {                      
                        columnWidths[i] = 25;
                    }                
                }
            
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "III. HÁBITOS NOCIVOS", fontTitleTableNegro);
                document.Add(table);

                #endregion

                #region Antecedentes Patológicos Familiares

                if (familyMedicalAntecedent.Count == 0)
                {
                    familyMedicalAntecedent.Add(new FamilyMedicalAntecedentsList { v_FullAntecedentName = "No Refiere Antecedentes Patológicos Familiares." });
                    columnWidths = new float[] { 100f };
                    include = "v_FullAntecedentName";
                }
                else
                {
                    columnWidths = new float[] { 0.7f, 23.6f };
                    include = "i_Item,v_FullAntecedentName";
                }

                var pathologicalFamilyHistory = HandlingItextSharp.GenerateTableFromList(familyMedicalAntecedent, columnWidths, include, fontColumnValue, "IV. ANTECEDENTES PATOLÓGICOS FAMILIARES", fontTitleTableNegro);

                document.Add(pathologicalFamilyHistory);

                #endregion

                #region Evaluación Médica

                #region Anamnesis

                var rpta = anamnesis.i_HasSymptomId == null || anamnesis.i_HasSymptomId == 0 ? "No" : "Si";
                var sinto = anamnesis.v_MainSymptom == null ? "NO REFIERE" : anamnesis.v_MainSymptom + " / " + anamnesis.i_TimeOfDisease + "días";
                var relato = anamnesis.v_Story == null ? "PACIENTE ASINTOMÁTICO" : anamnesis.v_Story;

                cells = new List<PdfPCell>()
                {
                    new PdfPCell(new Phrase("ANAMNESIS: ", fontSubTitleNegroNegrita)) { Colspan = 3 ,
                                                                            //BackgroundColor = new BaseColor(System.Drawing.Color.Gray),
                                                                            HorizontalAlignment = Element.ALIGN_LEFT }, 
                    new PdfPCell(new Phrase("¿PRESENTA SÍNTOMAS?: " + rpta, fontColumnValue)),                   
                    new PdfPCell(new Phrase("SÍNTOMAS PRINCIPALES: " + sinto, fontColumnValue)),                               
                    new PdfPCell(new Phrase("RELATO: " + relato, fontColumnValue)) { Colspan = 2 },                        
                                                   
                };

                columnWidths = new float[] { 20f, 30f, 50f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "V. EVALUACIÓN MÉDICA", fontTitleTableNegro);

                document.Add(table);

                #endregion

                string[] orderPrint = new string[]
                { 
                    Sigesoft.Common.Constants.ANTROPOMETRIA_ID, 
                    Sigesoft.Common.Constants.FUNCIONES_VITALES_ID,
                };

                ReportBuilderReportForExamenClinico(serviceComponent, orderPrint, fontTitleTable, fontSubTitleNegroNegrita, fontColumnValue, subTitleBackGroundColor, document);

                cells = new List<PdfPCell>();

                // Subtitulo  ******************
                cell = new PdfPCell(new Phrase("FUNCIONES BIOLÓGICAS: ", fontSubTitleNegroNegrita))
                {
                   
                    BackgroundColor = subTitleBackGroundColor,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                };

                cells.Add(cell);
                //*****************************************

                var funcionesBiologicas = string.IsNullOrEmpty(anamnesis.v_Findings) ? "Sin Alteración" : anamnesis.v_Findings;
                     
                cells.Add(new PdfPCell(new Phrase(funcionesBiologicas, fontColumnValue)));
                 
                columnWidths = new float[] { 100f };
              
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);
                document.Add(table);

                #region Examen fisico

                var examenFisico = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID);

                string Estoscopia = "", Estado_Mental = "", PielX = "", Piel = "", CabelloX = "", Cabello = "", OjoAnexoX = "",
                Hallazgos = "", AgudezaVisualOjoDerechoSC = "", AgudezaVisualOjoIzquierdoSC = "", AgudezaVisualOjoDerechoCC = "",
                AgudezaVisualOjoIzquierdoCC = "", TiempoEstereopsis = "", VisonProfundidad = "", VisonColores = "",
                OjoAnexo = "", OidoX = "", Oido = "", NarizX = "", Nariz = "", BocaX = "", Boca = "", FaringeX = "",
                Faringe = "", CuelloX = "", Cuello = "", ApaRespiratorioX = "", ApaRespiratorio = "", ApaCardioVascularX = "",
                ApaCardioVascular = "", ApaDigestivoX = "", ApaDigestivo = "", ApaGenitoUrinarioX = "", ApaGenitoUrinario = "",
                ApaLocomotorX = "", ApaLocomotor = "",MarchaX = "", Marcha = "", ColumnaX = "", Columna = "", SuperioresX = "",
                Superiores = "", InferioresX = "", Inferiores = "", SistemaLinfaticoX = "",SistemaLinfatico = "",
                SistemaNerviosoX = "", SistemaNervioso = "";

                if (examenFisico != null)
                {
                    #region logica de variables

                    if (examenFisico.ServiceComponentFields.Count > 0)
                    {                     
                        Estoscopia = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID).v_Value1;

                        Estado_Mental = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_ESTADO_METAL_DESCRIPCION_ID).v_Value1;

                        Piel = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).v_Value1;
                        string PielHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_PIEL_ID).v_Value1;
                        if (PielHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) PielX = "X";

                        Cabello = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).v_Value1;
                        string CabelloHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CABELLO_ID).v_Value1;
                        if (CabelloHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) CabelloX = "X";

                        Oido = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).v_Value1;
                        string OidoHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OIDOS_ID).v_Value1;
                        if (OidoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) OidoX = "X";

                        Nariz = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).v_Value1;
                        string NarizHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_NARIZ_ID).v_Value1;
                        if (NarizHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) NarizX = "X";

                        Boca = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).v_Value1;
                        string BocaHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_BOCA_ID).v_Value1;
                        if (BocaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) BocaX = "X";

                        Faringe = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID).v_Value1;
                        string FaringeHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_FARINGE_ID).v_Value1;
                        if (FaringeHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) FaringeX = "X";

                        Cuello = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).v_Value1;
                        string CuelloHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CUELLO_ID).v_Value1;
                        if (CuelloHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) CuelloX = "X";

                        ApaRespiratorio = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).v_Value1;
                        string ApaRespiratorioHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID).v_Value1;
                        if (ApaRespiratorioHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaRespiratorioX = "X";

                        ApaCardioVascular = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).v_Value1;
                        string ApaCardioVascularHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID).v_Value1;
                        if (ApaCardioVascularHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaCardioVascularX = "X";

                        ApaDigestivo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).v_Value1;
                        string ApaDigestivoHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID).v_Value1;
                        if (ApaDigestivoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaDigestivoX = "X";

                        ApaGenitoUrinario = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).v_Value1;
                        string ApaGenitoUrinarioHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_GENITOURINARIO_ID).v_Value1;
                        if (ApaGenitoUrinarioHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaGenitoUrinarioX = "X";

                    }

                    // Alejandro
                    // si es mujer el trabajador mostrar sus antecedentes

                    int? sex = anamnesis.i_SexTypeId;

                    if (sex == (int?)Sigesoft.Common.Gender.FEMENINO)
                    {
                        ApaGenitoUrinario = string.Format("Menarquia: {0} ," +
                                                           "FUM: {1} ," +
                                                           "Régimen Catamenial: {2} ," +
                                                           "Gestación y Paridad: {3} ," +
                                                           "MAC: {4} ," +
                                                           "Cirugía Ginecológica: {5}", string.IsNullOrEmpty(anamnesis.v_Menarquia) ? "No refiere" : anamnesis.v_Menarquia,
                                                                                        anamnesis.d_Fur == null ? "No refiere" : anamnesis.d_Fur.Value.ToShortDateString(),
                                                                                        string.IsNullOrEmpty(anamnesis.v_CatemenialRegime) ? "No refiere" : anamnesis.v_CatemenialRegime,
                                                                                        anamnesis.v_Gestapara,
                                                                                        anamnesis.v_Mac,
                                                                                        string.IsNullOrEmpty(anamnesis.v_CiruGine) ? "No refiere" : anamnesis.v_CiruGine);



                        ApaLocomotor = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).v_Value1;
                        string ApaLocomotorHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID).v_Value1;
                        if (ApaLocomotorHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ApaLocomotorX = "X";

                        Marcha = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).v_Value1;
                        string MarchaHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_MARCHA_ID).v_Value1;
                        if (MarchaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) MarchaX = "X";

                        Columna = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).v_Value1;
                        string ColumnaHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_COLMNA_ID).v_Value1;
                        if (ColumnaHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) ColumnaX = "X";

                        Superiores = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).v_Value1;
                        string SuperioresHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID).v_Value1;
                        if (SuperioresHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SuperioresX = "X";

                        Inferiores = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).v_Value1;
                        string InferioresHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID).v_Value1;
                        if (InferioresHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) InferioresX = "X";

                        SistemaLinfatico = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).v_Value1;
                        string SistemaLinfaticoHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_LINFATICOS_ID).v_Value1;
                        if (SistemaLinfaticoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SistemaLinfaticoX = "X";

                        SistemaNervioso = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).v_Value1;
                        string SistemaNerviosoHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID).v_Value1;
                        if (SistemaNerviosoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) SistemaNerviosoX = "X";

                        OjoAnexo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).v_Value1;
                        string OjoAnexoHallazgo = examenFisico.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXAMEN_FISICO_OJOSANEXOS_ID).v_Value1;
                        if (OjoAnexoHallazgo == ((int)Sigesoft.Common.NormalAlteradoHallazgo.SinHallazgos).ToString()) OjoAnexoX = "X";

                    }

                    var Oftalmologia = serviceComponent.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);

                    if (Oftalmologia.ServiceComponentFields.Count > 0)
                    {
                        Hallazgos = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_HALLAZGOS_ID).v_Value1;
                        AgudezaVisualOjoDerechoSC = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO).v_Value1;
                        AgudezaVisualOjoIzquierdoSC = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO).v_Value1;

                        //var ff = Oftalmologia.Find(p => p.v_Value1 == "20 / 30");
                        AgudezaVisualOjoDerechoCC = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO).v_Value1;
                        AgudezaVisualOjoIzquierdoCC = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_IZQUIERDO).v_Value1;

                        //TEST DE ESTEREOPSIS:Frec. 10 seg/arc, Normal.
                        string TestEstereopsisNormal = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_NORMAL_ID).v_Value1;
                        string TestEstereopsisAnormal = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_ANORMAL_ID).v_Value1;
                        TiempoEstereopsis = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_TIEMPO_ID).v_Value1;

                        if (TestEstereopsisNormal == "1")
                        {
                            VisonProfundidad = "normal";
                        }
                        else if (TestEstereopsisAnormal == "1")
                        {
                            VisonProfundidad = "Anormal";
                        }

                        //TEST DE ISHIHARA: Anormal, Discromatopsia: No definida.
                        string TestIshiharaNormal = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_NORMAL).v_Value1;
                        string TestIshiharaAnormal = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.TEST_ISHIHARA_ANORMAL).v_Value1;
                        string Dicromatopsia = Oftalmologia.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.OFTALMOLOGIA_DICROMATOPSIA_ID).v_Value1;

                        if (TestIshiharaNormal == "1")
                        {
                            VisonColores = "Normal";
                        }
                        else if (TestIshiharaAnormal == "1")
                        {
                            string combo = "";
                            if (Dicromatopsia == "11")
                            {
                                combo = "Rojo";
                            }
                            else if (Dicromatopsia == "12")
                            {
                                combo = "Verde";
                            }
                            else if (Dicromatopsia == "14")
                            {
                                combo = "Azul - Amarillo";
                            }
                            else if (Dicromatopsia == "15")
                            {
                                combo = "No Definida";
                            }
                            else if (Dicromatopsia == "16")
                            {
                                combo = "No Presenta";
                            }
                            VisonColores = " Anormal" + " Dicromatopsia: " + combo;
                        }

                    }

                    #endregion

                }
               
                #region TABLA Examen fisico
		 
	         
                cells = new List<PdfPCell>()
               {
                //fila                                                
                    new PdfPCell(new Phrase("Examen Físico", fontSubTitleNegroNegrita))
                                      { Colspan=12, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila    
                    new PdfPCell(new Phrase("Estoscopia", fontColumnValue)),                        
                    new PdfPCell(new Phrase(Estoscopia, fontColumnValue))
                                      { Colspan=11, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila                                                
                     new PdfPCell(new Phrase("Estado Mental", fontColumnValue)),                        
                    new PdfPCell(new Phrase(Estado_Mental, fontColumnValue))
                                      { Colspan=11, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 


                    //fila                                                
                    // new PdfPCell(new Phrase("Organo o Sistema", fontColumnValue)),                        
                    //new PdfPCell(new Phrase("Sin Hallazgos", fontColumnValue)),
                    //new PdfPCell(new Phrase("Hallazgos", fontColumnValue))
                    //                  { Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila                                                
                     new PdfPCell(new Phrase("Piel", fontColumnValue)),                        
                    new PdfPCell(new Phrase(PielX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Piel, fontColumnValue))
                                      { Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila                                                
                     new PdfPCell(new Phrase("Cabellos", fontColumnValue)),                        
                    new PdfPCell(new Phrase(CabelloX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Cabello, fontColumnValue))
                                      { Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                         //fila                                                
                     new PdfPCell(new Phrase("Ojos y Anexos", fontColumnValue))
                                { Rowspan=0, HorizontalAlignment = PdfPCell.ALIGN_LEFT},   
                    new PdfPCell(new Phrase(OjoAnexoX, fontColumnValue))
                                { Rowspan=0, HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    //new PdfPCell(new Phrase("Hallazgos", fontColumnValue))
                    //            { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                    //new PdfPCell(new Phrase(Hallazgos, fontColumnValue))
                    //                  { Colspan=9, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila
                
                     //new PdfPCell(new Phrase("Agudeza Visual", fontColumnValue))
                     //           {  HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                     // new PdfPCell(new Phrase("OD", fontColumnValue)),
                     //   new PdfPCell(new Phrase(AgudezaVisualOjoDerechoSC, fontColumnValue)),
                     //   new PdfPCell(new Phrase("OI", fontColumnValue)),
                     //   new PdfPCell(new Phrase(AgudezaVisualOjoIzquierdoSC, fontColumnValue)),
                     //   new PdfPCell(new Phrase("Con Correctores", fontColumnValue)),
                     //   new PdfPCell(new Phrase("OD", fontColumnValue)),
                     //   new PdfPCell(new Phrase(AgudezaVisualOjoDerechoCC, fontColumnValue)),
                     //   new PdfPCell(new Phrase("OI", fontColumnValue)), 
                     //   new PdfPCell(new Phrase(AgudezaVisualOjoIzquierdoCC, fontColumnValue)),

                     //   //fila
                     //   //new PdfPCell(new Phrase("", fontColumnValue)),
                     //   new PdfPCell(new Phrase("Visión de Profundidad", fontColumnValue))
                     //               { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                     // new PdfPCell(new Phrase("Test de Estereopsis: Frec. " + TiempoEstereopsis + "seg/arc, " + VisonProfundidad, fontColumnValue))
                     //               { Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 
                     //   new PdfPCell(new Phrase("Visión de Colores", fontColumnValue))
                     //                { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                     //   new PdfPCell(new Phrase("Test de ISHIHARA: " +VisonColores, fontColumnValue))
                     //               { Colspan=3, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila
                         //new PdfPCell(new Phrase("", fontColumnValue)),
                          //new PdfPCell(new Phrase("Examen Clínico", fontColumnValue))
                          //           { Colspan=2, HorizontalAlignment = PdfPCell.ALIGN_LEFT},
                           new PdfPCell(new Phrase(OjoAnexo, fontColumnValue))
                                        { Colspan=10, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                    //fila                                                
                     new PdfPCell(new Phrase("Oido", fontColumnValue)),                        
                    new PdfPCell(new Phrase(OidoX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Oido, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},            
                     new PdfPCell(new Phrase("Nariz", fontColumnValue)),                        
                    new PdfPCell(new Phrase(NarizX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Nariz, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                         //fila                                                
                     new PdfPCell(new Phrase("Boca", fontColumnValue)),                        
                    new PdfPCell(new Phrase(BocaX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Boca, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                                
                     new PdfPCell(new Phrase("Faringe", fontColumnValue)),                        
                    new PdfPCell(new Phrase(FaringeX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Faringe, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                         //fila                                                
                     new PdfPCell(new Phrase("Cuello", fontColumnValue)),                        
                    new PdfPCell(new Phrase(CuelloX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Cuello, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                                
                     new PdfPCell(new Phrase("Aparato Respiratorio", fontColumnValue)),                        
                    new PdfPCell(new Phrase(ApaRespiratorioX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaRespiratorio, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                         //fila                                                
                     new PdfPCell(new Phrase("Aparato Cardiovascular", fontColumnValue)),                        
                    new PdfPCell(new Phrase(ApaCardioVascularX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaCardioVascular, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                              
                     new PdfPCell(new Phrase("Aparato Digestivo", fontColumnValue)),                        
                    new PdfPCell(new Phrase(ApaDigestivoX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaDigestivo, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                       //fila                                                
                     new PdfPCell(new Phrase("Aparato Genitourinario", fontColumnValue)),                        
                    new PdfPCell(new Phrase(ApaGenitoUrinarioX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaGenitoUrinario, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                                
                     new PdfPCell(new Phrase("Aparato Locomotor", fontColumnValue)),                        
                    new PdfPCell(new Phrase(ApaLocomotorX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(ApaLocomotor, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                       //fila                                                
                     new PdfPCell(new Phrase("Marcha", fontColumnValue)),                        
                    new PdfPCell(new Phrase(MarchaX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Marcha, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                               
                     new PdfPCell(new Phrase("Columna", fontColumnValue)),                        
                    new PdfPCell(new Phrase(ColumnaX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Columna, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                       //fila                                                
                     new PdfPCell(new Phrase("Miembros Superiores", fontColumnValue)),                        
                    new PdfPCell(new Phrase(SuperioresX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Superiores, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                                
                     new PdfPCell(new Phrase("Miembros Inferiores", fontColumnValue)),                        
                    new PdfPCell(new Phrase(InferioresX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(Inferiores, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 

                                       //fila                                                
                     new PdfPCell(new Phrase("Sistema Linfático", fontColumnValue)),                        
                    new PdfPCell(new Phrase(SistemaLinfaticoX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(SistemaLinfatico, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT},                                                
                     new PdfPCell(new Phrase("Sistema Nervioso", fontColumnValue)),                        
                    new PdfPCell(new Phrase(SistemaNerviosoX, fontColumnValue)){HorizontalAlignment = PdfPCell.ALIGN_CENTER},
                    new PdfPCell(new Phrase(SistemaNervioso, fontColumnValue))
                                      { Colspan=4, HorizontalAlignment = PdfPCell.ALIGN_LEFT}, 





               };

                #endregion

                columnWidths = new float[] { 14f, 9f, 9f, 9f, 9f, 9f, 11f, 12f, 9f, 10f, 13f, 7f };

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                document.Add(table);

                #endregion

                #endregion

                #region Hallazgos y recomendaciones

                cells = new List<PdfPCell>();

                var filterDiagnosticRepository = diagnosticRepository.FindAll(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_ID);

                if (filterDiagnosticRepository != null && filterDiagnosticRepository.Count > 0)
                {
                    columnWidths = new float[] { 0.7f, 23.6f };
                    include = "i_Item,v_RecommendationName";

                    foreach (var item in filterDiagnosticRepository)
                    {
                        if (item.v_DiseasesId == "N009-DD000000029")
                        {
                            cell = new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            cells.Add(cell);
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(item.v_DiseasesName, fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            cells.Add(cell);
                        }
                    
                        // Crear tabla de recomendaciones para insertarla en la celda que corresponde
                        table = HandlingItextSharp.GenerateTableFromList(item.Recomendations, columnWidths, include, fontColumnValue);
                        cell = new PdfPCell(table);
                        cells.Add(cell);
                    }

                    columnWidths = new float[] { 20.6f, 40.6f };
                }
                else
                {
                    cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                    columnWidths = new float[] { 100 };
                }

                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "VI. HALLAZGOS Y RECOMENDACIONES", fontTitleTableNegro);

                document.Add(table);

                #endregion          

                #region Firma

                #region Creando celdas de tipo Imagen y validando nulls

                // Firma del trabajador ***************************************************
                PdfPCell cellFirmaTrabajador = null;

                if (filiationData.FirmaTrabajador != null)
                    cellFirmaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.FirmaTrabajador, 40F));
                else
                    cellFirmaTrabajador = new PdfPCell(new Phrase("Sin Firma Trabajador", fontColumnValue));

                // Huella del trabajador **************************************************
                PdfPCell cellHuellaTrabajador = null;

                if (filiationData.HuellaTrabajador != null)
                    cellHuellaTrabajador = new PdfPCell(HandlingItextSharp.GetImage(filiationData.HuellaTrabajador, 40F));
                else
                    cellHuellaTrabajador = new PdfPCell(new Phrase("Sin Huella", fontColumnValue));

                // Firma del doctor EXAMEN FISICO **************************************************

                PdfPCell cellFirma = null;

                if (doctoPhisicalExam.FirmaDoctor != null)
                    cellFirma = new PdfPCell(HandlingItextSharp.GetImage(doctoPhisicalExam.FirmaDoctor, 30F));
                else
                    cellFirma = new PdfPCell(new Phrase("Sin Firma", fontColumnValue));

                #endregion

                #region Crear tablas en duro (para la Firma y huella del trabajador)

                cells = new List<PdfPCell>();

                cellFirmaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirmaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirmaTrabajador.FixedHeight = 80F;
                cells.Add(cellFirmaTrabajador);
                cells.Add(new PdfPCell(new Phrase("Firma del Examinado", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                columnWidths = new float[] { 100f };

                var tableFirmaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                //***********************************************

                cells = new List<PdfPCell>();

                cellHuellaTrabajador.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHuellaTrabajador.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellHuellaTrabajador.FixedHeight = 80F;
                cells.Add(cellHuellaTrabajador);
                cells.Add(new PdfPCell(new Phrase("Huella del Examinado", fontColumnValue)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                columnWidths = new float[] { 100f };

                var tableHuellaTrabajador = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, "", fontTitleTable);

                #endregion

                cells = new List<PdfPCell>();

                // 1 celda vacia              
                cells.Add(new PdfPCell(tableFirmaTrabajador));

                // 1 celda vacia
                cells.Add(new PdfPCell(tableHuellaTrabajador));

                // 2 celda
                cell = new PdfPCell(new Phrase("Firma y Sello Médico", fontColumnValue));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cells.Add(cell);

                // 3 celda (Imagen)
                cellFirma.HorizontalAlignment = Element.ALIGN_CENTER;
                cellFirma.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellFirma.FixedHeight = 30F;
                cells.Add(cellFirma);

                columnWidths = new float[] { 25f, 25f, 20f, 30F };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, " ", fontTitleTable);

                document.Add(table);

                #endregion
            
                // step 5: we close the document
                document.Close();
                writer.Close();
                writer.Dispose();
                //RunFile(filePDF);

            }
            catch (DocumentException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }

        }

        private static void ReportBuilderReportForExamenClinico(List<ServiceComponentList> serviceComponent, string[] order, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor, Document document)
        {
            if (order != null)
            {
                var sortEntity = GetSortEntity(order, serviceComponent);

                if (sortEntity != null)
                {
                    foreach (var ent in sortEntity)
                    {
                        var table = TableBuilderReportForExamenClinico(ent, fontTitle, fontSubTitle, fontColumnValue, SubtitleBackgroundColor);

                        if (table != null)
                            document.Add(table);
                    }
                }
            }
        }

        private static PdfPTable TableBuilderReportForExamenClinico(ServiceComponentList serviceComponent, Font fontTitle, Font fontSubTitle, Font fontColumnValue, BaseColor SubtitleBackgroundColor)
        {
            PdfPTable table = null;
            List<PdfPCell> cells = null;
            PdfPCell cell = null;
            float[] columnWidths = null;

            switch (serviceComponent.v_ComponentId)
            {
                case Sigesoft.Common.Constants.ANTROPOMETRIA_ID:

                    #region ANTROPOMETRIA

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 4,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var talla = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_TALLA_ID);
                        var peso = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PESO_ID);
                        var imc = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_IMC_ID);
                        var periCintura = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID);

                        cells.Add(new PdfPCell(new Phrase("TALLA: " + talla.v_Value1 + " " + talla.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("PESO: " + peso.v_Value1 + " " + peso.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("IMC: " + imc.v_Value1 + " " + imc.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("PERÍMETRO ABDOMINAL: " + periCintura.v_Value1 + " " + periCintura.v_MeasurementUnitName, fontColumnValue)));

                        columnWidths = new float[] { 20f, 25f, 25f, 25f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("NO SE HAN REGISTRADO DATOS.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                case Sigesoft.Common.Constants.FUNCIONES_VITALES_ID:

                    #region FUNCIONES VITALES

                    cells = new List<PdfPCell>();

                    // Subtitulo  ******************
                    cell = new PdfPCell(new Phrase(serviceComponent.v_ComponentName + ": ", fontSubTitle))
                    {
                        Colspan = 5,
                        BackgroundColor = SubtitleBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                    };

                    cells.Add(cell);
                    //*****************************************

                    if (serviceComponent.ServiceComponentFields.Count > 0)
                    {
                        var pas = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAS_ID);
                        var pad = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_PAD_ID);
                        var fecCardiaca = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID);
                        var fecRespiratoria = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID);
                        var satO2 = serviceComponent.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.FUNCIONES_VITALES_SAT_O2_ID);

                        cells.Add(new PdfPCell(new Phrase("P.A.S: " + pas.v_Value1 + " " + pas.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("P.A.D: " + pad.v_Value1 + " " + pad.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("Frecuencia Cardíaca: " + fecCardiaca.v_Value1 + " " + fecCardiaca.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("Frecuencia Respiratoria: " + fecRespiratoria.v_Value1 + " " + fecRespiratoria.v_MeasurementUnitName, fontColumnValue)));
                        cells.Add(new PdfPCell(new Phrase("Sat. O2: " + satO2.v_Value1 + " " + satO2.v_MeasurementUnitName, fontColumnValue)));

                        columnWidths = new float[] { 10f, 10f, 20f, 20f, 10f };
                    }
                    else
                    {
                        cells.Add(new PdfPCell(new Phrase("No se han registrado datos.", fontColumnValue)));
                        columnWidths = new float[] { 100f };
                    }

                    table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths);

                    #endregion

                    break;
                default:
                    break;
            }

            return table;

        }

        #endregion

        #region Utils

        private static List<ServiceComponentList> GetSortEntity(string[] order, List<ServiceComponentList> serviceComponent)
        {
            List<ServiceComponentList> orderEntities = new List<ServiceComponentList>();

            foreach (var op in order)
            {
                var find = serviceComponent.Find(P => P.v_ComponentId == op);

                if (find != null)
                {
                    orderEntities.Add(find);

                    // Eliminar 
                    serviceComponent.Remove(find);
                }
            }

            // Unir la entidades

            orderEntities.AddRange(serviceComponent);

            return orderEntities;
        }

        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
         
        }

        #endregion

        #region Informe Médico Ocupacional


        public static void CreateInformeMedicoOcupacional(PacientList filiationData,string filePDF)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));
                pdfPage page = new pdfPage();
                writer.PageEvent = page;
                document.Open();

                #region Fonts
                Font fontTitle1 = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                #endregion


                document.Close();

                RunFile(filePDF);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion

    }
}
