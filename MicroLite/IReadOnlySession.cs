﻿// -----------------------------------------------------------------------
// <copyright file="IReadOnlySession.cs" company="MicroLite">
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
    /// The interface which provides the read methods to map objects to database records.
    /// </summary>
    public interface IReadOnlySession : IHideObjectMethods, IDisposable
    {
        /// <summary>
        /// Gets the current transaction or null if one has not been started.
        /// </summary>
        ITransaction CurrentTransaction
        {
            get;
        }

        /// <summary>
        /// Gets the operations which allow additional objects to be queried in a single database call.
        /// </summary>
        IIncludeSession Include
        {
            get;
        }

        /// <summary>
        /// Begins a transaction using <see cref="IsolationLevel"/>.ReadCommitted.
        /// </summary>
        /// <returns>An <see cref="ITransaction"/> with the default isolation level of the connection.</returns>
        /// <remarks>It is a good idea to perform all insert/update/delete actions inside a transaction.</remarks>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         // perform actions against ISession.
        ///         // ...
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        ITransaction BeginTransaction();

        /// <summary>
        /// Begins the transaction with the specified <see cref="IsolationLevel"/>.
        /// </summary>
        /// <param name="isolationLevel">The isolation level to use for the transaction.</param>
        /// <returns>An <see cref="ITransaction"/> with the specified <see cref="IsolationLevel"/>.</returns>
        /// <remarks>It is a good idea to perform all insert/update/delete actions inside a transaction.</remarks>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        /// {
        ///     // This overload allows us to specify a specific IsolationLevel.
        ///     using (var transaction = session.BeginTransaction(IsolationLevel.ReadCommitted))
        ///     {
        ///         // perform actions against ISession.
        ///         // ...
        ///
        ///         try
        ///         {
        ///             transaction.Commit();
        ///         }
        ///         catch (Exception exception)
        ///         {
        ///             transaction.Rollback();
        ///             // Log or throw the exception.
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>The objects that match the query in a list.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         var query = new SqlQuery("SELECT * FROM Invoices WHERE CustomerId = @p0", 1324);
        ///
        ///         var invoices = session.Fetch&lt;Invoice&gt;(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        IList<T> Fetch<T>(SqlQuery sqlQuery);

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="PagedResult&lt;T&gt;"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam>
        /// <param name="sqlQuery">The SQL query to page before executing.</param>
        /// <param name="pagingOptions">The <see cref="PagingOptions"/>.</param>
        /// <returns>A <see cref="PagedResult&lt;T&gt;"/> containing the desired results.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         var query = new SqlQuery("SELECT * FROM Customers WHERE LastName = @p0", "Smith");
        ///
        ///         var customers = session.Paged&lt;Customer&gt;(query, PagingOptions.ForPage(page: 1, resultsPerPage: 25));
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        PagedResult<T> Paged<T>(SqlQuery sqlQuery, PagingOptions pagingOptions);

        /// <summary>
        /// Returns the instance of the specified type which corresponds to the row with the specified identifier
        /// in the mapped table, or null if the identifier values does not exist in the table.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="identifier">The record identifier.</param>
        /// <returns>An instance of the specified type or null if no matching record exists.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         var customer = session.Single&lt;Customer&gt;(17867);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Single", Justification = "It's used in loads of places by the linq extension methods as a method name.")]
        T Single<T>(object identifier) where T : class, new();

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>An instance of the specified type or null if no matching record exists.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         var query = new SqlQuery("SELECT * FROM Customers WHERE EmailAddress = @p0", "fred.flintstone@bedrock.com");
        ///
        ///         // This overload is useful to retrieve a single object based upon a unique value which isn't its identifier.
        ///         var customer = session.Single&lt;Customer&gt;(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Single", Justification = "It's used in loads of places by the linq extension methods as a method name.")]
        T Single<T>(SqlQuery sqlQuery);

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataSet object.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataSet will contain primary key information.</param>
        /// <returns>System.Data.DataSet</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        DataSet DataSet(SqlQuery sqlQuery, bool withKey);

        /// <summary>
        /// Executes the specified SQL query and returns instance of DataTable object.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="withKey">Indicates whether the returned DataTable will contain primary key information.</param>
        /// <returns>System.Data.DataTable</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the query.</exception>
        DataTable DataTable(SqlQuery sqlQuery, bool withKey);

        /// <summary>
        /// Executes any pending queries which have been queued using the Include API.
        /// </summary>
        void ExecutePendingQueries();
    }
}