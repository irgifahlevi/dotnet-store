// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
EventProduk e = new EventProduk();
e.Proses += ProsesSelesai;

void ProsesSelesai(object sender, string pesan)
{
    Console.WriteLine(pesan);
}

EventCart e2 = new EventCart();
e2.ProsesCart += ProsesCart;

void ProsesCart(object sender, string pesan)
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
var modelCart = new List<Cart>();

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
            Console.WriteLine("===========================");
            Console.WriteLine("Edit produk");
            Console.WriteLine("===========================");
            Console.WriteLine("Masukan sku : ");
            string eSku = Console.ReadLine();

            Produk editProduk = modelProduk.Find(p => p.Sku == eSku);
            
            if(editProduk != null)
            {
                Console.Write("Masukan nama baru : ");
                string namaBaru = Console.ReadLine();

                Console.Write("Masukan stock : ");
                string stockBaru = Console.ReadLine();

                Console.Write("Masukan harga : ");
                string hargaBaru = Console.ReadLine();

                int intStockBaru = Convert.ToInt32(stockBaru);
                int intHargaBaru = Convert.ToInt32(hargaBaru);

                editProduk.Nama = namaBaru;
                editProduk.Stock = intStockBaru;
                editProduk.Harga = intHargaBaru;

                e.TriggerEvent("Produk berhasil di update");
            }
            else
            {
               e.TriggerEvent("Sku yang dimasukan tidak cocok"); 
            }
            
            Console.ReadLine();
            break;
        case "3" :
            Console.WriteLine("===========================");
            Console.WriteLine("List produk");
            Console.WriteLine("===========================");
            if (modelProduk.Count > 0)
            {
                Console.WriteLine("|    SKU    |       NAMA      |  STOCK  |   HARGA   |");
                foreach(var index in modelProduk)
                {
                    Console.WriteLine("|    {0}    |       {1}       |   {2}   |    {3}    |", index.Sku, index.Nama, index.Stock, index.Harga);
                }
            }
            else
            {
                e.TriggerEvent("Produk masih kosong");
            }

            Console.ReadLine();
            break;
        case "4" :
            Console.WriteLine("===========================");
            Console.WriteLine("Hapus produk");
            Console.WriteLine("===========================");
            Console.Write("Masukan sku : ");
            string hSku = Console.ReadLine();

            var hapusProduk = modelProduk.Find(h => h.Sku == hSku);

            if(hapusProduk != null)
            {
                modelProduk.Remove(hapusProduk);
                e.TriggerEvent("Produk berhasil dihapus");
            }
            else
            {
                e.TriggerEvent("Sku yang di masukan tidak cocok");
            }
            Console.ReadLine();
            break;
        case "5" :
            Console.WriteLine("===========================");
            Console.WriteLine("Add produk cart");
            Console.WriteLine("===========================");
            Console.Write("Masukan sku : ");
            string chartSku = Console.ReadLine();

            var skuProduk = modelProduk.Find(c => c.Sku == chartSku);

            if(skuProduk != null)
            {   
                // Cart cartBaru = new Cart(skuProduk.Sku, skuProduk.Nama, +=1, skuProduk.Harga);
                // modelCart.Add(cartBaru);
                // e2.TriggerCartEvent("Berhasil di tambahkan ke cart");
            }

            Console.ReadLine();
            break;
        case "6" :
                    Console.WriteLine("===========================");
            Console.WriteLine("Edit produk");
            Console.WriteLine("===========================");
            break;
        case "7" :
                    Console.WriteLine("===========================");
            Console.WriteLine("Edit produk");
            Console.WriteLine("===========================");
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