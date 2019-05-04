using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.ML.Data;

namespace ML
{
    public class MlData
    {
        [LoadColumn(0)]
        public int VendorId { get; set; }
        [LoadColumn(1)]
        public int TypeId { get; set; }
        [LoadColumn(2)]
        public float WorkTime { get; set; }
    }

    public class WorkTimePrediction
    {
        [Column("WorkTime")]
        public float WorkTime { get; set; }
    }
}
