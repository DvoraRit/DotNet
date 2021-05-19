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
    /// Interaction logic for MoreDetailsWindow.xaml
    /// </summary>
    public partial class MoreDetailsWindow : Window
    {
        public MoreDetailsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//BACK BUTTON
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//watch trainees more info
        {
            ShowTrainees showTrainees = new ShowTrainees();
            showTrainees.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//watch testers more info
        {
            ViewAllTesters viewAllTesters = new ViewAllTesters();
            viewAllTesters.Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//watch tests more info
        {
            ShowTests showTests = new ShowTests();
            showTests.Show();
            Close();
        }
    }
}
