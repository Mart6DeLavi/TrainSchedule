using System;

namespace TrainSchedule.ViewModels;

public class TrainRouteViewModel
{
    public string From { get; set; } = "";
    public string To { get; set; } = "";

    public DateTime DepartureDate { get; set; } = DateTime.Today;
    public TimeSpan DepartureTime { get; set; } = TimeSpan.Zero;
    public TimeSpan ArrivalTime { get; set; } = TimeSpan.Zero;

    public string NumberOfSeats { get; set; } = "";
    public string NumberOfWagons { get; set; } = "";
    public string TicketPrice { get; set; } = "";
    
    public DateTime GetFullDepartureTime() => DateTime.SpecifyKind(DepartureDate.Date + DepartureTime, DateTimeKind.Utc);
    public DateTime GetFullArrivalTime() => DateTime.SpecifyKind(DepartureDate.Date + ArrivalTime, DateTimeKind.Utc);

}
