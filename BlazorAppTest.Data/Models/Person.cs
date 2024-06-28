namespace BlazorAppTest.Data.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccountDetail> AccountDetails { get; set; } // Nueva relación
    }
}
