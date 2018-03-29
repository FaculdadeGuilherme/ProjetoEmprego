using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteDoEmprego.Models
{
    public class Produto
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Nome { get; set; }        
        public decimal Preco { get; set; }

       
    }
}