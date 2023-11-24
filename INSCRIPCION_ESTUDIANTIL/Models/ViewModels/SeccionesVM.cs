using Microsoft.AspNetCore.Mvc.Rendering;

namespace INSCRIPCION_ESTUDIANTIL.Models.ViewModels
{
    public class SeccionesVM
    {
        public Seccione oSecciones { get; set; }
        public List<SelectListItem> oListaCursos { get; set; }
    }
}
