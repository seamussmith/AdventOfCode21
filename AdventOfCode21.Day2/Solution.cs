
namespace AdventOfCode21.Day2;

public class Solution
{
    List<SubCommand> commands;
    public Solution(string input)
    {
        commands = input.Split("\n").Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split(" "))
                .Select(x => new SubCommand(x[0] switch
                {
                    "forward" => Direction.FORWARD,
                    "down" => Direction.DOWN,
                    "up" => Direction.UP,
                    _ => throw new Exception()
                }, long.Parse(x[1])))
                .ToList();
    }
    public long Solution1()
    {
        long x = 0;
        long y = 0;
        commands.ForEach(c =>
        {
            var z = c.Dir switch
            {
                Direction.DOWN => y += c.Count,
                Direction.UP => y -= c.Count,
                Direction.FORWARD => x += c.Count,
            };
        });
        return x * y;
    }
    public long Solution2()
    {
        throw new NotImplementedException("Solution 2 is not implemented yet");
    }
    enum Direction
    {
        FORWARD,
        DOWN,
        UP
    }
    record struct SubCommand(Direction Dir, long Count);
}
