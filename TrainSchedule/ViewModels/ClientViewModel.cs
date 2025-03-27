using System;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using TrainSchedule.Config;
using TrainSchedule.Entity;

namespace TrainSchedule.ViewModels;

public class ClientViewModel : ReactiveObject
{
    private string _from = "";
    public string From
    {
        get => _from;
        set => this.RaiseAndSetIfChanged(ref _from, value);
    }

    private string _to = "";
    public string To
    {
        get => _to;
        set => this.RaiseAndSetIfChanged(ref _to, value);
    }

    private DateTime _selectedDate = DateTime.Today;
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => this.RaiseAndSetIfChanged(ref _selectedDate, value);
    }

    private TimeSpan _selectedTime = TimeSpan.Zero;
    public TimeSpan SelectedTime
    {
        get => _selectedTime;
        set => this.RaiseAndSetIfChanged(ref _selectedTime, value);
    }

    private ObservableCollection<TrainRoute> _routes = new();
    public ObservableCollection<TrainRoute> Routes
    {
        get => _routes;
        set => this.RaiseAndSetIfChanged(ref _routes, value);
    }

    public void SearchRoutes()
    {
        // Берём дату и время, как пользователь ввёл
        var localDateTime = SelectedDate.Date + SelectedTime;

        // Ищем маршруты, у которых DepartureTime попадает в этот час
        var nextHour = localDateTime.AddHours(1);

        using var db = new DatabaseContext();

        var matched = db.TrainRoutes
            .Where(r =>
                r.From.ToLower() == From.ToLower() &&
                r.To.ToLower() == To.ToLower() &&
                r.DepartureTime >= localDateTime &&
                r.DepartureTime < nextHour
            )
            .ToList();

        Routes = new ObservableCollection<TrainRoute>(matched);
    }

}