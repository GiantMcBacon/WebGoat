using NuGet.ContentModel;
using WebGoat.NET.Domain_primitives;
using Xunit.Sdk;

namespace WebGoatTestProjekt
{
    public class PasswordTest
    {
        // Tester input valideringen af passwordet - tester at passwordet er gyldigt
        [Theory]
        [InlineData("test0123456789")]
        [InlineData("Password!123")]
        public void PasswordIsValidTest(string validPassword)
        {
            //Act
            Password password = new Password(validPassword);

            //Assert
            Assert.Equal(validPassword, password.GetValue());
        }

        // Tester input valideringen af password - tester at den smider en exception ved ugydligt password
        [Theory]
        [InlineData("test")]
        [InlineData("DetHerErEtMegetLangPasswordSomOverskriderMax")]
        [InlineData("UgyldgigtPassword#;")]
        public void PasswordIsIndvalid_throwsException(string invalidPassword)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Password(invalidPassword));
        }
    }
}