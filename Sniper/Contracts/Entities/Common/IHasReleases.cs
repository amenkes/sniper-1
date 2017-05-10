﻿using Sniper.Common;
using System.Collections.ObjectModel;

namespace Sniper.Contracts.Entities.Common
{
    public interface IHasReleases
    {
        Collection<Release> Releases { get; }
    }
}