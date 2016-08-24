using Ada.Framework.Util.FileMonitor;
using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace Ada.Framework.Maintenance.FileManager.Entities
{
    [XmlType(TypeName = "CompressCompress")]
    public class Comprimir : Accion
    {
        [XmlAttribute(AttributeName = "DestinationPath")]
        public string RutaDestino { get; set; }

        public override void Realizar(FileSystem fileSystem)
        {
            if (fileSystem.Info is FileInfo)
            {
                string rutaFinal = RutaDestino;

                if (rutaFinal[rutaFinal.Length - 1] == '\\')
                {
                    rutaFinal = rutaFinal.Substring(rutaFinal.Length - 2);
                }

                rutaFinal = string.Format(@"{0}\{1}.zip", rutaFinal, fileSystem.Info.Name.Replace(fileSystem.Info.Extension, ""));

                IMonitorArchivo monitor = MonitorArchivoFactory.ObtenerArchivo();
                monitor.PrepararDirectorio(rutaFinal);

                using (FileStream zipToOpen = new FileStream(rutaFinal, FileMode.OpenOrCreate))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        archive.CreateEntryFromFile(fileSystem.Info.FullName, fileSystem.Info.Name);
                    }
                }
                new Eliminar().Realizar(fileSystem);
            }
            else
            {
                throw new Exception("¡No se puede mover un directorio!");
            }
        }
    }
}
