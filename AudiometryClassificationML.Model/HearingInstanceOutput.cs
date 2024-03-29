using Microsoft.ML.Data;

namespace AudiometryClassificationML.Model
{
    public class HearingInstanceOutput
    {
        [ColumnName("PredictedConfig")]
        public string ConfigPrediction { get; set; }

        [ColumnName("PredictedDegree")]
        public string DegreePrediction { get; set; }

        [ColumnName("PredictedType")]
        public string  TypePrediction  { get; set; }
    }
}