using System;
using System.Windows.Forms;

namespace Lab05_MultiForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            // 1. Mở Form Đăng nhập trước
            frmDangNhap fLogin = new frmDangNhap();

            // 2. Nếu đăng nhập thành công (admin / 123456) mới khởi chạy Form1
            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form1());
            }
        }
    }
}