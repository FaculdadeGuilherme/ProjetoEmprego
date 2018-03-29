using System;
using System.Collections.Generic;


namespace TesteDoEmprego.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public Pedido Pedidos { get; set; }
         
    }
}