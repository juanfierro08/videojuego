using System.Collections.Generic;

namespace GameMaster
{
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
