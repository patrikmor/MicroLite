// -----------------------------------------------------------------------
// <copyright file="ReadOnlySessionExtensions.cs" company="MicroLite">
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

    /// <summary>
    /// Extension methods to <see cref="IReadOnlySession" />.
    /// </summary>
    public static class ReadOnlySessionExtensions
    {
        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>The objects that match the query in a list.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static IList<T> Fetch<T>(this IReadOnlySession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.Fetch<T>(sqlQuery);
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>A <see cref="PagedResult&lt;T&gt;"/> containing the desired results.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static PagedResult<T> Paged<T>(this IReadOnlySession session, string commandText, PagingOptions pagingOptions, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.Paged<T>(sqlQuery, pagingOptions);
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>An instance of the specified type or null if no matching record exists.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static T Single<T>(this IReadOnlySession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.Single<T>(sqlQuery);
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>System.Data.DataSet</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static DataSet DataSet(this IReadOnlySession session, string commandText, bool withKey, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.DataSet(sqlQuery, withKey);
        }

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="session">The instance of IReadOnlySession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>System.Data.DataTable</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        public static DataTable DataTable(this IReadOnlySession session, string commandText, bool withKey, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.DataTable(sqlQuery, withKey);
        }
    }
}
