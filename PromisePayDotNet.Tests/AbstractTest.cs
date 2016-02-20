using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;

namespace PromisePayDotNet.Tests
{
    public class AbstractTest
    {
        protected Mock<IRestClient> GetMockClient(string content)
        {
            var response = new Mock<IRestResponse>(MockBehavior.Strict);
            response.SetupGet(x => x.Content).Returns(content);
            response.SetupGet(x => x.ResponseUri).Returns(new Uri("http://google.com"));
            response.SetupGet(x => x.StatusDescription).Returns("");
            response.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            var client = new Mock<IRestClient>(MockBehavior.Strict);
            client.SetupSet(x => x.BaseUrl = It.IsAny<Uri>());
            client.SetupSet(x => x.Authenticator = It.IsAny<IAuthenticator>());
            client.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response.Object);
            return client;
        }
    }
}
