// -----------------------------------------------------------------------
// <copyright file="IAsyncSession.cs" company="MicroLite">
// Copyright 2012 - 2015 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace MicroLite
{
#if NET_4_5

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The interface which provides the asynchronous write methods to map objects to database records.
    /// </summary>
    public interface IAsyncSession : IHideObjectMethods, IAsyncReadOnlySession
    {
        /// <summary>
        /// Asynchronously deletes the database record for the specified instance.
        /// </summary>
        /// <param name="instance">The instance to delete from the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// bool deleted = false;
        ///
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         try
        ///         {
        ///             deleted = await session.DeleteAsync(customer);
        ///
        ///             transaction.Commit();
        ///         }
        ///         catch
        ///         {
        ///             deleted = false;
        ///
        ///             transaction.Rollback();
        ///             // Log or throw the exception.
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes DeleteAsync(object, CancellationToken) with CancellationToken.None.</remarks>
        Task<bool> DeleteAsync(object instance);

        /// <summary>
        /// Asynchronously deletes the database record for the specified instance.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="instance">The instance to delete from the database.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// bool deleted = false;
        ///
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         try
        ///         {
        ///             deleted = await session.DeleteAsync(customer);
        ///
        ///             transaction.Commit();
        ///         }
        ///         catch
        ///         {
        ///             deleted = false;
        ///
        ///             transaction.Rollback();
        ///             // Log or throw the exception.
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<bool> DeleteAsync(object instance, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously deletes the database records for the specified instances.
        /// </summary>
        /// <typeparam name="T">The type to delete.</typeparam>
        /// <param name="instances">The collection of instances to delete from the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// int deleted = 0;
        ///
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         try
        ///         {
        ///             deleted = await session.DeleteAsync(customers);
        ///
        ///             transaction.Commit();
        ///         }
        ///         catch
        ///         {
        ///             deleted = 0;
        ///
        ///             transaction.Rollback();
        ///             // Log or throw the exception.
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes DeleteAsync(object, CancellationToken) with CancellationToken.None.</remarks>
        Task<int> DeleteAsync<T>(IList<T> instances);

        /// <summary>
        /// Asynchronously deletes the database records for the specified instances.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type to delete.</typeparam>
        /// <param name="instances">The collection of instances to delete from the database.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// int deleted = 0;
        ///
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         try
        ///         {
        ///             deleted = await session.DeleteAsync(customers);
        ///
        ///             transaction.Commit();
        ///         }
        ///         catch
        ///         {
        ///             deleted = 0;
        ///
        ///             transaction.Rollback();
        ///             // Log or throw the exception.
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<int> DeleteAsync<T>(IList<T> instances, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously deletes the database record of the specified type with the specified identifier.
        /// </summary>
        /// <param name="type">The type to delete.</param>
        /// <param name="identifier">The identifier of the record to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified type or identifier is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         bool wasDeleted = await session.DeleteAsync(type: typeof(Customer), identifier: 12823);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes DeleteAsync(Type, object, CancellationToken) with CancellationToken.None.</remarks>
        Task<bool> DeleteAsync(Type type, object identifier);

        /// <summary>
        /// Asynchronously deletes the database record of the specified type with the specified identifier.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="type">The type to delete.</param>
        /// <param name="identifier">The identifier of the record to delete.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified type or identifier is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         bool wasDeleted = await session.DeleteAsync(type: typeof(Customer), identifier: 12823);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<bool> DeleteAsync(Type type, object identifier, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously inserts a new database record for the specified instance.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.InsertAsync(customer);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes InsertAsync(object, CancellationToken) with CancellationToken.None.</remarks>
        Task InsertAsync(object instance);

        /// <summary>
        /// Asynchronously inserts a new database record for the specified instance.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.InsertAsync(customer);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task InsertAsync(object instance, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously inserts a new database records for the specified instances.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.InsertAsync(customers);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes InsertAsync(object, CancellationToken) with CancellationToken.None.</remarks>
        Task InsertAsync<T>(IList<T> instances);

        /// <summary>
        /// Asynchronously inserts a new database records for the specified instances.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.InsertAsync(customers);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task InsertAsync<T>(IList<T> instances, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously updates the database record for the specified instance with the current property values.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.UpdateAsync(customer);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes UpdateAsync(object, CancellationToken) with CancellationToken.None.</remarks>
        Task<bool> UpdateAsync(object instance);

        /// <summary>
        /// Asynchronously updates the database record for the specified instance with the current property values.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.UpdateAsync(customer);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<bool> UpdateAsync(object instance, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously updates the database records for the specified instances with the current property values.
        /// </summary>
        /// <typeparam name="T">The type to update.</typeparam>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.UpdateAsync(customers);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes UpdateAsync(object, CancellationToken) with CancellationToken.None.</remarks>
        Task<int> UpdateAsync<T>(IList<T> instances);

        /// <summary>
        /// Asynchronously updates the database records for the specified instances with the current property values.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type to update.</typeparam>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         await session.UpdateAsync(customers);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<int> UpdateAsync<T>(IList<T> instances, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously performs a partial update on a table row based upon the values specified in the object delta.
        /// </summary>
        /// <param name="objectDelta">The object delta containing the changes to be applied.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         // Create an ObjectDelta which only updates specific properties:
        ///         var objectDelta = new ObjectDelta(type: typeof(Customer), identifier: 12823);
        ///         objectDelta.AddChange(propertyName: "Locked", newValue: false); // Add 1 or more changes.
        ///
        ///         bool wasUpdated = await session.UpdateAsync(objectDelta);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes UpdateAsync(ObjectDelta, CancellationToken) with CancellationToken.None.</remarks>
        Task<bool> UpdateAsync(ObjectDelta objectDelta);

        /// <summary>
        /// Asynchronously performs a partial update on a table row based upon the values specified in the object delta.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="objectDelta">The object delta containing the changes to be applied.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         // Create an ObjectDelta which only updates specific properties:
        ///         var objectDelta = new ObjectDelta(type: typeof(Customer), identifier: 12823);
        ///         objectDelta.AddChange(propertyName: "Locked", newValue: false); // Add 1 or more changes.
        ///
        ///         bool wasUpdated = await session.UpdateAsync(objectDelta);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<bool> UpdateAsync(ObjectDelta objectDelta, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         var query = new SqlQuery("UPDATE Customers SET Locked = @p0 WHERE Locked = @p1", false, true);
        ///
        ///         int unlockedRowCount = await session.ExecuteAsync(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes ExecuteAsync(SqlQuery, CancellationToken) with CancellationToken.None.</remarks>
        Task<int> ExecuteAsync(SqlQuery sqlQuery);

        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         var query = new SqlQuery("UPDATE Customers SET Locked = @p0 WHERE Locked = @p1", false, true);
        ///
        ///         int unlockedRowCount = await session.ExecuteAsync(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<int> ExecuteAsync(SqlQuery sqlQuery, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         // Create a query which returns a single result.
        ///         var query = new SqlQuery("SELECT COUNT(CustomerId) FROM Customers");
        ///
        ///         int customerCount = await session.ExecuteScalarAsync&lt;int&gt;(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <remarks>Invokes ExecuteScalarAsync(SqlQuery, CancellationToken) with CancellationToken.None.</remarks>
        Task<T> ExecuteScalarAsync<T>(SqlQuery sqlQuery);

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenAsyncSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         // Create a query which returns a single result.
        ///         var query = new SqlQuery("SELECT COUNT(CustomerId) FROM Customers");
        ///
        ///         int customerCount = await session.ExecuteScalarAsync&lt;int&gt;(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<T> ExecuteScalarAsync<T>(SqlQuery sqlQuery, CancellationToken cancellationToken);
    }

#endif
}