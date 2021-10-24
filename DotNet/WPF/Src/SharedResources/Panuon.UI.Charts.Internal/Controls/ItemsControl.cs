using System;
using System.Collections.Generic;
using System.Text;

namespace Panuon.UI.Charts.Internal.Controls
{
    internal class ItemsControl : System.Windows.Controls.ItemsControl
    {
        #region Overrides
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return false;
        }
        #endregion
    }
}
