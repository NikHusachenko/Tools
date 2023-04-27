using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using Tools.Database.Entities;

namespace Tools.Services.DocumentServices
{
    public class DocumentService : IDocumentService
    {
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
                for (int j = 0; j < 0; j++)
                {
                    Cell cell = table.Cell(i, j);

                    cell.Range.Text = tools[i].Id.ToString();
                    cell.Range.Text = tools[i].Brand;
                    cell.Range.Text = tools[i].Name;
                    cell.Range.Text = tools[i].Subgroup.Name;
                    cell.Range.Text = tools[i].OrganizationalType.ToString();
                    cell.Range.Text = tools[i].Registration.ToString();
                    cell.Range.Text = tools[i].RegistrationNumber.ToString();
                    cell.Range.Text = tools[i].CreatingDate.ToString("dd.MM.yyyy");
                    cell.Range.Text = tools[i].CommissioningDate.ToString("dd.MM.yyyy");
                }
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
    }
}