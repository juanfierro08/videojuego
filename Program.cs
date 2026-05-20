#pragma warning disable IDE0130
using System;
using System.Collections.Generic;
using Castle.Windsor;
using Castle.MicroKernel.Registration;

namespace GameMaster
{
    class Program
    {
        // [PARADIGMA ORIENTADO A ASPECTOS]: Contenedor de Inversión de Control (IoC)
        private static IWindsorContainer _container = null!;
        
        // [PRINCIPIOS SOLID - DIP]: Dependemos de abstracciones (interfaces), no de implementaciones concretas
        private static IPersistenciaCSV<Videojuego> dbVideojuegos = null!;
        private static IPersistenciaCSV<Cliente> dbClientes = null!;
        private static IPersistenciaCSV<Pedido> dbPedidos = null!;
        
        // [PARADIGMA ORIENTADO A OBJETOS]: Agregación física
        static readonly Tienda miTiendaFisica = new() { NombreSucursal = "GameMaster Central" };

        static void Main()
        {
            // 1. AOP - Configuración de Castle Windsor y resolución de dependencias
            ConfigurarContenedorDI();

            // 2. Eventos - Suscripción de los Event Handlers reactivos
            NotificadorEventos.Suscribir();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n=== SISTEMA DE GESTIÓN DE VIDEOJUEGOS ===");
                Console.WriteLine("1. Gestión de Videojuegos");
                Console.WriteLine("2. Gestión de Pedidos");
                Console.WriteLine("3. Consultas Avanzadas ");
                Console.WriteLine("4. Ver Catálogo de Productos ");
                Console.WriteLine("5. Gestión de Clientes ");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                string? opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1": MenuCRUD("Videojuegos", GestionarVideojuegos); break;
                    case "2": MenuCRUD("Pedidos", GestionarPedidos); break;
                    case "3": MenuReportes(); break;
                    case "4": DemostrarPolimorfismo(); break;
                    case "5": MenuCRUD("Clientes", GestionarClientes); break;
                    case "6": salir = true; break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
        }

        static void ConfigurarContenedorDI()
        {
            _container = new WindsorContainer();

            // Registrar Interceptores AOP
            _container.Register(Component.For<LoggingInterceptor>());
            _container.Register(Component.For<ErrorHandlingInterceptor>());

            // Registrar y resolver Servicios (simulando DI de constructores en la raíz)
            // Se inyecta la ruta en el constructor usando DependsOn
            _container.Register(Component.For<IPersistenciaCSV<Videojuego>>()
                .ImplementedBy<PersistenciaCSV<Videojuego>>()
                .DependsOn(Dependency.OnValue("nombreArchivo", @"Data\videojuegos_nuevo.csv"))
                .Interceptors<ErrorHandlingInterceptor, LoggingInterceptor>());


            _container.Register(Component.For<IPersistenciaCSV<Cliente>>()
                .ImplementedBy<PersistenciaCSV<Cliente>>()
                .DependsOn(Dependency.OnValue("nombreArchivo", @"Data\clientes.csv"))
                .Interceptors<ErrorHandlingInterceptor, LoggingInterceptor>());
                
            _container.Register(Component.For<IPersistenciaCSV<Pedido>>()
                .ImplementedBy<PersistenciaCSV<Pedido>>()
                .DependsOn(Dependency.OnValue("nombreArchivo", @"Data\pedidos.csv"))
                .Interceptors<ErrorHandlingInterceptor, LoggingInterceptor>());

            dbVideojuegos = _container.Resolve<IPersistenciaCSV<Videojuego>>();
            dbClientes = _container.Resolve<IPersistenciaCSV<Cliente>>();
            dbPedidos = _container.Resolve<IPersistenciaCSV<Pedido>>();
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
                    Console.Write("Precio: "); 
                    if (decimal.TryParse(Console.ReadLine(), out decimal precio)) juego.Precio = precio;
                    
                    Console.WriteLine("--- Requisitos del Sistema ---");
                    Console.Write("Procesador (CPU): "); string cpu = Console.ReadLine() ?? "N/A";
                    Console.Write("Memoria RAM: "); string ram = Console.ReadLine() ?? "N/A";
                    Console.Write("Tarjeta Gráfica (GPU): "); string gpu = Console.ReadLine() ?? "N/A";
                    Console.Write("Almacenamiento: "); string almacenamiento = Console.ReadLine() ?? "N/A";
                    
                    // [PARADIGMA FUNCIONAL]: Uso de Records inmutables
                    var reqCpu = new RequisitoSistema("CPU", cpu);
                    var reqRam = new RequisitoSistema("RAM", ram);
                    var reqGpu = new RequisitoSistema("GPU", gpu);
                    var reqAlm = new RequisitoSistema("Almacenamiento", almacenamiento);
                    
                    juego.RequisitosRaw = $"{reqCpu.Componente}: {reqCpu.Especificacion} | {reqRam.Componente}: {reqRam.Especificacion} | {reqGpu.Componente}: {reqGpu.Especificacion} | {reqAlm.Componente}: {reqAlm.Especificacion}";
                    
                    dbVideojuegos.Registrar(juego);
                    // [PARADIGMA ORIENTADO A EVENTOS]: Se dispara evento de creación de Videojuego
                    EventosDominio.DispararVideojuegoCreado(null!, juego);
                    break;
                case "2": dbVideojuegos.MostrarConsola(); break;
                case "3": 
                    Console.Write("Ingrese el ID del Videojuego a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizarV))
                    {
                        var jEditado = new Videojuego();
                        Console.Write("Nuevo Nombre: "); jEditado.Nombre = Console.ReadLine() ?? "";
                        Console.Write("Nuevo Precio: "); 
                        if (decimal.TryParse(Console.ReadLine(), out decimal p)) jEditado.Precio = p;
                        dbVideojuegos.Actualizar(idActualizarV, jEditado);
                    }
                    break;
                case "4":
                    Console.Write("Ingrese el ID a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminarV)) dbVideojuegos.Eliminar(idEliminarV);
                    break;
            }
        }
    

        static void GestionarClientes(string operacion)
        {
            switch (operacion)
            {
                case "1": 
                    var cliente = new Cliente();
                    Console.Write("Nombre Completo: "); cliente.NombreCompleto = Console.ReadLine() ?? "";
                    Console.Write("Correo Electrónico: "); cliente.Correo = Console.ReadLine() ?? "";
                    dbClientes.Registrar(cliente);
                    break;
                case "2": dbClientes.MostrarConsola(); break;
                case "3": 
                    Console.Write("Ingrese el ID del Cliente a actualizar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idActualizarC))
                    {
                        var cEditado = new Cliente();
                        Console.Write("Nuevo Nombre: "); cEditado.NombreCompleto = Console.ReadLine() ?? "";
                        Console.Write("Nuevo Correo: "); cEditado.Correo = Console.ReadLine() ?? "";
                        dbClientes.Actualizar(idActualizarC, cEditado);
                    }
                    break;
                case "4":
                    Console.Write("Ingrese el ID a eliminar: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid idEliminarC)) dbClientes.Eliminar(idEliminarC);
                    break;
            }
        }

        static void GestionarPedidos(string operacion)
        {
            switch (operacion)
            {
                case "1": 
                    var pedido = new Pedido();
                    Console.Write("ID del Cliente que compra: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid cId)) pedido.ClienteId = cId;
                    
                    Console.Write("Total a Pagar: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal t)) pedido.Total = t;

                    dbPedidos.Registrar(pedido);
                    
                    // [PARADIGMA ORIENTADO A EVENTOS]: Disparar la orden de Pedido para notificar al cliente.
                    EventosDominio.DispararPedidoCreado(null!, pedido);
                    break;
                case "2": dbPedidos.MostrarConsola(); break;
            }
        }

        static void MenuReportes()
        {
            Console.WriteLine("\n--- CONSULTAS FUNCIONALES (LINQ & Funciones Puras) ---");
            var juegos = dbVideojuegos.Listar();
            
            // [PARADIGMA FUNCIONAL]: Uso de función pura pasando comportamiento como Func<>
            decimal totalInventario = ReportesFuncionales.CalcularValorInventario(juegos, j => j.Precio > 0);
            Console.WriteLine($"\nValor total del Inventario de todos los juegos: ${totalInventario}");

            Console.Write("\n¿De qué Género quieres filtrar los juegos? (Ej. Acción, Aventura, RPG): ");
            string generoFiltro = Console.ReadLine() ?? "Acción";

            Console.WriteLine($"\nJuegos del género '{generoFiltro}':");
            // [PARADIGMA FUNCIONAL]: Uso de función de alto orden (Action<>) para imprimir en consola de forma pura.
            ReportesFuncionales.MostrarReportePersonalizado(juegos, j => j.Genero.Equals(generoFiltro, StringComparison.OrdinalIgnoreCase), 
                j => Console.WriteLine($"- {j.Nombre} (${j.Precio})"));
        }

        // [PARADIGMA ORIENTADO A OBJETOS]: Polimorfismo.
        static void DemostrarPolimorfismo()
        {
            Console.WriteLine("\n--- POLIMORFISMO (POO) ---");
            List<Producto> catalogoBase = new List<Producto>();
            
            // 1. Cargamos los videojuegos reales que creaste en el CSV
            var juegosGuardados = dbVideojuegos.Listar();
            catalogoBase.AddRange(juegosGuardados);
            Console.WriteLine($"Se han cargado {juegosGuardados.Count} videojuegos desde la base de datos a la lista de Productos.");

            // 2. Te permitimos crear una Consola en vivo
            Console.Write("\n¿Deseas crear una Consola en este momento para añadirla a la lista mixta? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                var consola = new Consola { Id = Guid.NewGuid() };
                Console.Write("Nombre de la Consola (Ej. Xbox Series X): "); consola.Nombre = Console.ReadLine() ?? "";
                Console.Write("Fabricante (Ej. Microsoft): "); consola.Fabricante = Console.ReadLine() ?? "";
                Console.Write("Precio: "); 
                if (decimal.TryParse(Console.ReadLine(), out decimal p)) consola.Precio = p;
                
                catalogoBase.Add(consola);
            }

            Console.WriteLine("\n--- POLIMORFISMO ---");
            foreach (var producto in catalogoBase)
            {
                // Aquí ocurre la magia: Aunque todos están guardados como "Producto", 
                // C# sabe si llamar al ObtenerResumen() del Videojuego o de la Consola.
                Console.WriteLine(producto.ObtenerResumen());
            }
        }
    }
}
