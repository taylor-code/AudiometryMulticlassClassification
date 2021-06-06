using AudiometryClassificationML.Model;

namespace AudiometryClassificationML.ConsoleApp
{
    class PredictionTestSets
    {
        public static readonly HearingSetInput conductiveSet = new HearingSetInput()
        {
            //Type      = "Conductive",
            //Degree    = "Mild",
            //Config    = "Bilateral | Symmetrical",
            AC_L_250  = 30,
            AC_L_500  = 25,
            AC_L_1000 = 40,
            AC_L_2000 = 25,
            AC_L_4000 = 30,
            AC_L_8000 = 30,
            AC_R_250  = 40,
            AC_R_500  = 30,
            AC_R_1000 = 35,
            AC_R_2000 = 30,
            AC_R_4000 = 35,
            AC_R_8000 = 25,
            BC_L_250  = 0,
            BC_L_500  = 5,
            BC_L_1000 = 5,
            BC_L_2000 = 5,
            BC_L_4000 = 5,
            BC_L_8000 = 10,
            BC_R_250  = 5,
            BC_R_500  = 5,
            BC_R_1000 = 10,
            BC_R_2000 = 0,
            BC_R_4000 = 5,
            BC_R_8000 = 10
        };


        public static readonly HearingSetInput mixedSet = new HearingSetInput()
        {
            //Type      = "Mixed",
            //Degree    = "AC: Profound & BC: Severe",
            //Config    = "Bilateral | Symmetrical",
            AC_L_250  = 95,
            AC_L_500  = 95,
            AC_L_1000 = 95,
            AC_L_2000 = 95,
            AC_L_4000 = 95,
            AC_L_8000 = 90,
            AC_R_250  = 90,
            AC_R_500  = 95,
            AC_R_1000 = 95,
            AC_R_2000 = 95,
            AC_R_4000 = 90,
            AC_R_8000 = 100,
            BC_L_250  = 75,
            BC_L_500  = 80,
            BC_L_1000 = 75,
            BC_L_2000 = 85,
            BC_L_4000 = 85,
            BC_L_8000 = 85,
            BC_R_250  = 85,
            BC_R_500  = 85,
            BC_R_1000 = 70,
            BC_R_2000 = 75,
            BC_R_4000 = 85,
            BC_R_8000 = 85
        };


        public static readonly HearingSetInput noneSet = new HearingSetInput()
        {
            //Type      = "None",
            //Degree    = "Normal",
            //Config    = "None",
            AC_L_250  = 0,
            AC_L_500  = 5,
            AC_L_1000 = 5,
            AC_L_2000 = 5,
            AC_L_4000 = 5,
            AC_L_8000 = 5,
            AC_R_250  = 0,
            AC_R_500  = 0,
            AC_R_1000 = 0,
            AC_R_2000 = 5,
            AC_R_4000 = 5,
            AC_R_8000 = 5,
            BC_L_250  = 10,
            BC_L_500  = 5,
            BC_L_1000 = 5,
            BC_L_2000 = 5,
            BC_L_4000 = 5,
            BC_L_8000 = 5,
            BC_R_250  = 0,
            BC_R_500  = 0,
            BC_R_1000 = 0,
            BC_R_2000 = 5,
            BC_R_4000 = 5,
            BC_R_8000 = 10
        };


        public static readonly HearingSetInput sensorineuralSet = new HearingSetInput()
        {
            //Type      = "Sensorineural",
            //Degree    = "Moderate",
            //Config    = "Bilateral | Symmetrical",
            AC_L_250  = 40,
            AC_L_500  = 45,
            AC_L_1000 = 55,
            AC_L_2000 = 45,
            AC_L_4000 = 45,
            AC_L_8000 = 45,
            AC_R_250  = 50,
            AC_R_500  = 50,
            AC_R_1000 = 40,
            AC_R_2000 = 45,
            AC_R_4000 = 45,
            AC_R_8000 = 45,
            BC_L_250  = 50,
            BC_L_500  = 45,
            BC_L_1000 = 45,
            BC_L_2000 = 55,
            BC_L_4000 = 45,
            BC_L_8000 = 55,
            BC_R_250  = 50,
            BC_R_500  = 50,
            BC_R_1000 = 50,
            BC_R_2000 = 45,
            BC_R_4000 = 45,
            BC_R_8000 = 50
        };


        public static readonly HearingSetInput lowFrequencySet = new HearingSetInput()
        {
            //Type      = "Left: Sensorineural & Right: Mixed",
            //Degree    = "Left: Mild & Right: AC: Slight | BC: Mild",
            //Config    = "Low-Frequency | Bilateral",
            AC_L_250  = 40,
            AC_L_500  = 30,
            AC_L_1000 = 20,
            AC_L_2000 = 20,
            AC_L_4000 = 15,
            AC_L_8000 = 5,
            AC_R_250  = 30,
            AC_R_500  = 30,
            AC_R_1000 = 25,
            AC_R_2000 = 15,
            AC_R_4000 = 15,
            AC_R_8000 = 10,
            BC_L_250  = 35,
            BC_L_500  = 30,
            BC_L_1000 = 25,
            BC_L_2000 = 15,
            BC_L_4000 = 10,
            BC_L_8000 = 15,
            BC_R_250  = 40,
            BC_R_500  = 30,
            BC_R_1000 = 25,
            BC_R_2000 = 15,
            BC_R_4000 = 10,
            BC_R_8000 = 10
        };

    }
}
