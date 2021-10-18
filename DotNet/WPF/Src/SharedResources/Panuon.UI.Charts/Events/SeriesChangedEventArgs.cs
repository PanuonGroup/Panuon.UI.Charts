using System;
using System.Collections.Generic;

namespace Panuon.UI.Charts
{
    public class SeriesChangedEventArgs : EventArgs
    {
        #region Ctor
        public SeriesChangedEventArgs(IList<ChartSeriesBase> addedSeries, IList<ChartSeriesBase> removedSeries)
        {
            AddedSeries = addedSeries;
            RemovedSeries = removedSeries;
        }
        #endregion

        #region Properties

        public IList<ChartSeriesBase> AddedSeries { get; }

        public IList<ChartSeriesBase> RemovedSeries { get; }

        #endregion
    }
}
