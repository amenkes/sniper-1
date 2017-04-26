﻿using System.Collections.ObjectModel;
using Sniper.Common;

namespace Sniper.Contracts
{
    public interface IHasTestCases
    {
        Collection<TestCase> TestCases { get; set; }
    }
}
