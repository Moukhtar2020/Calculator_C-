using System.Collections;
using System.Diagnostics;
using System.Reflection.Metadata;
using Newtonsoft.Json; 

namespace CalculatorLibrary
{
    
public class Calculator
{
    JsonWriter writer;
    ArrayList information = new ArrayList();
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }   
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // default value is "not a number" if an operation such as division, could result in a error
         writer.WriteStartObject();
         writer.WritePropertyName("Operand1");
         writer.WriteValue(num1);
         writer.WritePropertyName("Operand2");
         writer.WriteValue(num2);
         writer.WritePropertyName("Operation");
         // Use a switch statement to do the math.
        switch(op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                //ask user to enter a non zero divisor
                if(num2 != 0)
                {
                    result = num1 / num2;            
                }
                writer.WriteValue("Divide");
                break;
                //return text for in incorrect option entry
                default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        information.Add(result);
        return result;
    }
    public void printResults()
    {
    
        foreach(var view in information)
        {
            if(view != null)
            {
                System.Console.WriteLine($"Previous Results: {view}");   
            }
            else
            {
                System.Console.WriteLine("Storage is empty, there is nothing to view");
            }             
        }
    }

    public void deleteResults()
    {
        foreach(var view in information)
        {
            if(view != null)
            {
                information.Remove(view);
                System.Console.WriteLine("Previous results deleted!");
            }
            else
            {
                System.Console.WriteLine("Storage is empty, there is nothing to remove");
            }
        }
    }

 

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}
}