using Microsoft.ML.Data;
using System;

namespace AudiometryClassificationML.Model
{
    public class HearingSetInput
    {
        [ColumnName("Type"), LoadColumn(0)]
        public string Type { get; set; }


        [ColumnName("Degree"), LoadColumn(1)]
        public string Degree { get; set; }


        [ColumnName("Config"), LoadColumn(2)]
        public string Config { get; set; }


        [ColumnName("L_AC_250"), LoadColumn(3)]
        public int L_AC_250 { get; set; }


        [ColumnName("L_AC_500"), LoadColumn(4)]
        public int L_AC_500 { get; set; }


        [ColumnName("L_AC_1000"), LoadColumn(5)]
        public int L_AC_1000 { get; set; }


        [ColumnName("L_AC_2000"), LoadColumn(6)]
        public int L_AC_2000 { get; set; }


        [ColumnName("L_AC_4000"), LoadColumn(7)]
        public int L_AC_4000 { get; set; }


        [ColumnName("L_AC_8000"), LoadColumn(8)]
        public int L_AC_8000 { get; set; }


        [ColumnName("L_BC_250"), LoadColumn(9)]
        public int L_BC_250 { get; set; }


        [ColumnName("L_BC_500"), LoadColumn(10)]
        public int L_BC_500 { get; set; }


        [ColumnName("L_BC_1000"), LoadColumn(11)]
        public int L_BC_1000 { get; set; }


        [ColumnName("L_BC_2000"), LoadColumn(12)]
        public int L_BC_2000 { get; set; }


        [ColumnName("L_BC_4000"), LoadColumn(13)]
        public int L_BC_4000 { get; set; }


        [ColumnName("L_BC_8000"), LoadColumn(14)]
        public int L_BC_8000 { get; set; }


        [ColumnName("R_AC_250"), LoadColumn(15)]
        public int R_AC_250 { get; set; }


        [ColumnName("R_AC_500"), LoadColumn(16)]
        public int R_AC_500 { get; set; }


        [ColumnName("R_AC_1000"), LoadColumn(17)]
        public int R_AC_1000 { get; set; }


        [ColumnName("R_AC_2000"), LoadColumn(18)]
        public int R_AC_2000 { get; set; }


        [ColumnName("R_AC_4000"), LoadColumn(19)]
        public int R_AC_4000 { get; set; }


        [ColumnName("R_AC_8000"), LoadColumn(20)]
        public int R_AC_8000 { get; set; }


        [ColumnName("R_BC_250"), LoadColumn(21)]
        public int R_BC_250 { get; set; }


        [ColumnName("R_BC_500"), LoadColumn(22)]
        public int R_BC_500 { get; set; }


        [ColumnName("R_BC_1000"), LoadColumn(23)]
        public int R_BC_1000 { get; set; }


        [ColumnName("R_BC_2000"), LoadColumn(24)]
        public int R_BC_2000 { get; set; }


        [ColumnName("R_BC_4000"), LoadColumn(25)]
        public int R_BC_4000 { get; set; }


        [ColumnName("R_BC_8000"), LoadColumn(26)]
        public int R_BC_8000 { get; set; }
      
      
        public static HearingSetInput ReadFromCSV(string csvLine)
        {
            string[] values = csvLine.Split(',');

            return new HearingSetInput()
            {
                Type      = values[0],
                Degree    = values[1],
                Config    = values[2],
                L_AC_250  = Int32.Parse(values[3]),
                L_AC_500  = Int32.Parse(values[4]),
                L_AC_1000 = Int32.Parse(values[5]),
                L_AC_2000 = Int32.Parse(values[6]),
                L_AC_4000 = Int32.Parse(values[7]),
                L_AC_8000 = Int32.Parse(values[8]),
                L_BC_250  = Int32.Parse(values[9]),
                L_BC_500  = Int32.Parse(values[10]),
                L_BC_1000 = Int32.Parse(values[11]),
                L_BC_2000 = Int32.Parse(values[12]),
                L_BC_4000 = Int32.Parse(values[13]),
                L_BC_8000 = Int32.Parse(values[14]),
                R_AC_250  = Int32.Parse(values[15]),
                R_AC_500  = Int32.Parse(values[16]),
                R_AC_1000 = Int32.Parse(values[17]),
                R_AC_2000 = Int32.Parse(values[18]),
                R_AC_4000 = Int32.Parse(values[19]),
                R_AC_8000 = Int32.Parse(values[20]),
                R_BC_250  = Int32.Parse(values[21]),
                R_BC_500  = Int32.Parse(values[22]),
                R_BC_1000 = Int32.Parse(values[23]),
                R_BC_2000 = Int32.Parse(values[24]),
                R_BC_4000 = Int32.Parse(values[25]),
                R_BC_8000 = Int32.Parse(values[26])
            };
        }

    }
}