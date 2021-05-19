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

namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonTraineeEntrence_Click(object sender, RoutedEventArgs e)
        {
            LogInTraineeWindow logInTraineeWindow = new LogInTraineeWindow();
            logInTraineeWindow.Show();
            Close();
        }


        private void ButtonTestersEntrence_Click(object sender, RoutedEventArgs e)
        {
            TesterLogIn testerLogIn = new TesterLogIn();
            testerLogIn.Show();
            Close();
            //TestersEntrens testersEntrens = new TestersEntrens();
            //testersEntrens.Show();
            //mainWindow.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MoreDetailsWindow moreDetailsWindow = new MoreDetailsWindow();
            moreDetailsWindow.Show();
            Close();
        }
    }
}
