using System;

namespace GameMaster
{
    public class Cliente : IEntidad
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        public string ObtenerResumen()
        {
            return $"[ID: {Id}] Cliente: {NombreCompleto} | Contacto: {Correo}";
        }
    }
}
