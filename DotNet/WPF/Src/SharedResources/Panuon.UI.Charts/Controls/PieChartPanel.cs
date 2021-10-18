using Panuon.UI.Charts.Internal.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts
{
    public class PieChartPanel : ChartPanelBase
    {
        #region Fields
        private PieChartPresenter _presenter;
        #endregion

        #region Ctor
        public PieChartPanel()
        {
            _presenter = new PieChartPresenter(this);
            AddVisualChild(_presenter);
        }
        #endregion

        #region Properties



        #endregion

        #region Overrides
        protected override Size MeasureOverride(Size availableSize)
        {
            _presenter.Measure(availableSize);
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _presenter.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            return base.ArrangeOverride(finalSize);
        }
        #endregion
    }
}
