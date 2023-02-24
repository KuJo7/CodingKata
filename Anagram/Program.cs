using System.Diagnostics;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Xml;

var file = @"word_list.txt";
var input = File.ReadAllText(file);

var words = input.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

var baseWord = "documenting";

Console.WriteLine($"{words.Length} words");

var sw = Stopwatch.StartNew();

var baseWordLetterMask = ConvertWordToLetterMask(baseWord);

Span<char> baseWordDuplicateLetterStorage = stackalloc char[32];
var baseWordDuplicateLetters = new StackList<char>(baseWordDuplicateLetterStorage);
{
    var tempMask = baseWordLetterMask;
    RecordDuplicateLetters(ref baseWordDuplicateLetters, ref tempMask, baseWord.AsSpan());
}
baseWordDuplicateLetters.Sort();

// Convert words into numbers
var backlinks = new int[words.Length];
var letterMasks = new int[words.Length];
var lengths = new int[words.Length];
var newWordCount = 0;
for (var i = 0; i < words.Length; i++)
{
    var word = words[i];
    if (word.Length > baseWord.Length)
        continue;

    var number = ConvertWordToLetterMask(word);

    // Remove words that don't contain a subset of letters of the base word
    if ((baseWordLetterMask | number) != baseWordLetterMask)
        continue;

    backlinks[newWordCount] = i;
    letterMasks[newWordCount] = number;
    lengths[newWordCount] = word.Length;
    newWordCount++;
}

Span<char> listStorage = stackalloc char[32];
var duplicateLetters = new StackList<char>(listStorage);

var results = new List<(string, string)>(1024);

var baseWordLetterMaskVector = Vector256.Create(
    baseWordLetterMask,
    baseWordLetterMask,
    baseWordLetterMask,
    baseWordLetterMask,
    baseWordLetterMask,
    baseWordLetterMask,
    baseWordLetterMask,
    baseWordLetterMask);
var letterMasksSpan = new ReadOnlySpan<int>(letterMasks);
for (var i = 0; i < newWordCount; i++)
{
    
  // var iLetterMask = letterMasks[i];
  // var iVector = Vector256.Create(iLetterMask, iLetterMask, iLetterMask, iLetterMask, iLetterMask, iLetterMask, iLetterMask, iLetterMask);
  // for (var j = i + 1; j < newWordCount; j += 8)
  int j = i + 1;
  while(j < newWordCount)
  {
    var jSpan = letterMasksSpan.Slice(j);
    
    // var jVector = Vector256.Create(letterMasks, j);

    // Both words together use the same letters as the base word
    // var bitwiseOrResult = Vector256.BitwiseOr(iVector, jVector);
    // if (!Vector256.EqualsAny(bitwiseOrResult, baseWordLetterMaskVector))
    //   continue;
    // for (int k = 0; k < 8; k++)
    {
      // if (bitwiseOrResult.GetElement(k) == baseWordLetterMask)
      {
        //if ((letterMasks[i] | letterMasks[j]) != baseWordLetterMask)
        //continue;

        // The length of both words matches the length of the base word
        if (lengths[i] + lengths[j] == baseWord.Length)
        {
          var left = words[backlinks[i]];
          var right = words[backlinks[j]];

          if (baseWordDuplicateLetters.Length == 0)
          {
            // its a match
            results.Add((left, right));
          }
          else
          {
            duplicateLetters.Length = 0;

            var combinedMask = baseWordLetterMask;
            RecordDuplicateLetters(ref duplicateLetters, ref combinedMask, left.AsSpan());
            RecordDuplicateLetters(ref duplicateLetters, ref combinedMask, right.AsSpan());

            if (baseWordDuplicateLetters.Length == 1)
            {
              if (duplicateLetters.Length == 1 && duplicateLetters[0] == baseWordDuplicateLetters[0])
              {
                // its a match
                results.Add((left, right));
              }
            }
            else
            {
              duplicateLetters.Sort();
              if (baseWordDuplicateLetters.Span.SequenceEqual(duplicateLetters.Span))
              {
                // its a match
                results.Add((left, right));
              }
            }
          }
        }
      }
    }
  }
}

sw.Stop();

foreach (var result in results)
{
    Console.WriteLine($"{result.Item1} {result.Item2}");
}

Console.WriteLine($"{results.Count} matches");
Console.WriteLine($"Reference: 12ms");
Console.WriteLine($"{sw.ElapsedMilliseconds}ms ({sw.ElapsedTicks}t)");

static int ConvertWordToLetterMask(ReadOnlySpan<char> value)
{
    var result = 0;
    for (var i = 0; i < value.Length; i++)
        result |= 1 << (value[i] - 'a');

    return result;
}

static void RecordDuplicateLetters(ref StackList<char> duplicateLetters, ref int mask, ReadOnlySpan<char> value)
{
    for (var i = 0; i < value.Length; i++)
    {
        var c = value[i];
        var letterMask = 1 << (c - 'a');
        if ((mask & letterMask) != 0)
        {
            // First occurence of the letter -> remove from mask
            mask ^= letterMask;
        }
        else
        {
            duplicateLetters.Add(c);
        }
    }
}

public ref struct StackList<T>
{
    private readonly Span<T> _storage;

    private int _length;

    public Span<T> Span => _storage[.._length];

    public StackList(Span<T> storage)
    {
        _storage = storage;
    }

    public int Length
    {
        get => _length;
        set => _length = value;
    }

    public void Add(T value)
    {
        _storage[_length++] = value;
    }

    public void Sort()
    {
        Span.Sort();
    }

    public ref T this[int index] => ref _storage[index];
}