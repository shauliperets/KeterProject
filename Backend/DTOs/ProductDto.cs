namespace Backend.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public float Price { get; set; } //run migration for this
        public string Image { get; set; }

    }
}