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
using System.Windows.Shapes;
using BL;

namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for ShowTests.xaml
    /// </summary>
    public partial class ShowTests : Window
    {
        IBL bl;
        int choice;
        List<string> ListForGrouping3;

        public ShowTests()
        {
            bl = FactoryBL.GetBL();
            InitializeComponent();

            testDataGrid.Visibility = Visibility.Hidden;

            ListForGrouping3 = new List<string>();
            ListForGrouping3.Add("סוג רכב");
            ListForGrouping3.Add("סוג תיבת הילוכים");
            groupingUserControl.Visibility = Visibility.Hidden;
            testDataGrid.DataContext = bl.GetTests();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
        }

        private void AllTestsButton_Click(object sender, RoutedEventArgs e)
        {
            if (testDataGrid.Visibility == Visibility.Hidden)
            {
                testDataGrid.DataContext = bl.GetTests();
                testDataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                testDataGrid.DataContext = bl.GetTests();
                testDataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                choice = testGroup.SelectedIndex;

                switch (choice)
                {
                    case 0://means he chose "עיר בה הטסט מתקיים"
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":עיר בה הטסט מתקיים";
                            groupingUserControl.Source = bl.GetTestsByCity();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    case 1://means he chose "מספר הטסטים בעיר"
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":מספר הטסטים בעיר";
                            groupingUserControl.Source = bl.GetNumOfTestsByCity();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    case 2:
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":תוצאת הטסט";
                            groupingUserControl.Source = bl.GetTestBySuccess();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + "check your input and try again");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("הידעת מספר הנכשלים בטסט השנה הוא " + bl.CountNumFailed(DateTime.Now.Year));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("הידעת מספר העובר בטסט השנה הוא " + bl.CountNumPass(DateTime.Now.Year));
        }
    }
}






