using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;

class Program
{
    static async Task Main()
    {
        try
        {
            Console.WriteLine("Initializing chat with Ollama...");
            
            // Build kernel using Ollama
            var builder = Kernel.CreateBuilder();
            builder.AddOllamaChatCompletion(
                modelId: "llama3",
                endpoint: new Uri("http://localhost:11434")
            );

            var kernel = builder.Build();
            var chat = kernel.GetRequiredService<IChatCompletionService>();
            var history = new ChatHistory();
            
            // Add a system message to set the AI's behavior
            history.AddSystemMessage("You are a helpful AI assistant.");

            while (true)
            {
                Console.Write("\nYou: ");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a valid message.");
                    continue;
                }

                // Add user message to history
                history.AddUserMessage(input);

                try
                {
                    var result = await chat.GetChatMessageContentAsync(history);
                    Console.WriteLine($"\nAI: {result}");
                    
                    // Add AI's response to history
                    history.AddAssistantMessage(result.Content ?? "");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
        }
    }
}
