using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.Services.ExaminationServices;
using Tools.Services.ToolServices;

namespace Tools.Services.DocumentServices
{
    public class DocumentService : IDocumentService
    {
        private readonly IToolService _toolService;
        private readonly IExaminationService _examinationService;

        public DocumentService(IToolService toolService,
            IExaminationService examinationService)
        {
            _toolService = toolService;
            _examinationService = examinationService;
        }

        public void PrintCertifications(IList<ExaminationEntity> examinations)
        {
            Application application = new Application();
            Document word = application.Documents.Add();
            word.PageSetup.PageWidth = application.InchesToPoints(12f);

            var para1 = word.Content.Paragraphs.Add();
            para1.Range.Bold = 1;
            para1.Range.Font.Name = "Times New Roman";
            para1.Range.Font.Size = 11;
            para1.Range.Text = "Картка обліку технічних (експертних) оглядів обладнання.";
            para1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            para1.Range.InsertParagraphAfter();

            Paragraph para2 = word.Content.Paragraphs.Add(para1.Range);
            para2.Range.Font.Name = "Times New Roman";
            para2.Range.Font.Size = 11;
            para2.Range.Text = $"(Станом на {DateTime.Now.ToString("yyyy.MM.dd")})\n";
            para2.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            para1.Range.InsertParagraphAfter();

            Paragraph para3 = word.Content.Paragraphs.Add(para2.Range);
            para3.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            para3.Range.InsertParagraphAfter();

            Table header = word.Content.Tables.Add(para3.Range, 7, 2);
            header.Range.Font.Name = "Times New Roman";
            header.Range.Font.Size = 11;
            header.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            header.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPercent;
            header.PreferredWidth = 75f;

            Cell cell = header.Cell(1, 1);
            cell.Range.Text = "Назва обладнання";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;
            cell = header.Cell(1, 2);
            cell.Range.Text = examinations.First().Tool.Brand;
            cell.Borders.Enable = 1;

            cell = header.Cell(2, 1);
            cell.Range.Text = "Марка обладнання";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;
            cell = header.Cell(2, 2);
            cell.Range.Text = examinations.First().Tool.Brand;
            cell.Borders.Enable = 1;

            cell = header.Cell(3, 1);
            cell.Range.Text = "Підгрупа обладнання";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;
            cell = header.Cell(3, 2);
            cell.Range.Text = examinations.First().Tool.Subgroup.Name;
            cell.Borders.Enable = 1;

            cell = header.Cell(4, 1);
            cell.Range.Text = "Структурний підрозділ";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;
            cell = header.Cell(4, 2);
            cell.Range.Text = examinations.First().Tool.OrganizationUnit.Name;
            cell.Borders.Enable = 1;

            cell = header.Cell(5, 1);
            cell.Range.Text = "";
            cell.Borders.Enable = 1;
            cell = header.Cell(5, 2);
            cell.Range.Text = "";
            cell.Borders.Enable = 1;

            cell = header.Cell(6, 1);
            cell.Range.Text = "Реєстрація";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;
            cell = header.Cell(6, 2);
            cell.Range.Text = RegistrationTypeDisplay.GetDisplayName(examinations.First().Tool.Registration);
            cell.Borders.Enable = 1;

            cell = header.Cell(7, 1);
            cell.Range.Text = "Внутрішньозаводський номер";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;
            cell = header.Cell(7, 2);
            cell.Range.Text = examinations.First().Tool.IntraFactoryNumber;
            cell.Borders.Enable = 1;

            Paragraph para4 = word.Content.Paragraphs.Add();
            para4.Range.Font.Name = "Times New Roman";
            para4.Range.Font.Size = 11;
            para4.Range.Font.Italic = 1;
            para4.Range.Text = $"Дата виготовлення / Дата введеня в експлуатацію: {examinations.First().Tool.CreatingDate.ToString("yyyy.MM.dd")}/{examinations.First().Tool.CommissioningDate.ToString("yyyy.MM.dd")}\n";
            para4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            para4.Range.InsertParagraphAfter();

            Paragraph para5 = word.Content.Paragraphs.Add();
            para5.Range.Font.Name = "Times New Roman";
            para5.Range.Font.Size = 11;
            para5.Range.Font.Italic = 1;
            para5.Range.Text = $"Просрочені технічні (Експертні) огляди";
            para5.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            para5.Range.InsertParagraphAfter();

            List<ExaminationEntity> expired = examinations.Where(examination => !examination.ActualExaminationDate.HasValue &&
                                                                                        examination.ScheduleExaminationDate < DateTime.Now)
                                                                                        .ToList();

            Table expiredTable = word.Content.Tables.Add(para5.Range, Math.Max(expired.Count + 1, 2), 5);
            expiredTable.Range.Font.Name = "Times New Roman";
            expiredTable.Range.Font.Size = 11;
            expiredTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            expiredTable.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPercent;

            cell = expiredTable.Cell(1, 1);
            cell.Range.Text = "№";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = expiredTable.Cell(1, 2);
            cell.Range.Text = "Вид технічого огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = expiredTable.Cell(1, 3);
            cell.Range.Text = "Характер обстеження";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = expiredTable.Cell(1, 4);
            cell.Range.Text = "Причина огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = expiredTable.Cell(1, 5);
            cell.Range.Text = "Просрочена дата огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            for (int i = 0; i < expired.Count; i++)
            {
                cell = expiredTable.Cell(i + 2, 1);
                cell.Range.Text = expired[i].Id.ToString();
                cell.Borders.Enable = 1;

                cell = expiredTable.Cell(i + 2, 2);
                cell.Range.Text = expired[i].ExaminationType.Name;
                cell.Borders.Enable = 1;

                cell = expiredTable.Cell(i + 2, 3);
                cell.Range.Text = expired[i].ExaminationNature.Name;
                cell.Borders.Enable = 1;

                cell = expiredTable.Cell(i + 2, 4);
                cell.Range.Text = expired[i].ExaminationReason.Name;
                cell.Borders.Enable = 1;

                cell = expiredTable.Cell(i + 2, 5);
                cell.Range.Text = expired[i].ScheduleExaminationDate.ToString("yyyy.MM.dd");
                cell.Borders.Enable = 1;
            }

            Paragraph para6 = word.Content.Paragraphs.Add();
            para6.Range.Font.Name = "Times New Roman";
            para6.Range.Font.Size = 11;
            para6.Range.Font.Italic = 1;
            para6.Range.Text = $"\nМайбутні технічні (Експертні) огляди";
            para6.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            para6.Range.InsertParagraphAfter();

            List<ExaminationEntity> future = examinations.Where(examination => !examination.ActualExaminationDate.HasValue &&
                                                                                examination.ScheduleExaminationDate > DateTime.Now)
                                                                                .ToList();

            Table futureTable = word.Content.Tables.Add(para6.Range, Math.Max(future.Count + 1, 2), 5);
            futureTable.Range.Font.Name = "Times New Roman";
            futureTable.Range.Font.Size = 11;
            futureTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            futureTable.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPercent;

            cell = futureTable.Cell(1, 1);
            cell.Range.Text = "№";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = futureTable.Cell(1, 2);
            cell.Range.Text = "Вид технічого огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = futureTable.Cell(1, 3);
            cell.Range.Text = "Характер обстеження";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = futureTable.Cell(1, 4);
            cell.Range.Text = "Причина огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = futureTable.Cell(1, 5);
            cell.Range.Text = "Дата майбутнього огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            for (int i = 0; i < future.Count; i++)
            {
                cell = futureTable.Cell(i + 2, 1);
                cell.Range.Text = future[i].Id.ToString();
                cell.Borders.Enable = 1;

                cell = futureTable.Cell(i + 2, 2);
                cell.Range.Text = future[i].ExaminationType.Name;
                cell.Borders.Enable = 1;

                cell = futureTable.Cell(i + 2, 3);
                cell.Range.Text = future[i].ExaminationNature.Name;
                cell.Borders.Enable = 1;

                cell = futureTable.Cell(i + 2, 4);
                cell.Range.Text = future[i].ExaminationReason.Name;
                cell.Borders.Enable = 1;

                cell = futureTable.Cell(i + 2, 5);
                cell.Range.Text = future[i].ScheduleExaminationDate.ToString("yyyy.MM.dd");
                cell.Borders.Enable = 1;
            }

            List<ExaminationEntity> completed = examinations.Where(examination => examination.ActualExaminationDate.HasValue)
                                                                                    .ToList();

            Paragraph para7 = word.Content.Paragraphs.Add();
            para7.Range.Font.Name = "Times New Roman";
            para7.Range.Font.Size = 11;
            para7.Range.Font.Italic = 1;
            para7.Range.Text = $"\nВиконані технічні (Експертні) огляди";
            para7.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            para7.Range.InsertParagraphAfter();

            Table completedTable = word.Content.Tables.Add(para7.Range, Math.Max(completed.Count + 1, 2), 7);
            completedTable.Range.Font.Name = "Times New Roman";
            completedTable.Range.Font.Size = 11;
            completedTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            completedTable.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPercent;

            cell = completedTable.Cell(1, 1);
            cell.Range.Text = "№";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = completedTable.Cell(1, 2);
            cell.Range.Text = "Вид технічого огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = completedTable.Cell(1, 3);
            cell.Range.Text = "Характер обстеження";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = completedTable.Cell(1, 4);
            cell.Range.Text = "Причина огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = completedTable.Cell(1, 5);
            cell.Range.Text = "Планова дата огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = completedTable.Cell(1, 6);
            cell.Range.Text = "Фактична дата огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            cell = completedTable.Cell(1, 7);
            cell.Range.Text = "Результат огляду";
            cell.Borders.Enable = 1;
            cell.Range.Bold = 1;

            for (int i = 0; i < completed.Count; i++)
            {
                cell = completedTable.Cell(i + 2, 1);
                cell.Range.Text = completed[i].Id.ToString();
                cell.Borders.Enable = 1;

                cell = completedTable.Cell(i + 2, 2);
                cell.Range.Text = completed[i].ExaminationType.Name;
                cell.Borders.Enable = 1;

                cell = completedTable.Cell(i + 2, 3);
                cell.Range.Text = completed[i].ExaminationNature.Name;
                cell.Borders.Enable = 1;

                cell = completedTable.Cell(i + 2, 4);
                cell.Range.Text = completed[i].ExaminationReason.Name;
                cell.Borders.Enable = 1;

                cell = completedTable.Cell(i + 2, 5);
                cell.Range.Text = completed[i].ScheduleExaminationDate.ToString("yyyy.MM.dd");
                cell.Borders.Enable = 1;

                cell = completedTable.Cell(i + 2, 6);
                cell.Range.Text = completed[i].ActualExaminationDate.Value.ToString("yyyy.MM.dd");
                cell.Borders.Enable = 1;

                cell = completedTable.Cell(i + 2, 7);
                cell.Range.Text = completed[i].ExaminationResult;
                cell.Borders.Enable = 1;
            }

            application.Visible = true;
        }

        public async void PrintCertifications(IList<long> ids)
        {
            List<ExaminationEntity> examinations = new List<ExaminationEntity>();
            foreach (long id in ids)
            {
                var response = await _examinationService.GetById(id);
                if (!response.IsError)
                {
                    examinations.Add(response.Value);
                }
            }
            PrintCertifications(examinations);
        }

        public void PrintTools(IList<ToolEntity> tools)
        {
            Application application = new Application();
            Document word = application.Documents.Add();
            word.PageSetup.PageWidth = application.InchesToPoints(12f);

            var para1 = word.Content.Paragraphs.Add();
            para1.Range.Bold = 1;
            para1.Range.Text = "Картотека обладнання";
            para1.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            para1.Range.InsertParagraphAfter();

            Paragraph para2 = word.Content.Paragraphs.Add(para1.Range);
            para2.Range.Text = $"(Станом на {DateTime.Now.ToString("yyyy.MM.dd")})\n";
            para2.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            para1.Range.InsertParagraphAfter();

            //  1     2      3         4             5                6                  7                8               9
            // Id | Brand | Name | Subgroup | OrganizationUnit | Registration | RegistrationNumber | CreatingDate | CommissionDate

            Table table = word.Tables.Add(para2.Range, tools.Count, 9);
            table.Borders.Enable = 1;

            Cell cell = table.Cell(0 + 1, 1);
            cell.Range.Bold = 1;
            cell.Range.Text = "Id";

            cell = table.Cell(0 + 1, 2);
            cell.Range.Bold = 1;
            cell.Range.Text = "Марка";

            cell = table.Cell(0 + 1, 3);
            cell.Range.Bold = 1;
            cell.Range.Text = "Назва";

            cell = table.Cell(0 + 1, 4);
            cell.Range.Bold = 1;
            cell.Range.Text = "Підгрупа обладнання";

            cell = table.Cell(0 + 1, 5);
            cell.Range.Bold = 1;
            cell.Range.Text = "Структурний підрозділ";

            cell = table.Cell(0 + 1, 6);
            cell.Range.Bold = 1;
            cell.Range.Text = "Підлягає реєстрації";

            cell = table.Cell(0 + 1, 7);
            cell.Range.Bold = 1;
            cell.Range.Text = "Рєстраційний номер";

            cell = table.Cell(0 + 1, 8);
            cell.Range.Bold = 1;
            cell.Range.Text = "Дата виготовлення";

            cell = table.Cell(0 + 1, 9);
            cell.Range.Bold = 1;
            cell.Range.Text = "Введення в експлуатацію";

            for (int i = 1; i < tools.Count; i++)
            {
                cell = table.Cell(i + 1, 1);
                cell.Range.Text = tools[i].Id.ToString();

                cell = table.Cell(i + 1, 2);
                cell.Range.Text = tools[i].Brand;

                cell = table.Cell(i + 1, 3);
                cell.Range.Text = tools[i].Name;

                cell = table.Cell(i + 1, 4);
                cell.Range.Text = tools[i].Subgroup.Name;

                cell = table.Cell(i + 1, 5);
                cell.Range.Text = tools[i].OrganizationUnit.Name;

                cell = table.Cell(i + 1, 6);
                cell.Range.Text = tools[i].Registration.ToString();

                cell = table.Cell(i + 1, 7);
                cell.Range.Text = tools[i].RegistrationNumber.ToString();

                cell = table.Cell(i + 1, 8);
                cell.Range.Text = tools[i].CreatingDate.ToString("yyyy.MM.dd");

                cell = table.Cell(i + 1, 9);
                cell.Range.Text = tools[i].CommissioningDate.ToString("yyyy.MM.dd");
            }

            application.Visible = true;

            /*FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string savePath = dialog.SelectedPath;
            word.SaveAs(savePath);
            application.Documents.OpenNoRepairDialog(savePath);*/
        }

        public async void PrintTools(IList<long> ids)
        {
            List<ToolEntity> tools = new List<ToolEntity>();
            for (int i = 0; i < ids.Count; i++)
            {
                var result = await _toolService.GetById(ids[i]);
                if (!result.IsError)
                {
                    tools.Add(result.Value);
                }
            }

            PrintTools(tools);
        }
    }
}