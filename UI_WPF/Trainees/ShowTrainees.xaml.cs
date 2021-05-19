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
using BE;
using BL;


namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for ShowTrainees.xaml
    /// </summary>
    public partial class ShowTrainees : Window
    {
        BL.IBL bl;
        int choice;
        List<string> ListForGrouping;

        public ShowTrainees()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            traineeDataGrid.Visibility = Visibility.Hidden;
            ListForGrouping = new List<string>();
            ListForGrouping.Add("שם בית הספר");
            ListForGrouping.Add("שם מורה");
            ListForGrouping.Add("מספר טסטים");
            groupingUserControl.Visibility = Visibility.Hidden;
        }

        private void showAllTrainees_Click(object sender, RoutedEventArgs e)
        {
            /// dataGrid.ItemsSource = bl.GetTrainees();
            //dataGrid.DataContext = bl.GetTrainees();
            //dataGrid.Visibility = Visibility.Visible;
            if (traineeDataGrid.Visibility == Visibility.Hidden)
            {
                traineeDataGrid.DataContext = bl.GetTrainees();
                traineeDataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                traineeDataGrid.DataContext = bl.GetTrainees();
                traineeDataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void ShowTrainnes_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                choice =traineeGroup.SelectedIndex;
                
                switch (choice)
                {
                    case 0://means he chose "שם בית הספר"
                        { 
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":שם בית הספר";
                            groupingUserControl.Source = bl.GetTraineesBySchoolName();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    case 1:
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":שם מורה";
                            groupingUserControl.Source = bl.GetTraineesByTeacherName();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    case 2:
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":מספר טסטים";
                            groupingUserControl.Source = bl.GetTraineeByNumOfTest();
                            this.showGroups.Content = groupingUserControl;
                            break;
                        }
                    case 3:
                        {
                            groupingUserControl.Visibility = Visibility.Visible;
                            groupingUserControl.Lable.Content = ":עיר מגורים";
                            groupingUserControl.Source = bl.GetTraineeByCity();
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
