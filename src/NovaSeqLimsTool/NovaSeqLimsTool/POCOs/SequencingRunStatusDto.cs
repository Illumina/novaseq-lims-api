using System;
using System.Collections.Generic;
using System.Text;

namespace NovaSeqLimsTool.POCOs
{
    public class SequencingRunStatusDto
    {
        public RunInfo RunInfo { get; set; }
        public SequencingRunStatus Status { get; set; }
        public IEnumerable<Reagent> Reagents { get; set; }
        public int CurrentCycle { get; set; }
        public int CurrentRead { get; set; }
        public string InstrumentControlSoftwareVersion { get; set; }
        public string RtaVersion { get; set; }
        public string FirmwareVersion { get; set; }

        public SequencingRunStatusDto()
        {
            Reagents = new List<Reagent>();
        }

        public override string ToString()
        {
            var reagentStringBuilder = new StringBuilder();
            reagentStringBuilder.Append($"[{Environment.NewLine}");
            foreach (var r in Reagents)
            {
                reagentStringBuilder.Append(r);
                reagentStringBuilder.Append(Environment.NewLine);
            }
            reagentStringBuilder.Append($"]{Environment.NewLine}");
            return $"{Environment.NewLine}[{Environment.NewLine}RecipeDto:{Environment.NewLine}" +
                   $"\tRun Info: { RunInfo}{Environment.NewLine}" +
                   $"\tStatus: {Status}{Environment.NewLine}" +
                   $"\tReagents: {reagentStringBuilder}{Environment.NewLine}" +
                   $"\tCurrentCycle: {CurrentCycle}, " +
                   $"CurrentRead: {CurrentRead}, " +
                   $"InstrumentControlSoftwareVersion: {InstrumentControlSoftwareVersion},{Environment.NewLine}" +
                   $"\tRtaVersion: {RtaVersion}, " +
                   $"FirmwareVersion: {FirmwareVersion}{Environment.NewLine}]";

        }
    }
}
