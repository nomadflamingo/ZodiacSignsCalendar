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
using ZodiacSignsCalendar.ViewModels;

namespace ZodiacSignsCalendar.Views
{
    /// <summary>
    /// Interaction logic for ZodiacSignControl.xaml
    /// </summary>
    public partial class ZodiacSignCalcControl : UserControl
    {
        private ZodiacSignCalcViewModel _viewModel;
        public ZodiacSignCalcControl()
        {
            InitializeComponent();
            DataContext = _viewModel = new ZodiacSignCalcViewModel();
        }
    }
}
