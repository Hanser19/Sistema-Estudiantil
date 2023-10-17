using System;
using System.Collections.Generic;

namespace INSCRIPCION_ESTUDIANTIL.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public string? NombreCurso { get; set; }

    public string? DescripcionCurso { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();

    public virtual ICollection<Seccione> Secciones { get; set; } = new List<Seccione>();
}
