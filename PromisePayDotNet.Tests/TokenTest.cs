﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Implementations;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    [Ignore] 
    public class TokenTest
    {
        [TestMethod]
        //it seems I have created a token already, so it return error
        public void RequestToken()
        {
            var repo = new TokenRepository();
            var token = repo.RequestToken();
        }

        [TestMethod]
        public void RequestSessionToken()
        {
            var repo = new TokenRepository();
            var result = repo.RequestSessionToken(new Token
            {
                CurrentUserId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c",
                Amount = "$100.00",
                ExternalItemId = "b3b4d024-a6de-4b04-9f8d-6545eef3b28f",
                ExternalBuyerId = "fdf58725-96bd-4bf8-b5e6-9b61be20662e",
                ExternalSellerId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c",
                BuyerEmail = "aaa@bbb.com",
                SellerEmail = "ccc@ddd.com",
                BuyerCountry = "AUS",
                SellerCountry = "AUS",
                BuyerFirstName = "Preved",
                SellerFirstName = "Medved",
                ItemName = "Bear"
            });
        }

        [TestMethod]
        public void Widget()
        {
            var repo = new TokenRepository();
            var widget = repo.GetWidget("aaa-bbb-cc");
        }
    }
}
