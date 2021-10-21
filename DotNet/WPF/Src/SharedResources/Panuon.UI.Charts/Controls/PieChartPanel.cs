using Panuon.UI.Charts.Internal.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts
{
    public class PieChartPanel : ChartPanelBase
    {
        #region Fields
        private PieChartPresenter _presenter;
        #endregion

        #region Ctor
        public PieChartPanel()
        {
            _presenter = new PieChartPresenter(this);
            AddVisualChild(_presenter);
        }
        #endregion

        #region Properties

        #region Spacing
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register("Spacing", typeof(double), typeof(PieChartPanel), new ChartPropertyMetadata(3d));
        #endregion

        #region Radius
        public double? Radius
        {
            get { return (double?)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double?), typeof(PieChartPanel), new ChartPropertyMetadata(null));
        #endregion

        #region CornerRadius
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(PieChartPanel), new ChartPropertyMetadata(0d));
        #endregion

        #endregion

        #region Overrides
        protected override Size MeasureOverride(Size availableSize)
        {
            _presenter.Measure(availableSize);
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _presenter.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            return base.ArrangeOverride(finalSize);
        }
        #endregion
    }
}
