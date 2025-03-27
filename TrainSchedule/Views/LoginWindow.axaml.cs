using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Media;
using TrainSchedule.Config;
using Avalonia.Layout;  // <-- Для HorizontalAlignment, VerticalAlignment

namespace TrainSchedule.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }

    // Обработчик входа сделан асинхронным
    private async void OnLoginClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not LoginViewModel vm)
            return;

        using var db = new DatabaseContext();

        var user = db.Users.FirstOrDefault(u => u.Username == vm.Username);
        if (user == null)
        {
            await ShowErrorDialog("User not found");
            return;
        }

        if (!BCrypt.Net.BCrypt.Verify(vm.Password, user.Password))
        {
            await ShowErrorDialog("Incorrect password");
            return;
        }

        Console.WriteLine($"✅ Login completed. User's role: {user.Role}");

        if (user.Role == "ADMIN")
        {
            var scheduleWindow = new ScheduleWindow();
            scheduleWindow.Show();
        }
        else if (user.Role == "USER")
        {
            var clientWindow = new ClientWindow();
            clientWindow.Show();
        }
        else
        {
            await ShowErrorDialog("Unknown role.");
            return;
        }

        CloseLoginWindow();
    }

    private void CloseLoginWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            foreach (var window in desktop.Windows)
            {
                if (window is LoginWindow loginWindow)
                {
                    loginWindow.Close();
                    break;
                }
            }
        }
    }

    private void OpenScheduleWindow(object? sender, RoutedEventArgs e)
    {
        var registrationWindow = new RegistrationWindow();
        registrationWindow.Show();
        this.Close();
    }
    
    private async Task ShowErrorDialog(string message)
    {
        var dialog = new Window
        {
            Title = "Error",
            Width = 400,
            Height = 150,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        var stack = new StackPanel
        {
            Spacing = 10,
            HorizontalAlignment = HorizontalAlignment.Center, 
            VerticalAlignment = VerticalAlignment.Center,     
            Margin = new Thickness(20)
        };

        var textBlock = new TextBlock
        {
            Text = message,
            Foreground = Brushes.Red,
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var button = new Button
        {
            Content = "OK",
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 0),
            Width = 80,
            
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center
        };


        button.Click += (s, args) => dialog.Close();

        stack.Children.Add(textBlock);
        stack.Children.Add(button);
        dialog.Content = stack;

        await dialog.ShowDialog(this);
    }
}
