
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
        var validLines = lines.Where(x => (x.PointA.X == x.PointB.X) || (x.PointA.Y == x.PointB.Y)).ToList();
        // An 8 megabyte large array of hits...
        // https://www.youtube.com/watch?v=DNeikvoqQ-s
        var grid = new long[1000, 1000];

        // Hit every point on the grid each line covers
        foreach (var line in validLines)
        {
            for (var x = line.PointA.X;
                line.PointA.X <= line.PointB.X ? x <= line.PointB.X : x >= line.PointB.X;
                x += line.PointA.X > line.PointB.X ? -1 : 1)
                for (var y = line.PointA.Y;
                    line.PointA.Y <= line.PointB.Y ? y <= line.PointB.Y : y >= line.PointB.Y;
                    y += line.PointA.Y > line.PointB.Y ? -1 : 1)
                {
                    grid[x, y] += 1;
                }
        }

        // IEnumerable.Cast flattens the array
        // Where there is one or less hit, there is no intercection.
        // Where there are 2 or more, n - 1 is the number of intercections.
        // There has to be some obscure bug in the spaghetti that is my for loops, though all inputs seem to have reasonable behavior
        // TURNS OUT THE "OBSCURE BUG" WAS MY HALF-WITTED READING SKILLS. I NEED THE COUNT NOT THE SUM
        return grid.Cast<long>().Where(x => x > 1).Count();
    }

    public long Solution2()
    {
        // A couple minutes after re-reading my code, after I got the second star,
        // I realized that these names are switched up
        // Oh well ??\_(???)_/?? 
        var diags = lines.Where(x => (x.PointA.X == x.PointB.X) || (x.PointA.Y == x.PointB.Y)).ToList();
        var horis = lines.Where(x => (x.PointA.X != x.PointB.X) && (x.PointA.Y != x.PointB.Y)).ToList();
        var grid = new long[1000, 1000];

        // Hit every point on the grid each line covers
        foreach (var line in diags)
        {
            for (var x = line.PointA.X;
                line.PointA.X <= line.PointB.X ? x <= line.PointB.X : x >= line.PointB.X;
                x += line.PointA.X > line.PointB.X ? -1 : 1)
                for (var y = line.PointA.Y;
                    line.PointA.Y <= line.PointB.Y ? y <= line.PointB.Y : y >= line.PointB.Y;
                    y += line.PointA.Y > line.PointB.Y ? -1 : 1)
                {
                    grid[x, y] += 1;
                }
        }
        foreach (var line in horis)
        {
            // uuuuuuuuuuuuuuuuuuu
            // so hey i have these neat little operator overloads i made for these points
            // so why the heck dont i use them???
            for (var p = line.PointA;
                line.PointA.X <= line.PointB.X ? p.X <= line.PointB.X : p.X >= line.PointB.X;
                p -= new Point(
                    line.PointA.X > line.PointB.X ? 1 : -1,
                    line.PointA.Y > line.PointB.Y ? 1 : -1
                ))
            {
                grid[p.X, p.Y] += 1;
            }
        }
        return grid.Cast<long>().Where(x => x > 1).Count();
    }
    // All of these methods other than the operator overloads are pretty much useless.
    // I tried to think up of a clever mathy way of doing things, but since mathematicians are short-sighted idiots, they only work with real numbers
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
