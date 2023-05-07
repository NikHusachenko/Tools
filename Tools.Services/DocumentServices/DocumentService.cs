using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using Tools.Database.Entities;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Models;

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
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
            Document word = application.Documents.Add();

            //  1     2      3         4             5                6                  7                8               9
            // Id | Brand | Name | Subgroup | OrganizationUnit | Registration | RegistrationNumber | CreatingDate | CommissionDate

            Table table = word.Tables.Add(word.Range(), tools.Count, 9);
            table.Borders.Enable = 1;

            for (int i = 0; i < tools.Count; i++)
            {
                Cell cell = table.Cell(i + 1, 1);
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
                cell.Range.Text = tools[i].CreatingDate.ToString("dd.MM.yyyy");

                cell = table.Cell(i + 1, 9);
                cell.Range.Text = tools[i].CommissioningDate.ToString("dd.MM.yyyy");
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