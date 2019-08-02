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
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        VM vm = new VM();
        public AddPatient()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void rhbutton(object sender, RoutedEventArgs e)
        {
            RadioButton choice = (sender as RadioButton);
            var finalchoice = choice.Content.ToString();
            vm.RHFactor = finalchoice;
        }

        private void Btypebutton(object sender, RoutedEventArgs e)
        {
            RadioButton choice = (sender as RadioButton);
            var finalchoice = choice.Content.ToString();
            if (finalchoice == "A")
            {
                vm.BloodType = Data.BloodType.A;
            } else if (finalchoice == "B")
            {
                vm.BloodType =  Data.BloodType.B;
            } else if (finalchoice == "AB")
            {
                vm.BloodType = Data.BloodType.AB;
            } else if (finalchoice == "O")
            {
                vm.BloodType = Data.BloodType.O;
            }
        }

    }
}
