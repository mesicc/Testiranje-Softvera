# Smart Library System

**Predmet:** Testiranje softvera
**Autori:**  
| GitHub Profil | Ime i Prezime | Index |
|---------------|---------------|-------|
| MuxBH28 | Muhammed Šehić | 227-ST |
| Lambda-44 | Kerim Gazić | 226-ST |
| mesicc | Kemal Mešić | 236-ST |

**Fakultet:** Elektrotehnički fakultet Sarajevo  

---

## Opis projekta

Ovo je prošireni projekat **Smart Library System** koji je prvobitno implementiran timski u Zadaći 1, a zatim proširen pojedinačno za Zadaću 2. Projekat omogućava upravljanje korisnicima, inventar knjiga, sistem posudbe i dodatne funkcionalnosti kao što su preporuke knjiga, rezervacije i podsjetnici.

---

## Funkcionalnosti

### Moduli iz Zadaće 1
1. **Upravljanje korisnicima**  
   - Registracija korisnika  
   - Upravljanje korisničkim ulogama  

2. **Inventar knjiga**  
   - Dodavanje, ažuriranje i brisanje knjiga  
   - Pretraga knjiga po naslovu, autoru ili žanru  

3. **Sistem posudbe**  
   - Posudba i vraćanje knjiga  
   - Evidencija historije posudbi  

### Novi moduli iz Zadaće 2
1. **Preporuke knjiga**  
   - Automatski preporučuje knjige korisniku na osnovu historije posudbi  
   - Određuje najomiljeniji žanr i predlaže knjige koje korisnik još nije pročitao  

2. **Sistem rezervacija knjiga**   
   - Omogućava korisnicima rezervaciju knjiga unaprijed  

3. **Podsjetnici za rokove**   
   - Slanje obavijesti korisnicima za predstojeće rokove vraćanja knjiga  

---

## Testiranje

- Projekat je pokriven **unit testovima** koji testiraju različite funkcionalnosti modula.  
- Pokrivenost koda: **≥80%**  
- Minimalno **10 testova**, uključujući **data-driven unit test**.  
- Testovi pokrivaju validaciju unosa, logiku posudbe i preporuka, kao i interakcije sa zamjenskim objektima.

---

## Instalacija

1. Klonirajte repozitorij:  
```bash
git clone https://github.com/korisnik/SmartLibrary.git
cd SmartLibrary
