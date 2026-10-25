namespace PLANAZO_POO_CSharp.Modelos;

// Reto adicional: aplica un recargo del 5 % por logística deportiva.
public sealed class EventoDeportivo : Evento
{
    public EventoDeportivo(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima) { }

    public override decimal CalcularPrecioFinal() => PrecioBase * 1.05m;
}
