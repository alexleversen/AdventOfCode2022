namespace AdventOfCode2022;

public class Day2Part1 : IPuzzleSolver
{
    private const int RockScore = 1;
    private const int PaperScore = 2;
    private const int ScissorsScore = 3;
    private const int LossScore = 0;
    private const int DrawScore = 3;
    private const int WinScore = 6;
    public string Run(string[] input)
    {
        var score = 0;
        foreach (var line in input)
        {
            var moves = line.Split(' ');
            var otherMove = moves[0];
            var myMove = moves[1];
            switch (otherMove)
            {
                case "A":
                    switch (myMove)
                    {
                        case "X":
                            score += RockScore;
                            score += DrawScore;
                            break;
                        case "Y":
                            score += PaperScore;
                            score += WinScore;
                            break;
                        case "Z":
                            score += ScissorsScore;
                            score += LossScore;
                            break;
                        default:
                            throw new InvalidDataException($"Found invalid input {otherMove}");
                    }
                    break;
                case "B":
                    switch (myMove)
                    {
                        case "X":
                            score += RockScore;
                            score += LossScore;
                            break;
                        case "Y":
                            score += PaperScore;
                            score += DrawScore;
                            break;
                        case "Z":
                            score += ScissorsScore;
                            score += WinScore;
                            break;
                        default:
                            throw new InvalidDataException($"Found invalid input {otherMove}");
                    }
                    break;
                case "C":
                    switch (myMove)
                    {
                        case "X":
                            score += RockScore;
                            score += WinScore;
                            break;
                        case "Y":
                            score += PaperScore;
                            score += LossScore;
                            break;
                        case "Z":
                            score += ScissorsScore;
                            score += DrawScore;
                            break;
                        default:
                            throw new InvalidDataException($"Found invalid input {otherMove}");
                    }
                    break;
                default:
                    throw new InvalidDataException($"Found invalid input {otherMove}");
            }
        }

        return $"{score}";
    }
}