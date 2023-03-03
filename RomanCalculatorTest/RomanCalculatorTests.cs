namespace RomanCalculatorTest;

public class RomanCalculatorTests
{
  [SetUp]
  public void Setup ()
  {
  }

  private static object[] TestCasesForAdd = {
                                         new[] { "I", "I", "II" },
                                         new[] { "X", "X", "XX" },
                                         new[] { "X", "XI", "XXI" },
                                         new[] { "X", "", "X" },
                                     };
  [Test]
  [TestCaseSource(nameof(TestCasesForAdd))]
 
  public void Add (string firstNumber, string secondNumber, string result)
  {
    var romanCalculator = new RomanCalculator.RomanCalculator();
    Assert.That(romanCalculator.Add(firstNumber, secondNumber), Is.EqualTo(result));
  }                                  

[Test]
[TestCaseSource(nameof(TestCasesForAdd))]
 
public void AddInverse (string firstNumber, string secondNumber, string result)
{
  var romanCalculator = new RomanCalculator.RomanCalculator();
  Assert.That(romanCalculator.Add(secondNumber, firstNumber), Is.EqualTo(result));
}

[Test]
[TestCase("IV", "IIII")]
public void Simplify (string number, string result)
{
  var romanCalculator = new RomanCalculator.RomanCalculator();
  Assert.That(romanCalculator.Simplify(number), Is.EqualTo(result));
}



}