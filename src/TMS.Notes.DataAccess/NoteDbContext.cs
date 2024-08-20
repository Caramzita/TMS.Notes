using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using TMS.Notes.Core;
using TMS.Notes.DataAccess.Dtos;

namespace TMS.Notes.DataAccess;

/// <summary>
/// Представляет контекст базы данных для домена Notes, 
/// предоставляя доступ к базовой базе данных и управляя конфигурацией моделей сущностей.
/// </summary>
public class NoteDbContext : DbContext
{
    /// <summary>
    /// Конфигурация приложения.
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<NoteDbContext> _logger;

    /// <summary>
    /// Возвращает или устанавливает набор данных для объекта <see cref="Note"/>.
    /// </summary>
    public DbSet<NoteDto> Notes { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NoteDbContext"/>.
    /// </summary>
    /// <param name="configuration"> Экземпляр конфигурации. </param>
    /// <param name="logger"> Экземпляр логгера. </param>
    /// <exception cref="ArgumentNullException"> Генерируется, 
    /// если значение конфигурации или регистратора равно null. </exception>
    public NoteDbContext(IConfiguration configuration, ILogger<NoteDbContext> logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DatabaseConnection"))
            .EnableSensitiveDataLogging()
            .LogTo(log => _logger.LogInformation(log));
    }
}
