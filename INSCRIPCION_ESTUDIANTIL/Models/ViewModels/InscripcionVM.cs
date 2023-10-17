using Microsoft.AspNetCore.Mvc.Rendering;

namespace INSCRIPCION_ESTUDIANTIL.Models.ViewModels
{
    public class InscripcionVM
    {
        public Inscripcion oInscripcion { get; set; }

        public List<SelectListItem> oListaCurso { get; set; }
    }
}