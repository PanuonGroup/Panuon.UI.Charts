using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts.Internal.Controls
{
    internal abstract class DrawingElementBase : FrameworkElement
    {
        #region Ctor
        public DrawingElementBase(ChartSeriesBase seriesBase)
        {
            SeriesBase = seriesBase;
            seriesBase.InvalidDrawing += SeriesBase_InvalidDrawing;
        }
        #endregion

        #region Properties
        public ChartSeriesBase SeriesBase { get; }
        #endregion

        #region Virtual Methods
        protected virtual void OnInvalidDrawing() { }
        #endregion

        #region Event Handlers
        private void SeriesBase_InvalidDrawing(object sender, EventArgs e)
        {
            InvalidateVisual();
            OnInvalidDrawing();
        }
        #endregion
    }
}
