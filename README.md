## Kanban Viewer ## 
En Blazor-Server webapplikation för att strukturera upp arbete och uppgifter.
Appen tillåter:
+ Skapande av konto
+ Skapande av kanban tavlor av alla användare
+ Delning av kanban tavlor mellan användare

Utvecklad med .NET Blazor-server side med Supabase som backend molnmiljö. 

Översikt över appens branchstruktur.
Main - Produktionsmiljö.
Dev - Utvecklingsmiljö
Feat/ - Olika brancher för de olika features.
Test/ - Brancher för att skapa tester.

Vid varje pullrequest så triggas ett workflow som skickar en notis till vår discord-server för att andra utvecklar lätt ska kunna ta del av nya features.
Workflow kör också de tester vi har i vårt projekt för att smidigt kunna åtgärna ifall projektets tester misslyckas.

## Teststruktur 
Integrationstester används för att säkerställa att data skickas till databas och att applikationens olika komponenter fungerar tillsammans som förväntat. Dessa tester hjälper oss att:
+ Verifiera att data sparas pch hämtas korrekt.
+ Säkerställa att API-anrop och databasinteraktioner fungerar som de ska.
+ Upptäcka enertuella fel i kommunikationen mellan olika lager i applikationen.
+ Säkerhetsställa att kopplingen mot databasen existerar.

