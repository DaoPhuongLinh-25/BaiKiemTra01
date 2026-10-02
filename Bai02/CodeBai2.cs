using System;
using System.Collections.Generic;
using System.Linq;

abstract class PhuongTien
{
    private string _maPT;
    private string _tenHang;
    private int _namSanXuat;
    private decimal _giaGoc;

    public string MaPT
    {
        get => _maPT;
        set => _maPT = string.IsNullOrWhiteSpace(value) ? "PT000" : value;
    }

    public string TenHang
    {
        get => _tenHang;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên hãng không được để trống.");
            _tenHang = value;
        }
    }

    public int NamSanXuat
    {
        get => _namSanXuat;
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException("Năm sản xuất không hợp lệ.");
            _namSanXuat = value;
        }
    }

    public decimal GiaGoc
    {
        get => _giaGoc;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Giá gốc phải > 0.");
            _giaGoc = value;
        }
    }

    public PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
    {
        MaPT = maPT;
        TenHang = tenHang;
        NamSanXuat = namSanXuat;
        GiaGoc = giaGoc;
    }

    public abstract decimal TinhGiaLanBanh();

    public virtual string GetInfo()
    {
        return $"{MaPT} - {TenHang} - {NamSanXuat} - Giá gốc: {GiaGoc:N0}";
    }
}

class OTo : PhuongTien
{
    public int SoChoNgoi { get; set; }
    public double DungTichDongCo { get; set; }

    public OTo(string maPT, string tenHang, int nam, decimal gia,
               int soCho, double dungTich)
        : base(maPT, tenHang, nam, gia)
    {
        if (soCho <= 0 || dungTich <= 0)
            throw new ArgumentException("Thông tin ô tô không hợp lệ.");

        SoChoNgoi = soCho;
        DungTichDongCo = dungTich;
    }

    public override decimal TinhGiaLanBanh()
    {
        return SoChoNgoi <= 9
            ? GiaGoc * 1.42m
            : GiaGoc * 1.10m;
    }

    public override string GetInfo()
    {
        return base.GetInfo() +
               $" - Số chỗ: {SoChoNgoi} - Động cơ: {DungTichDongCo}L";
    }
}

class XeMay : PhuongTien
{
    public int DungTichXylanh { get; set; }

    public XeMay(string maPT, string tenHang, int nam, decimal gia, int cc)
        : base(maPT, tenHang, nam, gia)
    {
        if (cc <= 0)
            throw new ArgumentException("Dung tích xy-lanh phải > 0.");

        DungTichXylanh = cc;
    }

    public override decimal TinhGiaLanBanh()
    {
        return DungTichXylanh < 175
            ? GiaGoc * 1.02m
            : GiaGoc * 1.05m;
    }

    public override string GetInfo()
    {
        return base.GetInfo() +
               $" - Xy-lanh: {DungTichXylanh}cc";
    }
}

class QuanLyPhuongTien
{
    private List<PhuongTien> danhSach = new();

    public void AddPhuongTien(PhuongTien pt)
    {
        danhSach.Add(pt);
    }

    public void DisplayAll()
    {
        foreach (var pt in danhSach)
        {
            Console.WriteLine(pt.GetInfo());
            Console.WriteLine($"Giá lăn bánh: {pt.TinhGiaLanBanh():N0}");
            Console.WriteLine();
        }
    }

    public PhuongTien? FindMaxGiaLanBanh()
    {
        return danhSach
            .OrderByDescending(pt => pt.TinhGiaLanBanh())
            .FirstOrDefault();
    }

    public List<PhuongTien> SearchByName(string keyword)
    {
        return danhSach
            .Where(pt => pt.TenHang.Contains(
                keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        QuanLyPhuongTien ql = new();

        ql.AddPhuongTien(
            new OTo("OT01", "Toyota", 2022, 700000000m, 7, 2.0));

        ql.AddPhuongTien(
            new OTo("OT02", "Ford", 2023, 900000000m, 16, 2.5));

        ql.AddPhuongTien(
            new XeMay("XM01", "Honda", 2024, 50000000m, 150));

        ql.AddPhuongTien(
            new XeMay("XM02", "Yamaha", 2023, 80000000m, 200));

        // Hiển thị toàn bộ
        ql.DisplayAll();

        // Tìm phương tiện có giá lăn bánh cao nhất
        var max = ql.FindMaxGiaLanBanh();
        Console.WriteLine("Phương tiện giá lăn bánh cao nhất:");
        Console.WriteLine(max?.GetInfo());

        // Tìm theo tên hãng
        Console.WriteLine("\nKết quả tìm kiếm Toyota:");
        foreach (var pt in ql.SearchByName("Toyota"))
            Console.WriteLine(pt.GetInfo());
    }
}
