# Muzikos atlikejų registravimo ir paieškos sistema

Projektas yra internetinė platforma muzikos atlikejų paieškai ir profilių peržiurai. Veikimo principas panašus i music-map.com. Sistema leidžia vartotojams registruotis, prisijungti ir naršyti atlikejus bei dainas.

## Pagrindinės funkcijos

- Registracija ir prisijungimas su el. paštu ir slaptažodžiu, „Remember me“ parinktimi.
- Paskyros nustatymai: slaptažodžio keitimas ir paskyros ištrynimas.
- Atlikejų paieška pagal pavadinima, su filtrais (žanras, kalba, aktyvumo metai) ir rikiavimu pagal populiaruma.
- Atlikejo profilio peržiura: aprašymas, žanrai, šalis, aktyvumo metai, nuorodos i Spotify/YouTube, albumai, dainos, turai, komentarai.
- Panašių atlikejų paieška (pasirenkami 3 atlikejai, grąžinamas panašiausio žanro atlikėjas).
- Dainų paieška pagal pavadinima su filtrais (žanras, BPM), puslapiavimu ir „I’m feeling lucky“.
- Profilio redagavimas: vardas, bio, kalba, žanrai, Spotify/YouTube nuorodos; albumų, dainų ir turu pridėjimas.
- Komentarai profiliuose su like/dislike balsavimu; galima istrinti savo komentarus.
- Mėgstamu atlikeju sekimas.

## Technologijos

- Backend: C#, ASP.NET Core Web API
- Frontend: Blazor (Razor Components, Interactive Server)

## Kaip atsisiųsti

1. Klonuokite repozitorija:
   - `git clone <repo-url>`
2. Atidarykite sprendini Visual Studio arba VS Code.

## Konfigūravimas

Frontend API adresas nustatomas faile `Frontend/appsettings.json`:

- `BackendUrl`: backend API bazinis URL.

## Paleidimas

Atidarykite dvi terminalo sesijas.

### Backend

```bash
cd Backend
dotnet run
```

### Frontend

```bash
cd Frontend
dotnet run
```

## Naudojimas

- Prisijunkite arba susikurkite paskyra.
- Atlikejų paieška: naudokite paieškos laukeli ir filtrus.
- Atlikejo profilis: peržiurėkite albumus, dainas, turus, komentarus; prisijungus galite pridėti i mėgstamus.
- Profilio skiltyje redaguokite savo informacija ir pridėkite albumus/dainas/turus.
- „Similar artist“ puslapyje pasirinkite 3 atlikejus ir gaukite panašu rezultata.

## Testavimas

Testavimo dokumentacija: https://docs.google.com/document/d/1XKPTNouZP_7VXh3ohMYDDGe7vasOOOnXCp0FTokN7IU/edit?usp=sharing


## Kūrėjai
IFIN-4/3 Grupės "Tiesiai į penketą" nariai 
Salvijus Poška
Adomas Rizk
Dovydas Birbalas

