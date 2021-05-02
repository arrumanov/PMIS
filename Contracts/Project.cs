namespace PMIS.Contracts
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public short Quantity { get; set; }
    }
}