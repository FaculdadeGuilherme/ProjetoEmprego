using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteDoEmprego.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime? Data { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ItemPedido> ItemPedido { get; set; }
    }
}
