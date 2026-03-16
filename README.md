# Sistema de Gestión de Tienda de Videojuegos 

Bienvenido al repositorio del Taller 1 de Programación Orientada a Objetos. Este proyecto implementa un sistema de gestión de backend por consola para la tienda de videojuegos.

## Enunciado del Sistema

La tienda requiere automatizar sus procesos diarios referentes a tres pilares fundamentales:
1. **Gestión de Inventario (Videojuegos y Consolas)**: Monitorear el catálogo de productos disponibles para la venta.
2. **Desarrolladoras**: Mantener un vínculo claro entre qué estudio desarrolló cada videojuego para gestionar pagos por licenciamiento.
3. **Clientes**: Administrar la información de los compradores recurrentes para fidelizarlos en futuros lanzamientos.

Para lograr esto de manera eficiente y escalable, el sistema se ha construido utilizando los principios de **Programación Orientada a Objetos (POO)** en C# .NET. 

Se han implementado las siguientes características clave:
* **Herencia y Polimorfismo**: Productos base con derivaciones a Videojuego y Consola.
* **Asociación**: Cada Videojuego se asocia estructuralmente a una *Desarrolladora* creadora.
* **Componentes Exclusivos (Composición)**: Cada juego administra sus propios _Requisitos de Sistema_ que no pueden existir aislados.
* **Catálogo Global (Agregación)**: La *Tienda* engloba comercialmente la lista completa del stock, pero dichos juegos existen independientemente de esta sucursal.

## Funcionalidad
El proyecto es una aplicación de consola en **C#** que implementa **3 CRUDs completos** (Crear, Leer, Actualizar y Eliminar) respaldados mediante lectura y escritura de archivos **CSV** utilizando `CsvHelper`, cumpliendo con el estándar de una interfaz genérica `IEntidad` para modularidad.

**Módulos de Gestión**:
1. Videojuegos / Catálogo.
2. Empresas Desarrolladoras.
3. Clientes.
 
