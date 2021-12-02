
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
            switch (c.Dir)
            {
                case Direction.DOWN:
                    y += c.Count;
                    break;
                case Direction.UP:
                    y -= c.Count;
                    break;
                case Direction.FORWARD:
                    x += c.Count;
                    break;
            };
        });
        return x * y;
    }
    public long Solution2()
    {
        long x = 0;
        long y = 0;
        long a = 0;
        commands.ForEach(c =>
        {
            switch (c.Dir)
            {
                case Direction.DOWN:
                    a += c.Count;
                    break;
                case Direction.UP:
                    a -= c.Count;
                    break;
                case Direction.FORWARD:
                    x += c.Count;
                    y += a * c.Count;
                    break;
            }
        });
        return x * y;
    }
    enum Direction
    {
        FORWARD,
        DOWN,
        UP
    }
    record struct SubCommand(Direction Dir, long Count);
}
