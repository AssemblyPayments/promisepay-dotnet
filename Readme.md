# The PromisePay .NET Package

A .NET package providing access to the Promisepay API.

## Installation

Binary (preferable): Add PromisePay package into your project via NuGet package manager. Package name is “Promise Pay API”.

Source: download latest sources from GitHub, add project into your solution and build it.

## Usage

Before interacting with Promispay API, you need to generate an access token.

See http://docs.promisepay.com/v2.2/docs/request_token for more information.

### Client

PromisePay API package is build using Dependency Injection principle. If makes integration into your application easy and seamless.

First, you'll need to add PromisePay settings into your application config file. It's named App.config for console/Windows applications, and Web.config for web applications.

Here is the sample file
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <sectionGroup name="PromisePay">
      <section name="Settings" type="PromisePayDotNet.Settings.SettingsHandler,PromisePayDotNet" />
    </sectionGroup>
.. . other sections
  </configSections>
  <PromisePay>
    <Settings>
      <add key="ApiUrl" value="https://test.api.promisepay.com" />
      <add key="Login" value="YOUR LOGIN" />
      <add key="Password" value="YOUR PASSWORD" />
      <add key="Key" value="YOUR API KEY" />
    </Settings>
  </PromisePay>

.. . Some other content
</configuration>


URL value is
https://test.api.promisepay.com/ 
for test environment, and 
https://secure.api.promisepay.com/ 
for production environment

Then, you'll need to setup your DI container to bind interfaces and implementations of the package together.

If you use Unity container, just invoke init method, as it's shown below:

var container = new UnityContainer();
PromisePayDotNet.DI.InitUnityContainer.Init(container);

If you use another container, just bind interfaces from PromisePayDotNet.Interfaces to PromisePayDotNet.Implementations. You may use any lifecycle, implementations are stateless.


Then, you can use repositories from package, by resolving interface with container, or passing dependencies into constructor.

For details and example, please consider the following MSDN article:
https://msdn.microsoft.com/ru-ru/library/dn178463(v=pandp.30).aspx

Example:
List Users:

var repo = container.Resolve<IUserRepository>();
var users = repo.ListUsers();

Create User:

var repo = container.Resolve<IUserRepository>();

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

## Contributing

1. Fork it ( https://github.com/PromisePay/promisepay-dotnet/fork )
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create a new Pull Request