using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.ML.Data;

namespace ML
{
    public class PredictionInput
    {
        [LoadColumn(0)]
        public int VendorId { get; set; }
        [LoadColumn(1)]
        public int TypeId { get; set; }
        [LoadColumn(2)]
        public double WorkTime { get; set; }
        [LoadColumn(3)]
        public double Power { get; set; }
    }

    public class WorkTimePrediction
    {
        [Column("WorkTime")]
        public double WorkTime { get; set; }

        public DateTime ToDateTime()
        {
            var timeSpan = TimeSpan.FromSeconds(WorkTime);
            return DateTime.Now + timeSpan;
        }
    }
}
