# NexusGaming: Plataforma de eSports y Distribución Digital

Este proyecto es una aplicación de consola en **C# (.NET 8)** que implementa principios de **Programación Orientada a Objetos (POO)** y persistencia de datos mediante archivos **CSV** utilizando la librería `CsvHelper`.

## Descripción del Problema Computacional
La industria moderna de los videojuegos enfrenta una fragmentación operativa: las plataformas que distribuyen y venden los títulos operan de manera independiente a las organizaciones que gestionan las ligas competitivas de eSports. Esto obliga a los **Estudios de Desarrollo** a gestionar sus productos en múltiples lugares y dificulta la centralización de datos respecto a qué títulos están activos en qué competiciones corporativas.

Con el fin de centralizar esta información técnica y comercial, surge la necesidad de **NexusGaming**. Esta es una plataforma back-office que permite a los administradores registrar el catálogo de productos digitales, vincularlos con los estudios responsables, gestionar los motores gráficos y arquitecturas (requisitos) que los componen, y administrar las ligas competitivas asociadas.

## Enunciado del Sistema y Entidades
El sistema provee menús interactivos que permiten realizar operaciones **CRUD completas** (Crear, Listar, Actualizar, Eliminar) sobre el ecosistema. Su arquitectura obedece estrictamente a los siguientes pilares de POO dictados para la entrega:

1. **Herencia (1):** Toda entidad vendible hereda de la clase base abstracta `ProductoDigital`, la cual define el comportamiento de cálculo de descuentos mediante polimorfismo, aplicado a `Videojuego`.
2. **Asociación (1):** Cada `Videojuego` está fuertemente asociado comercialmente a un `EstudioDesarrollo` (organizador y creador).
3. **Composición (1):** A nivel arquitectónico, un `Videojuego` está compuesto de una `EspecificacionTecnica` (Motor Gráfico). La existencia de este requerimiento técnico no tiene sentido si el videojuego es eliminado del catálogo.
4. **Agregación (1):** Las `LigaEsports` gestionan la competición, incorporando y agrupando una lista de objetos de tipo `EquipoCompetitivo`. Si la liga finaliza de operar, los equipos corporativos siguen existiendo como entidades independientes.
5. **Interfaz implementada (1):** Las entidades raíz cuentan con la interfaz `IPersistible` para garantizar su salvado modular en archivos .csv.

## Requisitos Analíticos de Funcionalidad
El sistema responde correctamente generando, listando, modificando y eliminando registros en los siguientes submódulos (mínimo 3 CRUDs operativos):
- Módulo de Videojuegos
- Módulo de Estudios de Desarrollo
- Módulo de Ligas de eSports
