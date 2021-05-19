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
using BL;
using BE;
using System.Diagnostics;

namespace UI_WPF
{

    /// <summary>
    /// Interaction logic for UpdateTraineeWindow.xaml
    /// </summary>
    public partial class UpdateTraineeWindow : Window
    {
        IBL bl;
        Trainee temp_Trainee;
        public UpdateTraineeWindow(string Str_id)
        {
            bl = FactoryBL.GetBL();
            InitializeComponent();
            temp_Trainee = bl.FindTrainee(Str_id);
            DataContext = temp_Trainee;
            TraCityComboBox.DataContext = temp_Trainee.Address;

            NumClassTextBox.Text = Convert.ToString(temp_Trainee.Num_Of_Driving_Classes);
            TraCityComboBox.Text = temp_Trainee.Address.city;
            StreetTextBox.Text = temp_Trainee.Address.street;
            NumHouseTextBox.Text = Convert.ToString(temp_Trainee.Address.houseNum);
            TtraBDay.Text = Convert.ToString(temp_Trainee.DateOfBitrth);

        }

        private void UpdateTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            temp_Trainee.Num_Of_Driving_Classes = Convert.ToInt32(NumClassTextBox.Text);
            Address address = new Address(StreetTextBox.Text, Convert.ToInt32(NumHouseTextBox.Text), TraCityComboBox.Text);
            temp_Trainee.Address = address;
            temp_Trainee.DateOfBitrth = Convert.ToDateTime(TtraBDay.Text);
            bl.UpdateTrainee(temp_Trainee);
            MessageBox.Show("פרטי התלמיד עודכנו");//massage box will show
            TraineeEntrens traineeEntrens = new TraineeEntrens(Convert.ToString(temp_Trainee.Trainee_Id));
            traineeEntrens.Show();
            Close();
        }

        private void deleteTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק משתמש זה", "מחיקת משתמש", MessageBoxButton.YesNo, icon: MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                bl.RemoveTrainee(temp_Trainee);
                MessageBox.Show("פרטי התלמיד נמחקו");//massage box will show
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();

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
