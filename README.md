## Decyzje projektowe

### Model domenowy

#### Klasa bazowa `Device`
Wszystkie typy sprzętu (`Laptop`, `Camera`, `Battery`) dziedziczą z abstrakcyjnej klasy `Device`. Klasa bazowa przechowuje pola wspólne dla każdego urządzenia: `Id`, `Name`, `IsAvailable`. Każda podklasa dodaje własne pola specyficzne — np. `Laptop` ma `Ram`, `OperatingSystem`, `Model`, a `Camera` ma `Mpix`, `SensorX`, `SensorY`.

Dziedziczenie wynika z modelu domenowego, nie z chęci "żeby było obiektowo" — każde urządzenie ma nazwę i status dostępności, niezależnie od typu.

#### Klasa bazowa `User`
Analogicznie `Student` i `Employee` dziedziczą z abstrakcyjnej klasy `User`. Kluczowa decyzja: właściwość `MaxRentals` jest **abstrakcyjna** w klasie bazowej i nadpisywana w podklasach:

```csharp
// Student
public override int MaxRentals => 2;

// Employee  
public override int MaxRentals => 5;
```

Dzięki temu serwis pyta `user.MaxRentals` i nie musi wiedzieć z jakim typem użytkownika ma do czynienia. Zmiana limitu to zmiana jednej linii w jednym pliku.

#### Enum `UserType`
Użyty do identyfikacji typu użytkownika przy tworzeniu i wyświetlaniu listy. Ogranicza zbiór wartości do znanych typów i eliminuje ryzyko literówek przy porównywaniu stringów.

#### Enum `ConnectorType`
Użyty w klasie `Battery` dla pól `InputType` i `OutputType`. Typy złączy USB to zamknięty, znany zbiór — enum lepiej to wyraża niż dowolny string.

#### Klasa `Rental`
Przechowuje referencje do obiektów `User` i `Device` (nie same Id) — dzięki temu dostęp do danych użytkownika i urządzenia jest bezpośredni, bez szukania po Id.

`ReturnDate` jest typu `DateTime?` (nullowalny) — brak wartości oznacza że sprzęt nie został jeszcze zwrócony.

`IsOverdue` jest wyliczaną właściwością bez settera:
```csharp
public bool IsOverdue => ReturnDate == null && DateTime.Now > DueDate;
```
Wartość jest zawsze aktualna — nie trzeba jej ręcznie aktualizować.

---

### Serwisy

Logika biznesowa jest oddzielona od modelu domenowego i od interfejsu konsolowego. Każdy serwis ma jedną wyraźną odpowiedzialność:

- `UserService` — zarządzanie użytkownikami
- `DeviceService` — zarządzanie sprzętem
- `RentalService` — logika wypożyczeń i zwrotów
- `DailyPenaltyCalculator` — liczenie kar za opóźnienia

Każdy serwis ma odpowiadający mu interfejs (`IUserService`, `IDeviceService` itd.), co pozwala na łatwą podmianę implementacji bez zmiany reszty kodu.

---
