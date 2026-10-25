namespace PLANAZO_POO_CSharp.Modelos;

public sealed class Usuario
{
    public string Nombre { get; }
    private readonly List<Reserva> _reservas = new();
    public IReadOnlyList<Reserva> Reservas => _reservas.AsReadOnly();

    public Usuario(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del usuario no puede estar vacío.");
        Nombre = nombre.Trim();
    }

    internal void AgregarReserva(Reserva reserva) => _reservas.Add(reserva);
}
