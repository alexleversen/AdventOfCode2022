using System.Text.RegularExpressions;
using AdventOfCode2022.Models.Day11;

namespace AdventOfCode2022;

public class Day11Part1 : IPuzzleSolver
{
    private readonly Regex _monkeyRegex = new Regex("^Monkey \\d+:$");
    private readonly Regex _startingItemsRegex = new Regex("^\\s*Starting items: ((\\d+,?\\s?)*)$");
    private readonly Regex _operationRegex = new Regex("^\\s*Operation: new = old ([+\\-*/]) (\\d+|old)$");
    private readonly Regex _testRegex = new Regex("^\\s*Test: divisible by (\\d+)$");
    private readonly Regex _conditionalRegex = new Regex("^\\s*If (true|false): throw to monkey (\\d+)");
    public string Run(string[] input)
    {
        List<Monkey> monkeys = new List<Monkey>();
        Monkey? currentMonkey = null;
        foreach (var line in input)
        {
            if (_monkeyRegex.IsMatch(line))
            {
                currentMonkey = new Monkey();
                monkeys.Add(currentMonkey);
            }

            if (_startingItemsRegex.IsMatch(line))
            {
                var groups = _startingItemsRegex.Match(line).Groups;
                foreach (var itemString in groups[1].Value.Split(", "))
                {
                    currentMonkey?.Items.Add(int.Parse(itemString));
                }
            }

            if (_operationRegex.IsMatch(line))
            {
                var groups = _operationRegex.Match(line).Groups;
                var mathOperator = groups[1].Value;
                var operand = groups[2].Value;
                switch (mathOperator)
                {
                    case "+":
                        if (currentMonkey != null)
                        {
                            if (operand == "old")
                            {
                                currentMonkey.Operation = n => n + n;
                                break;
                            }

                            var operandValue = int.Parse(operand);
                            currentMonkey.Operation = n => n + operandValue;
                        }
                        break;
                    case "-":
                        if (currentMonkey != null)
                        {
                            if (operand == "old")
                            {
                                currentMonkey.Operation = _ => 0;
                                break;
                            }

                            var operandValue = int.Parse(operand);
                            currentMonkey.Operation = n => n - operandValue;
                        }
                        break;
                    case "*":
                        if (currentMonkey != null)
                        {
                            if (operand == "old")
                            {
                                currentMonkey.Operation = n => n * n;
                                break;
                            }

                            var operandValue = int.Parse(operand);
                            currentMonkey.Operation = n => n * operandValue;
                        }
                        break;
                    case "/":
                        if (currentMonkey != null)
                        {
                            if (operand == "old")
                            {
                                currentMonkey.Operation = _ => 1;
                                break;
                            }

                            var operandValue = int.Parse(operand);
                            currentMonkey.Operation = n => n / operandValue;
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid math operator: {mathOperator}");
                }
            }

            if (_testRegex.IsMatch(line))
            {
                var groups = _testRegex.Match(line).Groups;
                if (currentMonkey != null)
                {
                    currentMonkey.Divisor = int.Parse(groups[1].Value);
                }
            }

            if (_conditionalRegex.IsMatch(line))
            {
                var groups = _conditionalRegex.Match(line).Groups;
                if (currentMonkey == null)
                {
                    continue;
                }
                if (bool.Parse(groups[1].Value))
                {
                    currentMonkey.TrueIndex = int.Parse(groups[2].Value);
                }
                else
                {
                    currentMonkey.FalseIndex = int.Parse(groups[2].Value);
                }
            }
        }

        for (var round = 0; round < 20; round++)
        {
            foreach (var monkeyAtIndex in monkeys)
            {
                while (monkeyAtIndex.Items.Count > 0)
                {
                    var currentItem = monkeyAtIndex.Items.First();
                    monkeyAtIndex.Items.RemoveAt(0);
                    monkeyAtIndex.InspectionCount++;
                    currentItem = monkeyAtIndex.Operation!(currentItem) / 3;
                    if (currentItem % monkeyAtIndex.Divisor == 0)
                    {
                        monkeys[monkeyAtIndex.TrueIndex!.Value].Items.Add(currentItem);
                    }
                    else
                    {
                        monkeys[monkeyAtIndex.FalseIndex!.Value].Items.Add(currentItem);
                    }
                }
            }
        }

        var inspectionCounts = monkeys.OrderByDescending(m => m.InspectionCount)
            .Select(m => m.InspectionCount)
            .ToList();

        return $"{inspectionCounts[0] * inspectionCounts[1]}";
    }


}