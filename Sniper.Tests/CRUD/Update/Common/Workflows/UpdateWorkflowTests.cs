using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using Xunit;

namespace Sniper.Tests.CRUD.Update.Common.Workflows
{
    public class WorkflowTests 
     { 
        [Fact] 
        public void WorkflowThrowsError() 
        { 
            var client = new TargetProcessClient 
            { 
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.Workflows) 
            }; 
            var workflow = new Workflow 
            { 
            }; 
        } 
    } 
} 
