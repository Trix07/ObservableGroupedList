using System.Collections.Generic;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using Firell.Common.Collections.Demo.Services;

namespace Firell.Common.Collections.Demo.ViewModels;

public class ObservableGroupViewModel : ObservableObject
{
    public ObservableGroupViewModel()
    {
        Dictionary<string, PersonViewModel> persons = PersonService.FetchPersons().Where(x => x.Gender.Equals(Gender.Female)).ToDictionary(x => x.FullName, y => y);
        PersonGroup = new ObservableGroup<string, PersonViewModel>(Gender.Female, persons);
    }

    public ObservableGroup<string, PersonViewModel> PersonGroup { get; private set; }
}
