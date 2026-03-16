using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace GameMaster
{
 
    public interface IEntidad
    {
        Guid Id { get; set; }
        string ObtenerResumen();
    }

    public abstract class Producto : IEntidad
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        public abstract string ObtenerResumen();
    }

    public class Consola : Producto
    {
        public string Fabricante { get; set; } = string.Empty;

        public override string ObtenerResumen()
        {
            return $"Consola: {Nombre} | Fabricante: {Fabricante} | Precio: ${Precio}";
        }
    }

    public class Videojuego : Producto
    {
        public string Genero { get; set; } = string.Empty;
       
        public Guid DesarrolladoraId { get; set; }

        [Ignore] // Referencia en memoria
        public Desarrolladora? EstudioCreador { get; set; }
        [Ignore]
        public List<RequisitoSistema> RequisitosMinimos { get; private set; }

        [Name("RequisitosRaw")]
        public string RequisitosRaw { get; set; } = string.Empty;

        public Videojuego()
        {
        
            RequisitosMinimos = new List<RequisitoSistema>();
        }

        public override string ObtenerResumen()
        {
            return $"Videojuego: {Nombre} ({Genero}) | Precio: ${Precio} | Requisitos: {RequisitosRaw}";
        }
    }

    public class RequisitoSistema
    {
        public string Componente { get; set; } = string.Empty;
        public string Especificacion { get; set; } = string.Empty;
    }

    public class Desarrolladora : IEntidad
    {
        public Guid Id { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;

        public string ObtenerResumen()
        {
            return $"Desarrolladora: {NombreEmpresa} | País: {PaisOrigen}";
        }
    }

    public class Cliente : IEntidad
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        public string ObtenerResumen()
        {
            return $"Cliente: {NombreCompleto} | Contacto: {Correo}";
        }
    }

    public class Tienda
    {
        public string NombreSucursal { get; set; } = string.Empty;

        public List<Videojuego> InventarioVideojuegos { get; set; } = new List<Videojuego>();

        public void AgregarAlInventario(Videojuego juego)
        {
            InventarioVideojuegos.Add(juego);
        }
    }
}
