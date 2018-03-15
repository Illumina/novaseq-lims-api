using Newtonsoft.Json;
using System;

namespace NovaSeqLimsTool.POCOs
{
    public class RecipeDto
    {
        [JsonProperty(PropertyName = "run_name")]
        public string RunName { get; set; }

        [JsonProperty(PropertyName = "run_mode")]
        public string RunMode { get; set; }

        [JsonProperty(PropertyName = "workflow_type")]
        public string WorkflowType { get; set; }

        [JsonProperty(PropertyName = "library_tube_id")]
        public string LibraryTubeId { get; set; }

        [JsonProperty(PropertyName = "flowcell_id")]
        public string FlowCellId { get; set; }

        [JsonProperty(PropertyName = "sample_loading_type")]
        public string SampleLoadingType { get; set; }

        [JsonProperty(PropertyName = "rehyb")]
        public bool Rehyb { get; set; }

        [JsonProperty(PropertyName = "paired_end")]
        public bool PairedEnd { get; set; }

        [JsonProperty(PropertyName = "read1")]
        public int Read1 { get; set; }

        [JsonProperty(PropertyName = "read2")]
        public int Read2 { get; set; }

        [JsonProperty(PropertyName = "index_read1")]
        public int IndexRead1 { get; set; }

        [JsonProperty(PropertyName = "index_read2")]
        public int IndexRead2 { get; set; }

        [JsonProperty(PropertyName = "output_folder")]
        public string OutputFolder { get; set; }

        [JsonProperty(PropertyName = "samplesheet")]
        public string Samplesheet { get; set; }

        [JsonProperty(PropertyName = "usecustomrecipe")]
        public bool UseCustomRecipe { get; set; }

        [JsonProperty(PropertyName = "customRecipe")]
        public string CustomRecipe { get; set; }

        [JsonProperty(PropertyName = "use_basespace")]
        public bool UseBasespace { get; set; }

        [JsonProperty(PropertyName = "basespace_mode")]
        public string BasespaceMode { get; set; }

        [JsonProperty(PropertyName = "use_custom_read1_primer")]
        public bool UseCustomRead1Primer { get; set; }

        [JsonProperty(PropertyName = "use_custom_read2_primer")]
        public bool UseCustomRead2Primer { get; set; }

        [JsonProperty(PropertyName = "use_custom_index_read1_primer")]
        public bool UseCustomIndexRead1Primer { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}[{Environment.NewLine}RecipeDto:{Environment.NewLine}" +
                   $"\tRunName: {RunName}, " +
                   $"RunMode: {RunMode}, " +
                   $"WorkflowType: {WorkflowType}, " +
                   $"LibraryTubeId: {LibraryTubeId}, " +
                   $"FlowCellId: {FlowCellId}{Environment.NewLine}" +
                   $"\tSampleLoadingType: {SampleLoadingType}, " +
                   $"Rehyb: {Rehyb}, " +
                   $"PairedEnd: {PairedEnd}, " +
                   $"Read1: {Read1}, " +
                   $"Read2: {Read2}, " +
                   $"IndexRead1: {IndexRead1}, " +
                   $"IndexRead2: {IndexRead2}, {Environment.NewLine}" +
                   $"\tOutputFolder: {OutputFolder}, " +
                   $"Samplesheet: {Samplesheet}, " +
                   $"UseCustomRecipe: {UseCustomRecipe}, " +
                   $"CustomRecipe: {CustomRecipe},{Environment.NewLine}" +
                   $"\tUseBasespace: {UseBasespace}, " +
                   $"BasespaceMode: {BasespaceMode}, " +
                   $"UseCustomRead1Primer: {UseCustomRead1Primer}, " +
                   $"UseCustomRead2Primer: {UseCustomRead2Primer}, " +
                   $"UseCustomIndexRead1Primer: {UseCustomIndexRead1Primer}{Environment.NewLine}]";
        }
    }
}
