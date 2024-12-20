﻿using eShopForecastModelsTrainer;
using Microsoft.ML;
using static eShopForecastModelsTrainer.ConsoleHelperExt;

namespace Salesforecasting
{
    internal class Program
    {
        private static readonly string ProductDataPath = @"D:\leeyeonjun\git\leeyeonjun85\source\MLDotNet\Salesforecasting\Data\products.stats.csv";

        static void Main(string[] args)
        {
            try
            {
                // This sample shows two different ML tasks and algorithms that can be used for forecasting:
                // 1.) Regression using FastTreeTweedie Regression
                // 2.) Time Series using Single Spectrum Analysis
                // Each of these techniques are used to forecast monthly units for the same products so that you can compare the forecasts.

                var mlContext = new MLContext(seed: 1);  //Seed set to any number so you have a deterministic environment

                ConsoleWriteHeader("Forecast using Regression model");

                RegressionProductModelHelper.TrainAndSaveModel(mlContext, ProductDataPath);
                RegressionProductModelHelper.TestPrediction(mlContext);

                ConsoleWriteHeader("Forecast using Time Series SSA estimation");

                TimeSeriesModelHelper.PerformTimeSeriesProductForecasting(mlContext, ProductDataPath);
            }
            catch (Exception ex)
            {
                ConsoleWriteException(ex.ToString());
            }
            ConsolePressAnyKey();
        }
    }
}