// -----------------------------------------------------------------------
// <copyright file="AsyncReadOnlySessionExtensions.cs" company="MicroLite">
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
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods to <see cref="IAsyncReadOnlySession" />.
    /// </summary>
    public static class AsyncReadOnlySessionExtensions
    {
        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <remarks>Invokes FetchAsync&lt;T&gt;(SqlQuery, CancellationToken) with CancellationToken.None.</remarks>
        public static Task<IList<T>> FetchAsync<T>(this IAsyncReadOnlySession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.FetchAsync<T>(sqlQuery);
        }

        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<IList<T>> FetchAsync<T>(this IAsyncReadOnlySession session, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return await session.FetchAsync<T>(sqlQuery, cancellationToken);
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <remarks>Invokes PagedAsync&lt;T&gt;(SqlQuery, PagingOptions, CancellationToken) with CancellationToken.None.</remarks>
        public static Task<PagedResult<T>> PagedAsync<T>(this IAsyncReadOnlySession session, string commandText, PagingOptions pagingOptions, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.PagedAsync<T>(sqlQuery, pagingOptions);
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<PagedResult<T>> PagedAsync<T>(this IAsyncReadOnlySession session, string commandText, PagingOptions pagingOptions, CancellationToken cancellationToken, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return await session.PagedAsync<T>(sqlQuery, pagingOptions, cancellationToken);
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <remarks>Invokes SingleAsync&lt;T&gt;(SqlQuery, CancellationToken) with CancellationToken.None.</remarks>
        public static Task<T> SingleAsync<T>(this IAsyncReadOnlySession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.SingleAsync<T>(sqlQuery);
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<T> SingleAsync<T>(this IAsyncReadOnlySession session, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return await session.SingleAsync<T>(sqlQuery, cancellationToken);
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<DataSet> DataSetAsync(this IAsyncReadOnlySession session, string commandText, bool withKey, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.DataSetAsync(sqlQuery, withKey);
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<DataSet> DataSetAsync(this IAsyncReadOnlySession session, string commandText, bool withKey, CancellationToken cancellationToken, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return await session.DataSetAsync(sqlQuery, withKey, cancellationToken);
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static Task<DataTable> DataTableAsync(this IAsyncReadOnlySession session, string commandText, bool withKey, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.DataTableAsync(sqlQuery, withKey);
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static async Task<DataTable> DataTableAsync(this IAsyncReadOnlySession session, string commandText, bool withKey, CancellationToken cancellationToken, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return await session.DataTableAsync(sqlQuery, withKey, cancellationToken);
        }
    }

#endif
}
