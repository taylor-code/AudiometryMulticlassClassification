using Microsoft.ML.Data;

namespace AudiometryBinaryClassificationML.Model
{
    public class HearingSetInput
    {
        [ColumnName("col0"), LoadColumn(0)]
        public float Type { get; set; }


        [ColumnName("col1"), LoadColumn(1)]
        public string Degree { get; set; }


        [ColumnName("col2"), LoadColumn(2)]
        public string AC_L_250 { get; set; }


        [ColumnName("col3"), LoadColumn(3)]
        public string AC_L_500 { get; set; }


        [ColumnName("col4"), LoadColumn(4)]
        public string AC_L_1000 { get; set; }


        [ColumnName("col5"), LoadColumn(5)]
        public string AC_L_2000 { get; set; }


        [ColumnName("col6"), LoadColumn(6)]
        public string AC_L_4000 { get; set; }


        [ColumnName("col7"), LoadColumn(7)]
        public string AC_L_8000 { get; set; }


        [ColumnName("col8"), LoadColumn(8)]
        public string AC_R_250 { get; set; }


        [ColumnName("col9"), LoadColumn(9)]
        public string AC_R_500 { get; set; }


        [ColumnName("col10"), LoadColumn(10)]
        public string AC_R_1000 { get; set; }


        [ColumnName("col11"), LoadColumn(11)]
        public string AC_R_2000 { get; set; }


        [ColumnName("col12"), LoadColumn(12)]
        public string AC_R_4000 { get; set; }


        [ColumnName("col13"), LoadColumn(13)]
        public string AC_R_8000 { get; set; }


        [ColumnName("col14"), LoadColumn(14)]
        public string BC_L_250 { get; set; }


        [ColumnName("col15"), LoadColumn(15)]
        public string BC_L_500 { get; set; }


        [ColumnName("col16"), LoadColumn(16)]
        public string BC_L_1000 { get; set; }


        [ColumnName("col17"), LoadColumn(17)]
        public string BC_L_2000 { get; set; }


        [ColumnName("col18"), LoadColumn(18)]
        public string BC_L_4000 { get; set; }


        [ColumnName("col19"), LoadColumn(19)]
        public string BC_L_8000 { get; set; }


        [ColumnName("col20"), LoadColumn(20)]
        public string BC_R_250 { get; set; }


        [ColumnName("col21"), LoadColumn(21)]
        public string BC_R_500 { get; set; }


        [ColumnName("col22"), LoadColumn(22)]
        public string BC_R_1000 { get; set; }


        [ColumnName("col23"), LoadColumn(23)]
        public string BC_R_2000 { get; set; }


        [ColumnName("col24"), LoadColumn(24)]
        public string BC_R_4000 { get; set; }


        [ColumnName("col25"), LoadColumn(25)]
        public string BC_R_8000 { get; set; }

    }
}
