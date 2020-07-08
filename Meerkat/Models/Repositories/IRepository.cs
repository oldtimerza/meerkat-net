using System.Collections.Generic;

namespace Meerkat.Models
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get a list of all objects
        /// </summary>
        /// <returns>List of objects</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Create a new object
        /// </summary>
        /// <param name="t">The new object to create</param>
        void Create(T t);

        /// <summary>
        /// Update the values of an existing object
        /// </summary>
        /// <param name="id">The id of the object to update</param>
        /// <param name="t">An instance with the new details to update with</param>
        /// <returns></returns>
        T Update(int id, T t);

        /// <summary>
        /// Delete an existing object.
        /// </summary>
        /// <param name="id">The id of the object to delete</param>
        void Delete(int id);
    }
}
