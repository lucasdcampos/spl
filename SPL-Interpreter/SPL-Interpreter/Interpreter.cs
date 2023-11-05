using System.Collections.Generic;
using System.Text;

namespace SPL_Interpreter
{
    internal class Interpreter
    {
        // A dictionary to store the variables defined in the code.
        private static Dictionary<string, object> variables = new Dictionary<string, object>();

        // The Interpret method takes a string of source code and executes it.
        public static void Interpret(string source)
        {
            // Split the source code into lines.
            string[] lines = source.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // Process each line individually.
            foreach (string line in lines)
            {
                // Ignore everything after the // for comments.
                string code = line.Split("//")[0];

                // Split the line into instructions by the semicolon.
                string[] instructions = code.Split(";");

                // Process each instruction individually.
                foreach (string i in instructions)
                {
                    // Split the instruction into tokens by the space.
                    string[] tokens = i.Split(" ");

                    // Process each token individually.
                    for (int x = 0; x < tokens.Length; x++)
                    {
                        // Perform different actions based on the token.
                        switch (tokens[x].ToLower())
                        {
                            case "sum":
                                double sumResult = Sum(tokens.Skip(x + 1).ToArray());
                                x += tokens.Length - 1;
                                break;
                            case "sub":
                                double subResult = Sub(tokens.Skip(x + 1).ToArray());
                                x += tokens.Length - 1;
                                break;
                            case "mult":
                                double mulResult = Mul(tokens.Skip(x + 1).ToArray());
                                x += tokens.Length - 1;
                                break;
                            case "div":
                                double divResult = Div(tokens.Skip(x + 1).ToArray());
                                x += tokens.Length - 1;
                                break;
                            case "equals":
                                bool eqResult = Eq(tokens.Skip(x + 1).ToArray());
                                x += tokens.Length - 1;
                                break;
                            case "set":
                                Set(tokens[x + 1], tokens.Skip(x + 2).ToArray());
                                x += tokens.Length - 1;
                                break;
                            case "print":
                                Print(tokens.Skip(x + 1).ToArray());
                                x += tokens.Length - 1;
                                break;
                            case "wait":
                                // Will pause the application until user hits enter
                                Console.Read();
                                break;
                            default:
                                // Unknown Token
                                if (tokens[x] == string.Empty) break;
                                Console.WriteLine($"Unknown Token '{tokens[x]}'");
                                Console.ReadLine();
                                break;
                        }
                    }
                }
            }
        }

        // The Sum function takes an array of strings representing numbers and returns their sum.
        static double Sum(string[] nums)
        {
            // Checks if all elements in the array are numbers or existing variable names.
            if (nums.All(num => isNumber(num) || variables.ContainsKey(num)))
            {
                // Sums all numbers in the array.
                return nums.Sum(num => getNumber(num));
            }
            else
            {
                // If any element in the array is not a number or an existing variable, prints an error.
                Console.WriteLine("error: invalid syntax");
                return 0;
            }
        }

        // The Sub function takes an array of strings representing numbers and returns their subtraction.
        static double Sub(string[] nums)
        {
            // Checks if all elements in the array are numbers or existing variable names.
            if (nums.All(num => isNumber(num) || variables.ContainsKey(num)))
            {
                // Subtracts all numbers in the array.
                return nums.Select(num => getNumber(num)).Aggregate((a, b) => a - b);
            }
            else
            {
                // If any element in the array is not a number or an existing variable, prints an error.
                Console.WriteLine("error: invalid syntax");
                return 0;
            }
        }

        // The Mul function takes an array of strings representing numbers and returns their multiplication.
        static double Mul(string[] nums)
        {
            // Checks if all elements in the array are numbers or existing variable names.
            if (nums.All(num => isNumber(num) || variables.ContainsKey(num)))
            {
                // Multiplies all numbers in the array.
                return nums.Aggregate(1.0, (a, b) => a * getNumber(b));
            }
            else
            {
                // If any element in the array is not a number or an existing variable, prints an error.
                Console.WriteLine("error: invalid syntax");
                return 0;
            }
        }

        // The Div function takes an array of strings representing numbers and returns their division.
        static double Div(string[] nums)
        {
            // Checks if all elements in the array are numbers or existing variable names.
            if (nums.All(num => isNumber(num) || variables.ContainsKey(num)))
            {
                // Divides all numbers in the array.
                return nums.Select(num => getNumber(num)).Aggregate((a, b) => a / b);
            }
            else
            {
                // If any element in the array is not a number or an existing variable, prints an error.
                Console.WriteLine("error: invalid syntax");
                return 0;
            }
        }

        // The Set function assigns a value to a variable.
        static void Set(string varName, string[] values)
        {
            // If there is only one value, it is directly assigned to the variable.
            if (values.Length == 1)
            {
                // If the value is a number, it is converted to a number and assigned to the variable.
                if (isNumber(values[0]))
                {
                    variables[varName] = getNumber(values[0]);
                }
                // If the value is a string (enclosed in quotes), it is assigned to the variable as a string.
                else if (values[0].StartsWith("\"") && values[0].EndsWith("\""))
                {
                    variables[varName] = values[0].Trim('"');
                }
                // If the value is a boolean (true or false), it is converted to a boolean and assigned to the variable.
                else if (values[0].ToLower() == "true" || values[0].ToLower() == "false")
                {
                    variables[varName] = bool.Parse(values[0]);
                }
            }
            // If there are three values and the second one is 'equals', the result of the comparison is assigned to the variable.
            else if (values.Length == 3 && values[1] == "equals")
            {
                if (isNumber(values[0]) && isNumber(values[2]))
                {
                    variables[varName] = getNumber(values[0]) == getNumber(values[2]);
                }
                else
                {
                    Console.WriteLine("error: invalid syntax");
                }
            }
            // If there is more than one value, the first one is considered an operation and the result of the operation is assigned to the variable.
            else if (values.Length > 1)
            {
                switch (values[0].ToLower())
                {
                    case "sum":
                        variables[varName] = Sum(values.Skip(1).ToArray());
                        break;
                    case "sub":
                        variables[varName] = Sub(values.Skip(1).ToArray());
                        break;
                    case "mult":
                        variables[varName] = Mul(values.Skip(1).ToArray());
                        break;
                    case "div":
                        variables[varName] = Div(values.Skip(1).ToArray());
                        break;
                    default:
                        Console.WriteLine("error: invalid syntax");
                        break;
                }
            }
            else
            {
                Console.WriteLine("error: invalid syntax");
            }
        }

        // The Eq function checks if two values are equal.
        static bool Eq(string[] values)
        {
            // If there are not exactly two values, it is considered a syntax error.
            if (values.Length != 2)
            {
                Console.WriteLine("error: invalid syntax");
                return false;
            }

            // If both values are numbers, they are compared as numbers.
            if (isNumber(values[0]) && isNumber(values[1]))
            {
                return getNumber(values[0]) == getNumber(values[1]);
            }
            // If both values are existing variables, they are compared as strings.
            else if (variables.ContainsKey(values[0]) && variables.ContainsKey(values[1]))
            {
                return variables[values[0]].ToString() == variables[values[1]].ToString();
            }
            else
            {
                Console.WriteLine("error: invalid syntax");
                return false;
            }
        }

        // The Print function takes an array of strings and prints their values.
        static void Print(string[] values)
        {
            // The skip variable is used to skip over tokens that have already been processed.
            int skip = 0;
            for (int v = 0; v < values.Length; v++)
            {
                // If skip is greater than 0, decrement it and continue to the next iteration.
                if (skip > 0)
                {
                    skip--;
                    continue;
                }

                string value = values[v];
                // If the value is a string (enclosed in quotes), print it.
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    Console.Write(value.Trim('"') + " ");
                }
                // If the value is a variable, print its value.
                else if (variables.ContainsKey(value))
                {
                    Console.Write(variables[value]);
                }
                // If the value is the 'equals' operator, print the result of the comparison.
                else if (values[0] == "equals")
                {
                    if (isNumber(values[1]) && isNumber(values[2]))
                    {
                        Console.WriteLine(getNumber(values[1]) == getNumber(values[2]));
                    }
                    else
                    {
                        Console.WriteLine("error: invalid syntax");
                    }
                }
                // If the value is an operation, print the result of the operation.
                else
                {
                    switch (value.ToLower())
                    {
                        case "sum":
                            Console.Write(Sum(values.Skip(v + 1).ToArray()));
                            skip = values.Length - v - 1;
                            break;
                        case "sub":
                            Console.Write(Sub(values.Skip(v + 1).ToArray()));
                            skip = values.Length - v - 1;
                            break;
                        case "mult":
                            Console.Write(Mul(values.Skip(v + 1).ToArray()));
                            skip = values.Length - v - 1;
                            break;
                        case "div":
                            Console.Write(Div(values.Skip(v + 1).ToArray()));
                            skip = values.Length - v - 1;
                            break;
                        default:
                            Console.Write(value + " ");
                            break;
                    }
                }
            }

            // Print a newline at the end.
            Console.WriteLine();
        }

        // The Concat function takes an array of strings and concatenates their values.
        static string Concat(string[] values)
        {
            // Use a StringBuilder for efficient string concatenation.
            StringBuilder sb = new StringBuilder();
            foreach (string value in values)
            {
                // If the value is a string (enclosed in quotes), append it to the StringBuilder.
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    sb.Append(value.Trim('"'));
                }
                // If the value is a variable, append its value to the StringBuilder.
                else if (variables.ContainsKey(value))
                {
                    sb.Append(variables[value].ToString());
                }
                // Otherwise, append the value itself to the StringBuilder.
                else
                {
                    sb.Append(value);
                }
            }

            // Return the concatenated string.
            return sb.ToString();
        }

        // The isNumber function checks if a string can be converted to a number.
        static bool isNumber(string s)
        {
            // Try to parse the string as a double. If it succeeds, return true. Otherwise, return false.
            return double.TryParse(s, out double _);
        }

        // The getNumber function retrieves the numeric value of a string.
        static double getNumber(string s)
        {
            // If the string is a variable name, return the value of the variable.
            if (variables.ContainsKey(s))
            {
                return (double)variables[s];
            }
            else
            {
                // If the string is not a variable name, try to parse it as a double and return the result.
                double.TryParse(s, out double n);
                return n;
            }
        }
    }
}