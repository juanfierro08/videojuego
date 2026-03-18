namespace GameMaster
{
    public class Consola : Producto
    {
        public string Fabricante { get; set; } = string.Empty;

        public override string ObtenerResumen()
        {
            return $"Consola: {Nombre} | Fabricante: {Fabricante} | Precio: ${Precio}";
        }
    }
}
