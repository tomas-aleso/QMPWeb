using System;
using QueMePongo;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace queMePongo.Repositories
{
    public class UsuarioRepository
    {
        public void Insert(Usuario usuario, DB context)
        {
            context.usuarios.Add(usuario);
            context.SaveChanges();
        }
        public int tipoUsuario(String usuario, DB context)
        {
            var user = context.usuarios.Single(u => u.usuario == usuario);
            return user.tipoDeUsuario;
        }

        public Usuario BuscarUsuarioPorId(int? id){
            DB db = new DB();
            return db.usuarios.FromSqlRaw($"Select * From usuarios Where id_usuario = '{id}'").FirstOrDefault();
        }

        public Usuario BuscarUsuarioPorUsername(String username){
            DB db = new DB();
            return db.usuarios.FromSqlRaw($"Select * From usuarios Where usuario = '{username}'").FirstOrDefault();
        }

        

    }
}
