﻿using Sniper.Contracts;

namespace Sniper.Common
{
    ///<summary>
    /// A single Test Step Run
    /// </summary>
    /// <remarks>
    /// See the <a href="https://md5.tpondemand.com/api/v1/TestStepRuns/meta">API documentation - TestStepRun</a>
    /// </remarks>
    public class TestStepRun : IHasId, IHasDescription, IHasTestCaseRun, IHasTestStep
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Passed { get; set; }
        public string Result { get; set; }
        public bool Runned { get; set; }
        public int RunOrder { get; set; }

        public TestCaseRun TestCaseRun { get; set; }
        public TestStep TestStep { get; set; }
    }
}