using Ada.Framework.Expressions.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Ada.Framework.Maintenance.FileManager.Entities
{
    [XmlRoot(ElementName = "FileManager")]
    public class AdministradorArchivoTag
    {
        [XmlArray("Actions")]
        [XmlArrayItem("Move",typeof(Mover))]
        [XmlArrayItem("Delete", typeof(Eliminar))]
        [XmlArrayItem("Compress", typeof(Comprimir))]
        public List<Accion> Acciones { get; set; }

        [XmlArray("Parameters")]
        public List<Parametro> Parametros { get; set; }

        [XmlArray("Evaluators")]
        public List<EvaluadorTag> Evaluadores { get; set; }

        public EvaluadorTag ObtenerEvaluador(string nombre)
        {
            return Evaluadores.First(c => c.Nombre == nombre);
        }
    }
}
