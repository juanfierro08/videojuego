# Sistema de Gestión de Videojuegos (Multiparadigma)

Este proyecto es un Sistema de Gestión para una tienda de videojuegos desarrollado en C# (.NET 8). El sistema permite administrar videojuegos, clientes y pedidos, y fue diseñado aplicando rigurosamente cuatro paradigmas de programación distintos, así como los principios SOLID.

## 📌 Descripción del Sistema
El dominio principal modela el funcionamiento interno de una tienda de videojuegos, enfocándose en un inventario rico y un sistema de ventas. El sistema permite:
- Registrar videojuegos con sus requisitos técnicos y desarrolladora.
- Registrar clientes.
- Emitir pedidos de compra.
- Consultar el valor total del inventario y filtrar juegos por género.

## 🏗️ Decisiones de Diseño por Paradigma

A continuación se detalla cómo se integró cada paradigma sobre el dominio base:

### 1. Programación Orientada a Objetos (POO) - *La Base*
El sistema se construyó con un fuerte enfoque en POO para modelar las entidades del mundo real.
- **Abstracción e Interfaces:** Uso de la interfaz `IEntidad` para unificar clases y `Producto` como clase abstracta.
- **Herencia y Polimorfismo:** Las clases `Videojuego` y `Consola` heredan de `Producto`. El método `ObtenerResumen()` está sobreescrito (override) en las clases derivadas, lo que permite invocar el comportamiento polimórfico correcto dependiendo del tipo de objeto en tiempo de ejecución.
- **Asociación:** La clase `Pedido` demuestra una relación asociativa con `Cliente` (`ClienteAsociado`).

### 2. Paradigma Orientado a Aspectos (AOP)
Se utilizó **Castle Windsor** para inyectar interceptores sin modificar la lógica de negocio.
- **LoggingInterceptor:** Intercepta la entrada y salida de los métodos, registrando las acciones en consola para fines de auditoría.
- **ErrorHandlingInterceptor:** Intercepta los métodos de acceso a la capa de persistencia (archivos CSV). Captura y maneja centralizadamente cualquier error de I/O, previniendo que la aplicación colapse.
- **Decisión de Diseño:** Los interceptores se aplican a nivel de la interfaz genérica `IPersistenciaCSV<T>`. En lugar de requerir métodos `virtual`, Castle DynamicProxy genera el proxy utilizando la abstracción de la interfaz, logrando un desacoplamiento perfecto.

### 3. Programación Funcional
Las consultas complejas y la generación de reportes se aislaron en una clase estática de funciones puras llamada `ReportesFuncionales`.
- **Funciones Puras:** Los métodos reciben los datos de entrada y devuelven un nuevo resultado sin mutar el estado original y sin efectos secundarios (ej. `CalcularValorInventario`).
- **Higher-Order Functions:** Uso extensivo de delegados como `Func<T, bool>` y `Action<T>` para inyectar comportamiento dinámico desde el llamador hacia el reporte.
- **LINQ:** Empleo intensivo de `Where`, `Select` y `Aggregate` para manipular colecciones como tubos de datos (pipelines).
- **Inmutabilidad (Records):** Implementación del tipo `RequisitoSistema` utilizando `record` de C#, el cual ofrece inmutabilidad por defecto.

### 4. Programación Orientada a Eventos (Reactividad)
Se diseñó un mecanismo de mensajería interno basado en eventos semánticamente ricos del dominio de negocio.
- **Eventos Significativos:** En lugar de emitir eventos genéricos, se crearon `PedidoCreado` y `VideojuegoCreado` (dentro de `EventosDominio`).
- **Desacoplamiento Reactivo:** Al guardar un pedido o registrar un videojuego, se dispara un evento. Un suscriptor ajeno (`NotificadorEventos`) escucha la notificación de negocio y reacciona, simulando en este caso el envío de un correo de comprobante al cliente o la actualización del catálogo web, respectivamente.

## 🏛️ Aplicación de Principios SOLID
- **S - Single Responsibility Principle (SRP):** La clase `PersistenciaCSV` tiene una única responsabilidad: el manejo de lectura/escritura de los archivos, sin conocer lógica de negocio.
- **O - Open/Closed Principle (OCP):** El uso de interceptores AOP (Logging y Errores) nos permite agregar funcionalidades a la capa de persistencia sin modificar su código.
- **D - Dependency Inversion Principle (DIP):** El código principal (`Program.cs`) solo depende de `IPersistenciaCSV<T>`, y las dependencias (y la configuración del archivo) se resuelven inyectadas mediante el contenedor de DI de Castle Windsor.

## 📂 Entregables Incluidos
- **Proyecto .NET C#:** Código fuente listo para ejecutar usando `dotnet run`.
- **Archivo `.gitignore`:** Excluye correctamente directorios de build como `/bin` y `/obj`.
- **Diagrama UML:** Ubicado en la carpeta `/Docs` en formato `Draw.io`.
- **Este archivo README.md:** Con la justificación teórica y técnica.

---
*Para probar este proyecto, ubícate en la raíz del repositorio usando una terminal, y ejecuta `dotnet run`. Asegúrate de restaurar los paquetes NuGet de antemano si el IDE no lo hace automáticamente (`dotnet restore`).*
