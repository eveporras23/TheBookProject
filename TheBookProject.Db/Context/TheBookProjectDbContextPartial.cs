using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
 
namespace TheBookProject.Db.Context;

public partial class TheBookProjectDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
         
            builder.DataSource = Path.Combine(AppContext.BaseDirectory, "TheBookProject.sqlite");
            optionsBuilder.UseSqlite(builder.ConnectionString);
        }
    }
}