using System;
using System.Collections.ObjectModel;
using TrainSchedule.Entity;

namespace TrainSchedule.ViewModels;

public class TrainRouteViewModel
{
    public string From { get; set; } = "Select city";
    public string To { get; set; } = "Select city";

    public DateTime DepartureDate { get; set; } = DateTime.Today;
    public TimeSpan DepartureTime { get; set; } = TimeSpan.Zero;
    public TimeSpan ArrivalTime { get; set; } = TimeSpan.Zero;

    public string NumberOfSeats { get; set; } = "";
    public string NumberOfWagons { get; set; } = "";
    public string TicketPrice { get; set; } = "";
    public DateTime GetFullDepartureTime() => DepartureDate.Date + DepartureTime;
    public DateTime GetFullArrivalTime() => DepartureDate.Date + ArrivalTime;

    public ObservableCollection<TrainRoute> Routes { get; } = new();
    
    public ObservableCollection<string> AvailableCities { get; } = new ObservableCollection<string>
    {
        "Select city",
        "City A",
        "City B",
        "City C",
        "City D"
    };
}
