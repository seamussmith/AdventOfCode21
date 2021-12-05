
namespace AdventOfCode21.Day5;

public class Solution
{
    readonly IReadOnlyList<Line> lines;
    public Solution(string input)
    {
        lines = input.Split(Environment.NewLine)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Split(" -> "))
            .Select(x => new Line(
                new Point(long.Parse(x[0].Split(",")[0]), long.Parse(x[0].Split(",")[1])),
                new Point(long.Parse(x[1].Split(",")[0]), long.Parse(x[1].Split(",")[1]))
            ))
            .ToList();
    }
    public long Solution1()
    {
        // (foreshadowing...)
        // Grab all straight lines then aggregate their length
        var validLines = lines.Where(x => (x.PointA.X == x.PointB.X) ^ (x.PointA.Y == x.PointB.Y)).ToList();
        // An 8 megabyte large array of hits...
        // https://www.youtube.com/watch?v=DNeikvoqQ-s
        var grid = new long[1000, 1000];

        foreach (var i in validLines)
        {
            for (var x = i.PointA.X;
                i.PointA.X <= i.PointB.X ? x <= i.PointB.X : x >= i.PointB.X;
                x += i.PointA.X > i.PointB.X ? -1 : 1)
                for (var y = i.PointA.Y;
                    i.PointA.Y <= i.PointB.Y ? y <= i.PointB.Y : y >= i.PointB.Y;
                    y += i.PointA.Y > i.PointB.Y ? -1 : 1)
                {
                    grid[x, y] += 1;
                }
        }

        return grid.Cast<long>().Where(x => x > 1).Select(x => x - 1).Sum();
    }

    public long Solution2()
    {
        throw new NotImplementedException("Solution 2 is not implemented yet");
    }
    record struct Point
    {
        public long X { get; init; }
        public long Y { get; init; }
        // Note to self: A getter property that returns the record itself will cause the ToString method to overflow
        // This will in turn make the program undebuggable! (WOWIE!!)
        public Point Absolute() => new Point(Math.Abs(X), Math.Abs(Y));
        public long Sum() => X + Y;
        public Point(long x, long y)
            => (X, Y) = (x, y);
        public static Point operator +(Point a, Point b)
            => new Point(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b)
            => new Point(a.X - b.X, a.Y - b.Y);
        public long? ToSlope()
            => X == 0 ? null : Y / X;
    }
    record struct Line
    {
        public Point PointA { get; init; }
        public Point PointB { get; init; }
        public long? Slope => (PointA - PointB).ToSlope();
        public long Length => (long)Math.Sqrt((PointA - PointB).Absolute().Sum());
        public Line(Point pointA, Point pointB)
            => (PointA, PointB) = (pointA, pointB);


    }
}
