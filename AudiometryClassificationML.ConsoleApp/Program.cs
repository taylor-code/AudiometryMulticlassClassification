using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using AudiometryClassificationML.Model;


namespace AudiometryClassificationML.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// After initializing the model, uses it
        /// to predict values for hearing sets.
        /// </summary>
        public static void Main()
        {
            InitializeModel();

            List<HearingSetInput> predInstances = ReadPredictionCSV();

            foreach (var instance in predInstances)
            {
                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                PredictLabels(instance);
                stopWatch.Stop();
                Console.WriteLine($"\n\nElapsed Time: {stopWatch.ElapsedMilliseconds} (ms)");
            }

            Console.WriteLine("\n\nPress any key to quit.");
            Console.ReadKey();
        }


        /// <summary>
        /// Runs the model creation process and
        /// times how long the process takes.
        /// </summary>
        private static void InitializeModel()
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            ModelBuilder.CreateModel();
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine($"\n\nElapsed Time: {ts.Minutes}:{ts.Seconds} (Min:Sec)");

            Console.WriteLine("\n============================================\n\n");
        }
      
      
        /// <summary>
        /// Reads in the instances to predict.
        /// </summary>
        /// <returns> A list of HearingSetInput instances. </returns>
        public static List<HearingSetInput> ReadPredictionCSV()
        {
            List<HearingSetInput> values = File.ReadAllLines(@"..\..\..\Data\AudiometryPred.csv")
                                               .Skip(1)
                                               .Select(v => HearingSetInput.ReadFromCSV(v))
                                               .ToList();
            return values;
        }


        private static void PredictLabels(HearingSetInput hearingSet)
        {
            Console.WriteLine("\nUsing model to make predictions for the following data:\n");
            PrintDataSet(hearingSet);
            PrintPredictionResults(ConsumeModel.Predict(hearingSet));
        }


        private static void PrintDataSet(HearingSetInput instance)
        {
            Console.WriteLine($"AC_L_250:  { instance.AC_L_250 }");
            Console.WriteLine($"AC_L_500:  { instance.AC_L_500 }");
            Console.WriteLine($"AC_L_1000: { instance.AC_L_1000 }");
            Console.WriteLine($"AC_L_2000: { instance.AC_L_2000 }");
            Console.WriteLine($"AC_L_4000: { instance.AC_L_4000 }");
            Console.WriteLine($"AC_L_8000: { instance.AC_L_8000 }");
            Console.WriteLine($"AC_R_250:  { instance.AC_R_250 }");
            Console.WriteLine($"AC_R_500:  { instance.AC_R_500 }");
            Console.WriteLine($"AC_R_1000: { instance.AC_R_1000 }");
            Console.WriteLine($"AC_R_2000: { instance.AC_R_2000 }");
            Console.WriteLine($"AC_R_4000: { instance.AC_R_4000 }");
            Console.WriteLine($"AC_R_8000: { instance.AC_R_8000 }");
            Console.WriteLine($"BC_L_250:  { instance.BC_L_250 }");
            Console.WriteLine($"BC_L_500:  { instance.BC_L_500 }");
            Console.WriteLine($"BC_L_1000: { instance.BC_L_1000 }");
            Console.WriteLine($"BC_L_2000: { instance.BC_L_2000 }");
            Console.WriteLine($"BC_L_4000: { instance.BC_L_4000 }");
            Console.WriteLine($"BC_L_8000: { instance.BC_L_8000 }");
            Console.WriteLine($"BC_R_250:  { instance.BC_R_250 }");
            Console.WriteLine($"BC_R_500:  { instance.BC_R_500 }");
            Console.WriteLine($"BC_R_1000: { instance.BC_R_1000 }");
            Console.WriteLine($"BC_R_2000: { instance.BC_R_2000 }");
            Console.WriteLine($"BC_R_4000: { instance.BC_R_4000 }");
            Console.WriteLine($"BC_R_8000: { instance.BC_R_8000 }");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("    Desired Prediction Labels:    ");
            Console.WriteLine($"Type:      { instance.Type }");
            Console.WriteLine($"Degree:    { instance.Degree }");
            Console.WriteLine($"Config:    { instance.Config }");
        }


        private static void PrintPredictionResults(HearingSetOutput prediction)
        {
            Console.WriteLine($"\n*******************************************");
            Console.WriteLine($"*             Prediction Metrics             ");
            Console.WriteLine($"*------------------------------------------");
            Console.WriteLine($"*   Predicted Type:   {prediction.TypePrediction}");
            Console.WriteLine($"*   Predicted Degree: {prediction.DegreePrediction}");
            Console.WriteLine($"*   Predicted Config: {prediction.ConfigPrediction}");
            Console.WriteLine($"*******************************************");
        }
    }
}