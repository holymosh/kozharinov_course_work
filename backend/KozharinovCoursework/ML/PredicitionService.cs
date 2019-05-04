using System;
using System.Collections.Generic;
using System.IO;
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

        public PredicitionService()
        {
            _mlContext = new MLContext(0);
            _transformer = Train(_mlContext, _trainDataPath);
        }

        public  ITransformer Train(MLContext mlContext, string dataPath)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<MlData>(dataPath, hasHeader: true, separatorChar: ',');
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

        private  void Evaluate(MLContext mlContext, ITransformer model)
        {
            Evaluate(mlContext, model);
            IDataView dataView = mlContext.Data.LoadFromTextFile<MlData>(_testDataPath, hasHeader: true, separatorChar: ',');
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
        }

        private  void TestSinglePrediction(MLContext mlContext)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<MlData, WorkTimePrediction>(_transformer);
        }

    }
}