using System;
using System.Collections.Generic;

namespace GameMaster
{
    // [PARADIGMA ORIENTADO A ASPECTOS] y [PRINCIPIOS SOLID - DIP/OCP]: 
    // Interfaz para la resolución de dependencias a través de Castle Windsor.
    public interface IPersistenciaCSV<T> where T : class, IEntidad, new()
    {
        void Registrar(T entidad);
        List<T> Listar();
        void MostrarConsola();
        void Actualizar(Guid id, T nuevaEntidad);
        void Eliminar(Guid id);
    }
}
