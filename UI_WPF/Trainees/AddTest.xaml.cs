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
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        IBL bl;
        Trainee temp_Trainee;
        Test newTest = new Test();
        public AddTest(string Str_id)
        {
            bl = FactoryBL.GetBL();
            temp_Trainee = bl.FindTrainee(Str_id);
            InitializeComponent();
            TraineeGrid.DataContext = temp_Trainee;
            TestGrid.DataContext = newTest;
            cityComboBox.SelectedItem = temp_Trainee.Address.city;
            streetTxtBox.Text = temp_Trainee.Address.street;
            houseTextBox.Text = Convert.ToString(temp_Trainee.Address.houseNum);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TraineeEntrens traineeEntrens = new TraineeEntrens(Convert.ToString(temp_Trainee.Trainee_Id));
            traineeEntrens.Show();
            Close();

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(Convert.ToString(temp_Trainee.Trainee_Id));
            updateTraineeWindow.Show();
            Close();
        }


        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Address address = new Address();
                address.city = Convert.ToString(cityComboBox.Text);
                address.street = streetTxtBox.Text;
                address.houseNum = Convert.ToInt32(houseTextBox.Text);
                newTest.Address_Of_StartOfTest = address;
                newTest.Type_Of_Car = temp_Trainee.Type_Of_Car;
                newTest.Type_Of_GearBox = temp_Trainee.Type_Of_GearBox;
                newTest.Trainee_Id = temp_Trainee.Trainee_Id;
                bl.AddTest(newTest);
                MessageBox.Show("המבחן נוסף שיהיה בהצלחה");
                TraineeEntrens traineeEntrens = new TraineeEntrens(temp_Trainee.Trainee_Id);
                traineeEntrens.Show();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
