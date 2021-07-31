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
        /*          MACHING LEARNING METHODS          */
        /**********************************************/

        /// <summary>
        /// Driver for the model creation.
        /// </summary>
        public static void CreateModel()
        {
            mlContext = new MLContext(seed: 1);
            Console.WriteLine("Initialized MLContext.");

            TrainingDataView = LoadDataFile(TRAIN_DATA_FILEPATH);
            Console.WriteLine("Loaded the training data.");

            TrainingPipeline = BuildTrainingPipeline();
            Console.WriteLine("Processed the data.");

            Console.WriteLine("Training the model...");
            TrainedModel = TrainModel();

            Console.WriteLine("Evaluating the model...");
            Evaluate();

            SaveModel(TrainingDataView.Schema);
            Console.WriteLine("Saved the model.");
        }


        /// <summary>
        /// Extracts and transforms the data.
        /// </summary>
        /// <returns> trainingPipeline </returns>
        private static IEstimator<ITransformer> BuildTrainingPipeline()
        {
            var dataPipeline = mlContext.Transforms.Conversion.MapValueToKey(new[] {
                                      new InputOutputColumnPair("Type",   "Type"),
                                      new InputOutputColumnPair("Degree", "Degree"),
                                      new InputOutputColumnPair("Config", "Config")
                                  })
                                  // OneHotEncoding() assigns each category a unique value.
                                  .Append(mlContext.Transforms.Categorical.OneHotEncoding(new[] {
                                      new InputOutputColumnPair("L_AC_250",  "L_AC_250"),
                                      new InputOutputColumnPair("L_AC_500",  "L_AC_500"),
                                      new InputOutputColumnPair("L_AC_1000", "L_AC_1000"),
                                      new InputOutputColumnPair("L_AC_2000", "L_AC_2000"),
                                      new InputOutputColumnPair("L_AC_4000", "L_AC_4000"),
                                      new InputOutputColumnPair("L_AC_8000", "L_AC_8000"),
                                      new InputOutputColumnPair("L_BC_250",  "L_BC_250"),
                                      new InputOutputColumnPair("L_BC_500",  "L_BC_500"),
                                      new InputOutputColumnPair("L_BC_1000", "L_BC_1000"),
                                      new InputOutputColumnPair("L_BC_2000", "L_BC_2000"),
                                      new InputOutputColumnPair("L_BC_4000", "L_BC_4000"),
                                      new InputOutputColumnPair("L_BC_8000", "L_BC_8000"),
                                      new InputOutputColumnPair("R_AC_250",  "R_AC_250"),
                                      new InputOutputColumnPair("R_AC_500",  "R_AC_500"),
                                      new InputOutputColumnPair("R_AC_1000", "R_AC_1000"),
                                      new InputOutputColumnPair("R_AC_2000", "R_AC_2000"),
                                      new InputOutputColumnPair("R_AC_4000", "R_AC_4000"),
                                      new InputOutputColumnPair("R_AC_8000", "R_AC_8000"),
                                      new InputOutputColumnPair("R_BC_250",  "R_BC_250"),
                                      new InputOutputColumnPair("R_BC_500",  "R_BC_500"),
                                      new InputOutputColumnPair("R_BC_1000", "R_BC_1000"),
                                      new InputOutputColumnPair("R_BC_2000", "R_BC_2000"),
                                      new InputOutputColumnPair("R_BC_4000", "R_BC_4000"),
                                      new InputOutputColumnPair("R_BC_8000", "R_BC_8000"), }
                                  ))
                                  .Append(mlContext.Transforms.Concatenate("Features", new[] {
                                      "L_AC_250",  "L_AC_500",  "L_AC_1000",  "L_AC_2000",  "L_AC_4000",  "L_AC_8000",
                                      "L_BC_250",  "L_BC_500",  "L_BC_1000",  "L_BC_2000",  "L_BC_4000",  "L_BC_8000",
                                      "R_AC_250",  "R_AC_500",  "R_AC_1000",  "R_AC_2000",  "R_AC_4000",  "R_AC_8000",
                                      "R_BC_250",  "R_BC_500",  "R_BC_1000",  "R_BC_2000",  "R_BC_4000",  "R_BC_8000" }
                                  ));


            // SdcaMaximumEntropy() is the multi-class classification training algorithm.
            // Create three trainers: one for each prediction (Config, Degree, and Type).
            var trainers = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(@"Config", "Features")
                              .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedConfig", "PredictedLabel"))
                              .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(@"Degree", "Features"))
                              .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedDegree", "PredictedLabel"))
                              .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(@"Type", "Features"))
                              .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedType", "PredictedLabel"));

            var trainingPipeline = dataPipeline.Append(trainers);

            return trainingPipeline;
        }


        /// <summary>
        /// Fits the training data to the training pipeline.
        /// </summary>
        /// <returns> The model </returns>
        private static ITransformer TrainModel()
        {
            return TrainingPipeline.Fit(TrainingDataView);
        }


        /// <summary>
        /// Evaluates the model using the test dataset.
        /// </summary>
        private static void Evaluate()
        {
            // Load the test data.
            IDataView testDataView = LoadDataFile(TEST_DATA_FILEPATH);

            // Evaluate the model's quality metrics.
            var testMetrics = mlContext.MulticlassClassification.Evaluate(TrainedModel.Transform(testDataView), labelColumnName: @"Type");
            PrintMulticlassClassificationMetrics(testMetrics);
        }



        /**********************************************/
        /*               HELPER METHODS               */
        /**********************************************/

        /// <summary>
        /// Loads the data file into an IDataView object.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>The IDataView object </returns>
        private static IDataView LoadDataFile(string filepath)
        {
            return mlContext.Data.LoadFromTextFile<HearingSetInput>(
                     path: filepath,
                     hasHeader: true,
                     separatorChar: ','
                   );
        }


        /// <summary>
        /// Saves the trained model to a .ZIP file.
        /// </summary>
        private static void SaveModel(DataViewSchema modelInputSchema)
        {
            mlContext.Model.Save(TrainedModel, modelInputSchema, GetAbsolutePath(MODEL_FILE));
        }


        /// <summary>
        /// Uses Path to get the absolute path of a file.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns> The full path </returns>
        private static string GetAbsolutePath(string relativePath)
        {
            FileInfo dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = dataRoot.Directory.FullName;

            return Path.Combine(assemblyFolderPath, relativePath);
        }


        /// <summary>
        /// Prints the micro- and macro-accuracy, log-loss,
        /// and log-loss reduction of the model evaluation.
        /// </summary>
        /// <param name="metrics"></param>
        private static void PrintMulticlassClassificationMetrics(MulticlassClassificationMetrics metrics)
        {
            /*
             * Metrics for Multi-Class Classification:
             * 
             * Micro Accuracy      :  Better if close to 1
             * Macro Accuracy      :  Better if close to 1
             * Log-Loss            :  Better if close to 0
             * Log-Loss Reduction  :  Better if close to 1
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

    }
}