using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using queMePongo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QueMePongo
{

    public class DB : DbContext
    {

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Guardarropa> guardarropas { get; set; }
        public DbSet<Evento> eventos { get; set; }
        public DbSet<Prenda> prendas { get; set; }
        public DbSet<Atuendo> atuendos { get; set; }
        public DbSet<TipoPrenda> tipoprendas { get; set; }
        public DbSet<Tela> telas { get; set; }
        public DbSet<Error> errores {get; set;}
        public DbSet<guardarropaXprendaRepository> guardarropaXprendaRepositories { get; set; }
        public DbSet<guardarropaXusuarioRepository> guardarropaXusuarioRepositories { get; set; }
        public DbSet<prendaXatuendoRepository> prendaXatuendoRepositories { get; set; }
        public DbSet<sugerenciaXeventoRepository> sugerenciaXeventoRepositories { get; set; }
        public DbSet<telaXtipoPrendaRepository> telaXtipoPrendaRepositories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseNpgsql("User ID=lguuiapahnunky;Password=91fd2a2efcfe9d82c14e5815c33ea978be33eebd6dde73513dead9181271b13a;Host=ec2-50-17-234-161.compute-1.amazonaws.com;Port=5432;Database=d6to59n2lk2uvl;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;EntityAdminDatabase=d6to59n2lk2uvl");
        }


        public Usuario[] consultarUsuarios()
        {
            return usuarios.ToArray();
        }

        public Guardarropa[] consultarGuardarropas()
        {
            return guardarropas.ToArray();
        }

        public Evento[] consultarEventos()
        {
            return eventos.ToArray();
        }

        public Prenda[] consultarPrendas()
        {
            return prendas.ToArray();
        }

        public Atuendo[] consultarAtuendos()
        {
            return atuendos.ToArray();
        }

        public TipoPrenda[] consultarTipoPrendas()
        {
            return tipoprendas.ToArray();
        }

        public Tela[] consultarTelas()
        {
            return telas.ToArray();
        }

        public guardarropaXusuarioRepository[] consultarGuardarropaXusuario()
        {
            return guardarropaXusuarioRepositories.ToArray();
        }

    }


}