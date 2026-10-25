
using PLANAZO_POO_CSharp.Modelos;
using PLANAZO_POO_CSharp.Servicios;

namespace PLANAZO_POO_CSharp;

internal class Program
{
    private static readonly GestorReservas gestor = new();

    private static void Main()
    {
        CargarEventosIniciales();
        bool continuar = true;

        while (continuar)
        {
            MostrarMenu();

            Console.Write("Seleccione una opción: ");
            string? opcion = Console.ReadLine();

            Console.WriteLine();

            try
            {
                switch (opcion)
                {
                    case "1":
                        VerEventosDisponibles();
                        break;

                    case "2":
                        ConsultarEvento();
                        break;

                    case "3":
                        ReservarEntradas();
                        break;

                    case "4":
                        VerReservas();
                        break;

                    case "5":
                        CancelarReserva();
                        break;

                    case "6":
                        MostrarResumen();
                        break;

                    case "7":
                        ConsultarReservasUsuario();
                        break;

                    case "0":
                        continuar = false;
                        Console.WriteLine("Gracias por usar PLANAZO.");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private static void MostrarMenu()
    {
        Console.WriteLine("===============================");
        Console.WriteLine("       PLANAZO - EVENTOS");
        Console.WriteLine("===============================");
        Console.WriteLine("1. Ver eventos disponibles");
        Console.WriteLine("2. Consultar evento");
        Console.WriteLine("3. Reservar entradas");
        Console.WriteLine("4. Ver reservas realizadas");
        Console.WriteLine("5. Cancelar una reserva");
        Console.WriteLine("6. Ver resumen de reservas");
        Console.WriteLine("7. Consultar reservas por usuario (reto)");
        Console.WriteLine("0. Salir");
        Console.WriteLine();
    }

    private static void CargarEventosIniciales()
    {
        gestor.AgregarEvento(new Cine(1, "Interestelar",new DateTime(2026, 11, 5), 18000m, 40));

        gestor.AgregarEvento(new Cine(2, "Coco",new DateTime(2026, 11, 8), 15000m, 35));

        gestor.AgregarEvento(new Concierto(3, "Noche de Rock",new DateTime(2026, 11, 15), 50000m, 100));

        gestor.AgregarEvento(new Concierto(4, "Festival de Pop",new DateTime(2026, 12, 2), 70000m, 150));

        gestor.AgregarEvento(new Taller(5, "Fotografía básica",new DateTime(2026, 11, 12), 40000m, 20));

        gestor.AgregarEvento(new Taller(6, "Pintura creativa",new DateTime(2026, 11, 20), 35000m, 18));

        gestor.AgregarEvento(new EventoDeportivo(7, "Torneo de fútbol",new DateTime(2026, 12, 10), 30000m, 80));
    }

    private static void VerEventosDisponibles()
    {
        Console.WriteLine("EVENTOS DISPONIBLES");

        foreach (Evento evento in gestor.ObtenerEventos())
        {
            Console.WriteLine(evento);
        }
    }

    private static void ConsultarEvento()
    {
        int id = LeerEntero("Ingrese el identificador del evento: ");

        Evento evento = gestor.BuscarEvento(id);

        Console.WriteLine(evento);

        Console.WriteLine($"Precio final por entrada: {evento.CalcularPrecioFinal():C0}");
    }

    private static void ReservarEntradas()
    {
        int idEvento = LeerEntero("Identificador del evento: ");

        Evento evento = gestor.BuscarEvento(idEvento);

        Console.WriteLine($"Evento: {evento.Nombre} | " +$"Precio por entrada: {evento.CalcularPrecioFinal():C0}");

        Console.Write("Nombre de la persona: ");
        string nombre = Console.ReadLine() ?? "";

        int cantidad = LeerEntero("Cantidad de entradas: ");

        Reserva reserva = gestor.CrearReserva(nombre, evento.Id, cantidad);

        Console.WriteLine($"Reserva creada correctamente. Código: {reserva.Id}");

        Console.WriteLine($"Total: {reserva.ValorTotal:C0}");
    }

    private static void VerReservas()
    {
        IReadOnlyList<Reserva> reservas = gestor.ObtenerReservas();

        if (reservas.Count == 0)
        {
            Console.WriteLine("Todavía no hay reservas.");
            return;
        }

        foreach (Reserva reserva in reservas)
        {
            Console.WriteLine(reserva);
        }
    }

    private static void CancelarReserva()
    {
        int id = LeerEntero("Identificador de la reserva que desea cancelar: ");

        gestor.CancelarReserva(id);

        Console.WriteLine("Reserva cancelada. Los cupos fueron devueltos al evento.");
    }

    private static void MostrarResumen()
    {
        ResumenReservas resumen = gestor.ObtenerResumen();

        Console.WriteLine("RESUMEN DE RESERVAS ACTIVAS");
        Console.WriteLine($"Reservas activas: {resumen.CantidadReservas}");

        Console.WriteLine($"Entradas reservadas: {resumen.CantidadEntradas}");

        Console.WriteLine($"Valor total: {resumen.ValorTotal:C0}");
    }

    private static void ConsultarReservasUsuario()
    {
        Console.Write("Nombre de la persona: ");
        string nombre = Console.ReadLine() ?? "";

        var reservas = gestor.ObtenerReservasPorUsuario(nombre);

        if (reservas.Count == 0)
        {
            Console.WriteLine("No hay reservas activas para esa persona.");
            return;
        }

        foreach (Reserva reserva in reservas)
        {
            Console.WriteLine(reserva);
        }
    }

    private static int LeerEntero(string mensaje)
    {
        Console.Write(mensaje);

        if (!int.TryParse(Console.ReadLine(), out int valor))
        {
            throw new ArgumentException("Debe ingresar un número entero válido.");
        }
        return valor;
    }
}
