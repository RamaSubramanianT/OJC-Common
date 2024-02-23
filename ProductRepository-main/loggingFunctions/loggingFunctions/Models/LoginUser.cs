using System.ComponentModel.DataAnnotations;
namespace loggingFunctions.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage="Username Required")]
        [StringLength(20)]
        public string? username { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [StringLength(20)]
        public string? passwd { get; set; }
        public string? TableAccessName { get; set; }
    }
}
