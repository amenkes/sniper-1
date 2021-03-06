using Sniper.Common;
using Sniper.Http;
using Sniper.TargetProcess.Routes;
using Xunit;

namespace Sniper.Tests.CRUD.Delete.Common.CustomFieldConfigs
{
    public class DeleteCustomFieldConfigTests 
     { 
        [Fact] 
        public void CustomFieldConfigThrowsError() 
        { 
            var client = new TargetProcessClient 
            { 
                ApiSiteInfo = new ApiSiteInfo(TargetProcessRoutes.Route.CustomFieldConfigs) 
            }; 
            var customFieldConfig = new CustomFieldConfig 
            { 
            }; 
        } 
    } 
} 
