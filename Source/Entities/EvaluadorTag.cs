using System.Xml.Serialization;

namespace Ada.Framework.Maintenance.FileManager.Entities
{
    [XmlType(TypeName = "Evaluator")]
    public class EvaluadorTag
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Nombre { get; set; }

        [XmlAttribute(AttributeName = "Instance")]
        public string Instancia { get; set; }

        [XmlAttribute(AttributeName = "AssemblyName")]
        public string NombreEnsamblado { get; set; }

        [XmlAttribute(AttributeName = "AssemblyPath")]
        public string RutaEnsamblado { get; set; }
    }
}
