namespace END2
{
    public partial class Form1 : Form
    {
        public string LastSelectedButton = "";
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            this.MouseClick += Form1_Click;
            button6.Visible = false;
            button1_Click(null, null);
            button1.Select();
            LastSelectedButton = "button1";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void UpdateCustomerInDb(int customerId, string customerName, string phone)
        {
            try
            {
                var db = new Models.SuperMarketDbContext();
                var customer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                if (customer != null)
                {
                    customer.CustomerName = customerName;
                    customer.Phone = phone;
                    db.SaveChanges();
                    button1_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateProductInDb(int productId, string productName, decimal price, int stock)
        {
            try
            {
                var db = new Models.SuperMarketDbContext();
                var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    product.ProductName = productName;
                    product.Price = price;
                    product.Stock = stock;
                    db.SaveChanges();
                    button2_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(LastSelectedButton =="button3")
            {
                return;
            }
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];


                if (selectedRow.Cells.Count > 2)
                {
                    textBox2.Text = selectedRow.Cells[1].Value?.ToString();
                    textBox3.Text = selectedRow.Cells[2].Value?.ToString();
                }

                if (LastSelectedButton != "button1" && selectedRow.Cells.Count > 3)
                {
                    textBox4.Text = selectedRow.Cells[3].Value?.ToString();
                }

                button4.Text = "Update";
                button6.Visible = true;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 || dataGridView1.SelectedCells.Count > 0)
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

                dataGridView1.ClearSelection();
                button4.Text = "Add";
                button6.Visible = false;
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            Form1_Click(sender, e);
            LastSelectedButton = "button1";
            var customers = new Models.SuperMarketDbContext().Customers.Select(c => new
            {
                c.CustomerId,
                c.CustomerName,
                c.Phone
            }).ToList();
            dataGridView1.DataSource = customers;

            label1.Text = "Customer Name ";
            label2.Text = "Phone";
            label3.Visible = false;
            textBox4.Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            var Search = textBox1.Text.ToLower();
            if (LastSelectedButton == "button1")
            {
                var customers = new Models.SuperMarketDbContext().Customers
                    .Where(c => c.CustomerName.ToLower().Contains(Search) || c.Phone.ToLower().Contains(Search))
                    .Select(c => new
                    {
                        c.CustomerId,
                        c.CustomerName,
                        c.Phone
                    }).ToList();
                dataGridView1.DataSource = customers;
            }
            else if (LastSelectedButton == "button2")
            {
                var products = new Models.SuperMarketDbContext().Products
                    .Where(p => p.ProductName.ToLower().Contains(Search) || p.Price.ToString().Contains(Search) || p.Stock.ToString().Contains(Search))
                    .Select(p => new
                    {
                        p.ProductId,
                        p.ProductName,
                        p.Price,
                        p.Stock
                    }).ToList();
                dataGridView1.DataSource = products;
            }
            else if (LastSelectedButton == "button3")
            {
                var sales = new Models.SuperMarketDbContext().Sales
                    .Where(s => s.CustomerId.ToString().Contains(Search) || s.ProductId.ToString().Contains(Search) || s.Quantity.ToString().Contains(Search) || s.SaleDate.ToString().Contains(Search))
                    .Select(s => new
                    {
                        s.CustomerId,
                        s.ProductId,
                        s.Quantity,
                        s.SaleDate
                    }).ToList();
                dataGridView1.DataSource = sales;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1_Click(sender, e); 
            LastSelectedButton = "button2";
            var products = new Models.SuperMarketDbContext().Products.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.Price,
                p.Stock
            }).ToList();
            dataGridView1.DataSource = products;
            label1.Text = "Product Name ";
            label2.Text = "Price";
            label3.Visible = true;
            textBox4.Visible = true;
            label3.Text = "Stock";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1_Click(sender, e);
            button6.Visible = false;
            LastSelectedButton = "button3";
            var sales = new Models.SuperMarketDbContext().Sales.Select(s => new
            {
                s.CustomerId,
                s.ProductId,
                s.Quantity,
                s.SaleDate
            }).ToList();
            dataGridView1.DataSource = sales;
            label1.Text = "Customer ID ";
            label2.Text = "Product ID";
            label3.Visible = true;
            textBox4.Visible = true;
            label3.Text = "Quantity";


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Update")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    if (LastSelectedButton == "button1")
                    {
                        int customerId = Convert.ToInt32(selectedRow.Cells[0].Value);
                        string customerName = textBox2.Text;
                        string phone = textBox3.Text;
                        UpdateCustomerInDb(customerId, customerName, phone);
                    }
                    else if (LastSelectedButton == "button2")
                    {
                        int productId = Convert.ToInt32(selectedRow.Cells[0].Value);
                        string productName = textBox2.Text;
                        decimal price;
                        int stock;
                        if (!decimal.TryParse(textBox3.Text, out price) || !int.TryParse(textBox4.Text, out stock) || price < 0 || stock < 0)
                        {
                            MessageBox.Show("Please enter valid numbers for Price and Stock.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        UpdateProductInDb(productId, productName, price, stock);
                    }
                }
                button4.Text = "Add";
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dataGridView1.ClearSelection();
            }
            else
            {

                if (LastSelectedButton == "button1")
                {
                    if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var customer = new Models.Customer()
                    {

                        CustomerName = textBox2.Text,
                        Phone = textBox3.Text
                    };
                    try
                    {
                        var db = new Models.SuperMarketDbContext();
                        db.Customers.Add(customer);
                        db.SaveChanges();
                        button1_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                }
                else if (LastSelectedButton == "button2")
                {
                    if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var product = new Models.Product()
                    {
                        ProductName = textBox2.Text,
                        Price = decimal.Parse(textBox3.Text),
                        Stock = int.Parse(textBox4.Text)
                    };
                    try
                    {
                        var db = new Models.SuperMarketDbContext();
                        db.Products.Add(product);
                        db.SaveChanges();
                        button2_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                else if (LastSelectedButton == "button3")
                {
                    if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var sale = new Models.Sale()
                    {
                        CustomerId = int.Parse(textBox2.Text),
                        ProductId = int.Parse(textBox3.Text),
                        Quantity = int.Parse(textBox4.Text),

                    };

                    if (sale.Quantity <= 0 || sale.CustomerId <= 0 || sale.ProductId <= 0)
                    {
                        MessageBox.Show("Please enter valid positive numbers.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    var db = new Models.SuperMarketDbContext();
                    var customer = db.Customers.FirstOrDefault(c => c.CustomerId == sale.CustomerId);
                    if (customer == null)
                    {
                        MessageBox.Show("Customer not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var product = db.Products.FirstOrDefault(p => p.ProductId == sale.ProductId);
                    if (product == null)
                    {
                        MessageBox.Show("Product not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (product.Stock < sale.Quantity)
                    {
                        MessageBox.Show("Insufficient stock for the product.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    product.Stock -= sale.Quantity;
                    sale.SaleDate = DateTime.Now;

                    db.Products.Update(product);

                    try
                    {

                        db.Sales.Add(sale);
                        db.SaveChanges();
                        button3_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                try
                {
                    var db = new Models.SuperMarketDbContext();
                    if (LastSelectedButton == "button1")
                    {
                        int customerId = Convert.ToInt32(selectedRow.Cells[0].Value);
                        var customer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                        if (customer != null)
                        {
                            var result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No) return;
                            db.Customers.Remove(customer);
                            db.SaveChanges();
                            button1_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Customer not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (LastSelectedButton == "button2")
                    {
                        int productId = Convert.ToInt32(selectedRow.Cells[0].Value);
                        var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
                        if (product != null)
                        {
                            
                            var result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No) return;
                            db.Products.Remove(product);
                            db.SaveChanges();
                            button2_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Product not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dataGridView1.ClearSelection();
                button4.Text = "Add";
                button6.Visible = false;
            }

        }
    }
}
