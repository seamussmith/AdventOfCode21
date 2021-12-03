
namespace AdventOfCode21.Day3;

public class Solution
{
    readonly IReadOnlyList<string> bits;
    public Solution(string input)
    {
        bits = input.Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
    public long Solution1()
    {
        var comm = GetCommonBits(bits);
        var majorBits = bits.Count() / 2;
        var gamma = Convert.ToInt64(comm.Aggregate("", (a, b) => a + (b >= majorBits ? "1" : "0")), 2);
        var epsilon = Convert.ToInt64(comm.Aggregate("", (a, b) => a + (b <= majorBits ? "1" : "0")), 2);

        return gamma * epsilon;
    }
    public long[] GetCommonBits(IReadOnlyList<string> arr)
    {
        var comm = new long[arr[0].Length];
        var majorBits = arr.Count() / 2;
        foreach (var b in arr)
        {
            for (var i = 0; i != b.Length; ++i)
                if (b[i] == '1')
                    comm[i] += 1;
        }
        return comm;
    }
    public long Solution2()
    {
        long o2rating;
        long co2rating;
        var nums = bits.Select(x => x).ToList();
        var iter = 0;
        while (nums.Count() != 1)
        {
            var comm = GetCommonBits(nums);
            var majorBits = Math.Ceiling(((double)nums.Count()) / 2);
            if (comm[iter] >= majorBits)
                nums = nums.Where(x => x[iter] == '1').ToList();
            else
                nums = nums.Where(x => x[iter] == '0').ToList();
            ++iter;
        }
        o2rating = Convert.ToInt64(nums.First(), 2);
        nums = bits.Select(x => x).ToList();
        iter = 0;
        while (nums.Count() != 1)
        {
            var comm = GetCommonBits(nums);
            var majorBits =  Math.Ceiling(((double)nums.Count()) / 2);
            if (comm[iter] >= majorBits)
                nums = nums.Where(x => x[iter] == '0').ToList();
            else
                nums = nums.Where(x => x[iter] == '1').ToList();
            ++iter;
        }
        co2rating = Convert.ToInt64(nums.First(), 2);

        return o2rating * co2rating;
    }
}
