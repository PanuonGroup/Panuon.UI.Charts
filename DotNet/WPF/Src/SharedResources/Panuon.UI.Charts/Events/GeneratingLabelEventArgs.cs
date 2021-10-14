using System;
using System.Windows.Media;

namespace Panuon.UI.Charts
{
    public class GeneratingLabelEventArgs : EventArgs
    {
        #region Ctor
        public GeneratingLabelEventArgs(object value, double doubleValue)
        {
            Value = value;
            DoubleValue = doubleValue;
        }
        #endregion

        #region Properties

        public object Value { get; }

        public double DoubleValue { get; }

        public string Label { get; set; }

        public Brush LabelForeground { get; set; }

        #endregion
    }
}
