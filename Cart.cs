public class Cart
{
    public string Sku { get; set; }
    public string Nama { get; set; }
    public string Quantity { get; set; }
    public string Harga { get; set; }


    public Cart(string sku, string nama, string quantity, string harga)
    {
        Sku = sku;
        Nama = nama;
        Quantity = quantity;
        Harga = harga;
    }
}