using System.ComponentModel;
using System;

namespace Code
{
    public partial class frmMain : Form
    {
        private BindingList<Product> products = new BindingList<Product>();
        public frmMain()
        {
            InitializeComponent();
            dgvProducts.DataSource = products;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!");
                txtProductId.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                txtProductName.Focus();
                return;
            }

            if (cboCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục!");
                cboCategory.Focus();
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal price))
            {
                MessageBox.Show("Đơn giá không hợp lệ!");
                txtUnitPrice.Focus();
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Số lượng không hợp lệ!");
                txtQuantity.Focus();
                return;
            }

            Product product = new Product
            {
                ProductId = txtProductId.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Category = cboCategory.Text,
                UnitPrice = price,
                Quantity = quantity,
                Image = picAvatar.Image
            };

            products.Add(product);

            MessageBox.Show("Thêm sản phẩm thành công!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            Product product = dgvProducts.CurrentRow.DataBoundItem as Product;

            if (product == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                products.Remove(product);

                MessageBox.Show("Xóa sản phẩm thành công!");
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Product product = dgvProducts.Rows[e.RowIndex].DataBoundItem as Product;

            if (product == null)
                return;

            txtProductId.Text = product.ProductId;
            txtProductName.Text = product.ProductName;
            cboCategory.Text = product.Category;
            txtUnitPrice.Text = product.UnitPrice.ToString();
            txtQuantity.Text = product.Quantity.ToString();

            picAvatar.Image = product.Image;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
                return;
            }

            Product product = dgvProducts.CurrentRow.DataBoundItem as Product;

            if (product == null)
                return;

            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                return;
            }

            if (cboCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục!");
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal price))
            {
                MessageBox.Show("Đơn giá không hợp lệ!");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Số lượng không hợp lệ!");
                return;
            }

            product.ProductId = txtProductId.Text.Trim();
            product.ProductName = txtProductName.Text.Trim();
            product.Category = cboCategory.Text;
            product.UnitPrice = price;
            product.Quantity = quantity;
            product.Image = picAvatar.Image;

            dgvProducts.Refresh();

            MessageBox.Show("Cập nhật sản phẩm thành công!");
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn ảnh sản phẩm";
                openFileDialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picAvatar.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }
    }
}
