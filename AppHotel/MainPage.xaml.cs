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

        // 2. Determina o preço unitário da diária e a capacidade máxima de acordo com a suíte selecionada.
        // Ponto de melhoria técnica implementado: Agora a capacidade é dinâmica e validada contra os hóspedes.
        decimal valorDiaria = pkSuite.SelectedIndex switch
        {
            0 => 120.00m, // Standard
            1 => 220.00m, // Luxo
            2 => 350.00m, // Premium
            _ => 0
        };

        int capacidade = pkSuite.SelectedIndex switch
        {
            0 => 2, // Standard comporta no máximo 2 pessoas
            1 => 4, // Luxo comporta no máximo 4 pessoas
            2 => 6, // Premium comporta no máximo 6 pessoas
            _ => 0
        };

        // Validação: Quantidade de hóspedes não pode exceder a capacidade permitida pela suíte
        if (qtdHospedes > capacidade)
        {
            string nomeSuite = pkSuite.SelectedItem?.ToString()?.Split('-')[0].Trim() ?? "Selecionada";
            DisplayAlert("Capacidade Excedida", $"A suíte {nomeSuite} comporta no máximo {capacidade} hóspedes. Você informou {qtdHospedes} hóspedes.", "OK");
            return;
        }

        // 3. Instancia e preenche o modelo Suite
        Suite suite = new Suite
        {
            Tipo = pkSuite.SelectedItem?.ToString() ?? "",
            Capacidade = capacidade,
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
        lblTipoSuite.Text = $"Suite: {suite.Tipo.Split('-')[0].Trim()} (Capacidade: {suite.Capacidade} pessoas)";
        lblPeriodo.Text = $"Período: {checkIn:dd/MM/yyyy} a {checkOut:dd/MM/yyyy}";
        lblDiarias.Text = $"Diárias: {reserva.DiasReservados} noite(s)";
        lblHospedes.Text = $"Hóspedes: {qtdHospedes}";
        
        // Exibe o total formatado e indica se o desconto de 10% para estadias longas (>= 10 dias) foi aplicado
        decimal total = reserva.CalcularTotal();
        if (reserva.DiasReservados >= 10)
        {
            lblTotal.Text = $"Valor Total: {total:C}\n(10% de Desconto Aplicado!)";
        }
        else
        {
            lblTotal.Text = $"Valor Total: {total:C}";
        }

        // Torna a seção (Border) de resumo visível para o usuário
        frameResultado.IsVisible = true;
    }

    /// <summary>
    /// Evento disparado quando a data do check-in é alterada.
    /// Ajusta dinamicamente a data mínima permitida para o check-out, evitando datas inválidas na interface.
    /// </summary>
    private void DpCheckIn_DateSelected(object? sender, DateChangedEventArgs e)
    {
        // A data mínima de saída deve ser pelo menos 1 dia após a data de entrada selecionada.
        dpCheckOut.MinimumDate = e.NewDate.AddDays(1);

        // Se a data atual de check-out for inferior ou igual à nova data de check-in,
        // ajustamos automaticamente a data de check-out para o dia seguinte do check-in.
        if (dpCheckOut.Date <= e.NewDate)
        {
            dpCheckOut.Date = e.NewDate.AddDays(1);
        }
    }

    /// <summary>
    /// Evento disparado ao clicar no botão "Avançar".
    /// Realiza a navegação segura utilizando as rotas do .NET MAUI Shell (GoToAsync).
    /// </summary>
    private async void BtnAvancar_Clicked(object? sender, EventArgs e)
    {
        // Navega de forma segura para a tela de Sucesso utilizando o nome da classe registrado como rota global no AppShell.
        await Shell.Current.GoToAsync(nameof(SucessoPage));
    }
}