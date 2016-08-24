using Ada.Framework.Util.FileMonitor;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Ada.Framework.Maintenance.FileManager.Entities
{
    [XmlType(TypeName = "Move")]
    public class Mover : Accion
    {
        [XmlAttribute(AttributeName = "DestinationPath")]
        public string RutaDestino { get; set; }

        public override void Realizar(FileSystem fileSystem)
        {
            if (fileSystem.Info is FileInfo)
            {
                IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();
                monitor.PrepararDirectorio(RutaDestino);
                if (RutaDestino[RutaDestino.Length - 1] != '\\')
                {
                    RutaDestino += "\\";
                }
                (fileSystem.Info as FileInfo).MoveTo(RutaDestino + fileSystem.Info.Name);
            }
            else
            {
                throw new Exception("¡No se puede mover un directorio!");
            }
        }
    }
}
