// -----------------------------------------------------------------------
// <copyright file="SybaseSqlAnywhereDialect.cs" company="MicroLite">
// Copyright 2012 - 2014 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace MicroLite.Dialect
{
    using System;
    using System.Globalization;
    using System.Text;
    using MicroLite.Mapping;

    /// <summary>
    /// The implementation of <see cref="ISqlDialect"/> for Sybase.
    /// </summary>
    internal sealed class SybaseSqlAnywhereDialect : SqlDialect
    {
        private static readonly SqlQuery selectIdentityQuery = new SqlQuery("SELECT @@IDENTITY");

        /// <summary>
        /// Initialises a new instance of the <see cref="SybaseSqlAnywhereDialect"/> class.
        /// </summary>
        internal SybaseSqlAnywhereDialect()
            : base(SybaseSqlAnywhereCharacters.Instance)
        {
        }

        public override bool SupportsSelectInsertedIdentifier
        {
            get
            {
                return true;
            }
        }

        public override SqlQuery BuildSelectInsertIdSqlQuery(IObjectInfo objectInfo)
        {
            return selectIdentityQuery;
        }

        public override SqlQuery PageQuery(SqlQuery sqlQuery, PagingOptions pagingOptions)
        {
            if (sqlQuery == null)
            {
                throw new ArgumentNullException("sqlQuery");
            }

            var stringBuilder = new StringBuilder()
                .AppendFormat("SELECT TOP {0} START AT {1} ", pagingOptions.Count.ToString(CultureInfo.InvariantCulture), pagingOptions.Offset.ToString(CultureInfo.InvariantCulture))
                .Append(sqlQuery.CommandText.Replace(Environment.NewLine, string.Empty).Substring(7));

            return new SqlQuery(stringBuilder.ToString(), sqlQuery.ArgumentsArray);
        }

        protected override string BuildInsertCommandText(IObjectInfo objectInfo)
        {
            var commandText = base.BuildInsertCommandText(objectInfo);

            if (objectInfo.TableInfo.IdentifierStrategy == IdentifierStrategy.Sequence)
            {
                var firstParenthesisIndex = commandText.IndexOf('(') + 1;

                commandText = commandText.Insert(
                    firstParenthesisIndex,
                    this.SqlCharacters.EscapeSql(objectInfo.TableInfo.IdentifierColumn.ColumnName) + ",");

                var secondParenthesisIndex = commandText.IndexOf('(', firstParenthesisIndex) + 1;

                commandText = commandText.Insert(
                    secondParenthesisIndex,
                    objectInfo.TableInfo.IdentifierColumn.SequenceName + ".NEXTVAL,");
            }

            return commandText;
        }
    }
}