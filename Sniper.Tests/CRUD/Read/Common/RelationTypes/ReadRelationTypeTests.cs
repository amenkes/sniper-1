using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using Xunit;

namespace Sniper.Tests.CRUD.Read.Common.RelationTypes
{
    public class ReadRelationTypeTests
    { 
        [Fact] 
        public void RelationTypeThrowsError() 
        { 
            var client = new TargetProcessClient 
            { 
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.RelationTypes) 
            }; 
            var relationType = new RelationType 
            { 
            }; 
        } 
    } 
} 
