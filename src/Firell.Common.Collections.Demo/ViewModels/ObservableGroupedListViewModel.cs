using System.Collections.Generic;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using Firell.Common.Collections.Demo.Services;

namespace Firell.Common.Collections.Demo.ViewModels;

public class ObservableGroupedListViewModel : ObservableObject
{
    public ObservableGroupedListViewModel()
    {
        Dictionary<string, PersonViewModel> persons = PersonService.FetchPersons().ToDictionary(x => x.FullName, y => y);
        GroupedPersons = new ObservableGroupedList<char, string, PersonViewModel>(x => x.FullName[0], persons);
    }

    public ObservableGroupedList<char, string, PersonViewModel> GroupedPersons { get; private set; }
}
