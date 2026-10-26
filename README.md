## Instuktioner och beskrivning av garaget
Kör programmet med dotnet run. Sen kan du navigera genom att trycka in kommandon. Sen när du är inne i programmet kan du skapa en garage. Efter det har du tillgång till alla funktionerna i menyn och kan då skapa nytt fordon, ta bort fordon, lista alla fordon, och söka på fordon. Du kan också skapa en ny garage som kommer då att ta bort tidigare garaget och allt som finns sparat där. 

## Arkitektur
- Program hanterar kommandflödet.
- Menu innehåller alla funktioner som anropas av Program klassen, tex. skapa garage/vehicle, ta bort vehicle, osv.
- Garage sparar en lista av Vehicles och inehåller mmetoder för att lägga till fordon / ta bort fordon / söka osv som anropas av metoderna i Menu.
- Vehicle sparar en registreringsnummer, färg, och nummer av hjul. Sen har vehicle flera deriverade klasser (Car, Motorcycle, Bus, Boat, Airplane) som har egna egenskaper. Jag bestämde att vehicle skulle inte vara abstract så användaren skulle kunna skapa generella fordon i fall de är inte inkluderad som subklass. 

## Saker som jag vill ha feedback på
Jag vill ha feedback på arkitekturen. I tidigare versionen av programmet så hade jag alla funktioner direkt inne i switchsatsen i Program.cs. Nu har jag flyttat ut dem till Menu.cs som statiska metoder. Har jag gjort rätt här? Skulle ni dela upp dem på något annat sätt? 

En annan grej som bekymrar mig lite är AddVehicle metoden i Menu.cs. Just nu är den ganska lång då den innefattar alla subtyper som vehicle har. Jag skulle kunna dela upp det ytterligarre så det blir AddCar, AddMotorcycle osv. men det känns inter helt rätt. Hur tänker ni. kring det?