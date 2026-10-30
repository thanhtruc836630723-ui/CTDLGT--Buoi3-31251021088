using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT__Buoi3_31251021088
{
    internal class Bai_01
    {
        public static void Main4444(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Nhập chuỗi chứa các dấu ngoặc từ bàn phím
            string s = Console.ReadLine();
            // Khởi tạo Stack lưu trữ 2 thông tin:
            // Item1: Ký tự ngoặc mở
            // Item2: Vị trí xuất hiện của ngoặc mở trong chuỗi
            Stack<(char, int)> st = new Stack<(char, int)>();
            // Duyệt lần lượt từng ký tự trong chuỗi
            for (int i = 0; i < s.Length; i++)
            {
                // Lấy ký tự tại vị trí i
                char c = s[i];
                if (c == '(' || c == '[' || c == '{')
                {
                    // Đưa ngoặc mở và vị trí của nó vào Stack
                    st.Push((c, i));
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    // Kiểm tra Stack có rỗng không
                    // Nếu rỗng nghĩa là có ngoặc đóng nhưng không có ngoặc mở tương ứng
                    if (st.Count == 0)
                    {
                        Console.WriteLine("SAI tại vị trí " + i);
                        return;
                    }
                    // Lấy ngoặc mở ở đỉnh Stack để kiểm tra
                    // Peek() chỉ xem phần tử, không xóa phần tử khỏi Stack
                    char NgoacMo = st.Peek().Item1;
                    // Kiểm tra ngoặc mở và ngoặc đóng có cùng loại hay không
                    if ((c == ')' && NgoacMo != '(') || (c == ']' && NgoacMo != '[') || (c == '}' && NgoacMo != '{'))
                    {
                        // Nếu không khớp thì thông báo vị trí sai
                        Console.WriteLine("SAI tại vị trí " + i);
                        return;
                    }
                    // Nếu ngoặc khớp thì xóa ngoặc mở khỏi Stack
                    // Pop() lấy và xóa phần tử ở đỉnh Stack
                    st.Pop();
                }
            }

            // Sau khi duyệt hết chuỗi, kiểm tra Stack còn phần tử không
            if (st.Count > 0)
            {
                // Khởi tạo vị trí sai ban đầu bằng độ dài chuỗi
                int ViTri = s.Length;
                // Lặp cho đến khi Stack rỗng
                while (st.Count > 0)
                {
                    // Lấy vị trí của ngoặc mở còn dư trong Stack
                    // Đồng thời xóa phần tử đó khỏi Stack
                    int pos = st.Pop().Item2;
                    // Tìm vị trí nhỏ nhất trong các ngoặc mở còn dư
                    if (pos < ViTri)
                        ViTri = pos;
                }
                // Thông báo vị trí ngoặc mở chưa được đóng
                Console.WriteLine("SAI tại vị trí " + ViTri);
            }
            else
                // Nếu Stack rỗng nghĩa là tất cả ngoặc đều hợp lệ
                Console.WriteLine("HOP LE");
        }
    }
}
