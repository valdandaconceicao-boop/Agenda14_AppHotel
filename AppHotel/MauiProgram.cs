using Microsoft.Extensions.Logging;

namespace AppHotel;

/// <summary>
/// Ponto de entrada (bootstrap) do aplicativo .NET MAUI.
/// Responsável por construir, configurar e inicializar toda a estrutura da aplicação.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Cria e configura a instância principal do MauiApp.
    /// Define qual é a classe do aplicativo principal (App), configura as fontes globais
    /// e configura o sistema de logging para depuração.
    /// </summary>
    /// <returns>A instância configurada e construída do MauiApp.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>() // Especifica que a classe 'App' gerencia o ciclo de vida do app.
            .ConfigureFonts(fonts =>
            {
                // Registra as fontes OpenSans do projeto e mapeia seus apelidos curtos para uso no XAML.
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        // Registra o provedor de logs no console de depuração caso a compilação esteja em modo DEBUG.
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
