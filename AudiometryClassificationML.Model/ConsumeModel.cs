using System;
using System.IO;
using Microsoft.ML;

namespace AudiometryClassificationML.Model
{
    public class ConsumeModel
    {
        private static readonly Lazy<PredictionEngine<HearingInstanceInput, HearingInstanceOutput>> PredEngine = new Lazy<PredictionEngine<HearingInstanceInput, HearingInstanceOutput>>(CreatePredEngine);

        public static string MODEL_PATH = Path.GetFullPath("MLModel.zip");


        /// <summary>
        /// Consumes the model in the console app.
        /// </summary>
        /// <param name="input"> an instance of HearingInstanceInput </param>
        /// <returns> result, an instance of HearingInstanceOutput </returns>
        public static HearingInstanceOutput Predict(HearingInstanceInput input)
        {
            return PredEngine.Value.Predict(input);
        }


        /// <summary>
        /// Loads the model and creates a
        /// PredictionEngine to consume the model.
        /// </summary>
        /// <returns> a PredictionEngine </returns>
        public static PredictionEngine<HearingInstanceInput, HearingInstanceOutput> CreatePredEngine()
        {
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MODEL_PATH, out var _);
            return mlContext.Model.CreatePredictionEngine<HearingInstanceInput, HearingInstanceOutput>(mlModel);
        }
    }
}