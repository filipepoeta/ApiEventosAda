using System.ComponentModel.DataAnnotations;

namespace EventosServices.Entity;

public class EventReservation
{
    public int IdReservation { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório")]
    public int IdEvent { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Campo {0} deve ter entre {2} e {1} caracteres")]
    public string PersonName { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório")]
    public int Quantity { get; set; }
}
