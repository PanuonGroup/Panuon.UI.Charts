using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Panuon.UI.Charts
{
    public class SeriesCollection : ObservableCollection<ChartSeriesBase>
    {
        #region Overrides
        public new void Clear()
        {
            var items = Items.ToList();
            base.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items));
        }
        #endregion
    }
}
