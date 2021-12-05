using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode21.Day5.Test;

public class Test
{
    private readonly string TEST_INPUT = System.IO.File.ReadAllText("../../../Inputs/testinput.txt");
    private readonly string PUZZLE_INPUT = System.IO.File.ReadAllText("../../../Inputs/puzzleinput.txt");
    private readonly ITestOutputHelper output;
    public Test(ITestOutputHelper _output)
    {
        output = _output;
    }
    [Fact]
    public void Solution1_ShouldOutputTheExampleResult()
    {
        var sol = new Solution(TEST_INPUT);
        Assert.Equal(5, sol.Solution1());
    }
    [Fact]
    public void Solution1_PuzzleOutput()
    {
        var sol = new Solution(PUZZLE_INPUT);
        output.WriteLine($"Puzzle 1 Output: {sol.Solution1()}");
    }
    [Fact(Skip = "Not Implemented")]
    public void Solution2_ShouldOutputTheExampleResult()
    {
        var sol = new Solution(TEST_INPUT);
        Assert.Equal(0, sol.Solution2());
    }
    [Fact(Skip = "Not Implemented")]
    public void Solution2_PuzzleOutput()
    {
        var sol = new Solution(PUZZLE_INPUT);
        output.WriteLine($"Puzzle 2 Output: {sol.Solution2()}");
    }
}
