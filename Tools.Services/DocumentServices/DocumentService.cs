using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Tools.Database.Entities;
using Tools.Services.ToolServices;

namespace Tools.Services.DocumentServices
{
    public class DocumentService : IDocumentService
    {
        private readonly IToolService _toolService;

        public DocumentService(IToolService toolService)
        {
            _toolService = toolService;
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