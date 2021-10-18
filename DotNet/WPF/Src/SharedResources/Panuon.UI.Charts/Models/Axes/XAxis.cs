using Panuon.UI.Charts.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts
{
    public class XAxis : AxisBase, IXAxis
    {
        #region Ctor
        public XAxis()
        {

        }
        #endregion

        #region Events
        internal event RedrawEventHandler Redraw;

        public event GeneratingLabelEventHandler GeneratingLabel;
        #endregion

        #region Properties

        #region Values
        public IEnumerable Values
        {
            get { return (IEnumerable)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(IEnumerable), typeof(XAxis));
        #endregion

        #region ValueMemberPath
        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }

        public static readonly DependencyProperty ValueMemberPathProperty =
            DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(XAxis));
        #endregion

        #endregion

        #region Event Handlers
        #endregion

        #region Functions
        #endregion
    }
}
