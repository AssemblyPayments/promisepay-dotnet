﻿using Newtonsoft.Json;
using NUnit.Framework;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Implementations;
using System.IO;
using System.Linq;

namespace PromisePayDotNet.Tests
{
    public class CompanyTest : AbstractTest
    {
        [Test]
        public void CompanyDeserialization()
        {
            const string jsonStr = "{ \"legal_name\": \"Igor\", \"name\": null, \"id\": \"e466dfb4-f05c-4c7f-92a3-09a0a28c7af5\", \"related\": { \"address\": \"07ed45e5-bb9d-459f-bb7b-a02ecb38f443\" }, \"links\": { \"self\": \"/companies/e466dfb4-f05c-4c7f-92a3-09a0a28c7af5\" } }";
            var company = JsonConvert.DeserializeObject<Company>(jsonStr);
            Assert.IsNotNull(company);
            Assert.AreEqual("Igor", company.LegalName);
            Assert.AreEqual("e466dfb4-f05c-4c7f-92a3-09a0a28c7af5", company.Id);
        }

        [Test]
        public void ListCompaniesSuccessfully()
        {
            var content = File.ReadAllText("../../Fixtures/companies_list.json");

            var client = GetMockClient(content);
            var repo = new CompanyRepository(client.Object);
            var companies = repo.ListCompanies();
            client.VerifyAll();
            Assert.IsNotNull(companies);
            Assert.IsTrue(companies.Any());
        }

        [Test]
        public void GetCompanyByIdSuccessfully()
        {
            var content = File.ReadAllText("../../Fixtures/companies_get_by_id.json");

            var client = GetMockClient(content);
            var repo = new CompanyRepository(client.Object);
            var company = repo.GetCompanyById("e466dfb4-f05c-4c7f-92a3-09a0a28c7af5");
            client.VerifyAll();
            Assert.IsNotNull(company);
            Assert.AreEqual("e466dfb4-f05c-4c7f-92a3-09a0a28c7af5",company.Id);
        }

        [Test]
        public void CreateCompanySuccessfully()
        {
            var content = File.ReadAllText("../../Fixtures/companies_create.json");

            var client = GetMockClient(content);
            var repo = new CompanyRepository(client.Object);
            var createdCompany = repo.CreateCompany(new Company
            {
                LegalName = "Test company #1",
                Name = "Test company #1",
                Country = "AUS"
            });
            client.VerifyAll();
            Assert.IsNotNull(createdCompany);
            Assert.IsNotNull(createdCompany.Id);
        }

        [Test]
        public void EditCompanySuccessfully()
        {
            var content = File.ReadAllText("../../Fixtures/companies_edit.json");

            var client = GetMockClient(content);
            var repo = new CompanyRepository(client.Object);
            var editedCompany = repo.EditCompany(new Company
            {
                Id = "739dcfc5-adf0-4a00-b639-b4e05922994d",
                LegalName = "Test company #2",
                Name = "Test company #2",
                Country = "AUS"
            });
            client.VerifyAll();
            Assert.AreEqual("Test company #2", editedCompany.Name);
        }


    }
}
