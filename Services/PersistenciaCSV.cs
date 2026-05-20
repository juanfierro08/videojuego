using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GameMaster
{
    // [PRINCIPIOS SOLID - SRP]: La clase solo se encarga de la lectura/escritura física de datos.
    public class PersistenciaCSV<T> : IPersistenciaCSV<T> where T : class, IEntidad, new()
    {
        private readonly string _rutaArchivo;
        private List<T> _memoria = new List<T>();
        private bool _inicializado = false;

        public PersistenciaCSV(string nombreArchivo)
        {
            _rutaArchivo = nombreArchivo;
            var directory = Path.GetDirectoryName(_rutaArchivo);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void EjecutarConReintentos(Action accion, int maxReintentos = 3)
        {
            for (int i = 0; i < maxReintentos; i++)
            {
                try
                {
                    accion();
                    return;
                }
                catch (IOException ex)
                {
                    if (i == maxReintentos - 1)
                    {
                        Console.WriteLine($"\n[ERROR IO]: {ex.Message}");
                        return; // Silenciar error
                    }
                    System.Threading.Thread.Sleep(500);
                }
                catch (UnauthorizedAccessException ex)
                {
                    if (i == maxReintentos - 1)
                    {
                        Console.WriteLine($"\n[ERROR UNAUTHORIZED]: {ex.Message}");
                        return; // Silenciar error
                    }
                    System.Threading.Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n[ERROR CRÍTICO CSV]: " + ex.ToString());
                    if (i == maxReintentos - 1) return; // Silenciar error
                }
            }
        }

        public List<T> Listar()
        {
            if (!_inicializado)
            {
                if (File.Exists(_rutaArchivo))
                {
                    EjecutarConReintentos(() =>
                    {
                        using var stream = new FileStream(_rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        using var reader = new StreamReader(stream);
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            MissingFieldFound = null,
                            HeaderValidated = null
                        };
                        using var csv = new CsvReader(reader, config);
                        _memoria = csv.GetRecords<T>().ToList();
                    });
                }
                _inicializado = true;
            }
            return _memoria.ToList(); // Retorna copia
        }

        private void SobrescribirArchivo(List<T> datos)
        {
            EjecutarConReintentos(() =>
            {
                using var stream = new FileStream(_rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                using var writer = new StreamWriter(stream);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                csv.WriteRecords(datos);
                csv.Flush();
                writer.Flush();
                stream.Flush();
            });
        }

        public void Registrar(T item)
        {
            Listar(); // Forzar inicialización si no lo estaba
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            _memoria.Add(item);
            SobrescribirArchivo(_memoria);
            Console.WriteLine("--> Registro guardado con éxito.");
        }

        public void MostrarConsola()
        {
            var catalogo = Listar();
            if (catalogo.Count == 0)
            {
                Console.WriteLine("--> La base de datos está vacía.");
                return;
            }

            foreach (var elemento in catalogo)
            {
                Console.WriteLine(elemento.ObtenerResumen());
            }
        }

        public void Actualizar(Guid idObjetivo, T itemEditado)
        {
            Listar();
            var indice = _memoria.FindIndex(x => x.Id == idObjetivo);
            
            if (indice >= 0)
            {
                itemEditado.Id = idObjetivo; // Mantiene el ID primario
                _memoria[indice] = itemEditado;
                SobrescribirArchivo(_memoria);
                Console.WriteLine("--> Registro actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("--> Error: No se ha encontrado el registro con ese ID.");
            }
        }

        public void Eliminar(Guid idObjetivo)
        {
            Listar();
            var registroSeleccionado = _memoria.FirstOrDefault(x => x.Id == idObjetivo);
            
            if (registroSeleccionado != null)
            {
                _memoria.Remove(registroSeleccionado);
                SobrescribirArchivo(_memoria);
                Console.WriteLine("--> Registro eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("--> Error: No se ha encontrado el registro con ese ID.");
            }
        }
    }
}
