using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApiDemo.Models
{
    public class UserDto
    {
        public string Name { get; set; }       
        public int Age { get; set; } 
        public string Email { get; set; }
    }
}
