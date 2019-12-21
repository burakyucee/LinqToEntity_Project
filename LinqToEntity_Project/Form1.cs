using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToEntity_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities db = new NorthwindEntities();

        private void btnDbQuery1_Click(object sender, EventArgs e)
        {
            //Ürün fiyatları 50'ye eşit ve 50'den büyük olan ürünlerin Ürün Adı, Stock Mikatını, Pirim Fiyatını getirin ayrıca ürün fiyatlarına göre çoktan aza sıralayın.


            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice >= 50).OrderByDescending(x => x.UnitPrice).Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitsInStock,
                x.UnitPrice
            }).ToList();
        }

        private void btnDbQuery2_Click(object sender, EventArgs e)
        {
            //CategoryID'si 7'den küçük olanları, büyükten küçüğe sıralayınız.


            dataGridView1.DataSource = db.Categories.Where(x => x.CategoryID <= 7).OrderByDescending(x => x.CategoryID).ToList();
        }

        private void btnDbQuery3_Click(object sender, EventArgs e)
        {
            //İngiltere'de oturan bayanların adi, soyadi, mesleği, ünvanı, ülkesi ve doğum tarihlerini listeleyin.

            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Ms." || x.TitleOfCourtesy == "Mrs.").Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Country,
                x.TitleOfCourtesy,
                BirthDate=x.BirthDate
                
            }).ToList();
            
        }

        private void btnDbQuery4_Click(object sender, EventArgs e)
        {
            //Doğum TArihi 1930 ile 1960 arasında olup USA'de çalışanları isim, soyisimlerini listeleyin.

            dataGridView1.DataSource = db.Employees.Where((x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1930 && SqlFunctions.DatePart("Year", x.BirthDate) <= 1960 && x.Country == "USA")).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.BirthDate

            }).ToList();
        }

        private void btnDbQuery5_Click(object sender, EventArgs e)
        {
            //Çalışanalrın firstname,lastname,titleofcourtesy ve age ekrana getirilsin, yaşa göre azalan şekilde sırlanasın.

            dataGridView1.DataSource = db.Employees.OrderByDescending(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)).Select(x => new
            {
                İsim = x.FirstName,
                Soyİsim = x.LastName,
                x.TitleOfCourtesy,
                Age = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)


            }).ToList();
        }

        private void btnDbQuery6_Click(object sender, EventArgs e)
        {
            //Her bir siparişleri birim fiyata göre listelenmesi.
            dataGridView1.DataSource = db.Order_Details.OrderByDescending(x => x.UnitPrice).ToList();
        }

        private void btnDbQuery7_Click(object sender, EventArgs e)
        {
            //Kategorilerime göre toplam stok miktarı.
            dataGridView1.DataSource = db.Products.GroupBy(x => x.Category.CategoryName).Select(y => new
            {
                KategoriAdı= y.Key,
                ToplamStok = y.Sum(x => x.UnitsInStock)


            }).ToList();
        }

        private void btnDbQuery8_Click(object sender, EventArgs e)
        {
            //Baş harfi "a" olup içinde "e" harfi bulunan çalışanları harf sırasına göre listeleyin.
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.StartsWith("a") && x.FirstName.Contains("e")).OrderByDescending(y => y.FirstName).ToList();
        }

        private void btnDbQuery9_Click(object sender, EventArgs e)
        {
            //Ürünler tablosundan Ürünadı, Stock Miktarı 20 ila 50 arasında olanları ve birim fiyatını getiriniz, birim fiyata göre çoktan az sıralayayınız.
            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock > 20 && x.UnitsInStock < 50).OrderByDescending(y => y.UnitPrice).Select(x => new
            {
                x.ProductName,
                x.ProductID,
                x.UnitsInStock,
                x.UnitPrice
            }).ToList();
        }

        private void btnDbQuery10_Click(object sender, EventArgs e)
        {

        }
    }
}
