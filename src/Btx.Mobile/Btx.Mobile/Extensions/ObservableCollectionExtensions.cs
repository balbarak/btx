using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Btx.Mobile.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void Sort<T>(this ObservableRangeCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);

            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }

        public static void Sort<TSource, TKey>(this ObservableRangeCollection<TSource> observableCollection, Func<TSource, TKey> keySelector)
        {

            var sorted = observableCollection.OrderBy(keySelector).ToList();

            observableCollection.Clear();

            foreach (var b in sorted)
            {
                observableCollection.Add(b);
            }
        }
    }
}
