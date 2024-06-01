using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Televisor3D
{
    class ArchivoJson<T>
    {
        public static void Guardar(string path, T objetoAGuardar)
        {
            try
            {
                string output = JsonConvert.SerializeObject(objetoAGuardar);
                File.WriteAllText(path, output);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        public static T Cargar(string path)
        {

            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"El archivo '{path}' no existe.");
                }

                string output = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(output);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
                return default;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar el JSON: {ex.Message}");
                return default;
            }
        }
    }
}
