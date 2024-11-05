using System.ComponentModel.DataAnnotations;

namespace CarshoppingCartApi.DTO
{
    public class CheckOrderModel
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }
    }
}
