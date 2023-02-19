using System.Collections.Generic;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using Firell.Common.Collections.Demo.Services;

namespace Firell.Common.Collections.Demo.ViewModels;

public class ObservableSortedListViewModel : ObservableObject
{
    private readonly IComparer<string> _personComparer;

    public ObservableSortedListViewModel()
    {
       _personComparer = Comparer<string>.Create((x, y) => y.CompareTo(x));
        SortedPersons = new ObservableSortedList<string, PersonViewModel>(PersonService.FetchPersons().ToDictionary(x => x.FullName, y => y), _personComparer);
    }

    public ObservableSortedList<string, PersonViewModel> SortedPersons { get; private set; }
}
