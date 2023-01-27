using IESPioBaroja.EjemploWeb.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace IESPioBaroja.EjemploWeb.Repositorios
{
    public class RepositorioJson<T> : IRepositorio<T>, IFileJsonRepository<IEnumerable<T>> where T : ModelBase
    {
        private readonly string _path;
        private readonly string _filename;
        private readonly IList<T> _elementos;

        public RepositorioJson(string path, string filename)
        {
            _path = path;
            _filename = filename;
            _elementos = Load().ToList();
        }

        public RepositorioJson() : this(Directory.GetCurrentDirectory(), "elementos.json") { }

        public virtual IEnumerable<T> Load()
        {
            var filePath = string.Format(@"{0}\{1}", _path, _filename);
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<IEnumerable<T>>(jsonString);
            }

            return new List<T>();
        }

        public virtual void Save()
        {
            string json = JsonSerializer.Serialize(_elementos);
            File.WriteAllText(string.Format(@"{0}\{1}", _path, _filename), json);
        }

        public virtual void Anadir(T elemento)
        {
            // Completar...

            Save();
        }

        public virtual void Eliminar(string id)
        {
            // Completar...

            Save();
        }

        public virtual void Actualizar(T elemento)
        {
            // Completar...

            Save();
        }

        public virtual T ObtenerPorId(string id)
        {
            // Completar...
        }

        public virtual IEnumerable<T> ObtenerElementos()
        {
            // Completar...
        }
    }
}
