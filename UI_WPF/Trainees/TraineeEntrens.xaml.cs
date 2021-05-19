using BE;
using BL;
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


namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class TraineeEntrens : Window
    {
        IBL bl;
        Trainee temp_Trainee;

        public TraineeEntrens(string Str_id)
        {

            bl = FactoryBL.GetBL();
            InitializeComponent();
            temp_Trainee = bl.FindTrainee(Str_id);
            DataContext = temp_Trainee;
        }


        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            traineeEntrens.Close();
        }


        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            addTraineeWindow.Show();
        }

        private void UpdateTaineeButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(temp_Trainee.Trainee_Id);
            updateTraineeWindow.Show();
            traineeEntrens.Close();
        }

        private void AddTestButtom_Click(object sender, RoutedEventArgs e)
        {
            AddTest addTest = new AddTest(Convert.ToString(temp_Trainee.Trainee_Id));
            addTest.Show();
            Close();
        }

        private void UpdateTestButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateAndDeleteTestOfTrainee updateAndDeleteTestOfTrainee = new UpdateAndDeleteTestOfTrainee(temp_Trainee.Trainee_Id);
            updateAndDeleteTestOfTrainee.Show();
            Close();
        }
    }
}
