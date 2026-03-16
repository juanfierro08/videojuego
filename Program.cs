using System;

namespace GameMaster
{
    class Program
    {
        static PersistenciaCSV<Videojuego> dbVideojuegos = new PersistenciaCSV<Videojuego>("videojuegos.csv");
        static PersistenciaCSV<Desarrolladora> dbDesarrolladoras = new PersistenciaCSV<Desarrolladora>("desarrolladoras.csv");
        static PersistenciaCSV<Cliente> dbClientes = new PersistenciaCSV<Cliente>("clientes.csv");

        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n=== SISTEMA DE GESTIÓN DE VIDEOJUEGOS ===");
                Console.WriteLine("1. Gestión de Videojuegos");
                Console.WriteLine("2. Gestión de Desarrolladoras");
                Console.WriteLine("3. Gestión de Clientes");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                string? opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1": MenuCRUD("Videojuegos", GestionarVideojuegos); break;
                    case "2": MenuCRUD("Desarrolladoras", GestionarDesarrolladoras); break;
                    case "3": MenuCRUD("Clientes", GestionarClientes); break;
                    case "4": salir = true; break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
        }

        static void MenuCRUD(string entidad, Action<string> accionCRUD)
        {
            bool volver = false;
            while (!volver)
            {
                Console.WriteLine($"\n--- MÓDULO DE {entidad.ToUpper()} ---");
                Console.WriteLine("1. Crear");
                Console.WriteLine("2. Leer / Listar");
                Console.WriteLine("3. Actualizar");
                Console.WriteLine("4. Eliminar");
                Console.WriteLine("5. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                string? opcion = Console.ReadLine();
                if (opcion == "5") volver = true;
                else accionCRUD(opcion ?? "");
            }
        }

        static void GestionarVideojuegos(string operacion)
        {
            switch (operacion)
            {
                case "1": 
                    var juego = new Videojuego();
                    Console.Write("Nombre: "); juego.Nombre = Console.ReadLine() ?? "";
                    Console.Write("Género: "); juego.Genero = Console.ReadLine() ?? "";
                    Console.Write("Precio: "); decimal.TryParse(Console.ReadLine(), out decimal precio); juego.Precio = precio;
                    Console.Write("Requisitos Generales (Ej. 8GB RAM, i5): "); juego.RequisitosRaw = Console.ReadLine() ?? "";

                    Console.Write("ID de Desarrolladora (Dejar vacío si no aplica): ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid devId)) juego.DesarrolladoraId = devId;

                    dbVideojuegos.Registrar(juego);
                    break;
                case "2":
                    dbVideojuegos.MostrarConsola();
                    break;
                case "3": 
                    Console.Write("Ingrese el ID del Videojuego a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizarV))
                    {
                        var jEditado = new Videojuego();
                        Console.Write("Nuevo Nombre: "); jEditado.Nombre = Console.ReadLine() ?? "";
                        Console.Write("Nuevo Género: "); jEditado.Genero = Console.ReadLine() ?? "";
                        Console.Write("Nuevo Precio: "); decimal.TryParse(Console.ReadLine(), out decimal p); jEditado.Precio = p;
                        Console.Write("Nuevos Requisitos: "); jEditado.RequisitosRaw = Console.ReadLine() ?? "";
                        dbVideojuegos.Actualizar(idActualizarV, jEditado);
                    }
                    else Console.WriteLine("Formato de ID inválido.");
                    break;
                case "4"
                    Console.Write("Ingrese el ID del Videojuego a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminarV)) dbVideojuegos.Eliminar(idEliminarV);
                    else Console.WriteLine("Formato de ID inválido.");
                    break;
            }
        }
    
        static void GestionarDesarrolladoras(string operacion)
        {
            switch (operacion)
            {
                case "1": 
                    var dev = new Desarrolladora();
                    Console.Write("Nombre de Empresa: "); dev.NombreEmpresa = Console.ReadLine() ?? "";
                    Console.Write("País de Origen: "); dev.PaisOrigen = Console.ReadLine() ?? "";
                    dbDesarrolladoras.Registrar(dev);
                    break;
                case "2": 
                    dbDesarrolladoras.MostrarConsola();
                    break;
                case "3":
                    Console.Write("Ingrese el ID de la Desarrolladora a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizarD))
                    {
                        var devEditado = new Desarrolladora();
                        Console.Write("Nuevo Nombre: "); devEditado.NombreEmpresa = Console.ReadLine() ?? "";
                        Console.Write("Nuevo País: "); devEditado.PaisOrigen = Console.ReadLine() ?? "";
                        dbDesarrolladoras.Actualizar(idActualizarD, devEditado);
                    }
                    else Console.WriteLine("Formato de ID inválido.");
                    break;
                case "4":
                    Console.Write("Ingrese el ID de la Desarrolladora a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminarD)) dbDesarrolladoras.Eliminar(idEliminarD);
                    else Console.WriteLine("Formato de ID inválido.");
                    break;
            }
        }

   
        static void GestionarClientes(string operacion)
        {
            switch (operacion)
            {
                case "1": 
                    var cli = new Cliente();
                    Console.Write("Nombre Completo: "); cli.NombreCompleto = Console.ReadLine() ?? "";
                    Console.Write("Correo Electrónico: "); cli.Correo = Console.ReadLine() ?? "";
                    dbClientes.Registrar(cli);
                    break;
                case "2": 
                    dbClientes.MostrarConsola();
                    break;
                case "3":
                    Console.Write("Ingrese el ID del Cliente a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizarC))
                    {
                        var cliEditado = new Cliente();
                        Console.Write("Nuevo Nombre: "); cliEditado.NombreCompleto = Console.ReadLine() ?? "";
                        Console.Write("Nuevo Correo: "); cliEditado.Correo = Console.ReadLine() ?? "";
                        dbClientes.Actualizar(idActualizarC, cliEditado);
                    }
                    else Console.WriteLine("Formato de ID inválido.");
                    break;
                case "4":
                    Console.Write("Ingrese el ID del Cliente a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminarC)) dbClientes.Eliminar(idEliminarC);
                    else Console.WriteLine("Formato de ID inválido.");
                    break;
            }
        }
    }
}


