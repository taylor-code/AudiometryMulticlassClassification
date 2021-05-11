using System;
using AudiometryBinaryClassificationML.Model;

namespace AudiometryBinaryClassificationML.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            ModelBuilder.CreateModel();

            Console.WriteLine("\n\n============================================\n\n");

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

            Console.WriteLine("Using model to make a single prediction for the following data:\n");
            PrintDataSet(sampleData);

            var predictionResult = ConsumeModel.Predict(sampleData);
            PrintPredictionResults(predictionResult);

            Console.WriteLine("\n\nPress any key to quit.");
            Console.ReadKey();
        }


        static void PrintDataSet(HearingSetInput dataObj)
        {
            Console.WriteLine($"Degree:    { dataObj.Degree }");
            Console.WriteLine($"AC_L_250:  { dataObj.AC_L_250 }");
            Console.WriteLine($"AC_L_500:  { dataObj.AC_L_500 }");
            Console.WriteLine($"AC_L_1000: { dataObj.AC_L_1000 }");
            Console.WriteLine($"AC_L_2000: { dataObj.AC_L_2000 }");
            Console.WriteLine($"AC_L_4000: { dataObj.AC_L_4000 }");
            Console.WriteLine($"AC_L_8000: { dataObj.AC_L_8000 }");
            Console.WriteLine($"AC_R_250:  { dataObj.AC_R_250 }");
            Console.WriteLine($"AC_R_500:  { dataObj.AC_R_500 }");
            Console.WriteLine($"AC_R_1000: { dataObj.AC_R_1000 }");
            Console.WriteLine($"AC_R_2000: { dataObj.AC_R_2000 }");
            Console.WriteLine($"AC_R_4000: { dataObj.AC_R_4000 }");
            Console.WriteLine($"AC_R_8000: { dataObj.AC_R_8000 }");
            Console.WriteLine($"BC_L_250:  { dataObj.BC_L_250 }");
            Console.WriteLine($"BC_L_500:  { dataObj.BC_L_500 }");
            Console.WriteLine($"BC_L_1000: { dataObj.BC_L_1000 }");
            Console.WriteLine($"BC_L_2000: { dataObj.BC_L_2000 }");
            Console.WriteLine($"BC_L_4000: { dataObj.BC_L_4000 }");
            Console.WriteLine($"BC_L_8000: { dataObj.BC_L_8000 }");
            Console.WriteLine($"BC_R_250:  { dataObj.BC_R_250 }");
            Console.WriteLine($"BC_R_500:  { dataObj.BC_R_500 }");
            Console.WriteLine($"BC_R_1000: { dataObj.BC_R_1000 }");
            Console.WriteLine($"BC_R_2000: { dataObj.BC_R_2000 }");
            Console.WriteLine($"BC_R_4000: { dataObj.BC_R_4000 }");
            Console.WriteLine($"BC_R_8000: { dataObj.BC_R_8000 }");
        }


        static void PrintPredictionResults(HearingSetOutput prediction)
        {
            Console.WriteLine($"\n*******************************************");
            Console.WriteLine($"*       Metrics for Single Prediction      ");
            Console.WriteLine($"*------------------------------------------");
            Console.WriteLine($"*   Predicted Type: {GetTextFromValue(prediction.Prediction)}");
            Console.WriteLine($"*   Predicted Type Scores:");
            Console.WriteLine($"*       Conductive    = {prediction.Score[0]:0.######}");
            Console.WriteLine($"*       Sensorineural = {prediction.Score[1]:0.######}");
            Console.WriteLine($"*******************************************");
        }


        static string GetTextFromValue(float valueF)
        {
            return (int)valueF == 0 ? "Conductive" : "Sensorineural";
        }
    }
}