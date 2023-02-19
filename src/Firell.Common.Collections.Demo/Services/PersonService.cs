using System.Collections.Generic;

using Firell.Common.Collections.Demo.ViewModels;

namespace Firell.Common.Collections.Demo.Services;

public static class PersonService
{
    public static IEnumerable<PersonViewModel> FetchPersons()
    {
        yield return new PersonViewModel("Richard", "Rogers", Gender.Male);
        yield return new PersonViewModel("Sarah", "Farrell", Gender.Female);
        yield return new PersonViewModel("Sabrina", "Montgomery", Gender.Female);
        yield return new PersonViewModel("Sydney", "Richardson", Gender.Female);
        yield return new PersonViewModel("Alberta", "Davis", Gender.Female);
        yield return new PersonViewModel("Adrianna", "Harper", Gender.Female);
        yield return new PersonViewModel("Amy", "Morrison", Gender.Female);
        yield return new PersonViewModel("Evelyn", "Hunt", Gender.Female);
        yield return new PersonViewModel("Arnold", "Morris", Gender.Male);
        yield return new PersonViewModel("Jenna", "Sullivan", Gender.Female);
        yield return new PersonViewModel("Samantha", "Nelson", Gender.Female);
        yield return new PersonViewModel("Kristian", "Ryan", Gender.Male);
        yield return new PersonViewModel("Victoria", "Cooper", Gender.Female);
        yield return new PersonViewModel("James", "Riley", Gender.Male);
        yield return new PersonViewModel("Alexander", "Cole", Gender.Male);
        yield return new PersonViewModel("George", "Hamilton", Gender.Male);
        yield return new PersonViewModel("Cadie", "Payne", Gender.Female);
        yield return new PersonViewModel("Rubie", "Gray", Gender.Female);
        yield return new PersonViewModel("Lilianna", "Hunt", Gender.Female);
        yield return new PersonViewModel("Ned", "Henderson", Gender.Male);
        yield return new PersonViewModel("Adrian", "Douglas", Gender.Male);
        yield return new PersonViewModel("Fenton", "Owens", Gender.Male);
        yield return new PersonViewModel("Paige", "Phillips", Gender.Female);
        yield return new PersonViewModel("Charlie", "Murray", Gender.Male);
        yield return new PersonViewModel("Wilson", "Williams", Gender.Male);
        yield return new PersonViewModel("Kirsten", "Harrison", Gender.Female);
        yield return new PersonViewModel("James", "Dixon", Gender.Male);
        yield return new PersonViewModel("Arianna", "Warren", Gender.Female);
        yield return new PersonViewModel("Alisa", "Baker", Gender.Female);
        yield return new PersonViewModel("Clark", "Stevens", Gender.Male);
    }
}
