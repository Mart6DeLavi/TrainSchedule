using System;
using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using TrainSchedule.Views;

public class LoginViewModel : ReactiveObject
{
    private string _username = "";
    private string _password = "";

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = ReactiveCommand.Create(ExecuteLogin);
    }

    private void ExecuteLogin()
    {
        try
        {
            var scheduleWindow = new ScheduleWindow();
            scheduleWindow.Show();
            CloseLoginWindow();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
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
}