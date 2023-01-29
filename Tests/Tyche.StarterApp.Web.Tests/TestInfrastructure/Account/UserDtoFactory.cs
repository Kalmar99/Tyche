using System;
using Bogus;
using Tyche.StarterApp.Account;

namespace Tyche.StarterApp.Web.Tests.Account;

public static class UserDtoFactory
{
    private static readonly Faker Faker = new Faker();

    public static UserDto Create(string accountId) => new(Guid.NewGuid().ToString(), Faker.Person.FirstName, Faker.Person.Email, Guid.NewGuid().ToString(), UserRole.User, accountId);
}