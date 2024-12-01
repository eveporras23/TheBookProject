using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TheBookProject.Db.Context;

public static class TheBookProjectContextExtensions
{
    public static IServiceCollection AddTheBookProjectContext(this IServiceCollection services, string? connectionString = null)
    {
        if (connectionString == null)
        {
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
            builder.DataSource = Path.Combine(AppContext.BaseDirectory, "TheBookProject.sqlite");
           
            connectionString = builder.ConnectionString;
        }

        services.AddDbContext<TheBookProjectDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        return services;
    }
}