using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using System;
using Xunit;

namespace Sniper.Tests.CRUD.Read.Common.Bugs
{
    public class ReadBugTests
    {
        [Fact]
        public void CreateBugThrowsError()
        {
            var client = new TargetProcessClient
            {
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.Bugs)
            };
            var bug = new Bug
            {
            };
            var data = client.CreateData<Bug>(bug);

            Assert.NotNull(data);
            Assert.True(data.HttpResponse.IsError);
        }


        [Fact]
        public void CreateBugWithMinimumDataSucceeds()
        {
            var client = new TargetProcessClient
            {
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.Bugs)
            };
            var bug = new Bug
            {
                Name = $"Sample Bug From Code - {DateTime.Now}",
                Project = new Project { Id = 194 },
            };
            var data = client.CreateData<Bug>(bug);

            Assert.NotNull(data);
            Assert.False(data.HttpResponse.IsError);
        }
    }
}
