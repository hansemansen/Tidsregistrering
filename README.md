Tidsregistreringssystem (C# / ASP.NET MVC 5)

Dette er et simpelt og funktionelt tidsregistreringssystem lavet som eksamensprojekt.

Systemet gør det muligt for medarbejdere at registrere deres arbejdstimer på forskellige sager. Det hele sker via en webside, og der er fokus på at gøre det nemt at bruge og følge reglerne for arbejdstid.

---

Funktioner:

- Vælg afdeling og medarbejder ved opstart
- Registrér arbejdstimer på en sag med dato og tekst
- Se en oversigt over tidligere registreringer
- Systemet holder øje med, at man ikke registrerer mere end 37 timer pr. uge
- Man behøver ikke logge ind

---

Teknologier brugt:

- C# (.NET Framework 4.8)
- ASP.NET MVC 5
- Entity Framework 6.5.1
- Razor Views
- SQL Server (lokal database)
- Bootstrap (for et simpelt og responsivt design)

---

Projektets opbygning:

- Models: datamodeller og DTO-klasser
- BLL: forretningslogik og regler
- DAL: datahåndtering og adgang til databasen
- Controllers: styrer hvad der sker, når brugeren klikker rundt
- Views: siderne man ser og bruger

---

Sådan starter du projektet:

1. Klon projektet:
   git clone https://github.com/hansemansen/Tidsregistrering.git
   cd Tidsregistrering

2. Åbn løsningen i Visual Studio 2022

3. Gå til "Package Manager Console" og skriv:
   Update-Database

4. Start programmet med IIS Express eller en anden lokal webserver

---

Læringsmål:

- Lære at bruge MVC i ASP.NET
- Få styr på at lægge regler i BLL-laget
- Bruge Entity Framework til at arbejde med data
- Lave en brugervenlig webside

Projektet er lavet til skolebrug og ikke til rigtig produktion.
