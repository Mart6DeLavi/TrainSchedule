using System;
using System.Reactive;
using TrainSchedule.Config;
using TrainSchedule.Entity;

namespace TrainSchedule.ViewModels;

using ReactiveUI;

public class RegistrationViewModel : ReactiveObject
{
    private string _name = "";
    private string _secondName = "";
    private string _username = "";
    private string _email = "";
    private string _password = "";
    private string _phoneNumber = "";
    private bool _isPasswordVisible;
    private char _passwordChar = '●';

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string SecondName
    {
        get => _secondName;
        set => this.RaiseAndSetIfChanged(ref _secondName, value);
    }

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
    }
    
    public bool IsPasswordVisible
    {
        get => _isPasswordVisible;
        set
        {
            this.RaiseAndSetIfChanged(ref _isPasswordVisible, value);
            this.RaisePropertyChanged(nameof(PasswordChar));
        }
    }

    public char PasswordChar => IsPasswordVisible ? '\0' : '●';

    // Оставим только одну команду для переключения видимости пароля
    public ReactiveCommand<Unit, Unit> TogglePasswordVisibilityCommand { get; }

    public RegistrationViewModel()
    {
        TogglePasswordVisibilityCommand = ReactiveCommand.Create(() =>
        {
            IsPasswordVisible = !IsPasswordVisible;
        });
    }
}