using System;
using System.Collections.Generic;

namespace INSCRIPCION_ESTUDIANTIL.Models;

public partial class Seccione
{
    public int IdSecciones { get; set; }

    public string? NombreSeccion { get; set; }

    public int? CuposSeccion { get; set; }

    public int? IdCurso { get; set; }

    public virtual Curso? oCurso { get; set; }

    public virtual ICollection<Profesore> Profesores { get; set; } = new List<Profesore>();
}
