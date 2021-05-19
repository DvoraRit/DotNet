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
using BE;
namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for ViewAllTesters.xaml
    /// </summary>
    public partial class ViewAllTesters : Window
    {
        IBL bl;
        int choice;
        //BE.Tester tempTester;
        List<string> ListForGrouping2;


        public ViewAllTesters()
        {
            bl = FactoryBL.GetBL();
            InitializeComponent();
            //DataContext = bl.GetTesters();
            testerDataGrid.Visibility = Visibility.Hidden;

            ListForGrouping2 = new List<string>();
            ListForGrouping2.Add("סוג רכב");
            ListForGrouping2.Add("סוג תיבת הילוכים");
            groupingUserControl.Visibility = Visibility.Hidden;


        }

        private void AllTestersButton_Click(object sender, RoutedEventArgs e)
        {
            if (testerDataGrid.Visibility == Visibility.Hidden)
            {
                testerDataGrid.DataContext = bl.GetTesters();
                testerDataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                testerDataGrid.DataContext = bl.GetTesters();
                testerDataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                choice = testerGroup.SelectedIndex;

                switch (choice)
                {
                    case 0://means he chose "סוג רכב"
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":סוג רכב";
                            groupingUserControl.Source = bl.GetTestersByTypeOfCar();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    case 1://means he chose "סוג תיבת הילוכים"
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":סוג תיבת הילוכים";
                            groupingUserControl.Source = bl.GetTestersByTypeOfGear();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    case 2://means he chose "מספר שנות ניסיון"
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":מספר שנות ניסיון";
                            groupingUserControl.Source = bl.GetTestersByYearOfEx();
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

       
    }
}




