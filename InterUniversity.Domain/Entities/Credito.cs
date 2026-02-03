namespace InterUniversity.Domain.Entities;

public partial class Credito
{
    public int CreditoId { get; set; }

    public byte Creditos { get; set; }

    public string Descripcion { get; set; } = null!;
}
