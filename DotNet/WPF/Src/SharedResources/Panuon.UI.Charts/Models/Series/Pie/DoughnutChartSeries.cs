using System.Windows;

namespace Panuon.UI.Charts
{
    public class DoughnutChartSeries : PieChartSeries
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
            DependencyProperty.RegisterAttached("Thickness", typeof(double), typeof(DoughnutChartSeries), new FrameworkPropertyMetadata(5d, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region CornerRadius
        public double  
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(double), typeof(DoughnutChartSeries), new FrameworkPropertyMetadata(5d, FrameworkPropertyMetadataOptions.Inherits));
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(DoughnutChartSeries), new ChartSeriesPropertyMetadata(0d));
        #endregion


        #endregion
    }
}
