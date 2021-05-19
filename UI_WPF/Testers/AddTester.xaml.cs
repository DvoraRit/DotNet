using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : Window
    {
        IBL bl;
        BE.Tester tempTester;
        public AddTester()
        {
            bl = FactoryBL.GetBL();
            InitializeComponent();
            tempTester = new BE.Tester();
            AddTesterGrid.DataContext = tempTester;
            CityComboBox.DataContext = tempTester.Adress_Of_The_Tester;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tester_IdTextBox.Text.Length != 9)
                    throw new Exception("שגיאה! הכנס תעודת זהות באורך 9 ספרות");
                tester_IdTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                first_NameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                last_NameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                genderComboBox.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
                if (dateOfBitrthDatePicker.SelectedDate > DateTime.Now)
                {
                    throw new Exception("שגיאה! תאריך הלידה לא תקין");

                }
                tempTester.DateOfBitrth = (DateTime)dateOfBitrthDatePicker.SelectedDate;
                if (tell_NumberTextBox.Text.Length != 10)
                    throw new Exception("שגיאה! מספר הטלפון חייב להיות בן 10 ספרות ");
                tell_NumberTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                type_Of_CarComboBox.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
                type_Of_GearBoxComboBox.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
                years_of_expirienceTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                max_of_weekly_testsTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                max_distance_for_testTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                //setting the working hours of the new tester
                bool[,] workingDays = new bool[5, 7];

                //if the new tester is working on sunday
                if ((bool)testerHours.sundayCheckBox.IsChecked)
                {
                    int i = int.Parse(testerHours.sundayHours.BeginComboBoxHour.Text) - 9;//begining hour -9 because the array starting from 0
                    int j = int.Parse(testerHours.sundayHours.EndComboBoxHour.Text) - 9;//ending hour
                    for (; i <= j; i++)
                    {
                        workingDays[0, i] = true;
                    }
                }
                //if the new tester is working on monday
                if ((bool)testerHours.mondayCheckBox.IsChecked)
                {
                    int i = int.Parse(testerHours.mondayHours.BeginComboBoxHour.Text) - 9;
                    int j = int.Parse(testerHours.mondayHours.EndComboBoxHour.Text) - 9;
                    for (; i <= j; i++)
                    {
                        workingDays[1, i] = true;
                    }
                }
                //if the new tester is working on tuesday
                if ((bool)testerHours.tuesdayCheckBox.IsChecked)
                {
                    int i = int.Parse(testerHours.tuesdayHours.BeginComboBoxHour.Text) - 9;
                    int j = int.Parse(testerHours.tuesdayHours.EndComboBoxHour.Text) - 9;
                    for (; i <= j; i++)
                    {
                        workingDays[2, i] = true;
                    }
                }
                //if the new tester is working on wednesday
                if ((bool)testerHours.wednesdayCheckBox.IsChecked)
                {
                    int i = int.Parse(testerHours.wensdayHours.BeginComboBoxHour.Text) - 9;
                    int j = int.Parse(testerHours.wensdayHours.EndComboBoxHour.Text) - 9;
                    for (; i <= j; i++)
                    {
                        workingDays[3, i] = true;
                    }
                }
                //if the new tester is working on thursday
                if ((bool)testerHours.thursdayCheckBox.IsChecked)
                {
                    int i = int.Parse(testerHours.thursdayHours.BeginComboBoxHour.Text) - 9;
                    int j = int.Parse(testerHours.thursdayHours.EndComboBoxHour.Text) - 9;
                    for (; i <= j; i++)
                    {
                        workingDays[4, i] = true;
                    }
                }

                tempTester.Days_of_work = workingDays;

                Address address = new Address();
                address.city = Convert.ToString(CityComboBox.Text);
                address.street = StreetTextBox.Text;
                address.houseNum = Convert.ToInt32(HouseNumTextBox.Text);
                tempTester.Adress_Of_The_Tester = address;

                bl.AddTester(tempTester);
                MessageBox.Show("הבוחן נוסף בהצלחה");
                TestersEntrens testersEntrens = new TestersEntrens(Convert.ToString(tempTester.Tester_Id));
                testersEntrens.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + "בדוק את הקלט ונסה שנית");

            }

        }

        private void tester_IdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {//this property makes sure that the user is input numbers only in the id text box
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void tell_NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {//this property makes sure that the user is input numbers only in the tellphone text box
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
