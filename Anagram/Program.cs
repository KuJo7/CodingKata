using System.Diagnostics;

var file = @"word_list.txt";
var input = File.ReadAllText(file);

var words = input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

var baseWord = "documenting";

Console.WriteLine($"{words.Length} words");

var sw = Stopwatch.StartNew();
var partialMatches = GetPartialAnagramWords(baseWord, words);

var matchCandidates = GetMatchCandidates(partialMatches);
foreach (var (part1, part2) in matchCandidates)
{
    var other = part1 + part2;
    if (IsAnagram(baseWord, other))
    {
        //Console.WriteLine(part1 + " " + part2);
    }
}
sw.Stop();
Console.WriteLine($"{sw.ElapsedMilliseconds}ms ({sw.ElapsedTicks}t)");

static IEnumerable<(string Part1, string Part2)> GetMatchCandidates(IReadOnlyList<string> partialMatches)
{
    for (var i = 0; i < partialMatches.Count; i++)
    {
        for (var j = i + 1; j < partialMatches.Count; j++)
        {
            yield return (partialMatches[i], partialMatches[j]);
        }
    }
}

bool IsAnagram(string word, string other)
{
    return other.OrderBy(e => e).SequenceEqual(word.OrderBy(e => e));
}

bool IsSubSet2(string baseWord, string subWord)
{
    return subWord.All(baseWord.Contains);
}

static List<string> GetPartialAnagramWords(string baseWord, string[] words)
{
    bool IsSubSet(string baseWord, string subWord)
    {
        return !subWord.Except(baseWord).Any();
    }

    return words.Where(w => IsSubSet(baseWord, w)).ToList();
}