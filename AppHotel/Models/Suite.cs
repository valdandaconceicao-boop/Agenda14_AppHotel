namespace AppHotel.Models;

/// <summary>
/// Classe que representa o modelo de uma suíte de hotel.
/// Encapsula a suíte (Tipo, Capacidade, ValorDiaria).
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