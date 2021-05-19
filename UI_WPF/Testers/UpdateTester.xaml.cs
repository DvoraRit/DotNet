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
    /// Interaction logic for UpdateTester.xaml
    /// </summary>
    public partial class UpdateTester : Window
    {
        IBL bl;
        BE.Tester tempTester;
        public UpdateTester(string Str_id)
        {
            bl = FactoryBL.GetBL();
            tempTester = bl.FindTester(Str_id);
       
            DataContext = tempTester;
            InitializeComponent();

            
            testersHours.sundayCheckBox.IsChecked = bl.IsWorkingSunday(tempTester);
            testersHours.mondayCheckBox.IsChecked = bl.IsWorkingMonday(tempTester);
            testersHours.thursdayCheckBox.IsChecked = bl.isWorkingThursday(tempTester);
            testersHours.wednesdayCheckBox.IsChecked = bl.isWorkingWednesday(tempTester);
            testersHours.tuesdayCheckBox.IsChecked = bl.IsWorkingTuesday(tempTester);

            testersHours.sundayHours.BeginComboBoxHour.Text = Convert.ToString(bl.BeginingHourOfTesterSunday(tempTester));
            testersHours.mondayHours.BeginComboBoxHour.Text = Convert.ToString(bl.BeginingHourOfTesterMonday(tempTester));
            testersHours.thursdayHours.BeginComboBoxHour.Text = Convert.ToString(bl.BeginingHourOfTesterThursday(tempTester));
            testersHours.wensdayHours.BeginComboBoxHour.Text = Convert.ToString(bl.BeginingHourOfTesterWednesday(tempTester));
            testersHours.tuesdayHours.BeginComboBoxHour.Text = Convert.ToString(bl.BeginingHourOfTesterTuesday(tempTester));
            
            testersHours.sundayHours.EndComboBoxHour.Text = Convert.ToString(bl.EndingHourOfTesterSunday(tempTester));
            testersHours.mondayHours.EndComboBoxHour.Text = Convert.ToString(bl.EndingHourOfTesterMonday(tempTester));
            testersHours.thursdayHours.EndComboBoxHour.Text = Convert.ToString(bl.EndingHourOfTesterThursday(tempTester));
            testersHours.wensdayHours.EndComboBoxHour.Text = Convert.ToString(bl.EndingHourOfTesterWednesday(tempTester));
            testersHours.tuesdayHours.EndComboBoxHour.Text = Convert.ToString(bl.EndingHourOfTesterTuesday(tempTester));

            CityComboBox.Text = tempTester.Adress_Of_The_Tester.city;
            StreetTextBox.Text = tempTester.Adress_Of_The_Tester.street;
            HouseNumTextBox.Text = tempTester.Adress_Of_The_Tester.houseNum.ToString();
            type_Of_CarComboBox.Text = tempTester.Type_Of_Car.ToString();
            type_Of_GearBoxComboBox.Text = tempTester.Type_Of_GearBox.ToString();
            genderComboBox.Text = tempTester.Gender.ToString();
        }

        private void deleteButtonTester_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק משתמש זה", "מחיקת משתמש", MessageBoxButton.YesNo, icon: MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                bl.RemoveTester(tempTester);
                MessageBox.Show("פרטי הבוחן נמחקו");//massage box will show
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();

            }

           
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Address address = new Address();
            address.city = CityComboBox.Text;
            address.houseNum = int.Parse(HouseNumTextBox.Text);
            address.street = StreetTextBox.Text;
            tempTester.Adress_Of_The_Tester = address;
            bl.UpdateTester(tempTester);
            MessageBox.Show("פרטי הבוחן עודכנו");//massage box will show
            TestersEntrens testersEntrens = new TestersEntrens(Convert.ToString(tempTester.Tester_Id));
            testersEntrens.Show();
            Close();
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
