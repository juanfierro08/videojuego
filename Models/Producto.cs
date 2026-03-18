using System;

namespace GameMaster
{
    public abstract class Producto : IEntidad
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        public abstract string ObtenerResumen();
    }
}
