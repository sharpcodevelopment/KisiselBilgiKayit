using KisiselBilgiKayit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KisilerDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=KisilerDB;Trusted_Connection=True;");

        using (var db = new KisilerDbContext(optionsBuilder.Options))
        {
            bool devamEt = true;

            while (devamEt)
            {
                Console.Clear();
                Console.WriteLine("Kişisel Bilgi Yönetim Sistemi");
                Console.WriteLine("1. Kişi Ekle");
                Console.WriteLine("2. Kişi Düzenle");
                Console.WriteLine("3. Kişi Sil");
                Console.WriteLine("4. Kişi Görüntüle");
                Console.WriteLine("5. Tüm Kişileri Listele");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        KisiEkle(db);
                        break;
                    case "2":
                        KisiDuzenle(db);
                        break;
                    case "3":
                        KisiSil(db);
                        break;
                    case "4":
                        KisiGoruntule(db);
                        break;
                    case "5":
                        TumKisileriListele(db);
                        break;
                    case "6":
                        devamEt = false;
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }

            Console.WriteLine("Oyundan çıkılıyor. Teşekkürler!");
        }
    }

    static void KisiEkle(KisilerDbContext db)
    {
        Console.Write("Kişi adı: ");
        string ad = Console.ReadLine();
        Console.Write("Kişi soyadı: ");
        string soyad = Console.ReadLine();
        Console.Write("Telefon numarası: ");
        string telefon = Console.ReadLine();
        Console.Write("E-posta: ");
        string email = Console.ReadLine();

        var kisi = new Kisi { ad = ad, soyad = soyad, telefon = telefon, email = email };
        db.kisiler.Add(kisi);
        db.SaveChanges();

        Console.WriteLine("Kişi eklendi!");
        Console.ReadKey();
    }

    static void KisiDuzenle(KisilerDbContext db)
    {
        TumKisileriListele(db);
        Console.Write("Düzenlemek istediğiniz kişinin ID'si: ");
        int id = int.Parse(Console.ReadLine());

        var kisi = db.kisiler.Find(id);
        if (kisi != null)
        {
            Console.Write("Yeni kişi adı (boş bırakmak istemiyorsanız): ");
            string ad = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ad)) kisi.ad = ad;

            Console.Write("Yeni kişi soyadı (boş bırakmak istemiyorsanız): ");
            string soyad = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(soyad)) kisi.soyad = soyad;

            Console.Write("Yeni telefon numarası (boş bırakmak istemiyorsanız): ");
            string telefon = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telefon)) kisi.telefon = telefon;

            Console.Write("Yeni e-posta (boş bırakmak istemiyorsanız): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email)) kisi.email = email;

            db.SaveChanges();

            Console.WriteLine("Kişi bilgileri güncellendi!");
        }
        else
        {
            Console.WriteLine("Geçersiz ID.");
        }

        Console.ReadKey();
    }

    static void KisiSil(KisilerDbContext db)
    {
        TumKisileriListele(db);
        Console.Write("Silmek istediğiniz kişinin ID'si: ");
        int id = int.Parse(Console.ReadLine());

        var kisi = db.kisiler.Find(id);
        if (kisi != null)
        {
            db.kisiler.Remove(kisi);
            db.SaveChanges();

            Console.WriteLine("Kişi silindi!");
        }
        else
        {
            Console.WriteLine("Geçersiz ID.");
        }

        Console.ReadKey();
    }

    static void KisiGoruntule(KisilerDbContext db)
    {
        TumKisileriListele(db);
        Console.Write("Görüntülemek istediğiniz kişinin ID'si: ");
        int id = int.Parse(Console.ReadLine());

        var kisi = db.kisiler.Find(id);
        if (kisi != null)
        {
            Console.WriteLine($"ID: {kisi.id}, Ad: {kisi.ad}, Soyad: {kisi.soyad}, Telefon: {kisi.telefon}, Email: {kisi.email}");
        }
        else
        {
            Console.WriteLine("Geçersiz ID.");
        }

        Console.ReadKey();
    }

    static void TumKisileriListele(KisilerDbContext db)
    {
        var kisiler = db.kisiler.ToList();

        Console.WriteLine("Tüm Kişiler:");
        foreach (var kisi in kisiler)
        {
            Console.WriteLine($"ID: {kisi.id}, Ad: {kisi.ad}, Soyad: {kisi.soyad}, Telefon: {kisi.telefon}, Email: {kisi.email}");
        }

        Console.ReadKey();
    }
}
