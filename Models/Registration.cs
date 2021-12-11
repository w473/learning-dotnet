using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class Registration
    {
        public int Id { get; set; }

        public Events.Models.Event Event { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
    }
}