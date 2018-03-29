using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDoEmprego.Models
{
    public class ItemPedido
    {
        //private object p;

        //public ItemPedido(object p)
        //{
        //    this.p = p;
        //}

        //public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
