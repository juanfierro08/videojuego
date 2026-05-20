using System;

namespace GameMaster
{
    public interface IEntidad
    {
        Guid Id { get; set; }
        string ObtenerResumen();
    }
}
