# Sistema de Gestión de Videojuegos y eSports

Este proyecto es una aplicación de consola en **C# (.NET 8)** que implementa principios de **Programación Orientada a Objetos (POO)** y persistencia de datos con archivos **CSV** utilizando la librería `CsvHelper`.

## Descripción del Problema
La industria de los deportes electrónicos (eSports) y el desarrollo de videojuegos crece a un ritmo acelerado a nivel mundial. Sin embargo, muchas organizaciones, equipos competitivos y plataformas administradoras carecen de herramientas centralizadas para gestionar de forma eficiente la información de su ecosistema. Especialmente, es complejo mantener de manera consolidada el catálogo de videojuegos con sus distintos requisitos técnicos, los estudios o desarrolladores detrás de estos títulos, y los perfiles de los jugadores profesionales o casuales que participan. 

Actualmente, estos datos suelen estar dispersos o no cuentan con un diseño estructurado bajo buenas prácticas de ingeniería de software. Esta falta de organización genera redundancia de información e incapacidad de escalar a nuevos modelos de negocio, dificultando su administración mediante operaciones básicas.

## Enunciado del Sistema
Para resolver la problemática descrita, se solicita el desarrollo de un **Sistema de Gestión de Videojuegos**. Se construirá una aplicación de consola en C# implementando los pilares de la Programación Orientada a Objetos (POO). El sistema actuará como un repositorio central de información donde el usuario podrá administrar el ecosistema a través de menús interactivos, ejecutando las operaciones **CRUD** (Crear, Leer, Actualizar y Eliminar) sobre tres entidades fundamentales:

1. **Videojuegos**: Registro de los títulos disponibles, su género y su precio. Internamente administra los requisitos mínimos de hardware que requieren para funcionar de manera indispensable (**Composición**). Cada juego está ligado de forma obligatoria al estudio responsable de su creación (**Asociación**).
2. **Desarrolladores**: Catálogo de los estudios o empresas creadoras de software de la industria, registrando su nombre comercial y el país de origen donde operan.
3. **Jugadores**: Repositorio de usuarios o atletas de eSports, manteniendo su nickname competitivo y nivel actual. Adicionalmente, el sistema simula la creación y gestión de Equipos Competitivos, a los cuales estos jugadores pueden pertenecer (**Agregación**).

---

## Requisitos POO Obligatorios
El sistema implementa los siguientes requisitos exigidos por el entregable:
1. **Herencia (1):** La clase `Videojuego` hereda de la clase base abstracta `Producto`.
2. **Asociación (1):** La clase `Videojuego` se asocia con la clase `Desarrollador` mediante un identificador (`DesarrolladorId`).
3. **Agregación (1):** La clase `EquipoEsports` contiene una lista de `Jugador` (los jugadores existen independientemente del equipo, es decir, si el equipo se disuelve, el jugador sigue existiendo en el sistema).
4. **Composición (1):** La clase `Videojuego` instancia y destruye internamente la clase `RequisitosSistema`. Un requisito de hardware mínimo (OS, RAM) no puede vivir si no le pertenece a un videojuego registrado.
5. **Interfaz implementada (1):** Todas las entidades persistidas a nivel de archivo incluyen la interfaz `IPersistible` para garantizar que expongan características vitales para su guardado en base de datos.
