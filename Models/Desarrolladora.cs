using System;

namespace GameMaster
{
    public class Desarrolladora : IEntidad
    {
        public Guid Id { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;

        public string ObtenerResumen()
        {
            return $"[ID: {Id}] Desarrolladora: {NombreEmpresa} | País: {PaisOrigen}";
        }
    }
}
