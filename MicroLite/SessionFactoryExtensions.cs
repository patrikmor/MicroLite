// -----------------------------------------------------------------------
// <copyright file="SessionFactoryExtensions.cs" company="MicroLite">
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
    using System;
    using System.Collections.Generic;
    using System.Data;

#if NET_4_5

    using System.Threading;
    using System.Threading.Tasks;

#endif

    /// <summary>
    /// Extension methods to <see cref="ISessionFactory" />.
    /// </summary>
    public static class SessionFactoryExtensions
    {

#if NET_4_5

        #region IAsyncReadOnlySession Extensions
        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<IList<T>> FetchAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return session.FetchAsync<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<IList<T>> FetchAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return await session.FetchAsync<T>(sqlQuery, cancellationToken);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<IList<T>> FetchAsync<T>(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.FetchAsync<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<IList<T>> FetchAsync<T>(this ISessionFactory factory, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return await session.FetchAsync<T>(sqlQuery, cancellationToken);
            }
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to page before executing.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<PagedResult<T>> PagedAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery, PagingOptions pagingOptions)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return session.PagedAsync<T>(sqlQuery, pagingOptions);
            }
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to page before executing.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<PagedResult<T>> PagedAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery, PagingOptions pagingOptions, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return await session.PagedAsync<T>(sqlQuery, pagingOptions, cancellationToken);
            }
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<PagedResult<T>> PagedAsync<T>(this ISessionFactory factory, string commandText, PagingOptions pagingOptions, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.PagedAsync<T>(sqlQuery, pagingOptions);
            }
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<PagedResult<T>> PagedAsync<T>(this ISessionFactory factory, string commandText, PagingOptions pagingOptions, CancellationToken cancellationToken, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return await session.PagedAsync<T>(sqlQuery, pagingOptions, cancellationToken);
            }
        }

        /// <summary>
        /// Returns the instance of the specified type which corresponds to the row with the specified identifier
        /// in the mapped table, or null if the identifier values does not exist in the table.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="identifier">The record identifier.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<T> SingleAsync<T>(this ISessionFactory factory, object identifier)
           where T : class, new()
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return session.SingleAsync<T>(identifier);
            }
        }

        /// <summary>
        /// Returns the instance of the specified type which corresponds to the row with the specified identifier
        /// in the mapped table, or null if the identifier values does not exist in the table.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="identifier">The record identifier.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<T> SingleAsync<T>(this ISessionFactory factory, object identifier, CancellationToken cancellationToken)
           where T : class, new()
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return await session.SingleAsync<T>(identifier, cancellationToken);
            }
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<T> SingleAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return session.SingleAsync<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<T> SingleAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return await session.SingleAsync<T>(sqlQuery, cancellationToken);
            }
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<T> SingleAsync<T>(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.SingleAsync<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<T> SingleAsync<T>(this ISessionFactory factory, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return await session.SingleAsync<T>(sqlQuery, cancellationToken);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<DataSet> DataSetAsync(this ISessionFactory factory, SqlQuery sqlQuery, bool withKey)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return session.DataSetAsync(sqlQuery, withKey);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<DataSet> DataSetAsync(this ISessionFactory factory, SqlQuery sqlQuery, bool withKey, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return await session.DataSetAsync(sqlQuery, withKey, cancellationToken);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<DataSet> DataSetAsync(this ISessionFactory factory, string commandText, bool withKey, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.DataSetAsync(sqlQuery, withKey);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<DataSet> DataSetAsync(this ISessionFactory factory, string commandText, bool withKey, CancellationToken cancellationToken, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return await session.DataSetAsync(sqlQuery, withKey, cancellationToken);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<DataTable> DataTableAsync(this ISessionFactory factory, SqlQuery sqlQuery, bool withKey)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return session.DataTableAsync(sqlQuery, withKey);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<DataTable> DataTableAsync(this ISessionFactory factory, SqlQuery sqlQuery, bool withKey, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                return await session.DataTableAsync(sqlQuery, withKey, cancellationToken);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<DataTable> DataTableAsync(this ISessionFactory factory, string commandText, bool withKey, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.DataTableAsync(sqlQuery, withKey);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<DataTable> DataTableAsync(this ISessionFactory factory, string commandText, bool withKey, CancellationToken cancellationToken, params object[] arguments)
        {
            using (var session = factory.OpenAsyncReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return await session.DataTableAsync(sqlQuery, withKey, cancellationToken);
            }
        }
        #endregion

        #region IAsyncSession Extensions
        /// <summary>
        /// Asynchronously deletes the database record for the specified instance.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to delete from the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static Task<bool> DeleteAsync(this ISessionFactory factory, object instance)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.DeleteAsync(instance);
            }
        }

        /// <summary>
        /// Asynchronously deletes the database record for the specified instance.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to delete from the database.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static async Task<bool> DeleteAsync(this ISessionFactory factory, object instance, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.DeleteAsync(instance);
            }
        }

        /// <summary>
        /// Asynchronously deletes the database records for the specified instances.
        /// </summary>
        /// <typeparam name="T">The type to delete.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to delete from the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static Task<int> DeleteAsync<T>(this ISessionFactory factory, IList<T> instances)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.DeleteAsync<T>(instances);
            }
        }

        /// <summary>
        /// Asynchronously deletes the database records for the specified instances.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type to delete.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to delete from the database.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static async Task<int> DeleteAsync<T>(this ISessionFactory factory, IList<T> instances, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.DeleteAsync<T>(instances);
            }
        }

        /// <summary>
        /// Asynchronously deletes the database record of the specified type with the specified identifier.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="type">The type to delete.</param>
        /// <param name="identifier">The identifier of the record to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified type or identifier is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static Task<bool> DeleteAsync(this ISessionFactory factory, Type type, object identifier)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.DeleteAsync(type, identifier);
            }
        }

        /// <summary>
        /// Asynchronously deletes the database record of the specified type with the specified identifier.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="type">The type to delete.</param>
        /// <param name="identifier">The identifier of the record to delete.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified type or identifier is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static async Task<bool> DeleteAsync(this ISessionFactory factory, Type type, object identifier, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.DeleteAsync(type, identifier, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously inserts a new database record for the specified instance.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        public static Task InsertAsync(this ISessionFactory factory, object instance)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.InsertAsync(instance);
            }
        }

        /// <summary>
        /// Asynchronously inserts a new database record for the specified instance.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        public static async Task InsertAsync(this ISessionFactory factory, object instance, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                await session.InsertAsync(instance, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously inserts a new database records for the specified instances.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        public static Task InsertAsync<T>(this ISessionFactory factory, IList<T> instances)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.InsertAsync<T>(instances);
            }
        }

        /// <summary>
        /// Asynchronously inserts a new database records for the specified instances.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        public static async Task InsertAsync<T>(this ISessionFactory factory, IList<T> instances, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                await session.InsertAsync<T>(instances, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously updates the database record for the specified instance with the current property values.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        public static Task<bool> UpdateAsync(this ISessionFactory factory, object instance)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.UpdateAsync(instance);
            }
        }

        /// <summary>
        /// Asynchronously updates the database record for the specified instance with the current property values.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        public static async Task<bool> UpdateAsync(this ISessionFactory factory, object instance, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.UpdateAsync(instance, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously updates the database records for the specified instances with the current property values.
        /// </summary>
        /// <typeparam name="T">The type to update.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        public static Task<int> UpdateAsync<T>(this ISessionFactory factory, IList<T> instances)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.UpdateAsync(instances);
            }
        }

        /// <summary>
        /// Asynchronously updates the database records for the specified instances with the current property values.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type to update.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        public static async Task<int> UpdateAsync<T>(this ISessionFactory factory, IList<T> instances, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.UpdateAsync(instances, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously performs a partial update on a table row based upon the values specified in the object delta.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="objectDelta">The object delta containing the changes to be applied.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static Task<bool> UpdateAsync(this ISessionFactory factory, ObjectDelta objectDelta)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.UpdateAsync(objectDelta);
            }
        }

        /// <summary>
        /// Asynchronously performs a partial update on a table row based upon the values specified in the object delta.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="objectDelta">The object delta containing the changes to be applied.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task<bool> UpdateAsync(this ISessionFactory factory, ObjectDelta objectDelta, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.UpdateAsync(objectDelta, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static Task<int> ExecuteAsync(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.ExecuteAsync(sqlQuery);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static async Task<int> ExecuteAsync(this ISessionFactory factory, SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.ExecuteAsync(sqlQuery, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static Task<int> ExecuteAsync(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.ExecuteAsync(sqlQuery);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static async Task<int> ExecuteAsync(this ISessionFactory factory, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return await session.ExecuteAsync(sqlQuery, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static Task<T> ExecuteScalarAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return session.ExecuteScalarAsync<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static async Task<T> ExecuteScalarAsync<T>(this ISessionFactory factory, SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                return await session.ExecuteScalarAsync<T>(sqlQuery, cancellationToken);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static Task<T> ExecuteScalarAsync<T>(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.ExecuteScalarAsync<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static async Task<T> ExecuteScalarAsync<T>(this ISessionFactory factory, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            using (var session = factory.OpenAsyncSession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return await session.ExecuteScalarAsync<T>(sqlQuery, cancellationToken);
            }
        }
        #endregion

#endif

        #region IReadOnlySession Extensions
        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>The objects that match the query in a list.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static IList<T> Fetch<T>(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                return session.Fetch<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>The objects that match the query in a list.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static IList<T> Fetch<T>(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.Fetch<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to page before executing.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <returns>A <see cref="PagedResult&lt;T&gt;"/> containing the desired results.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static PagedResult<T> Paged<T>(this ISessionFactory factory, SqlQuery sqlQuery, PagingOptions pagingOptions)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                return session.Paged<T>(sqlQuery, pagingOptions);
            }
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A <see cref="PagedResult&lt;T&gt;"/> containing the desired results.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static PagedResult<T> Paged<T>(this ISessionFactory factory, string commandText, PagingOptions pagingOptions, params object[] arguments)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.Paged<T>(sqlQuery, pagingOptions);
            }
        }

        /// <summary>
        /// Returns the instance of the specified type which corresponds to the row with the specified identifier
        /// in the mapped table, or null if the identifier values does not exist in the table.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="identifier">The record identifier.</param>
        /// <returns>An instance of the specified type or null if no matching record exists.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static T Single<T>(this ISessionFactory factory, object identifier)
           where T : class, new()
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                return session.Single<T>(identifier);
            }
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>An instance of the specified type or null if no matching record exists.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static T Single<T>(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using (var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                return session.Single<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>An instance of the specified type or null if no matching record exists.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static T Single<T>(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using (var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.Single<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <returns>System.Data.DataSet</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static DataSet DataSet(this ISessionFactory factory, SqlQuery sqlQuery, bool withKey)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                return session.DataSet(sqlQuery, withKey);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>System.Data.DataSet</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static DataSet DataSet(this ISessionFactory factory, string commandText, bool withKey, params object[] arguments)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.DataSet(sqlQuery, withKey);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <returns>System.Data.DataTable</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static DataTable DataTable(this ISessionFactory factory, SqlQuery sqlQuery, bool withKey)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                return session.DataTable(sqlQuery, withKey);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>System.Data.DataTable</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static DataTable DataTable(this ISessionFactory factory, string commandText, bool withKey, params object[] arguments)
        {
            using(var session = factory.OpenReadOnlySession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.DataTable(sqlQuery, withKey);
            }
        }
        #endregion

        #region ISession Extensions
        /// <summary>
        /// Deletes the database record for the specified instance.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to delete from the database.</param>
        /// <returns>true if the object was deleted successfully; otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static bool Delete(this ISessionFactory factory, object instance)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.Delete(instance);
            }
        }

        /// <summary>
        /// Deletes the database records for the specified instances.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to delete from the database.</param>
        /// <returns>Count of successfully deleted objects.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static int Delete<T>(this ISessionFactory factory, IList<T> instances)
        {
            using (var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.Delete(instances);
            }
        }

        /// <summary>
        /// Deletes the database record of the specified type with the specified identifier.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="type">The type to delete.</param>
        /// <param name="identifier">The identifier of the record to delete.</param>
        /// <returns>true if the object was deleted successfully; otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified type or identifier is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        public static bool Delete(this ISessionFactory factory, Type type, object identifier)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.Delete(type, identifier);
            }
        }

        /// <summary>
        /// Inserts a new database record for the specified instance.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        public static void Insert(this ISessionFactory factory, object instance)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                session.Insert(instance);
            }
        }

        /// <summary>
        /// Inserts a new database records for the specified instances.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        public static void Insert<T>(this ISessionFactory factory, IList<T> instances)
        {
            using (var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                session.Insert(instances);
            }
        }

        /// <summary>
        /// Updates the database record for the specified instance with the current property values.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <returns>true if the object was updated successfully; otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        public static bool Update(this ISessionFactory factory, object instance)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.Update(instance);
            }
        }

        /// <summary>
        /// Updates the database records for the specified instances with the current property values.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <returns>Count of successfully updated objects.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        public static int Update<T>(this ISessionFactory factory, IList<T> instances)
        {
            using (var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.Update(instances);
            }
        }

        /// <summary>
        /// Performs a partial update on a table row based upon the values specified in the object delta.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="objectDelta">The object delta containing the changes to be applied.</param>
        /// <returns>true if the object was updated successfully; otherwise false.</returns>
        public static bool Update(this ISessionFactory factory, ObjectDelta objectDelta)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.Update(objectDelta);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>The number of rows affected by the SQL query.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static int Execute(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.Execute(sqlQuery);
            }
        }

        /// <summary>
        /// Executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>The number of rows affected by the SQL query.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static int Execute(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.Execute(sqlQuery);
            }
        }

        /// <summary>
        /// Executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>The result of the scalar query (the first column in the first row returned).</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static T ExecuteScalar<T>(this ISessionFactory factory, SqlQuery sqlQuery)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                return session.ExecuteScalar<T>(sqlQuery);
            }
        }

        /// <summary>
        /// Executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="factory">The instance of session factory object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>The result of the scalar query (the first column in the first row returned).</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static T ExecuteScalar<T>(this ISessionFactory factory, string commandText, params object[] arguments)
        {
            using(var session = factory.OpenSession(ConnectionScope.PerSession))
            {
                var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
                return session.ExecuteScalar<T>(sqlQuery);
            }
        }
        #endregion
    }
}
