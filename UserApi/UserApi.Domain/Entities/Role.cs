namespace UserApi.Domain.Entities
{    public class Role : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
