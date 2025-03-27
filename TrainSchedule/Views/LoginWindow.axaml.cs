using System;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using TrainSchedule.Config;

namespace TrainSchedule.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }

    private void OnLoginClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not LoginViewModel vm)
            return;

        using var db = new DatabaseContext();

        var user = db.Users.FirstOrDefault(u => u.Username == vm.Username);
        if (user == null)
        {
            Console.WriteLine("❌ User not found");
            return;
        }

        if (!BCrypt.Net.BCrypt.Verify(vm.Password, user.Password))
        {
            Console.WriteLine("❌ Incorrect password");
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
            Console.WriteLine("❌ Unknown role.");
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
}
