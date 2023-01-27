using IESPioBaroja.EjemploWeb.Model;
using Microsoft.Extensions.Configuration;

namespace IESPioBaroja.EjemploWeb.Repositorios
{
    public class RepositoryStrategy<T> where T : ModelBase
    {
        private readonly IConfiguration _config;

        public RepositoryStrategy(IConfiguration config)
        {
            _config = config;
        }

        public IRepositorio<T> ObtenerRepositorio()
        {
            if (_config.GetValue<bool>("Repo:EnabledCosmosDB"))
            {
                var connectionString = _config.GetValue<string>("Repo:ConnectionString");

                return new RepositorioCosmosDb<T>(connectionString);
            }
            else
            {
                return new RepositorioJson<T>();
            }
        }
    }
}
