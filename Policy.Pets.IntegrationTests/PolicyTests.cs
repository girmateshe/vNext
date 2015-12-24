using System;
using System.Threading.Tasks;
using Common;
using Common.Configuration;
using Common.Http;
using Common.Json;
using FluentAssertions;
using Ninject;
using NUnit.Framework;
using Policy.Pets.Models;
using Newtonsoft.Json;

namespace Policy.Pets.IntegrationTests
{
    [TestFixture]
    public class PolicyTests
    {
        protected StandardKernel Kernel { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl;

        public PolicyTests()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<IConfiguration>().To<Configuration>();
            Kernel.Bind<ISerializer>().To<Common.Json.JsonSerializer>();
            Kernel.Bind<IHttpClient>().To<HttpClient>();
            Kernel.Bind<JsonSerializerSettings>().To<JsonSerializerSettings>();
            

            _configuration = Kernel.Get<IConfiguration>();
            _httpClient = Kernel.Get<IHttpClient>();

            _baseUrl = _configuration.RootRestApiUrl + "/policies";
        }

        [SetUp]
        public void Init()
        { /* ... */ }

        [TearDown]
        public void Cleanup()
        { /* ... */ }

        [Test,
        TestCaseSource("Names"),
        Category("PolicyTests"),
        Description("Test owner names")]
        public async Task AddPolicyTests_OwnerNames(string name)
        {
            var result = await _httpClient.PostAsync<PetOwner, PetOwner>
                          (new Uri(_baseUrl), 
                              new PetOwner
                              {
                                  Name = name,
                                  CountryIsoCode = "USA",
                                  Email = "mytest@email.com"
                              });

            var policy = result.Content;
            policy.Name.Should().Be(name);
            policy.CountryIsoCode.Should().Be("USA");
            policy.Email.Should().Be("mytest@email.com");
        }

        [Test,
            TestCaseSource("IsoCodes"),
            Category("PolicyTests"),
            Description("Test country")]
        public async Task AddPolicyTests_Country(string isoCode)
        {
            var result = await _httpClient.PostAsync<PetOwner, PetOwner>
              (new Uri(_baseUrl),
                  new PetOwner
                  {
                      Name = "Test",
                      CountryIsoCode = isoCode,
                      Email = "mytest@email.com"
                  });

            var policy = result.Content;

            policy.Name.Should().Be("Test");
            policy.CountryIsoCode.Should().Be(isoCode);
            policy.Email.Should().Be("mytest@email.com");
        }

        [Test,
        TestCaseSource("Emails"),
        Category("PolicyTests"),
        Description("Test Emails")]
        public async Task AddPolicyTests_Emails(string email)
        {
            var result = await _httpClient.PostAsync<PetOwner, PetOwner>
                                  (new Uri(_baseUrl),
                                      new PetOwner
                                      {
                                          Name = "Test",
                                          CountryIsoCode = "USA",
                                          Email = email
                                      });

            var policy = result.Content;

            policy.Name.Should().Be("Test");
            policy.CountryIsoCode.Should().Be("USA");
            policy.Email.Should().Be(email);
        }

        private static readonly string[] Names = { "Dizzy", "户网站", "Movie" };
        private static readonly string[] IsoCodes = { "ETH", "USA" };
        private static readonly string[] Emails = { "test1@gmail.com", "户网站@gmail.com", "gmail@gmail.com" };
    }

}
