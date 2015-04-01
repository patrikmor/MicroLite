// -----------------------------------------------------------------------
// <copyright file="SessionExtensions.cs" company="MicroLite">
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

    /// <summary>
    /// Extension methods to <see cref="ISession" />.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="session">The instance of ISession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>The number of rows affected by the SQL query.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static int Execute(this ISession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.Execute(sqlQuery);
        }

        /// <summary>
        /// Executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="session">The instance of ISession object.</param>
        /// <param name="commandText">The SQL command text to be executed against the data source.</param>
        /// <param name="arguments">The argument values for the SQL command.</param>
        /// <returns>The result of the scalar query (the first column in the first row returned).</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        public static T ExecuteScalar<T>(this ISession session, string commandText, params object[] arguments)
        {
            var sqlQuery = arguments != null ? new SqlQuery(commandText, arguments) : new SqlQuery(commandText);
            return session.ExecuteScalar<T>(sqlQuery);
        }
    }
}
