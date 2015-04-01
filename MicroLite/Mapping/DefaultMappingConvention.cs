// -----------------------------------------------------------------------
// <copyright file="DefaultMappingConvention.cs" company="MicroLite">
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
namespace MicroLite.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using MicroLite.FrameworkExtensions;
    using MicroLite.Logging;
    using MicroLite.TypeConverters;

    /// <summary>
    /// The implementation of <see cref="IMappingConvention"/> which uses custom attributes to map tables and columns to types and properties.
    /// </summary>
    internal sealed class DefaultMappingConvention : IMappingConvention
    {
        private readonly ILog log = LogManager.GetCurrentClassLog();

        public IObjectInfo CreateObjectInfo(Type forType)
        {
            if(forType == null)
            {
                throw new ArgumentNullException("forType");
            }

            var tableAttribute = forType.GetAttribute<TableAttribute>(inherit: false);

            if(tableAttribute == null)
            {
                throw new MappingException(ExceptionMessages.DefaultMappingConvention_NoTableAttribute.FormatWith(forType.FullName));
            }

            var identifierStrategy = IdentifierStrategy.DbGenerated;
            var columns = this.CreateColumnInfos(forType, ref identifierStrategy);

            var tableInfo = new TableInfo(columns, identifierStrategy, tableAttribute.Name, tableAttribute.Schema);

            if(this.log.IsDebug)
            {
                this.log.Debug(LogMessages.MappingConvention_MappingTypeToTable, forType.FullName, tableInfo.Schema, tableInfo.Name);
            }

            return new PocoObjectInfo(forType, tableInfo);
        }

        private List<ColumnInfo> CreateColumnInfos(Type forType, ref IdentifierStrategy identifierStrategy)
        {
            var properties = forType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var columns = new List<ColumnInfo>(properties.Length);

            foreach(var property in properties.OrderBy(p => p.Name))
            {
                if(!property.CanRead || !property.CanWrite)
                {
                    if(this.log.IsDebug)
                    {
                        this.log.Debug(LogMessages.MappingConvention_PropertyNotGetAndSet, forType.Name, property.Name);
                    }

                    continue;
                }

                var ignoreAttribute = property.GetAttribute<IgnoreColumnAttribute>(inherit: true);

                if(ignoreAttribute != null)
                {
                    if(this.log.IsDebug)
                    {
                        this.log.Debug(LogMessages.DefaultMappingConvention_IgnoringProperty, forType.Name, property.Name);
                    }

                    continue;
                }

                var identifierAttribute = property.GetAttribute<IdentifierAttribute>(inherit: true);

                if(identifierAttribute != null)
                {
                    identifierStrategy = identifierAttribute.IdentifierStrategy;
                }

                var columnAttribute = property.GetAttribute<ColumnAttribute>(inherit: true);

                ColumnInfo columnInfo;
                if(columnAttribute != null)
                {
                    columnInfo = new ColumnInfo(
                        columnName: columnAttribute.Name,
                        dbType: columnAttribute.DbType ?? TypeConverter.ResolveDbType(property.PropertyType),
                        propertyInfo: property,
                        isIdentifier: identifierAttribute != null,
                        allowInsert: identifierAttribute != null ? identifierStrategy == IdentifierStrategy.Assigned : columnAttribute.AllowInsert,
                        allowUpdate: identifierAttribute != null ? false : columnAttribute.AllowUpdate,
                        sequenceName: identifierAttribute != null ? identifierAttribute.SequenceName : null);
                }
                else
                {
                    columnInfo = new ColumnInfo(
                        columnName: property.Name,
                        dbType: TypeConverter.ResolveDbType(property.PropertyType),
                        propertyInfo: property,
                        isIdentifier: identifierAttribute != null,
                        allowInsert: identifierAttribute != null ? identifierStrategy == IdentifierStrategy.Assigned : true,
                        allowUpdate: identifierAttribute != null ? false : true,
                        sequenceName: identifierAttribute != null && identifierStrategy == IdentifierStrategy.Sequence ? identifierAttribute.SequenceName : null);
                }

                if(this.log.IsDebug)
                {
                    this.log.Debug(LogMessages.MappingConvention_MappingColumnToProperty, forType.Name, columnInfo.PropertyInfo.Name, columnInfo.ColumnName);
                }

                columns.Add(columnInfo);
            }

            return columns;
        }
    }
}
