using System.Threading.Tasks;
using Common.Configuration;
using FluentAssertions;
using Ninject;
using NUnit.Framework;
using Policy.Pets.Models;
using Policy.Pets.Provider;
using Policy.Pets.Provider.Interfaces;

namespace Policy.Pets.Tests
{
    [TestFixture]
    public class PolicyProviderTests
    {
        protected StandardKernel Kernel { get; set; }

        private readonly IPolicyProvider _policyProvider; 
        public PolicyProviderTests()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<IConfiguration>().To<Configuration>();
            Kernel.Bind<IDebugContext>().To<DebugContext>();
            Kernel.Bind<IPolicyProvider>().To<PolicyProvider>();

            _policyProvider = Kernel.Get<PolicyProvider>();
            _policyProvider.DebugContext = Kernel.Get<DebugContext>();
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
            var policy = await _policyProvider.Enroll(
                                                new PetOwner
                                                {
                                                    Name = name, 
                                                    CountryIsoCode = "USA", 
                                                    Email = "mytest@email.com"
                                                });

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
             var policy = await _policyProvider.Enroll(
                                                 new PetOwner
                                                 {
                                                     Name = "Test",
                                                     CountryIsoCode = isoCode,
                                                     Email = "mytest@email.com"
                                                 });

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
             var policy = await _policyProvider.Enroll(
                                                 new PetOwner
                                                 {
                                                     Name = "Test",
                                                     CountryIsoCode = "USA",
                                                     Email = email
                                                 });

             policy.Name.Should().Be("Test");
             policy.CountryIsoCode.Should().Be("USA");
             policy.Email.Should().Be(email);
         }

         private static readonly string[] Names = { "Dizzy", "户网站", "Movie" };
         private static readonly string[] IsoCodes = { "ETH", "USA"};
         private static readonly string[] Emails = { "test1@gmail.com", "户网站@gmail.com", "gmail@gmail.com" };
    }
}
