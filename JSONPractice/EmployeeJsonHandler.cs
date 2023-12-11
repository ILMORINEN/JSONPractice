using Newtonsoft.Json;

namespace JSONPractice
{
    public static class EmployeeJsonHandler
    {
        private static string? json;

        public static void AddEmployee(string jsonPath, Dictionary<string, string> args)
        {
            Employee employee;
            List<Employee> employeesList = new();
            int maxId = 1;

            try
            {
                ReadJson(jsonPath);
                if (!string.IsNullOrEmpty(json))
                {
                    employeesList = JsonConvert.DeserializeObject<List<Employee>>(json);
                    maxId = employeesList.Select(employee => employee.Id)
                                          .OrderBy(id => id)
                                          .LastOrDefault(0);
                    maxId++;
                }
                employee = new(maxId, args["FirstName"], args["LastName"], Convert.ToDecimal(args["Salary"]));
            }
            catch (Exception)
            {
                throw;
            }

            employeesList.Add(employee);
            json = JsonConvert.SerializeObject(employeesList, Formatting.Indented);
            WriteJson(jsonPath);
        }

        public static void UpdateEmployee(string jsonPath, Dictionary<string, string> args)
        {
            Employee? refreshebleEmployee;
            List<Employee> employeesList;
            int employeeIndex;
            string? firstName;
            string? lastName;
            string? salary;

            try
            {
                ReadJson(jsonPath);
                employeesList = JsonConvert.DeserializeObject<List<Employee>>(json);
                employeeIndex = employeesList.FindIndex(employee => employee.Id == Convert.ToInt32(args["Id"]));
                if (employeeIndex == -1)
                    throw new Exception("Employee not found");
            }
            catch (Exception)
            {
                throw;
            }

            refreshebleEmployee = employeesList[employeeIndex];

            if (args.TryGetValue("FirstName", out firstName))
                refreshebleEmployee.FirstName = firstName;

            if (args.TryGetValue("LastName", out lastName))
                refreshebleEmployee.LastName = lastName;

            if (args.TryGetValue("Salary", out salary))
                refreshebleEmployee.SalaryPerHour = Convert.ToDecimal(salary);

            employeesList[employeeIndex] = refreshebleEmployee;

            json = JsonConvert.SerializeObject(employeesList, Formatting.Indented);
            WriteJson(jsonPath);
        }
        public static Employee GetEmployee(string jsonPath, Dictionary<string, string> args)
        {
            int employeeIndex;
            try
            {
                ReadJson(jsonPath);
                List<Employee> employeesList = JsonConvert.DeserializeObject<List<Employee>>(json);
                employeeIndex = employeesList.FindIndex(employee => employee.Id == Convert.ToInt32(args["Id"]));

                if (employeeIndex == -1)
                    throw new Exception("Employee not found");
                
                return employeesList[employeeIndex];
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Employee[] GetAllEmployees(string jsonPath)
        {
            try
            {
                ReadJson(jsonPath);
                List<Employee> employeesList = JsonConvert.DeserializeObject<List<Employee>>(json);
                return employeesList.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void DeleteEmployee(string jsonPath, Dictionary<string, string> args)
        {
            List<Employee> employeesList;
            int employeeIndex;

            try
            {
                ReadJson(jsonPath);
                employeesList = JsonConvert.DeserializeObject<List<Employee>>(json);
                employeeIndex = employeesList.FindIndex(employee => employee.Id == Convert.ToInt32(args["Id"]));
                
                if (employeeIndex == -1)
                    throw new Exception("Employee not found");
            }
            catch (Exception)
            {
                throw;
            }

            employeesList.RemoveAt(employeeIndex);
            json = JsonConvert.SerializeObject(employeesList, Formatting.Indented);
            WriteJson(jsonPath);
        }
        private static void ReadJson(string jsonPath)
        {
            try
            {
                using StreamReader reader = new StreamReader(jsonPath);
                json = reader.ReadToEnd();
            }
            catch (Exception)
            {
                throw new FileReadException();
            }

        }
        private static void WriteJson(string jsonPath)
        {
            using (StreamWriter writer = new StreamWriter(jsonPath))
            {
                writer.WriteLine(json);
            }
        }
    }
}