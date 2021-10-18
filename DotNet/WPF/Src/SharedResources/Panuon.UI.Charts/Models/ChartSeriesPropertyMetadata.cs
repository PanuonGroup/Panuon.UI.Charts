using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts
{
    public class ChartSeriesPropertyMetadata : PropertyMetadata
    {
        #region Ctor
        public ChartSeriesPropertyMetadata(object defaultValue)
            : base(defaultValue)
        {
            SetPropertyChangedCallback();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ChartSeriesPropertyMetadata(object defaultValue,
        PropertyChangedCallback propertyChangedCallback)
            : base(defaultValue)
        {
            SetPropertyChangedCallback(propertyChangedCallback);
        }
      
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ChartSeriesPropertyMetadata(object defaultValue, CoerceValueCallback coerceValueCallback)
            : this(defaultValue)
        {
            CoerceValueCallback = coerceValueCallback;
        }

        /// <summary>
        /// 绘制属性元数据。当该属性发生变化时，将触发重绘（如果该控件继承自DrawingElementBase）。
        /// </summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ChartSeriesPropertyMetadata(object defaultValue,
                                          PropertyChangedCallback propertyChangedCallback,
                                          CoerceValueCallback coerceValueCallback)
            : this(defaultValue, propertyChangedCallback)
        {
            CoerceValueCallback = coerceValueCallback;
        }
        #endregion

        #region Functions
        private void SetPropertyChangedCallback(PropertyChangedCallback propertyChangedCallback = null)
        {
            PropertyChangedCallback = new PropertyChangedCallback((d, e) =>
            {
                if (d is ChartSeriesBase chartSeries)
                {
                    chartSeries.InvalidateDrawing();
                }
                propertyChangedCallback?.Invoke(d, e);
            });
        }
        #endregion
    }
}
