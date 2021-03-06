using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using Xunit;

namespace Sniper.Tests.CRUD.Update.Common.Requesters
{
    public class UpdateRequesterTests
    { 
        [Fact] 
        public void RequesterThrowsError() 
        { 
            var client = new TargetProcessClient 
            { 
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.Requesters) 
            }; 
            var requester = new Requester 
            { 
            }; 
        } 
    } 
} 
