namespace dotnet_rpg.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Character> Characters => Set<Character>(); //Aqui definimos quais entidades existem e devem ter tabela representando no banco de dados
    }
}