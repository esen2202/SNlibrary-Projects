﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace SN.Class.Derivations
{
    public class ObservableCollectionPropertyNotify<T> : ObservableCollection<T>
    {

        public void Refresh()
        {
            for (var i = 0; i < this.Count(); i++)
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }
}
