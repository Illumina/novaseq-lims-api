using System;

namespace NovaSeqLimsTool.POCOs
{
    public class Reagent
    {
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LotNumber { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
        public string Mode { get; set; }
        public int Cycles { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}[{Environment.NewLine}Reagent: {Environment.NewLine}" +
                   $"\tName: {Name}, " +
                   $"Expiration Date: {ExpirationDate}, " +
                   $"Lot Number: {LotNumber}, " +
                   $"Serial Number: {SerialNumber}, {Environment.NewLine}" +
                   $"\tPart Number: {PartNumber}, " +
                   $"Mode: {Mode}, " +
                   $"Cycles: {Cycles}{Environment.NewLine}]";

        }
    }
}
