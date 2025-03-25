
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainSchedule.Config;
using TrainSchedule.Entity;
using TrainSchedule.ViewModels;

namespace TrainSchedule.Views;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
        DataContext = new RegistrationViewModel();
    }

    private void OnRegisterClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is RegistrationViewModel vm)
        {
            var newUser = new User
            {
                Name = vm.Name,
                SecondName = vm.SecondName,
                Username = vm.Username,
                Email = vm.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                PhoneNumber = vm.PhoneNumber
            };

            using var db = new DatabaseContext();
            db.Users.Add(newUser);
            db.SaveChanges();

            Console.WriteLine("User successfully registered!");
        }
    }
}