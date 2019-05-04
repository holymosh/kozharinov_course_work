using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.ML;

namespace ML
{
    public class PredicitionService : IPredictionService
    {
        static readonly string _trainDataPath =
            Path.Combine(Environment.CurrentDirectory, "Data", "equipment-train.csv");

        static readonly string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "equipment-test.csv");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");
        private readonly ITransformer _transformer;
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<PredictionInput, WorkTimePrediction> _predictionEngine;
        

        public PredicitionService()
        {
            _mlContext = new MLContext(0);
            _transformer = Train(_mlContext, _trainDataPath);
            Evaluate(_mlContext,_transformer);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<PredictionInput, WorkTimePrediction>(_transformer);
        }

        public ITransformer Train(MLContext mlContext, string dataPath)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<PredictionInput>(dataPath, hasHeader: true, separatorChar: ',');
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "FareAmount")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "VendorIdEncoded",
                    inputColumnName: "VendorId"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RateCodeEncoded",
                    inputColumnName: "RateCode"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "PaymentTypeEncoded",
                    inputColumnName: "PaymentType")).Append(mlContext.Transforms.Concatenate("Features",
                    "VendorIdEncoded", "RateCodeEncoded", "PassengerCount", "TripTime", "TripDistance",
                    "PaymentTypeEncoded")).Append(mlContext.Regression.Trainers.OnlineGradientDescent());
            var model = pipeline.Fit(dataView);
            return model;
        }

        private void Evaluate(MLContext mlContext, ITransformer model)
        {
            Evaluate(mlContext, model);
            IDataView dataView =
                mlContext.Data.LoadFromTextFile<PredictionInput>(_testDataPath, hasHeader: true, separatorChar: ',');
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
        }

        private void TestSinglePrediction(MLContext mlContext)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<PredictionInput, WorkTimePrediction>(_transformer);
            predictionFunction.Predict(new PredictionInput {TypeId = 1, VendorId = 1, WorkTime = 2000});
        }

        public WorkTimePrediction PredictWorkTime(PredictionInput data)
        {
            return _predictionEngine.Predict(data);
        }

        public IEnumerable<WorkTimePrediction> PredictWorkTime(IEnumerable<PredictionInput> datas)
        {
            return datas.Select(data => _predictionEngine.Predict(data));
        }
    }
}