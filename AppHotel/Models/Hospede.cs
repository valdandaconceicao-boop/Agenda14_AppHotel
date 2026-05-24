namespace AppHotel.Models;

/// <summary>
/// Classe que representa o modelo de um hóspede no sistema de reservas do hotel.
/// Encapsula os dados do cliente (Nome, CPF, Email).
/// </summary>
public class Hospede
{
    /// <summary>
    /// Nome completo do hóspede.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Cadastro de Pessoas Físicas (CPF) do hóspede, usado para fins fiscais e cadastrais.
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Endereço de e-mail do hóspede para envio de confirmações e comunicações.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}