// -----------------------------------------------------------------------
// <copyright file="ISession.cs" company="MicroLite">
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

    /// <summary>
    /// The interface which provides the write methods to map objects to database records.
    /// </summary>
    public interface ISession : IHideObjectMethods, IReadOnlySession
    {
        /// <summary>
        /// Deletes the database record for the specified instance.
        /// </summary>
        /// <param name="instance">The instance to delete from the database.</param>
        /// <returns>true if the object was deleted successfully; otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// bool deleted = false;
        ///
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         try
        ///         {
        ///             deleted = session.Delete(customer);
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
        bool Delete(object instance);

        /// <summary>
        /// Deletes the database records for the specified instances.
        /// </summary>
        /// <typeparam name="T">The type to delete.</typeparam>
        /// <param name="instances">The collection of instances to delete from the database.</param>
        /// <returns>Count of successfully deleted objects.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// bool deleted = false;
        ///
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         try
        ///         {
        ///             deleted = session.Delete(customers);
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
        int Delete<T>(IList<T> instances);

        /// <summary>
        /// Deletes the database record of the specified type with the specified identifier.
        /// </summary>
        /// <param name="type">The type to delete.</param>
        /// <param name="identifier">The identifier of the record to delete.</param>
        /// <returns>true if the object was deleted successfully; otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified type or identifier is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         bool wasDeleted = session.Delete(type: typeof(Customer), identifier: 12823);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        bool Delete(Type type, object identifier);

        /// <summary>
        /// Inserts a new database record for the specified instance.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         session.Insert(customer);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        void Insert(object instance);

        /// <summary>
        /// Inserts a new database records for the specified instances.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         session.Insert(customers);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        void Insert<T>(IList<T> instances);

        /// <summary>
        /// Updates the database record for the specified instance with the current property values.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <returns>true if the object was updated successfully; otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         session.Update(customer);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        bool Update(object instance);

        /// <summary>
        /// Updates the database records for the specified instances with the current property values.
        /// </summary>
        /// <typeparam name="T">The type to update.</typeparam>
        /// <param name="instances">The collection of instances to persist the values for.</param>
        /// <returns>Count of successfully updated objects.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified instance is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the update command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         session.Update(customers);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        int Update<T>(IList<T> instances);

        /// <summary>
        /// Performs a partial update on a table row based upon the values specified in the object delta.
        /// </summary>
        /// <param name="objectDelta">The object delta containing the changes to be applied.</param>
        /// <returns>true if the object was updated successfully; otherwise false.</returns>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         // Create an ObjectDelta which only updates specific properties:
        ///         var objectDelta = new ObjectDelta(type: typeof(Customer), identifier: 12823);
        ///         objectDelta.AddChange(propertyName: "Locked", newValue: false); // Add 1 or more changes.
        ///
        ///         bool wasUpdated = session.Update(objectDelta);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        bool Update(ObjectDelta objectDelta);

        /// <summary>
        /// Executes the specified SQL query and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>The number of rows affected by the SQL query.</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         var query = new SqlQuery("UPDATE Customers SET Locked = @p0 WHERE Locked = @p1", false, true);
        ///
        ///         int unlockedRowCount = session.Execute(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        int Execute(SqlQuery sqlQuery);

        /// <summary>
        /// Executes the specified SQL query as a scalar command.
        /// </summary>
        /// <typeparam name="T">The type of result to be returned.</typeparam>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>The result of the scalar query (the first column in the first row returned).</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the session has been disposed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the specified SqlQuery is null.</exception>
        /// <exception cref="MicroLiteException">Thrown if there is an error executing the command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        /// {
        ///     using (var transaction = session.BeginTransaction())
        ///     {
        ///         // Create a query which returns a single result.
        ///         var query = new SqlQuery("SELECT COUNT(CustomerId) FROM Customers");
        ///
        ///         int customerCount = session.ExecuteScalar&lt;int&gt;(query);
        ///
        ///         transaction.Commit();
        ///     }
        /// }
        /// </code>
        /// </example>
        T ExecuteScalar<T>(SqlQuery sqlQuery);
    }
}