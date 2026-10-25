namespace PLANAZO_POO_CSharp.Modelos;

public enum EstadoReserva { Activa, Cancelada }

public sealed class Reserva
{
    public int Id { get; }
    public Usuario Usuario { get; }
    public string NombrePersona => Usuario.Nombre;
    public Evento Evento { get; }
    public int CantidadEntradas { get; }
    public decimal ValorTotal { get; }
    public EstadoReserva Estado { get; private set; }

    public Reserva(int id, Usuario usuario, Evento evento, int cantidadEntradas)
    {
        if (id <= 0) throw new ArgumentException("El identificador de reserva debe ser mayor que cero.");
        if (cantidadEntradas <= 0) throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");

        Id = id;
        Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
        Evento = evento ?? throw new ArgumentNullException(nameof(evento));
        CantidadEntradas = cantidadEntradas;
        ValorTotal = Evento.CalcularPrecioFinal() * cantidadEntradas;
        Estado = EstadoReserva.Activa;
    }

    public void Cancelar()
    {
        if (Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException("Esta reserva ya fue cancelada.");
        Evento.DevolverCupos(CantidadEntradas);
        Estado = EstadoReserva.Cancelada;
    }

    public override string ToString() =>
        $"Reserva #{Id} | Persona: {NombrePersona} | Evento: {Evento.Nombre} | " +
        $"Entradas: {CantidadEntradas} | Total: {ValorTotal:C0} | Estado: {Estado}";
}
