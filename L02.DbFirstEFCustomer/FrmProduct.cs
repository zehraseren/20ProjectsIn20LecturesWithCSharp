using System;
using System.Linq;
using System.Windows.Forms;

namespace L02.DbFirstEFCustomer
{
    public partial class FrmProduct : Form
    {
        L02_EfDbFirstCustomerEntities db = new L02_EfDbFirstCustomerEntities();
        public FrmProduct()
        {
            InitializeComponent();
        }

        void ProductList()
        {
            dataGridView1.DataSource = db.Product.ToList();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            var values = db.Category.ToList();
            cmbProductCategory.DisplayMember = "CategoryName";
            cmbProductCategory.ValueMember = "CategoryId";
            cmbProductCategory.DataSource = values;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ProductName = txtProductName.Text;
            product.ProductPrice = decimal.Parse(txtProductPrice.Text);
            product.ProductStock = int.Parse(txtProductStock.Text);
            product.CategoryId = int.Parse(cmbProductCategory.SelectedValue.ToString());
            db.Product.Add(product);
            db.SaveChanges();
            ProductList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var value = db.Product.Find(int.Parse(txtProductId.Text));
            db.Product.Remove(value);
            db.SaveChanges();
            ProductList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var value = db.Product.Find(int.Parse(txtProductId.Text));
            value.ProductName = txtProductName.Text;
            value.ProductPrice = decimal.Parse(txtProductPrice.Text);
            value.ProductStock = int.Parse(txtProductStock.Text);
            value.CategoryId = int.Parse(cmbProductCategory.SelectedIndex.ToString());
            db.SaveChanges();
            ProductList();
        }

        private void btnProductListWithCategory_Click(object sender, EventArgs e)
        {
            var values = db.Product
                .Join(db.Category,
                product => product.CategoryId,
                category => category.CategoryId,
                (product, category) => new
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductStock = product.ProductStock,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                }
                ).ToList();
            dataGridView1.DataSource = values;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = db.Product.Where(x => x.ProductName == txtProductName.Text).ToList();
            dataGridView1.DataSource = values;
        }
    }
}
