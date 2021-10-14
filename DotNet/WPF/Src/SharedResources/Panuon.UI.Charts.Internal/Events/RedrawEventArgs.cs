using System;

namespace Panuon.UI.Charts.Internal
{
    internal class RedrawEventArgs : EventArgs
    {
        internal RedrawEventArgs(bool forceRender) 
        {
            ForceRender = forceRender;
        }

        #region Properties
        public bool ForceRender { get; }
        #endregion
    }
}
