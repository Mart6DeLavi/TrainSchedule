using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using TrainSchedule.Config;
using TrainSchedule.Entity;
using TrainSchedule.ViewModels;

namespace TrainSchedule.Views
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }

        // Сделали обработчик асинхронным
        private async void OnRegisterClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel vm)
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(vm.Name) ||
                    string.IsNullOrWhiteSpace(vm.SecondName) ||
                    string.IsNullOrWhiteSpace(vm.Username) ||
                    string.IsNullOrWhiteSpace(vm.Email) ||
                    string.IsNullOrWhiteSpace(vm.Password) ||
                    string.IsNullOrWhiteSpace(vm.PhoneNumber))
                {
                    await ShowErrorDialog("All fields are required.");
                    return;
                }

                // Создаём нового пользователя
                var newUser = new User
                {
                    Name = vm.Name,
                    SecondName = vm.SecondName,
                    Username = vm.Username,
                    Email = vm.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                    PhoneNumber = vm.PhoneNumber,
                    Role = "USER"
                };

                // Валидация объекта User с помощью DataAnnotations
                if (!User.ValidateUser(newUser, out List<string> errors))
                {
                    string errorMessage = string.Join("\n", errors);
                    await ShowErrorDialog(errorMessage);
                    return;
                }

                // Если всё ок, сохраняем пользователя в базу
                using var db = new DatabaseContext();
                db.Users.Add(newUser);
                db.SaveChanges();

                await ShowInfoDialog("User successfully registered!");
            }
        }
        
        private void OnBackToLoginClick(object? sender, RoutedEventArgs e)
        {
            // Предположим, у вас есть класс LoginWindow
            var loginWindow = new LoginWindow();
            loginWindow.Show();

            // Закрываем текущее окно регистрации
            this.Close();
        }

        // Вспомогательный метод для показа диалога с сообщением об ошибке
        private async Task ShowErrorDialog(string message)
        {
            // 1) Создаём само окно
            var dialog = new Window
            {
                Title = "Error",
                Width = 400,
                Height = 100,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            // 2) Создаём Grid, чтобы можно было центрировать содержимое
            var grid = new Grid();

            // Добавляем одну строку и один столбец, оба «звёздочные» (растягиваются на весь размер)
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // 3) Создаём StackPanel для текста и кнопки
            var stack = new StackPanel
            {
                Spacing = 10,
                // Выравниваем сам StackPanel по центру окна
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // 4) Создаём TextBlock с сообщением
            var textBlock = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.Red,
                // Выравниваем текст внутри TextBlock
                TextAlignment = TextAlignment.Center
            };

            // 5) Создаём кнопку «OK»
            var button = new Button
            {
                Content = "OK",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            // При нажатии — закрыть диалог
            button.Command = ReactiveCommand.Create(() => dialog.Close());

            // 6) Добавляем элементы в StackPanel
            stack.Children.Add(textBlock);
            stack.Children.Add(button);

            // 7) Помещаем StackPanel в Grid
            Grid.SetRow(stack, 0);
            Grid.SetColumn(stack, 0);
            grid.Children.Add(stack);

            // 8) Присваиваем Grid в окно
            dialog.Content = grid;

            // 9) Показываем диалог
            await dialog.ShowDialog(this);
        }

        
        private async Task ShowInfoDialog(string message)
        {
            var dialog = new Window
            {
                Title = "Information",
                Width = 400,
                Height = 100,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            // 1) Создаём Grid, чтобы выровнять содержимое по центру
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

            // 2) Создаём StackPanel по центру
            var stack = new StackPanel
            {
                Spacing = 10,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20)
            };

            // 3) Создаём TextBlock с сообщением
            var textBlock = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.Green,
                TextAlignment = TextAlignment.Center
            };

            // 4) Кнопка «OK»
            var button = new Button
            {
                Content = "OK",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            button.Command = ReactiveCommand.Create(() => dialog.Close());

            // 5) Добавляем элементы в StackPanel
            stack.Children.Add(textBlock);
            stack.Children.Add(button);

            // 6) Помещаем StackPanel в Grid
            grid.Children.Add(stack);

            // 7) Присваиваем Grid в Content окна
            dialog.Content = grid;

            // 8) Показываем окно
            await dialog.ShowDialog(this);
        }


    }
}
