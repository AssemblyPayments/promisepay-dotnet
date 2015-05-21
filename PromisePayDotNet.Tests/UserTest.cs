using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Implementations;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class UserTest
    {
        public string GetUserByIdResponse =
            "{\"created_at\":\"2015-05-18T06:50:51.684Z\",\"updated_at\":\"2015-05-18T11:36:14.050Z\",\"full_name\":\"Igor Sidorov\",\"email\":\"idsidorov@gmail.com\",\"mobile\":null,\"phone\":null,\"first_name\":\"Igor\",\"last_name\":\"Sidorov\",\"id\":\"ef831cd65790e232f6e8c316d6a2ce50\",\"verification_state\":\"pending\",\"held_state\":false,\"dob\":\"Not provided.\",\"government_number\":\"Not provided.\",\"drivers_license\":\"Not provided.\",\"related\":{\"addresses\":\"f08a5f8a-698f-41cf-ac2e-7d5cc52eb915\",\"companies\":\"e466dfb4-f05c-4c7f-92a3-09a0a28c7af5\"},\"links\":{\"self\":\"/users/ef831cd65790e232f6e8c316d6a2ce50\",\"items\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/items\",\"card_accounts\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/card_accounts\",\"paypal_accounts\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/paypal_accounts\",\"bank_accounts\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/bank_accounts\"}}";

        [TestMethod]
        public void TestUserDaoObject()
        {
            var user = JsonConvert.DeserializeObject<User>(GetUserByIdResponse);
            Assert.AreEqual("Igor Sidorov", user.FullName);
            Assert.IsTrue(user.CreatedAt.HasValue);
            Assert.IsTrue(user.UpdatedAt.HasValue);
        }

        [TestMethod]
        public void TestCreateUserMethod()
        {
            var repo = new UserRepository();
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                ID = id,
                FirstName = "Test",
                LastName = "Test",
                City = "Test",
                AddressLine1 = "Line 1",
                Country = "AUS",
                State = "state",
                Zip = "123456",
                Email = id + "@google.com"
            };

            var createdUser = repo.CreateUser(user);

            Assert.AreEqual(user.ID, createdUser.ID);
            Assert.AreEqual(user.FirstName, createdUser.FirstName);
            Assert.AreEqual(user.LastName, createdUser.LastName);
            Assert.AreEqual("Test Test", createdUser.FullName);
            Assert.AreEqual(user.Email, createdUser.Email);
            Assert.IsTrue(createdUser.CreatedAt.HasValue);
            Assert.IsTrue(createdUser.UpdatedAt.HasValue);
            
        }
    }
}
