// StoryClarityEvaluator.cs
namespace StoryAnalysisApp.Analysis
{
    public class StoryClarityEvaluator
    {
        private static readonly string[] VerbEndings = { "s", "ed" };
        private static readonly string[] ExpectationKeywords = { "should", "must", "will", "shall", "expected to", "required to", "needs to", "have to" };
        private static readonly string[] ConditionTriggers = { "when", "if", "after", "upon" };
        private static readonly string[] PurposeIndicators = { "so that", "in order to", "because" };

        public static (int Score, string Message) Evaluate(string ac)
        {
            if (string.IsNullOrWhiteSpace(ac))
                return (0, "❗ AC is empty or undefined.");

            bool hasVerb = ac.Split(' ')
                             .Any(word => VerbEndings.Any(end => word.ToLower().EndsWith(end)));

            bool hasExpectedOutcome = ExpectationKeywords
                                      .Any(keyword => ac.ToLower().Contains(keyword));

            bool hasCondition = ConditionTriggers
                                .Any(trigger => ac.ToLower().Contains(trigger));

            bool hasPurpose = PurposeIndicators
                              .Any(purpose => ac.ToLower().Contains(purpose));

            int clarityScore = 0;
            if (hasVerb) clarityScore++;
            if (hasExpectedOutcome) clarityScore++;
            if (hasCondition || hasPurpose) clarityScore++;

            string status = clarityScore switch
            {
                3 => "✅ AC looks clear and actionable.",
                2 => "⚠️ AC might be slightly vague. Consider clarifying condition or purpose.",
                1 => "⚠️ AC is vague. Add expected outcomes or context.",
                _ => "❗ AC is vague. Add actions, outcomes, and context."
            };

            return (clarityScore, status);
        }
    }
}
