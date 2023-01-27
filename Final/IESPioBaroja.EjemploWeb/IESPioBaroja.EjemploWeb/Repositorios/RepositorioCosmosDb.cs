using IESPioBaroja.EjemploWeb.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IESPioBaroja.EjemploWeb.Repositorios
{
    public class RepositorioCosmosDb<T> : IRepositorio<T> where T : ModelBase
    {
        private readonly Container _container;

        public RepositorioCosmosDb(string connectionString, string databaseName, string containerName)
        {
            var client = new CosmosClientBuilder(connectionString)
                .Build();

            Database database = Task.Run(async () => await client.CreateDatabaseIfNotExistsAsync(databaseName)).Result;

            _container = Task.Run(async () =>
                await database.CreateContainerIfNotExistsAsync(
                    containerName,
                    "/id"))
                .Result;
        }

        public RepositorioCosmosDb(string connectionString) : this(connectionString, "alumnosDatabase", "alumnos") { }

        public void Anadir(T elemento)
        {
            _ = Task.Run(async () => await _container.CreateItemAsync(elemento, new PartitionKey(elemento.Id))).Result;
        }

        public void Actualizar(T elemento)
        {
            var elementoExistente = Task.Run(async () => await _container.ReadItemAsync<T>(elemento.Id, new PartitionKey(elemento.Id))).Result.Resource;

            if (elementoExistente == null)
            {
                throw new Exception("404 - Not Found");
            }

            _ = Task.Run(async () => await _container.UpsertItemAsync(elemento)).Result;
        }

        public void Eliminar(string id)
        {
            _ = Task.Run(async () => await _container.DeleteItemAsync<T>(id, new PartitionKey(id))).Result.Resource;
        }

        public IEnumerable<T> ObtenerElementos()
        {
            var elementos = new List<T>();
            IOrderedQueryable<T> queryable = _container.GetItemLinqQueryable<T>();

            // Convert to feed iterator
            using FeedIterator<T> linqFeed = queryable.ToFeedIterator();

            // Iterate query result pages
            while (linqFeed.HasMoreResults)
            {
                FeedResponse<T> response = Task.Run(async () => await linqFeed.ReadNextAsync()).Result;

                // Iterate query results
                foreach (T item in response)
                {
                    elementos.Add(item);
                }
            }

            return elementos;
        }

        public T ObtenerPorId(string id)
        {
            var elementoExistente = Task.Run(async () => await _container.ReadItemAsync<T>(id, new PartitionKey(id))).Result.Resource;

            return elementoExistente;
        }
    }
}
