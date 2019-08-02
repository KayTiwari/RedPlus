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

namespace RedPlus
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        VM vm = new VM();
        public HomeScreen()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        public void New_Patient(object sender, RoutedEventArgs e)
        {
            AddPatient ap = new AddPatient();
            ap.Show();
        }
        
    }
}
