namespace Events.Models
{
    public class RegistrationResponseListDto
    {
        public int count { get; set; }

        public IEnumerable<RegistrationResponseDto>? Registrations { get; set; }
    }
}