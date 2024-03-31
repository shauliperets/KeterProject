using System.Text.Json;

using Backend.Entities;

namespace Backend.Models
{

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short DepartmentID { get; set; }
        public short SubDepartmentID { get; set; }
        public float Price { get; set; } //run migration for this
        public string Image { get; set; }

        /*public Product()
        {
            this.Id = -1;
            this.Name = "";
            this.Price = -1;
            this.Image = "";
        }*/
        
        /*public Product(JsonElement data)
        {
            JsonDocument document = JsonDocument.Parse(data.ToString());

            JsonElement root = document.RootElement;

            string name = root.GetProperty("name").ToString();

            int price = int.Parse(root.GetProperty("price").ToString());

            //string image = root.GetProperty("image").ToString();

            string image = root.GetProperty("image").GetProperty("fileName").ToString();

            this.Name = name;
            this.Price = price;
            this.Image = image; // add file properties class backend
        }*/
    }
}