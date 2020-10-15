using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace MockIdentityServer
{
    class TestData
    {
        public string Id { get; set; }
        public string SubjectId { get; set; }
    }

    public class TestUsers
    {
        private static readonly List<TestData> TestData = new List<TestData>()
        {
            {new TestData() {SubjectId = "dd367d2d-9032-44ca-afc2-02add9f0e698", Id = "One"}},
            {new TestData() {SubjectId = "fcb42b48-5bbd-4a31-977b-01c39688abed", Id = "Two"}},
            {new TestData() {SubjectId = "a82dc16d-4581-4726-91c1-07e2c7221fd5", Id = "Three"}},
            {new TestData() {SubjectId = "070259e6-54b8-4e13-b4f2-ad98b76c666e", Id = "Four"}},
            {new TestData() {SubjectId = "dd367d2d-9032-44ca-afc2-02add9f0e698", Id = "Five"}},
        };

        public static List<TestUser> Users
        {
            get
            {
                var testUsers = new List<TestUser>();         

                TestData.ForEach(d =>
                {
                    testUsers.Add(new TestUser
                    {
                        SubjectId = d.SubjectId,
                        Username = $"test{d.Id}".ToLower(),
                        Password = "password",
                        Claims = new List<Claim>(new[]
                        {
                            new Claim(JwtClaimTypes.Subject, d.SubjectId),
                            new Claim(JwtClaimTypes.Name, $"Test{d.Id}"),
                            new Claim(JwtClaimTypes.GivenName, "Test"),
                            new Claim(JwtClaimTypes.FamilyName, d.Id),
                            new Claim(JwtClaimTypes.Email, $"test.{d.Id}@example".ToLower())
                        })
                    });
                });

                return testUsers;
            }
        }
    }
}
