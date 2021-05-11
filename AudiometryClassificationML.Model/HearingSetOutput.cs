using System;
using Microsoft.ML.Data;

namespace AudiometryClassificationML.Model
{
    public class HearingSetOutput
    {
        [ColumnName("PredictedLabel")]
        public Single Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
