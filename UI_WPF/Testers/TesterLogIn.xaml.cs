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

namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for TesterLogIn.xaml
    /// </summary>
    public partial class TesterLogIn : Window
    {
        IBL bl;
        public TesterLogIn()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();
        }
        
        private void tester_login_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.FindTester(tester_idTextBox.Text) == null)
                    throw new Exception("בוחן עם תעודת זהות " + tester_idTextBox.Text + " לא נמצא ");
                TestersEntrens testersEntrens = new TestersEntrens(tester_idTextBox.Text);
                testersEntrens.Show();
                Close();
                //TraineeEntrens traineeEntrens = new TraineeEntrens(tester_idTextBox.Text);
                //traineeEntrens.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + "בדוק את הקלט ונסה שוב ");
            }
        }

        private void tester_login_new_account_Click(object sender, RoutedEventArgs e)
        {
            AddTester addTester = new AddTester();
            addTester.Show();
            Close();
        }

        private void Double(object sender, MouseButtonEventArgs e)
        {
            tester_idTextBox.Text = null;
        }
    }
}
