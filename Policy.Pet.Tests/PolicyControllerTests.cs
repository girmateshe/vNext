using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using Common.Configuration;
using FluentAssertions;
using Moq;
using Ninject;
using NUnit.Framework;
using Policy.Pets.Controllers;
using Policy.Pets.Models;
using Policy.Pets.Provider;
using Policy.Pets.Provider.Interfaces;

namespace Policy.Pets.Tests
{
    [TestFixture]
    public class PolicyControllerTests
    {
        protected StandardKernel Kernel { get; set; }

        private readonly IPolicyProvider _policyProvider;
        private readonly IDebugContext _debugContext; 
        public PolicyControllerTests()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<IConfiguration>().To<Configuration>();
            Kernel.Bind<IDebugContext>().To<DebugContext>();
            Kernel.Bind<IPolicyProvider>().To<PolicyProvider>();

            _policyProvider = Kernel.Get<PolicyProvider>();
            _debugContext = Kernel.Get<IDebugContext>();
            _policyProvider.DebugContext = _debugContext;
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
             var policyProviderMock = new Mock<IPolicyProvider>();
             policyProviderMock.Setup((pp => pp.Enroll(It.IsAny<PetOwner>())))
                 .Returns(Task.FromResult(
                     new PetOwner
                        {
                            Name = name, 
                            CountryIsoCode = "USA", 
                            Email = "mytest@email.com"
                        }));

             var controller = GetController(policyProviderMock.Object);

             var result = await controller.PostPetOwner(null);
             var response = await result.ExecuteAsync(new CancellationToken());
             var policy = await response.Content.ReadAsAsync<PetOwner>();

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
             var policyProviderMock = new Mock<IPolicyProvider>();
             policyProviderMock.Setup((pp => pp.Enroll(It.IsAny<PetOwner>())))
                 .Returns(Task.FromResult(
                     new PetOwner
                     {
                         Name = "Test",
                         CountryIsoCode = isoCode,
                         Email = "mytest@email.com"
                     }));

             var controller = GetController(policyProviderMock.Object);

             var result = await controller.PostPetOwner(null);
             var response = await result.ExecuteAsync(new CancellationToken());
             var policy = await response.Content.ReadAsAsync<PetOwner>();

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
             var policyProviderMock = new Mock<IPolicyProvider>();
             policyProviderMock.Setup((pp => pp.Enroll(It.IsAny<PetOwner>())))
                 .Returns(Task.FromResult(
                         new PetOwner
                         {
                             Name = "Test",
                             CountryIsoCode = "USA",
                             Email = email
                         }));

             var controller = GetController(policyProviderMock.Object);

             var result = await controller.PostPetOwner(null);
             var response = await result.ExecuteAsync(new CancellationToken());
             var policy = await response.Content.ReadAsAsync<PetOwner>();

             policy.Name.Should().Be("Test");
             policy.CountryIsoCode.Should().Be("USA");
             policy.Email.Should().Be(email);
         }

         private static readonly string[] Names = { "Dizzy", "户网站", "Movie" };
         private static readonly string[] IsoCodes = { "ETH", "USA"};
         private static readonly string[] Emails = { "test1@gmail.com", "户网站@gmail.com", "gmail@gmail.com" };

        private PoliciesController GetController(IPolicyProvider policyProvider)
        {
            var controller = new PoliciesController(policyProvider, _debugContext) { Request = new HttpRequestMessage() };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return controller;
        }
    }
}
