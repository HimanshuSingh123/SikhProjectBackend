using System.Reflection;
using Mapster;

namespace QuoteApi.Api.ServiceExtensions;
/// <summary>
/// Provides configuration for Mapster type mapping library.
/// </summary>
public static class MapsterConfig
{
    private static readonly Lazy<int> Initializer = new(() =>
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly()).Count);

    /// <summary>
    /// Configures Mapster by scanning the executing assembly for mapping configurations.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="Lazy{T}"/> to avoid tests causing multiple initializations.
    /// </remarks>
    public static void Configure() =>
        _ = Initializer.Value;
}