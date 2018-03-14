using System;

namespace NovaSeqLimsTool.POCOs
{
    public class RunInfo
    {
        public string RunId { get; set; }
        public string FlowCellId { get; set; }
        public string LibraryTubeId { get; set; }
        public string InstrumentId { get; set; }
        public SequencingInstrumentType InstrumentType { get; set; }
        public string FlowCellSide { get; set; }
        public DateTime DateTime { get; set; }
        public string OutputFolder { get; set; }
        public string UserName { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}[{Environment.NewLine}RunInfo: {Environment.NewLine}" +
                   $"\tRunId: {RunId}, " +
                   $"FlowCellId: {FlowCellId}, " +
                   $"LibraryTubeId: {LibraryTubeId}, {Environment.NewLine}" +
                   $"\tInstrumentId: {InstrumentId}, " +
                   $"InstrumentType: {InstrumentType}, " +
                   $"FlowCellSide: {FlowCellSide}, {Environment.NewLine}" +
                   $"\tDateTime: {DateTime}, " +
                   $"OutputFolder: {OutputFolder}, " +
                   $"UserName: {UserName}{Environment.NewLine}]";
        }
    }
}
