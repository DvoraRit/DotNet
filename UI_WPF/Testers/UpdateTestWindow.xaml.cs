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
using System.Globalization;
using BE;
using BL;

namespace UI_WPF
{
    
    public class BooleanToEnumConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Enum_Success.עבר;
            }
            else
                return Enum_Success.נכשל;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    /// <summary>
    /// Interaction logic for UpdateTest.xaml
    /// </summary>
    public partial class UpdateTest : Window
    {
        IBL bl;
        Test temp_test;
        CriteriaOfTheTest cr;
        public UpdateTest(string Str_idTester)
        {
            bl = FactoryBL.GetBL();
            InitializeComponent();
            temp_test = new Test();
            testIDComboBox.ItemsSource = bl.GetTests(ts=>ts.Result_Of_Test==Enum_Success.NoData && ts.Tester_Id== Str_idTester);
            testIDComboBox.DisplayMemberPath = "Num_Of_Test";
            testIDComboBox.SelectedValuePath = "Num_Of_Test";

            cr= new CriteriaOfTheTest();
            temp_test.CriteriaOfTheTest = cr;
            this.UpdateTestGrid.DataContext = cr;
        }
        public  BE.Enum_Success BooleanToEnumConverter(bool value)
        {
            if (value)
                return Enum_Success.עבר;
            else
                return Enum_Success.נכשל;
        }

        private void OKbutton_Click(object sender, RoutedEventArgs e)
        {
            cr.compliance_traffic_signs = BooleanToEnumConverter(CheckBoxTrafficSigns.IsChecked.Value);
            cr.gave_priority = BooleanToEnumConverter(CheckBoxPrioraty.IsChecked.Value);
            cr.mirrors_checked = BooleanToEnumConverter(CheckBoxMirrorLook.IsChecked.Value);
            cr.reverse_parking = BooleanToEnumConverter(CheckBoxRevrsPark.IsChecked.Value); 
            cr.saved_distance = BooleanToEnumConverter(CheckBoxSavedDist.IsChecked.Value);
            cr.tester_break = BooleanToEnumConverter(CheckBoxIsBreak.IsChecked.Value); 
            cr.tester_touch_weel= BooleanToEnumConverter(CheckBoxIsTouch.IsChecked.Value);
            cr.speed = BooleanToEnumConverter(CheckBoxSpeed.IsChecked.Value);
            cr.signaled = BooleanToEnumConverter(CheckBoxSignal.IsChecked.Value);
  
            
            bl.UpdateTest(temp_test, cr);
            MessageBox.Show(" מבחן מספר " + testIDComboBox.SelectedValue + " עודכן בהצלחה/n התלמיד " + bl.FindTest(temp_test.Num_Of_Test).Result_Of_Test.ToString());
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void testIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                temp_test = (Test)testIDComboBox.SelectedItem;
                dateOfTestLabel.Content = temp_test.Date_Of_Test.Date;
                hourOfTest.Content = temp_test.Time_Of_Test;
                typeOfCarOfTest.Content = temp_test.Type_Of_Car;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + "check your input and try again");
            }

        }

        private void commentsTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            commentsTextBox.Text = "";
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("?הפרטים שהזנת לא ישמרו, האם אתה בטוח שאת רוצה לחזור לתפריט הראשי", "אזהרה", MessageBoxButton.YesNo, icon: MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
    }
}
