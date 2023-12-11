namespace JSONPractice
{
    public class Employee
    {
        public Employee()
        { }
        public Employee(int id, string firstName, string lastName, decimal salary)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SalaryPerHour = salary;
        }
        public override string ToString() => $"Id = {Id}, FirstName = {FirstName}, LastName = {LastName}, SalaryPerHour = { SalaryPerHour}";
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal SalaryPerHour { get; set; }
    }
}
