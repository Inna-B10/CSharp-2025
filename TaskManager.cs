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


var todoItem = new TodoItem();
Console.WriteLine(todoItem.scheduledAt);

var itemWithNewDate = todoItem + 10;
Console.WriteLine(itemWithNewDate.scheduledAt);


var choice = AnsiConsole.Prompt<string>(
    new SelectionPrompt<string>()
    .Title("[green] Welcome to a personal task manager tool![/]")
    .PageSize(10)
    .AddChoices(
        [
        "List Tasks",
        "Add Task",
        "Mark Task As Complete",
        "Save Tasks To Disk"
        ]
    )
);






public static class TodoItemExtension
{
    extension(TodoItem item)
    {
        public TodoItem MarkAsComplete() => item with { IsComplete = true};

        public TodoItem AddName(string name) => item with {TodoName = name};

        public TodoItem AddDescription(string desc) => item with {Description = desc};
        public  static TodoItem operator +(TodoItem addItem, int days) => addItem with {scheduledAt = addItem.scheduledAt.AddDays(days)};
    }
}

public record TodoItem(DateTime scheduledAt = default, string TodoName = default, string Description = default, bool IsComplete = false);

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(TodoItem))]
[JsonSerializable(typeof(List<TodoItem>))]
public partial class JsonContext : JsonSerializerContext
{
    
}