namespace HouseFinderAPI.Models.Dto
{
    public class RegistrationRequestDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [MaxLength(100)]
        [EmailAddress(ErrorMessage ="Enter email address.")]
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        
    }
}
