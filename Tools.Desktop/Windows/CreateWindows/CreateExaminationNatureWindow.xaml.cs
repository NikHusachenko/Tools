using System.Collections.Generic;
using System.Windows;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.ExaminationNatureServices;

namespace Tools.Desktop.Windows.CreateWindows
{
    public partial class CreateExaminationNatureWindow : Window
    {
        private readonly IExaminationNatureService _examinationNatureService;

        public CreateExaminationNatureWindow(IExaminationNatureService examinationNatureService)
        {
            _examinationNatureService = examinationNatureService;

            InitializeComponent();
        }

        private void selectExaminationNatureComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string nature = selectExaminationNatureComboBox.SelectedItem as string;
            if (nature == null)
            {
                return;
            }
            natureRename.Text = nature;

            examinationNatureRenameContainer.Visibility = Visibility.Visible;
            examinationNatureControlPanel.Visibility = Visibility.Visible;
        }

        private async void selectExaminationNatureComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            selectExaminationNatureComboBox.Items.Clear();

            ICollection<ExaminationNatureEntity> natures = await _examinationNatureService.GetAll();
            foreach (ExaminationNatureEntity nature in natures)
            {
                selectExaminationNatureComboBox.Items.Add(nature.Name);
            }
            natureRename.Text = string.Empty;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string newName = newNatureName.Text;
            var response = await _examinationNatureService.Create(newName);
            if (response.IsError)
            {
                newNatureName.Text = string.Empty;
                return;
            }

            MessageBox.Show(Messages.CREATED_SUCCESSFULY);
            newNatureName.Text = string.Empty;
            selectExaminationNatureComboBox_Loaded(sender, e);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = selectExaminationNatureComboBox.SelectedItem as string;
            if (name == null)
            {
                return;
            }

            var response = await _examinationNatureService.Delete(name);
            if (response.IsError)
            {
                MessageBox.Show(response.ErrorMessage);
                return;
            }

            MessageBox.Show(Messages.DELETED_SUCCESSFULY);
            selectExaminationNatureComboBox_Loaded(sender, e);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string oldName = selectExaminationNatureComboBox.SelectedItem as string;
            if (oldName == null)
            {
                newNatureName.Text = string.Empty;
                return;
            }

            string newName = natureRename.Text;
            var response = await _examinationNatureService.Rename(oldName, newName);
            if (response.IsError)
            {
                newNatureName.Text = string.Empty;
                return;
            }

            MessageBox.Show(Messages.REMANED_SUCCESSFULY);
            selectExaminationNatureComboBox_Loaded(sender, e);
        }
    }
}