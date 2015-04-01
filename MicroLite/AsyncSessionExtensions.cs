// -----------------------------------------------------------------------
// <copyright file="AsyncSessionExtensions.cs" company="MicroLite">
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
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods to <see cref="IAsyncSession" />.
    /// </summary>
    public static class AsyncSessionExtensions
    {
        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <remarks>Invokes ExecuteAsync(SqlQuery, CancellationToken) with CancellationToken.None.</remarks>
        public static Task<int> ExecuteAsync(this IAsyncSession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.ExecuteAsync(sqlQuery);
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query and returns the number of rows affected.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static async Task<int> ExecuteAsync(this IAsyncSession session, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return await session.ExecuteAsync(sqlQuery, cancellationToken);
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <remarks>Invokes ExecuteScalarAsync(SqlQuery, CancellationToken) with CancellationToken.None.</remarks>
        public static Task<T> ExecuteScalarAsync<T>(this IAsyncSession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.ExecuteScalarAsync<T>(sqlQuery);
        }

        /// <summary>
        /// Asynchronously executes the specified SQL query as a scalar command.
        /// This method propagates a notification that operations should be cancelled.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static async Task<T> ExecuteScalarAsync<T>(this IAsyncSession session, string commandText, CancellationToken cancellationToken, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return await session.ExecuteScalarAsync<T>(sqlQuery, cancellationToken);
        }
    }

#endif
}
