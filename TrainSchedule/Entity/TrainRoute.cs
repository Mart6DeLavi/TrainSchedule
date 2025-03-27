using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainSchedule.Entity;

[Table("train_routes")]
public class TrainRoute
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("from")]
    public string From { get; set; } = "";

    [Required]
    [Column("to")]
    public string To { get; set; } = "";

    [Required]
    [Column("departure_time", TypeName = "timestamp without time zone")]
    public DateTime DepartureTime { get; set; }

    [Required]
    [Column("arrival_time", TypeName = "timestamp without time zone")]
    public DateTime ArrivalTime { get; set; }

    [Range(1, int.MaxValue)]
    [Column("number_of_seats")]
    public int NumberOfSeats { get; set; }

    [Range(1, int.MaxValue)]
    [Column("number_of_wagons")]
    public int NumberOfWagons { get; set; }

    [Range(0.01, double.MaxValue)]
    [Column("ticket_price")]
    public decimal TicketPrice { get; set; }
}