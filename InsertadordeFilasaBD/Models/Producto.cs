using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace InsertadordeFilasaBD.Models;

public partial class Producto
{
    public int? Idproducto { get; set; }

    [Required]
    public string Nameproduct { get; set; } = null!;

    public virtual ICollection<Protocolo> Protocolos { get; set; } = new List<Protocolo>();
}
