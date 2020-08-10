using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using queMePongo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class Scheduler
    {
        private static Scheduler instance = new Scheduler();
        private static StdSchedulerFactory factory;
        private static IScheduler scheduler;

        private Scheduler()
        {

        }

        public void crearSchedulerEvento(String nombre, int tipoEvento, DateTime fechaIni, Evento even)
        {
            JobDataMap jobData = new JobDataMap();
            jobData.Add(nombre, even);
            IJobDetail jobComp = JobBuilder.Create<JobComp>().WithIdentity(nombre, "grupoEjemplo").UsingJobData(jobData).Build();

            //ITrigger triggerComp = TriggerBuilder.Create().WithIdentity(nombre, "grupoEjemplo").StartNow()
            //     .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build();
            ITrigger triggerComp = crearTrigger(tipoEvento, fechaIni, nombre);
            this.agregarTask(jobComp, triggerComp);	
        }

        private ITrigger crearTrigger(int tipoEvento, DateTime fechaIni, String nombre)
        {
            /* 0 para unico, 1 para diario, 2 para semanal, 3 para mensual y 4 para anual*/
            ITrigger triggerComp;
            switch (tipoEvento)
            {
                case 1:
                    String s = fechaIni.Second.ToString() + " " + fechaIni.Minute.ToString() + " " + fechaIni.Hour.ToString() + " ? * *";
                    triggerComp = TriggerBuilder.Create().WithIdentity(nombre, "grupoEjemplo").WithCronSchedule(s).StartNow().Build();
                    break;

                case 2:
                    s = fechaIni.Second.ToString() + " " + fechaIni.Minute.ToString() + " " + fechaIni.Hour.ToString() + " ? * " + convertirDiaDeSemana(fechaIni.DayOfWeek.ToString());
                    triggerComp = TriggerBuilder.Create().WithIdentity(nombre, "grupoEjemplo").WithCronSchedule(s).StartNow().Build();
                    break;

                case 3:
                    s = fechaIni.Second.ToString() + " " + fechaIni.Minute.ToString() + " " + fechaIni.Hour.ToString() + " " + fechaIni.Day + " * ?";
                    triggerComp = TriggerBuilder.Create().WithIdentity(nombre, "grupoEjemplo").WithCronSchedule(s).StartNow().Build();
                    break;

                case 4:
                    s = fechaIni.Second.ToString() + " " + fechaIni.Minute.ToString() + " " + fechaIni.Hour.ToString() + " " + fechaIni.Day + " " + fechaIni.Month + "? *";
                    triggerComp = TriggerBuilder.Create().WithIdentity(nombre, "grupoEjemplo").WithCronSchedule(s).StartNow().Build();
                    break;

                default:
                    triggerComp = TriggerBuilder.Create().WithIdentity(nombre, "grupoEjemplo").StartAt(fechaIni).Build();
                    break;
            }
            return triggerComp;
        }

        private String convertirDiaDeSemana(String dia)
        {
            String respuesta = null;
            switch (dia)
            {
                case "Monday":
                    respuesta = "MON";
                    break;
                case "Tuesday":
                    respuesta = "TUE";
                    break;
                case "Wednesday":
                    respuesta = "WED";
                    break;
                case "Thursday":
                    respuesta = "THU";
                    break;
                case "Friday":
                    respuesta = "FRI";
                    break;
                case "Saturday":
                    respuesta = "SAT";
                    break;
                default:
                    respuesta = "SUN";
                    break;
            }
            return respuesta;
        }

        public static Scheduler getInstance()
        {
            Scheduler.doInstance();
            return Scheduler.instance;
        }

        private static async void doInstance()
        {

            if (instance != null)
            {
                factory = new StdSchedulerFactory();
                scheduler = await factory.GetScheduler();
            }

        }

        public async void run()
        {
            await scheduler.Start();
        }

        public async void stop()
        {
            await scheduler.Shutdown();
        }

        public void agregarTask(IJobDetail job, ITrigger trigger)
        {
            scheduler.ScheduleJob(job, trigger);
        }

    }
    class JobComp : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            String nombre = context.JobDetail.Key.ToString();
            nombre = nombre.Substring(13, nombre.Length - 13);
            Evento even = (Evento)context.JobDetail.JobDataMap.Get(nombre);
            AtuendoRepository atRepo = new AtuendoRepository();

            DB db = new DB();
            Usuario usuario = db.usuarios.FromSqlRaw($"Select * from usuarios where id_usuario = '{even.id_usuario}'").FirstOrDefault();

            List<Atuendo> atuendos = even.generarAtuendos();

            foreach (Atuendo a in atuendos) { atRepo.Insert(a, even, db); }

            even.ejecutarEvento(usuario.mail);
        }

    }
}