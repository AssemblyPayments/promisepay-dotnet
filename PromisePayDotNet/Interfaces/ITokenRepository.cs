using PromisePayDotNet.DTO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface ITokenRepository
    {
        string RequestToken();

        IDictionary<string, object> RequestSessionToken(Token token);

        Widget GetWidget(string sessionToken);
    }
}
