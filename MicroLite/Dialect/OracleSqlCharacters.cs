// -----------------------------------------------------------------------
// <copyright file="OracleSqlCharacters.cs" company="MicroLite">
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
    /// <summary>
    /// An implementation of SqlCharacters for Oracle.
    /// </summary>
    internal sealed class OracleSqlCharacters : SqlCharacters
    {
        /// <summary>
        /// The single instance of SqlCharacters for Oracle.
        /// </summary>
        internal static readonly SqlCharacters Instance = new OracleSqlCharacters();

        /// <summary>
        /// Prevents a default instance of the <see cref="OracleSqlCharacters"/> class from being created.
        /// </summary>
        private OracleSqlCharacters()
        {
        }
    }
}