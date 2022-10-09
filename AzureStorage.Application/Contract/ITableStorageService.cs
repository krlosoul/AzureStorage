namespace AzureStorage.Application.Contract
{
    using Microsoft.Azure.Cosmos.Table;
    using System.Linq.Expressions;

    public interface ITableStorageService<T> where T : ITableEntity, new()
    {
        #region Queries
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets all where expression asynchronous.
        /// </summary>
        /// /// <param name="expression">The expression.</param>
        /// <returns>IEnumerable&lt;T&gt;</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get data where expression asynchronous.
        /// </summary>
        /// /// <param name="expression">The expression.</param>
        /// <returns>T</returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        #endregion

        #region Commands
        /// <summary>
        /// Insert the entity asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>Etag.</returns>
        Task<string> InsertAsync(T entity);

        /// <summary>
        /// Update the entity asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>Etag.</returns>
        Task<string> UpdateAsync(T entity);

        /// <summary>
        /// Delete the entity asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>Etag.</returns>
        Task<string> DeleteAsync(T entity);
        #endregion
    }
}
