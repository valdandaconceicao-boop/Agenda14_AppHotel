namespace AppHotel.Models;

/// <summary>
/// Classe que representa o modelo de uma suíte de hotel.
/// Define as características do quarto disponível para reserva, incluindo capacidade máxima e preço da diária.
/// </summary>
public class Suite
{
    /// <summary>
    /// Tipo ou categoria da suíte (Ex: Standard, Luxo, Premium).
    /// </summary>
    public string Tipo { get; set; } = string.Empty;

    /// <summary>
    /// Capacidade máxima de hóspedes permitida nesta suíte.
    /// </summary>
    public int Capacidade { get; set; }

    /// <summary>
    /// Valor cobrado por cada dia (diária) de permanência na suíte.
    /// </summary>
    public decimal ValorDiaria { get; set; }
}