using Caliburn.Micro;
using Panuon.UI.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Samples
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new ViewModel();
            DataContext = viewModel;

            viewModel.Series = new SeriesCollection()
            {
                new DoughnutSeries()
                {
                     Caption = "门诊医师工作站",
                      Value = 3,
                      Fill = Brushes.Red,
                },
                new DoughnutSeries()
                {
                     Caption = "护士工作站",
                      Value = 2,
                      Fill = Brushes.Green,
                },
            };
        }
    }

    public class ViewModel : PropertyChangedBase
    {
        public SeriesCollection Series { get => _series; set => Set(ref _series, value); }
        private SeriesCollection _series;
    }
}
