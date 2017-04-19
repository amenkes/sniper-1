﻿#if false
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Sniper.Request
{
    /// <summary>
    /// Used to add assignees to an issue.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/git/commits/#create-a-commit  //TODO: Replace with TargetProcess if this is usable
    /// </remarks>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class AssigneesUpdate
    {
        public AssigneesUpdate(IReadOnlyList<string> assignees)
        {
            Assignees = assignees;
        }

        public IReadOnlyList<string> Assignees { get; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Assignees: {0}", Assignees);
    }
}
#endif