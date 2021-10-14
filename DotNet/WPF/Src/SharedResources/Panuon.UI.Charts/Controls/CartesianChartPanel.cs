using Panuon.UI.Charts.Internal.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Panuon.UI.Charts
{
    public class CartesianChartPanel : FrameworkElement
    {
        #region Fields
        private static XAxis _defaultXAxis;

        private XAxisPresenter _xAxisPresenter;

        private YAxisPresenter _yAxisPresenter;

        private ChartsPresenter _chartsPresenter;

        private ObservableCollection<XAxis> _xAxes;

        private ObservableCollection<YAxis> _yAxes;

        private readonly List<UIElement> _visualChildren;

        private readonly List<XAxis> _logicalXAxes;

        private readonly List<YAxis> _logicalYAxes;
        #endregion

        #region Ctor
        static CartesianChartPanel()
        {
            _defaultXAxis = new XAxis();
        }

        public CartesianChartPanel()
        {
            _visualChildren = new List<UIElement>();

            _xAxes = new ObservableCollection<XAxis>();
            _xAxes.CollectionChanged += Axes_CollectionChanged;

            _yAxes = new ObservableCollection<YAxis>();
            _yAxes.CollectionChanged += Axes_CollectionChanged;

            _xAxisPresenter = new XAxisPresenter(() => XAxes);
            _visualChildren.Add(_xAxisPresenter);
            AddVisualChild(_xAxisPresenter);

            _yAxisPresenter = new YAxisPresenter(() => YAxes);
            _visualChildren.Add(_yAxisPresenter);
            AddVisualChild(_yAxisPresenter);
        }

        #endregion

        #region Properties

        #region XAxis
        public ObservableCollection<XAxis> XAxes => _xAxes;
        #endregion

        #region YAxis
        public ObservableCollection<YAxis> YAxes => _yAxes;
        #endregion

        #region Series
        #endregion

        #endregion

        #region Overrides

        #region VisualChildrenCount
        protected override int VisualChildrenCount => _visualChildren.Count;
        #endregion

        #region GetVisualChild
        protected override Visual GetVisualChild(int index) => _visualChildren[index];
        #endregion

        #region MeasureOverride
        protected override Size MeasureOverride(Size availableSize)
        {
            _xAxisPresenter.Measure(availableSize);
            _yAxisPresenter.Measure(availableSize);

            return base.MeasureOverride(availableSize);
        }
        #endregion

        #region ArrangeOverride
        protected override Size ArrangeOverride(Size finalSize)
        {
            _xAxisPresenter.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            _yAxisPresenter.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));

            return base.ArrangeOverride(finalSize);
        }
        #endregion

        #region OnRender
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
        #endregion

        #endregion

        #region Event Handlers
        private void Axes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach(var )
            if (oldAxis != null)
            {
                oldAxis.Redraw -= Axis_Redraw;
                RemoveLogicalChild(oldAxis);
            }
            if (newAxis != null)
            {
                newAxis.Redraw += Axis_Redraw;
                AddLogicalChild(newAxis);
            }
            chart.Redraw(false);
        }

        private void Axis_Redraw(object sender, Internal.RedrawEventArgs e)
        {
            Redraw(e.ForceRender);
        }
        #endregion

        #region Functions
        private void Redraw(bool force)
        {
            _xAxisPresenter.InvalidateVisual();
            _yAxisPresenter.InvalidateVisual();

            if (force)
            {
                _xAxisPresenter.UpdateLayout();
                _yAxisPresenter.UpdateLayout();
            }
        }
        #endregion
    }
}
