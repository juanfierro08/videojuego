using System;

namespace videojuegos
{
    class Program
    {
        static ServicioCSV<Videojuego> svVideojuegos = new ServicioCSV<Videojuego>("videojuegos.csv");
        static ServicioCSV<Desarrollador> svDesarrollador = new ServicioCSV<Desarrollador>("desarrolladores.csv");
        static ServicioCSV<Jugador> svJugadores = new ServicioCSV<Jugador>("jugadores.csv");

        static void Main(string[] args)
        {
            int opcion = -1;
            while (opcion != 0)
            {
                Console.WriteLine("\n--- SISTEMA DE VIDEOJUEGOS ---");
                Console.WriteLine("1. Gestión de Videojuegos");
                Console.WriteLine("2. Gestión de Desarrolladores");
                Console.WriteLine("3. Gestión de Jugadores");
                Console.WriteLine("4. Test de Agregación (Esports)");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1: MenuVideojuegos(); break;
                        case 2: MenuDesarrolladores(); break;
                        case 3: MenuJugadores(); break;
                        case 4: TestAgregacion(); break;
                        case 0: Console.WriteLine("Saliendo..."); break;
                        default: Console.WriteLine("Opción no válida."); break;
                    }
                }
            }
        }

        // --- Menú Videojuegos ---
        static void MenuVideojuegos()
        {
            Console.WriteLine("\n--- CRUD VIDEOJUEGOS ---");
            Console.WriteLine("1. Crear\n2. Listar\n3. Actualizar\n4. Eliminar");
            var opc = Console.ReadLine();
            switch (opc)
            {
                case "1":
                    var vj = new Videojuego();
                    Console.Write("Nombre: "); vj.Nombre = Console.ReadLine();
                    Console.Write("Precio: "); 
                    if(decimal.TryParse(Console.ReadLine(), out decimal precio)) vj.Precio = precio;
                    Console.Write("Género: "); vj.Genero = Console.ReadLine();
                    Console.Write("OS Mínimo (Composición): "); vj.MinimoOS = Console.ReadLine();
                    Console.Write("RAM Mínima (Composición): "); vj.MinimoRAM = Console.ReadLine();
                    svVideojuegos.Crear(vj);
                    break;
                case "2": svVideojuegos.ListarTodos(); break;
                case "3":
                    Console.Write("ID a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizar))
                    {
                        var nvj = new Videojuego();
                        Console.Write("Nuevo Nombre: "); nvj.Nombre = Console.ReadLine();
                        Console.Write("Nuevo Precio: ");
                        if(decimal.TryParse(Console.ReadLine(), out decimal num_precio)) nvj.Precio = num_precio;
                        Console.Write("Nuevo Género: "); nvj.Genero = Console.ReadLine();
                        Console.Write("Nuevo OS Mínimo: "); nvj.MinimoOS = Console.ReadLine();
                        Console.Write("Nueva RAM Mínima: "); nvj.MinimoRAM = Console.ReadLine();
                        svVideojuegos.Actualizar(idActualizar, nvj);
                    }
                    break;
                case "4":
                    Console.Write("ID a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminar))
                        svVideojuegos.Eliminar(idEliminar);
                    break;
            }
        }

        // --- Menú Desarrolladores ---
        static void MenuDesarrolladores()
        {
            Console.WriteLine("\n--- CRUD DESARROLLADORES ---");
            Console.WriteLine("1. Crear\n2. Listar\n3. Actualizar\n4. Eliminar");
            var opc = Console.ReadLine();
            switch (opc)
            {
                case "1":
                    var dev = new Desarrollador();
                    Console.Write("Nombre: "); dev.Nombre = Console.ReadLine();
                    Console.Write("País: "); dev.Pais = Console.ReadLine();
                    svDesarrollador.Crear(dev);
                    break;
                case "2": svDesarrollador.ListarTodos(); break;
                case "3":
                    Console.Write("ID a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizar))
                    {
                        var ndev = new Desarrollador();
                        Console.Write("Nuevo Nombre: "); ndev.Nombre = Console.ReadLine();
                        Console.Write("Nuevo País: "); ndev.Pais = Console.ReadLine();
                        svDesarrollador.Actualizar(idActualizar, ndev);
                    }
                    break;
                case "4":
                    Console.Write("ID a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminar))
                        svDesarrollador.Eliminar(idEliminar);
                    break;
            }
        }

        // --- Menú Jugadores ---
        static void MenuJugadores()
        {
            Console.WriteLine("\n--- CRUD JUGADORES ---");
            Console.WriteLine("1. Crear\n2. Listar\n3. Actualizar\n4. Eliminar");
            var opc = Console.ReadLine();
            switch (opc)
            {
                case "1":
                    var j = new Jugador();
                    Console.Write("Nickname: "); j.Nickname = Console.ReadLine();
                    Console.Write("Nivel: "); 
                    if(int.TryParse(Console.ReadLine(), out int nivel)) j.Nivel = nivel;
                    svJugadores.Crear(j);
                    break;
                case "2": svJugadores.ListarTodos(); break;
                case "3":
                    Console.Write("ID a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizar))
                    {
                        var nj = new Jugador();
                        Console.Write("Nuevo Nickname: "); nj.Nickname = Console.ReadLine();
                        Console.Write("Nuevo Nivel: "); 
                        if(int.TryParse(Console.ReadLine(), out int nnivel)) nj.Nivel = nnivel;
                        svJugadores.Actualizar(idActualizar, nj);
                    }
                    break;
                case "4":
                    Console.Write("ID a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminar))
                        svJugadores.Eliminar(idEliminar);
                    break;
            }
        }

        static void TestAgregacion()
        {
            Console.WriteLine("--- Test de Agregación: Equipo Esports ---");
            var equipo = new EquipoEsports { NombreEquipo = "Team Liquid" };
            
            var jugadores = svJugadores.LeerTodos();
            if (jugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores. Por favor cree un jugador en Menú Jugadores.");
                return;
            }

            equipo.AgregarJugador(jugadores[0]); // Agrega el primer jugador si existe
            Console.WriteLine($"Se ha agregado el jugador '{jugadores[0].Nickname}' al equipo '{equipo.NombreEquipo}'.");
            Console.WriteLine("Esta es una relación de Agregación, donde el Jugador existe independientemente del equipo.");
        }
    }
}
