# Projektnapló – Konzolos rajzoló

## Csapat és munkamegosztás

| Név vagy GitHub-felhasználónév | Vállalt feladatok és szerep | Saját menüfunkció |
| --- | --- | --- |
| Kóti-Jószai Simon | Nyomdák elkészítése, Szöveges mintafájl elkészítése | Cellák törlése |
| Németh János | Menürendszer, Kurzorvezérlés, Állapotjlező panel | Ugrás a tábla közepére |

## Közös megállapodások

- A rajzlap mérete és helye: 60 oszlop 20 sor, a konzol jobb oldalán.
- A két munkarész kapcsolódási pontja: …
- A módosítások egyeztetése a main ágon: …

## Munkanapló

| Dátum | Ki | Mi készült el vagy miben állapodtunk meg? | Következő lépés vagy commit |
| --- | --- | --- | --- |
| 2026.11.11 | Mindketten | Elolvastuk és értelmeztük a feladatot. A projektet leklónoztuk az iskolai gépekre. | … |
| 2026.11.12 | Németh János | Repo létrehozása | … |
| 2026.11.18 | Néémeth János | Képernyőelrendezés, rajzlap állapota, paletták, Menü / választólista állapota | … |
| 2026.11.21 | Mindketten | Billentyűzet, Mozgás, Rajzolás elkészítése: János ; Nyomda elkészítése: Simon | … |
| 2026.11.22 | Mindketten | Menübillentyű, menü végrehajtás, Lista, Saját feladatok: János; Minta betöltése, Vászon törlése: Simon Maga a szabadkezi rajzolás: Mindketten ; tesztjegyzőkönyv kitöltése: Simon (János diktálta az ő "tapasztalatait") | … |
| 2026.11.24 | Kóti-Jószai Simon | Projektnapló kitöltése | … |

## Másik pár tesztje

- Tesztelt commit: …
- Visszajelzés és saját javítás: …
- Újrateszt eredménye: …

## Otthoni AI-használat

Aki használt AI-t, írja le a nevét, miben kért segítséget, és mit tanult meg belőle.

## Név: Kóti-Jószai Simon
- Nem tudtam hogyan kell megoldani azt hogy fix legyen a rajzlap mérete. Most már tudom hogy konstanst kell létrehozni.
- Hibát dobott arra amikor a megadott alakzatokat próbáltam meg kirajzoltatni. Most már tudom hogy a Math.Abs funkciót kell használni ami az abszolútértékét adja meg a számnak.
- Nem tudtam rájönni hogyan kell beolvasni a fájlokat. Utánanéztem és kiderült hogy a File.ReadAllLines használata szükséges ami arra jó hogy soronként beolvassa a fájlt.
- A throw new exception-nel saját hibafelismerő rendszert készítettem.
## Név: Németh János
- Nem tudtam hogyan kell megoldani azt hogy ne lehessen véletlenül beírni a menüpontokhoz. Most már tudom hogy readonly-t kell használni.
- Eleinte hibaüzenetet kaptam amikor túl kicsi volt az ablak de megtanulam hogy a try-catch használatával kell megoldani a problémát.
- Amikor nem stringet írattam ki akkor a program nem működött. A megoldás: Az Array.ConvertAll egy olyan beépített metódus, amely egy meglévő tömb minden egyes elemét átkonvertálja egy másik típussá, és az eredményt egy új tömbbe menti.
## Közös AI használat
- Tervezés
- Struktúra kialakítása

