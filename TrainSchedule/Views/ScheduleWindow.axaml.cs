using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainSchedule.Config;
using TrainSchedule.Entity;
using TrainSchedule.ViewModels;

namespace TrainSchedule.Views
{
    public partial class ScheduleWindow : Window
    {
        public ScheduleWindow()
        {
            InitializeComponent();
            DataContext = new TrainRouteViewModel(); 
        }
        
        private void OnAcceptClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not TrainRouteViewModel vm)
                return;

            // Проверка на пустые значения
            if (string.IsNullOrWhiteSpace(vm.From) ||
                string.IsNullOrWhiteSpace(vm.To) ||
                string.IsNullOrWhiteSpace(vm.NumberOfSeats) ||
                string.IsNullOrWhiteSpace(vm.NumberOfWagons) ||
                string.IsNullOrWhiteSpace(vm.TicketPrice))
            {
                Console.WriteLine("❌ Все поля должны быть заполнены.");
                return;
            }

            // Проверка на числовые значения
            if (!int.TryParse(vm.NumberOfSeats, out int seats) || seats <= 0 ||
                !int.TryParse(vm.NumberOfWagons, out int wagons) || wagons <= 0 ||
                !decimal.TryParse(vm.TicketPrice, out decimal price) || price <= 0)
            {
                Console.WriteLine("❌ Числовые поля должны быть корректными и положительными.");
                return;
            }

            if (vm.ArrivalTime <= vm.DepartureTime)
            {
                Console.WriteLine("❌ Время прибытия должно быть позже времени отправления.");
                return;
            }

            var newRoute = new TrainRoute
            {
                From = vm.From,
                To = vm.To,
                DepartureTime = vm.GetFullDepartureTime(),
                ArrivalTime = vm.GetFullArrivalTime(),
                NumberOfSeats = seats,
                NumberOfWagons = wagons,
                TicketPrice = price
            };

            using var db = new DatabaseContext();
            db.TrainRoutes.Add(newRoute);
            db.SaveChanges();

            Console.WriteLine("✅ Данные успешно сохранены в базу.");
        }

    }
}