using System;
using System.Linq;
using System.Windows.Forms;

namespace L02.DbFirstEFCustomer
{
    public partial class FrmCategory : Form
    {
        L02_EfDbFirstCustomerEntities db = new L02_EfDbFirstCustomerEntities();

        public FrmCategory()
        {
            InitializeComponent();
        }

        void CategoryList()
        {
            dataGridView1.DataSource = db.Category.ToList();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.CategoryName = txtCategoryName.Text;
            db.Category.Add(category);
            db.SaveChanges();
            CategoryList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            var value = db.Category.Find(id);
            db.Category.Remove(value);
            db.SaveChanges();
            CategoryList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryId.Text);
            var value = db.Category.Find(id);
            value.CategoryName = txtCategoryName.Text;
            db.SaveChanges();
            CategoryList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = db.Category.Where(x => x.CategoryName == txtCategoryName.Text).ToList();
            dataGridView1.DataSource = values;
        }
    }
}
