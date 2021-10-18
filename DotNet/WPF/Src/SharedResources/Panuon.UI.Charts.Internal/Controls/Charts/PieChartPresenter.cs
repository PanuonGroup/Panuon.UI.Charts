using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Charts.Internal.Controls
{
    internal class PieChartPresenter : FrameworkElement
    {
        #region Fields
        private PieChartPanel _pieChartPanel;

        private readonly List<ArcDrawingElement> _arcDrawingElements = new List<ArcDrawingElement>();
        #endregion

        #region Ctor
        public PieChartPresenter(PieChartPanel chartPanel)
        {
            _pieChartPanel = chartPanel;

            GenerateFromSeries();
            chartPanel.SeriesChanged += ChartPanel_SeriesChanged;
        }
        #endregion

        #region Overrides

        #region VisualChildrenCount
        protected override int VisualChildrenCount => _arcDrawingElements.Count;
        #endregion

        #region GetVisualChild
        protected override Visual GetVisualChild(int index) => _arcDrawingElements[index];
        #endregion

        #region MeasureOverride
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach(var arcDrawingElement in _arcDrawingElements)
            {
                arcDrawingElement.Measure(availableSize);
            }
            return base.MeasureOverride(availableSize);
        }
        #endregion

        #region ArrangeOverride
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (var arcDrawingElement in _arcDrawingElements)
            {
                arcDrawingElement.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            }
            return base.ArrangeOverride(finalSize);
        }
        #endregion

        #endregion

        #region Event Handlers
        private void ChartPanel_SeriesChanged(object sender, SeriesChangedEventArgs e)
        {
            if (e.RemovedSeries != null && e.RemovedSeries.Any())
            {
                for (int i = _arcDrawingElements.Count - 1; i >= 0; i--)
                {
                    var arcDrawingElement = _arcDrawingElements[i];
                    if (e.RemovedSeries.Contains(arcDrawingElement.SeriesBase))
                    {
                        _arcDrawingElements.RemoveAt(i);
                        RemoveVisualChild(arcDrawingElement);
                    }
                }
            }
            if (e.AddedSeries != null && e.AddedSeries.Any())
            {
                foreach (var seriesBase in e.AddedSeries)
                {
                    var arcDrawingElement = new ArcDrawingElement(seriesBase);
                    _arcDrawingElements.Add(arcDrawingElement);
                    AddVisualChild(arcDrawingElement);
                }
            }
            InvalidateVisual();
        }
        #endregion

        #region Functions
        private void GenerateFromSeries()
        {
            if (_pieChartPanel.Series != null)
            {
                foreach (var seriesBase in _pieChartPanel.Series)
                {
                    var arcDrawingElement = new ArcDrawingElement(seriesBase);
                    _arcDrawingElements.Add(arcDrawingElement);
                    AddVisualChild(arcDrawingElement);
                }
            }
            InvalidateVisual();
        }

        #endregion
    }
}
