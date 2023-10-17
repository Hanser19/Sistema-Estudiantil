using System;
using System.Collections.Generic;

namespace INSCRIPCION_ESTUDIANTIL.Models;

public partial class Inscripcion
{
    public int IdInscripcion { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public string? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public string? EstadoInscripcion { get; set; }

    public virtual Curso? oCurso { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
