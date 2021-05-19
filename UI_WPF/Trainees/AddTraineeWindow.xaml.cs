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
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        BE.Trainee tempTrainee;
        IBL bl;

        public AddTraineeWindow()
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();
            tempTrainee = new BE.Trainee();
            this.AddTraineeGrid.DataContext = tempTrainee;
            CityComboBox.DataContext = tempTrainee.Address;
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (trainee_IdTextBox.Text.Length > 9)
                {

                    throw new Exception("תעודת הזהות אורכה מידי");
                }
                if (trainee_IdTextBox.Text.Length < 9)
                {

                    throw new Exception("תעודת הזהות קצרה מידי");
                }

                trainee_IdTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                first_NameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                last_NameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                genderComboBox.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
                if (tell_NumberTextBox.Text.Length > 10 || tell_NumberTextBox.Text.Length < 5)// if the phone number to short or to long throw exception
                {

                    throw new Exception("מספר הטלפון שגוי");
                }
                tell_NumberTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                school_NameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                teachers_NameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                num_Of_Driving_ClassesTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                type_Of_CarComboBox.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
                type_Of_GearBoxComboBox.GetBindingExpression(ComboBox.TextProperty).UpdateSource();
                
                //address

                BE.Address address = new Address();
                address.city = Convert.ToString(CityComboBox.Text);
                address.street = StreetTextBox.Text;
               address.houseNum = Convert.ToInt32(HouseNumTextBox.Text);
               tempTrainee.Address= address;

                DateTime dateOfBirth = new DateTime();
                dateOfBirth = (DateTime)dateOfBitrthDatePicker.SelectedDate;
                tempTrainee.DateOfBitrth = dateOfBirth;

                bl.AddTrainee(tempTrainee);
                MessageBox.Show("התלמיד נוסף בהצלחה");
                TraineeEntrens traineeEntrens = new TraineeEntrens(trainee_IdTextBox.Text);
                traineeEntrens.Show();
                Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
