using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Panuon.UI.Charts.Internal.Controls
{
    internal class YAxisPresenter : FrameworkElement
    {
        #region Field
        private Func<IEnumerable<YAxis>> _getAxisCallback;
        #endregion

        #region Ctor
        public YAxisPresenter(Func<IEnumerable<YAxis>> getAxisCallback)
        {
            _getAxisCallback = getAxisCallback;
        }
        #endregion

        #region Properties

        public IEnumerable<YAxis> Axes => _getAxisCallback();
        #endregion       

        #region Overrides
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
        }
        #endregion
    }
}
