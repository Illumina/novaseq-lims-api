using System;

namespace NovaSeqLimsTool.POCOs
{
    public class SequencingRunMetrics
    {
        public RunInfo RunInfo { get; set; }
        public SequencingRunStatus Status { get; set; }
        public float YieldPfR1 { get; set; }
        public float YieldPfR2 { get; set; }
        public float ReadsPfR1 { get; set; }
        public float ReadsPfR2 { get; set; }
        public float ClusterDensityR1 { get; set; }
        public float ClusterDensityR2 { get; set; }
        public float PercentPfR1 { get; set; }
        public float PercentPfR2 { get; set; }
        public float PercentGreaterThanQ30R1 { get; set; }
        public float PercentGreaterThanQ30R2 { get; set; }
        public float IntensityCycle1R1 { get; set; }
        public float IntensityCycle1R2 { get; set; }
        public float PercentAlignedR1 { get; set; }
        public float PercentAlignedR2 { get; set; }
        public float PercentErrorRateR1 { get; set; }
        public float PercentErrorRateR2 { get; set; }
        public float PercentPhasingR1 { get; set; }
        public float PercentPhasingR2 { get; set; }
        public float PercentPrePhasingR1 { get; set; }
        public float PercentPrePhasingR2 { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}[{Environment.NewLine}SequencingRunMetrics:{Environment.NewLine}" +
                   $"\tStatus: {Status}, " +
                   $"YieldPfR1: {YieldPfR1}, " +
                   $"YieldPfR2: {YieldPfR2}, " +
                   $"ReadsPfR1: {ReadsPfR1}, " +
                   $"ReadsPfR2: {ReadsPfR2}, {Environment.NewLine}" +
                   $"\tClusterDensityR1: {ClusterDensityR1}, " +
                   $"ClusterDensityR2: {ClusterDensityR2}, " +
                   $"PercentPfR1: {PercentPfR1}, " +
                   $"PercentPfR2: {PercentPfR2}, " +
                   $"PercentGreaterThanQ30R1: {PercentGreaterThanQ30R1}, " +
                   $"PercentGreaterThanQ30R2: {PercentGreaterThanQ30R2}, {Environment.NewLine}" +
                   $"\tIntensityCycle1R1: {IntensityCycle1R1}, " +
                   $"IntensityCycle1R2: {IntensityCycle1R2}, " +
                   $"PercentAlignedR1: {PercentAlignedR1}, " +
                   $"PercentAlignedR2: {PercentAlignedR2}, {Environment.NewLine}" +
                   $"\tPercentErrorRateR1: {PercentErrorRateR1}, " +
                   $"PercentErrorRateR2: {PercentErrorRateR2}, " +
                   $"PercentPhasingR1: {PercentPhasingR1}, " +
                   $"PercentPhasingR2: {PercentPhasingR2}, {Environment.NewLine}" +
                   $"\tPercentPrePhasingR1: {PercentPrePhasingR1}, " +
                   $"PercentPrePhasingR2: {PercentPrePhasingR2}, {Environment.NewLine}]";

        }
    }
}
