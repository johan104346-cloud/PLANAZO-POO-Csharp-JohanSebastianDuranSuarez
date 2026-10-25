# PLANAZO - Sistema de gestión de eventos y reservas

Estudiante: Johan Sebastian Duran Suarez  
Lenguaje: C#  
Plataforma: .NET  
Tipo de aplicación: Consola  

## 1. Descripción del proyecto

PLANAZO es una aplicación de consola desarrollada en C# que permite gestionar eventos y realizar reservas de entradas. El sistema permite consultar eventos, verificar la disponibilidad de cupos, registrar reservas, cancelar reservas y consultar un resumen de las operaciones realizadas.

El proyecto utiliza Programación Orientada a Objetos para organizar el código mediante clases, abstracción, encapsulamiento, herencia y polimorfismo.

## 2. Objetivo

Desarrollar una aplicación que facilite la administración de eventos y reservas, garantizando el control de los cupos disponibles, el cálculo correcto de los precios y la validación de las operaciones realizadas por los usuarios.

## 3. Funcionalidades principales

- Listar los eventos disponibles.
- Consultar la información de un evento.
- Realizar reservas de entradas.
- Validar la cantidad de entradas solicitadas.
- Controlar los cupos disponibles de cada evento.
- Calcular el precio de las entradas según el tipo de evento.
- Consultar todas las reservas registradas.
- Consultar las reservas activas de una persona.
- Cancelar reservas y liberar los cupos correspondientes.
- Evitar la cancelación duplicada de una reserva.
- Mostrar un resumen de reservas activas, entradas reservadas y valor total.
- Manejar errores y operaciones no permitidas.

## 4. Tipos de eventos

| Tipo de evento | Regla de precio |
|---|---|
| Cine | Conserva el precio base. |
| Concierto | Aumenta el precio base un 15 %. |
| Taller | Aplica un descuento del 10 %. |
| Evento deportivo | Aumenta el precio base un 5 %. |

La aplicación inicia con siete eventos de ejemplo: dos películas, dos conciertos, dos talleres y un evento deportivo.

## 5. Estructura del proyecto

PLANAZO-POO-CSharp/
│
├── Modelos/
│   ├── Evento.cs
│   ├── Cine.cs
│   ├── Concierto.cs
│   ├── Taller.cs
│   ├── EventoDeportivo.cs
│   ├── Usuario.cs
│   └── Reserva.cs
│
├── Servicios/
│   └── GestorReservas.cs
│
├── Capturas/
│
├── Program.cs
├── PLANAZO-POO-CSharp.csproj
├── README.md
└── .gitignore
```

## 6. Descripción de las clases

### Evento

Es una clase abstracta que contiene las características comunes de los eventos: identificador, nombre, fecha, precio base, capacidad máxima y cupos disponibles. También define el método `CalcularPrecioFinal()` y controla la reserva y devolución de cupos.

### Cine

Hereda de `Evento` y calcula el precio final conservando el precio base.

### Concierto

Hereda de `Evento` y calcula el precio final aplicando un incremento del 15 %.

### Taller

Hereda de `Evento` y calcula el precio final aplicando un descuento del 10 %.

### EventoDeportivo

Hereda de `Evento` y representa un tipo adicional de evento. Aplica un incremento del 5 % sobre el precio base.

### Usuario

Representa a la persona que realiza las reservas. Almacena su nombre y mantiene una colección de las reservas asociadas.

### Reserva

Representa una reserva de entradas. Relaciona un usuario con un evento y almacena el identificador de reserva, la cantidad de entradas, el valor total y el estado. También permite cancelar una reserva y devolver los cupos correspondientes.

### GestorReservas

Administra los eventos y las reservas. Permite buscar eventos, registrar reservas, cancelar operaciones, consultar las reservas de una persona y calcular el resumen general.

### Program.cs

Contiene el menú principal y permite al usuario interactuar con las funcionalidades del sistema desde la consola.

## 7. Aplicación de la Programación Orientada a Objetos

### Abstracción

Se utiliza la clase abstracta `Evento` para representar las características y operaciones comunes de los eventos sin crear directamente objetos de esa clase.

### Herencia

Las clases `Cine`, `Concierto`, `Taller` y `EventoDeportivo` heredan de `Evento`, reutilizando sus propiedades y métodos.

### Polimorfismo

Cada tipo de evento implementa el método `CalcularPrecioFinal()` con una regla diferente para determinar el precio de las entradas.

### Encapsulamiento

Las propiedades utilizan modificadores de acceso, como `private set`, para proteger los datos y controlar sus modificaciones mediante métodos.

### Colecciones y asociaciones

Se utiliza `List<T>` para almacenar eventos y reservas. La clase `Reserva` se relaciona con un evento y un usuario, mientras que `GestorReservas` administra las operaciones del sistema.

### Excepciones

Se utilizan excepciones para controlar operaciones incorrectas, como consultar un evento inexistente, reservar más entradas de las disponibles o cancelar dos veces una misma reserva.

## 8. ¿Por qué se utiliza una clase abstracta `Evento`?

La clase `Evento` es abstracta porque define las características y comportamientos comunes de los eventos, pero no representa por sí misma un tipo específico. Las clases `Cine`, `Concierto` y `Taller` heredan sus propiedades y proporcionan su propia implementación del método `CalcularPrecioFinal()`.

Por ejemplo, la clase `Cine` conserva el precio base, `Concierto` aplica un incremento del 15 % y `Taller` aplica un descuento del 10 %.

Esta decisión evita duplicar código y facilita la incorporación de nuevos tipos de eventos. Para agregar un nuevo tipo, basta con crear una clase que herede de `Evento` e implemente el método de cálculo correspondiente.

## 9. Validaciones implementadas

- El identificador del evento debe ser válido y no puede repetirse.
- El nombre del evento y de la persona es obligatorio.
- El precio base no puede ser negativo.
- La capacidad máxima debe ser mayor que cero.
- La cantidad de entradas debe ser mayor que cero.
- No se permite reservar más entradas que los cupos disponibles.
- Al confirmar una reserva, los cupos disminuyen.
- Al cancelar una reserva, los cupos se devuelven.
- No se permite cancelar dos veces la misma reserva.
- Se muestran mensajes de error cuando una operación no puede realizarse.

## 10. Tecnologías utilizadas

- C#
- .NET 8
- Visual Studio Code
- Git
- GitHub

## 11. Instrucciones de ejecución

### Requisitos

Tener instalado el SDK de .NET 8.

### Compilar el proyecto

Desde una terminal ubicada en la carpeta principal del proyecto, ejecutar:

```bash
dotnet restore
dotnet build
```

### Ejecutar la aplicación

```bash
dotnet run
```

## 12. Casos de prueba

1. Registrar una reserva válida.
2. Intentar reservar más entradas que los cupos disponibles.
3. Intentar realizar una reserva con una cantidad negativa.
4. Cancelar una reserva y comprobar que se devuelven los cupos.
5. Intentar cancelar dos veces la misma reserva.
6. Consultar un evento inexistente.
7. Comprobar que cada tipo de evento calcula correctamente su precio.



## 13. Diagrama UML


                 PLANAZO - EVENTOS
                  DIAGRAMA UML
                         |
                         v
        +----------------------------------+
        |       <<abstract>>               |
        |            EVENTO                |
        +----------------------------------+
        | - Id: int                        |
        | - Nombre: string                 |
        | - Fecha: DateTime                |
        | - PrecioBase: decimal             |
        | - CapacidadMaxima: int            |
        | - CuposDisponibles: int            |
        +----------------------------------+
        | + CalcularPrecioFinal()           |
        | + ReservarCupos(cantidad)         |
        | + DevolverCupos(cantidad)         |
        +----------------------------------+
                         ^
                         |
          +--------------+--------------+
          |              |              |
          |              |              |
    +-----------+  +-------------+  +-----------+  +------------------+
    |   CINE    |  |  CONCIERTO  |  |   TALLER  |  | EVENTO DEPORTIVO  |
    +-----------+  +-------------+  +-----------+  +------------------+
    | Calcular  |  | Calcular    |  | Calcular  |  | Calcular         |
    | Precio()  |  | Precio()    |  | Precio()  |  | Precio()         |
    +-----------+  +-------------+  +-----------+  +------------------+
          |              |              |                 |
          +--------------+--------------+-----------------+
                         |
                         v
        +----------------------------------+
        |          GESTOR RESERVAS         |
        +----------------------------------+
        | - eventos: List<Evento>           |
        | - reservas: List<Reserva>         |
        | - usuarios: Dictionary            |
        +----------------------------------+
        | + AgregarEvento()                 |
        | + BuscarEvento()                  |
        | + CrearReserva()                  |
        | + CancelarReserva()               |
        | + ObtenerEventos()                |
        | + ObtenerReservas()               |
        | + ObtenerReservasPorUsuario()      |
        | + ObtenerResumen()                |
        +----------------------------------+
                    |             |
             gestiona             registra
                    |             |
                    v             v
        +----------------+  +----------------+
        |    RESERVA     |  |     USUARIO    |
        +----------------+  +----------------+
        | - Id: int      |  | - Nombre       |
        | - Usuario      |  | - Reservas     |
        | - Evento       |  +----------------+
        | - Cantidad     |          |
        | - ValorTotal   |          | tiene
        | - Activa       |          |
        +----------------+          |
        | + Cancelar()   |<---------+
        +----------------+
                 |
                 | genera resumen
                 v
        +----------------------------------+
        |         RESUMEN RESERVAS         |
        +----------------------------------+
        | - CantidadReservas: int           |
        | - CantidadEntradas: int           |
        | - ValorTotal: decimal             |
        +----------------------------------+


## 14. Conclusión

El desarrollo de PLANAZO permitió aplicar los conceptos de Programación Orientada a Objetos en C#, organizando el código en clases independientes y construyendo un sistema funcional de gestión de eventos y reservas.

Además, se implementaron validaciones para controlar los cupos, calcular los precios y gestionar correctamente las cancelaciones. La organización del proyecto facilita su comprensión, mantenimiento y ampliación.


