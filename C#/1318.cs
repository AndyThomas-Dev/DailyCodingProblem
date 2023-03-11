using NUnit.Framework;

namespace MainProj;

class Program
{
    // Daily Coding Problem: Problem #1318
    // You are given a string of length N and a parameter k.The string can be manipulated by taking one of the first k letters and moving it to the end.

    // Write a program to determine the lexicographically smallest string that can be created after an unlimited number of moves.

    // For example, suppose we are given the string daily and k = 1.The best we can create in this case is ailyd.

    static void Main(string[] args)
    {

        try
        {
            string result = MutateString("daily", 1);
            Assert.That(result == "ailyd");

            result = MutateString("dailyyz", 1);
            Assert.That(result == "ailyyzd");

            result = MutateString("daily", 2);
            Assert.That(result == "ilyda");

            result = MutateString("zzzzzzA", 6);
            Assert.That(result == "Azzzzzz");
        }
        catch(Exception ex)
        {
            throw;
        }


    }

    private static string MutateString(string input, int k)
    {
        return input.Substring(k) + input.Substring(0, k);
    }
}