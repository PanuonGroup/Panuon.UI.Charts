using System;
using System.Windows;

namespace Panuon.UI.Charts.Internal.Controls
{
    internal abstract class DrawingElementBase : FrameworkElement
    {
        #region Ctor
        public DrawingElementBase(ChartPanelBase chartPanel,ChartSeriesBase series)
        {
            ChartPanel = chartPanel;
            chartPanel.InvalidDrawing += ChartPanel_InvalidDrawing;

            Series = series;
            series.InvalidDrawing += SeriesBase_InvalidDrawing;
        }
        #endregion

        #region Properties
        public ChartPanelBase ChartPanel { get; }
        public ChartSeriesBase Series { get; }
        #endregion

        #region Virtual Methods
        protected virtual void OnInvalidDrawing() { }
        #endregion

        #region Event Handlers
        private void ChartPanel_InvalidDrawing(object sender, EventArgs e)
        {
            InvalidateVisual();
            OnInvalidDrawing();
        }

        private void SeriesBase_InvalidDrawing(object sender, EventArgs e)
        {
            InvalidateVisual();
            OnInvalidDrawing();
        }
        #endregion
    }
}
