using Newtonsoft.Json;
using NUnit.Framework;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Implementations;
using System;
using System.Linq;
using RestSharp;

namespace PromisePayDotNet.Tests
{
    public class UserTest
    {
        public string GetUserByIdResponse =
            "{\"created_at\":\"2015-05-18T06:50:51.684Z\",\"updated_at\":\"2015-05-18T11:36:14.050Z\",\"full_name\":\"Igor Sidorov\",\"email\":\"idsidorov@gmail.com\",\"mobile\":null,\"phone\":null,\"first_name\":\"Igor\",\"last_name\":\"Sidorov\",\"id\":\"ef831cd65790e232f6e8c316d6a2ce50\",\"verification_state\":\"pending\",\"held_state\":false,\"dob\":\"Not provided.\",\"government_number\":\"Not provided.\",\"drivers_license\":\"Not provided.\",\"related\":{\"addresses\":\"f08a5f8a-698f-41cf-ac2e-7d5cc52eb915\",\"companies\":\"e466dfb4-f05c-4c7f-92a3-09a0a28c7af5\"},\"links\":{\"self\":\"/users/ef831cd65790e232f6e8c316d6a2ce50\",\"items\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/items\",\"card_accounts\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/card_accounts\",\"paypal_accounts\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/paypal_accounts\",\"bank_accounts\":\"/users/ef831cd65790e232f6e8c316d6a2ce50/bank_accounts\"}}";

        [Test]
        public void TestUserDaoObject()
        {
            var user = JsonConvert.DeserializeObject<User>(GetUserByIdResponse);
            Assert.AreEqual("Igor Sidorov", user.FullName);
            Assert.IsTrue(user.CreatedAt.HasValue);
            Assert.IsTrue(user.UpdatedAt.HasValue);
        }

        [Test]
        public void UserCreateSuccessful()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
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

            Assert.AreEqual(user.Id, createdUser.Id);
            Assert.AreEqual(user.FirstName, createdUser.FirstName);
            Assert.AreEqual(user.LastName, createdUser.LastName);
            Assert.AreEqual("Test Test", createdUser.FullName);
            Assert.AreEqual(user.Email, createdUser.Email);
            Assert.IsTrue(createdUser.CreatedAt.HasValue);
            Assert.IsTrue(createdUser.UpdatedAt.HasValue);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidationErrorUserCreateMissedId()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = null,
                FirstName = "Test",
                LastName = "Test",
                City = "Test",
                AddressLine1 = "Line 1",
                Country = "AUS",
                State = "state",
                Zip = "123456",
                Email = id + "@google.com"
            };
            repo.CreateUser(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidationErrorUserCreateMissedFirstName()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                FirstName = null,
                LastName = "Test",
                City = "Test",
                AddressLine1 = "Line 1",
                Country = "AUS",
                State = "state",
                Zip = "123456",
                Email = id + "@google.com"
            };
            repo.CreateUser(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidationErrorUserCreateWrongCountry()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                FirstName = "Test",
                LastName = "Test",
                City = "Test",
                AddressLine1 = "Line 1",
                Country = "Australia", //Not a correct ISO code
                State = "state",
                Zip = "123456",
                Email = id + "@google.com"
            };
            repo.CreateUser(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ValidationErrorUserCreateWrongEmail()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                FirstName = "Test",
                LastName = "Test",
                City = "Test",
                AddressLine1 = "Line 1",
                Country = "AUS",
                State = "state",
                Zip = "123456",
                Email = id
            };
            repo.CreateUser(user);
        }

        [Test]
        public void ListUsersSuccessful()
        {
            //First, create a user, so we'll have at least one 
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
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

            //Then, list users
            var users = repo.ListUsers(200);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Any(x => x.Id == id));

        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void ListUsersNegativeParams()
        {
            var repo = new UserRepository(new RestClient());
            repo.ListUsers(-10, -20);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ListUsersTooHighLimit()
        {
            var repo = new UserRepository(new RestClient());
            repo.ListUsers(201);
        }


        [Test]
        public void GetUserSuccessful()
        {
            //First, create a user with known id
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
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

            //Then, get user
            var gotUser = repo.GetUserById(id);

            Assert.IsNotNull(gotUser);
            Assert.AreEqual(gotUser.Id, id);
        }

        [Test]
        [ExpectedException(typeof(UnauthorizedException))] 
        //That's bad idea not to distinguish between "wrong login/password" and "There is no such ID in DB"
        public void GetUserMissingId()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            repo.GetUserById(id);
        }

        [Ignore] //Skipped until API method will be fixed
        [Test]
        public void DeleteUserSuccessful()
        {
            //First, create a user with known id
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
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

            //Then, get user
            var gotUser = repo.GetUserById(id);
            Assert.IsNotNull(gotUser);
            Assert.AreEqual(gotUser.Id, id);

            //Now, delete user
            repo.DeleteUser(id);

            //And check whether user exists now
            var success = false;
            try
            {
                repo.GetUserById(id);
            }
            catch (UnauthorizedException)
            {
                success = true;
            }

            if (!success)
            {
                Assert.Fail("Delete user failed!");
            }
        }

        [Test]
        //That's bad idea not to distinguish between "wrong login/password" and "There is no such ID in DB"
        public void DeleteUserMissingId()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            Assert.IsFalse(repo.DeleteUser(id));
        }


        [Test]
        public void EditUserSuccessful()
        {
            //First, create a user we'll work with
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
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

            //Now, try to edit newly created user
            user.FirstName = "Test123";
            user.LastName = "Test123";
            var modifiedUser = repo.UpdateUser(user);

            Assert.AreEqual("Test123", modifiedUser.FirstName);
            Assert.AreEqual("Test123", modifiedUser.LastName);
        }

        [Test]
        [ExpectedException(typeof(UnauthorizedException))]
        public void EditUserMissingId()
        {
            var repo = new UserRepository(new RestClient());
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                FirstName = "Test",
                LastName = "Test",
                City = "Test",
                AddressLine1 = "Line 1",
                Country = "AUS",
                State = "state",
                Zip = "123456",
                Email = id + "@google.com"
            };

            repo.UpdateUser(user);
        }


        [Test]
        [Ignore] //Currently, this test returns 401
        public void SendMobilePinSuccessful()
        {
            var repo = new UserRepository(new RestClient());
        }

        [Test]
        public void ListUserItemsSuccessful()
        {
            var repo = new UserRepository(new RestClient());
            var items = repo.ListItemsForUser("89592d8a-6cdb-4857-a90d-b41fc817d639");
        }

        [Test]
        public void ListUserBankAccountsSuccessful()
        {
            var repo = new UserRepository(new RestClient());
            var items = repo.ListBankAccountsForUser("89592d8a-6cdb-4857-a90d-b41fc817d639");
        }

        [Test]
        public void ListUserCardAccountsSuccessful()
        {
            var repo = new UserRepository(new RestClient());
            var items = repo.ListCardAccountsForUser("89592d8a-6cdb-4857-a90d-b41fc817d639");
        }

        [Test]
        public void ListUserPayPalAccountsSuccessful()
        {
            var repo = new UserRepository(new RestClient());
            var items = repo.ListPayPalAccountsForUser("89592d8a-6cdb-4857-a90d-b41fc817d639");
        }

        [Test]
        [Ignore]
        public void ListUserDisbursementAccountsSuccessful()
        {
            var repo = new UserRepository(new RestClient());
            var items = repo.SetDisbursementAccount("89592d8a-6cdb-4857-a90d-b41fc817d639", "123");
        }


    }
}
