using System;

namespace GameMaster
{
    // [PARADIGMA ORIENTADO A EVENTOS]: Clases EventArgs propias que transmiten el estado del dominio.
    public class PedidoCreadoEventArgs : EventArgs
    {
        public Pedido Pedido { get; }
        public PedidoCreadoEventArgs(Pedido pedido) => Pedido = pedido;
    }

    public class VideojuegoCreadoEventArgs : EventArgs
    {
        public Videojuego Videojuego { get; }
        public VideojuegoCreadoEventArgs(Videojuego videojuego) => Videojuego = videojuego;
    }

    // [PARADIGMA ORIENTADO A EVENTOS]: Eventos del sistema semánticamente significativos.
    public static class EventosDominio
    {
        public static event EventHandler<PedidoCreadoEventArgs>? PedidoCreado;
        public static event EventHandler<VideojuegoCreadoEventArgs>? VideojuegoCreado;

        public static void DispararPedidoCreado(object sender, Pedido pedido)
        {
            PedidoCreado?.Invoke(sender, new PedidoCreadoEventArgs(pedido));
        }

        public static void DispararVideojuegoCreado(object sender, Videojuego videojuego)
        {
            VideojuegoCreado?.Invoke(sender, new VideojuegoCreadoEventArgs(videojuego));
        }
    }
}
