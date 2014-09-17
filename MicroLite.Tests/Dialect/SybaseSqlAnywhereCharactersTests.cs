namespace MicroLite.Tests.Dialect
{
    using MicroLite.Dialect;
    using Xunit;

    /// <summary>
    /// Unit Tests for the <see cref="SybaseSqlAnywhereCharacters"/> class.
    /// </summary>
    public class SybaseSqlAnywhereCharactersTests
    {
        [Fact]
        public void InstanceReturnsTheSameInstanceEachTime()
        {
            var characters1 = SybaseSqlAnywhereCharacters.Instance;
            var characters2 = SybaseSqlAnywhereCharacters.Instance;

            Assert.Same(characters1, characters2);
        }

        [Fact]
        public void LeftDelimiterReturnsCorrectValue()
        {
            Assert.Equal("\"", SybaseSqlAnywhereCharacters.Instance.LeftDelimiter);
        }

        [Fact]
        public void RightDelimiterReturnsCorrectValue()
        {
            Assert.Equal("\"", SybaseSqlAnywhereCharacters.Instance.RightDelimiter);
        }

        [Fact]
        public void SqlParameterReturnsColon()
        {
            Assert.Equal(":", SybaseSqlAnywhereCharacters.Instance.SqlParameter);
        }

        [Fact]
        public void StoredProcedureInvocationCommandReturnsExec()
        {
            Assert.Equal("EXEC", SybaseSqlAnywhereCharacters.Instance.StoredProcedureInvocationCommand);
        }

        [Fact]
        public void SupportsNamedParametersReturnsTrue()
        {
            Assert.True(SybaseSqlAnywhereCharacters.Instance.SupportsNamedParameters);
        }
    }
}