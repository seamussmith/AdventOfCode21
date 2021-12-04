
using System.Text.RegularExpressions;

namespace AdventOfCode21.Day4;

public class Solution
{
    readonly IReadOnlyList<long> drawnNums;
    readonly IReadOnlyList<IReadOnlyList<IReadOnlyList<BoardSlot>>> boards;
    readonly static Regex num = new Regex(@"[0-9]+", RegexOptions.Compiled);
    public Solution(string input)
    {
        var split = input.Split(Environment.NewLine);
        var firstRow = split[0].Split(",").Select(x => long.Parse(x)).ToList();
        drawnNums = firstRow;
        var bingoBoards = split.Skip(2)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select((x, i) => new { Value = x, Index = i }) // Preserve the index, we need this for grouping
            .GroupBy(x => x.Index / 5)                      // Group our strings by 5, since thats the size of the boards
            .Select(x => x.Select(x => x.Value).ToList())   // Convert the groupings to a list of lists
            .Select(
                x => x.Select(
                    x => num.Matches(x).Select(
                        x => new BoardSlot(false, long.Parse(x.Value)) // Convert each list of strings into a list of list of BoardSlots
                    ).ToList()
                ).ToList()
            ).ToList();
        boards = bingoBoards;
    }
    bool IsWinner(IReadOnlyList<IReadOnlyList<BoardSlot>> board)
    {
        // Hori
        for (var y = 0; y < board[0].Count(); ++y)
        {
            var inARow = 0;
            for (var x = 0; x < board[0].Count(); ++x)
                if (board[x][y].Marked)
                    ++inARow;
            if (inARow == board[0].Count())
                return true;
        }

        // Vert
        for (var x = 0; x < board[0].Count(); ++x)
        {
            var inARow = 0;
            for (var y = 0; y < board[0].Count(); ++y)
                if (board[x][y].Marked)
                    ++inARow;
            if (inARow == board[0].Count())
                return true;
        }

        return false;
    }
    public long Solution1()
    {
        // MULTIDIMENSIONAL CLONING AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        // might aswell just use haskell if i am gonna be this crazy with immutable state
        // well there is F#...
        var game = boards.Select(
            x => x.Select(
                x => x.Select(
                    x => x
                ).ToList()
            ).ToList()
        ).ToList();
        var round = 0;

        // While there isnt any winner...
        while (!game.Any(x => IsWinner(x)))
        {
            // Mark ALL boardslots in the game that match the drawn number
            game = game.Select(
                x => x.Select(
                    x => x.Select(
                        x => x with
                        {
                            Marked = x.Value == drawnNums[round] || x.Marked
                        }
                    ).ToList()
                ).ToList()
            ).ToList();
            ++round;
        }
        // The winner should be the first (and only) board when filtering for winners
        var winner = game.Where(x => IsWinner(x)).First();
        // Aggregate the values of all unmarked slots.
        // Multi-dimensional aggregation because two dimensional list
        return winner.Aggregate((long)0, (a, b) =>
                a + b.Aggregate((long)0, (a, b) =>
                    a + (b.Marked ? 0 : b.Value)
                )
            ) * drawnNums[round - 1];
    }
    public long Solution2()
    {
        var game = boards.Select(
            x => x.Select(
                x => x.Select(
                    x => x
                ).ToList()
            ).ToList()
        ).ToList();
        var round = 0;

        // Stack of indexes of won boards
        var wonBoards = new Stack<int>();
        // Until all boards are winners...
        while (!game.All(x => IsWinner(x)))
        {
            game = game.Select(
                x => x.Select(
                    x => x.Select(
                        x => x with
                        {
                            Marked = x.Value == drawnNums[round] || x.Marked
                        }
                    ).ToList()
                ).ToList()
            ).ToList();
            // Add new winners to the wonBoards stack
            for (var i = 0; i < game.Count(); ++i)
            {
                if (!IsWinner(game[i]) || wonBoards.Contains(i))
                    continue;
                wonBoards.Push(i);
            }
            ++round;
        }
        // The winner's index should be on top of the stack
        var winner = game[wonBoards.Pop()];
        return winner.Aggregate((long)0, (a, b) =>
                a + b.Aggregate((long)0, (a, b) =>
                    a + (b.Marked ? 0 : b.Value)
                )
            ) * drawnNums[round - 1];
    }
    record struct BoardSlot(bool Marked, long Value);
}
