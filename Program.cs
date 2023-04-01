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
    Console.WriteLine("8. Checkout");
    Console.WriteLine("9. Keluar");
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
            Console.WriteLine("+-----------+-----------------+---------+-----------+");
            Console.WriteLine("|                     List produk                   |");
            if (modelProduk.Count > 0)
            {
                Console.WriteLine("+-----------+-----------------+---------+-----------+");
                Console.WriteLine("|    SKU    |       NAMA      |  STOCK  |   HARGA   |");
                Console.WriteLine("+-----------+-----------------+---------+-----------+");
                foreach(var index in modelProduk)
                {
                    Console.WriteLine("|    {0}    | {1,-15} | {2,7} | {3,10} |", index.Sku, index.Nama, index.Stock, index.Harga);
                }
                Console.WriteLine("+-----------+-----------------+---------+-----------+");
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

            var produk = modelProduk.Find(c => c.Sku == chartSku);

            if(produk != null)
            {   
                Console.Write("Masukan jumlah produk : ");
                string qty = Console.ReadLine();
                int intQty = Convert.ToInt32(qty);

                if(intQty <= produk.Stock)
                {
                    produk.Stock -= intQty;
                    var cartItem = modelCart.Find(c => c.Sku == produk.Sku && c.Nama == produk.Nama);
                    if (cartItem != null)
                    {
                        cartItem.Quantity += intQty;
                        cartItem.Harga += produk.Harga * intQty;
                    }
                    else
                    {
                        cartItem = new Cart(produk.Sku, produk.Nama, intQty, produk.Harga * intQty);
                        modelCart.Add(cartItem);
                    }
                    e2.TriggerCartEvent("Berhasil dimasukan ke cart");
                }
                else
                {
                    e2.TriggerCartEvent("Stok produk tidak mencukupi");
                }
            }
            else
            {
                Console.WriteLine("Produk tidak ada");
            }
            Console.ReadLine();
            break;
        case "6" :
            Console.WriteLine("===========================");
            Console.WriteLine("Hapus produk dari cart");
            Console.WriteLine("===========================");
            Console.Write("Masukan sku : ");
            string hCart = Console.ReadLine();

            var produkCart = modelCart.Find(c => c.Sku == hCart);

            if(produkCart != null)
            {
                var produk2 = modelProduk.Find(c => c.Sku == produkCart.Sku);

                produk2.Stock += produkCart.Quantity;

                modelCart.Remove(produkCart);

                e2.TriggerCartEvent("Produk berhasil di hapus dari cart");
            }
            else
            {
                e2.TriggerCartEvent("Produk tidak ada di cart");
            }
            Console.ReadLine();
            break;
        case "7" :
            Console.WriteLine("===========================");
            Console.WriteLine("Cart produk");
            Console.WriteLine("===========================");
            if (modelCart.Count > 0)
            {
                Console.WriteLine("|    SKU|    NAMA|   Quantity|   HARGA|");
                foreach(var index in modelCart)
                {
                    Console.WriteLine("|    {0}|    {1}|    {2}|    {3}|", index.Sku, index.Nama, index.Quantity, index.Harga);
                }
            }
            else
            {
                e.TriggerEvent("Cart produk masih kosong");
            }
            Console.ReadLine();
            break;
        case "8" :
            Console.WriteLine("===========================");
            Console.WriteLine("Cekout produk");
            Console.WriteLine("===========================");
            if (modelCart.Count > 0)
            {
                Console.WriteLine("|    SKU|    NAMA|   Quantity|   HARGA|");
                foreach(var index in modelCart)
                {
                    Console.WriteLine("|    {0}|    {1}|    {2}|    {3}|", index.Sku, index.Nama, index.Quantity, index.Harga);
                }

                int totalProduk = 0;

                foreach(var index in modelCart)
                {
                    totalProduk += index.Quantity;
                }

                int totalHarga = modelCart.Sum(item => item.Harga);

                Console.WriteLine("Total produk : {0}, Total bayar : {1}", totalProduk, totalHarga);

                Console.Write("Checkout sekarang? bayar/cancel : ");
                string inputCheckout = Console.ReadLine();

                if(inputCheckout == "bayar")
                {
                    foreach(var item in modelCart)
                    {
                        var produk3 = modelProduk.Find(p => p.Sku == item.Sku);
                        produk3.Stock += item.Quantity;
                    }
                    modelCart.Clear();

                    e2.TriggerCartEvent("Pembayaran berhasil. Terima kasih!");
                }
                else if(inputCheckout == "cancel")
                {
                    e2.TriggerCartEvent("Pembayaran dibatalkan");
                }
                else
                {
                    e2.TriggerCartEvent("Inputan tidak valid");
                }
            }
            else
            {
                e2.TriggerCartEvent("Cart masih kosong, add cart terlebih dahulu");
            }

            Console.ReadLine();
            break;
        case "9" :
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