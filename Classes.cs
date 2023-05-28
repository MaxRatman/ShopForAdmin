using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopForAdmin
{
    public enum AppForm
    {
        Product,
        Sales,
        Buyers
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    public class Buyers
    {
        public int Id { get; set; }
        public string Name { get;set; }
        public override string ToString()
        {
            return $"{ Name}";
        }
    }
    public class Sales
    { 
        public int Id { get; set; }
        //public int BuyersId { get; set; }
        //public int ProductId { get; set; }
        public Buyers Buyers { get; set; }
        public Product product { get; set; }
        public int CountProduct { get; set; }
        public DateTime dateTime { get; set; }
    }
    public class MenuStripP :MenuStrip
    {
        public Action<AppForm> Open { get; init; }
        public Action Save { get; init; }
        public Action Exit { get; init; }
        public MenuStripP() =>  Items.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("Действия",null, new ToolStripMenuItem []
            {
                new ToolStripMenuItem("Товары",null, (s,e)=>Open?.Invoke(AppForm.Product)),
                new ToolStripMenuItem("Продажи",null, (s,e)=>Open?.Invoke(AppForm.Sales)),
                new ToolStripMenuItem("Покупатели",null ,(s,e)=>Open?.Invoke(AppForm.Buyers)),
                new ToolStripMenuItem("Сохранить",null, (s,e)=>Save?.Invoke()),
                new ToolStripMenuItem("Выйти",null,(s,e)=>Exit?.Invoke())
            }
            )
        });
    }
    public class DataBaseClass :DbContext
    {
        public DbSet<Product> broducts { get; set;}
        public DbSet<Buyers> buyers { get; set;}
        public DbSet<Sales> sales { get; set;}
        public  DataBaseClass(DbContextOptions<DataBaseClass> dbContextOptions) :base(dbContextOptions){ }

    }
    public class ContextFactory:IDesignTimeDbContextFactory<DataBaseClass>
    {
        public DataBaseClass CreateDbContext(string[]args)
        {
            var optionsBilder = new DbContextOptionsBuilder<DataBaseClass>();
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("ConfigurationFile.json");
            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            string connectionString = configurationRoot.GetConnectionString("DefaultConnection");
            optionsBilder.UseSqlServer(connectionString);
            return new DataBaseClass(optionsBilder.Options);
        }
    }
}
