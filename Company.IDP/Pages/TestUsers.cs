// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;

namespace Company.IDP;

public static class TestUsers
{
	public static List<TestUser> Users
	{
		get
		{

			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = Guid.NewGuid().ToString(),
					Username = "David",
					Password = "password",
					Claims =
					{
						new Claim(JwtClaimTypes.GivenName, "David"),
						new Claim(JwtClaimTypes.FamilyName, "Flagg")  }
				},
				new TestUser
				{
					SubjectId = Guid.NewGuid().ToString(),
					Username = "Emma",
					Password = "password",
					Claims =
					{
						new Claim(JwtClaimTypes.GivenName, "Emma"),
						new Claim(JwtClaimTypes.FamilyName, "Flagg")  }
				}
			};
		}
	}
}