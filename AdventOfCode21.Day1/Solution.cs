
namespace AdventOfCode21.Day1;

public class Solution
{
    List<int> reports;
    public Solution(string input)
    {
        reports = input.Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
    }
    public long Solution1()
    {
        var inc = 0;
        for (var i = 1; i < reports.Count(); ++i)
        {
            if (reports[i - 1] < reports[i])
                ++inc;
        }
        return inc;
    }
    public long Solution2()
    {
        var inc = 0;
        int? last = null;
        for (var i = 2; i < reports.Count(); ++i)
        {
            var curr = reports[i - 2] + reports[i - 1] + reports[i];
            if (last is not null && curr > last)
                ++inc;
            last = curr;
        }
        return inc;
    }
}
