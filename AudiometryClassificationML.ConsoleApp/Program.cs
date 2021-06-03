using System;
using System.Diagnostics;

using AudiometryClassificationML.Model;


namespace AudiometryClassificationML.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// After initializing the model, uses it
        /// to predict values for hearing sets.
        /// </summary>
        static void Main()
        {
            InitializeModel();

            PredictTypes(PredictionTestSets.conductiveSet);
            PredictTypes(PredictionTestSets.mixedSet);
            PredictTypes(PredictionTestSets.noneSet);
            PredictTypes(PredictionTestSets.sensorineuralSet);

            Console.WriteLine("\n\nPress any key to quit.");
            Console.ReadKey();
        }


        /// <summary>
        /// Runs the model creation process and
        /// times how long the process takes.
        /// </summary>
        static void InitializeModel()
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            ModelBuilder.CreateModel();
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine($"\n\nElapsed Time: {ts.Minutes}:{ts.Seconds} (Min:Sec)");

            Console.WriteLine("\n============================================\n\n");
        }


        static void PredictTypes(HearingSetInput hearingSet)
        {
            Console.WriteLine("\nUsing model to make a single prediction for the following data:\n");
            PrintDataSet(hearingSet);
            PrintPredictionResults(ConsumeModel.Predict(hearingSet));
        }


        static void PrintDataSet(HearingSetInput hearingSet)
        {
            Console.WriteLine($"Degree:    { hearingSet.Degree }");
            Console.WriteLine($"Config:    { hearingSet.Config }");
            Console.WriteLine($"AC_L_250:  { hearingSet.AC_L_250 }");
            Console.WriteLine($"AC_L_500:  { hearingSet.AC_L_500 }");
            Console.WriteLine($"AC_L_1000: { hearingSet.AC_L_1000 }");
            Console.WriteLine($"AC_L_2000: { hearingSet.AC_L_2000 }");
            Console.WriteLine($"AC_L_4000: { hearingSet.AC_L_4000 }");
            Console.WriteLine($"AC_L_8000: { hearingSet.AC_L_8000 }");
            Console.WriteLine($"AC_R_250:  { hearingSet.AC_R_250 }");
            Console.WriteLine($"AC_R_500:  { hearingSet.AC_R_500 }");
            Console.WriteLine($"AC_R_1000: { hearingSet.AC_R_1000 }");
            Console.WriteLine($"AC_R_2000: { hearingSet.AC_R_2000 }");
            Console.WriteLine($"AC_R_4000: { hearingSet.AC_R_4000 }");
            Console.WriteLine($"AC_R_8000: { hearingSet.AC_R_8000 }");
            Console.WriteLine($"BC_L_250:  { hearingSet.BC_L_250 }");
            Console.WriteLine($"BC_L_500:  { hearingSet.BC_L_500 }");
            Console.WriteLine($"BC_L_1000: { hearingSet.BC_L_1000 }");
            Console.WriteLine($"BC_L_2000: { hearingSet.BC_L_2000 }");
            Console.WriteLine($"BC_L_4000: { hearingSet.BC_L_4000 }");
            Console.WriteLine($"BC_L_8000: { hearingSet.BC_L_8000 }");
            Console.WriteLine($"BC_R_250:  { hearingSet.BC_R_250 }");
            Console.WriteLine($"BC_R_500:  { hearingSet.BC_R_500 }");
            Console.WriteLine($"BC_R_1000: { hearingSet.BC_R_1000 }");
            Console.WriteLine($"BC_R_2000: { hearingSet.BC_R_2000 }");
            Console.WriteLine($"BC_R_4000: { hearingSet.BC_R_4000 }");
            Console.WriteLine($"BC_R_8000: { hearingSet.BC_R_8000 }");
        }


        static void PrintPredictionResults(HearingSetOutput prediction)
        {
            Console.WriteLine($"\n*******************************************");
            Console.WriteLine($"*       Metrics for Single Prediction      ");
            Console.WriteLine($"*------------------------------------------");
            Console.WriteLine($"*   Predicted Type: {prediction.Prediction}");
            Console.WriteLine($"*   Predicted Type Scores:");
            Console.WriteLine($"*       Conductive    = {prediction.Score[0]:0.######}");
            Console.WriteLine($"*       Mixed         = {prediction.Score[1]:0.######}");
            Console.WriteLine($"*       None          = {prediction.Score[2]:0.######}");
            Console.WriteLine($"*       Sensorineural = {prediction.Score[3]:0.######}");
            Console.WriteLine($"*******************************************");
        }
    }
}