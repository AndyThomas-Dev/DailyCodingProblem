using System;
using System.Collections;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class ClockHands
{

    // Problem #1818
    // This problem was asked by Google.
    // Given a set of points (x, y) on a 2D cartesian plane, find the two closest points.
    // For example, given the points[(1, 1), (-1, -1), (3, 4), (6, 1), (-1, -6), (-4, -3)],
    // return [(-1, -1), (1, 1)].

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please pass a list of coordinates.");
            return;
        }

        string input = args[0];

        Console.WriteLine($"Input: {input}");

        List<(int, int)> coordinates = ParseCoords(input);

        List<(int, int)> result = FindNearest(coordinates);

        Console.WriteLine($"Result: {result[0]}, {result[1]}");

        TestFindNearest();
        TestGetDistance();

        Console.WriteLine(" ");
    }

    // Help function: Convert the string argument to a co-ordinate list
    private static List<(int, int)> ParseCoords(string input)
    {
        List<(int, int)> coordinates = new List<(int, int)>();

        MatchCollection matches = Regex.Matches(input, @"\((-?\d+),\s*(-?\d+)\)");

        foreach (Match match in matches)
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            coordinates.Add((x, y));
        }

        return coordinates;
    }

    private static List<(int, int)> FindNearest(List<(int, int)> points)
    {
        // Sort points
        var sortedByX = points.OrderBy(p => p.Item1).ToList();
        var sortedByY = points.OrderBy(p => p.Item2).ToList();
        return ClosestPair(sortedByX, sortedByY);
    }


    // Divide & Conquer
    // O(n log n)
    private static List<(int, int)> ClosestPair(List<(int, int)> xSorted, List<(int, int)> ySorted)
    {
        int count = xSorted.Count;

        // If small list; faster to do a brute force
        if (count <= 3)
        {
            return BruteForce(xSorted);
        }

        // Get mid point for split
        int mid = count / 2;
        var midpoint = xSorted[mid];

        // Split the points into left and right halves based on the middle point
        var leftX = xSorted.GetRange(0, mid);
        var rightX = xSorted.GetRange(mid, count - mid);

        // Split the ySorted list into two parts: one for points left of the midpoint, one for right
        var leftY = ySorted.Where(p => p.Item1 <= midpoint.Item1).ToList();
        var rightY = ySorted.Where(p => p.Item1 > midpoint.Item1).ToList();

        // Recursively find the closest pair in the left and right halves
        var closestL = ClosestPair(leftX, leftY);
        var closestR = ClosestPair(rightX, rightY);

        double distL = GetDistance(closestL[0], closestL[1]);
        double distR = GetDistance(closestR[0], closestR[1]);

        double minDist = Math.Min(distL, distR);
        var result = distL < distR ? closestL : closestR;

        // Check points in the strip within minDist of the midline
        var strip = ySorted.Where(p => Math.Abs(p.Item1 - midpoint.Item1) < minDist).ToList();

        for (int i = 0; i < strip.Count; i++)
        {
            for (int j = i + 1; j < strip.Count && (strip[j].Item2 - strip[i].Item2) < minDist; j++)
            {
                double d = GetDistance(strip[i], strip[j]);

                // Found new smallest distance - assign
                if (d < minDist)
                {
                    minDist = d;
                    result = new List<(int, int)> { strip[i], strip[j] };
                }
            }
        }

        return SortCordsByLowest(result);
    }

    // Ensure that the pair with the lower sum of coordinates comes first
    // (so the test func knows what to expect)
    private static List<(int, int)> SortCordsByLowest(List<(int, int)> closest)
    {
        if (closest[0].Item1 + closest[0].Item2 > closest[1].Item1 + closest[1].Item2)
        {
            var temp = closest[0];
            closest[0] = closest[1];
            closest[1] = temp;
        }

        return closest;
    }

    // Calc Euclidean Distance
    private static int GetDistance((int, int) first, (int, int) second)
    {
        int dx = first.Item1 - second.Item1;
        int dy = first.Item2 - second.Item2;
        return (int) Math.Sqrt(dx * dx + dy * dy);
    }

    private static List<(int, int)> BruteForce(List<(int, int)> points)
    {
        double minDist = double.MaxValue;
        (int, int) pointA = (0, 0), pointB = (0, 0);

        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                double d = GetDistance(points[i], points[j]);
                if (d < minDist)
                {
                    minDist = d;
                    pointA = points[i];
                    pointB = points[j];
                }
            }
        }

        return SortCordsByLowest(new List<(int, int)> { pointA, pointB });
    }


    // Unit Tests

    private static void TestGetDistance()
    {
        Debug.Assert(GetDistance((0, 0), (0, 0)) == 0);
        Debug.Assert(GetDistance((0, 0), (3, 4)) == 5);
        Debug.Assert(GetDistance((1, 1), (4, 5)) == 5);
        Debug.Assert(GetDistance((1, 1), (1, 1)) == 0);
        Debug.Assert(GetDistance((-1, -1), (-4, -5)) == 5);
        Debug.Assert(GetDistance((5, 5), (10, 10)) == 7);
        Debug.Assert(GetDistance((3, 7), (6, 11)) == 5);
        Debug.Assert(GetDistance((0, 0), (-3, -4)) == 5);
        Debug.Assert(GetDistance((2, 3), (2, 3)) == 0);
        Debug.Assert(GetDistance((100, 100), (104, 103)) == 5);
        Debug.Assert(GetDistance((-2, -3), (-6, -7)) == 5);
        Debug.Assert(GetDistance((10, 0), (10, 10)) == 10);
        Debug.Assert(GetDistance((7, 24), (0, 0)) == 25);
        Debug.Assert(GetDistance((8, 15), (0, 0)) == 17);
        Debug.Assert(GetDistance((1, 2), (4, 6)) == 5);
        Debug.Assert(GetDistance((9, 12), (0, 0)) == 15);
        Debug.Assert(GetDistance((5, 5), (6, 9)) == 4);
        Debug.Assert(GetDistance((3, 4), (7, 1)) == 5);
        Debug.Assert(GetDistance((11, 13), (14, 9)) == 5);
        Debug.Assert(GetDistance((-3, -4), (0, 0)) == 5);

        Console.WriteLine("All GetDistance tests passed.");
    }

    private static void TestFindNearest()
    {
        List<((int, int)[], (int, int)[])> testCases = new()
        {
            // Format: input array, expected pair
            (new[] { (0, 0), (1, 1) }, new[] { (0, 0), (1, 1) }),
            (new[] { (0, 0), (3, 4), (1, 1) }, new[] { (0, 0), (1, 1) }),
            (new[] { (1, 1), (-1, -1), (3, 4), (6, 1), (-1, -6), (-4, -3) }, new[] { (-1, -1), (1, 1) }),
            (new[] { (0, 0), (5, 5), (5, 6) }, new[] { (5, 5), (5, 6) }),
            (new[] { (0, 0), (3, 4), (6, 8) }, new[] { (0, 0), (3, 4) }),
            (new[] { (-3, -4), (-2, -4) }, new[] { (-3, -4), (-2, -4) }),
            (new[] { (0, 0), (2, 2), (3, 3), (4, 5) }, new[] { (2, 2), (3, 3) }),
            (new[] { (1, 1), (3, 4), (2, 3) }, new[] { (3, 4), (2, 3) }),
            (new[] { (10, 10), (10, 11), (20, 20) }, new[] { (10, 10), (10, 11) }),
            (new[] { (0, 0), (1, 0), (5, 5) }, new[] { (0, 0), (1, 0) }),
            (new[] { (-1, -1), (1, 1), (1, 0) }, new[] { (1, 1), (1, 0) }),
            (new[] { (0, 0), (0, 2), (0, 1) }, new[] { (0, 0), (0, 1) }),
            (new[] { (100, 100), (105, 105), (104, 104) }, new[] { (104, 104), (105, 105) }),
            (new[] { (0, 0), (2, 2), (3, 3), (2, 3) }, new[] { (3, 3), (2, 3) }),
            (new[] { (0, 0), (0, 100), (0, 101) }, new[] { (0, 100), (0, 101) }),
            (new[] { (5, 5), (6, 5), (6, 6) }, new[] { (5, 5), (6, 5) }),
            (new[] { (0, 0), (0, 100), (0, 101) }, new[] { (0, 100), (0, 101) }),
        };

        foreach (var (inputPoints, expectedPair) in testCases)
        {
            var actual = FindNearest(inputPoints.ToList());

            bool match = (actual[0] == expectedPair[0] && actual[1] == expectedPair[1])
                         || (actual[0] == expectedPair[1] && actual[1] == expectedPair[0]);

            Debug.Assert(match, $"Failed for input: [{string.Join(", ", inputPoints)}]");
        }

        Console.WriteLine("All FindNearest tests passed.");
    }



}
