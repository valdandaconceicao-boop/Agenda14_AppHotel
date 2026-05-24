namespace AppHotel;

/// <summary>
/// Código-behind da página de sucesso (SucessoPage).
/// Responsável por exibir a confirmação final da reserva e retornar à tela inicial.
/// </summary>
public partial class SucessoPage : ContentPage
{
    /// <summary>
    /// Construtor da página. Inicializa a interface gráfica da tela de confirmação.
    /// </summary>
    public SucessoPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Evento disparado quando o usuário clica no botão "Voltar".
    /// Utiliza a navegação baseada em rotas do .NET MAUI Shell para retornar à tela anterior ("..").
    /// </summary>
    private async void BtnVoltar_Clicked(object? sender, EventArgs e)
    {
        // Retorna com segurança para a página anterior na pilha de navegação do Shell.
        await Shell.Current.GoToAsync("..");
    }
}