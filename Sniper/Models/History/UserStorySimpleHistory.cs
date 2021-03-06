﻿using Newtonsoft.Json;
using Sniper.Common;
using Sniper.Contracts.Entities.Common;
using static Sniper.CustomAttributes.CustomAttributes;

namespace Sniper.History
{
    ///<summary>
    /// A change of an entity
    /// </summary>
    /// <remarks>
    /// See the <a href="https://md5.tpondemand.com/api/v1/UserStorySimpleHistories/meta">API documentation - UserStorySimpleHistory</a>
    /// </remarks>
    [CannotCreateReadUpdateDelete]
    public class UserStorySimpleHistory : SimpleHistoryExtendedBase, IHasUserStory
    {
        [JsonProperty(Required = Required.Default)]
        public UserStory UserStory { get; internal set; }
    }
}