using Adventure.Contexts;
using OpenAI.Chat;

class Program
{
    private static string apiKey = ""; // Add your API key here
    private static readonly string model = "gpt-4";

    static async Task Main(string[] args)
    {
        Console.Write("Please enter your OpenAI API Key: ");
        apiKey = Console.ReadLine() ?? String.Empty;
        var client = new ChatClient(model, apiKey);
        Console.WriteLine("You approach the castle gates, your summons from the king in your hand. A large castle guard eyes you suspicously and moves to bar your way inside.");
        var messages = new List<ChatMessage>()
        {
            new SystemChatMessage(CastleGuardContext.SystemPrompt)
        };

        while (true)
        {
            Console.Write("You: ");
            string? userInput = Console.ReadLine();
            if (userInput == null)
                continue;

            if (userInput.ToLower() == "exit")
                break;

            messages.Add(new UserChatMessage(userInput));
            var chatCompletion = await client.CompleteChatAsync(messages);
            Console.WriteLine($"Guard: {chatCompletion.Value.Content.LastOrDefault()?.Text}");
            messages.Add(new AssistantChatMessage(chatCompletion.Value.Content.LastOrDefault()?.Text));
        }
    }
}
