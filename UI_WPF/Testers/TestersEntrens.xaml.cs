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
    /// Interaction logic for TestersEntrens.xaml
    /// </summary>
    public partial class TestersEntrens : Window
    {
        IBL bl;
        Tester temp_Tester;

        public TestersEntrens(string Str_id)
        {
            bl = FactoryBL.GetBL();
            InitializeComponent();
            temp_Tester = bl.FindTester(Str_id);
            DataContext = temp_Tester;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//back to main window button
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            testersEntrens.Close();
        }

        private void AddTesterButton_Click(object sender, RoutedEventArgs e)
        {
            AddTester addTester = new AddTester();
            addTester.Show();
        }

        private void UpdateTest_Click(object sender, RoutedEventArgs e)
        {
            UpdateTest updateTest = new UpdateTest(temp_Tester.Tester_Id);
            updateTest.Show();
        }


        private void updateTester_Click(object sender, RoutedEventArgs e)
        {
            UpdateTester updateTester = new UpdateTester(temp_Tester.Tester_Id);
            updateTester.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("אחוז העוברים אצלך בטסטים הוא " + bl.PrecentPassInTester(temp_Tester)+"%");
        }
    }
}
