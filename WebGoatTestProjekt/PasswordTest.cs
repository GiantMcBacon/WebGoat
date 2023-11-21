using NuGet.ContentModel;
using WebGoat.NET.Domain_primitives;
using Xunit.Sdk;

namespace WebGoatTestProjekt
{
    public class UsernameTest
    {
        // Tester input valideringen af brugernavn - tester at brugernavnet er gyldigt
        [Theory]
        [InlineData("test123")]
        [InlineData("DetteErEtBrugernavn")]
        public void UsernameIsValidTest(string validUsername)
        {
            //Act
            Username username = new Username(validUsername);

            //Assert
            Assert.Equal(validUsername, username.GetValue());
        }

        // Tester input valideringen af brugernavn - tester at den smider en exception ved ugydligt brugernavn
        [Theory]
        [InlineData("test")]
        [InlineData("DetHerErEtMegetLangBrugernavnSomOverskriderMax")]
        [InlineData("UgyldgigtBrugernavn!")]
        public void UsernameIsIndvalid_throwsException(string invalidUsername)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Username(invalidUsername));
        }
    }
}