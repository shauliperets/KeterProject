using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Models;  
using Backend.DTOs; 
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IO;
//using System.Data.Entity.Migrations;

namespace Backend.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        DataContext context;

        public ProductsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ActionResult<ProductDto> GetProducts()
        {
            return Ok(context.Database
                   .SqlQuery<ProductDto>($@"EXECUTE GetProducts")
                   .ToList());


            /* Linq example
            return Ok((from product in this.context.Products
                    join department in context.Departments on product.DepartmentID equals department.ID
                    select new { Id = product.Id,
                                 Name = product.Name,
                                 Department = department.Name,
                                 SubDepartment = "SubDepartment",
                                 Price = product.Price,
                                 Image = product.Image
                               }
                   ).ToList());*/
            
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        //public Product SaveProduct([FromBody] Product product)
        public Product SaveProduct([FromBody] JsonElement data)
        {
            JsonDocument document = JsonDocument.Parse(data.ToString());

            JsonElement root = document.RootElement;

            //add time to logs table

            context.Logs.Add(new Log("SaveProduct post start. data => " + data.ToString(), "ProductController"));
            context.SaveChanges();

            int id = int.Parse(root.GetProperty("id").ToString());
            string name = root.GetProperty("name").ToString();
            //product.Description = root.GetProperty("description").ToString();
            short price = short.Parse(root.GetProperty("price").ToString());
            string image = root.GetProperty("image").ToString();

            Product item = context.Products.SingleOrDefault(row => row.Id == id);
            //context.Entry(item).CurrentValues.SetValues(product);
            item.Name = name;
            item.Price = price;
            
            ///--> bug
            item.Image = image;
            
            
            context.Update(item);
            context.SaveChanges();

            //context.Products.Add

            return item;
        }


        [HttpPost]
        [Route("api/[controller]/[action]")]
        public string Test([FromBody] JsonElement data)
        {
            return "test-api-test-works";

        }

        /*[HttpPost]
        [Route("api/[controller]/[action]")]
        public Product Robi([FromBody] JsonElement data)
        {
            //return "new Product()22";
            return new Product();
        }*/

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public Product AddProduct([FromBody] JsonElement data) {
            //return new Product();
            //return "added";
            JsonDocument document = JsonDocument.Parse(data.ToString());

            JsonElement root = document.RootElement;

            //string name = root.GetProperty("image").ToString();
            string fileName = root.GetProperty("image").GetProperty("fileName").ToString();
            string fileAsBase64 = root.GetProperty("image").GetProperty("fileAsBase64").ToString();

            byte[] bytes = System.Convert.FromBase64String(fileAsBase64);

            this.saveFile(bytes, fileName);

            //string fileName = root.GetProperty("fileName").ToString();
            context.Logs.Add(new Log("root ==> " + root.ToString(), "ProductController"));
            context.Logs.Add(new Log("file name ==> " + fileName.Substring(fileName.Length - 4), "ProductController"));
            context.SaveChanges();

            //context.Logs.Add(new Log("folder is => ", "ProductsController"));
            //context.SaveChanges();
            
            //Product product = new Product(data);
            //product = new Product();

            //Product product = context.Products.SingleOrDefault(row => row.Id == 16);
            //product.Name = "Name";
            //product.Price = 2;
            //product.Image = null;

            //context.Products.Add(product);
            //context.SaveChanges();

            //return data;
            //return root.ToString();
            return null;
        }


        [HttpPost]
        [Route("api/[controller]/[action]")]
        public string DeleteProduct([FromBody] JsonElement data){

            
            return data.ToString();
        }

        private string saveFile(byte[] fileData, string fileName) {
            //JsonDocument document = JsonDocument.Parse(data.ToString());

            //JsonElement root = document.RootElement;

            //string filePath = root.GetProperty("image").ToString();
            /*var file = Request.Form.Files[0];
            using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            */
            
            //FileStream reader = new FileStream(filePath, FileMode.Open);

            //BinaryReader binaryReader = new BinaryReader(reader);
            //FileInfo fileInfo = new FileInfo(filePath);

            //byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            
            string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "../");
            //string imageFileLocation = string.Format("Frontend/images/{0}{1}", DateTime.Now.ToString().Replace("/","").Replace(":", "").Replace(" ", ""), fileExtension);
            string imageFileLocation = string.Format("Frontend/images/{0}", fileName);
            string imageFolder = Path.Combine(rootFolder, imageFileLocation);
            
            //context.Logs.Add(new Log("folder is => ", "ProductsController"));
            //context.SaveChanges();

            //FileStream f = new FileStream("e:\\b.txt", FileMode.OpenOrCreate);//creating file stream 

            FileStream writer = new FileStream(imageFolder, FileMode.Create);
            writer.Write(fileData);

            //f.WriteByte(65);//writing byte into stream  
            //f.Close();//closing stream  

            return imageFileLocation;
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        //public string UploadFile([FromBody] FileToUpload file)
        public string UploadFile([FromBody] JsonElement data){
            
            //context.Logs.Add(new Log("UploadFile post start. file name => " + file.FileName, "ProductController"));
            //context.SaveChanges();

            JsonDocument document = JsonDocument.Parse(data.ToString());

            JsonElement root = document.RootElement;

            string fileName = root.GetProperty("fileName").ToString();
            string fileAsBase64 = root.GetProperty("fileAsBase64").ToString();

            //context.Logs.Add(new Log("UploadFile post start. fileAsBase64 => " + fileAsBase64, "ProductController"));
            //context.SaveChanges();
            
            //file.FileAsByteArray = Convert.FromBase64String(file.FileAsBase64);
            var fileAsByteArray = Convert.FromBase64String(fileAsBase64);
            
            FileInfo fileInfo = new FileInfo(fileName);

            //byte[] fileData = System.IO.File.ReadAllBytes(file);
            
            string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "../");
            //string imageFileLocation = string.Format("Frontend/src/assets/images/{0}{1}", DateTime.Now.ToString().Replace("/","").Replace(":", "").Replace(" ", ""), fileInfo.Extension);
            string imageFileLocation = string.Format("Frontend/images/{0}{1}", DateTime.Now.ToString().Replace("/","").Replace(":", "").Replace(" ", ""), fileInfo.Extension);
            string imageFolder = Path.Combine(rootFolder, imageFileLocation);
            
            /*"/home/shaul/Workspace/Projects/ShoppingCartProject/ShoppingCart/Frontend/src/assets/images/temp/1.png"*/

            FileStream writer = new FileStream(imageFolder, FileMode.Create);

            //return data.ToString();

            //writer.Write(fileAsByteArray);
            //System.IO.File.WriteAllBytes("/home/shaul/Workspace/Projects/ShoppingCartProject/ShoppingCart/Frontend/temp/1.png"/*imageFolder*/, fileAsByteArray);
            //writer.Flush();
        
            return data.ToString();
            

            //return null;
        }
    
    }
}