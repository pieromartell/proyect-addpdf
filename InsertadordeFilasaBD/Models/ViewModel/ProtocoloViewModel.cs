using System.ComponentModel.DataAnnotations;

namespace InsertadordeFilasaBD.Models.ViewModel
{
    public class ProtocoloViewModel
    {
        [Required]
        public int Lote{ get; set; }
        
        [Required]
        public int Idproducto { get; set; }

    }
}
