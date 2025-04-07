using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;

public class StoryData
{
    public string Summary { get; set; }
    public float ComplexityScore { get; set; }
}

public class StoryPrediction
{
    [ColumnName("Score")]
    public float PredictedComplexity { get; set; }
}

public class MLModelService
{
    private readonly MLContext _mlContext;
    private PredictionEngine<StoryData, StoryPrediction> _predictionEngine;

    public MLModelService()
    {
        _mlContext = new MLContext();

        var trainingData = _mlContext.Data.LoadFromEnumerable(new List<StoryData>
        {
            new StoryData { Summary = "Create login form", ComplexityScore = 2 },
            new StoryData { Summary = "Implement payment gateway", ComplexityScore = 5 },
            new StoryData { Summary = "Show error messages for form", ComplexityScore = 3 },
        });

        var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(StoryData.Summary))
            .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(StoryData.ComplexityScore), featureColumnName: "Features"));

        var model = pipeline.Fit(trainingData);
        _predictionEngine = _mlContext.Model.CreatePredictionEngine<StoryData, StoryPrediction>(model);
    }

    public float PredictComplexity(string summary)
    {
        var prediction = _predictionEngine.Predict(new StoryData { Summary = summary });
        return (float)Math.Round(prediction.PredictedComplexity, 2);
    }

    public string AnalyzeAcceptanceCriteria(string ac)
    {
        if (string.IsNullOrWhiteSpace(ac))
            return "❌ AC is empty or missing.";

        bool hasVerb = ac.Split(' ').Any(word => word.EndsWith("s") || word.EndsWith("ed"));
        bool hasExpectedOutcome = ac.ToLower().Contains("should") || ac.ToLower().Contains("must");

        if (!hasVerb || !hasExpectedOutcome)
            return "⚠️ AC might be vague. Add actions and expectations.";

        return "✅ AC looks clear.";
    }
    
}