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
    /// Interaction logic for UpdateAndDeleteTestOfTrainee.xaml
    /// </summary>
    public partial class UpdateAndDeleteTestOfTrainee : Window
    {
        IBL bl;
        Trainee temp_Trainee;
        Test temp_test;

        public UpdateAndDeleteTestOfTrainee(string Str_id)
        {
            bl = FactoryBL.GetBL();
            InitializeComponent();
            temp_Trainee = bl.FindTrainee(Str_id);
            temp_test = new Test();
            numOfTest.ItemsSource = bl.GetTests(ts => ts.Trainee_Id == temp_Trainee.Trainee_Id);
            numOfTest.DisplayMemberPath = "Num_Of_Test";
            numOfTest.SelectedValuePath = "Num_Of_Test";

            traineeGrid.DataContext = temp_Trainee;
            testGrid.DataContext = temp_test;


        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TraineeEntrens traineeEntrens = new TraineeEntrens(temp_Trainee.Trainee_Id);
            traineeEntrens.Show();
            Close();
        }

        private void numOfTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                temp_test = (Test)numOfTest.SelectedItem;
                DateOfTest.SelectedDate = temp_test.Date_Of_Test.Date;
                statHourOfTest_ComboBox.Text = temp_test.Time_Of_Test.ToString();
                typeOfCarOfTest.Content = temp_test.Type_Of_Car;
                typeOfGearBoxOfTest.Content = temp_test.Type_Of_GearBox;
                cityComboBox.Text = temp_test.Address_Of_StartOfTest.city;
                streetTextBox.Text = temp_test.Address_Of_StartOfTest.street;
                houseTextBox.Text = temp_test.Address_Of_StartOfTest.houseNum.ToString();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק משתמש זה", "מחיקת משתמש", MessageBoxButton.YesNo, icon: MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                bl.RemoveTest(temp_test);
                MessageBox.Show("פרטי המבחן נמחקו");//massage box will show
                TraineeEntrens traineeEntrens = new TraineeEntrens(temp_Trainee.Trainee_Id);
                traineeEntrens.Show();
                Close();

            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            temp_test.Time_Of_Test = Convert.ToInt32(statHourOfTest_ComboBox.Text);
            Address address = new Address(streetTextBox.Text, Convert.ToInt32(houseTextBox.Text), cityComboBox.Text);
            temp_test.Address_Of_StartOfTest = address;
            temp_test.Date_Of_Test = Convert.ToDateTime(DateOfTest.Text);

            bl.UpdateTestTrainee(temp_test);
            MessageBox.Show("פרטי המבחן עודכנו");//massage box will show
            TraineeEntrens traineeEntrens = new TraineeEntrens(temp_Trainee.Trainee_Id);
            traineeEntrens.Show();
            Close();
        }
    }
}
