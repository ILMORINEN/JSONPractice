namespace JSONPractice
{
    public static class UtilityFunction
    {
        public static bool IsOperation(string arg)
        {
            return new string[] { "-add", "-update", "-get", "-getall", "-delete" }.Contains(arg);
        }

        public static Dictionary<string, string> ConvertToDictionary(string[] value)
        {
            return value.Select(pair => pair.Split(':'))
                        .Where(pair => pair.Length == 2)
                        .ToDictionary(pair => pair[0], pair => pair[1]);
        }
    }
}
