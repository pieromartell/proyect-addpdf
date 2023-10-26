using System;
using System.Collections.Generic;

namespace InsertadordeFilasaBD.Models;

public partial class Protocolo
{
    public int Idprotocolo { get; set; }

    public string Namedocument { get; set; } = null!;

    public string Document { get; set; } = null!;

    public int Lote { get; set; }

    public int Idproducto { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
