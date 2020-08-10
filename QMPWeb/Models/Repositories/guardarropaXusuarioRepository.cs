using System;
using System.Collections.Generic;
using QueMePongo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace queMePongo.Repositories
{
    [Table("guardarropaxusuario")]
    public class guardarropaXusuarioRepository
    {
        [Key]
        [Column("guardarropaxusuario_id")]
        public int guardarropaXusuario_id { get; set; }

        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Column("id_guardarropa")]
        public int id_guardarropa { get; set; }

        [Column("nombreguardarropa")]
        public string nombreGuardarropa {get; set;}
        public guardarropaXusuarioRepository() { }

        public List<guardarropaXusuarioRepository> listarGuardarropasDeUsuario(int idUsuario){

            DB db = new DB();

            return db.guardarropaXusuarioRepositories.FromSqlRaw($"Select * From guardarropaxusuario Where id_usuario = '{idUsuario}'").ToList();
        }

        public guardarropaXusuarioRepository BuscarGuardarropaPorIdYIdDeUsuario(int idGuardarropa, int idUsuario){
            DB db = new DB();
            return db.guardarropaXusuarioRepositories.FromSqlRaw($"Select * from guardarropaxusuario Where id_guardarropa = '{idGuardarropa}' and id_usuario = '{idUsuario}'").FirstOrDefault();
        }

    }
}
