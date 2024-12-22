using DiophantineEquationsSystemSolverCSharp.IO;
using DiophantineEquationsSystemSolverCSharp.Solver;

var inputService = new InputService();
var outputService = new OutputService();

var system = inputService.ReadSystem();
var solver = new Solver(system);
var solution = solver.Solve();
outputService.WriteSolution(solution);