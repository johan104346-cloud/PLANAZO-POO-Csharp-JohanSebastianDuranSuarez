using PLANAZO_POO_CSharp.Modelos;

namespace PLANAZO_POO_CSharp.Servicios;

public sealed class GestorReservas
{
    private readonly List<Evento> _eventos = new();
    private readonly List<Reserva> _reservas = new();
    private readonly Dictionary<string, Usuario> _usuarios = new(StringComparer.OrdinalIgnoreCase);
    private int _siguienteIdReserva = 1;

    public void AgregarEvento(Evento evento)
    {
        if (_eventos.Any(e => e.Id == evento.Id))
            throw new InvalidOperationException($"Ya existe un evento con ID {evento.Id}.");
        _eventos.Add(evento);
    }

    public IReadOnlyList<Evento> ObtenerEventos() => _eventos.AsReadOnly();
    public IReadOnlyList<Reserva> ObtenerReservas() => _reservas.AsReadOnly();

    public Evento BuscarEvento(int id) =>
        _eventos.FirstOrDefault(e => e.Id == id)
        ?? throw new KeyNotFoundException($"No existe el evento {id}.");

    public Reserva CrearReserva(string nombrePersona, int idEvento, int cantidad)
    {
        if (cantidad <= 0) throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");
        if (string.IsNullOrWhiteSpace(nombrePersona)) throw new ArgumentException("El nombre es obligatorio.");

        Evento evento = BuscarEvento(idEvento);
        // Primero se valida y descuentan los cupos; si algo falla, no se crea una reserva parcial.
        evento.ReservarCupos(cantidad);

        try
        {
            string clave = nombrePersona.Trim();
            if (!_usuarios.TryGetValue(clave, out Usuario? usuario))
            {
                usuario = new Usuario(clave);
                _usuarios.Add(clave, usuario);
            }

            var reserva = new Reserva(_siguienteIdReserva++, usuario, evento, cantidad);
            _reservas.Add(reserva);
            usuario.AgregarReserva(reserva);
            return reserva;
        }
        catch
        {
            evento.DevolverCupos(cantidad);
            throw;
        }
    }

    public void CancelarReserva(int id)
    {
        Reserva reserva = _reservas.FirstOrDefault(r => r.Id == id)
            ?? throw new KeyNotFoundException($"No existe una reserva con identificador {id}.");
        reserva.Cancelar();
    }

    public IReadOnlyList<Reserva> ObtenerReservasPorUsuario(string nombre) =>
        _reservas.Where(r => r.Estado == EstadoReserva.Activa &&
            r.NombrePersona.Equals(nombre?.Trim(), StringComparison.OrdinalIgnoreCase)).ToList().AsReadOnly();

    public ResumenReservas ObtenerResumen()
    {
        var activas = _reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();
        return new ResumenReservas(
            activas.Count,
            activas.Sum(r => r.CantidadEntradas),
            activas.Sum(r => r.ValorTotal));
    }
}

public sealed record ResumenReservas(int CantidadReservas, int CantidadEntradas, decimal ValorTotal);
