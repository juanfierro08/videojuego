using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GameMaster
{
    public class PersistenciaCSV<T> where T : IEntidad
    {
        private readonly string _rutaArchivo;

        public PersistenciaCSV(string nombreArchivo)
        {
            _rutaArchivo = nombreArchivo;
        }

        // --- READ: OBTENER TODOS LOS REGISTROS ---
        public List<T> Listar()
        {
            if (!File.Exists(_rutaArchivo)) return new List<T>();
            
            using var reader = new StreamReader(_rutaArchivo);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                HeaderValidated = null
            };
            using var csv = new CsvReader(reader, config);
            return csv.GetRecords<T>().ToList();
        }

        private void SobrescribirArchivo(List<T> datos)
        {
            using var writer = new StreamWriter(_rutaArchivo);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(datos);
        }

        // --- CREATE: AÑADIR NUEVO REGISTRO ---
        public void Registrar(T item)
        {
            var catalogo = Listar();
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }
            catalogo.Add(item);
            SobrescribirArchivo(catalogo);
            Console.WriteLine("--> Registro creado con éxito.");
        }

        // --- READ: MOSTRAR EN PANTALLA ---
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

        // --- UPDATE: ACTUALIZAR EXISTENTE ---
        public void Actualizar(Guid idObjetivo, T itemEditado)
        {
            var catalogo = Listar();
            var indice = catalogo.FindIndex(x => x.Id == idObjetivo);
            
            if (indice >= 0)
            {
                itemEditado.Id = idObjetivo; // Mantiene el ID primario
                catalogo[indice] = itemEditado;
                SobrescribirArchivo(catalogo);
                Console.WriteLine("--> Registro actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("--> Error: No se ha encontrado el registro con ese ID.");
            }
        }

        // --- DELETE: ELIMINAR REGISTRO ---
        public void Eliminar(Guid idObjetivo)
        {
            var catalogo = Listar();
            var registroSeleccionado = catalogo.FirstOrDefault(x => x.Id == idObjetivo);
            
            if (registroSeleccionado != null)
            {
                catalogo.Remove(registroSeleccionado);
                SobrescribirArchivo(catalogo);
                Console.WriteLine("--> Registro eliminado del CRUD exitosamente.");
            }
            else
            {
                Console.WriteLine("--> Error: No se ha encontrado el registro con ese ID.");
            }
        }
    }
}
