using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface ITokenRepository
    {
        string RequestToken();

        IDictionary<string, object> RequestSessionToken(IDictionary<string, object> token);

        IDictionary<string, object> GetWidget(string sessionToken);
    }
}
