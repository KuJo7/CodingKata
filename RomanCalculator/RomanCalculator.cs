namespace RomanCalculator;

using System.Text;

// I = 1
// V = 5
// X = 10
// L = 50
// C = 100
// D = 500
// M = 1000
public class RomanCalculator
{
  public string Add(string firstNumber, string secondNumber)
  {
    return Complexify(Combinify(Simplify(firstNumber), Simplify(secondNumber)));
  }

  public string Simplify(string number)
  {
    Console.WriteLine($"{nameof(Simplify)}: '{number}'");
    return number
      .Replace("IV", "IIII")
      .Replace("IX", "VIIII")
      .Replace("XL", "XXXX")
      .Replace("XC", "LXXXX")
      .Replace("CD", "CCCC")
      .Replace("CM", "DCCCC");
  }

  public string Complexify(string number)
  {
    Console.WriteLine($"{nameof(Complexify)}: '{number}'");
    return number
      .Replace("IIIII", "V")
      .Replace("VV", "X")
      .Replace("XXXXX", "L")
      .Replace("LL", "C")
      .Replace("CCCCC", "D")
      .Replace("DD", "M")
      .Replace("DCCCC", "CM")
      .Replace("CCCC", "CD")
      .Replace("LXXXX", "XC")
      .Replace("XXXX", "XL")
      .Replace("VIIII", "IX")
      .Replace("IIII", "IV");
  }

  public string Combinify(string firstNumber, string secondNumber)
  {
    Console.WriteLine($"{nameof(Combinify)}: '{firstNumber}' + '{secondNumber}'");

    var combinedNumber = firstNumber + secondNumber;
    var stringBuilder = new StringBuilder();

    var mdclxvi = "MDCLXVI";
    foreach (var c in mdclxvi)
    {
      foreach (var otherLetter in combinedNumber)
      {
        if (c == otherLetter)
          stringBuilder.Append(c);
      }
    }

    return stringBuilder.ToString();
  }
}