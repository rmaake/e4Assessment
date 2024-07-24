// See https://aka.ms/new-console-template for more information

int Add(string input)
{
    input = input.Trim();
    if (string.IsNullOrEmpty(input))
        return 0;

    List<int> numbers = GetSanitizedIntValues(input);
    
    if (numbers.Count(val =>  val < 0) != 0)
    {
        var negativeNumbers = String.Join(", ", numbers.Where(val => val < 0).Select(num => num.ToString()).ToArray());
        string errMessage = $"negatives not allowed: {negativeNumbers}";
        throw new Exception(errMessage);
    }

    return numbers.Sum();
}

List<int> GetSanitizedIntValues(string input)
{
    int skipItems;
    char delimiter = GetDelimiter(input, out skipItems)[0];
    input = input.Substring(skipItems);
    string[] values = input.Split(delimiter, '\n').Select(x => x.Trim()).ToArray();
    values = values.Where(x => !string.IsNullOrEmpty(x)).ToArray();

    return values.Select(str => int.Parse(str.Trim())).ToList();
}

string GetDelimiter(string input, out int startIndex)
{
    int delimiterIndex = input.LastIndexOf('/') + 1;
    int firstNewLineIndex = input.IndexOf("\n");
    if (delimiterIndex == 0)
    {
        startIndex = 0;
        return ",";
    }
    string value = input.Substring(delimiterIndex, firstNewLineIndex - delimiterIndex);
    startIndex = firstNewLineIndex;
    return !string.IsNullOrEmpty(value) ? value.Trim() : ",";
}

Console.WriteLine("Welcome to the String Calculator...\nPlease add your input in the form: //[delimiter]\\n[numbers...]. When done, enter Ctrl + Z");
try
{
    var input = Console.In.ReadToEnd();
    int sum = Add(input);
    Console.WriteLine($"Total: {sum}");
}
catch (FormatException _)
{
    Console.WriteLine("Invalid input");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

