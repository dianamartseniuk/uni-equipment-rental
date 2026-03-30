# System wypożyczania sprzętu

## Opis projektu

Aplikacja konsolowa w C#, która obsługuje proces wypożyczania sprzętu (laptopy, projektory, kamery) dla użytkowników (studenci, pracownicy). System implementuje podstawowe operacje (wypożyczenie, zwrot, raport) oraz reguły biznesowe (limity, kary).

---

## Podział projektu i uzasadnienie

Projekt został podzielony na trzy główne obszary:

- **Models** – klasy domenowe (`User`, `Equipment`, `Rental`)
- **Services** – logika operacyjna (`UserService`, `EquipmentService`, `RentalService`, `ReportService`)
- **Policies** – reguły biznesowe (`DefaultUserLimitPolicy`, `DefaultPenaltyPolicy`)

Taki podział pozwala oddzielić:
- reprezentację danych (Models),
- operacje na systemie (Services),
- zmienne reguły biznesowe (Policies).

Dzięki temu zmiana np. sposobu liczenia kary nie wymaga modyfikacji `RentalService`.

---

## Kohezja

Starałam się, aby każda klasa miała jedną, jasno określoną odpowiedzialność:

- `Rental` odpowiada tylko za logikę pojedynczego wypożyczenia (`IsActive`, `IsOverdue`, `ReturnEquipment`)
- `Equipment` zarządza swoim stanem (`MarkAsBorrowed`, `MarkAsAvailable`)
- `RentalService` koordynuje proces wypożyczenia i zwrotu
- `ReportService` zajmuje się wyłącznie agregacją danych i generowaniem raportu

Dzięki temu logika nie jest rozproszona i każda klasa jest łatwa do zrozumienia.

---

## Coupling

Ograniczyłam zależności między klasami poprzez:

- wstrzykiwanie zależności przez konstruktor (np. `RentalService` korzysta z `UserService` i `EquipmentService`)
- wydzielenie reguł biznesowych do osobnych klas (`Policies`)
- unikanie bezpośredniego odwoływania się do szczegółów implementacji innych klas

Na przykład `RentalService` nie wie, ile wynosi limit wypożyczeń ani jak liczona jest kara, deleguje to do odpowiednich klas. Dzięki temu zmiana jednej części systemu nie powoduje zmian w wielu innych miejscach.

---

## SOLID w praktyce

- **Single Responsibility Principle**  
  Każdy serwis ma jedną odpowiedzialność (np. `UserService` tylko zarządza użytkownikami)

- **Open/Closed Principle**  
  Reguły biznesowe są wydzielone do klas w `Policies`, więc można je zmienić bez modyfikowania `RentalService`

- **Dependency Inversion Principle (w uproszczonej formie)**  
  `RentalService` korzysta z abstrakcji (interfejsów polityk), a nie konkretnych implementacji

---

## Dlaczego taki podział jest sensowny

Wybrany podział pozwala:

- uniknąć jednej „dużej klasy” zawierającej całą logikę
- łatwo rozszerzać system (np. dodać nowy typ użytkownika lub inną politykę kary)
- utrzymać czytelność kodu przy rosnącej liczbie funkcjonalności

Najważniejsze było dla mnie oddzielenie:
- procesu (Service),
- danych (Model),
- decyzji biznesowych (Policy).