<h2>Câu 1: Value Types và Reference Types</h2>
<p>
    <b>Value Type:</b> Lưu trực tiếp giá trị của biến, ví dụ: <code>int</code>, <code>float</code>, <code>struct</code>.
    Biến được sao chép giá trị khi gán.
</p>
<p>
    <b>Reference Type:</b> Lưu tham chiếu đến đối tượng, ví dụ: <code>class</code>, <code>array</code>, <code>object</code>.
    Khi gán, các biến có thể cùng tham chiếu đến một đối tượng.
</p>
<p><b>Lưu ý:</b> Đối tượng Reference Type thường được cấp phát trên Heap; Value Type không phải lúc nào cũng nằm trên Stack.</p>

<h2>Câu 2: <code>init</code> và <code>set</code></h2>
<p>
    <code>set</code> cho phép thay đổi giá trị thuộc tính bất kỳ lúc nào.
    <code>init</code> chỉ cho phép gán giá trị khi khởi tạo đối tượng.
</p>
<pre><code>class Student {
    public string Name { get; init; }
}

Student s = new Student { Name = "An" };
// s.Name = "Bình"; // Lỗi
</code></pre>
<p><b>Thực tế:</b> Dùng <code>init</code> khi muốn dữ liệu không bị thay đổi sau khi khởi tạo.</p>

<h2>Câu 3: <code>virtual</code> và <code>override</code></h2>
<p>
    <code>virtual</code> được khai báo ở lớp cha để cho phép lớp con ghi đè.
    <code>override</code> được dùng ở lớp con để thay đổi cách triển khai.
</p>
<pre><code>class Animal {
    public virtual void Sound() {
        Console.WriteLine("Animal");
    }
}

class Dog : Animal {
    public override void Sound() {
        Console.WriteLine("Gâu gâu");
    }
}

Animal a = new Dog();
a.Sound(); // Gâu gâu
</code></pre>

<h2>Câu 4: Tại sao <code>static</code> không truy xuất qua Object?</h2>
<p>
    <code>static</code> thuộc về <b>Class</b>, không thuộc về từng Object.
    Vì vậy phải truy xuất bằng tên lớp.
</p>
<pre><code>class Student {
    public static int Count = 0;
}

Student.Count++; // Đúng

Student s = new Student();
// s.Count++;    // Không hợp lệ
</code></pre>
<p>
    <b>Tóm lại:</b> <code>static</code> → thuộc Class;
    thành phần không static → thuộc Object.
</p>
