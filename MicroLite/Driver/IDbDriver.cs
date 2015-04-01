﻿// -----------------------------------------------------------------------
// <copyright file="IDbDriver.cs" company="MicroLite">
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
namespace MicroLite.Driver
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// The interface for a class which handles IDbConnections and IDbCommands for a specific database.
    /// </summary>
    public interface IDbDriver
    {
        /// <summary>
        /// Gets or sets the connection string of the database this Db Driver is connecting to.
        /// </summary>
        string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the database provider factory.
        /// </summary>
        DbProviderFactory DbProviderFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this DbDriver supports batched queries.
        /// </summary>
        bool SupportsBatchedQueries
        {
            get;
        }

        /// <summary>
        /// Builds an IDbCommand command using the values in the specified SqlQuery.
        /// </summary>
        /// <param name="sqlQuery">The SQL query containing the values for the command.</param>
        /// <returns>An IDbCommand with the CommandText, CommandType, Timeout and Parameters set.</returns>
        /// <exception cref="MicroLiteException">Thrown if the number of arguments does not match the number of parameter names.</exception>
        IDbCommand BuildCommand(SqlQuery sqlQuery);

        /// <summary>
        /// Combines the specified SQL queries into a single SqlQuery.
        /// </summary>
        /// <param name="sqlQueries">The SQL queries to be combined.</param>
        /// <returns>An <see cref="SqlQuery" /> containing the combined command text and arguments.</returns>
        SqlQuery Combine(IEnumerable<SqlQuery> sqlQueries);

        /// <summary>
        /// Combines the specified SQL queries into a single SqlQuery.
        /// </summary>
        /// <param name="sqlQuery1">The first SQL query to be combined.</param>
        /// <param name="sqlQuery2">The second SQL query to be combined.</param>
        /// <returns>An <see cref="SqlQuery" /> containing the combined command text and arguments.</returns>
        SqlQuery Combine(SqlQuery sqlQuery1, SqlQuery sqlQuery2);

        /// <summary>
        /// Creates an IDbConnection to the database.
        /// </summary>
        /// <returns>The IDbConnection to the database.</returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// Creates an DbDataAdapter.
        /// </summary>
        /// <returns>System.Data.Common.DbDataAdapter</returns>
        DbDataAdapter CreateDataAdapter();

        /// <summary>
        /// Sets IDBCommand parameters values to specified SqlArgument array.
        /// </summary>
        /// <param name="command">An IDbCommand with the CommandText, CommandType, Timeout and Parameters set.</param>
        /// <param name="values">Array of SqlArgument with values.</param>
        void SetCommandParametersValues(IDbCommand command, SqlArgument[] values);

        /// <summary>
        /// Sets IDBCommand parameter value to specified value.
        /// </summary>
        /// <param name="command">An IDbCommand with the CommandText, CommandType, Timeout and Parameters set.</param>
        /// <param name="index">Index of parameter in IDbCommand Parameters array.</param>
        /// <param name="value">Value to set.</param>
        void SetCommandParameterValue(IDbCommand command, int index, object value);
    }
}