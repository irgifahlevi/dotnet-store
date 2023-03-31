// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
EventProduk e = new EventProduk();
e.Proses += ProsesSelesai;

void ProsesSelesai(object sender, string pesan)
{
    Console.WriteLine(pesan);
}

void Display()
{
    Console.Clear();
    Console.WriteLine("Selamat datang di toko kami");
    Console.WriteLine("1. Tambah produk");
    Console.WriteLine("2. Edit produk");
    Console.WriteLine("3. Tampilkan produk");
    Console.WriteLine("4. Hapus produk");
    Console.WriteLine("5. Add produk ke cart");
    Console.WriteLine("6. Hapus produk dari cart");
    Console.WriteLine("7. Tampilkan cart");
    Console.WriteLine("8. Keluar");
    Console.Write("Masukkan pilihan : ");
}

var keluar = false;
var modelProduk = new List<Produk>();

while(!keluar)
{
    Display();

    string pilihan = Console.ReadLine();
    switch(pilihan)
    {
        case "1" :
            Console.WriteLine("===========================");
            Console.WriteLine("Tambah produk");
            Console.WriteLine("===========================");

            Console.Write("Masukan sku : ");
            string sku = Console.ReadLine();

            if(modelProduk.Any(p => p.Sku == sku)){
                e.TriggerEvent("Sku produk sudah ada");
                Console.ReadLine();
                break;
            }
            else
            {
                Console.Write("Masukan nama : ");
                string nama = Console.ReadLine();

                Console.Write("Masukan stock : ");
                string stock = Console.ReadLine();

                Console.Write("Masukan harga : ");
                string harga = Console.ReadLine();

                int intStock = Convert.ToInt32(stock);
                int intHarga = Convert.ToInt32(harga);


                Produk simpanProduk = new Produk(sku, nama, intStock, intHarga);
                modelProduk.Add(simpanProduk);
                e.TriggerEvent("Produk berhasil ditambahkan");
            }
            Console.ReadLine();
            break;
        case "2" :
            break;
        case "3" :
            break;
        case "4" :
            break;
        case "5" :
            break;
        case "6" :
            break;
        case "7" :
            break;
        case "8" :
            e.TriggerEvent("Anda telah keluar");
            keluar = true;
            Console.ReadLine();
            break;
        default:
            e.TriggerEvent("Pilihan yang dimasukan tidak ada");
            Console.ReadLine();
            break;
    }
}