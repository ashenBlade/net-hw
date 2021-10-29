using Xunit;

namespace HW7.Tests
{
    public class CamelCaseFormatterTests
    {
        private readonly CamelCaseFormatter _formatter;

        public CamelCaseFormatterTests()
        {
            _formatter = new CamelCaseFormatter();
        }

        [Theory]
        [InlineData("name")]
        [InlineData("hello")]
        [InlineData("what")]
        [InlineData("nope")]
        public void FormatName_WithStringWithoutUpperCharacters_ShouldReturnSameString(string name)
        {
            // Arrange
            var expected = name;
            // Act
            var actual = _formatter.FormatName(name);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Name")]
        [InlineData("Hello")]
        [InlineData("What")]
        [InlineData("Nope")]
        public void FormatName_WithStringWithFirstUpperCharacter_ShouldReturnSameString(string name)
        {
            // Arrange
            var expected = name;
            // Act
            var actual = _formatter.FormatName(name);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("FirstName", "First Name")]
        [InlineData("HelloWorld", "Hello World")]
        [InlineData("WhazzUp", "Whazz Up")]
        [InlineData("NoHomoHere", "No Homo Here")]
        public void FormatName_WithMultipleUpperCharacters_ShouldSplitWordsExceptFirstUsingUpperCharacters(
            string name,
            string expected)
        {
            // Act
            var actual = _formatter.FormatName(name);
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}