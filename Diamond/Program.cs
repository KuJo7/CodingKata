var c = 'C';

var value = c - 'A';

printCharacters(value);

void printCharacters(int charValue)
{
    for (var i = 0; i <= charValue; i++)
    {
        PrintCharacter(charValue, i);
    }

    for (var i = charValue - 1; i >= 0; i--)
    {
        PrintCharacter(charValue, i);
    }
}

void PrintCharacter(int charValue, int i)
{
    for (var j = 0; j < charValue - i; j++)
    {
        Console.Write(' ');
    }

    var character = (char) ('A' + i);
    if (i != 0)
    {
        Console.Write(character);
        for (var j = 0; j < 2 * (i - 1) + 1; j++)
        {
            Console.Write(' ');
        }
    }
    
    Console.WriteLine(character);
}