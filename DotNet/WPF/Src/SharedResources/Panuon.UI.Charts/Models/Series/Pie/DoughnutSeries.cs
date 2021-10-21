using System.Windows;

namespace Panuon.UI.Charts
{
    public class DoughnutSeries : PieChartSeriesBase
    {
        #region Ctor
        #endregion

        #region Properties

        #region Thickness
        public static double GetThickness(DependencyObject chartPanel)
        {
            return (double)chartPanel.GetValue(ThicknessProperty);
        }

        public static void SetThickness(DependencyObject chartPanel, double value)
        {
            chartPanel.SetValue(ThicknessProperty, value);
        }

        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.RegisterAttached("Thickness", typeof(double), typeof(DoughnutSeries), new ChartPropertyMetadata(5d, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #endregion
    }
}
