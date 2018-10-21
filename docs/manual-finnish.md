# KomaHub - kotimainen sähkölaatikko tähtikuvaukseen

KomaHub on Komakalliolla kehitetty apulaite tähtikuvaukseen. KomaHubilla korvaat perinteisen rinnankytketyn 12 voltin sähkönjakolaatikon ja saat hienoja lisäominaisuuksia.

Laite on erityisen hyödyllinen etäkäyttöisessä observatoriossa, jossa on tarve käynnistää ja sammuttaa 12 voltilla toimivia laitteita etänä ja tarkkailla niiden virrankulutusta. Lisäksi KomaHubiin on mahdollista kytkeä useita erilaisia antureita, joiden avulla voi tarkkailla kuvausolosuhteita ja tallentaa tämän tiedon suoraan raakakuvien metadataksi.

Tyypillisiä käyttökohteita ovat esimerkiksi:
- Temppuilevan laitteen korjaaminen katkaisemalla virrat
- CCD-kameran sammuttaminen päiväsajaksi, jos kameran tuuletin muuten pyörisi aina kun virta on kytkettynä
- Laitteiden pitäminen virroissa tiivistyvän kosteuden välttämiseksi
- Huurrepantojen automaattinen säätö kuvauksen aikana
- Taivaan pimeyden ja muiden suureiden mittaaminen kuvatessa

Ominaisuudet:
- Kuusi yksittäin tietokoneella ohjattavaa virtaulostuloa
  - Maksimi 8 A per portti, yhteensä 15 A
  - Jatkuva virtamittaus per portti
  - Ohjelmallisesti toteutettu nopea sulake käyttäjän valitsemalla virtarajoituksella
- Yksi jatkuvasti päällä oleva virtaulostulo (kytketty rinnan sisääntulon kanssa)
- Jokaiseen kuudesta ulostulosta voi määrittää PWM:n esim. huurrepannan ohjaamista varten
- Sisääntulojännitteen mittaus
- Laadukkaat Powerpole-liittimet
- Mahdollisuus kytkeä ulkoisia antureita (lämpötila, paine, kosteus, pilvisyys, taustataivaan kirkkaus)
- Atmel ATmega32U4 mikrokontrolleri
- Toimii USB HID-laitteena - ei sarjaporttiajureiden asentelua!
- Ohjausohjelma Windowsille
- ASCOM-ajurit ObservingConditions ja SafetyMonitor -rajapinnoille
- Kaikki lähdekoodi (firmware, protokollat, ajurit ja ohjelmisto) ja piirilevysuunnittelu on avoimesti jaossa

KomaHub kytketään tietokoneeseen Mini-USB-liittimellä. Lisäksi laitteelle syötetään 12 voltin käyttöjännite alla olevaan kuvaan merkityn Powerpole-liittimen kautta (**12V input**). Syöttöjännitteelle tarkoitetun liittimen alapuolella on toinen Powerpole-liitin, joka on kytketty rinnakkain syöttöjännitteen kanssa. Tästä liittimestä voi jakaa 12 V sähkön laitteille, joiden haluat aina olevan päällä, esim. USB-hubi, johon KomaHub on kytketty.

![Liittimet](images/connectors.jpg)

Kuvaan merkityt **12V output** -liittimet ovat KomaHubilla ohjattavia laitteita varten. Liittimet on numeroitu kuvassa näkyvässä järjestyksessä.

Lisäksi laitteen kyljessä ovat RJ45- ja 3,5 mm stereoliittimet erilaisia oheisantureita varten.

## Windows-ohjelman käyttö

KomaHubia ohjataan Windows-työpöytäohjelmalla, jonka käyttöliittymästä näet kuvakaappauksen alla. **Power Outputs** -otsikon alta näet listauksen kaikista kuudesta virtalähdöstä sekä niiden tilasta. Jokaiselle lähdölle voi antaa nimen, joka vastaa lähtöön kytkettyä laitetta. Käyttöliittymä näyttää myös jokaisen lähdön virrankulutuksen, sekä antaa mahdollisuuden kytkeä kukin lähtö päälle tai pois klikkaamalla **On/Off**-nappia.

Jos lähdön tyypiksi on asetuksissa valittu PWM, näkyy lähdölle myös käyttöjakson säätö (0-100%). Tällä voi esimerkiksi lämmityspantojen tapauksessa säätää lämmitystehoa.

![KomaHub-kuvakaappaus](images/screenshot.jpg)

**Sensors**-otsikon alta näet kaikkien KomaHubiin kytkettyjen anturien tilan. Listan lopusta löytyy myös mitattu syöttöjännite. Syötetyn jännitteen tulisi olla välillä 12-14 V.

**Settings**-nappia painamalla voit säätää KomaHubin käyttämiä asetuksia. Jokaiselle lähdölle voi valita nimen, tyypin sekä sulakkeen käyttämän virtarajan.

Lähtöjen tyypit ovat seuraavanlaiset:
- **12V** - Lähtö on normaalisti käytössä ja antaa 12 voltin jännitettä
- **PWM** - Lähtö antaa säädettävää PWM-jännitettä, jolla voi ohjata esimerkiksi lämmityspantoja
- **OFF** - Lähdön käyttö on kokonaan estetty ja näkyy käyttöliittymässä *Disabled*-tilassa

