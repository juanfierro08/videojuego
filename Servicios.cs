using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace videojuegos
{
    public class ServicioCSV<T> where T : IPersistible
    {
        private readonly string _filePath;

        public ServicioCSV(string fileName)
        {
            _filePath = fileName;
        }

        public List<T> LeerTodos()
        {
            if (!File.Exists(_filePath)) return new List<T>();
            
            using var reader = new StreamReader(_filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }

        public void EscribirTodos(List<T> registros)
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(registros);
        }

        #region Operaciones CRUD
        // C - Create
        public void Crear(T entidad)
        {
            var registros = LeerTodos();
            if (entidad.Id == Guid.Empty)
                entidad.Id = Guid.NewGuid();
            registros.Add(entidad);
            EscribirTodos(registros);
            Console.WriteLine("Registro creado con éxito.");
        }

        // R - Read
        public void ListarTodos()
        {
            var registros = LeerTodos();
            if (!registros.Any())
            {
                Console.WriteLine("No hay registros disponibles.");
                return;
            }

            foreach (var registro in registros)
            {
                Console.WriteLine(registro.ObtenerDetalles());
            }
        }

        // U - Update
        public void Actualizar(Guid id, T nuevaEntidad)
        {
            var registros = LeerTodos();
            var index = registros.FindIndex(r => r.Id == id);
            
            if (index != -1)
            {
                nuevaEntidad.Id = id; // Mantener el mismo Id
                registros[index] = nuevaEntidad;
                EscribirTodos(registros);
                Console.WriteLine("Registro actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("Registro no encontrado.");
            }
        }

        // D - Delete
        public void Eliminar(Guid id)
        {
            var registros = LeerTodos();
            var registroAEliminar = registros.FirstOrDefault(r => r.Id == id);
            
            if (registroAEliminar != null)
            {
                registros.Remove(registroAEliminar);
                EscribirTodos(registros);
                Console.WriteLine("Registro eliminado con éxito.");
            }
            else
            {
                Console.WriteLine("Registro no encontrado.");
            }
        }
        #endregion
    }
}
