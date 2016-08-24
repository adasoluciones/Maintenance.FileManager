using Ada.Framework.Configuration.Xml;
using Ada.Framework.Expressions.Entities;
using Ada.Framework.Maintenance.FileManager.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Ada.Framework.Maintenance.FileManager
{
    public class FileSystemManager : ConfiguracionXmlManager<AdministradorArchivoTag>
    {
        private static readonly string TIPO_ACCION_ARCHIVO = "Files";
        private static readonly string TIPO_ACCION_DIRECTORIO = "Folder";

        private static IDictionary<string, Assembly> ensamblados = new Dictionary<string, Assembly>();
        
        public override string NombreArchivoConfiguracion
        {
            get { return "FileSystemActions"; }
        }

        public override string NombreArchivoPorDefecto
        {
            get { return "FileSystemActions.xml"; }
        }

        public override string NombreArchivoValidacionConfiguracion
        {
            get { return null; }
        }

        public override string NombreArchivoValidacionPorDefecto
        {
            get { return null; }
        }

        protected override bool ValidarXmlSchema
        {
            get { return false; }
        }

        protected override void ValidarXml(XmlDocument documento) { }

        public static IList<FileSystem> ObtenerFileSystems(Accion accion)
        {
            IList<FileSystem> retorno = new List<FileSystem>();

            if (accion.Tipo == TIPO_ACCION_ARCHIVO)
            {
                DirectoryInfo directory = new DirectoryInfo(accion.Ruta);
                FileInfo[] files = directory.GetFiles("*.*");

                foreach (FileInfo file in files)
                {
                    FileSystem archivo = new FileSystem();
                    archivo.Info = file;
                    retorno.Add(archivo);
                }
            }
            else if (accion.Tipo == TIPO_ACCION_DIRECTORIO)
            {
                foreach (DirectoryInfo directorio in new DirectoryInfo(accion.Ruta).GetDirectories())
                {
                    retorno.Add(new FileSystem()
                    {
                        Tipo = TipoFileSystem.Directorio,
                        Info = directorio
                    });
                }
            }

            return retorno;
        }

        public static Evaluador ObtenerEvaluador(EvaluadorTag evaluador)
        {
            if (evaluador.RutaEnsamblado != null)
            {
                evaluador.RutaEnsamblado = evaluador.RutaEnsamblado.Trim();
            }

            if (!string.IsNullOrEmpty(evaluador.RutaEnsamblado))
            {
                Assembly ensamblado = null;
                if (ensamblados.ContainsKey(evaluador.RutaEnsamblado))
                {
                    ensamblado = ensamblados[evaluador.RutaEnsamblado];
                }
                else
                {
                    ensamblado = Assembly.LoadFile(evaluador.RutaEnsamblado);
                    ensamblados.Add(evaluador.RutaEnsamblado, ensamblado);
                }

                Type tipo = ensamblado.GetType(evaluador.Instancia);
                return Activator.CreateInstance(tipo) as Evaluador;
            }
            return Activator.CreateInstance(evaluador.NombreEnsamblado, evaluador.Instancia).Unwrap() as Evaluador;
        }

        public static IList<Evaluador<object>> ObtenerEvaluadores(List<EvaluadorTag> evaluadores)
        {
            List<Evaluador<object>> retorno = new List<Evaluador<object>>();
            foreach (EvaluadorTag evaluador in evaluadores)
            {
                retorno.Add(ObtenerEvaluador(evaluador));
            }
            return retorno;
        }

        public static IList<ExpresionCondicional> ObtenerCondiciones(List<CondicionTag> condiciones)
        {
            IList<ExpresionCondicional> retorno = new List<ExpresionCondicional>();

            foreach (CondicionTag condicion in condiciones)
            {
                retorno.Add(condicion);
            }

            return retorno;
        }



    }
}
