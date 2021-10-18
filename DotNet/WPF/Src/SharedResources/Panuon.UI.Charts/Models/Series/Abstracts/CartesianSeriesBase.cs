using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Panuon.UI.Charts
{
    public abstract class CartesianSeriesBase : ChartSeriesBase
    {
        #region Methods
        public virtual void BeginDraw(DrawingContext drawingContext) { }

        public abstract void Draw(DrawingContext drawingContext);

        public virtual void EndDraw(DrawingContext drawingContext) { }
        #endregion
    }
}
