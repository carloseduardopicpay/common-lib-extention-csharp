
using Xunit;

namespace Test
{
    public class TemplateTest

    {     

        [Fact]
        public void GetListTest()
        {

            // Arrange
            var mock = 1;

            // Act
            var result = mock;

            // Assert
            Assert.NotNull(result);
        }
    }
}