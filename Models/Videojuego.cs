using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace GameMaster
{
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
            return $"[ID: {Id}] Videojuego: {Nombre} ({Genero}) | Precio: ${Precio} | Requisitos: {RequisitosRaw}";
        }
    }
}
