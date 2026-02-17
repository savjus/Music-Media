# Muzikos atlikėjų registravimo ir paieškos sistema
Projektas – tai internetinė platforma, skirta muzikos atlikėjų registravimui, informacijos saugojimui ir tarpusavio ryšių (panašumo) atvaizdavimui, veikianti pagal principą, panašų į music-map.com.
Sistema leidžia vartotojams kurti paskyras, prisijungti, registruotis, naršyti atlikėjus, atlikti paieška pagal atlikėjų informacija
# Pagrindinės funkcijos
1. Vartotojų autentifikacija
•	Registracija naudojant el. paštą ir slaptažodį
•	Prisijungimas / atsijungimas
•	Vartotojo rolės (pvz., administratorius, registruotas vartotojas, neregistruotas vartotojas)
2. Atlikėjų valdymas
•	Naujo muzikos atlikėjo pridėjimas (administratorius)
•	Vartotojo informacijos pridėjimas (savo informacija)
•	Atlikėjo informacijos redagavimas ir šalinimas (administratorius)
•	Vartotojo informacijos redagavimas ir šalinimas
•	Duomenys: pavadinimas, žanras, kalba, aprašymas, nuorodos į platformas (Spotify, YouTube ir kt.)
3. Paieška ir filtravimas
•	Greita atlikėjų paieška pagal pavadinimą
•	Filtravimas pagal žanrą, šalį ar populiarumą
4. Administravimo dalis
•	Vartotojų valdymas
•	Netinkamo turinio šalinimas
•	Statistikos peržiūra (registruotų atlikėjų, vartotojų skaičius ir pan.)

# Technologinis įgyvendinimas
•	Backend: c#, ASP.NET Core Web API (Universitete mokinomės)
•	Duomenų bazė: SQL Server + Entity Framework (gal)
•	Autentifikacija: JWT
•	Frontend: React / Angular / Blazor / Flutter (nuspresime išbande technologijas)
