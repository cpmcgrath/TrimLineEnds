using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrimLineEnds
{
    static class GuidList
    {
        public const string guidTrimLineEndsPkgString = "a1688f1c-21b0-42cb-a381-a799a95ebd65";
        public const string guidCommandSetString = "370b7457-7f7a-42ab-8795-8e2faa714cc6";

        public static readonly Guid guidTrimLineEndsCmdSet = new Guid(guidCommandSetString);
    };
}
