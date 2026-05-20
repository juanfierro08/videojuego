using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace GameMaster
{
    // [PARADIGMA ORIENTADO A OBJETOS]: Entidad Pedido que demuestra Asociación con Cliente y Videojuegos.
    public class Pedido : IEntidad
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        
        // Referencia en memoria
        [Ignore]
        public Cliente? ClienteAsociado { get; set; }
        
        // Lista de IDs de videojuegos comprados (para CSV)
        public string VideojuegosIdsCsv { get; set; } = string.Empty;

        // Propiedad calculada
        public decimal Total { get; set; }
        public DateTime FechaCompra { get; set; }

        public Pedido()
        {
            FechaCompra = DateTime.Now;
        }

        public string ObtenerResumen()
        {
            return $"[ID: {Id}] Pedido el {FechaCompra.ToShortDateString()} | Total: ${Total}";
        }
    }
}
