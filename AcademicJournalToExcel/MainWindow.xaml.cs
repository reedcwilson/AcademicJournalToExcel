using ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AcademicJournalToExcel
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Munger munger;

		public MainWindow()
		{
			InitializeComponent();
			munger = new Munger();
			itemsAddedTxt.Text = "0";
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			munger.Munge(rowInfoTxt.Text);
			itemsAddedTxt.Text = munger.Items.ToString();
			rowInfoTxt.Text = string.Empty;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(munger.ToString());
			munger.Clear();
			itemsAddedTxt.Text = "0";
		}
	}
}
