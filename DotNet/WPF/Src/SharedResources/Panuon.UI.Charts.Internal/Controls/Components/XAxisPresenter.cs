using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Panuon.UI.Charts.Internal.Controls
{
    internal class XAxisPresenter : FrameworkElement
    {
        #region Field
        private Func<IEnumerable<XAxis>> _getAxisCallback;
        #endregion

        #region Ctor
        public XAxisPresenter(Func<IEnumerable<XAxis>> getAxisCallback)
        {
            _getAxisCallback = getAxisCallback;
        }
        #endregion

        #region Properties

        public IEnumerable<XAxis> Axes => _getAxisCallback();
        #endregion
    }
}
