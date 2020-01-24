using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace People.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EncryptAndDecrypt()
        {
            //Arrange
            string originalName = "Joao";
            string password = Encoding.ASCII.GetString(CryptoHelper.GenerateBitsOfRandomEntropy(16));

            //Act

            string encryptedName = CryptoHelper.Encrypt(originalName, password, 16);
            string decryptedName = CryptoHelper.Decrypt(encryptedName, password, 16);

            //Assert
            Assert.AreEqual(originalName, decryptedName);
        }
    }
}
