using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using queMePongo.Repositories;

namespace QueMePongo
{
    [Table("prendas")]
    public class Prenda
    {
        [Key]
        [Column("id_prenda")]
        public int id_prenda { get; set; }

        [Column("id_tipoprenda")]
        public int tipoPrenda { get; set; }

        [Column("id_tela")]
        public int id_tela { get; set; }

        [Column("colorprincipal")]
        public String colorPrincipal { get; set; }

        [Column("colorsecundario")]
        public String colorSecundario { get; set; }
        
        [Column("calificacion")]
        public int calificacion { get; set; }

        [Column("cantcalif")]
        public int cantCalif { get; set; }
        
        [Column("urlimagen")]
        public string urlImagen { get; set; }
        [Column("id_duenio")]
        public int id_duenio  {get; set; }

        [NotMapped]
        public IFormFile imagen { get; set; }
        public List<Evento> eventos = new List<Evento>();

        [NotMapped]
        public virtual ICollection<Guardarropa> Guardarropas { get; set; }

        [NotMapped]
        public virtual ICollection<TipoPrenda> TiposPrendas { get; set; }

        [NotMapped]
        public virtual ICollection<Atuendo> Atuendos { get; set; }

        public Prenda() { }

        public String tela;

        [NotMapped]
        public Tela Tela { get; set; }

        [NotMapped]
        public TipoPrenda tipo;


        public Prenda(TipoPrenda tipoP, String tel, String cp, String cs)
        {
            if (cp == cs) throw new ArgumentException("el color principal no puede ser igual que el secundario");
            tipo = tipoP;
            tela = tel;
            colorPrincipal = cp;
            colorSecundario = cs;
            calificacion = 0;
            cantCalif = 0;
        }

        public void calificar(int calif)
        {
            calificacion += calif;
            cantCalif++;
        }

        public float getCalif()
        {
            return (float)calificacion / (float)cantCalif;
        }

        public bool Igual(Prenda prenda)
        {
            return tipo.Equals(prenda.tipo) && tela == prenda.tela && colorPrincipal == prenda.colorPrincipal && colorSecundario == prenda.colorSecundario;
        }

        public bool validarFechas(Evento even)
        {
            for (int i = 0; i < eventos.Count; i++)
            {
                if (even.fechaFinPrendas < eventos[i].fechaInicioPrendas)
                {
                    return true;
                }
                else
                {
                    if (even.fechaInicioPrendas > eventos[i].fechaFinPrendas)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public string nombreUnicoImagen(IFormFile imagenDePrenda, IHostingEnvironment ie){

            var imagen = imagenDePrenda;
            
            string nombreDeArchivo = Path.GetFileName(imagenDePrenda.FileName);

            var nombreUnico = Path.GetFileNameWithoutExtension(nombreDeArchivo)+"-"+Guid.NewGuid().ToString().Substring(0,4)+Path.GetExtension(nombreDeArchivo);;
            var uploads = Path.Combine(ie.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, nombreUnico);

            imagen.CopyTo(new FileStream(filePath, FileMode.Create));

            return nombreUnico;

        }

        public void crearPrendasVacias(int idGuardarropa, int idUsuario)
        {

            DB db = new DB();
            PrendaRepository prendaDAO = new PrendaRepository();

            for (int i = 0; i < 5; i++)
            {
                Prenda prendaNueva = new Prenda();

                //prendaNueva.colorPrincipal = form["colorPrincipal"];
                //prendaNueva.colorSecundario = form["colorSecundario"];

                prendaNueva.id_tela = 27;
                prendaNueva.tipoPrenda = 56 + i;
                prendaNueva.id_duenio = idUsuario;

                //prendaNueva.urlImagen = prendaNueva.nombreUnicoImagen(imagenDePrenda, hostingEnviroment);

                prendaDAO.CrearPrenda(prendaNueva, db, idGuardarropa);
            }

        }

    }
}