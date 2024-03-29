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
        /// to predict values for hearing instances.
        /// </summary>
        public static void Main()
        {
            ModelBuilder.CreateModel();

            List<HearingInstanceInput> predInstances = ReadPredictionCSV();

            // Time how long each prediction takes.
            foreach (var instance in predInstances)
            {
                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                PredictLabels(instance);
                stopWatch.Stop();

                Console.WriteLine($"\n\nPrediction Time: {stopWatch.ElapsedMilliseconds} (ms)");
            }

            Console.WriteLine("\n\nPress any key to quit.");
            Console.ReadKey();
        }

      
        /// <summary>
        /// Reads in the instances to predict.
        /// </summary>
        /// <returns> A list of HearingInstanceInput instances. </returns>
        public static List<HearingInstanceInput> ReadPredictionCSV()
        {
            List<HearingInstanceInput> values = File.ReadAllLines(@"..\..\..\Data\AudiometryPred.csv")
                                               .Skip(1)
                                               .Select(v => HearingInstanceInput.ReadFromCSV(v))
                                               .ToList();
            return values;
        }


        private static void PredictLabels(HearingInstanceInput instance)
        {
            Console.WriteLine("\nUsing model to make predictions for the following data:\n");
            PrintDataSet(instance);
            PrintPredictionResults(ConsumeModel.Predict(instance));
        }


        private static void PrintDataSet(HearingInstanceInput instance)
        {
            Console.WriteLine($"L_AC_250:  { instance.L_AC_250 }");
            Console.WriteLine($"L_AC_500:  { instance.L_AC_500 }");
            Console.WriteLine($"L_AC_1000: { instance.L_AC_1000 }");
            Console.WriteLine($"L_AC_2000: { instance.L_AC_2000 }");
            Console.WriteLine($"L_AC_4000: { instance.L_AC_4000 }");
            Console.WriteLine($"L_AC_8000: { instance.L_AC_8000 }");
            Console.WriteLine($"L_BC_250:  { instance.L_BC_250 }");
            Console.WriteLine($"L_BC_500:  { instance.L_BC_500 }");
            Console.WriteLine($"L_BC_1000: { instance.L_BC_1000 }");
            Console.WriteLine($"L_BC_2000: { instance.L_BC_2000 }");
            Console.WriteLine($"L_BC_4000: { instance.L_BC_4000 }");
            Console.WriteLine($"L_BC_8000: { instance.L_BC_8000 }");
            Console.WriteLine($"R_AC_250:  { instance.R_AC_250 }");
            Console.WriteLine($"R_AC_500:  { instance.R_AC_500 }");
            Console.WriteLine($"R_AC_1000: { instance.R_AC_1000 }");
            Console.WriteLine($"R_AC_2000: { instance.R_AC_2000 }");
            Console.WriteLine($"R_AC_4000: { instance.R_AC_4000 }");
            Console.WriteLine($"R_AC_8000: { instance.R_AC_8000 }");
            Console.WriteLine($"R_BC_250:  { instance.R_BC_250 }");
            Console.WriteLine($"R_BC_500:  { instance.R_BC_500 }");
            Console.WriteLine($"R_BC_1000: { instance.R_BC_1000 }");
            Console.WriteLine($"R_BC_2000: { instance.R_BC_2000 }");
            Console.WriteLine($"R_BC_4000: { instance.R_BC_4000 }");
            Console.WriteLine($"R_BC_8000: { instance.R_BC_8000 }");
            Console.WriteLine("\n----------------------------------");
            Console.WriteLine("    Desired Prediction Labels:    ");
            Console.WriteLine($"Type:      { instance.Type }");
            Console.WriteLine($"Degree:    { instance.Degree }");
            Console.WriteLine($"Config:    { instance.Config }");
            Console.WriteLine("----------------------------------\n");
        }


        private static void PrintPredictionResults(HearingInstanceOutput prediction)
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