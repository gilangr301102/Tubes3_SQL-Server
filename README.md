# Tubes3_SQL-Server

 ### Algoritma KMP:

Algoritma Knuth-Morris-Pratt (KMP) merupakan algoritma pencocokan pola yang memodifikasi algoritma brute-force. Pada algoritma brute-force, pergeseran dilakukan satu posisi setiap kali ditemukan ketidakcocokan. Sedangkan pada algoritma KMP, pergeseran posisi dapat lebih banyak, sehingga jumlah karakter yang diperiksa menjadi lebih sedikit. Ketika ditemukan ketidakcocokan antara teks dan pola P pada P[j], pergeseran posisi pola terbesar yang dapat dilakukan untuk menghindari perbandingan yang tidak perlu adalah sebesar ukuran prefix P[0 .. j-1] yang juga merupakan suffix dari P[1 .. j-1]. Untuk mendapatkan nilai tersebut pada setiap posisi di pola, dilakukan preprocessing dengan menghitung fungsi pinggiran KMP atau border function b(k). Setelah fungsi pinggiran dihitung, pencarian string dimulai dengan pergeseran posisi sesuai dengan fungsi tersebut saat terdapat ketidakcocokan. Adapun pada gambar di bawah ini dapat dilihat ilustrasi pencocokan pola dengan algoritma KMP.


### Algoritma Boyer Moore:

Algoritma Boyer-Moore adalah algoritma pencocokan pola yang menggabungkan dua teknik, yaitu teknik cermin dan teknik loncatan karakter. Pada varian yang menggunakan teknik looking-glass, pencocokan pola P dalam teks T dilakukan dari akhir P ke awal. Sedangkan pada varian yang menggunakan teknik character-jump, jika ditemukan ketidakcocokan karakter, salah satu dari tiga hal berikut dilakukan:
1. Jika pola P mengandung karakter yang menyebabkan ketidakcocokan di teks, geser P ke kanan sehingga karakter tersebut sejajar dengan kemunculan terakhir karakter yang sama dalam pola P.
2. Jika pola P mengandung karakter yang menyebabkan ketidakcocokan di teks, tetapi pergeseran ke kanan tidak mungkin menghindari kondisi sebelumnya, geser P sebanyak satu karakter.
3. Jika pola P tidak memenuhi kondisi pertama dan kedua, geser P sehingga karakter awal pola sejajar dengan satu karakter setelah karakter di teks yang menyebabkan ketidakcocokan.

### Regular Expression (Regex) 
Regular Expression (RE) ialah sebuah notasi yang digunakan untuk menjabarkan pola kata yang diinginkan. Awal mula konsep tentang regex tercatat pada tahun 1951, saat Stephen Cole Kleene, seorang matematikawan, merumuskan definisi mengenai bahasa formal. Di bidang pemrograman, RE digunakan untuk berbagai tujuan, seperti memvalidasi data, mencari, fitur penemuan dan penggantian, dan sebagainya.

### Algoritma LCS
Adapun pengukuran persentase dilakukan perhitungan terhadap subsequence yang muncul pada kedua string kemiripan yang dilakukan adalah dengan menggunakan metode Longest Common Subsequence (LCS), yang didefinisikan sebagai urutan terpanjang yang common di semua urutan masukan yang diberikan. Misalkan diberikan dua string, S1 dan S2 , untuk mencari LCS tersebut kita dapat mencari panjang Barisan Persekutuan Terpanjang, yaitu barisan terpanjang yang terdapat pada kedua string.

## How to Run Backend
 - clone repository ini terlebih dahulu
 - cd ke directory backend pada src dengan command `cd src` dan `cd backend`
 > Cara 1: Dengan menggunakan `Terminal or Command Prompt`.
 1. Migrate the data from dataset .BMP files: `dotnet ef database update .`
 2. Build: `dotnet build`
 3. Run: `dotnet run`

> Cara 2: Dengan menggunakan `Visual Studio`.
 or you can build and run on the project solution by Visual Studio instead.

## How to Run Frontend
- clone repository ini terlebih dahulu
- cd ke directory backend pada src dengan command `cd src` dan `cd frontend`
 > Cara 1: Dengan menggunakan `Terminal or Command Prompt`.
 1. Migrate the data from dataset .BMP files: `dotnet ef database update .`
 2. Build: `dotnet build`
 3. Run: `dotnet run`

> Cara 2: Dengan menggunakan `Visual Studio`.
 or you can build and run on the project solution by Visual Studio instead.

### Fitur yang disediakan:
- Tombol Insert citra sidik jari, beserta display citra sidik jari yang ingin dicari
- Toggle Button untuk memilih algoritma yang ingin digunakan (KMP atau BM)
- Tombol Search untuk memulai pencarian
- Display sidik jari yang paling mirip dari basis data
- Informasi mengenai waktu eksekusi
- Informasi mengenai tingkat kemiripan sidik jari dengan gambar yang ingin dicari,
dalam persentase (%)
- List biodata hasil pencarian dari basis data. Keluarkan semua nilai atribut dari
individu yang dirasa paling mirip. Perlu diperhatikan pendefinisian batas kemiripan
dapat memunculkan kemungkinan tidak ditemukan list biodata yang memiliki sidik
jari paling mirip.

### Tata cara penggunaan program:
- Jalankan program, kemudian akan diupload gambar dari file dataset 
- Akan dicocokkan pada pada data berkas citra hasil konversi dari gambar yang diupload sebagai input 
- Akan dikeluarkan output berupa list of biodata dan sidik jari yang cocok dengan batas threshold similarity beserta waktu pencarian dan persentase kecocokan yang didapatkan pada setiap sidik jari yang dilakukan.


# Anggota Kelompok

| Nama | NIM |
| ---- | --- |
| Muhammad Fuad Nugraha         | 10023520 |
| Rafii Ahmad Fahreza           | 10023570 |
| Muhammad Gilang Ramadhan      | 13520137 |
| Zachary Samuel Tobing         | 13522016 |