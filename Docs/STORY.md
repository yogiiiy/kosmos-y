# KÓSMOS Y — Story & World Document

> Dokumen ini isinya cerita, dunia, dan karakter — bukan mekanik/angka (itu tetap di `GAME_LOOP.md`).
> Lahir dari sesi brainstorming yang sengaja "ngalor-ngidul" dulu — nostalgia sama anime/game favorit
> punya sebelum ditarik jadi arah konkret. Prinsipnya sama kayak `GAME_LOOP.md` §8: **loop fun dulu,
> cerita secukupnya buat ngasih bobot, gak semua harus dijelasin gamblang.**

---

## 1. Premis Inti

MC pulang ke desanya sendiri setelah lama pergi — dan mendapati desa itu nyaris hancur.
Reruntuhan di sekitarnya jadi sarang monster (slime, kelelawar), dan cuma tersisa segelintir
penduduk yang bertahan, berkumpul di sekitar tavern — satu-satunya tempat yang masih terasa aman.

MC jadi orang yang ikut menjaga keamanan itu tiap malam, supaya sisa penduduk yang ada
bisa bertahan — dan pelan-pelan, desa ini bisa hidup lagi.

Bukan cerita menyelamatkan dunia. Skalanya kecil dan personal: **satu desa, sedikit orang, satu api.**

---

## 2. Dunia & Setting

**Desa yang nyaris hancur, tapi belum mati.** Rumah-rumah roboh, sebagian ditinggal, tapi
masih ada beberapa penduduk yang bertahan di dekat tavern — cukup untuk bikin tempat ini
kerasa masih "rumah", bukan reruntuhan kosong generic.

**Rhythm siang-malam ala Stardew Valley / Harvest Moon.** Waktu berjalan lewat jam desa —
pagi dan sore adalah waktu tenang untuk berdagang, ngobrol, siap-siap. Begitu jam masuk
**pukul 18:00**, malam dimulai, dan dunia berubah.

**Kenapa malam berbeda dari siang (logika dunia):**
Monster ada sepanjang hari — kelihatan di reruntuhan, tapi di siang mereka enggan mendekat,
agak lesu, gampang diusir. Begitu malam, mereka jadi kuat dan agresif: **indra manusia
terbatas saat gelap** — penglihatan kabur, jarak pandang pendek. Head-to-head dengan manusia
yang "buta" di kegelapan, posisi monster jelas lebih untung. Makanya penduduk kumpul di
tavern, dan makanya MC kerjanya jaga malam.

*(Catatan: asal-usul *(Catatan: asal-usul pasti kenapa api ini bisa terus menyala, dan kenapa reruntuhan ini jadi
sarang monster — sengaja belum dijawab penuh. Bisa jadi ada sesuatu dari langit yang jatuh
dulu, atau cerita lain. Itu ranah project lanjutan kalau memang mau digali; game ini gak butuh
jawaban itu untuk selesai dengan baik.)*

---

## 3. Protagonist (MC)

Laki-laki. Bukan pahlawan legendaris — orang biasa yang kebetulan **pulang** ke desanya
sendiri di saat yang tepat (atau terlambat, tergantung sudut pandang mana yang dipakai).

Alasan dia pergi dan kenapa baru sekarang kembali **tidak dijelaskan gamblang**. Cukup
tersirat lewat 1-2 baris dialog NPC tavern di awal permainan (misal: *"Akhirnya kau kembali
juga..."*) — cukup untuk kasih bobot tanpa perlu cutscene flashback atau dialog panjang.
Player tidak perlu tahu detailnya; cukup mengerti MC "pulang untuk membenahi sesuatu."

---

## 4. NPC Tavern

Satu-satunya karakter yang benar-benar dekat dengan MC sepanjang game. Perannya ganda:
- **Toko** — tempat MC jual hasil buruan malam, beli potion & upgrade senjata.
- **Gerbang ke malam** — interaksi "Siap ke Malam" ada di sini juga.

Dialognya berubah sedikit tergantung sudah malam ke berapa — makin lama, nadanya makin
lega dan penuh harap, seiring desa yang pelan-pelan pulih. Perubahan ini murah untuk
diimplementasikan (cuma teks berbeda per state Night), tapi dampaknya besar untuk kerasa
seperti ada hubungan yang berkembang, bukan NPC toko statis.

---

## 5. Sisa Penduduk

Beberapa penduduk lain masih bertahan di desa — jumlah dan detail visualnya belum
diputuskan (bisa 1-2 sprite statis dulu, ditambah kalau ada waktu). Fungsi mereka bukan
sistem interaktif baru, tapi **bukti visual** bahwa MC melindungi sesuatu yang nyata, dan
tempat untuk menaruh progres pemulihan desa (lihat §6).

---

## 6. Progres Narasi per Night

Setiap kali MC berhasil bertahan satu malam, desa di siang berikutnya sedikit membaik.
Ini reuse langsung dari Night counter yang sudah ada di sistem (`GameManager`/`NightManager`),
cuma nambah swap sprite/state background — bukan sistem baru.

| Night | Kondisi desa (siang setelahnya) | Nada dialog NPC Tavern |
|---|---|---|
| 1 | Masih suram, penduduk sembunyi di dalam | Waspada, secukupnya, agak dingin ("Akhirnya kau kembali juga...") |
| 2 | Satu lentera mulai dinyalakan lagi | Mulai ada nada lega kecil |
| 3 | Satu papan penutup jendela dicopot | Mulai percaya sama MC |
| 4 | Penduduk mulai berani duduk di luar | Nada hangat, mulai ada harapan terbuka |
| 5 | Desa terasa jauh lebih hidup dari Night 1 | Tenang, hangat, sedikit haru menjelang ending |

---

## 7. Ending (setelah Night 5)

**Bukan "menang lalu selesai" — tapi momen tenang di fajar.**

Setelah Night 5 berhasil dilewati: layar transisi ke fajar, musik yang tadinya tegang
mereda. Suasana tavern tenang — ancaman mereda untuk saat ini, dan desa di luar mulai
terdengar hidup lagi.

MC dan NPC Tavern duduk berdampingan, santai. Tidak perlu dialog panjang. Desa di
latar belakang terlihat jauh lebih hidup dibanding Night 1 — bukti visual dari apa yang
sudah dilalui, tanpa perlu dinarasikan ulang.

**Rasa yang dituju:** sedih yang berujung syukur, capek tapi selamat — bukan tragedi
terbuka, bukan pengkhianatan takdir, bukan reveal berat yang meremukkan. Cukup satu
kalimat penutup dari NPC atau narasi singkat, semacam:

> *"Malam ini akhirnya tenang. Dan besok, mungkin akan lebih tenang lagi."*

*(placeholder — belum final, tinggal diracik ulang pas polish)*

---

## 8. Musik & Arahan Emosional

Leitmotif tunggal, dipakai ulang di beberapa mood:
- **Siang:** hangat, akustik, ringan (vibe Harvest Moon).
- **Malam:** tegang tapi tetap "indah" — bukan horror murni, lebih ke melankolis-megah
  (vibe Nier Automata / Clair Obscur, tapi jangan sampai lebih gelap dari itu).
- **Ending:** melodi yang **sama persis** dengan tema siang, direharmonisasi ke minor key,
  tempo diperlambat. Teknik murah (gak perlu compose dari nol), tapi kuat karena telinga
  player sudah familiar dengan melodinya dari siang-siang sebelumnya.

---

## 9. Yang Belum Diputuskan (sengaja ditunda)

- Asal-usul pasti api & kenapa reruntuhan jadi sarang monster.
- Nama MC dan nama NPC Tavern.
- Jumlah pasti & tampilan sisa penduduk desa.
- Kalimat penutup ending final (masih placeholder di §7).

Semua ini oke ditunda — sesuai prinsip "loop fun dulu, cerita secukupnya." Bisa diisi
belakangan pas polish, atau sengaja dibiarkan sebagai misteri kecil kalau memang pas.

---

## 10. Catatan Teknis — Perlu Disinkronkan ke GAME_LOOP.md

Dua hal dari sesi ini punya implikasi mekanik yang **belum** direfleksikan di `GAME_LOOP.md`,
dicatat di sini biar gak lupa waktu balik ke pembahasan mekanik:

1. **Monster juga ada di siang hari** — ✅ DIPUTUSKAN: ambient saja. Kelihatan di
   reruntuhan (visual storytelling), tidak agresif, gak nyerang MC di siang. Lore-nya:
   mereka lesu/waspada siang hari, baru ganas pas malam karena indra manusia terbatas.
   Threat beneran di siang = fitur baru → parkiran.
2. **Sistem jam (pagi → 18:00 = malam)** ala Stardew/HM — ✅ DIPUTUSKAN: jam jalan
   OTOMATIS di backend, plus **icon/jam UI** supaya player tau waktu. TavernNPC boleh
   punya opsi "percepat ke malam" sebagai kenyamanan, bukan mekanik utama.
