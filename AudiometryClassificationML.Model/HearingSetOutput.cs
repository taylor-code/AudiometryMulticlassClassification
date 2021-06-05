using Microsoft.ML.Data;

namespace AudiometryClassificationML.Model
{
    public class HearingSetOutput
    {
        [ColumnName("PredictedType")]
        public string  TypePrediction { get; set; }
        public float[] Score          { get; set; }

        [ColumnName("PredictedDegree")]
        public string DegreePrediction { get; set; }

        [ColumnName("PredictedConfig")]
        public string ConfigPrediction { get; set; }
    }
}
