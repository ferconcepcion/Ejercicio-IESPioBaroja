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
            _path = Directory.GetCurrentDirectory();
            _filename = filename;
            _elementos = Load().ToList();
        }

        public RepositorioJson() : this("data", "elementos.json") { }

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
            _elementos.Add(elemento);

            Save();
        }

        public virtual void Eliminar(string id)
        {
            var elementoParaActualizar = _elementos.First(x => x.Id == id);

            _elementos.Remove(elementoParaActualizar);

            Save();
        }

        public virtual void Actualizar(T elemento)
        {
            var elementoParaActualizar = _elementos.First(x => x.Id == elemento.Id);

            _elementos.Remove(elementoParaActualizar);

            _elementos.Add(elemento);

            Save();
        }

        public virtual T ObtenerPorId(string id)
        {
            var elemento = _elementos.First(x => x.Id == id);

            return elemento;
        }

        public virtual IEnumerable<T> ObtenerElementos()
        {
            return _elementos;
        }
    }
}
