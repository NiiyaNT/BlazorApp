namespace BlazorAppTest.Data.Models
{
    public class AccountDetail
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
