using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

public class AsyncObservableCollection<T> : ObservableCollection<T> {
    private SynchronizationContext synchronizationContext = SynchronizationContext.Current;

    public AsyncObservableCollection() {}

    public AsyncObservableCollection(IEnumerable<T> list) : base(list){}

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
        if (SynchronizationContext.Current == synchronizationContext)
            // Execute the CollectionChanged event on the current thread
            RaiseCollectionChanged(e); 
        else
            synchronizationContext.Send(RaiseCollectionChanged, e); // Raises the CollectionChanged event on the creator thread
    }

    private void RaiseCollectionChanged(object param) {
        // We are in the creator thread, call the base implementation directly
        base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
        if (SynchronizationContext.Current == synchronizationContext)
            RaisePropertyChanged(e); // Execute the PropertyChanged event on the current thread
        else
            synchronizationContext.Send(RaisePropertyChanged, e); // Raises the PropertyChanged event on the creator thread
    }

    private void RaisePropertyChanged(object param) {
        base.OnPropertyChanged((PropertyChangedEventArgs)param); // We are in the creator thread, call the base implementation directly
    }
}
