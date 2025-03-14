using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TrainSchedule.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void OpenScheduleWindow(object? sender, RoutedEventArgs e)
        {
            
            var scheduleWindow = new RegistrationWindow();
            scheduleWindow.Show();
        }
    }
}