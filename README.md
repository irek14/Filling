# Filling Editor
FillingEditor to projekt stworzony w ramach drugiego laboratorium z przedmiotu Grafika Komputerowa 1. Jest to aplikacja, która wykorzystując bitmapę oraz normal mapę wektorów wypełnia przy ich pomocy zadany obszar symulując efekt 3D. Aplikacja wyposażona jest w kilka dodatkowych mechanizmów, które modyfikują uzyskany efekt. Ich lista znajduje się poniżej.

## Funkcjonalność
Edytor zapewnia poniższe funkcjnalności:
* Namalowanie obiektu, który może być:
    * Predefiniowaną bitmapą, która wyświetla się zaraz po uruchomieniu
    * Bitmapą załadowaną lokalnie
    * Jednolitą bitmapą, które kolor wybieramy z menu
* Wykorzystanie normal mapy
    * Predefiniowanej, wyświetlanej zaraz po uruchomieniu (fragment muru)
    * Załadowanej lokalnie
    * Mapą złożoną w tylko z wektorów normalny N w postaci [0,0,1]
* Obsługę trzech sposobów wypełeniania
    * Wypełnianie dokładne - dla każdego pixela obliczamy dokładną składową modelu Lamberta i składową zwierciadlaną
    * Wypełnianie wykorzystujące interpolację - dokładne wartości wyliczamy tylko w rogach trójkątów, które należą do wcześniej zdefiniowanej siatki. Pozostałe punkty wewnątrz trójkąta malowane są na kolor uzyskany w wyniku interpolacji z wykorzystaniem współrzędnych barycentrycznych
    * Wypełnianie "hybrydowe" - połączenie obu powyższych metod - dokładne wartości koloru i wektora normalnego wyznaczamy tylko w wierzchołkach trójkąta, wewnątrz niego interpolujemy, jednak wyliczając kolor korzystamy z dokładnych wartości pozostałcyh parametrów
* Edycja współczynników wpływu składowej na wynik a także współczynnika, który określa jak bardzo dany trójkąt jest zwierciadlany
    * Opcja wyboru tej samej wartości dla każdego z trójkątów przy pomocy suwaków
    * Wybór losowy - każdy trójkąt otrzyma inny parametr generowany losowo
* Edycje położenia wersora światła
    * Domyślnie wersor jest stały i odpowiada źródłowi światła umieszczonemu na środku ekranu w plus nieskończoności (wtedy dla każdego punktu L~=[0,0,1])
    * Światło może również wędrować po półsferze wokół ekranu
* Zmiana koloru świecącego światła
* Zmiana rozmiaru siatki trójkątów (a dokładniej liczby rzędów i kolumn trójkątów)

## Przyjęte założenia
* Jeśli rozmiar podanego obrazu bądź normal mapy jest zbyt mały, jest on powielany tak, aby zająć cały ekran.
* Dla wystarczająco dużych rozmiarów zdjęć poza siatką trójkątów widoczny jest podgląd oryginalnego obrazka w celu wizualnego porównania z obrazem uzyskanym po modyfikacjach.
* Siatka trójkątów jest widoczna, dopóki ich liczba na ekranie nie przekracza 1000, aby zachować przejrzystość obrazu
* Współczynniki kd i ks nie sumują się do 1, błędy wynikające z takiego założenia naprawiane są przy pomocy "ucinania" składowych, które wychodzą poza zakres prawidłowy
* Początkowo źródło światła umieszczone jest na wprost, daleko od ekranu, jednak gdy ustawimy go na ruchomy i później po jakimś czasie zatrzymamy, światło pozostanie w tej pozycji
* Źródło światła porusza się po półsferze wokół ekranu zgodnie z równanie krzywych Lisajoux (5,4), dodatkowo oddalając się i przybliżając do ekranu.
* Wszystkie zmiany dokonane w bocznym panelu z ustawieniami muszą być zatwierdzone poprzez naciśnięcie przycisku "Zastosuj"

 ## Autor
 [Ireneusz Stanicki](https://github.com/irek14), student Informatyki na wydziale Matematyki i Nauk Informacyjnych Politechniki Warszawskiej
