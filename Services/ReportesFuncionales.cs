using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMaster
{
    // [PARADIGMA FUNCIONAL]: Clase estática que contiene funciones puras (sin efectos secundarios ni mutación de estado).
    public static class ReportesFuncionales
    {
        // [PARADIGMA FUNCIONAL]: Uso de LINQ (Where, Select, Aggregate) y función de alto orden (Func<Videojuego, bool>).
        // Función Pura: Recibe los datos y el filtro, retorna un decimal nuevo. No modifica las listas.
        public static decimal CalcularValorInventario(IEnumerable<Videojuego> videojuegos, Func<Videojuego, bool> filtro)
        {
            return videojuegos
                .Where(filtro) // 1. LINQ: Filtrar
                .Select(v => v.Precio) // 2. LINQ: Proyectar
                .Aggregate(0m, (total, precio) => total + precio); // 3. LINQ: Agregar/Reducir
        }

        // [PARADIGMA FUNCIONAL]: Función pura que retorna transformaciones de datos usando LINQ.
        public static IEnumerable<string> ObtenerNombresPorGenero(IEnumerable<Videojuego> videojuegos, string generoEspecifico)
        {
            return videojuegos
                .Where(v => v.Genero.Equals(generoEspecifico, StringComparison.OrdinalIgnoreCase))
                .Select(v => v.Nombre);
        }

        // [PARADIGMA FUNCIONAL]: Función de orden superior que recibe Func<> (condición pura) y Action<> (efecto a aplicar por el consumidor).
        public static void MostrarReportePersonalizado<T>(IEnumerable<T> datos, Func<T, bool> condicion, Action<T> accionMostrar)
        {
            var resultados = datos.Where(condicion);
            foreach (var item in resultados)
            {
                accionMostrar(item);
            }
        }
    }
}
