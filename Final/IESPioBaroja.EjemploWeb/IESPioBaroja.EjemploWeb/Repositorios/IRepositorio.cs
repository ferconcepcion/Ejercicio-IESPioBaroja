using System.Collections.Generic;

namespace IESPioBaroja.EjemploWeb.Repositorios
{
    public interface IRepositorio<T>
    {
        void Actualizar(T elemento);
        void Anadir(T elemento);
        void Eliminar(string id);
        T ObtenerPorId(string id);
        IEnumerable<T> ObtenerElementos();
    }
}
