namespace MicroLite.Tests.Driver
{
    using System.Data;
    using System.Data.Common;
    using MicroLite.Driver;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit Tests for the <see cref="OracleDbDriver"/> class.
    /// </summary>
    public class OracleDbDriverTests : UnitTest
    {
        [Fact]
        public void BuildCommandForSqlQueryWithStoredProcedureWithoutParameters()
        {
            var sqlQuery = new SqlQuery("CALL GetTableContents()");

            var mockDbProviderFactory = new Mock<DbProviderFactory>();
            mockDbProviderFactory.Setup(x => x.CreateCommand()).Returns(new System.Data.SqlClient.SqlCommand());

            var dbDriver = new OracleDbDriver();
            dbDriver.DbProviderFactory = mockDbProviderFactory.Object;

            var command = dbDriver.BuildCommand(sqlQuery);

            // The command text should only contain the stored procedure name.
            Assert.Equal("GetTableContents", command.CommandText);
            Assert.Equal(CommandType.StoredProcedure, command.CommandType);
            Assert.Equal(0, command.Parameters.Count);
        }

        [Fact]
        public void BuildCommandForSqlQueryWithStoredProcedureWithParameters()
        {
            var sqlQuery = new SqlQuery(
                "CALL GetTableContents(identifier, cust_name)",
                100, "hello");

            var mockDbProviderFactory = new Mock<DbProviderFactory>();
            mockDbProviderFactory.Setup(x => x.CreateCommand()).Returns(new System.Data.SqlClient.SqlCommand());

            var dbDriver = new OracleDbDriver();
            dbDriver.DbProviderFactory = mockDbProviderFactory.Object;

            var command = dbDriver.BuildCommand(sqlQuery);

            // The command text should only contain the stored procedure name.
            Assert.Equal("GetTableContents", command.CommandText);
            Assert.Equal(CommandType.StoredProcedure, command.CommandType);
            Assert.Equal(2, command.Parameters.Count);

            var parameter1 = (IDataParameter)command.Parameters[0];
            Assert.Equal("identifier", parameter1.ParameterName);
            Assert.Equal(sqlQuery.Arguments[0], parameter1.Value);

            var parameter2 = (IDataParameter)command.Parameters[1];
            Assert.Equal("cust_name", parameter2.ParameterName);
            Assert.Equal(sqlQuery.Arguments[1], parameter2.Value);
        }
    }
}