
namespace AdventOfCode21.Day6;

public class Solution
{
    readonly IReadOnlyList<short> initalPopulation; 
    public Solution(string input)
    {
        initalPopulation = input.Split(",").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => short.Parse(x)).ToList();
    }
    public long Solution1()
    {
        return initalPopulation.Aggregate((long)0, (a, b) => a + SimulateFishies(b, 80));
    }
    static readonly Dictionary<(int, int), long> fishMemo = new();
    static long SimulateFishies(int breedTime, int days)
    {
        long fishies = 1;
        for (var i = days; i > 0; --i)
        {
            breedTime -= 1;
            if (breedTime < 0)
            {
                breedTime = 6;
                if (fishMemo.ContainsKey((8, i)))
                    fishies += fishMemo[(8, i)];
                else
                    fishies += (fishMemo[(8, i)] = SimulateFishies(8, i));
            }
        }
        return fishies;
    }
    public long Solution2()
    {
        throw new NotImplementedException("Solution 2 is not implemented yet");
    }
}
