using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts
{
    public class ChartPropertyMetadata : FrameworkPropertyMetadata
    {
        #region Ctor
        public ChartPropertyMetadata(object defaultValue)
            : base(defaultValue)
        {
            SetPropertyChangedCallback();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ChartPropertyMetadata(object defaultValue,
        PropertyChangedCallback propertyChangedCallback)
            : base(defaultValue)
        {
            SetPropertyChangedCallback(propertyChangedCallback);
        }
      
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ChartPropertyMetadata(object defaultValue, 
            CoerceValueCallback coerceValueCallback)
            : base(defaultValue, null, coerceValueCallback)
        {
            SetPropertyChangedCallback();
        }

        public ChartPropertyMetadata(object defaultValue,
           FrameworkPropertyMetadataOptions flags)
           : base(defaultValue, flags)
        {
            SetPropertyChangedCallback();
        }

        /// <summary>
        /// 绘制属性元数据。当该属性发生变化时，将触发重绘（如果该控件继承自DrawingElementBase）。
        /// </summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ChartPropertyMetadata(object defaultValue,
                                          PropertyChangedCallback propertyChangedCallback,
                                          CoerceValueCallback coerceValueCallback)
            : base(defaultValue, FrameworkPropertyMetadataOptions.None, null, coerceValueCallback)
        {
            SetPropertyChangedCallback(propertyChangedCallback);
        }

        public ChartPropertyMetadata(object defaultValue,
            FrameworkPropertyMetadataOptions flags,
            PropertyChangedCallback propertyChangedCallback,
            CoerceValueCallback coerceValueCallback)
            : base(defaultValue, flags, null, coerceValueCallback)
        {
            SetPropertyChangedCallback(propertyChangedCallback);
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
                if(d is ChartPanelBase chartPanel)
                {
                    chartPanel.InvalidateDrawing();
                }
                propertyChangedCallback?.Invoke(d, e);
            });
        }
        #endregion
    }
}
