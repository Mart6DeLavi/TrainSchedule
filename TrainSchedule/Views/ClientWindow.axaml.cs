using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainSchedule.ViewModels;

namespace TrainSchedule.Views;

public partial class ClientWindow : Window
{
    public ClientWindow()
    {
        InitializeComponent();
        DataContext = new ClientViewModel();
    }

    private void OnSearchClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is ClientViewModel vm)
        {
            vm.SearchRoutes();
        }
    }
}