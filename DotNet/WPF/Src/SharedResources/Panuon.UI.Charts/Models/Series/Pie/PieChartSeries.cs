using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Charts
{
    public class PieChartSeries : ChartSeriesBase
    {
        #region Ctor
        #endregion

        #region Properties

        #region Value
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(PieChartSeries), new ChartSeriesPropertyMetadata(null));
        #endregion

        #region ValueMemberPath
        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }

        public static readonly DependencyProperty ValueMemberPathProperty =
            DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(PieChartSeries), new ChartSeriesPropertyMetadata(null));
        #endregion

        #region DoubleValue
        public double DoubleValue => GetDoubleValue();
        #endregion

        #endregion

        #region Internal Methods
        private double GetDoubleValue()
        {
            return double.Parse(Value.ToString());
        }
        #endregion
    }
}
