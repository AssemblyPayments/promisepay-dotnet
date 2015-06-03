using PromisePayDotNet.DTO;

namespace PromisePayDotNet.Interfaces
{
    public interface ITokenRepository
    {
        string RequestToken();

        string RequestSessionToken(Token token);

        Widget GetWidget(string sessionToken);
    }
}
