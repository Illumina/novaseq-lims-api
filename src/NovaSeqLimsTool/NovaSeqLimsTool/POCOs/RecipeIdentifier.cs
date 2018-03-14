using System;

namespace NovaSeqLimsTool.POCOs
{
    public class RecipeIdentifier
    {
        public string FlowCellId { get; set; }
        public string LibraryContainerId { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}[{Environment.NewLine}Recipe Identifier: {Environment.NewLine}" +
                   $"\tFlowCellId: {FlowCellId}{Environment.NewLine}" +
                   $"\tLibraryContainerId: {LibraryContainerId}{Environment.NewLine}]";
        }
    }
}
