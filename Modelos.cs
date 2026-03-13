using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace videojuegos
{
    // 5. Interfaz implementada
    public interface IPersistible
    {
        Guid Id { get; set; }
        string ObtenerDetalles();
    }

    // Clase Base Abstracta
    public abstract class Producto : IPersistible
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public abstract string ObtenerDetalles();
    }

    // 1. Relación de Herencia: Videojuego hereda de Producto
    public class Videojuego : Producto
    {
        public string Genero { get; set; }

        // 2. Relación de Asociación: Un desarrollador desarrolla juegos
        public Guid DesarrolladorId { get; set; }

        [Ignore] // Ignore de CsvHelper
        public Desarrollador DesarrolladorAsociado { get; set; }

        // 4. Relación de Composición: RequisitosSistema se instancian con Videojuego
        [Name("MinimoOS")]
        public string MinimoOS { get; set; }
        [Name("MinimoRAM")]
        public string MinimoRAM { get; set; }

        [Ignore]
        public RequisitosSistema Requisitos 
        { 
            get => new RequisitosSistema { OS = MinimoOS, RAM = MinimoRAM }; 
            set { MinimoOS = value?.OS; MinimoRAM = value?.RAM; } 
        }

        public Videojuego()
        {
            Requisitos = new RequisitosSistema(); // Instancia como parte de la composición
        }

        public override string ObtenerDetalles()
        {
            return $"[{Id}] Videojuego: {Nombre} | Género: {Genero} | Precio: ${Precio}";
        }
    }

    // Clase Composición
    public class RequisitosSistema
    {
        public string OS { get; set; }
        public string RAM { get; set; }
    }

    public class Desarrollador : IPersistible
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }

        public string ObtenerDetalles()
        {
            return $"[{Id}] Desarrollador: {Nombre} | País: {Pais}";
        }
    }

    public class Jugador : IPersistible
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public int Nivel { get; set; }

        public string ObtenerDetalles()
        {
            return $"[{Id}] Jugador: {Nickname} | Nivel: {Nivel}";
        }
    }

    // 3. Relación de Agregación: Un Equipo contiene muchos Jugadores que pueden existir sin el equipo
    public class EquipoEsports
    {
        public string NombreEquipo { get; set; }
        public List<Jugador> Miembros { get; set; } = new List<Jugador>();

        public void AgregarJugador(Jugador jugador)
        {
            Miembros.Add(jugador);
        }
    }
}
