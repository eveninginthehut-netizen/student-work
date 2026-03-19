using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
namespace AL
{
    internal class Program
    {
        //Входные данные
        public class SentimentData
        {
            [LoadColumn(0)]//Атрибут обозначающий поле, которое будет загружаться из первой колонки источника данных 
            public string Text { get; set; } 
            [LoadColumn(1),ColumnName("Label")]//Поле будет загружаться из 2-ой колонки источника данных 
            public bool IsPositive { get; set; }
        }
        //Результат предсказания
        public class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]//Поле будет содержать предсказанное значение 
            public bool Prediction { get; set; }
            public float Probability { get; set;  }
            public float Score { get;set;  }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Привет!/n Начался анализ тональности текста...");
            // Шаг 1 - Создаем контекст Ml
            var mlContext=new MLContext();
            //Шаг 2 - Подготавливаем данные 
            var data = new List<SentimentData>
            {
                    new SentimentData { Text = "Это отличный фильм!",IsPositive=true},
                    new SentimentData { Text = "Ужасная еда",IsPositive=false},
                    new SentimentData { Text = "Прекрасная погода",IsPositive=true},
                    new SentimentData { Text = "Плохой сервис",IsPositive=false},
                    new SentimentData { Text = "Мне очент понравилось",IsPositive=true},
                    new SentimentData { Text = "Разочарован покупкой",IsPositive=false}
            };
            var trainData=mlContext.Data.LoadFromEnumerable(data);
            //шаг 3 - создаем последовательность операций
            var pipeline = mlContext.Transforms.Text.FeaturizeText(
                inputColumnName: "Text",
                outputColumnName: "Features"
                ).Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                    labelColumnName: "Features",
                    featureColumnName: "Features"));
            //Шаг 4 - Обучение модели
            Console.WriteLine("Обучение модели....");
            var model=pipeline.Fit(trainData);
            //Шаг 5 - создаем объект для быстрого предсказания
            var predictor = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
            //шаг 6 - Тестирование 
            var testText = new[]
            {
                "Это замечательный день",
                "Ужасное качество",
                "Обычный товар"
            };
            Console.WriteLine("Результаты анализа: ");
            foreach(var text in testText)
            {
                var prediction = predictor.Predict(new SentimentData { Text=text });
                Console.WriteLine($"\nТекст:{text}");
                Console.WriteLine($"\nТональность:{(prediction.Prediction ? "Позитивная" : "Негативная")}");
                Console.WriteLine($"Уверенность: {prediction.Probability * 100:F1}%");
                Console.WriteLine();
            }

        }
    }
}
