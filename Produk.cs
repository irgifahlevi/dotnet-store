public class Produk
{
    public string Sku { get; set; }
    public string Nama { get; set; }
    public int Stock { get; set; }
    public int Harga { get; set; }


    public Produk(string sku, string nama, int stock, int harga)
    {
        Sku = sku;
        Nama = nama;
        Stock = stock;
        Harga = harga;
    }
}