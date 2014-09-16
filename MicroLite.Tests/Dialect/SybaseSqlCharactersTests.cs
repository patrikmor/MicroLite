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
    }
}