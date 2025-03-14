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
    private char _passwordChar = '●'; // Символ для скрытия пароля

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

    // Свойство для управления видимостью пароля
    public bool IsPasswordVisible
    {
        get => _isPasswordVisible;
        set
        {
            this.RaiseAndSetIfChanged(ref _isPasswordVisible, value);
            this.RaisePropertyChanged(nameof(PasswordChar));
        }
    }

    public char PasswordChar => IsPasswordVisible ? '\0' : '●'; // '\0' — показать пароль, '●' — скрыть

    public ReactiveCommand<Unit, Unit> TogglePasswordVisibilityCommand { get; }

    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

    public RegistrationViewModel()
    {
        RegisterCommand = ReactiveCommand.Create(ExecuteRegister);

        // Команда для переключения видимости пароля
        TogglePasswordVisibilityCommand = ReactiveCommand.Create(() =>
        {
            IsPasswordVisible = !IsPasswordVisible;
        });
    }

    private void ExecuteRegister()
    {
        using (var db = new DatabaseContext())
        {
            var newUser = new User
            {
                Name = this.Name,
                SecondName = this.SecondName,
                Username = this.Username,
                Email = this.Email,
                Password = this.Password,  // В реальном проекте используй хеширование паролей!
                PhoneNumber = this.PhoneNumber
            };

            db.Users.Add(newUser);
            db.SaveChanges();
            Console.WriteLine("User successfully registered!");
        }
    }
}