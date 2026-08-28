# KÓSMOS Y — Game Loop Design Document

> Dokumen ini ada karena project ini awalnya cuma "tes kemampuan", tapi sekarang
> butuh arah biar selesai. Aturan mainnya: **fitur yang gak nutupin loop = gak dikerjain.**
> Terakhir diupdate: fase polish main menu selesai, siap masuk gameplay loop.

---

## 1. Premise (sementara, boleh berubah)

Kamu adalah penjaga perkemahan (tavern/campfire) di tepian dunia.
Setiap malam monster datang — slime dan kelelawar dari reruntuhan sekitar.
Siang harinya kamu berdagang dengan pengelola tavern: hasil buruanmu ditukar emas,
emas ditukar perlengkapan supaya malam berikutnya kamu bertahan lebih lama.

*(Cerita lengkap sengaja ditunda sampai loop-nya fun. Premis satu paragraf ini cukup buat ngarahkan desain.)*

---

## 2. Core Loop (diagram besar)

```
   [PAGI–SIANG–SORE / HUB]           [MALAM]                [SUBUH]
   Santai di campfire ────► Matahari terbenam ──► Musuh muncul tersebar,
   jual drop ke tavern      otomatis (jam game)    bertahan sampai subuh
        ▲                        │                       │
        │                        ▼                       ▼
        │                  Beli potion/sword      Musuh hilang saat subuh,
        │                  untuk malam ini        player pulang ke campfire
        │                                                │
        └──────────── Jual hasil malam ◄────────── Drop item (jelly/wing)
```

**Kalimat loop-nya:** *Satu hari satu siklus — siang siap-siap & dagang, malam bertahan, subuh panen hasil.*

---

## 3. Tiga Lapis Loop

### Loop Mikro (detik–menit) — "aksi seru"
Gerak → dekati musuh → slash → musuh mati → drop keluar → jalan ambil → item masuk inventory + SFX kecil.
✅ Sebagian besar **sudah jalan**: PlayerMovement, PlayerAttack (Slash), EnemyAI, EnemyHealth, ItemPickUp, Inventory.

### Loop Menengah (beberapa menit) — "progress terasa"
Drop numpuk → ke TavernNPC → jual (dapat Gold) → beli upgrade sword / stok potion → stat player naik.
🔨 **Belum ada — INI PRIORITAS #1.** Bahan-bahannya sudah ada semua (lihat tabel §5).

### Loop Makro (per hari / satu sesi main) — "tujuan"
Hari berjalan → tiap malam makin berat → player mati di suatu malam (atau selamat sampai hari target) → layar Game Over/Victory → retry dari menu.
🔨 **Belum ada — PRIORITAS #2.** Tanpa ini game gak punya "akhir sesi" dan terasa demo.

---

## 4. Aturan Desain Angka Awal (v0.1 — tempel dulu, tuning belakangan)

| Hal | Nilai awal | Catatan |
|---|---|---|
| Slime | HP 3, dmg 1, speed lambat | Drop SlimeJelly 100% |
| Bat | HP 2, dmg 1, speed cepat | Drop BatWing 60% |
| Jual SlimeJelly | 3 gold | |
| Jual BatWing | 5 gold | Lebih mahal = reward risiko (bat lebih nyebelin) |
| Potion | beli 15 gold, heal 3 HP | Usable dari inventory |
| Upgrade Sword lv+1 | 30 gold, dmg +1 | Cuma 2 level (lv1→lv3), jangan banyak-banyak dulu |
| Durasi 1 hari | 4 menit real-time (siang 2.5 + malam 1.5) | Jam otomatis di backend + icon jam UI ala HM/Stardew |
| Monster siang | Ada tapi ambient: lesu, gak nyerang | Lore: manusia lihat jelas siang hari; malam indra terbatas → musuh agresif |
| Malam hari N | musuh spawn tersebar, jumlah & agresivitas naik dikit per hari | Scaling halus, BUKAN gelombang |
| Subuh | semua musuh menghilang, kembali aman | Transisi langit + SFX burung (nanti) |
| Target menang | Selamat sampai hari ke-5 | Victory screen → sesi selesai ~20 menit |

Prinsip: **angka boleh jelek asal ADA.** Tuning itu kerjaan setelah loop jalan, bukan sebelum.

---

## 5. Status Sistem — yang udah ada vs yang harus dibangun

| Sistem | File yang sudah ada | Status | Yang kurang |
|---|---|---|---|
| Movement + Attack | PlayerMovement.cs, PlayerAttack.cs | ✅ Jalan | — |
| Musuh spawn & AI | EnemySpawner.cs, EnemyAI.cs | ✅ Jalan | Spawn tersebar + gating malam hari |
| HP player | PlayerHealth.cs, PlayerHealthBar.cs | ✅ Jalan | Death → Game Over screen |
| Item data & pickup | ItemData.cs, ItemPickUp.cs, ScriptableObjects | ✅ Jalan | Tambah `sellPrice`, `buyPrice` |
| Inventory + UI | Inventory.cs, InventoryUI.cs, Slot.cs | ✅ Jalan | Tombol "Use"/"Sell" |
| NPC Tavern | TavernNPC.cs | 🔨 Cek isi | Interaksi → panel toko (jual/beli) |
| Ekonomi (Gold) | — | ❌ Belum | GameManager.Gold + text UI (VT323 sudah ada) |
| Day/Night cycle | — | ❌ Belum | TimeManager: jam game, ganti lighting/langit, trigger spawn malam |
| Game Over / Victory | — | ❌ Belum | UI + retry ke Gameplay / balik MainMenu |
| Potion usable | Potion.asset ada | ❌ Belum | Use handler di inventory |

---

## 6. Roadmap (urutan pengerjaan — SATU milestone per fokus burst)

### M0 — Bersih-bersih kecil (½ sesi)
- [ ] Hover color tombol menu + slider styling (sisaan polish menu)
- [ ] Fix bug yang udah tau (null-check AudioMixer, quit editor order)

### M1 — Ekonomi dasar ⭐ mulai di sini
- [ ] `GameManager` dengan `Gold` (int) + display di UI
- [ ] `ItemData` dapat field `sellPrice`
- [ ] TavernNPC: tekan E di dekatnya → panel toko → jual item inventory → Gold naik
- **Definition of done:** bunuh slime → jual jelly-nya → angka Gold naik di layar.

### M2 — Toko & konsumsi
- [ ] Beli Potion (dan Sword upgrade) dari toko
- [ ] Potion usable dari inventory (heal)
- **Definition of done:** siklus penuh: bunuh → jual → beli potion → pakai potion saat HP turun.

### M3 — Siklus Hari & Kekalahan
- [ ] `TimeManager`: jam game jalan otomatis (1 hari = ~4 menit) + icon jam UI (VT323 / sprite matahari-bulan)
- [ ] Malam: lighting gelap + EnemySpawner mulai spawn tersebar; Subuh: musuh despawn, aman lagi
- [ ] Death → Game Over screen → Retry / Main Menu
- **Definition of done:** main dari pagi, malam datang sendiri, bisa MATI dengan benar dan langsung coba lagi tanpa restart Unity 😄

### M4 — Menang & wrap MVP loop
- [ ] Selamat sampai hari ke-5 → Victory screen
- [ ] Balance pass pertama (pakai tabel §4 sebagai titik awal)
- **Definition of done:** orang lain bisa main 20 menit dan ngerti tujuannya tanpa dijelasin.

### M5 (opsional, SETELAH M4 dan kalau masih semangat)
- Pilihan: day/night visual, musuh variasi baru 1 jenis, atau sound effect combat.
- ❌ Bukan: crafting, farming, quest, save system, map kedua. Itu project berikutnya.

---

## 7. Apa yang dipinjam dari Stardew Valley & HM FoMT (dan apa yang TIDAK)

**Dipinjam (esensinya):**
- Rhythm "siang santai–malam action"-nya FoMT: kontras tempo bikin keduanya terasa spesial.
- Ekonomi sederhana Stardew: hasil panen/buruan → jual → upgrade alat. Kita versi mini-nya.
- Campfire/tavern sebagai *home base* yang hangat (aset campfire animated kamu sudah pas banget).

**TIDAK dipinjam (untuk project ini):**
- Farming, social link, calendar/festival, stamina hari — itu beda genre dan bakal bunuh scope.
- Kalau suatu hari mau farming beneran → itu ide bagus untuk **project #5**, bukan tambahan di sini.

---

## 8. Cerita — DRAFT v0.1 (dibangun pelan-pelan)

> Status: kerangka. Boleh berubah selama M1–M3. Gak ada yang wajib diimplement
> sebelum M4 — ini kompas biar tone-nya konsisten.

**Premis:** Kamu adalah penjaga perkemahan di tepian dunia — bukan pahlawan,
bukan yang terpilih. Monster datang tiap malam; kamu mengusirnya karena itu
kerjaanmu, dan hasil buruanmu ditukar emas untuk bertahan hidup. Sederhana,
pragmatis, hangat. *(nuansa: Goblin Slayer × HM FoMT)*

**Motivasi player (kandidat, pilih SATU pas M4):**
1. "Kumpulin uang pulang" — tabung emas untuk bayar utang/biaya perjalanan pulang ke kampung halaman.
2. "Lindungi campfire" — api itu ingatan seseorang; kalau padam, sesuatu hilang.
3. "Musim dingin akan datang" — kumpulkan cukup sebelum musim berubah.

**Tone & gaya penceritaan:**
- Dunia kecil yang terasa hidup: NPC tavern punya jadwal & obrolan santai *(Stardew/FoMT)*.
- Kebebasan tanpa paksaan: gak ada quest log yang menyuruh; pemain pilih sendiri ritmenya *(GTA: rasa bebas, skala mini)*.
- Ending reflektif, pelan, personal — bukan epik dunia diselamatkan *(Frieren)*.
- Dialog pendek down-to-earth; harga & barter sebagai "cerita ekonomi", bukan teks panjang.

**Ending pendek victory screen (sketsa 2–3 kalimat):**
> Subuh kelima. Api masih menyala. Penjaga itu duduk sebentar,
> menyeduh sesuatu yang hangat, lalu tersenyum kecil.
> Dunia gak tau namanya. Tapi kampung ini tau.

---

## 9. Aturan anti-stuck (baca tiap kali mulai kerja)

1. Kerjakan **cuma milestone aktif**. Gak lihat M+1 sebelum M sekarang done.
2. Setiap sesi tutup dengan: commit + tulis 1 baris "selanjutnya: ..." di commit message.
3. Kalau bingung mau ngapain → buka §5, ambil baris pertama yang ❌/🔨.
4. Kalau ide baru muncul mid-work → tulis di bawah sini (§10), jangan dikerjain.

## 10. Backflow Ide (parkiran)

- *(tulis ide liar di sini, evaluasi pas M4 kelar)*

---

## 11. Peta Inspirasi (dari Yog — game & anime)

| Kategori | Inspirasi | Yang diambil ke Kósmos Y |
|---|---|---|
| Story | GTA (SA/V/VC), HM FoMT, Stardew Valley | Rasa bebas & dunia kecil yang hangat, ritme santai |
| Mechanic & Gameplay | Attack on Titan, NieR Automata, Frieren, Demon Slayer, Goblin Slayer, RDR2, HSR (Phainon Arc) | Hunter pragmatis ala Goblin Slayer (loop jual-beli), combat *feel* tajam (juice: hit-stop, shake, partikel) |
| Art Style | Anime Style, Stardew Valley, Pixel | Pixel-art + ekspresi anime lewat sprite & animasi, portrait minimal |
| Audio & Sound | NieR Automata, Harvest Moon, Clair Obscur: E33 | Melankolis-hangat; Bosca Ceoil melodi minor lembut untuk malam |

**Aturan praktis scope:** ambil *tone*-nya, jangan *sistem*-nya. Kalau ide butuh
quest log, cutscene, atau open world → simpan untuk project impian, bukan project ini.
