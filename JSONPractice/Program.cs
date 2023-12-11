namespace JSONPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!(args != null && args.Length != 0 && UtilityFunction.IsOperation(args[0])))
            {
                Console.WriteLine("Error: \"Uncorrect arguments\"");
                return;
            }

            int successOperationCount = 0;
            int failureOperationCount = 0;
            int currentOperationIndex = 0;
            int nextOperationIndex = 0;
            string? jsonPath;

            Console.WriteLine("Enter your file path:");
            jsonPath = Console.ReadLine();

            while (nextOperationIndex != -1)
            {
                nextOperationIndex = Array.FindIndex(args, currentOperationIndex + 1, UtilityFunction.IsOperation);

                int valuesSubarrayOffset = currentOperationIndex + 1;
                int valuesSubarrayLength = ((nextOperationIndex == -1) ? args.Length : nextOperationIndex) - valuesSubarrayOffset;
                string[] valuesSubarray = args.Skip(valuesSubarrayOffset)
                                              .Take(valuesSubarrayLength)
                                              .ToArray();
                var argsDictionary = UtilityFunction.ConvertToDictionary(valuesSubarray);

                try
                {
                    switch (args[currentOperationIndex])
                    {
                        case "-add":
                            EmployeeJsonHandler.AddEmployee(jsonPath, argsDictionary);
                            successOperationCount++;
                            break;
                        case "-update":
                            EmployeeJsonHandler.UpdateEmployee(jsonPath, argsDictionary);
                            successOperationCount++;
                            break;
                        case "-get":
                            Console.WriteLine(EmployeeJsonHandler.GetEmployee(jsonPath, argsDictionary));
                            successOperationCount++;
                            break;
                        case "-getall":
                            foreach (var employee in EmployeeJsonHandler.GetAllEmployees(jsonPath))
                                Console.WriteLine(employee);
                            successOperationCount++;
                            break;
                        case "-delete":
                            EmployeeJsonHandler.DeleteEmployee(jsonPath, argsDictionary);
                            successOperationCount++;
                            break;
                        default:
                            break;
                    }
                }
                catch(Newtonsoft.Json.JsonSerializationException)
                {
                    Console.WriteLine("Error: \"Json serialization/deserialization problem\"");
                    return;
                }
                catch (FileReadException ex)
                {
                    Console.WriteLine($"Error: \"{ex.Message}\"");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: \"{ex.Message}\"");
                    failureOperationCount++;
                }
                currentOperationIndex = nextOperationIndex;
            }
            Console.WriteLine($"Program finished\n" +
                              $"Success operation(-s): {successOperationCount}\n" +
                              $"Failure operation(-s): {failureOperationCount}");
            return;
        }
    }
}
