using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosServices.Dto;

public class CityEventDto
{
    [Required(ErrorMessage = "Obrigatório")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Campo {0} deve ter entre {2} e {1} caracteres")]
    public string Title { get; set; }
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Campo {0} deve ter entre {2} e {1} caracteres")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Obrigatório")]
    public DateTime DateHourEvent { get; set; }
    [Required(ErrorMessage = "Obrigatório")]
    [StringLength(200, MinimumLength = 4, ErrorMessage = "Campo {0} deve ter entre {2} e {1} caracteres")]
    public string Local { get; set; }
    [StringLength(200, MinimumLength = 4, ErrorMessage = "Campo {0} deve ter entre {2} e {1} caracteres")]
    public string Address { get; set; }
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Obrigatório")]
    public bool Status { get; set; }
}
