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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;

namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for LogInTraineeWindow.xaml
    /// </summary>
    public partial class LogInTraineeWindow : Window
    {
        IBL bl;
        public LogInTraineeWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();
        }

        private void Button_Click_login(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.FindTrainee(idTextBox.Text) == null)
                    throw new Exception("תלמיד עם תעודת זהות " + idTextBox.Text + " לא נמצא ");
                TraineeEntrens traineeEntrens = new TraineeEntrens(idTextBox.Text);
                traineeEntrens.Show();
                Close();
                //UpdateTraineeWindow updateTraineeWindow = new UpdateTraineeWindow(idTextBox.Text);
                //updateTraineeWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + "בדוק את הקלט ונסה שוב ");
            }

        }

        private void Button_Click_newaccount(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTraineeWindow = new AddTraineeWindow();
            addTraineeWindow.Show();
            Close();
           
            
        }

        private void idTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            idTextBox.Text = null;
        }

       
    }
}
