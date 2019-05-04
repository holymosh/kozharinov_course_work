using System.Collections.Generic;

namespace ML
{
    public interface IPredictionService
    {
        IEnumerable<WorkTimePrediction> PredictWorkTime(IEnumerable<PredictionInput> datas);
        WorkTimePrediction PredictWorkTime(PredictionInput data);
    }
}