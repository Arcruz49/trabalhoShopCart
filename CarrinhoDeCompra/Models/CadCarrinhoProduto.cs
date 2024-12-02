using System;
using System.Collections.Generic;

namespace CarrinhoDeCompra.Models;

public partial class CadCarrinhoProduto
{
    public int CdCarrinho { get; set; }

    public int? CdProduto { get; set; }

    public virtual CadProduto? CdProdutoNavigation { get; set; }
}
