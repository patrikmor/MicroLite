// -----------------------------------------------------------------------
// <copyright file="SybaseSqlDialect.cs" company="MicroLite">
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
    using MicroLite.Mapping;

    /// <summary>
    /// The implementation of <see cref="ISqlDialect"/> for Sybase.
    /// </summary>
    internal sealed class SybaseSqlDialect : SqlDialect
    {
        private static readonly SqlQuery selectIdentityQuery = new SqlQuery("SELECT @@IDENTITY");

        /// <summary>
        /// Initialises a new instance of the <see cref="SybaseSqlDialect"/> class.
        /// </summary>
        internal SybaseSqlDialect()
            : base(SybaseSqlCharacters.Instance)
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
            throw new System.NotImplementedException();
        }
    }
}