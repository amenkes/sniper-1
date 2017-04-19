﻿#if false
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sniper.Response
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class RepositoryTrafficViewSummary
    {
        public RepositoryTrafficViewSummary() { }

        [SuppressMessage(Categories.Naming, "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficViewSummary(int count, int uniques, IReadOnlyList<RepositoryTrafficView> views)
        {
            Count = count;
            Uniques = uniques;
            Views = views;
        }

        public int Count { get; protected set; }

        [SuppressMessage(Categories.Naming, "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; protected set; }

        public IReadOnlyList<RepositoryTrafficView> Views { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques);
    }

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class RepositoryTrafficView
    {
        public RepositoryTrafficView() { }

        [SuppressMessage(Categories.Naming, "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficView(DateTimeOffset timestamp, int count, int uniques)
        {
            Timestamp = timestamp;
            Count = count;
            Uniques = uniques;
        }

        public DateTimeOffset Timestamp { get; protected set; }

        public int Count { get; protected set; }

        [SuppressMessage(Categories.Naming, "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Timestamp: {0} Number: {1} Uniques: {2}", Timestamp, Count, Uniques);
    }
}
#endif