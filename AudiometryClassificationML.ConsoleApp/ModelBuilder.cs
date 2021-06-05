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
        public static IEstimator<ITransformer> BuildTrainingPipeline()
        {
            var dataPipeline = mlContext.Transforms.Conversion.MapValueToKey(new[] {
                                      new InputOutputColumnPair("Type",   "Type"),
                                      new InputOutputColumnPair("Degree", "Degree"),
                                      new InputOutputColumnPair("Config", "Config")
                                  })
                                  // OneHotEncoding() assigns each category a unique value.
                                  .Append(mlContext.Transforms.Categorical.OneHotEncoding(new[] {
                                      new InputOutputColumnPair("col3",  "col3"),
                                      new InputOutputColumnPair("col4",  "col4"),
                                      new InputOutputColumnPair("col5",  "col5"),
                                      new InputOutputColumnPair("col6",  "col6"),
                                      new InputOutputColumnPair("col7",  "col7"),
                                      new InputOutputColumnPair("col8",  "col8"),
                                      new InputOutputColumnPair("col9",  "col9"),
                                      new InputOutputColumnPair("col10", "col10"),
                                      new InputOutputColumnPair("col11", "col11"),
                                      new InputOutputColumnPair("col12", "col12"),
                                      new InputOutputColumnPair("col13", "col13"),
                                      new InputOutputColumnPair("col14", "col14"),
                                      new InputOutputColumnPair("col15", "col15"),
                                      new InputOutputColumnPair("col16", "col16"),
                                      new InputOutputColumnPair("col17", "col17"),
                                      new InputOutputColumnPair("col18", "col18"),
                                      new InputOutputColumnPair("col19", "col19"),
                                      new InputOutputColumnPair("col20", "col20"),
                                      new InputOutputColumnPair("col21", "col21"),
                                      new InputOutputColumnPair("col22", "col22"),
                                      new InputOutputColumnPair("col23", "col23"),
                                      new InputOutputColumnPair("col24", "col24"),
                                      new InputOutputColumnPair("col25", "col25"),
                                      new InputOutputColumnPair("col26", "col26") }
                                  ))
                                  .Append(mlContext.Transforms.Concatenate("Features", new[] {
                                      "col3",  "col4",  "col5",  "col6",  "col7",  "col8",
                                      "col9",  "col10", "col11", "col12", "col13", "col14",
                                      "col15", "col16", "col17", "col18", "col19", "col20",
                                      "col21", "col22", "col23", "col24", "col25", "col26" }
                                  ));


            // SdcaMaximumEntropy() is the multi-class classification training algorithm.
            // Create two trainers: one for the Type column and one for the Degree column.
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
        public static ITransformer TrainModel()
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
        public static string GetAbsolutePath(string relativePath)
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
        public static void PrintMulticlassClassificationMetrics(MulticlassClassificationMetrics metrics)
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