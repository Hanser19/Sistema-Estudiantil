using Microsoft.AspNetCore.Mvc.Rendering;

namespace INSCRIPCION_ESTUDIANTIL.Models.ViewModels
{
    public class PagosVM
    {
        public Pago oPagos { get; set; }

        public List<SelectListItem> oListaInscripciones { get; set; }
    }
}
