#.NET SDK - PromisePay API


#PHP SDK - PromisePay API

#1. Installation
**NuGet:** Install PromisePay via NuGet package manager. The package name is '[PromisePay](https://www.nuget.org/packages/PromisePay.API.NET/0.0.1)'.

**Source:** Download latest sources from GitHub, add project into your solution and build it.


#2. Configuration

Before interacting with PromisePay API, you need to generate an API token. See [http://docs.promisepay.com/v2.2/docs/request_token](http://docs.promisepay.com/v2.2/docs/request_token) for more information.

Once you have recorded your API token, configure the .NET package - see below.

Add the below configuration to either the **App.config** or **Web.config** file, depending if it is a Windows, or Web application.

	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
	  <configSections>
	    <sectionGroup name="PromisePay">
	      <section name="Settings" type="PromisePayDotNet.Settings.SettingsHandler,PromisePayDotNet" />
	    </sectionGroup>
	  </configSections>
	  <PromisePay>
	    <Settings>
	      <add key="ApiUrl" value="https://test.api.promisepay.com" />
	      <add key="Login" value="YOUR LOGIN" />
	      <add key="Password" value="YOUR PASSWORD" />
	      <add key="Key" value="YOUR API KEY" />
	    </Settings>
	  </PromisePay>
	</configuration>

**Environments**

	Prelive: https://test.api.promisepay.com
	Production: https://secure.api.promisepay.com

**Final configuration**

PromisePay API package is build using Dependency Injection principle. It makes integration into your application easy and seamless.

You will need to setup your DI container to bind interfaces and implementations of the package together.

If you use **Unity** container, just invoke init method, as it's shown below:

	var container = new UnityContainer();
	PromisePayDotNet.DI.InitUnityContainer.Init(container);

If you use another container, just bind interfaces from PromisePayDotNet.Interfaces to PromisePayDotNet.Implementations. You may use any lifecycle; implementations are stateless.


Then, you can use repositories from package, by resolving interface with container, or passing dependencies into constructor.

For details and example, please consider the following MSDN article:
[https://msdn.microsoft.com/ru-ru/library/dn178463(v=pandp.30).aspx](http://)

#3. Examples
**Create a user**

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
	
**Listing users**

	var repo = container.Resolve<IUserRepository>();
	var users = repo.ListUsers();



#4. Contributing
	1. Fork it ( https://github.com/PromisePay/promisepay-dotnet/fork )
	2. Create your feature branch (`git checkout -b my-new-feature`)
	3. Commit your changes (`git commit -am 'Add some feature'`)
	4. Push to the branch (`git push origin my-new-feature`)
	5. Create a new Pull Request
