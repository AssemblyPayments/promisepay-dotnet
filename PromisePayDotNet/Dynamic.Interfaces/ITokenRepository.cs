using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface ITokenRepository
    {
        IDictionary<string, object> RequestToken();

        IDictionary<string, object> RequestSessionToken(IDictionary<string, object> token);

        IDictionary<string, object> GetWidget(string sessionToken);

        IDictionary<string, object> GenerateCardToken(string tokenType, string userId);
    }
}
