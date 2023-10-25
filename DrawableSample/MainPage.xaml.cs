using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace DrawableSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}

public sealed partial class MainPageViewModel : ObservableObject
{
    public ObservableCollection<Person> People { get; private set; } = new();

    public MainPageViewModel()
    {
        AddPersons();
    }

    private void AddPersons()
    {
        for (int index = 1; index < 6; index++)
        {
            People.Add(new Person() { Id = index, Name = $"Random Person {index}" });
        }
    }
}


public sealed record Person
{
    public int Id { get; init; }
    public string Name { get; init; }
}