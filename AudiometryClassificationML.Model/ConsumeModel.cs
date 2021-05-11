using System;
using System.IO;
using Microsoft.ML;

namespace AudiometryClassificationML.Model
{
    public class ConsumeModel
    {
        private static readonly Lazy<PredictionEngine<HearingSetInput, HearingSetOutput>> PredEngine = new Lazy<PredictionEngine<HearingSetInput, HearingSetOutput>>(CreatePredictionEngine);

        public static string MODEL_PATH = Path.GetFullPath("MLModel.zip");


        /// <summary>
        /// Consumes the model in the console app.
        /// </summary>
        /// <param name="input"> an instance of HearingSetInput </param>
        /// <returns> result, an instance of HearingSetOutput </returns>
        public static HearingSetOutput Predict(HearingSetInput input)
        {
            HearingSetOutput result = PredEngine.Value.Predict(input);
            return result;
        }


        /// <summary>
        /// Creates a prediction engine to consume the model.
        /// </summary>
        /// <returns> predEngine </returns>
        public static PredictionEngine<HearingSetInput, HearingSetOutput> CreatePredictionEngine()
        {
            MLContext mlContext = new MLContext();

            // Load model and create prediction engine.
            ITransformer mlModel = mlContext.Model.Load(MODEL_PATH, out var _);// modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<HearingSetInput, HearingSetOutput>(mlModel);

            return predEngine;
        }
    }
}