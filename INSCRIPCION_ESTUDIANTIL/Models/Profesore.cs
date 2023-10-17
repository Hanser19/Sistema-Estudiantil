using System;
using System.Collections.Generic;

namespace INSCRIPCION_ESTUDIANTIL.Models;

public partial class Profesore
{
    public int IdProfesor { get; set; }

    public string? NombreProfesor { get; set; }

    public string? CorreoProfesor { get; set; }

    public int? IdSecciones { get; set; }

    public int? IdAsignaturas { get; set; }

    public virtual Asignatura? oAsignatura { get; set; }

    public virtual Seccione? oSecciones { get; set; }
}
