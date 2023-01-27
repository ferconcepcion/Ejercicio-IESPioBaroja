namespace IESPioBaroja.EjemploWeb.Repositorios
{
    public interface IFileJsonRepository<T>
    {
        public void Save();

        public T Load();
    }
}
