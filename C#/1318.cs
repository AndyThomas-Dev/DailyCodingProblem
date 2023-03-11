using NUnit.Framework;

namespace MainProj;

class Program
{
    // Also known as Orderly Queue problem on Leetcode
    // You are given a string of length N and a parameter k.The string can be manipulated by taking one of the first k letters and moving it to the end.

    // Write a program to determine the lexicographically smallest string that can be created after an unlimited number of moves.

    // For example, suppose we are given the string daily and k = 1.The best we can create in this case is ailyd.

    static void Main(string[] args)
    {

        try
        {
            string result = MutateString("daily", 1);
            Assert.That(result == "ailyd");

            result = MutateString("cba", 1);
            Assert.That(result == "acb");

            result = MutateString("accde", 1);
            Assert.That(result == "accde");

            result = MutateString("ccdea", 1);
            Assert.That(result == "accde");

            result = MutateString("zzzzzzA", 6);
            Assert.That(result == "Azzzzzz");

            result = MutateString("zzazzzA", 6);
            Assert.That(result == "Aazzzzz");

            result = MutateString("zzazzzA", 4);
            Assert.That(result == "Aazzzzz");
        }
        catch (Exception ex)
        {
            throw;
        }


    }

    private static string MutateString(string input, int k)
    {

        if(k == 1)
        {
            String result = input;

            for (int i = 0; i < input.Length; ++i)
            {
                String temp = input.Substring(i) + input.Substring(0, i);

                if (temp.CompareTo(result) < 0)
                {
                    result = temp;
                }
            }

            return result;
        }
        // When k > 1, the answer is input written in lexicographic order.
        else
        {
            return new string(input.OrderBy(c => c).ToArray());
        }

    }
}
