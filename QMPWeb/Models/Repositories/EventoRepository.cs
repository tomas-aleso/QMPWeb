using System;
using System.Collections.Generic;
using QueMePongo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;
using queMePongo.Repositories;

public class EventoRepository
    {
        public void Insert(Evento evento, DB context)
        {
            context.eventos.Add(evento);
            context.SaveChanges();
        }

    public void Update(IFormCollection form)
        {
            UsuarioRepository userDAO = new UsuarioRepository();
            Usuario usuario = new Usuario();
            DB db = new DB();

            string idUsuarioString = form["idUsuario"];
            string idEventoString = form["idEvento"];
            int idUser = Convert.ToInt32(idUsuarioString);
            int idEvento = Convert.ToInt32(idEventoString);
            string descripcionDelEvento = form["descripcionEvento"];
            string fechaInicioNueva = form["fechaInicioNueva"];
            string fechaFinNueva = form["fechaFinNueva"];
            string recordatorioNuevoString = form["recordatorioNuevo"];
            string frecuenciaNueva = form["frecuenciaNueva"];
            string lugar = form["lugar"];

            Evento evento = this.BuscarEventoPorId(idEvento);
            usuario = userDAO.BuscarUsuarioPorId(idUser);

            evento.descripcion = descripcionDelEvento;
            evento.lugar = lugar;

            if(fechaInicioNueva != ""){
                DateTime fechaInicioPrendas = Convert.ToDateTime(fechaInicioNueva);
                evento.fechaInicioPrendas = fechaInicioPrendas;
            }

            if(fechaFinNueva != ""){
                DateTime fechaFinPrendas = Convert.ToDateTime(fechaFinNueva);
                evento.fechaFinPrendas = fechaFinPrendas;
            }

            if(recordatorioNuevoString != "-"){
                if(fechaInicioNueva != ""){
                    DateTime fechaInicioPrendas = Convert.ToDateTime(fechaInicioNueva);
                    DateTime fechaDeRecordatorio = fechaInicioPrendas.AddHours(-1 * Convert.ToInt32(recordatorioNuevoString));
                    evento.fechaNotificacion = fechaDeRecordatorio;
                }else {
                    evento.fechaNotificacion = evento.fechaInicioPrendas.AddHours(-1 * Convert.ToInt32(recordatorioNuevoString));
                }
            } 
            
            if(frecuenciaNueva != "-"){
                int tipoEvento = Convert.ToInt32(frecuenciaNueva);
                evento.tipoEvento = tipoEvento;
            }

            db.eventos.Update(evento);
            db.SaveChanges();
        }

    public void Delete(int eventoId)
        {
            DB db = new DB();
            db.Database.ExecuteSqlRaw($"delete from eventos Where id_evento = '{eventoId}'");
        }
    public Evento BuscarEventoPorId(int idEvento){
        DB db = new DB();

        Evento evento = db.eventos.FromSqlRaw($"Select * from eventos where id_evento = '{idEvento}'").FirstOrDefault();

        return evento;
    
    }

}


