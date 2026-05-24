using AppHotel.Models;

namespace AppHotel;

/// <summary>
/// Código-behind (lógica da interface) da página principal (MainPage).
/// Gerencia a entrada de dados do formulário de reserva, realiza validações básicas,
/// instancia os modelos e exibe o resumo financeiro da simulação.
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Construtor da página. Inicializa os componentes do XAML e define
    /// a data mínima de check-in e check-out como o dia de hoje, evitando datas retroativas.
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
        
        // Define a data mínima aceitável como a data atual do sistema.
        dpCheckIn.MinimumDate = DateTime.Today;
        dpCheckOut.MinimumDate = DateTime.Today;
    }

    /// <summary>
    /// Evento disparado quando o usuário clica no botão "Calcular Reserva".
    /// Valida as datas, o tipo de suíte selecionada e a quantidade de hóspedes.
    /// Em seguida, instancia as classes de modelo necessárias para calcular o total e exibe o resumo na tela.
    /// </summary>
    private void BtnCalcular_Clicked(object? sender, EventArgs e)
    {
        // 1. Coleta os valores selecionados nos DatePickers
        DateTime checkIn = dpCheckIn.Date;
        DateTime checkOut = dpCheckOut.Date;

        // Validação: Check-out deve ser posterior ao Check-in
        if (checkOut <= checkIn)
        {
            DisplayAlert("Erro", "A data de saída deve ser maior que a data de entrada.", "OK");
            return;
        }

        // Validação: Deve ter uma suíte selecionada no Picker
        if (pkSuite.SelectedIndex < 0)
        {
            DisplayAlert("Erro", "Selecione um tipo de suíte.", "OK");
            return;
        }

        // Validação: Quantidade de hóspedes deve ser um número inteiro válido e positivo
        int qtdHospedes;
        if (!int.TryParse(txtHospedes.Text, out qtdHospedes) || qtdHospedes <= 0)
        {
            DisplayAlert("Erro", "Informe a quantidade de hóspedes.", "OK");
            return;
        }

        // 2. Determina o preço unitário da diária de acordo com o índice selecionado no Picker.
        // Ponto de melhoria técnica: Estes valores de preço e capacidade estão "chumbados" (hardcoded).
        decimal valorDiaria = pkSuite.SelectedIndex switch
        {
            0 => 120.00m, // Standard
            1 => 220.00m, // Luxo
            2 => 350.00m, // Premium
            _ => 0
        };

        // 3. Instancia e preenche o modelo Suite
        Suite suite = new Suite
        {
            Tipo = pkSuite.SelectedItem?.ToString() ?? "",
            Capacidade = 4, // Capacidade fixa em 4 neste ponto
            ValorDiaria = valorDiaria
        };

        // 4. Instancia o modelo Reserva associando a suíte e definindo um hóspede padrão provisório
        Reserva reserva = new Reserva
        {
            Suite = suite,
            CheckIn = checkIn,
            CheckOut = checkOut,
            Hospede = new Hospede { Nome = "Convidado" }
        };

        // 5. Atualiza os componentes da interface visual com o resumo da reserva calculada
        lblTipoSuite.Text = $"Suite: {suite.Tipo}";
        lblPeriodo.Text = $"Período: {checkIn:dd/MM/yyyy} a {checkOut:dd/MM/yyyy}";
        lblDiarias.Text = $"Diárias: {reserva.DiasReservados} noite(s)";
        lblHospedes.Text = $"Hóspedes: {qtdHospedes}";
        
        // Formata a exibição do valor total no formato de moeda local (:C)
        lblTotal.Text = $"Valor Total: {reserva.CalcularTotal():C}";

        // Torna a seção (Border) de resumo visível para o usuário
        frameResultado.IsVisible = true;
    }

    /// <summary>
    /// Evento disparado ao clicar no botão "Avançar".
    /// Realiza a navegação assíncrona inserindo a tela de Sucesso (SucessoPage) na pilha de navegação.
    /// </summary>
    private async void BtnAvancar_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new SucessoPage());
    }
}