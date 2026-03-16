using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace GameMaster
{
    // --- INTERFAZ IMPLEMENTADA ---
    public interface IEntidad
    {
        Guid Id { get; set; }
        string ObtenerResumen();
    }

    // -- CLASE BASE (Para Herencia) --
    public abstract class Producto : IEntidad
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        public abstract string ObtenerResumen();
    }

    // --- HERENCIA (Videojuego y Consola heredan de Producto) ---
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

        // --- ASOCIACIÓN (Videojuego -> Desarrolladora) ---
        // Se guarda el ID de la desarrolladora en el CSV para mantener la asociación relacional.
        public Guid DesarrolladoraId { get; set; }
        
        [Ignore] // Referencia en memoria
        public Desarrolladora? EstudioCreador { get; set; }

        // --- COMPOSICIÓN (Videojuego -> RequisitoSistema) ---
        // Los requisitos no existen fuera del videojuego. En CSV guardaremos una cadena plana por simplicidad,
        // pero en el objeto en memoria es una lista propia instanciada al construir.
        [Ignore]
        public List<RequisitoSistema> RequisitosMinimos { get; private set; }

        // Mapeo plano para el CSV (Almacena el detalle general de requisitos como string)
        [Name("RequisitosRaw")]
        public string RequisitosRaw { get; set; } = string.Empty;

        public Videojuego()
        {
            // Composición: La lista de requisitos se crea junto al Videojuego y deja de existir con él.
            RequisitosMinimos = new List<RequisitoSistema>();
        }

        public override string ObtenerResumen()
        {
            return $"Videojuego: {Nombre} ({Genero}) | Precio: ${Precio} | Requisitos: {RequisitosRaw}";
        }
    }

    // Clase auxiliar para Composición
    public class RequisitoSistema
    {
        public string Componente { get; set; } = string.Empty; // Ej. CPU, GPU
        public string Especificacion { get; set; } = string.Empty; // Ej. i5 10400F, RTX 3060
    }

    // --- ENTIDAD DESARROLLADORA ---
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

    // --- ENTIDAD CLIENTE ---
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

    // --- AGREGACIÓN (Tienda -> List<Videojuego>) ---
    public class Tienda
    {
        public string NombreSucursal { get; set; } = string.Empty;

        // Agregación: La Tienda agrupa a los videojuegos en su inventario, pero los videojuegos
        // existen independientemente de esta sucursal específica.
        public List<Videojuego> InventarioVideojuegos { get; set; } = new List<Videojuego>();

        public void AgregarAlInventario(Videojuego juego)
        {
            InventarioVideojuegos.Add(juego);
        }
    }
}
