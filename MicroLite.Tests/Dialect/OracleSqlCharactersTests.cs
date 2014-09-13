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
    }
}