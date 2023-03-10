namespace RomanCalculatorTest;

using RomanCalculator;

public class RomanCalculatorTests
{
  [SetUp]
  public void Setup()
  {
  }
  private static string numbers = @"I
II
                                  III
                                  IV
                                  V
                                  VI
                                  VII
                                  VIII
                                  IX
                                  X
                                  XI
                                  XII
                                  XIII
                                  XIV
                                  XV
                                  XVI
                                  XVII
                                  XVIII
                                  XIX
                                  XX
                                  XXI
                                  XXII
                                  XXIII
                                  XXIV
                                  XXV
                                  XXVI
                                  XXVII
                                  XXVIII
                                  XXIX
                                  XXX
                                  XXXI
                                  XXXII
                                  XXXIII
                                  XXXIV
                                  XXXV
                                  XXXVI
                                  XXXVII
                                  XXXVIII
                                  XXXIX
                                  XL
                                  XLI
                                  XLII
                                  XLIII
                                  XLIV
                                  XLV
                                  XLVI
                                  XLVII
                                  XLVIII
                                  XLIX
                                  L
                                  LI
                                  LII
                                  LIII
                                  LIV
                                  LV
                                  LVI
                                  LVII
                                  LVIII
                                  LIX
                                  LX
                                  LXI
                                  LXII
                                  LXIII
                                  LXIV
                                  LXV
                                  LXVI
                                  LXVII
                                  LXVIII
                                  LXIX
                                  LXX
                                  LXXI
                                  LXXII
                                  LXXIII
                                  LXXIV
                                  LXXV
                                  LXXVI
                                  LXXVII
                                  LXXVIII
                                  LXXIX
                                  LXXX
                                  LXXXI
                                  LXXXII
                                  LXXXIII
                                  LXXXIV
                                  LXXXV
                                  LXXXVI
                                  LXXXVII
                                  LXXXVIII
                                  LXXXIX
                                  XC
                                  XCI
                                  XCII
                                  XCIII
                                  XCIV
                                  XCV
                                  XCVI
                                  XCVII
                                  XCVIII
                                  XCIX
                                  C
                                  CI";

  private static object[] TestCasesForAdd =
  {
    new[] { "I", "I", "II" },
    new[] { "X", "X", "XX" },
    new[] { "X", "XI", "XXI" },
    new[] { "X", "", "X" },
    new[] { "III", "II", "V" },
    new[] { "VIII", "III", "XI" },
    new[] { "XLV", "V", "L" },
    //new[] { "", "", "" },
  };

  [Test]
  public void AllOrNothing()
  {
    var numbers2 = numbers.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
      .Select((romanNumber, index) => new { romanNumber = romanNumber.Trim(), index = index + 1 })
      .ToArray();

    var romanCalculator = new RomanCalculator();
    for (var i = 0; i < numbers2.Length; i++)
    {
      for (var i1 = 0; i1 < numbers2.Length; i1++)
      {
        var left = numbers2[i];
        var right = numbers2[i1];

        var result = left.index + right.index;
        if (result >= numbers2.Length)
          continue;
        
        Assert.That(romanCalculator.Add(left.romanNumber, right.romanNumber), Is.EqualTo(numbers2[result - 1].romanNumber));
      }
    }
  }
  
  [Test]
  [TestCaseSource(nameof(TestCasesForAdd))]
  public void Add(string firstNumber, string secondNumber, string result)
  {
    var romanCalculator = new RomanCalculator();
    Assert.That(romanCalculator.Add(firstNumber, secondNumber), Is.EqualTo(result));
  }

  [Test]
  [TestCaseSource(nameof(TestCasesForAdd))]
  public void AddInverse(string firstNumber, string secondNumber, string result)
  {
    var romanCalculator = new RomanCalculator();
    Assert.That(romanCalculator.Add(secondNumber, firstNumber), Is.EqualTo(result));
  }

  [Test]
  [TestCase("IIIII", "V")]
  [TestCase("VV", "X")]
  [TestCase("XXXXX", "L")]
  [TestCase("LL", "C")]
  [TestCase("CCCCC", "D")]
  [TestCase("DD", "M")]
  [TestCase("IIII", "IV")]
  [TestCase("VIIII", "IX")] 
  [TestCase("XXXX", "XL")]
  [TestCase("LXXXX", "XC")]
  [TestCase("CCCC", "CD")]
  [TestCase("DCCCC", "CM")]
  [TestCase("LXXXXVIIII", "XCIX")]
  public void Complexify(string number, string result)
  {
    var romanCalculator = new RomanCalculator();
    Assert.That(romanCalculator.Complexify(number), Is.EqualTo(result));
  }

  [Test]
  [TestCase("IV", "IIII")]
  [TestCase("IX", "VIIII")] 
  [TestCase("XL", "XXXX")]
  [TestCase("XC", "LXXXX")]
  [TestCase("CD", "CCCC")]
  [TestCase("CM", "DCCCC")]
  [TestCase("XCIX", "LXXXXVIIII")]
  public void Simplify(string number, string result)
  {
    var romanCalculator = new RomanCalculator();
    Assert.That(romanCalculator.Simplify(number), Is.EqualTo(result));
  }
}