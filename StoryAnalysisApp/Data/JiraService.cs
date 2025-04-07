public class JiraService
{
    public Task<List<JiraIssue>> GetPastSprintStories()
    {
        return Task.FromResult(new List<JiraIssue>
        {
            new JiraIssue
            {
                Key = "ST-1001",
                Fields = new Fields
                {
                    Summary = "Create login form with validation",
                    AcceptanceCriteria = "User enters credentials and sees errors if invalid",
                    StoryPoints = 3
                }
            },
            new JiraIssue
            {
                Key = "ST-1002",
                Fields = new Fields
                {
                    Summary = "Integrate payment gateway",
                    AcceptanceCriteria = "User can complete payment using credit card",
                    StoryPoints = 5
                }
            }
        });
    }
}