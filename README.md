# RentalApp — Uczelniana Wypożyczalnia Sprzętu

Aplikacja konsolowa w C# obsługująca uczelnianą wypożyczalnię sprzętu. System umożliwia rejestrowanie sprzętu, wypożyczanie go użytkownikom, zwroty z naliczaniem kar, kontrolę dostępności oraz generowanie raportów.

---

## Instrukcja uruchomienia

### Wymagania
- .NET 8.0 lub nowszy
- Dowolne IDE (Visual Studio, Rider) lub terminal

### Uruchomienie

```bash
# Klonowanie repozytorium
git clone <link-do-repozytorium>

# Przejście do folderu projektu
cd RentalApp

# Uruchomienie aplikacji
dotnet run
```

### Uruchomienie z danymi testowymi
Plik `Program.cs` zawiera gotowy zestaw danych testowych — użytkowników, urządzenia oraz aktywne wypożyczenia. Aplikacja uruchamia się z interaktywnym menu konsolowym.

---

## Struktura projektu

```
RentalApp/
├── Models/
│   ├── Equipment/
│   │   ├── Device.cs           # Abstrakcyjna klasa bazowa dla sprzętu
│   │   ├── Laptop.cs           # Laptop z polami: ScreenSize, OperatingSystem, Ram
│   │   ├── Camera.cs           # Kamera z polami: Mpix, SensorX, SensorY
│   │   ├── Battery.cs          # Bateria z polami: Capacity, InputType, OutputType
│   │   └── ConnectorType.cs    # Enum typów złączy USB
│   ├── Users/
│   │   ├── User.cs             # Abstrakcyjna klasa bazowa dla użytkowników
│   │   ├── Student.cs          # Student z limitem 2 wypożyczeń
│   │   └── Employee.cs         # Pracownik z limitem 5 wypożyczeń
│   ├── Enums/
│   │   └── UserType.cs         # Enum typów użytkowników
│   └── Rentals/
│       └── Rental.cs           # Model pojedynczego wypożyczenia
├── Services/
│   ├── UserService/
│   │   ├── IUserService.cs
│   │   └── UserService.cs
│   ├── DeviceService/
│   │   ├── IDeviceService.cs
│   │   └── DeviceService.cs
│   ├── RentalService/
│   │   ├── IRentalService.cs
│   │   └── RentalService.cs
│   └── Penalty/
│       ├── IPenaltyCalculator.cs
│       └── DailyPenaltyCalculator.cs
├── UI/
│   ├── MenuHandler.cs          # Główna pętla menu
│   ├── DeviceMenu.cs           # Podmenu zarządzania sprzętem
│   ├── UserMenu.cs             # Podmenu zarządzania użytkownikami
│   ├── RentalMenu.cs           # Podmenu wypożyczeń i zwrotów
│   └── ReportMenu.cs           # Podmenu raportów
└── Program.cs                  # Punkt wejścia, inicjalizacja serwisów
```

---

## Opis klas i ich odpowiedzialności

### Warstwa modelu (`Models/`)

**`Device`** — abstrakcyjna klasa bazowa dla całego sprzętu. Przechowuje pola wspólne dla każdego urządzenia: `Id` (auto-generowany), `Name` i `IsAvailable`. Nie zawiera żadnej logiki biznesowej.

**`Laptop`, `Camera`, `Battery`** — klasy pochodne dziedziczące z `Device`. Każda dodaje własne pola specyficzne dla danego typu sprzętu. Dziedziczenie wynika z modelu domenowego — każde urządzenie ma nazwę i status dostępności niezależnie od typu.

**`ConnectorType`** — enum typów złączy USB używany w klasie `Battery`. Ogranicza zbiór wartości do znanych typów i eliminuje ryzyko błędów przy porównywaniu stringów.

**`User`** — abstrakcyjna klasa bazowa dla użytkowników z abstrakcyjną właściwością `MaxRentals`. Przechowuje wspólne dane: `Id`, `Name`, `Surname`, `Email`.

**`Student`** — podklasa `User` z `MaxRentals = 2`.

**`Employee`** — podklasa `User` z `MaxRentals = 5`.

**`UserType`** — enum typów użytkowników używany przy tworzeniu i wyświetlaniu listy użytkowników.

**`Rental`** — model pojedynczego wypożyczenia. Przechowuje referencje do obiektów `User` i `Device` (nie same Id), daty wypożyczenia, termin zwrotu oraz faktyczną datę zwrotu jako `DateTime?`. Właściwość `IsOverdue` jest wyliczana dynamicznie na podstawie dat:

```csharp
public bool IsOverdue => ReturnDate == null && DateTime.Now > DueDate;
```

---

### Warstwa serwisów (`Services/`)

**`UserService`** — zarządza listą użytkowników. Tworzy `Student` lub `Employee` na podstawie przekazanego `UserType`. Udostępnia metody dodawania, pobierania i listowania użytkowników.

**`DeviceService`** — zarządza listą urządzeń. Obsługuje dodawanie sprzętu, pobieranie po Id, filtrowanie dostępnych urządzeń oraz ręczne zarządzanie statusem dostępności (np. oddanie do serwisu).

**`RentalService`** — zawiera główną logikę biznesową wypożyczeń. Waliduje limity użytkowników, sprawdza dostępność sprzętu, tworzy wypożyczenia i obsługuje zwroty z naliczaniem kar. Zależy od `IPenaltyCalculator` i `IDeviceService` przekazanych przez konstruktor.

**`DailyPenaltyCalculator`** — implementuje `IPenaltyCalculator`. Liczy karę za opóźnienie jako iloczyn liczby dni opóźnienia i stawki dziennej. Stawka przekazywana przez konstruktor — łatwa do zmiany bez modyfikacji innych klas.

---

### Warstwa UI (`UI/`)

**`MenuHandler`** — główna pętla aplikacji. Wyświetla menu główne i przekierowuje do podmenu. Otrzymuje wszystkie serwisy przez konstruktor i przekazuje je do odpowiednich podmenu.

**`DeviceMenu`** — obsługuje dodawanie sprzętu (z wyborem typu i walidacją danych) oraz ręczne zarządzanie dostępnością.

**`UserMenu`** — obsługuje dodawanie użytkowników z wyborem typu przez enum `UserType`.

**`RentalMenu`** — obsługuje wypożyczenia i zwroty. Wyświetla listy użytkowników i dostępnych urządzeń przed każdą operacją. Obsługuje wyjątki z serwisu i wyświetla komunikaty o błędach.

**`ReportMenu`** — wyświetla raporty: lista sprzętu, aktywne wypożyczenia użytkownika, przeterminowane wypożyczenia, raport podsumowujący.

---

## Decyzje projektowe

### Separacja warstw

Projekt jest podzielony na trzy wyraźne warstwy — `Models`, `Services`, `UI`. Model domenowy nie zna serwisów, serwisy nie znają UI. Zmiana interfejsu konsolowego na inny nie wymaga modyfikacji logiki biznesowej.

### Dziedziczenie wynikające z domeny

`Device` i `User` są klasami abstrakcyjnymi ponieważ mają wspólne dane i zachowanie — nie dlatego żeby "było bardziej obiektowo". `MaxRentals` jako abstrakcyjna właściwość w `User` pozwala serwisowi pytać `user.MaxRentals` bez znajomości konkretnego typu użytkownika. Zmiana limitu to zmiana jednej linii w jednym pliku.

### Interfejsy dla serwisów

Każdy serwis ma odpowiadający interfejs. Pozwala to na podmianę implementacji bez zmiany kodu który z nich korzysta. Przykład — `RentalService` zależy od `IPenaltyCalculator`, nie od `DailyPenaltyCalculator`. Jutro można dodać `WeeklyPenaltyCalculator` bez zmiany ani jednej linii w `RentalService`.

### Referencje do obiektów w `Rental`

`Rental` przechowuje referencje do `User` i `Device` zamiast samych Id. Dzięki temu dostęp do danych jest bezpośredni — `rental.User.Name` zamiast szukania po Id w każdym miejscu kodu.

### Stawka kary w konstruktorze

Stawka dzienna kary jest przekazywana przez konstruktor `DailyPenaltyCalculator`, nie zakodowana na stałe. To reguła biznesowa która może się zmieniać — trzymanie jej w jednym miejscu ułatwia modyfikację.

### Enum `ConnectorType` i `UserType`

Oba enumy ograniczają zbiór wartości do znanych przypadków. Eliminują ryzyko literówek i ułatwiają wyświetlanie danych w UI bez dodatkowych warunków.

---

## Kohezja i coupling

### Wysoka kohezja

Każda klasa skupia się na jednym obszarze odpowiedzialności:

- `Rental` — przechowuje dane pojedynczego wypożyczenia
- `DailyPenaltyCalculator` — wyłącznie obliczanie kar
- `RentalService` — logika biznesowa wypożyczeń
- `DeviceService` — zarządzanie urządzeniami
- `ReportMenu` — wyłącznie wyświetlanie raportów

Lista wypożyczeń należy do `RentalService`, nie do klasy `Rental` — każda klasa zarządza własnym zbiorem danych.

### Niski coupling

Serwisy komunikują się przez interfejsy, nie przez konkretne implementacje:

```csharp
private readonly IPenaltyCalculator _penaltyCalculator;
private readonly IDeviceService _deviceService;
```

Zależności płyną tylko w jednym kierunku — `UI` zna `Services`, `Services` znają `Models`, `Models` nie znają nikogo.

---

*Projekt zrealizowany w ramach przedmiotu APBD — Polsko-Japońska Akademia Technik Komputerowych*
