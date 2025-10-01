using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApiDemo.Models
{
    public class CreateUserDto
    {
        [Required(ErrorMessage ="Ad alanı zorunludur.")]
        public string Name { get; set; }
        [Range(18,60)]
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
