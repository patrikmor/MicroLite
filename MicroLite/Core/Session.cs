﻿// -----------------------------------------------------------------------
// <copyright file="Session.cs" company="MicroLite">
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
namespace MicroLite.Core
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MicroLite.Dialect;
    using MicroLite.Driver;
    using MicroLite.Listeners;
    using MicroLite.Mapping;
    using MicroLite.TypeConverters;

    /// <summary>
    /// The default implementation of <see cref="ISession"/>.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ConnectionScope: {ConnectionScope}")]
    internal sealed class Session : ReadOnlySession, ISession
    {
        private readonly IList<IDeleteListener> deleteListeners;
        private readonly IList<IInsertListener> insertListeners;
        private readonly IList<IUpdateListener> updateListeners;

        internal Session(
            ConnectionScope connectionScope,
            ISqlDialect sqlDialect,
            IDbDriver sqlDriver,
            IList<IDeleteListener> deleteListeners,
            IList<IInsertListener> insertListeners,
            IList<IUpdateListener> updateListeners)
            : base(connectionScope, sqlDialect, sqlDriver)
        {
            this.deleteListeners = deleteListeners;
            this.insertListeners = insertListeners;
            this.updateListeners = updateListeners;
        }

        public bool Delete(object instance)
        {
            this.ThrowIfDisposed();

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            for (int i = 0; i < this.deleteListeners.Count; i++)
            {
                this.deleteListeners[i].BeforeDelete(instance);
            }

            var objectInfo = ObjectInfo.For(instance.GetType());

            var identifier = objectInfo.GetIdentifierValue(instance);

            if (objectInfo.IsDefaultIdentifier(identifier))
            {
                throw new MicroLiteException(ExceptionMessages.Session_IdentifierNotSetForDelete);
            }

            var sqlQuery = this.SqlDialect.BuildDeleteSqlQuery(objectInfo, identifier);

            var rowsAffected = this.ExecuteQuery(sqlQuery);

            for (int i = this.deleteListeners.Count - 1; i >= 0; i--)
            {
                this.deleteListeners[i].AfterDelete(instance, rowsAffected);
            }

            return rowsAffected == 1;
        }

        public int Delete<T>(IList<T> instances)
        {
            this.ThrowIfDisposed();

            if (instances == null)
            {
                throw new ArgumentNullException("instances");
            }

            if (instances.Count == 0)
            {
                return 0;
            }

            var objectInfo = ObjectInfo.For(typeof(T));

            int allRowsAffected = 0;
            IDbCommand command = null;

            try
            {
                foreach(var instance in instances)
                {
                    for (int i = 0; i < this.deleteListeners.Count; i++)
                    {
                        this.deleteListeners[i].BeforeDelete(instance);
                    }

                    var identifier = objectInfo.GetIdentifierValue(instance);

                    if (objectInfo.IsDefaultIdentifier(identifier))
                    {
                        throw new MicroLiteException(ExceptionMessages.Session_IdentifierNotSetForDelete);
                    }

                    if (command == null)
                    {
                        //initialize IDbCommand in the first iteration
                        var sqlQuery = this.SqlDialect.BuildDeleteSqlQuery(objectInfo, identifier);
                        command = this.CreateCommand(sqlQuery);
                    }
                    else
                    {
                        //set command parameter value
                        this.DbDriver.SetCommandParameterValue(command, 0, identifier);
                    }

                    int rowsAffected = this.ExecuteQuery(command);

                    for (int i = this.deleteListeners.Count - 1; i >= 0; i--)
                    {
                        this.deleteListeners[i].AfterDelete(instance, rowsAffected);
                    }

                    allRowsAffected += rowsAffected;
                }
            }
            finally
            {
                this.CommandCompleted();

                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
            }

            return allRowsAffected;
        }

        public bool Delete(Type type, object identifier)
        {
            this.ThrowIfDisposed();

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (identifier == null)
            {
                throw new ArgumentNullException("identifier");
            }

            var objectInfo = ObjectInfo.For(type);

            var sqlQuery = this.SqlDialect.BuildDeleteSqlQuery(objectInfo, identifier);

            var rowsAffected = this.ExecuteQuery(sqlQuery);

            return rowsAffected == 1;
        }

        public int Execute(SqlQuery sqlQuery)
        {
            this.ThrowIfDisposed();

            if (sqlQuery == null)
            {
                throw new ArgumentNullException("sqlQuery");
            }

            return this.ExecuteQuery(sqlQuery);
        }

        public T ExecuteScalar<T>(SqlQuery sqlQuery)
        {
            this.ThrowIfDisposed();

            if (sqlQuery == null)
            {
                throw new ArgumentNullException("sqlQuery");
            }

            return this.ExecuteScalarQuery<T>(sqlQuery);
        }

        public void Insert(object instance)
        {
            this.ThrowIfDisposed();

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            for (int i = 0; i < this.insertListeners.Count; i++)
            {
                this.insertListeners[i].BeforeInsert(instance);
            }

            var objectInfo = ObjectInfo.For(instance.GetType());
            objectInfo.VerifyInstanceForInsert(instance);

            object identifier = this.InsertReturningIdentifier(objectInfo, instance);

            for (int i = this.insertListeners.Count - 1; i >= 0; i--)
            {
                this.insertListeners[i].AfterInsert(instance, identifier);
            }
        }

        public void Insert<T>(IList<T> instances)
        {
            this.ThrowIfDisposed();

            if (instances == null)
            {
                throw new ArgumentNullException("instances");
            }

            if (instances.Count == 0)
            {
                return;
            }

            var objectInfo = ObjectInfo.For(typeof(T));

            if (this.SqlDialect.SupportsSelectInsertedIdentifier
                && objectInfo.TableInfo.IdentifierStrategy != IdentifierStrategy.Assigned
                && !this.DbDriver.SupportsBatchedQueries)
            {
                throw new NotSupportedException(ExceptionMessages.Session_MultipleInsertsNotSupported);
            }

            IDbCommand command = null;

            try
            {
                foreach(var instance in instances)
                {
                    for(int i = 0; i < this.insertListeners.Count; i++)
                    {
                        this.insertListeners[i].BeforeInsert(instance);
                    }

                    objectInfo.VerifyInstanceForInsert(instance);

                    if(command == null)
                    {
                        //initialize IDbCommand in the first iteration
                        var insertSqlQuery = this.SqlDialect.BuildInsertSqlQuery(objectInfo, instance);

                        if (this.SqlDialect.SupportsSelectInsertedIdentifier
                            && objectInfo.TableInfo.IdentifierStrategy != IdentifierStrategy.Assigned)
                        {
                            var selectInsertIdSqlQuery = this.SqlDialect.BuildSelectInsertIdSqlQuery(objectInfo);
                            insertSqlQuery = this.DbDriver.Combine(insertSqlQuery, selectInsertIdSqlQuery);
                        }

                        command = this.CreateCommand(insertSqlQuery);
                    }
                    else
                    {
                        //set command parameters values
                        var values = objectInfo.GetInsertValues(instance);
                        this.DbDriver.SetCommandParametersValues(command, values);
                    }

                    object identifier = null;

                    if (objectInfo.TableInfo.IdentifierStrategy != IdentifierStrategy.Assigned)
                    {
                        identifier = this.ExecuteScalarQuery<object>(command);
                    }
                    else
                    {
                        this.ExecuteQuery(command);
                    }

                    for (int i = this.insertListeners.Count - 1; i >= 0; i--)
                    {
                        this.insertListeners[i].AfterInsert(instance, identifier);
                    }
                }
            }
            finally
            {
                this.CommandCompleted();

                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
            }
        }

        public bool Update(object instance)
        {
            this.ThrowIfDisposed();

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            for (int i = 0; i < this.updateListeners.Count; i++)
            {
                this.updateListeners[i].BeforeUpdate(instance);
            }

            var objectInfo = ObjectInfo.For(instance.GetType());

            if (objectInfo.HasDefaultIdentifierValue(instance))
            {
                throw new MicroLiteException(ExceptionMessages.Session_IdentifierNotSetForUpdate);
            }

            var sqlQuery = this.SqlDialect.BuildUpdateSqlQuery(objectInfo, instance);

            var rowsAffected = this.ExecuteQuery(sqlQuery);

            for (int i = this.updateListeners.Count - 1; i >= 0; i--)
            {
                this.updateListeners[i].AfterUpdate(instance, rowsAffected);
            }

            return rowsAffected == 1;
        }

        public int Update<T>(IList<T> instances)
        {
            this.ThrowIfDisposed();

            if (instances == null)
            {
                throw new ArgumentNullException("instances");
            }

            if (instances.Count == 0)
            {
                return 0;
            }

            var objectInfo = ObjectInfo.For(typeof(T));

            int allRowsAffected = 0;
            IDbCommand command = null;

            try
            {
                foreach(var instance in instances)
                {
                    for (int i = 0; i < this.updateListeners.Count; i++)
                    {
                        this.updateListeners[i].BeforeUpdate(instance);
                    }

                    if (objectInfo.HasDefaultIdentifierValue(instance))
                    {
                        throw new MicroLiteException(ExceptionMessages.Session_IdentifierNotSetForUpdate);
                    }

                    if (command == null)
                    {
                        //initialize IDbCommand in the first iteration
                        var sqlQuery = this.SqlDialect.BuildUpdateSqlQuery(objectInfo, instance);
                        command = this.CreateCommand(sqlQuery);
                    }
                    else
                    {
                        //set command parameters values
                        var values = objectInfo.GetUpdateValues(instance);
                        this.DbDriver.SetCommandParametersValues(command, values);
                    }

                    int rowsAffected = this.ExecuteQuery(command);

                    for (int i = this.updateListeners.Count - 1; i >= 0; i--)
                    {
                        this.updateListeners[i].AfterUpdate(instance, rowsAffected);
                    }

                    allRowsAffected += rowsAffected;
                }
            }
            finally
            {
                this.CommandCompleted();

                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
            }

            return allRowsAffected;
        }

        public bool Update(ObjectDelta objectDelta)
        {
            this.ThrowIfDisposed();

            if (objectDelta == null)
            {
                throw new ArgumentNullException("objectDelta");
            }

            if (objectDelta.ChangeCount == 0)
            {
                throw new MicroLiteException(ExceptionMessages.ObjectDelta_MustContainAtLeastOneChange);
            }

            var sqlQuery = this.SqlDialect.BuildUpdateSqlQuery(objectDelta);

            var rowsAffected = this.ExecuteQuery(sqlQuery);

            return rowsAffected == 1;
        }

        private int ExecuteQuery(SqlQuery sqlQuery)
        {
            try
            {
                using (var command = this.CreateCommand(sqlQuery))
                {
                    var result = command.ExecuteNonQuery();

                    this.CommandCompleted();

                    return result;
                }
            }
            catch (MicroLiteException)
            {
                // Don't re-wrap MicroLite exceptions
                throw;
            }
            catch (Exception e)
            {
                throw new MicroLiteException(e.Message, e);
            }
        }

        private int ExecuteQuery(IDbCommand command)
        {
            try
            {
                return command.ExecuteNonQuery();
            }
            catch(MicroLiteException)
            {
                // Don't re-wrap MicroLite exceptions
                throw;
            }
            catch(Exception e)
            {
                throw new MicroLiteException(e.Message, e);
            }
        }

        private T ExecuteScalarQuery<T>(SqlQuery sqlQuery)
        {
            try
            {
                using (var command = this.CreateCommand(sqlQuery))
                {
                    var result = command.ExecuteScalar();

                    this.CommandCompleted();

                    var resultType = typeof(T);
                    var typeConverter = TypeConverter.For(resultType) ?? TypeConverter.Default;
                    var converted = (T)typeConverter.ConvertFromDbValue(result, resultType);

                    return converted;
                }
            }
            catch (MicroLiteException)
            {
                // Don't re-wrap MicroLite exceptions
                throw;
            }
            catch (Exception e)
            {
                throw new MicroLiteException(e.Message, e);
            }
        }

        private T ExecuteScalarQuery<T>(IDbCommand command)
        {
            try
            {
                var result = command.ExecuteScalar();

                var resultType = typeof(T);
                var typeConverter = TypeConverter.For(resultType) ?? TypeConverter.Default;
                var converted = (T)typeConverter.ConvertFromDbValue(result, resultType);

                return converted;
            }
            catch (MicroLiteException)
            {
                // Don't re-wrap MicroLite exceptions
                throw;
            }
            catch (Exception e)
            {
                throw new MicroLiteException(e.Message, e);
            }
        }

        private object InsertReturningIdentifier(IObjectInfo objectInfo, object instance)
        {
            object identifier = null;

            SqlQuery insertSqlQuery = this.SqlDialect.BuildInsertSqlQuery(objectInfo, instance);

            if (this.SqlDialect.SupportsSelectInsertedIdentifier
                && objectInfo.TableInfo.IdentifierStrategy != IdentifierStrategy.Assigned)
            {
                var selectInsertIdSqlQuery = this.SqlDialect.BuildSelectInsertIdSqlQuery(objectInfo);

                if (this.DbDriver.SupportsBatchedQueries)
                {
                    var combined = this.DbDriver.Combine(insertSqlQuery, selectInsertIdSqlQuery);
                    identifier = this.ExecuteScalarQuery<object>(combined);
                }
                else
                {
                    this.ExecuteQuery(insertSqlQuery);
                    identifier = this.ExecuteScalarQuery<object>(selectInsertIdSqlQuery);
                }
            }
            else if (objectInfo.TableInfo.IdentifierStrategy != IdentifierStrategy.Assigned)
            {
                identifier = this.ExecuteScalarQuery<object>(insertSqlQuery);
            }
            else
            {
                this.ExecuteQuery(insertSqlQuery);
            }

            return identifier;
        }
    }
}