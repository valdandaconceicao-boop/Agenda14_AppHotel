namespace AppHotel;

/// <summary>
/// Contêiner de casca (Shell) da aplicação.
/// Define a hierarquia visual do aplicativo, navegação entre abas, painéis laterais
/// e o mapeamento de rotas para navegação dinâmica.
/// </summary>
public partial class AppShell : Shell
{
    /// <summary>
    /// Construtor da casca. Inicializa a interface e registra rotas personalizadas
    /// para navegação programática baseada em URL (Ex: Navigation.PushAsync).
    /// </summary>
    public AppShell()
    {
        InitializeComponent();
        
        // Registra a SucessoPage no sistema global de rotas do Shell usando o seu próprio nome de classe.
        Routing.RegisterRoute(nameof(SucessoPage), typeof(SucessoPage));
    }
}