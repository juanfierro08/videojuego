# Sistema de Gestión de Videojuegos

Este proyecto es una aplicación de consola en **C# (.NET 8)** que implementa principios de **Programación Orientada a Objetos (POO)** y persistencia de datos con archivos **CSV** utilizando la librería `CsvHelper`.

## Enunciado del Sistema
El sistema permite administrar la información de **Videojuegos, Desarrolladores y Jugadores**. Cuenta con un menú interactivo en consola mediante el cual se pueden crear, listar, actualizar y eliminar registros para las tres entidades.

## Requisitos POO Obligatorios
Implementa los siguientes requisitos exigidos:
1. **Herencia (1):** `Videojuego` hereda de la clase base abstracta `Producto`.
2. **Asociación (1):** `Videojuego` se asocia con `Desarrollador` mediante `DesarrolladorId`.
3. **Agregación (1):** `EquipoEsports` contiene una lista de `Jugador` (los jugadores existen independientemente del equipo).
4. **Composición (1):** `Videojuego` instancia internamente la clase `RequisitosSistema`.
5. **Interfaz implementada (1):** Todas las entidades persistentes implementan la interfaz `IPersistible`.

## Requisitos Funcionales (CRUD)
Se implementó un servicio genérico que satisface al menos **3 CRUDs completos** para:
- **Videojuego** (Create, Read, Update, Delete)
- **Desarrollador** (Create, Read, Update, Delete)
- **Jugador** (Create, Read, Update, Delete)
