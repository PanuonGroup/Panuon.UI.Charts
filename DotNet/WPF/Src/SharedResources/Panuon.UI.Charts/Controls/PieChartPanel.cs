using Panuon.UI.Charts.Internal.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Panuon.UI.Charts
{
    public class PieChartPanel : ChartPanelBase
    {
        #region Fields
        private PieChartPresenter _presenter;

        private ChartLegend _chartLegend;
        #endregion

        #region Ctor
        public PieChartPanel()
        {
            _presenter = new PieChartPresenter(this);
            AddVisualChild(_presenter);

            _chartLegend = new ChartLegend();
            _chartLegend.SetBinding(ChartLegend.StyleProperty, new Binding()
            {
                Path = new PropertyPath(LegendStyleProperty),
                Source = this,
            });
            _chartLegend.SetBinding(ChartLegend.VisibilityProperty, new Binding()
            {
                Path = new PropertyPath(LegendVisibilityProperty),
                Source = this,
            });
            _chartLegend.SetBinding(ChartLegend.ItemsSourceProperty, new Binding()
            {
                Path = new PropertyPath(SeriesProperty),
                Source = this,
            });
            AddVisualChild(_chartLegend);
        }
        #endregion

        #region Properties

        #region SeriesSpacing
        public double SeriesSpacing
        {
            get { return (double)GetValue(SeriesSpacingProperty); }
            set { SetValue(SeriesSpacingProperty, value); }
        }

        public static readonly DependencyProperty SeriesSpacingProperty =
            DependencyProperty.Register("SeriesSpacing", typeof(double), typeof(PieChartPanel), new ChartPropertyMetadata(3d));
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
            _chartLegend.Measure(availableSize);
            _presenter.Measure(availableSize);
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var legendSpacing = LegendSpacing ;
            _chartLegend.Arrange(new Rect(finalSize.Width - legendSpacing - _chartLegend.DesiredSize.Width,
                (finalSize.Height - _chartLegend.DesiredSize.Height - legendSpacing * 2) / 2,
                _chartLegend.DesiredSize.Width,
                _chartLegend.DesiredSize.Height));
            _presenter.Arrange(new Rect(0, 0, finalSize.Width - legendSpacing * 2 - _chartLegend.DesiredSize.Width, finalSize.Height));
            return base.ArrangeOverride(finalSize);
        }
        #endregion
    }
}
