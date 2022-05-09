using System.ComponentModel.DataAnnotations;

namespace CitaVehiculosApi.Models;

public class Cita
{
    [Key]
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public bool Atendida { get; set; } = false;
    [StringLength(10)]
    public string Placa { get; set; } = null!;
}
