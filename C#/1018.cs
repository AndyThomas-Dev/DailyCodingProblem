using System;
using System.Collections;
using System.Linq;
using System.Diagnostics;

public class ClockHands
{
    
    // Provided a time, calculate the angle between hour hand and minute hand
    // Good checker/help site: https://maniacs.info/clock-angles/angle-between-hour-and-minute-hand-at-1-05.html
    // Must be formtted: (hh:mm) ie. 02:30

    public static void Main(string[] args)
    {   
      // Console.WriteLine(FlipBits(ConvertToBinary(args[0])));
      int hh = Int32.Parse(args[0].Substring(0,2));
      int mm = Int32.Parse(args[0].Substring(3,2));
      
      Testing();

      Console.WriteLine("Hour hand angle:");   
      Console.WriteLine(ConvertHours(hh, mm));

      Console.WriteLine("Minute hand angle:");   
      Console.WriteLine(ConvertMins(mm));

      Console.WriteLine("Angle from hour hand to minute hand:");  
      Console.Write(CalculateAngle(ConvertHours(hh, mm), ConvertMins(mm)));

      Console.WriteLine(" ");  
    }

    private static void Testing()
    {
        Debug.Assert(ConvertHours(1,00) == 30);
        Debug.Assert(ConvertHours(2,00) == 60);
        Debug.Assert(ConvertHours(10,00) == 270);
        Debug.Assert(ConvertHours(11,00) == 300);
        Debug.Assert(ConvertHours(11,01) == 306);
        Debug.Assert(ConvertHours(11,03) == 318);

        Debug.Assert(ConvertMins(1) == 6);
        Debug.Assert(ConvertMins(2) == 12);
        Debug.Assert(ConvertMins(5) == 30);
        Debug.Assert(ConvertMins(59) == 354);

        Debug.Assert(CalculateAngle(2,55) == 242.5);
        Debug.Assert(CalculateAngle(2,10) == 355);
    }

    private static double ConvertHours(int hh, int mm)
    {
      int totalMins = (hh * 60) + mm;
      // Console.WriteLine(totalMins);
      return (double) totalMins * 0.5;
    }

    private static double ConvertMins(int mm)
    {
      return (double) mm * 6;
    }

    private static double CalculateAngle(double angleH, double angleM)
    {
        if(angleM > angleH){ return angleM - angleH; }
        else{ return (360 - (angleH - angleM)); }
        
    }

}