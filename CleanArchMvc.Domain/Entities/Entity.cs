namespace CleanArchMvc.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        public String Name { get; protected set; }
    }
}
