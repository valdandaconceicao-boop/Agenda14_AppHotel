namespace AppHotel.Models;

/// <summary>
/// Classe principal que gerencia o modelo de uma reserva.
/// Consolida as informações da suíte escolhida, o hóspede responsável,
/// as datas de estadia e realiza os cálculos financeiros associados.
/// </summary>
public class Reserva
{
    /// <summary>
    /// Instância do tipo de suíte vinculada a esta reserva.
    /// </summary>
    public Suite Suite { get; set; } = new();

    /// <summary>
    /// Instância do hóspede titular desta reserva.
    /// </summary>
    public Hospede Hospede { get; set; } = new();

    /// <summary>
    /// Data e hora de início da estadia (entrada/check-in).
    /// </summary>
    public DateTime CheckIn { get; set; }

    /// <summary>
    /// Data e hora de encerramento da estadia (saída/check-out).
    /// </summary>
    public DateTime CheckOut { get; set; }

    /// <summary>
    /// Propriedade calculada (apenas leitura) que retorna o total de dias/noites reservados.
    /// Faz a subtração entre as datas de check-out e check-in gerando um TimeSpan,
    /// do qual extrai a propriedade Days.
    /// </summary>
    public int DiasReservados
    {
        get
        {
            TimeSpan diferenca = CheckOut - CheckIn;
            return diferenca.Days;
        }
    }

    /// <summary>
    /// Calcula o valor total da reserva com base na quantidade de dias reservados
    /// e no valor unitário da diária da suíte vinculada.
    /// Também aplica uma regra de negócio importante: se a estadia for de 10 dias ou mais,
    /// o hóspede recebe um desconto de 10% sobre o total da simulação.
    /// </summary>
    /// <returns>O valor decimal correspondente ao total da reserva (com desconto se aplicável).</returns>
    public decimal CalcularTotal()
    {
        // Cálculo base do valor total: multiplicação direta dos dias pelo preço unitário da diária
        decimal total = DiasReservados * Suite.ValorDiaria;

        // Regra de negócio: Se o período de hospedagem for igual ou superior a 10 dias,
        // aplica-se um desconto de 10% sobre o valor total da reserva.
        if (DiasReservados >= 10)
        {
            // Subtrai 10% do valor total calculado anteriormente
            total -= total * 0.10m;
        }

        return total;
    }
}