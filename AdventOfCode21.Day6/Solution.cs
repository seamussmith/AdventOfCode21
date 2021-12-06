
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
                if (fishMemo.ContainsKey((9, i)))
                    fishies += fishMemo[(9, i)];
                else
                    fishies += (fishMemo[(9, i)] = SimulateFishies(9, i));
            }
        }
        return fishies;
    }
    public long Solution2()
    {
        return initalPopulation.Aggregate((long)0, (a, b) => a + SimulateFishies(b, 256));
    }
}
