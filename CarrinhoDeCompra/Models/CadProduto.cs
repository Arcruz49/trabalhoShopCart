using System;
using System.Collections.Generic;

namespace CarrinhoDeCompra.Models;

public partial class CadProduto
{
    public int CdProduto { get; set; }

    public string? NmProduto { get; set; }

    public decimal? Preco { get; set; }

    public string? Caminho { get; set; }

    public virtual ICollection<CadCarrinhoProduto> CadCarrinhoProdutos { get; set; } = new List<CadCarrinhoProduto>();
}
