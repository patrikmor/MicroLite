// -----------------------------------------------------------------------
// <copyright file="AsyncSession.cs" company="MicroLite">
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
#if NET_4_5

    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using MicroLite.Dialect;
    using MicroLite.Driver;
    using MicroLite.Listeners;
    using MicroLite.Mapping;
    using MicroLite.TypeConverters;
    using System.Data;

    /// <summary>
    /// The default implementation of <see cref="IAsyncSession"/>.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ConnectionScope: {ConnectionScope}")]
    internal sealed class AsyncSession : AsyncReadOnlySession, IAsyncSession
    {
        private readonly IList<IDeleteListener> deleteListeners;
        private readonly IList<IInsertListener> insertListeners;
        private readonly IList<IUpdateListener> updateListeners;

        internal AsyncSession(
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

        public Task<bool> DeleteAsync(object instance)
        {
            return this.DeleteAsync(instance, CancellationToken.None);
        }

        public async Task<bool> DeleteAsync(object instance, CancellationToken cancellationToken)
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

            var rowsAffected = await this.ExecuteQueryAsync(sqlQuery, cancellationToken).ConfigureAwait(false);

            for (int i = this.deleteListeners.Count - 1; i >= 0; i--)
            {
                this.deleteListeners[i].AfterDelete(instance, rowsAffected);
            }

            return rowsAffected == 1;
        }

        public Task<int> DeleteAsync<T>(IList<T> instances)
        {
            return this.DeleteAsync<T>(instances, CancellationToken.None);
        }

        public async Task<int> DeleteAsync<T>(IList<T> instances, CancellationToken cancellationToken)
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
            DbCommand command = null;

            try
            {
                foreach (var instance in instances)
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
                        command = (DbCommand)this.CreateCommand(sqlQuery);
                    }
                    else
                    {
                        //set command parameter value
                        this.DbDriver.SetCommandParameterValue(command, 0, identifier);
                    }

                    int rowsAffected = await this.ExecuteQueryAsync(command, cancellationToken).ConfigureAwait(false);

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

        public Task<bool> DeleteAsync(Type type, object identifier)
        {
            return this.DeleteAsync(type, identifier, CancellationToken.None);
        }

        public async Task<bool> DeleteAsync(Type type, object identifier, CancellationToken cancellationToken)
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

            var rowsAffected = await this.ExecuteQueryAsync(sqlQuery, cancellationToken).ConfigureAwait(false);

            return rowsAffected == 1;
        }

        public Task<int> ExecuteAsync(SqlQuery sqlQuery)
        {
            return this.ExecuteAsync(sqlQuery, CancellationToken.None);
        }

        public async Task<int> ExecuteAsync(SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed();

            if (sqlQuery == null)
            {
                throw new ArgumentNullException("sqlQuery");
            }

            return await this.ExecuteQueryAsync(sqlQuery, cancellationToken).ConfigureAwait(false);
        }

        public Task<T> ExecuteScalarAsync<T>(SqlQuery sqlQuery)
        {
            return this.ExecuteScalarAsync<T>(sqlQuery, CancellationToken.None);
        }

        public async Task<T> ExecuteScalarAsync<T>(SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed();

            if (sqlQuery == null)
            {
                throw new ArgumentNullException("sqlQuery");
            }

            return await this.ExecuteScalarQueryAsync<T>(sqlQuery, cancellationToken).ConfigureAwait(false);
        }

        public Task InsertAsync(object instance)
        {
            return this.InsertAsync(instance, CancellationToken.None);
        }

        public async Task InsertAsync(object instance, CancellationToken cancellationToken)
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

            object identifier = await this.InsertReturningIdentifierAsync(objectInfo, instance, cancellationToken).ConfigureAwait(false);

            for (int i = this.insertListeners.Count - 1; i >= 0; i--)
            {
                this.insertListeners[i].AfterInsert(instance, identifier);
            }
        }

        public Task InsertAsync<T>(IList<T> instances)
        {
            return this.InsertAsync<T>(instances, CancellationToken.None);
        }

        public async Task InsertAsync<T>(IList<T> instances, CancellationToken cancellationToken)
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

            DbCommand command = null;

            try
            {
                foreach (var instance in instances)
                {
                    for (int i = 0; i < this.insertListeners.Count; i++)
                    {
                        this.insertListeners[i].BeforeInsert(instance);
                    }

                    objectInfo.VerifyInstanceForInsert(instance);

                    if (command == null)
                    {
                        //initialize IDbCommand in the first iteration
                        var insertSqlQuery = this.SqlDialect.BuildInsertSqlQuery(objectInfo, instance);

                        if (this.SqlDialect.SupportsSelectInsertedIdentifier
                            && objectInfo.TableInfo.IdentifierStrategy != IdentifierStrategy.Assigned)
                        {
                            var selectInsertIdSqlQuery = this.SqlDialect.BuildSelectInsertIdSqlQuery(objectInfo);
                            insertSqlQuery = this.DbDriver.Combine(insertSqlQuery, selectInsertIdSqlQuery);
                        }

                        command = (DbCommand)this.CreateCommand(insertSqlQuery);
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
                        identifier = await this.ExecuteScalarQueryAsync<object>(command, cancellationToken).ConfigureAwait(false);
                    }
                    else
                    {
                        await this.ExecuteQueryAsync(command, cancellationToken).ConfigureAwait(false);
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

        public Task<bool> UpdateAsync(object instance)
        {
            return this.UpdateAsync(instance, CancellationToken.None);
        }

        public async Task<bool> UpdateAsync(object instance, CancellationToken cancellationToken)
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

            var rowsAffected = await this.ExecuteQueryAsync(sqlQuery, cancellationToken).ConfigureAwait(false);

            for (int i = this.updateListeners.Count - 1; i >= 0; i--)
            {
                this.updateListeners[i].AfterUpdate(instance, rowsAffected);
            }

            return rowsAffected == 1;
        }

        public Task<int> UpdateAsync<T>(IList<T> instances)
        {
            return this.UpdateAsync<T>(instances, CancellationToken.None);
        }

        public async Task<int> UpdateAsync<T>(IList<T> instances, CancellationToken cancellationToken)
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
            DbCommand command = null;

            try
            {
                foreach (var instance in instances)
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
                        command = (DbCommand)this.CreateCommand(sqlQuery);
                    }
                    else
                    {
                        //set command parameters values
                        var values = objectInfo.GetUpdateValues(instance);
                        this.DbDriver.SetCommandParametersValues(command, values);
                    }

                    int rowsAffected = await this.ExecuteQueryAsync(command, cancellationToken).ConfigureAwait(false);

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

        public Task<bool> UpdateAsync(ObjectDelta objectDelta)
        {
            return this.UpdateAsync(objectDelta, CancellationToken.None);
        }

        public async Task<bool> UpdateAsync(ObjectDelta objectDelta, CancellationToken cancellationToken)
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

            var rowsAffected = await this.ExecuteQueryAsync(sqlQuery, cancellationToken).ConfigureAwait(false);

            return rowsAffected == 1;
        }

        private async Task<int> ExecuteQueryAsync(SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            try
            {
                using (var command = (DbCommand)this.CreateCommand(sqlQuery))
                {
                    var result = await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);

                    this.CommandCompleted();

                    return result;
                }
            }
            catch (OperationCanceledException)
            {
                // Don't re-wrap Operation Canceled exceptions
                throw;
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

        private async Task<int> ExecuteQueryAsync(DbCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Don't re-wrap Operation Canceled exceptions
                throw;
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

        private async Task<T> ExecuteScalarQueryAsync<T>(SqlQuery sqlQuery, CancellationToken cancellationToken)
        {
            try
            {
                using (var command = (DbCommand)this.CreateCommand(sqlQuery))
                {
                    var result = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                    this.CommandCompleted();

                    var resultType = typeof(T);
                    var typeConverter = TypeConverter.For(resultType) ?? TypeConverter.Default;
                    var converted = (T)typeConverter.ConvertFromDbValue(result, resultType);

                    return converted;
                }
            }
            catch (OperationCanceledException)
            {
                // Don't re-wrap Operation Canceled exceptions
                throw;
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

        private async Task<T> ExecuteScalarQueryAsync<T>(DbCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                var resultType = typeof(T);
                var typeConverter = TypeConverter.For(resultType) ?? TypeConverter.Default;
                var converted = (T)typeConverter.ConvertFromDbValue(result, resultType);

                return converted;
            }
            catch (OperationCanceledException)
            {
                // Don't re-wrap Operation Canceled exceptions
                throw;
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

        private async Task<object> InsertReturningIdentifierAsync(IObjectInfo objectInfo, object instance, CancellationToken cancellationToken)
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
                    identifier = await this.ExecuteScalarQueryAsync<object>(combined, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    await this.ExecuteQueryAsync(insertSqlQuery, cancellationToken).ConfigureAwait(false);
                    identifier = await this.ExecuteScalarQueryAsync<object>(selectInsertIdSqlQuery, cancellationToken).ConfigureAwait(false);
                }
            }
            else if (objectInfo.TableInfo.IdentifierStrategy != IdentifierStrategy.Assigned)
            {
                identifier = await this.ExecuteScalarQueryAsync<object>(insertSqlQuery, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await this.ExecuteQueryAsync(insertSqlQuery, cancellationToken).ConfigureAwait(false);
            }

            return identifier;
        }
    }

#endif
}