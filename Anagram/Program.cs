// See https://aka.ms/new-console-template for more information

var file = @"C:\Users\joel.kudiyirickal\Downloads\word_list.txt";
var input = File.ReadAllText(file);

var words = input.Split(new char[]{ ' ', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

var baseWord = "documenting";

var word = "acrobat";


var partialMatches = words.Where(w => IsSubSet(baseWord, w)).ToList();

for (int i = 0; i < partialMatches.Count; i++)
{
  for (int j = i + 1; j < partialMatches.Count; j++)
  {
    if (IsAnagram(baseWord, partialMatches[i], partialMatches[j]))
    {
      Console.WriteLine(partialMatches[i] + partialMatches[j]);
    }
  }
}

bool IsAnagram(string word, string left, string right)
{
  return left.Concat(right).OrderBy(e => e).SequenceEqual(word.OrderBy(e => e));
}

bool IsSubSet2(string baseWord, string subWord)
{
  return subWord.All(baseWord.Contains);
}

bool IsSubSet(string baseWord, string subWord)
{
  return !subWord.Except(baseWord).Any();
}

