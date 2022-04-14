using System;
using System.Collections;
using System.Linq;

public class ReverseBits
{
  static void Main(string[] args)
  {

    string output = "";
    
    foreach (char c in args[0]){
      output += Convert.ToString(c, 2).PadLeft(8, '0');
    }

    // Console.WriteLine(output);

    string reversed = "";

    foreach (char c in output){
      char r = c == '1' ? '0' : '1';
      reversed += Convert.ToString(r);
    }
      
    Console.WriteLine(reversed);

  }
}
