namespace MicroLite.Tests.Dialect
{
    using MicroLite.Dialect;
    using Xunit;

    /// <summary>
    /// Unit Tests for the <see cref="SybaseSqlCharacters"/> class.
    /// </summary>
    public class SybaseSqlCharactersTests
    {
        [Fact]
        public void InstanceReturnsTheSameInstanceEachTime()
        {
            var characters1 = SybaseSqlCharacters.Instance;
            var characters2 = SybaseSqlCharacters.Instance;

            Assert.Same(characters1, characters2);
        }

        [Fact]
        public void LeftDelimiterReturnsCorrectValue()
        {
            Assert.Equal("\"", SybaseSqlCharacters.Instance.LeftDelimiter);
        }

        [Fact]
        public void RightDelimiterReturnsCorrectValue()
        {
            Assert.Equal("\"", SybaseSqlCharacters.Instance.RightDelimiter);
        }

        [Fact]
        public void SqlParameterReturnsColon()
        {
            Assert.Equal(":", SybaseSqlCharacters.Instance.SqlParameter);
        }

        [Fact]
        public void StoredProcedureInvocationCommandReturnsExec()
        {
            Assert.Equal("EXEC", SybaseSqlCharacters.Instance.StoredProcedureInvocationCommand);
        }

        [Fact]
        public void SupportsNamedParametersReturnsTrue()
        {
            Assert.True(SybaseSqlCharacters.Instance.SupportsNamedParameters);
        }
    }
}