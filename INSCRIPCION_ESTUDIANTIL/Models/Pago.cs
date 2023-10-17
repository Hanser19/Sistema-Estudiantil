using System;
using System.Collections.Generic;

namespace INSCRIPCION_ESTUDIANTIL.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public DateTime? FechaPago { get; set; }

    public int? IdInscripcion { get; set; }

    public double? Monto { get; set; }

    public virtual Inscripcion? oInscripcion { get; set; }
}
