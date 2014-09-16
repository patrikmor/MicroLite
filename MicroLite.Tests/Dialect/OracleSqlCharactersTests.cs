namespace MicroLite.Tests.Dialect
{
    using MicroLite.Dialect;
    using Xunit;

    /// <summary>
    /// Unit Tests for the <see cref="OracleSqlCharacters"/> class.
    /// </summary>
    public class OracleSqlCharactersTests
    {
        [Fact]
        public void InstanceReturnsTheSameInstanceEachTime()
        {
            var characters1 = OracleSqlCharacters.Instance;
            var characters2 = OracleSqlCharacters.Instance;

            Assert.Same(characters1, characters2);
        }

        [Fact]
        public void LeftDelimiterReturnsCorrectValue()
        {
            Assert.Equal("\"", OracleSqlCharacters.Instance.LeftDelimiter);
        }

        [Fact]
        public void RightDelimiterReturnsCorrectValue()
        {
            Assert.Equal("\"", OracleSqlCharacters.Instance.RightDelimiter);
        }

        [Fact]
        public void SqlParameterReturnsColon()
        {
            Assert.Equal(":", OracleSqlCharacters.Instance.SqlParameter);
        }

        [Fact]
        public void StoredProcedureInvocationCommandReturnsCall()
        {
            Assert.Equal("CALL", OracleSqlCharacters.Instance.StoredProcedureInvocationCommand);
        }

        [Fact]
        public void SupportsNamedParametersReturnsTrue()
        {
            Assert.True(OracleSqlCharacters.Instance.SupportsNamedParameters);
        }
    }
}