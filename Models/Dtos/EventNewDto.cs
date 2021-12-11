using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class EventNewDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
    }
}

