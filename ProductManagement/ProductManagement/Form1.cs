using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm_EF.Models;

namespace WinForm_EF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();

        }

        private void loadData()
        {
            using (MySaleDB1Context context = new MySaleDB1Context())
            {
                var data1 = context.Products.Select(item => new 
                { 
                    ProductID=item.ProductId,
                    ProductName=item.ProductName,
                    Price=item.UnitPrice,
                    Stock=item.UnitsInStock,
                    Image=item.Image,
                    CategoryName=item.Category.CategoryName
                }).ToList();
                dataGridView1.DataSource = data1;

                //var data2 = context.Categories.ToList();
                //comboBox1.DataSource = data2;
                //comboBox1.DisplayMember = "CategoryName";
                //comboBox1.ValueMember = "CategoryId";
            }

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            txtUnit.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            
            nbrUnitInStock.Value = Convert.ToUInt32 (dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString());
            txtImage.Text = dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (MySaleDB1Context context = new MySaleDB1Context())
            {
                // Tao doi tuong se insert
                Product product = new Product {
                
                    ProductName=txtName.Text,
                    UnitPrice=Convert.ToDecimal(txtUnit.Text),
                    UnitsInStock=Convert.ToInt32(nbrUnitInStock.Value),
                    Image=txtImage.Text,
                    CategoryId=Convert.ToInt32(comboBox1.SelectedValue)
                };
                context.Products.Add(product);
                if (context.SaveChanges()>0)
                {
                    MessageBox.Show("Insert successfully");
                    loadData();
                }

            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (MySaleDB1Context context = new MySaleDB1Context())
            {
                // Tim doi tuong can xoa
                Product product = context.Products.SingleOrDefault(item => item.ProductId == Convert.ToInt32(txtId.Text));
                
                context.Products.Remove(product);
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Delete successfully");
                    loadData();
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (MySaleDB1Context context = new MySaleDB1Context())
            {
                //Tim product muon update
                Product product = context.Products.SingleOrDefault(item => item.ProductId == Convert.ToInt32(txtId.Text));

                //setting lai nhung gia tri muon update
                product.ProductName = txtName.Text;
                product.UnitPrice = Convert.ToDecimal(txtUnit.Text);
                product.UnitsInStock = Convert.ToInt32(nbrUnitInStock.Text);
                product.Image = txtImage.Text;
                product.CategoryId = Convert.ToInt32(comboBox1.SelectedValue);

                
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("update successfully");
                    loadData();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
