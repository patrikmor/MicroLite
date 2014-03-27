﻿// -----------------------------------------------------------------------
// <copyright file="XDocumentTypeConverter.cs" company="MicroLite">
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
namespace MicroLite.TypeConverters
{
    using System;
    using System.Data;
    using System.IO;
    using System.Xml.Linq;

    /// <summary>
    /// An ITypeConverter which can convert an XDocument to and from the stored database value of either an xml or string column.
    /// </summary>
    public sealed class XDocumentTypeConverter : ITypeConverter
    {
        /// <summary>
        /// Determines whether this type converter can convert values for the specified type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>
        ///   <c>true</c> if this instance can convert the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanConvert(Type type)
        {
            return type == typeof(XDocument);
        }

        /// <summary>
        /// Converts the specified database value into an instance of the specified type.
        /// </summary>
        /// <param name="value">The database value to be converted.</param>
        /// <param name="type">The type to convert to.</param>
        /// <returns>An instance of the specified type containing the specified value.</returns>
        public object ConvertFromDbValue(object value, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            var document = XDocument.Parse(value.ToString());

            return document;
        }

        /// <summary>
        /// Converts value at the specified index in the IDataReader into an instance of the specified type.
        /// </summary>
        /// <param name="reader">The IDataReader containing the results.</param>
        /// <param name="index">The index of the record to read from the IDataReader.</param>
        /// <param name="type">The type to convert result value to.</param>
        /// <returns>An instance of the specified type containing the specified value.</returns>
        public object ConvertFromDbValue(IDataReader reader, int index, Type type)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (reader.IsDBNull(index))
            {
                return null;
            }

            var value = reader.GetString(index);
            var document = XDocument.Parse(value);

            return document;

            ////var buffer = new byte[8040];

            ////using (var stream = new MemoryStream(buffer))
            ////{
            ////    long offset = 0;
            ////    long read;
            ////    while ((read = reader.GetBytes(index, offset, buffer, 0, buffer.Length)) > 0)
            ////    {
            ////        offset += read;
            ////        stream.Write(buffer, 0, (int)read); // push downstream
            ////    }

            ////    var document = XDocument.Load(stream);

            ////    return document;
            ////}
        }

        /// <summary>
        /// Converts the specified value into an instance of the database value.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="type">The type to convert from.</param>
        /// <returns>An instance of the corresponding database type containing the value.</returns>
        public object ConvertToDbValue(object value, Type type)
        {
            if (value == null)
            {
                return value;
            }

            var document = (XDocument)value;

            var xml = document.ToString(SaveOptions.DisableFormatting);

            return xml;
        }
    }
}