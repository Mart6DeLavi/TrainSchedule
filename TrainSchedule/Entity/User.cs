using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TrainSchedule.Entity;

[Table("users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
    [Column("name")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
    [Column("second_name")]
    public string SecondName { get; set; } = "";

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
    [Column("username")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [Column("email")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [Column("password")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Invalid phone number format.")]
    [Column("phone_number")]
    public string PhoneNumber { get; set; } = "";
    
    [Required]
    [Column("role")]
    public string Role { get; set; } = "USER";

    public static bool ValidateUser(User user, out List<string> errors)
    {
        errors = new List<string>();
        var validationContext = new ValidationContext(user);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);
        if (!isValid)
        {
            errors.AddRange(validationResults.Select(vr => vr.ErrorMessage!));
        }

        return isValid;
    }
}