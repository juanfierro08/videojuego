using System;

namespace GameMaster
{
    // [PARADIGMA ORIENTADO A EVENTOS]: Suscriptor reactivo que maneja las consecuencias de los eventos.
    public static class NotificadorEventos
    {
        // Método que enlaza los eventos con sus respectivos manejadores
        public static void Suscribir()
        {
            EventosDominio.PedidoCreado += OnPedidoCreado;
            EventosDominio.VideojuegoCreado += OnVideojuegoCreado;
        }

        private static void OnPedidoCreado(object? sender, PedidoCreadoEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[EVENTO REACTIVO] ¡Notificación! Nuevo Pedido Creado (ID: {e.Pedido.Id})");
            Console.WriteLine($"[EVENTO REACTIVO] -> Simulando envío de comprobante de pago por un Total de ${e.Pedido.Total}");
            Console.ResetColor();
        }

        private static void OnVideojuegoCreado(object? sender, VideojuegoCreadoEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[EVENTO REACTIVO] ¡Alerta de Inventario! Nuevo Videojuego ({e.Videojuego.Nombre})");
            Console.WriteLine($"[EVENTO REACTIVO] -> Actualizando el catálogo web para clientes.");
            Console.ResetColor();
        }
    }
}
