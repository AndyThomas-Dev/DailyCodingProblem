using System;
using System.Collections;
using System.Linq;

public class ReverseBits
{

    static void Main(string[] args)
    {   
      Console.WriteLine(FlipBits(ConvertToBinary(args[0])));
    }

    private static string ConvertToBinary(string input)
    {

      string output = "";

      foreach (char c in input)
      {
        output += Convert.ToString(c, 2).PadLeft(8, '0');
      }

      // Console.WriteLine(output);
      return output;
    }

    private static string FlipBits(string input)
    {

      string reversed = "";

      foreach (char c in input)
      {
        char r = c == '1' ? '0' : '1';
        reversed += Convert.ToString(r);
      }

      return reversed;
    }
}
