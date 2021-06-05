using Microsoft.ML.Data;

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


        [ColumnName("col3"), LoadColumn(3)]
        public int AC_L_250 { get; set; }


        [ColumnName("col4"), LoadColumn(4)]
        public int AC_L_500 { get; set; }


        [ColumnName("col5"), LoadColumn(5)]
        public int AC_L_1000 { get; set; }


        [ColumnName("col6"), LoadColumn(6)]
        public int AC_L_2000 { get; set; }


        [ColumnName("col7"), LoadColumn(7)]
        public int AC_L_4000 { get; set; }


        [ColumnName("col8"), LoadColumn(8)]
        public int AC_L_8000 { get; set; }


        [ColumnName("col9"), LoadColumn(9)]
        public int AC_R_250 { get; set; }


        [ColumnName("col10"), LoadColumn(10)]
        public int AC_R_500 { get; set; }


        [ColumnName("col11"), LoadColumn(11)]
        public int AC_R_1000 { get; set; }


        [ColumnName("col12"), LoadColumn(12)]
        public int AC_R_2000 { get; set; }


        [ColumnName("col13"), LoadColumn(13)]
        public int AC_R_4000 { get; set; }


        [ColumnName("col14"), LoadColumn(14)]
        public int AC_R_8000 { get; set; }


        [ColumnName("col15"), LoadColumn(15)]
        public int BC_L_250 { get; set; }


        [ColumnName("col16"), LoadColumn(16)]
        public int BC_L_500 { get; set; }


        [ColumnName("col17"), LoadColumn(17)]
        public int BC_L_1000 { get; set; }


        [ColumnName("col18"), LoadColumn(18)]
        public int BC_L_2000 { get; set; }


        [ColumnName("col19"), LoadColumn(19)]
        public int BC_L_4000 { get; set; }


        [ColumnName("col20"), LoadColumn(20)]
        public int BC_L_8000 { get; set; }


        [ColumnName("col21"), LoadColumn(21)]
        public int BC_R_250 { get; set; }


        [ColumnName("col22"), LoadColumn(22)]
        public int BC_R_500 { get; set; }


        [ColumnName("col23"), LoadColumn(23)]
        public int BC_R_1000 { get; set; }


        [ColumnName("col24"), LoadColumn(24)]
        public int BC_R_2000 { get; set; }


        [ColumnName("col25"), LoadColumn(25)]
        public int BC_R_4000 { get; set; }


        [ColumnName("col26"), LoadColumn(26)]
        public int BC_R_8000 { get; set; }

    }
}
