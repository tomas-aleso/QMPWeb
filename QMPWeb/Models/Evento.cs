using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using queMePongo.Repositories;

namespace QueMePongo
{
    [Table("eventos")]
    public class Evento
    {
        [Key]
        [Column("id_evento")]
        public int id_evento { get; set; }

        [Column("fechanotificacion")]
        public DateTime fechaNotificacion { get; set; }

        [Column("fechadeinicio")]
        public DateTime fechaInicioPrendas { get; set; }

        [Column("fechafinal")]
        public DateTime fechaFinPrendas { get; set; }

        [Column("lugar")]
        public String lugar { get; set; }

        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        [Column("descripcion")]
        public String descripcion { get; set; }

        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Column("tipoevento")]
        public int tipoEvento { get; set; }

        [NotMapped]
        public Usuario user { get; set; }

        [NotMapped]
        public Atuendo atuendo { get; set; }

        public Evento(String lug, String descript, Usuario u, DateTime fechaIni, DateTime fechaIniPrendas, DateTime fechaFinPrenda, int tipoEvent)
        {
            lugar = lug;
            descripcion = descript;
            user = u;
            fechaNotificacion = fechaIni;
            fechaInicioPrendas = fechaIniPrendas;
            fechaFinPrendas = fechaFinPrenda;
            id_usuario = u.id_usuario;
            tipoEvento = tipoEvent;
            Scheduler sched = Scheduler.getInstance();
            sched.run();
            sched.crearSchedulerEvento(descript, tipoEvent, fechaIni, this);
        }

        public Evento() { }

        public void ejecutarEvento(string mailDeUsuario)
        {
            try{
                MailMessage mail = new MailMessage("quemepongofsoc@gmail.com", mailDeUsuario, "Sugerencias listas para uno de tus eventos", "Ya armamos las sugerencias de atuendos para el evento: "+this.descripcion);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("quemepongofsoc@gmail.com", "asd123asd456");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mail);
                smtpClient.Dispose();
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public List<Atuendo> generarAtuendos()
        {
            DB db = new DB();
            guardarropaXusuarioRepository guardarropaDAO = new guardarropaXusuarioRepository();
            GuardarropaRepository guardarropaREP = new GuardarropaRepository();
            GenerarSugerencias generador = new GenerarSugerencias();
            TipoPrendaRepository tpr = new TipoPrendaRepository();
            List<Atuendo> atuendos = new List<Atuendo>();
            List<guardarropaXusuarioRepository> guardarropasParciales = guardarropaDAO.listarGuardarropasDeUsuario(id_usuario);
            List<List<Prenda>> prendas = new List<List<Prenda>>();
            foreach (guardarropaXusuarioRepository g in guardarropasParciales) { prendas.Add(guardarropaREP.PrendasDelGuardarropas(g.id_guardarropa, db)); }
            foreach (List<Prenda> p in prendas) { foreach (Prenda pr in p) { pr.tipo = tpr.TraerTipoDePrendaPorId(pr.tipoPrenda); } }
            foreach (List<Prenda> p in prendas) { atuendos.AddRange(generador.ejecutarGenerar(20, p, this)); }
            return atuendos;
        }

    }
}
