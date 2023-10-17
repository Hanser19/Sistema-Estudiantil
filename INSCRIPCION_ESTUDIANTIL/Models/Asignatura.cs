using System;
using System.Collections.Generic;

namespace INSCRIPCION_ESTUDIANTIL.Models;

public partial class Asignatura
{
    public int IdAsignaturas { get; set; }

    public string? NombreAsignatura { get; set; }

    public string? Descripcion { get; set; }

    public int? Creditos { get; set; }

    public virtual ICollection<Profesore> Profesores { get; set; } = new List<Profesore>();
}
