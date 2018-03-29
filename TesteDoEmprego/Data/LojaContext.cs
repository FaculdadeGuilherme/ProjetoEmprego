using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteDoEmprego.Models;

namespace TesteDoEmprego.Data
{
    public class LojaContext : DbContext 
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        { }
       
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemPedido>().HasKey(t => new { t.PedidoId, t.ProdutoId });
            modelBuilder.Entity<Pedido>().HasIndex(i => i.UsuarioId).IsUnique(false);
        }
    }
}

