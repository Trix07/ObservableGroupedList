using System;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Firell.Common.Collections.Demo.ViewModels;

public class PersonViewModel : ObservableObject
{
    public PersonViewModel()
    {
        ID = Guid.NewGuid();
    }

    public PersonViewModel(string firstName, string lastName, Gender gender) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
    }

    private Guid _id;
    public Guid ID
    {
        get => _id;
        private set => SetProperty(ref _id, value);
    }

    private string _firstName = string.Empty;
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (SetProperty(ref _firstName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set
        {
            if (SetProperty(ref _lastName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    public string FullName { get => $"{FirstName} {LastName}"; }

    private Gender _gender;
    public Gender Gender
    {
        get => _gender;
        set => SetProperty(ref _gender, value);
    }
}

public enum Gender
{
    Unknown,
    Female,
    Male,
}
