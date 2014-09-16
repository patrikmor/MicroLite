// -----------------------------------------------------------------------
// <copyright file="OracleDbDriver.cs" company="MicroLite">
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
namespace MicroLite.Driver
{
    using System;
    using System.Data;
    using System.Reflection;

    /// <summary>
    /// The implementation of <see cref="IDbDriver"/> for Oracle.
    /// </summary>
    internal sealed class OracleDbDriver : DbDriver
    {
        private static PropertyInfo bindByNameProperty;

        /// <summary>
        /// Initialises a new instance of the <see cref="OracleDbDriver" /> class.
        /// </summary>
        internal OracleDbDriver()
        {
        }

        protected override IDbCommand CreateCommand(SqlQuery sqlQuery)
        {
            var command = base.CreateCommand(sqlQuery);

            SetBindByName(command);

            return command;
        }

        protected override string GetCommandText(string commandText)
        {
            if (commandText.StartsWith("CALL", StringComparison.OrdinalIgnoreCase))
            {
                var openParenthesisPosition = commandText.IndexOf('(');

                if (openParenthesisPosition > 4)
                {
                    return commandText.Substring(4, openParenthesisPosition - 4).Trim();
                }
                else
                {
                    return commandText.Substring(4, commandText.Length - 4).Trim();
                }
            }

            return base.GetCommandText(commandText);
        }

        protected override CommandType GetCommandType(string commandText)
        {
            if (commandText.StartsWith("CALL", StringComparison.OrdinalIgnoreCase))
            {
                return CommandType.StoredProcedure;
            }

            return base.GetCommandType(commandText);
        }

        private static void SetBindByName(IDbCommand command)
        {
            if (bindByNameProperty == null)
            {
                bindByNameProperty = command.GetType().GetProperty("BindByName");
            }

            if (bindByNameProperty != null)
            {
                bindByNameProperty.SetValue(command, true, null);
            }
        }
    }
}