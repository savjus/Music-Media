# Muzikos atlikejų registravimo ir paieškos sistema

Šis projektas yra internetinė platforma, skirta muzikos atlikejų registravimui, informacijos saugojimui ir tarpusavio ryšių (panašumo) atvaizdavimui. Veikimo principas panašus i music-map.com. Sistema leidžia vartotojams kurti paskyras, prisijungti, registruotis, naršyti atlikejus ir vykdyti paieška.

## Pagrindinės funkcijos

- Vartotojų autentifikacija (registracija, prisijungimas, atsijungimas, rolės)
- Atlikejų valdymas (kurti, redaguoti, šalinti; administratorius ir vartotojas)
- Paieška ir filtravimas (pagal pavadinima, žanra, šali, populiaruma)
- Administravimas (vartotojų valdymas, netinkamo turinio šalinimas, statistika)

## Technologijos

- Backend: C#, ASP.NET Core Web API
- Duomenų bazė: SQL Server + Entity Framework (planuojama)
- Autentifikacija: JWT
- Frontend: Blazor (esamas projektas), alternatyvos buvo React/Angular/Flutter

## Kaip atsisiųsti

1. Atsisiųskite arba klonuokite repozitorija:
	- `git clone <repo-url>`
2. Atidarykite sprendini Visual Studio arba VS Code.

## Konfigūravimas

Pagrindiniai nustatymai yra faile `Backend/appsettings.json`:

- `ConnectionStrings`: SQL Server prisijungimas.
- `Jwt`: raktas, issuer, audience, token galiojimo laikas.

Jei naudojate atskiras aplinkas, koreguokite `Backend/appsettings.Development.json`.

Frontend nustatymai yra faile `Frontend/appsettings.json` (pvz., API bazinis adresas).

## Paleidimas

### Backend

1. Pereikite i `Backend` projekta.
2. Paleiskite:
	- `dotnet run`

### Frontend

1. Pereikite i `Frontend` projekta.
2. Paleiskite:
	- `dotnet run`

## Naudojimas

- Užsiregistruokite arba prisijunkite.
- Naudokite paieška atlikėjų paieškai.
- Per profilio puslapius redaguokite savo informacija.
- Administratorius gali valdyti atlikejus ir vartotojus.

## Testavimas

Testavimo dokumentacija: https://docs.google.com/document/d/1XKPTNouZP_7VXh3ohMYDDGe7vasOOOnXCp0FTokN7IU/edit?usp=sharing

Jei norite pridėti .doc/.docx faila i repozitorija:

1. Atsisiųskite dokumenta is Google Docs kaip .docx.
2. Sukurkite kataloga `docs/testing/` (jei dar nera).
3. Idėkite faila, pvz. `docs/testing/Testavimas.docx`.
4. Itraukite ji i versiju kontrole (git add/commit).
