using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT__Buoi3_31251021088
{
    using System;

    namespace CTDLGT__Buoi3_31251021088
    {
        // Lớp cài đặt hàng đợi vòng (Circular Queue) dạng Generic <T>
        public class CircularQueue<T>
        {
            private T[] _items;    // Mảng một chiều thuần túy để lưu dữ liệu
            private int _front;    // Chỉ số trỏ vào phần tử đầu hàng đợi
            private int _rear;     // Chỉ số trỏ vào vị trí sẽ nhận phần tử tiếp theo
            private int _count;    // Số lượng phần tử thực tế hiện có trong hàng đợi

            // Số phần tử hiện có
            public int Count => _count;

            // Dung lượng hiện tại của mảng (Capacity)
            public int Capacity => _items.Length;

            // Khởi tạo hàng đợi với dung lượng mặc định ban đầu là 4
            public CircularQueue()
            {
                _items = new T[4];
                _front = 0;
                _rear = 0;
                _count = 0;
            }

            // Phương thức mở rộng mảng gấp đôi khi đầy
            private void Resize()
            {
                int newCapacity = _items.Length * 2;
                T[] newArray = new T[newCapacity];

                // Chép tuần tự các phần tử theo đúng thứ tự logic FIFO từ _front sang mảng mới bắt đầu từ 0
                for (int i = 0; i < _count; i++)
                {
                    // Dùng toán tử chia lấy dư (%) để quay vòng mảng cũ
                    newArray[i] = _items[(_front + i) % _items.Length];
                }

                _items = newArray;
                _front = 0;         // Đưa đầu hàng đợi về chỉ số 0
                _rear = _count;     // Đặt vị trí chèn kế tiếp ngay sau phần tử cuối cùng
            }

            // Thêm phần tử vào cuối hàng đợi
            public void Enqueue(T value)
            {
                // Nếu hàng đợi đã đầy (số phần tử bằng dung lượng mảng), tiến hành nhân đôi
                if (_count == _items.Length)
                {
                    Resize();
                }

                // Gán giá trị vào vị trí rear hiện tại
                _items[_rear] = value;

                // Tăng chỉ số rear và quay vòng về 0 nếu chạm tới cuối mảng
                _rear = (_rear + 1) % _items.Length;

                _count++;
            }

            // Lấy và xóa phần tử ở đầu hàng đợi
            public T Dequeue()
            {
                // Ném ngoại lệ InvalidOperationException nếu hàng đợi rỗng
                if (_count == 0)
                {
                    throw new InvalidOperationException("Hang doi dang rong!");
                }

                // Lấy giá trị tại vị trí front
                T removedItem = _items[_front];

                // Xóa tham chiếu/giá trị cũ để giải phóng bộ nhớ
                _items[_front] = default(T);

                // Dịch chỉ số front sang vị trí tiếp theo và quay vòng (không dồn mảng, đạt O(1))
                _front = (_front + 1) % _items.Length;

                _count--;

                return removedItem;
            }

            // Xem phần tử ở đầu hàng đợi mà không xóa
            public T Peek()
            {
                // Ném ngoại lệ InvalidOperationException nếu hàng đợi rỗng
                if (_count == 0)
                {
                    throw new InvalidOperationException("Hang doi dang rong!");
                }

                return _items[_front];
            }
        }

        internal class Bai_03
        {
            public static void Main(string[] args)
            {
   
                Console.OutputEncoding = Encoding.UTF8;

                // Khởi tạo một hàng đợi vòng chứa chuỗi (string)
                CircularQueue<string> queue = new CircularQueue<string>();

                // Vòng lặp nhận lệnh tổng quát liên tục từ bàn phím
                while (true)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) break;

                    // Tách câu lệnh theo khoảng trắng (ví dụ: "ENQUEUE A", "DEQUEUE", "STATUS",...)
                    string[] parts = line.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string command = parts[0].ToUpper();

                    try
                    {
                        switch (command)
                        {
                            case "ENQUEUE":
                                // Cho phép thêm nhiều phần tử trên cùng 1 dòng (ví dụ: ENQUEUE A B C D)
                                for (int i = 1; i < parts.Length; i++)
                                {
                                    queue.Enqueue(parts[i]);
                                }
                                Console.WriteLine($"Da Enqueue. Count: {queue.Count}, Capacity: {queue.Capacity}");
                                break;

                            case "DEQUEUE":
                                // Nhập "DEQUEUE" hoặc "DEQUEUE 2" để lấy ra số lần tương ứng
                                int times = (parts.Length > 1) ? int.Parse(parts[1]) : 1;
                                for (int i = 0; i < times; i++)
                                {
                                    string val = queue.Dequeue();
                                    Console.WriteLine($"Dequeue: {val}");
                                }
                                Console.WriteLine($"Count hien tai: {queue.Count}, Capacity: {queue.Capacity}");
                                break;

                            case "PEEK":
                                // Xem phần tử đầu hàng đợi
                                Console.WriteLine($"Phan tu dau (Peek): {queue.Peek()}");
                                break;

                            case "STATUS":
                                // Hiển thị trạng thái hiện thời
                                Console.WriteLine($"Count: {queue.Count}, Capacity: {queue.Capacity}");
                                break;

                            case "EXIT":
                                return;

                            default:
                                Console.WriteLine("Lenh khong hop le (Dung cac lenh: ENQUEUE <val...>, DEQUEUE [so_lan], PEEK, STATUS, EXIT)");
                                break;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Bắt và in ra đúng ngoại lệ InvalidOperationException khi Peek/Dequeue trên hàng rỗng
                        Console.WriteLine($"[Loi]: {ex.GetType().Name} - {ex.Message}");
                    }
                }
            }
        }
    }

}
