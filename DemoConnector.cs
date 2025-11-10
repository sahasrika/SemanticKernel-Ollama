using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;
using System;
using System.Threading.Tasks;

namespace SemanticKernelOllama
{
    class DemoConnector
    {
        static async Task Main()
        {
            var kernel = Kernel.CreateBuilder()
                .AddOllamaChatCompletion("llama3.1:8b", "http://localhost:11434")
                .Build();

            Console.Write("Enter your prompt: ");
            var prompt = Console.ReadLine();

            var result = await kernel.InvokePromptAsync(prompt);
            Console.WriteLine($"\nOllama Response:\n{result}");
        }
    }
}
