using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts
{
    public abstract class PieChartSeriesBase : ChartSeriesBase
    {
        #region Properties

        #region Value
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(PieChartSeriesBase), new ChartPropertyMetadata(0d));
        #endregion

        #endregion
    }
}
