#!/usr/bin/env dotnet
#:package Spectre.Console@0.54.0
#:package Spectre.Console.Cli@0.53.1

using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

List<TodoItem> items = [];

string filePath = "./todoitems.json";

if (File.Exists(filePath))
{
    var rawData = await File.ReadAllTextAsync(filePath);
    items = JsonSerializer.Deserialize(rawData, JsonContext.Default.ListTodoItem) ?? [];
}

bool running = true;
do
{
    var choice = AnsiConsole.Prompt<string>(
    new SelectionPrompt<string>()
    .Title("[green] Welcome to a personal task manager tool![/]")
    .PageSize(10)
    .AddChoices(
        [
        "List Tasks",
        "Add Task",
        "Mark Task As Complete",
        "Save Tasks To Disk",
        "Exit"
            ]
        )
    );
    switch (choice)
    {
        case "Add Task":
            await AddAndSaveFile(filePath, items);
            break;
        case "List Tasks":
            items.ForEach(item => AnsiConsole.WriteLine(item.PrettyPrint()));
            break;
        case "Mark Task As Complete":
            MarkItemAsComplete(items);
            await SaveItemsToDisc(filePath, items);
            break;
        default:
            running = false;
            break;
    }
} while (running);

static async Task AddAndSaveFile(string filePath, List<TodoItem> items)
{
    items.Add(
            new TodoItem(DateTime.UtcNow)
            .AddName(AnsiConsole.Ask<string>("What is the name of the task?"))
            .AddDescription(AnsiConsole.Ask<string>("Add the description for the task"))
            );
       await SaveItemsToDisc(filePath, items);
}

static async Task SaveItemsToDisc(string filePath, List<TodoItem> items)
{
    var json = JsonSerializer.Serialize(items, JsonContext.Default.ListTodoItem);
    await File.WriteAllTextAsync(filePath, json); 
}

static void MarkItemAsComplete(List<TodoItem> items)
{
    var item = items.Where(item => 
        item.TodoName == 
        AnsiConsole.Prompt<string>(
            new SelectionPrompt<string>()
            .Title("Which item do you want to mark as complete?")
            .PageSize(10)
            .AddChoices(
                items.Select(item => item.TodoName)
            )
        ) && !item.IsComplete).FirstOrDefault();
        item?.MarkAsComplete();
        Console.WriteLine("Item was marked as complete"); 
}

public static class TodoItemExtension
{
    extension(TodoItem item)
    {
        public TodoItem MarkAsComplete() => item with { IsComplete = true};

        public TodoItem AddName(string name) => item with {TodoName = name};

        public TodoItem AddDescription(string desc) => item with {Description = desc};
        public  static TodoItem operator +(TodoItem addItem, int days) => addItem with {scheduledAt = addItem.scheduledAt.AddDays(days)};

        public string PrettyPrint() => $"[red]{item.TodoName.PadRight(10)}: [yellow]{item.Description.PadLeft(10)}[/]\n{item.IsComplete.ToString().PadRight(10)}: {item.scheduledAt:dddd, dd MMMM yyyy}\n";
    }
}

public record TodoItem(DateTime scheduledAt = default, string TodoName = default, string Description = default, bool IsComplete = false);

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(TodoItem))]
[JsonSerializable(typeof(List<TodoItem>))]
public partial class JsonContext : JsonSerializerContext
{
    
}