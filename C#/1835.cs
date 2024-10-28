using NUnit.Framework;
namespace MainProj;

class Program
{
    // # Problem #1835
    // Compute the running median of a sequence of numbers.That is, given a stream of numbers, print out the median of the list so far on each new element.
    // Recall that the median of an even-numbered list is the average of the two middle numbers.

    // For example, given the sequence[2, 1, 5, 7, 2, 0, 5], your algorithm should print out:

    //2
    //1.5
    //2
    //3.5
    //2
    //2
    //2

    static void Main(string[] args)
    {

        bool allTestsPassed = true;

        try
        {
            int[] input = [2, 1, 5, 7, 2, 0, 5];
            decimal[] output = [2, 1.5M, 2, 3.5M, 2, 2, 2];

            decimal[] result = GetMedianArray(input);
            Console.WriteLine($"[{string.Join(", ", result)}]");

            Assert.That(result, Is.EqualTo(output));
        }
        catch (Exception ex)
        {
            allTestsPassed = false;
            Console.WriteLine($"Test failed: {ex.Message}");
        }

        if (allTestsPassed)
        {
            Console.WriteLine("All tests passed.");
        }
        else
        {
            Console.WriteLine("Some tests failed.");
        }


    }

    private static decimal[] GetMedianArray(int[] input)
    {
        List<decimal> result = new List<decimal>();
        List<decimal> dummyArray = new List<decimal>();

        // Example: Add elements to the list
        foreach (decimal value in input)
        {
            dummyArray.Add(value);
            dummyArray.Sort();
            decimal medianValue = CalculateMedian(dummyArray.ToArray());  // Replace with actual median calculation logic
            result.Add(medianValue);
        }

        return result.ToArray();
    }

    private static decimal CalculateMedian(decimal[] input)
    {
        bool isEvenArray = input.Length % 2 == 0 ? true : false;

        if(isEvenArray)
        {
            decimal top = input[input.Length / 2];
            decimal bottom = input[(input.Length / 2) - 1];

            return bottom + ((top - bottom) / 2);
        }

        return input[input.Length/2];
    }
}
