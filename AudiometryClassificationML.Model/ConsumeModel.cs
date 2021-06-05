using System;
using System.IO;
using Microsoft.ML;

namespace AudiometryClassificationML.Model
{
    public class ConsumeModel
    {
        private static readonly Lazy<PredictionEngine<HearingSetInput, HearingSetOutput>> PredEngine = new Lazy<PredictionEngine<HearingSetInput, HearingSetOutput>>(CreatePredEngine);

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
        /// Loads the model and creates a
        /// PredictionEngine to consume the model.
        /// </summary>
        /// <returns> a PredictionEngine </returns>
        public static PredictionEngine<HearingSetInput, HearingSetOutput> CreatePredEngine()
        {
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MODEL_PATH, out var _);
            return mlContext.Model.CreatePredictionEngine<HearingSetInput, HearingSetOutput>(mlModel);
        }
    }
}