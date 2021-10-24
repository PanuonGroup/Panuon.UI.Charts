using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Windows.Media;
using System.Linq;
using System.Collections;

namespace Panuon.UI.Charts
{
    [ContentProperty(nameof(Series))]
    public abstract class ChartPanelBase : FrameworkElement
    {
        #region Fields
        protected readonly List<UIElement> _visualChildren = new List<UIElement>();

        protected readonly List<DependencyObject> _logicalChildren = new List<DependencyObject>();

        #endregion

        #region Ctor
        public ChartPanelBase()
        {
            Series = new SeriesCollection();
        }
        #endregion

        #region Events
        public event SeriesChangedEventHandler SeriesChanged;

        public event EventHandler InvalidDrawing;
        #endregion

        #region Properties

        #region Series
        public SeriesCollection Series
        {
            get { return (SeriesCollection)GetValue(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        public static readonly DependencyProperty SeriesProperty =
            DependencyProperty.Register("Series", typeof(SeriesCollection), typeof(ChartPanelBase), new PropertyMetadata(OnSeriesChanged));
        #endregion

        #region LegendVisibility
        public Visibility LegendVisibility
        {
            get { return (Visibility)GetValue(LegendVisibilityProperty); }
            set { SetValue(LegendVisibilityProperty, value); }
        }

        public static readonly DependencyProperty LegendVisibilityProperty =
            DependencyProperty.Register("LegendVisibility", typeof(Visibility), typeof(ChartPanelBase), new PropertyMetadata(Visibility.Visible));
        #endregion

        #region LegendStyle
        public Style LegendStyle
        {
            get { return (Style)GetValue(LegendStyleProperty); }
            set { SetValue(LegendStyleProperty, value); }
        }

        public static readonly DependencyProperty LegendStyleProperty =
            DependencyProperty.Register("LegendStyle", typeof(Style), typeof(ChartPanelBase));
        #endregion

        #region LegendPlacement
        public LegendPlacement LegendPlacement
        {
            get { return (LegendPlacement)GetValue(LegendPlacementProperty); }
            set { SetValue(LegendPlacementProperty, value); }
        }

        public static readonly DependencyProperty LegendPlacementProperty =
            DependencyProperty.Register("LegendPlacement", typeof(LegendPlacement), typeof(ChartPanelBase), new FrameworkPropertyMetadata(LegendPlacement.Left, FrameworkPropertyMetadataOptions.AffectsArrange));
        #endregion

        #region LegendSpacing
        public double LegendSpacing
        {
            get { return (double)GetValue(LegendSpacingProperty); }
            set { SetValue(LegendSpacingProperty, value); }
        }

        public static readonly DependencyProperty LegendSpacingProperty =
            DependencyProperty.Register("LegendSpacing", typeof(double), typeof(ChartPanelBase), new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsArrange));
        #endregion

        #endregion

        #region Protected Properties
        protected void AddVisualChild(UIElement child)
        {
            _visualChildren.Add(child);
            base.AddVisualChild(child);
        } 

        protected void AddLogicalChild(DependencyObject child)
        {
            _logicalChildren.Add(child);
            base.AddLogicalChild(child);
        }
        #endregion

        #region Overrides
        protected override int VisualChildrenCount => _visualChildren.Count;

        protected override Visual GetVisualChild(int index) => _visualChildren[index];
        #endregion

        #region Methods
        public void InvalidateDrawing()
        {
            InvalidDrawing?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Event Handlers
        private static void OnSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chartPanel = (ChartPanelBase)d;

            var addedItems = new List<ChartSeriesBase>();
            var removedItems = new List<ChartSeriesBase>();

            if (e.OldValue is SeriesCollection oldSeries)
            {
                removedItems = oldSeries.ToList();
                oldSeries.CollectionChanged -= chartPanel.Series_CollectionChanged;
            }
            if (e.NewValue is SeriesCollection newSeries)
            {
                addedItems = newSeries.ToList();
                if (newSeries.Any())
                {
                    foreach (ChartSeriesBase item in newSeries)
                    {
                        if (item != null)
                        {
                            chartPanel.AddLogicalChild(item);
                        }
                    }
                }
                newSeries.CollectionChanged += chartPanel.Series_CollectionChanged;
            }

            if (removedItems.Any() || addedItems.Any())
            {
                chartPanel.SeriesChanged?.Invoke(chartPanel, new SeriesChangedEventArgs(addedItems, removedItems));
            }

            chartPanel.InvalidateArrange();
        }

        private void Series_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var newItems = new List<ChartSeriesBase>();
            var removedItems = new List<ChartSeriesBase>();

            if (e.NewItems != null)
            {
                foreach (ChartSeriesBase item in e.NewItems)
                {
                    if (item != null)
                    {
                        newItems.Add(item);
                        AddLogicalChild(item);
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (ChartSeriesBase item in e.OldItems)
                {
                    if (item != null)
                    {
                        removedItems.Add(item);
                        RemoveLogicalChild(item);
                    }
                }
            }

            SeriesChanged?.Invoke(this, new SeriesChangedEventArgs(newItems, removedItems));
        }

        #endregion
    }
}
