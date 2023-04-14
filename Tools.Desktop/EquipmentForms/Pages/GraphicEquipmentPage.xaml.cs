using System.Windows;
using System.Windows.Controls;

namespace Tools.Desktop.EquipmentForms.Pages
{
	/// <summary>
	/// Логика взаимодействия для GraphicEquipmentPage.xaml
	/// </summary>
	public partial class GraphicEquipmentPage : Page
	{
		public GraphicEquipmentPage()
		{
			InitializeComponent();
		}

		private void FirstRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(true);
			pagesFrame.Navigate(null);
		}

		private void SecondRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(true);
			pagesFrame.Navigate(new UpcomingSurveysSelectedDivisionPage());
		}

		private void ThirdRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(true);
			pagesFrame.Navigate(new UpcomingSurveysSelectedSubgroupEquipmentPage());
		}

		private void FourthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(true);
			pagesFrame.Navigate(new UpcomingSurveysByTypeSurvey());
		}

		private void FifthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(null);
		}

		private void SixthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(new UpcomingSurveysSelectedDivisionPage());
		}

		private void SeventhRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(new UpcomingSurveysSelectedSubgroupEquipmentPage());
		}

		private void EighthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(new UpcomingSurveysByTypeSurvey());
		}
		private void ShowCalendarStackPanel(bool isShowing)
		{
			if(isShowing) calendarStackPanel.Visibility = Visibility.Visible;
			else calendarStackPanel.Visibility = Visibility.Collapsed;
		}
	}
}
