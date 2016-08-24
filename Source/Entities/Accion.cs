using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ada.Framework.Maintenance.FileManager.Entities
{
    [XmlInclude(typeof(Mover))]
    [XmlInclude(typeof(Eliminar))]
    [XmlInclude(typeof(Comprimir))]
    public abstract class Accion
    {
        [XmlAttribute(AttributeName = "Path")]
        public string Ruta { get; set; }
        
        [XmlArray("Conditions")]
        public List<CondicionTag> Condiciones { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Tipo { get; set; }

        public abstract void Realizar(FileSystem fileSystem);
    }
}