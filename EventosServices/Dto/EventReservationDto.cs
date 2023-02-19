using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosServices.Dto;

public class EventReservationDto
{
    [Required(ErrorMessage = "Campo Obrigatório")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Campo {0} deve ter entre {2} e {1} caracteres")]
    public string PersonName { get; set; }
    [Required(ErrorMessage = "Campo Obrigatório")]
    public int Quantity { get; set; }
}
