List<string> names = [
    "John",
    "Jørgen",
    "Lars Gunnar",
    "Jørund"
    ];

string newName = "Jarand";

var originalName = names.Where(name => name == "Jørund").FirstOrDefault();

originalName = newName;

List<People> people = [
    new ("Jørgen", 30),
    new ("John", 33),
    new ("Lars Gunnar", 35),
    new ("Jørund", 30)
];

var personWhoShouldChangeName = people.Where(person => person.Name == "Theodor").FirstOrDefault();

personWhoShouldChangeName?.Name = newName;



public class People(string name, int age)
{
    public string Name = name;
    public int Age = age;
};

