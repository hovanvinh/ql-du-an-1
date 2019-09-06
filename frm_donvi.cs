using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QL_Duan
{
    public partial class frm_donvi : DevExpress.XtraEditors.XtraForm
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        public frm_donvi()
        {
            InitializeComponent();
            // Cài đặt thuộc tính OFD
            xtraOpenFileDialog1.Title = "Select Picture";
            xtraOpenFileDialog1.Filter = "Windows Bitmap|*.bmp|JPEG Image|*.jpg|All Files|*.*";
        }
        //ảnh -> byte[]
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        //byte[] -> ảnh
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void frm_donvi_Load(object sender, EventArgs e)
        {
            try
            {
                var collection = from c in data.Thongtindonvis
                                 select c;

                txt_Madonvi.Text = collection.Single().Madv;
                txt_Tendonvi.Text = collection.Single().Tendonvi;
                txt_Nguoidaidien.Text = collection.Single().Nguoidaidien;
                txt_Chucvu.Text = collection.Single().Chucvu;
                txt_Diachi.Text = collection.Single().Diachi;
                txt_Sodienthoai.Text = collection.Single().Sodienthoai;
                txt_Email.Text = collection.Single().Email;
                txt_Website.Text = collection.Single().Website;
                pic_Logo.Image = byteArrayToImage(collection.Single().Logo.ToArray());


            }
            catch
            {
                MessageBox.Show("Lỗi load dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void but_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void but_Update_Click(object sender, EventArgs e)
        {
            try
            {

                Thongtindonvi update_thongtin = data.Thongtindonvis.Single(p => p.Madv == txt_Madonvi.Text);
                update_thongtin.Tendonvi = txt_Tendonvi.Text;
                update_thongtin.Nguoidaidien = txt_Nguoidaidien.Text;
                update_thongtin.Chucvu = txt_Chucvu.Text;
                update_thongtin.Diachi = txt_Diachi.Text;
                update_thongtin.Sodienthoai = txt_Sodienthoai.Text;
                update_thongtin.Email = txt_Email.Text;
                update_thongtin.Website = txt_Website.Text;

                update_thongtin.Logo = imageToByteArray(pic_Logo.Image);
                data.SubmitChanges();
                MessageBox.Show("Bạn đã Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch
            {
                MessageBox.Show("Đường truyền Internet bị ngắt kết nối\n Kiểm tra lại đường truyền Internet của bạn. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } 
        }

        private void but_hinhanh_Click(object sender, EventArgs e)
        {
            // Show hộp thoại open file ra
            // Nhận kết quả trả về qua biến kiểu DialogResult
            DialogResult result = xtraOpenFileDialog1.ShowDialog();

            //Kiểm tra xem người dùng đã chọn file chưa
            if (result == DialogResult.OK)
            {
                // Lấy hình ảnh
                Image img = Image.FromFile(xtraOpenFileDialog1.FileName);

                // Gán ảnh
                pic_Logo.Image = img;
            }
        }
    }
}