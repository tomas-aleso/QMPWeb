using System;
using QueMePongo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace queMePongo.Repositories
{
    public class PrendaRepository
    {
        public void CrearPrenda(Prenda prenda, DB context, int idGuardarropa)
        {
            context.prendas.Add(prenda);
            context.SaveChanges();
            guardarropaXprendaRepository gpr = new guardarropaXprendaRepository();
            gpr.id_guardarropa = idGuardarropa;
            gpr.id_prenda = prenda.id_prenda;
            context.guardarropaXprendaRepositories.Add(gpr);
            context.SaveChanges();
        }

        public bool EditarPrenda(Prenda prenda, int idUsuarioModificador)
        {

            DB context = new DB();

            if(prenda.id_duenio == idUsuarioModificador){
                var s = context.prendas.FromSqlRaw($"Select * from prendas where id_prenda = '{prenda.id_prenda}'").AsNoTracking().FirstOrDefault();
                s.calificacion = prenda.calificacion;
                s.cantCalif = prenda.cantCalif;

                context.prendas.Update(prenda);
                context.SaveChanges();

                return true;

            } else {
                return false;
            }

        }

        public bool EliminarPrenda(int prendaId, int idUsuario, DB context)
        {
            Prenda prenda = new Prenda();
            prenda = context.prendas.FromSqlRaw($"Select * from prendas where id_prenda='{prendaId}' and id_duenio='{idUsuario}'").FirstOrDefault();

            if(prenda != null){

                string pathDeImagenADeletear = "wwwroot/uploads/"+prenda.urlImagen;

                List<guardarropaXprendaRepository> gpr = new List<guardarropaXprendaRepository>();
                gpr = context.guardarropaXprendaRepositories.Where(u => u.id_prenda == prendaId).ToList();
                foreach (guardarropaXprendaRepository a in gpr)
                {
                    context.guardarropaXprendaRepositories.Remove(a);
                }

                File.Delete(pathDeImagenADeletear);

                context.prendas.Remove(prenda);
                context.SaveChanges();

                return true;
            } else {
                return false;
            }
        }

        public Prenda BuscarPrendaPorId(int idPrenda){
            DB db = new DB();

            Prenda prenda = db.prendas.FromSqlRaw($"Select * From prendas Where id_prenda = '{idPrenda}'").AsNoTracking().FirstOrDefault();

            return prenda;

        }

        public List<Prenda> PrendasDelUsuario(int idUsuario){

            DB db = new DB();

            return db.prendas.Where(prenda => prenda.id_duenio == idUsuario).ToList();

        }

        public bool agregarPrendaAGuardarropa(int idPrenda, int idGuardarropa, int idUsuario){

            DB db = new DB();
            guardarropaXprendaRepository gxpDAO = new guardarropaXprendaRepository();
            GuardarropaRepository guardarropaDAO = new GuardarropaRepository();

            var guardarropa = guardarropaDAO.buscarGuardarropaPorId(idGuardarropa);
            var gxp = db.guardarropaXprendaRepositories.FromSqlRaw($"Select * from guardarropaxprenda where id_guardarropa = '{idGuardarropa}' and id_prenda='{idPrenda}'").FirstOrDefault();

            if(gxp == null){
                gxpDAO.id_guardarropa = idGuardarropa;
                gxpDAO.id_prenda = idPrenda;

                db.guardarropaXprendaRepositories.Add(gxpDAO);
                db.SaveChanges();

                return true; //Se agregó la prenda al guardarropa

            } else {
                return false; //No se pudo agregar porque la prenda ya está asociada al guardarropas
            }


        }
    }
}