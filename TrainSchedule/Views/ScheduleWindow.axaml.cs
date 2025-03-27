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
            if (DataContext is not TrainRouteViewModel viewModel)
                return;

            if (string.IsNullOrWhiteSpace(viewModel.From) ||
                string.IsNullOrWhiteSpace(viewModel.To) ||
                string.IsNullOrWhiteSpace(viewModel.NumberOfSeats) ||
                string.IsNullOrWhiteSpace(viewModel.NumberOfWagons) ||
                string.IsNullOrWhiteSpace(viewModel.TicketPrice))
            {
                Console.WriteLine("❌ All fields must be completed");
                return;
            }

            if (!int.TryParse(viewModel.NumberOfSeats, out int seats) || seats <= 0 ||
                !int.TryParse(viewModel.NumberOfWagons, out int wagons) || wagons <= 0 ||
                !decimal.TryParse(viewModel.TicketPrice, out decimal price) || price <= 0)
            {
                Console.WriteLine("❌ Numeric fields must be correct and positive.");
                return;
            }

            if (viewModel.ArrivalTime <= viewModel.DepartureTime)
            {
                Console.WriteLine("❌ The arrival time must be later than the departure time.");
                return;
            }
            
            TrainRoute route = new()
            {
                From = viewModel.From,
                To = viewModel.To,
                DepartureTime = viewModel.GetFullDepartureTime(),
                ArrivalTime = viewModel.GetFullArrivalTime(), 
                NumberOfSeats = seats,
                NumberOfWagons = wagons,
                TicketPrice = price
            };
    
            using (var db = new DatabaseContext())
            {
                db.TrainRoutes.Add(route);
                db.SaveChanges();
            }

            viewModel.Routes.Add(route);

            Console.WriteLine("✅ The data has been successfully saved to the database.");
        }

    }
}