using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CTDLGT__Buoi3_31251021088
{
    class Customer
    {
        // Số thứ tự khách hàng
        public int STT;

        // Thời điểm khách đến
        public long Den;

        // Thời gian phục vụ khách
        public long PhucVu;

        // Hàm khởi tạo thông tin khách hàng
        public Customer(int stt, long den, long phucVu)
        {
            STT = stt;
            Den = den;
            PhucVu = phucVu;
        }
    }

    internal class Bai_02
    {
        public static void Main44444(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Nhập số lượng khách hàng n và số quầy k
            Console.Write("Nhập n và k: ");
            string[] input = Console.ReadLine().Split(' ');
            int n = int.Parse(input[0]);
            int k = int.Parse(input[1]);

            // Khởi tạo Queue để lưu danh sách khách hàng
            Queue<Customer> q = new Queue<Customer>();

            // Nhập thông tin của n khách hàng
            Console.WriteLine("Nhập thông tin khách hàng: ");
            for (int i = 1; i <= n; i++)
            {
                // Nhập thời điểm đến và thời gian phục vụ
                string[] a = Console.ReadLine().Split(' ');

                long den = long.Parse(a[0]);
                long phucVu = long.Parse(a[1]);

                // Tạo khách hàng mới
                Customer kh = new Customer(i, den, phucVu);

                // Đưa khách hàng vào cuối hàng đợi
                q.Enqueue(kh);
            }

            // Mảng lưu thời điểm rảnh của từng quầy
            // Ban đầu tất cả các quầy đều rảnh tại thời điểm 0
            long[] quayRanh = new long[k];

            // Tổng thời gian chờ của tất cả khách hàng
            long tongCho = 0;

            // Thời gian chờ lâu nhất
            long choLauNhat = 0;

            // Thời điểm hoàn thành phục vụ cuối cùng
            long hoanThanh = 0;

            // Duyệt hàng đợi cho đến khi không còn khách
            while (q.Count > 0)
            {
                // Lấy khách hàng đầu tiên ra khỏi Queue
                Customer kh = q.Dequeue();

                // Giả sử quầy đầu tiên là quầy rảnh sớm nhất
                int quay = 0;

                // Duyệt các quầy để tìm quầy rảnh sớm nhất
                for (int i = 1; i < k; i++)
                {
                    // Nếu quầy hiện tại rảnh sớm hơn quầy đang chọn
                    if (quayRanh[i] < quayRanh[quay])
                    {
                        // Cập nhật quầy được chọn
                        quay = i;
                    }
                }

                // Thời điểm bắt đầu phục vụ
                // Lấy giá trị lớn hơn giữa thời điểm quầy rảnh và thời điểm khách đến
                long batDau = Math.Max(quayRanh[quay], kh.Den);

                // Tính thời gian khách phải chờ
                long cho = batDau - kh.Den;

                // Tính thời điểm kết thúc phục vụ
                long xong = batDau + kh.PhucVu;

                // Cập nhật thời điểm rảnh mới của quầy
                quayRanh[quay] = xong;

                // Cộng thời gian chờ vào tổng thời gian chờ
                tongCho += cho;

                // Cập nhật thời gian chờ lâu nhất
                if (cho > choLauNhat)
                {
                    choLauNhat = cho;
                }

                // Cập nhật thời điểm hoàn thành cuối cùng
                if (xong > hoanThanh)
                {
                    hoanThanh = xong;
                }

                // Xuất thông tin từng khách hàng
                // quay + 1 vì chỉ số mảng bắt đầu từ 0
                Console.WriteLine($"KH {kh.STT}: đến {kh.Den}, quầy {quay + 1}, bắt đầu {batDau}, chờ {cho}, xong {xong}");
            }

            // Tính thời gian chờ trung bình
            double trungBinh = (double)tongCho / n;

            // Xuất thời gian chờ trung bình với 2 chữ số thập phân
            // InvariantCulture giúp sử dụng dấu chấm thay vì dấu phẩy
            Console.WriteLine($"Chờ trung bình: {trungBinh.ToString("F2", CultureInfo.InvariantCulture)}");

            // Xuất thời gian chờ lâu nhất
            Console.WriteLine($"Chờ lâu nhất: {choLauNhat}");

            // Xuất thời điểm hoàn thành cuối cùng
            Console.WriteLine($"Hoàn thành lúc: {hoanThanh}");
        }
    }
}
