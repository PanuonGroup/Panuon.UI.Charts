using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.UI.Charts
{
    public class ChartLegend : Control
    {
        #region Ctor
        static ChartLegend()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartLegend), new FrameworkPropertyMetadata(typeof(ChartLegend)));
        }
        #endregion

        #region Properties

        #region ItemTemplate
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ChartLegend));
        #endregion




        #endregion


        #region Internal Properties

        #region ItemsSource
        internal IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        internal static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ChartLegend));
        #endregion

        #endregion

    }
}
