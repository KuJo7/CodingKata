namespace RomanCalculator;

public class RomanCalculator
{
  public string Add (string firstNumber, string secondNumber)
  {
    return Complexify(Combinify(Simplify(firstNumber), Simplify(secondNumber)));
  }

  public string Simplify(string number)
  {

    return number;
  }

  public string Complexify(string number)
  {
    return number;
  }

  public string Combinify(string firstNumber, string secondNumber)
  {
    var digits = new char[firstNumber.Length + secondNumber.Length];

    int firstIndex = 0;
    int secondIndex = 0;
    for (int i = 0; i < digits.Length; i++)
    {
      if (secondIndex >= secondNumber.Length
          || firstIndex < firstNumber.Length && firstNumber[firstIndex] == secondNumber[secondIndex])
      {
        digits[i] = firstNumber[firstIndex];
        firstIndex++;
        //if (firstNumber[i] == secondNumber[i])
        // {
        //   digits[i + 1] = secondNumber[i];
        //   i++;
        // }
      }
      else
      {
        digits[i] = secondNumber[secondIndex];
        secondIndex++;
      }
    }
    return new string(digits);
  }
}