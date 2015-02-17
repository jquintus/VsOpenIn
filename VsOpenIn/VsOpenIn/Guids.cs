// Guids.cs
// MUST match guids.h
using System;

namespace MasterDevs.VsOpenIn
{
    static class GuidList
    {
        public const string guidVsOpenInPkgString = "e869a176-ebba-4477-ab62-38f1b3d24c55";
        public const string guidVsOpenInCmdSetString = "79f81cee-3f29-4af6-b150-3ff7db159a5b";

        public static readonly Guid guidVsOpenInCmdSet = new Guid(guidVsOpenInCmdSetString);
    };
}