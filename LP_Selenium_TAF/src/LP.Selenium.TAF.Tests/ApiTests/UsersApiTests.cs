using System.Net;
using LP.Selenium.TAF.Core.API.Services;
using LP.Selenium.TAF.Tests.Base;

namespace LP.Selenium.TAF.Tests.ApiTests;

[Category("API")]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
[TestFixture]
public class UsersApiTests : BaseApiTest
{
    private UsersApiService _usersApiService;

    [SetUp]
    public void UsersApiSetUp()
    {
        _usersApiService = new UsersApiService(ApiClient);
    }

    [Test]
    public async Task GetUsers_ValidRequest_Returns200AndAllRequiredFields()
    {
        var response = await _usersApiService.GetUsersAsync();

        var users = response.Data!;

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ErrorMessage, Is.Null);
            Assert.That(response.Data, Is.Not.Null.And.Not.Empty);

            foreach (var user in users)
            {
                Assert.That(user.Id, Is.GreaterThan(0));
                Assert.That(user.Name, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Username, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Email, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Company, Is.Not.Null);
                Assert.That(user.Company!.Name, Is.Not.Null.And.Not.Empty);
            }
        });
    }

    [Test]
    public async Task GetUsers_ValidRequest_ReturnsJsonContentType()
    {
        var response = await _usersApiService.GetUsersAsync();

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ErrorMessage, Is.Null);
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
        });
    }

    [Test]
    public async Task GetUsers_ValidRequest_ReturnsTenUniqueUsersWithNamesAndCompany()
    {
        var response = await _usersApiService.GetUsersAsync();

        var users = response.Data!;

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ErrorMessage, Is.Null);
            Assert.That(users.Count(), Is.EqualTo(10));

            Assert.That(
                users.Select(user => user.Id).Distinct().Count(),
                Is.EqualTo(users.Count()),
                "Every user should have a unique ID.");

            foreach (var user in users)
            {
                Assert.That(user.Name, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Username, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Company, Is.Not.Null);
                Assert.That(user.Company!.Name, Is.Not.Null.And.Not.Empty);
            }
        });
    }

    [TestCase("John Smith", "johnsmith")]
    public async Task CreateUser_WithNameAndUserName_Returns201WithNonEmptyId(string name, string username)
    {
        username = $"{username}_{Guid.NewGuid().ToString("N")[..8]}";
        name = $"{name} {Guid.NewGuid().ToString("N")[8..]}";

        var response = await _usersApiService.CreateUserAsync(name, username);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.ErrorMessage, Is.Null);
            Assert.That(response.Data!, Is.Not.Null);
            Assert.That(response.Data!.Id, Is.GreaterThan(0));
        });
    }

    [Test]
    public async Task GetRequest_InvalidEndpoint_Returns404()
    {
        var response = await _usersApiService.GetInvalidEndpointAsync();
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.ErrorMessage, Is.Null);
        });
    }
}
