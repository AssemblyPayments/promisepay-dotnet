using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Implementations;
using System.Linq;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        public void CompanyDeserialization()
        {
            var jsonStr = "{ \"legal_name\": \"Igor\", \"name\": null, \"id\": \"e466dfb4-f05c-4c7f-92a3-09a0a28c7af5\", \"related\": { \"address\": \"07ed45e5-bb9d-459f-bb7b-a02ecb38f443\" }, \"links\": { \"self\": \"/companies/e466dfb4-f05c-4c7f-92a3-09a0a28c7af5\" } }";
            var company = JsonConvert.DeserializeObject<Company>(jsonStr);
            Assert.IsNotNull(company);
            Assert.AreEqual("Igor", company.LegalName);
            Assert.AreEqual("e466dfb4-f05c-4c7f-92a3-09a0a28c7af5", company.Id);
        }

        [TestMethod]
        public void ListCompaniesSuccessfully()
        {
            var repo = new CompanyRepository();
            var companies = repo.ListCompanies();
            Assert.IsNotNull(companies);
            Assert.IsTrue(companies.Any());
        }

        [TestMethod]
        public void GetCompanyByIdSuccessfully()
        {
            var repo = new CompanyRepository();
            var company = repo.GetCompanyById("e466dfb4-f05c-4c7f-92a3-09a0a28c7af5");
            Assert.IsNotNull(company);
            Assert.AreEqual("e466dfb4-f05c-4c7f-92a3-09a0a28c7af5",company.Id);
        }

        [TestMethod]
        public void CreateCompanySuccessfully()
        {
            var repo = new CompanyRepository();
            var createdCompany = repo.CreateCompany(new Company
            {
                LegalName = "Test company #1",
                Name = "Test company #1",
                Country = "AUS"
            });
            Assert.IsNotNull(createdCompany);
            Assert.IsNotNull(createdCompany.Id);
        }

        [TestMethod]
        public void EditCompanySuccessfully()
        {
            var repo = new CompanyRepository();
            var editedCompany = repo.EditCompany(new Company
            {
                Id = "739dcfc5-adf0-4a00-b639-b4e05922994d",
                LegalName = "Test company #2",
                Name = "Test company #2",
                Country = "AUS"
            });
            Assert.AreEqual("Test company #2", editedCompany.Name);
        }


    }
}
