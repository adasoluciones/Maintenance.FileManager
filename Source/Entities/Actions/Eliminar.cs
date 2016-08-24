using System.Xml.Serialization;

namespace Ada.Framework.Maintenance.FileManager.Entities
{
    [XmlType(TypeName = "Delete")]
    public class Eliminar : Accion 
    {
        public override void Realizar(FileSystem fileSystem)
        {
            fileSystem.Info.Delete();
        }
    }
}
