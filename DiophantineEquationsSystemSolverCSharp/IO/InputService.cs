using DiophantineEquationsSystemSolverCSharp.Extensions;

namespace DiophantineEquationsSystemSolverCSharp.IO;

public class InputService
{
    public EquationsSystem ReadSystem()
    {
        (var originRowsCount, _) = Console.ReadLine()!.ToInts();

        var nums = new int[originRowsCount][];
        for (int i = 0; i < originRowsCount; i++)
        {
            nums[i] = Console.ReadLine()!.ToInts();
            nums[i][^1] *= -1;
        }
        return new(nums);
    }
}