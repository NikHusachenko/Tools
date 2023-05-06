using System.Windows;
using System.Windows.Controls;

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для CertificationLayout.xaml
	/// </summary>
	public partial class CertificationLayoutPage : Page
	{
		public CertificationLayoutPage()
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
			pagesFrame.Navigate(new SurveysBySelectedDivisionPage());
		}

		private void ThirdRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(true);
			pagesFrame.Navigate(new SurveysBySelectedSubgroupPage());
		}

		private void FourthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(true);
			pagesFrame.Navigate(new SurveysByTypeSurvey());
		}

		private void FifthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(null);
		}

		private void SixthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(new SurveysBySelectedDivisionPage());
		}

		private void SeventhRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(new SurveysBySelectedSubgroupPage());
		}

		private void EighthRadioBtn_Click(object sender, RoutedEventArgs e)
		{
			ShowCalendarStackPanel(false);
			pagesFrame.Navigate(new SurveysByTypeSurvey());
		}
		private void ShowCalendarStackPanel(bool isShowing)
		{
			if (isShowing) calendarStackPanel.Visibility = Visibility.Visible;
			else calendarStackPanel.Visibility = Visibility.Collapsed;
		}

		private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void CalendarButton_Click(object sender, RoutedEventArgs e)
		{
			if (datePicker.Visibility == Visibility.Visible)
			{
				datePicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				datePicker.Visibility = Visibility.Visible;
			}
		}
	}
}
