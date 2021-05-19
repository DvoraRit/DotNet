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
using System.Collections;
using BE;
namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for GroupingUserControl.xaml
    /// </summary>
    public partial class GroupingUserControl : UserControl
    {
        private IEnumerable source;
        /// <summary>
        /// proprty to source of Grouping
        /// </summary>
        public IEnumerable Source
        {
            get { return source; }
            set
            {
                source = value;
                this.listView.ItemsSource = source;
            }
        }
        public GroupingUserControl()
        {
            InitializeComponent();
        }
    }
}
