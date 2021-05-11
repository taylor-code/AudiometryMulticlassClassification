using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using AudiometryClassificationML.Model;

namespace AudiometryClassificationML.ConsoleApp
{
    public static class ModelBuilder
    {
        /**********************************************/
        /*                 ATTRIBUTES                 */
        /**********************************************/

        /* FILE PATHS */

        private static string APP_PATH => Environment.CurrentDirectory;
        /// <summary> Dataset used to test the model. </summary>
        private static readonly string TEST_DATA_FILEPATH = Path.Combine(APP_PATH, "..", "..", "..", "Data", "AudiometryTest.csv");
        /// <summary> Dataset used to train the model. </summary>
        private static readonly string TRAIN_DATA_FILEPATH = Path.Combine(APP_PATH, "..", "..", "..", "Data", "AudiometryTrain.csv");
        /// <summary> Path where the trained model is saved. </summary>
        private static readonly string MODEL_FILE = ConsumeModel.MODEL_PATH;


        /* MODEL ATTRIBUTES */

        private static MLContext mlContext;
        private static IDataView TrainingDataView;
        private static ITransformer TrainedModel;
        private static IEstimator<ITransformer> TrainingPipeline;



        /**********************************************/
        /*                  METHODS                   */
        /**********************************************/

        /// <summary>
        /// Driver for the model creation.
        /// </summary>
        public static void CreateModel()
        {
            mlContext = new MLContext(seed: 1);
            Console.WriteLine("Initialized MLContext.");

            // Load data.
            TrainingDataView = mlContext.Data.LoadFromTextFile<HearingSetInput>(
                                  path: TRAIN_DATA_FILEPATH,
                                  hasHeader: true,
                                  separatorChar: ',',
                                  allowQuoting: true,
                                  allowSparse: false
                               );
            Console.WriteLine("Loaded the training data.");

            // Build training pipeline.
            TrainingPipeline = BuildTrainingPipeline();
            Console.WriteLine("Processed the data.");

            // Train model.
            Console.WriteLine("Training the model...");
            TrainedModel = TrainModel();

            // Evaluate model.
            Console.WriteLine("Evaluating the model...");
            Evaluate();

            // Save model.
            SaveModel(TrainingDataView.Schema);
            Console.WriteLine("Saved the model.");

            Console.WriteLine("Predicting...");
            Predict();
        }


        /// <summary>
        /// Extracts and transforms the data.
        /// </summary>
        /// <returns> trainingPipeline </returns>
        public static IEstimator<ITransformer> BuildTrainingPipeline()
        {
            // Extract features and transform the data.
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("col0", "col0")
                                      .Append(mlContext.Transforms.Categorical.OneHotEncoding(new[] {
                                          new InputOutputColumnPair("col1", "col1"),
                                          new InputOutputColumnPair("col2", "col2"),
                                          new InputOutputColumnPair("col3", "col3"),
                                          new InputOutputColumnPair("col4", "col4"),
                                          new InputOutputColumnPair("col5", "col5"),
                                          new InputOutputColumnPair("col6", "col6"),
                                          new InputOutputColumnPair("col7", "col7"),
                                          new InputOutputColumnPair("col8", "col8"),
                                          new InputOutputColumnPair("col9", "col9"),
                                          new InputOutputColumnPair("col10", "col10"),
                                          new InputOutputColumnPair("col11", "col11"),
                                          new InputOutputColumnPair("col12", "col12"),
                                          new InputOutputColumnPair("col13", "col13") }
                                      ))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col14_tf", "col14"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col15_tf", "col15"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col16_tf", "col16"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col17_tf", "col17"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col18_tf", "col18"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col19_tf", "col19"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col20_tf", "col20"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col21_tf", "col21"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col22_tf", "col22"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col23_tf", "col23"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col24_tf", "col24"))
                                      .Append(mlContext.Transforms.Text.FeaturizeText("col25_tf", "col25"))
                                      .Append(mlContext.Transforms.Concatenate("Features", new[] {
                                          "col1", "col2", "col3", "col4", "col5", "col6", "col7", "col8",
                                          "col9", "col10", "col11", "col12", "col13", "col14_tf", "col15_tf",
                                          "col16_tf", "col17_tf", "col18_tf", "col19_tf", "col20_tf",
                                          "col21_tf", "col22_tf", "col23_tf", "col24_tf", "col25_tf" }
                                      ));

            // Set the training algorithm.
            //var trainer = mlContext.MulticlassClassification.Trainers.LightGbm(labelColumnName: @"col0", featureColumnName: "Features")
            //                          .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));

            // SdcaMaximumEntropy is the multi-class classification training algorithm.
            var trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(@"col0", "Features")
                                      .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }


        /// <summary>
        /// Fits the training data to the training pipeline.
        /// </summary>
        /// <returns> model </returns>
        public static ITransformer TrainModel()
        {
            return TrainingPipeline.Fit(TrainingDataView);
        }


        /// <summary>
        /// Evaluates the model using the test dataset.
        /// </summary>
        private static void Evaluate()
        {
            // Load data.
            IDataView testDataView = mlContext.Data.LoadFromTextFile<HearingSetInput>(
                                        path: TEST_DATA_FILEPATH,
                                        hasHeader: true,
                                        separatorChar: ',',
                                        allowQuoting: true,
                                        allowSparse: false
                                     );


            // Evaluate the model's quality metrics.
            var testMetrics = mlContext.MulticlassClassification.Evaluate(TrainedModel.Transform(testDataView), labelColumnName: @"col0");
            PrintMulticlassClassificationMetrics(testMetrics);
        }


        /// <summary>
        /// Saves the trained model to a .ZIP file.
        /// </summary>
        /// <returns> model </returns>
        private static void SaveModel(DataViewSchema modelInputSchema)
        {
            mlContext.Model.Save(TrainedModel, modelInputSchema, GetAbsolutePath(MODEL_FILE));
        }


        /// <summary>
        /// Uses Path to get the absolute path of a file.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns> fullPath </returns>
        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }


        /// <summary>
        /// Prints the micro- and macro-accuracy, log-loss,
        /// and log-loss reduction of the model evaluation.
        /// </summary>
        /// <param name="metrics"></param>
        public static void PrintMulticlassClassificationMetrics(MulticlassClassificationMetrics metrics)
        {
            /*
             * Metrics for Multi-Class Classification:
             * 
             * Micro Accuracy      :  Better if close to 1
             * Macro Accuracy      :  Better if close to 1
             * Log-loss            :  Better if close to 0
             * Log-loss Reduction  :  Better if close to 1
             */

            // Display the metrics for model validation.
            Console.WriteLine($"\n*****************************************************");
            Console.WriteLine($"*    Metrics for Multi-Class Classification Model   ");
            Console.WriteLine($"*----------------------------------------------------");
            Console.WriteLine($"*   Macro Accuracy     = {metrics.MacroAccuracy:0.####}");
            Console.WriteLine($"*   Micro Accuracy     = {metrics.MicroAccuracy:0.####}");
            Console.WriteLine($"*   Log-Loss           = {metrics.LogLoss:0.####}");
            Console.WriteLine($"*   Log-Loss Reduction = {metrics.LogLossReduction:0.####}");
            Console.WriteLine($"*****************************************************\n");
        }


        /******************************** TO BE DELETED ********************************/

        /// <summary>
        /// Executes the following tasks:
        ///   Creates the training algorithm class.
        ///   Trains the model.
        ///   Predicts Area based on training data.
        ///   Returns the model.
        /// </summary>
        /// <returns>trainingPipeline</returns>
        public static void Predict()
        {
            // PredictionEngine is a convenience API. Performs a prediction on a single instance of data.
            var PredictionEngine = mlContext.Model.CreatePredictionEngine<HearingSetInput, HearingSetOutput>(TrainedModel);


            /* Predict with the trained model */
            HearingSetInput sampleData = new HearingSetInput()
            {
                Degree = @"Mild",
                AC_L_250 = @"30",
                AC_L_500 = @"25",
                AC_L_1000 = @"40",
                AC_L_2000 = @"25",
                AC_L_4000 = @"30",
                AC_L_8000 = @"30",
                AC_R_250 = @"40",
                AC_R_500 = @"30",
                AC_R_1000 = @"35",
                AC_R_2000 = @"30",
                AC_R_4000 = @"35",
                AC_R_8000 = @"25",
                BC_L_250 = @"0",
                BC_L_500 = @"5",
                BC_L_1000 = @"-5",
                BC_L_2000 = @"5",
                BC_L_4000 = @"5",
                BC_L_8000 = @"10",
                BC_R_250 = @"5",
                BC_R_500 = @"-5",
                BC_R_1000 = @"10",
                BC_R_2000 = @"0",
                BC_R_4000 = @"5",
                BC_R_8000 = @"10"
            };

            var prediction = PredictionEngine.Predict(sampleData);


            /* Use the model: prediction results */
            Console.WriteLine($"=============== Single Prediction just-trained-model - Result: {prediction.Prediction} ===============");
        }

    }
}