namespace AppHotel;

/// <summary>
/// Classe que representa o aplicativo como um todo.
/// Controla o ciclo de vida global e define a janela inicial da aplicação.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Construtor principal da classe App.
    /// Inicializa a renderização de componentes XAML.
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Sobrescreve o método de criação de janelas da plataforma.
    /// Retorna uma nova janela configurada para usar o AppShell como contêiner de rotas e navegação.
    /// </summary>
    /// <param name="activationState">Estado de ativação da plataforma (opcional).</param>
    /// <returns>A janela (Window) principal da aplicação.</returns>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}