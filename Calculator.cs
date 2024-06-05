namespace Calculator;
class Calculator
{
    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // default value is "not a number" if an operation such as division, could result in a error
        switch(op)
        {
            case "a":
                result = num1 + num2;
                break;
            case "s":
                result = num1 - num2;
                break;
            case "m":
                result = num1 * num2;
                break;
            case "d":
                //ask user to enter a non zero divisor
                if(num2 != 0)
                {
                    result = num1 / num2;
                }
                //return text for in incorrect option entry
                break;
        }
        return result;
    }
}