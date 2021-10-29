using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AlgoTecture.Models.GeoAdminModels
{

    public static class GwrCodeLookup
    {
        public static string GetCode(string code)
        {
            string result;

            string key = null;
            if (code.IndexOf("Item")>=0)
            {
                key = code;
            } else
            {
                key = "Item" + code;
            }

            if ( codes.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<string, string> codes = new Dictionary<string, string>
        {
           {"Item0","Nein"},
           {"Item1","Ja"},
           {"Item3","Abgeschlossen"},
           {"Item4","In Bearbeitung"},
           {"Item5","Nur lesen"},
           {"Item6","Lesen und schreiben"},
           {"Item10","Datei importieren"},
           {"Item11","Datei importiert"},
           {"Item12","Datei zurückgewiesen"},
           {"Item13","Datensätze zurückg. "},
           {"Item14","Datei Pre-Import"},
           {"Item21","Bauprojekte"},
           {"Item22","Gebäude / Wohnungen"},
           {"Item23","Adressen"},
           {"Item24","Strassenverzeichnis"},
           {"Item31","=(gleich)"},
           {"Item32",">=(gleich oder grösser)"},
           {"Item33","<=(gleich oder kleiner)"},
           {"Item34","<>(ungleich)"},
           {"Item35","><(zwischen)"},
           {"Item40","Links aussen"},
           {"Item41","Links nord"},
           {"Item42","Links ost"},
           {"Item43","Links"},
           {"Item44","Links süd"},
           {"Item45","Links west"},
           {"Item46","Mitte links"},
           {"Item47","Mitte nord"},
           {"Item48","Mitte ost"},
           {"Item49","Mitte"},
           {"Item50","Mitte süd"},
           {"Item51","Mitte west"},
           {"Item52","Mitte rechts"},
           {"Item53","Rechts nord"},
           {"Item54","Rechts ost"},
           {"Item55","Rechts"},
           {"Item56","Rechts süd"},
           {"Item57","Rechts west"},
           {"Item58","Rechts aussen"},
           {"Item59","Nord"},
           {"Item60","Nord-ost"},
           {"Item61","Ost"},
           {"Item62","Süd-ost"},
           {"Item63","Süd"},
           {"Item64","Süd-west"},
           {"Item65","West"},
           {"Item66","Nord-west"},
           {"Item70","Priorität 1"},
           {"Item71","Priorität 2"},
           {"Item72","Priorität 3"},
           {"Item73","Priorität 4"},
           {"Item80","Doppel Erfassung BAU"},
           {"Item81","Doppel Erf. Bereinigung"},
           {"Item82","Leiche"},
           {"Item83","Gebäude Zweiteilung"},
           {"Item84","Gebäude Fusion"},
           {"Item85","Externe Register"},
           {"Item90","Vierteljährlich"},
           {"Item91","Jährlich"},
           {"Item100","Deaktiviert"},
           {"Item101","Fehlerfrei"},
           {"Item111","Ohne Fehler BAU (MISS)"},
           {"Item113","Ohne Fehler BAU (G+W)"},
           {"Item114","Mit Fehler BAU"},
           {"Item115","Mit Fehler BAU/ADR"},
           {"Item116","Mit Fehler KLX"},
           {"Item120","In Bearbeitung"},
           {"Item122","In Bearbeitung (ADR)"},
           {"Item123","In Bearbeitung (G+W)"},
           {"Item124","In Bearbeitung (BAU)"},
           {"Item130","Gegründet"},
           {"Item131","Archiviert"},
           {"Item132","Gelöscht"},
           {"Item150","Nicht geprüft"},
           {"Item155","Nicht geprüft (VAL)"},
           {"Item201","GK frei (innerhalb AV)"},
           {"Item202","GK frei (AV Merkmal)"},
           {"Item203","GK frei (AV mehrfach)"},
           {"Item204","GK frei (AV ausser. Gde)"},
           {"Item205","Reserve 205"},
           {"Item206","GK frei (AV Geb-Umr)"},
           {"Item207","GK frei (AV ParzNr)"},
           {"Item208","GK frei (AV ha)"},
           {"Item209","Reserve 209"},
           {"Item211","GK frei (ausserhalb AV)"},
           {"Item212","GK frei (a. AV Merkmal)"},
           {"Item213","GK frei (a. AV mehrfach)"},
           {"Item214","GK frei (a. AV aus. Gde)"},
           {"Item215","GK frei (ausser. AV HNr)"},
           {"Item216","GK frei (a. AV Geb-Umr)"},
           {"Item217","GK frei (auss. AV Parz)"},
           {"Item218","GK frei (ausserh. AV ha)"},
           {"Item219","Reserve 219"},
           {"Item242","GK pendent (AV Merkmal)"},
           {"Item243","GK pendent (AV mehrfach)"},
           {"Item244","GK pendent (AV Gde)"},
           {"Item245","Reserve 245"},
           {"Item246","GK pendent (AV Geb-Umr)"},
           {"Item247","GK pendent (AV ParzNr)"},
           {"Item248","GK pendent (AV ha)"},
           {"Item249","GK neu ermittelt (AV)"},
           {"Item252","Pendent (a. AV Merkmal)"},
           {"Item253","Pendent (a. AV mehrfach)"},
           {"Item254","GK pendent (a. AV Gde)"},
           {"Item255","GK pendent (a. AV HNr)"},
           {"Item256","Pendent (a. AV Geb-Um)"},
           {"Item257","Pendent (a. AV ParzNr)"},
           {"Item258","GK pendent (a. AV, ha)"},
           {"Item259","GK neu ermitt. (aus. AV)"},
           {"Item288","GK neu ermittelt"},
           {"Item298","Falsche GK"},
           {"Item299","GK fehlen"},
           {"Item300","Gem. BFS Initialbestand"},
           {"Item301","Gemäss Amtsstelle"},
           {"Item302","Identisch VZ90"},
           {"Item303","Ident. Amtsstelle VZ90"},
           {"Item304","Identisch mit der Post"},
           {"Item305","Ident. Post und Amtsst. "},
           {"Item306","Ident. Post und VZ90"},
           {"Item307","Ident. Post, Amst. , VZ90"},
           {"Item309","Ohne Wohnungen"},
           {"Item410","Datenaustausch-Plattform"},
           {"Item411","Kundentyp A"},
           {"Item412","Kundentyp B"},
           {"Item413","Kundentyp C kommunal"},
           {"Item414","Kundentyp D kommunal"},
           {"Item415","Kundentyp E kommunal"},
           {"Item416","Kundentyp C kantonal"},
           {"Item417","Kundentyp D kantonal"},
           {"Item418","Kundentyp E kantonal"},
           {"Item419","Kundentyp F kommunal"},
           {"Item420","Kundentyp F kantonal"},
           {"Item421","EST BAU+GWR kommunal"},
           {"Item422","EST nur BAU kommunal"},
           {"Item423","EST nur GWR kommunal"},
           {"Item424","EST BAU+GWR kom. (WS)"},
           {"Item425","Kundentyp E+ Kommunal"},
           {"Item426","EST kantonal"},
           {"Item428","EST Bund"},
           {"Item429","EST privat"},
           {"Item430","BFS-Administrator"},
           {"Item431","BFS-Administrator+"},
           {"Item432","BFS-Kunde"},
           {"Item433","BFS-GWR"},
           {"Item434","BFS-BAU"},
           {"Item436","Kt. Aufarbeitung BAU+GWR"},
           {"Item437","Kt. Aufarbeitung GWR"},
           {"Item438","ARE-Administrator"},
           {"Item441","GK-GIS-Applikation"},
           {"Item442","BAU-Applikation"},
           {"Item445","Benutzerverwalter"},
           {"Item450","Initiallogon"},
           {"Item451","Freier Logon"},
           {"Item459","Blockierter Logon"},
           {"Item460","Online-Zugriff EHS"},
           {"Item461","Online-Zugriff Kunde"},
           {"Item462","Zugriff über Web Services EHS"},
           {"Item463","Zugriff über Web Services Kunde"},
           {"Item464","Résiliation d'accès"},
           {"Item465","Gebäudeverwaltung"},
           {"Item470","ARE, BWO, BFE, BAFU, GBA, st 411/41 "},
           {"Item471","Vermessungsamt 413/416"},
           {"Item472","Grundbuchamt 413/416"},
           {"Item473","Schätzungsamt 414/417"},
           {"Item474","Umweltschutzamt 414/417"},
           {"Item475","Landwirtschaftsamt 414/417"},
           {"Item476","Kant. Gebäudeversicherung 414/417"},
           {"Item477","Einwohnerkontrolle 415/418"},
           {"Item478","Statistikamt 419/420"},
           {"Item479","Bauamt 419/420"},
           {"Item480","Planungsamt 419/420"},
           {"Item490","Zu bearbeiten"},
           {"Item491","Angenommen"},
           {"Item492","Abgelehnt"},
           {"Item501","Zu bearbeiten"},
           {"Item502","Offen"},
           {"Item503","Abgelehnt"},
           {"Item504","Importiert"},
           {"Item505","Gelöscht"},
           {"Item510","Gebäude"},
           {"Item511","Gebäudeeingang"},
           {"Item512","Wohnung"},
           {"Item520","Hinzufügen"},
           {"Item521","Ändern"},
           {"Item522","Löschen"},
           {"Item610","Erhebung  initialisiert"},
           {"Item620","EHS aufgefordert"},
           {"Item625","Daten importiert und getestet"},
           {"Item630","Abschluss EHS mit Fehler"},
           {"Item640","Abschluss des anerk. GWR mit Fehler"},
           {"Item660","Abschluss EHS"},
           {"Item665","Abschluss kontrollieren"},
           {"Item680","Abschluss BFS"},
           {"Item690","Daten kopiert Snapshots"},
           {"Item691","Erzwungener Abschl. BFS"},
           {"Item699","EST deaktiviert"},
           {"Item720","Ersterfassung GDE/BEZ"},
           {"Item721","Neugründung GDE/BEZ"},
           {"Item722","Namensänderung BEZ"},
           {"Item723","Namensänderung GDE"},
           {"Item724","Neuer BEZ/KT"},
           {"Item726","Gebietsänderung GDE"},
           {"Item727","Neunummerierung GDE/BEZ"},
           {"Item729","Aufhebung GDE/BEZ"},
           {"Item730","Mutation annulliert"},
           {"Item801","Papier"},
           {"Item802","Internet"},
           {"Item803","Fremdapplikation"},
           {"Item805","Sonderlösung"},
           {"Item806","Web Services"},
           {"Item807","Anerkanntes GWR"},
           {"Item850","Gemeinde"},
           {"Item851","Gemäss amtlicher Vermessung"},
           {"Item852","Gemäss amtlicher Schätzung"},
           {"Item853","Gemäss Gebäudeversicherung"},
           {"Item854","Grundbuch"},
           {"Item855","Gemäss Feuerungskontrolle"},
           {"Item856","Die Post"},
           {"Item857","Gemäss Kontaktperson"},
           {"Item858","Gemäss GEAK"},
           {"Item859","Andere"},
           {"Item860","Gemäss Volkszählung 2000"},
           {"Item861","Anerkannte Register"},
           {"Item862","Einwohnerkontrolle"},
           {"Item863","Andere Personenregister"},
           {"Item864","Gemäss Daten des Kantons"},
           {"Item865","Gemäss Daten der Gemeinde"},
           {"Item866","Touristische Organisationen"},
           {"Item867","Geometer"},
           {"Item868","BFS"},
           {"Item869","Gemäss Baubewilligung"},
           {"Item870","Gemäss Versorgungswerk"},
           {"Item871","Gemäss Minergie"},
           {"Item878","Nicht bestimmbares Volumen"},
           {"Item879","Keine Datenquelle"},
           {"Item901","AV, DM. 01"},
           {"Item902","AV, hergeleitet"},
           {"Item903","Geometer"},
           {"Item904","Baugesuch"},
           {"Item905","BFS"},
           {"Item906","GeoPost"},
           {"Item909","Andere Datenquelle"},
           {"Item950","Nicht aktiv"},
           {"Item951","VZ-aktiv"},
           {"Item952","BZ-aktiv"},
           {"Item954","VZ+BZ-aktiv"},
           {"Item961","Gemäss SIA-Norm 116"},
           {"Item962","Gemäss SIA-Norm 416"},
           {"Item969","Unbekannt"},
           {"Item1001","Projektiert"},
           {"Item1002","Bewilligt"},
           {"Item1003","Im Bau"},
           {"Item1004","Bestehend"},
           {"Item1005","Nicht nutzbar"},
           {"Item1007","Abgebrochen"},
           {"Item1008","Nicht realisiert"},

           {"Item1010","Prov. Unterkunft"},
           {"Item1020","Für Wohnnutzung"},
           {"Item1030","Andere Wohngebäude"},
           {"Item1040","Mit  teilw. Wohnnutzung"},
           {"Item1060","Ohne Wohnnutzung"},
           {"Item1080","Sonderbau"},

           {"Item1110","Gebäude mit 1 Wohnung"},
           {"Item1121","Gebäude mit 2 Wohnungen"},
           {"Item1122","Gebäude mit 3+ Whgen"},
           {"Item1130","Wohngeb. f. Gemeinschaften"},
           {"Item1211","Hotelgebäude"},
           {"Item1212","Andere Beherbergungen"},
           {"Item1220","Bürogebäude"},
           {"Item1230","Gross- und Einzelhandel"},
           {"Item1231","Restaurants und Bars"},
           {"Item1241","Bahnhöfe usw. "},
           {"Item1242","Garagengebäude"},
           {"Item1251","Industriegebäude"},
           {"Item1252","Behälter, Silo, Lager"},
           {"Item1261","Kultur-/Freizeitstätte"},
           {"Item1262","Museen / Bibliotheken"},
           {"Item1263","Schul-/Hochschulgebäude"},
           {"Item1264","Krankenhaus"},
           {"Item1265","Sporthalle"},
           {"Item1271","Landw. Betriebsgebäude"},
           {"Item1272","Kirche / Kultgebäude"},
           {"Item1273","Denkmal"},
           {"Item1274","Sonstiger Hochbau"},
           {"Item1275","Andere kollektive Unterkünfte"},
           {"Item1276","Tierhaltung"},
           {"Item1277","Pflanzenbau"},
           {"Item1278","Andere landw. Geb. "},


           {"Item2001","Projektiert"},
           {"Item2003","Im Bau"},
           {"Item2004","Bestehend"},
           {"Item2007","Aufgehoben"},
           {"Item3001","Projektiert"},
           {"Item3002","Bewilligt"},
           {"Item3003","Im Bau"},
           {"Item3004","Bestehend"},
           {"Item3005","Nicht nutzbar"},
           {"Item3007","Aufgehoben"},
           {"Item3008","Nicht realisiert"},
           {"Item3010","Erstwohnung"},
           {"Item3020","Zweitwohnung"},
           {"Item3030","Anders als zum Wohnen genutzt"},
           {"Item3031","Erwerbs-/Ausbildungszwecken"},
           {"Item3032","PH mit mehreren Whg im Geb"},
           {"Item3033","Personen nicht im EWR"},
           {"Item3034","Leerwohnung (<2 Jahre)"},
           {"Item3035","Alpwirtschaftlich"},
           {"Item3036","Personalunterkunft"},
           {"Item3037","Dienstwohnungen"},
           {"Item3038","Kollektivhaushalt"},
           {"Item3070","Wohnung unbewohnbar"},
           {"Item3090","Automatische Aktualisierung"},
           {"Item3091","Einwohnerkontrolle"},
           {"Item3092","Eigentümer/in oder Verwaltung"},
           {"Item3093","Andere Datenquelle"},
           {"Item3100","Parterre"},
           {"Item3101","1. Stock"},
           {"Item3102","2. Stock"},
           {"Item3103","3. Stock"},
           {"Item3104","4. Stock"},
           {"Item3105","5. Stock"},
           {"Item3106","6. Stock"},
           {"Item3107","7. Stock"},
           {"Item3108","8. Stock"},
           {"Item3109","9. Stock"},
           {"Item3110","10. Stock"},
           {"Item3111","11. Stock"},
           {"Item3112","12. Stock"},
           {"Item3113","13. Stock"},
           {"Item3114","14. Stock"},
           {"Item3115","15. Stock"},
           {"Item3116","16. Stock"},
           {"Item3117","17. Stock"},
           {"Item3118","18. Stock"},
           {"Item3119","19. Stock"},
           {"Item3120","20. Stock"},
           {"Item3121","21. Stock"},
           {"Item3122","22. Stock"},
           {"Item3123","23. Stock"},
           {"Item3124","24. Stock"},
           {"Item3125","25. Stock"},
           {"Item3126","26. Stock"},
           {"Item3127","27. Stock"},
           {"Item3128","28. Stock"},
           {"Item3129","29. Stock"},
           {"Item3130","30. Stock"},
           {"Item3131","31. Stock"},
           {"Item3132","32. Stock"},
           {"Item3133","33. Stock"},
           {"Item3134","34. Stock"},
           {"Item3135","35. Stock"},
           {"Item3136","36. Stock"},
           {"Item3137","37. Stock"},
           {"Item3138","38. Stock"},
           {"Item3139","39. Stock"},
           {"Item3140","40. Stock"},
           {"Item3141","41. Stock"},
           {"Item3142","42. Stock"},
           {"Item3143","43. Stock"},
           {"Item3144","44. Stock"},
           {"Item3145","45. Stock"},
           {"Item3146","46. Stock"},
           {"Item3147","47. Stock"},
           {"Item3148","48. Stock"},
           {"Item3149","49. Stock"},
           {"Item3150","50. Stock"},
           {"Item3151","51. Stock"},
           {"Item3152","52. Stock"},
           {"Item3153","53. Stock"},
           {"Item3154","54. Stock"},
           {"Item3155","55. Stock"},
           {"Item3156","56. Stock"},
           {"Item3157","57. Stock"},
           {"Item3158","58. Stock"},
           {"Item3159","59. Stock"},
           {"Item3160","60. Stock"},
           {"Item3161","61. Stock"},
           {"Item3162","62. Stock"},
           {"Item3163","63. Stock"},
           {"Item3164","64. Stock"},
           {"Item3165","65. Stock"},
           {"Item3166","66. Stock"},
           {"Item3167","67. Stock"},
           {"Item3168","68. Stock"},
           {"Item3169","69. Stock"},
           {"Item3170","70. Stock"},
           {"Item3171","71. Stock"},
           {"Item3172","72. Stock"},
           {"Item3173","73. Stock"},
           {"Item3174","74. Stock"},
           {"Item3175","75. Stock"},
           {"Item3176","76. Stock"},
           {"Item3177","77. Stock"},
           {"Item3178","78. Stock"},
           {"Item3179","79. Stock"},
           {"Item3180","80. Stock"},
           {"Item3181","81. Stock"},
           {"Item3182","82. Stock"},
           {"Item3183","83. Stock"},
           {"Item3184","84. Stock"},
           {"Item3185","85. Stock"},
           {"Item3186","86. Stock"},
           {"Item3187","87. Stock"},
           {"Item3188","88. Stock"},
           {"Item3189","89. Stock"},
           {"Item3190","90. Stock"},
           {"Item3191","91. Stock"},
           {"Item3192","92. Stock"},
           {"Item3193","93. Stock"},
           {"Item3194","94. Stock"},
           {"Item3195","95. Stock"},
           {"Item3196","96. Stock"},
           {"Item3197","97. Stock"},
           {"Item3198","98. Stock"},
           {"Item3199","99. Stock (Maximum)"},
           {"Item3401","1. Untergeschoss"},
           {"Item3402","2. Untergeschoss"},
           {"Item3403","3. Untergeschoss"},
           {"Item3404","4. Untergeschoss"},
           {"Item3405","5. Untergeschoss"},
           {"Item3406","6. Untergeschoss"},
           {"Item3407","7. Untergeschoss"},
           {"Item3408","8. Untergeschoss"},
           {"Item3409","9. Untergeschoss"},
           {"Item3410","10. Untergeschoss"},
           {"Item3411","11. Untergeschoss"},
           {"Item3412","12. Untergeschoss"},
           {"Item3413","13. Untergeschoss"},
           {"Item3414","14. Untergeschoss"},
           {"Item3415","15. Untergeschoss"},
           {"Item3416","16. Untergeschoss"},
           {"Item3417","17. Untergeschoss"},
           {"Item3418","18. Untergeschoss"},
           {"Item3419","19. Untergeschoss (Maximum)"},
           {"Item4011","Parzelle"},
           {"Item4012","Anteil Stockwerkeigentum"},
           {"Item4013","Gewöhnliches Miteigentum"},
           {"Item4014","Konzession"},
           {"Item4015","SDR"},
           {"Item4016","Bergwerk"},
           {"Item5000","Bauzone"},
           {"Item5001","RPG 16a I / RPV 34 I"},
           {"Item5002","RPG 16a I / RPV 34 II"},
           {"Item5003","RPG 16a I / RPV 34 III"},
           {"Item5004","RPG 16a / RPV 35"},
           {"Item5005","RPG 16a II / RPV 36"},
           {"Item5006","RPG 16a II / RPV 37"},
           {"Item5007","RPG 16a III / RPV 38"},
           {"Item5008","RPG 16a I / RPV 34a"},
           {"Item5009","RPG 17 allg. "},
           {"Item5011","RPG 18 allg. "},
           {"Item5012","RPG 18 / RPV 33"},
           {"Item5015","RPG 18a"},
           {"Item5021","RPG 24"},
           {"Item5022","RPG 24 / RPV 39 I"},
           {"Item5023","RPG 24 / RPV 39 II"},
           {"Item5031","RPG 24a"},
           {"Item5041","RPG 24b I"},
           {"Item5043","RPG 24b Ibis"},
           {"Item5044","RPG 24b Iter"},
           {"Item5051","RPG 24c / RPV 42"},
           {"Item5061","RPG 24d I / RPV 42a"},
           {"Item5062","RPG 24d II"},
           {"Item5063","RPG 24e I"},
           {"Item5064","RPG 24e II-IV"},
           {"Item5071","RPG 37a / RPV 43"},
           {"Item6001","Neubau"},
           {"Item6002","Umbau"},
           {"Item6007","Abbruch"},
           {"Item6010","Tiefbau"},
           {"Item6011","Hochbau"},
           {"Item6101","SBB"},
           {"Item6103","VBS"},
           {"Item6104","BBL"},
           {"Item6105","ASTRA"},
           {"Item6107","Swisscom"},
           {"Item6108","Die Post"},
           {"Item6110","Kanton Verwaltung"},
           {"Item6111","Kanton Unternehmung"},
           {"Item6115","Gemeinde Verwaltung"},
           {"Item6116","Gemeinde Unternehmung"},
           {"Item6121","Versicherung"},
           {"Item6122","Personalfürsorgestiftung"},
           {"Item6123","Krankenkasse, SUVA"},
           {"Item6124","Bank"},
           {"Item6131","Priv.  Elektrizitätswerk"},
           {"Item6132","Privates Gaswerk"},
           {"Item6133","Privatbahn"},
           {"Item6141","Ein-/Pers. -Immob. -Gesell"},
           {"Item6142","Wohnbaugenossenschaft"},
           {"Item6143","Kap. -Gesellsch. (Immob. )"},
           {"Item6151","Einzelfirma oder Person"},
           {"Item6152","Kapitalgesellschaften"},
           {"Item6161","Privatperson"},
           {"Item6162","And. priv. Auftraggeber"},
           {"Item6163","Intern. Organisation"},
           {"Item6211","Wasserversorgungsanlagen"},
           {"Item6212","Elektrizitätswerk/-netz"},
           {"Item6213","Gaswerke und -netze"},
           {"Item6214","Fernheizungsanlagen"},
           {"Item6219","Übrige Versorgungsanlage"},
           {"Item6221","Wasserentsorgungsanlagen"},
           {"Item6222","Kehrichtentsorgungsanl. "},
           {"Item6223","Übrige Entsorgungsanl. "},
           {"Item6231","Nationalstrassen"},
           {"Item6232","Kantonsstrassen"},
           {"Item6233","Gemeindestrassen"},
           {"Item6234","Übriger Strassenbau, Parkplätze"},
           {"Item6235","Parkhäuser"},
           {"Item6241","Bahnanlagen"},
           {"Item6242","Bus- und Tramanlagen"},
           {"Item6243","Schiffsverkehrsanlagen"},
           {"Item6244","Flugverkehrsanlagen"},
           {"Item6245","Kommunikationsanlagen"},
           {"Item6249","Übrige Verkehrsanlagen"},
           {"Item6251","Schulen, Bildungswesen"},
           {"Item6252","Höheres Bildungswesen"},
           {"Item6253","Akut. - allg. Spitäler"},
           {"Item6254","Heime mit Pflege"},
           {"Item6255","Übriges Gesundheitswesen"},
           {"Item6256","Freizeit-, Tourismusanlagen"},
           {"Item6257","Kirchen, Sakralbauten"},
           {"Item6258","Kulturbauten"},
           {"Item6259","Sporthallen/Sportplätze"},
           {"Item6261","Gewässerverbauung"},
           {"Item6262","Landesverteidigung"},
           {"Item6269","Übrige Infrastruktur"},
           {"Item6271","EFH Einfamilienhäuser"},
           {"Item6272","EFH angebaut"},
           {"Item6273","Mehrfamilienhäuser reine"},
           {"Item6274","Andere Wohngebäude"},
           {"Item6276","Wohnheime ohne Pflege"},
           {"Item6278","Garagen, Parkplätze"},
           {"Item6279","Übrige Bauten i. Z. Wohnen"},
           {"Item6281","Landwirtschaftsbauten"},
           {"Item6282","Forstwirtschaftsbauten"},
           {"Item6283","Meliorationen"},
           {"Item6291","Werkstätten, Fabriken"},
           {"Item6292","Lagerhallen, Depots usw. "},
           {"Item6293","Büro-, Verwaltungsgeb. "},
           {"Item6294","Kaufhäuser/Geschäftsgeb. "},
           {"Item6295","Hotels, Restaurants"},
           {"Item6296","Andere Beherbergungenen"},
           {"Item6299","Übrige i. Z. Wirtschaft"},
           {"Item6604","Abgeschlossen"},
           {"Item6605","In Bearbeitung"},
           {"Item6606","Zurückgestellt"},
           {"Item6609","Annulliert"},
           {"Item6701","Beantragt"},
           {"Item6702","Bewilligt"},
           {"Item6703","Baubegonnen"},
           {"Item6704","Abgeschlossen"},
           {"Item6706","Sistiert"},
           {"Item6707","Abgelehnt"},
           {"Item6708","Nicht realisiert"},
           {"Item6709","Zurückgezogen"},
           {"Item7100","Keine Heizung"},
           {"Item7101","Einzelofenheizung"},
           {"Item7102","Etagenheizung"},
           {"Item7103","Zentralheizung für Geb. "},
           {"Item7104","ZH für mehrere Gebäude"},
           {"Item7105","Fernwärmeversorgung"},
           {"Item7109","Andere Heizungsart"},
           {"Item7200","Keine Energieträger"},
           {"Item7201","Heizöl"},
           {"Item7202","Kohle"},
           {"Item7203","Gas"},
           {"Item7204","Elektrizität"},
           {"Item7205","Holz"},
           {"Item7206","Wärmepumpe"},
           {"Item7207","Sonnenkollektor"},
           {"Item7208","Fernwärme"},
           {"Item7209","Andere Energie"},
           {"Item7400","Kein Wärmeerzeuger"},
           {"Item7410","Wärmepumpe für ein Geb. "},
           {"Item7411","Wärmepumpe für mehr. Geb. "},
           {"Item7420","Thermische Solaranlage für ein Geb. "},
           {"Item7421","Therm. Solaranlage für mehr. Geb. "},
           {"Item7430","Heizkessel (gene. ) für ein Geb. "},
           {"Item7431","Heizkessel (gen. ) für mehr. Geb. "},
           {"Item7432","Heizkessel nicht kond. für ein Geb. "},
           {"Item7433","Heizkessel nicht kond. mehr. Geb. "},
           {"Item7434","Heizkessel kond. für ein Geb. "},
           {"Item7435","Heizkessel konde. für mehr. Geb. "},
           {"Item7436","Ofen"},
           {"Item7440","Wärmekraftkopplungsanlage ein Geb. "},
           {"Item7441","Wärmekraftkopplungsanlage mehr. Geb. "},
           {"Item7450","Elektro-Zentralheizung ein Geb. "},
           {"Item7451","Elektro-Zentralheizung mehr. Geb. "},
           {"Item7452","Elektro direkt"},
           {"Item7460","Wärmetauscher für ein Geb. "},
           {"Item7461","Wärmetauscher für mehr. Geb. "},
           {"Item7499","Andere"},
           {"Item7500","Keine"},
           {"Item7501","Luft"},
           {"Item7510","Erdwärme (generisch)"},
           {"Item7511","Erdwärmesonde"},
           {"Item7512","Erdregister"},
           {"Item7513","Wasser"},
           {"Item7520","Gas"},
           {"Item7530","Heizöl"},
           {"Item7540","Holz (generisch)"},
           {"Item7541","Holz (Stückholz)"},
           {"Item7542","Holz (Pellets)"},
           {"Item7543","Holz (Schnitzel)"},
           {"Item7550","Abwärme"},
           {"Item7560","Elektrizität"},
           {"Item7570","Sonne (thermisch)"},
           {"Item7580","Fernwärme (generisch)"},
           {"Item7581","Fernwärme (Hochtemperatur)"},
           {"Item7582","Fernwärme (Niedertemperatur)"},
           {"Item7598","Unbestimmt"},
           {"Item7599","Andere"},
           {"Item7600","Kein Wärmeerzeuger"},
           {"Item7610","Wärmepumpe"},
           {"Item7620","Thermische Solaranlage"},
           {"Item7630","Heizkessel (generisch)"},
           {"Item7632","Heizkessel nicht kondensierend"},
           {"Item7634","Heizkessel kondensierend"},
           {"Item7640","Wärmekraftkopplungsanlage"},
           {"Item7650","Zentraler Elektroboiler"},
           {"Item7651","Kleinboiler"},
           {"Item7660","Wärmetauscher"},
           {"Item7699","Andere"},
           {"Item8011","Vor 1919"},
           {"Item8012","1919-1945"},
           {"Item8013","1946-1960"},
           {"Item8014","1961-1970"},
           {"Item8015","1971-1980"},
           {"Item8016","1981-1985"},
           {"Item8017","1986-1990"},
           {"Item8018","1991-1995"},
           {"Item8019","1996-2000"},
           {"Item8020","2001-2005"},
           {"Item8021","2006-2010"},
           {"Item8022","2011-2015"},
           {"Item8023",">2015"},
           {"Item9801","Platz (Punktobjekt)"},
           {"Item9802","Benanntes Gebiet (Flächenobjekt)"},
           {"Item9803","Keine Angabe zur Geometrie"},
           {"Item9809","Keine Angabe zur Geom. "},
           {"Item9811","Projektiert"},
           {"Item9812","Baubegonnen"},
           {"Item9813","Bestehend"},
           {"Item9814","Aufgehoben"},
           {"Item9830","Keine Nummern"},
           {"Item9832","Beliebig (zufällig)"},
           {"Item9835","Aufsteigend o. A. li/re"},
           {"Item9836","Aufsteig. , ung. nr. li. "},
           {"Item9837","Aufsteig. , ung. nr. re. "},
           {"Item9839","Keine Angabe"},
           {"Item9901","Deutsch"},
           {"Item9902","Rätoromanisch"},
           {"Item9903","Französisch"},
           {"Item9904","Italienisch"},
           {"Item9905","Deutsch / Rätoromanisch"},
           {"Item9907","Spezialgebiete"},
           {"Item9908","Seen"},
           {"Item9909","Enklaven"},

        };
    }

    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //     Runtime Version:4.0.30319.42000
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------

    


    // 
    // This source code was auto-generated by xsd, Version=4.0.30319.33440.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    [System.Xml.Serialization.XmlRootAttribute("maddRequest", Namespace = "http://www.ech.ch/xmlns/eCH-0206/2", IsNullable = false)]
    public partial class maddRequestType
    {

        private requestHeaderType requestHeaderField;

        private maddRequestTypeRequestContext requestContextField;

        private maddRequestTypeRequestQuery requestQueryField;

        private maddRequestTypeOptions optionsField;

        private object extensionField;

        /// <remarks/>
        public requestHeaderType requestHeader
        {
            get
            {
                return this.requestHeaderField;
            }
            set
            {
                this.requestHeaderField = value;
            }
        }

        /// <remarks/>
        public maddRequestTypeRequestContext requestContext
        {
            get
            {
                return this.requestContextField;
            }
            set
            {
                this.requestContextField = value;
            }
        }

        /// <remarks/>
        public maddRequestTypeRequestQuery requestQuery
        {
            get
            {
                return this.requestQueryField;
            }
            set
            {
                this.requestQueryField = value;
            }
        }

        /// <remarks/>
        public maddRequestTypeOptions options
        {
            get
            {
                return this.optionsField;
            }
            set
            {
                this.optionsField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class requestHeaderType
    {

        private string messageIdField;

        private string businessReferenceIdField;

        private sendingApplicationType requestingApplicationField;

        private string commentField;

        private System.DateTime requestDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string messageId
        {
            get
            {
                return this.messageIdField;
            }
            set
            {
                this.messageIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string businessReferenceId
        {
            get
            {
                return this.businessReferenceIdField;
            }
            set
            {
                this.businessReferenceIdField = value;
            }
        }

        /// <remarks/>
        public sendingApplicationType requestingApplication
        {
            get
            {
                return this.requestingApplicationField;
            }
            set
            {
                this.requestingApplicationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }

        /// <remarks/>
        public System.DateTime requestDate
        {
            get
            {
                return this.requestDateField;
            }
            set
            {
                this.requestDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    public partial class sendingApplicationType
    {

        private string manufacturerField;

        private string productField;

        private string productVersionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                this.manufacturerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string product
        {
            get
            {
                return this.productField;
            }
            set
            {
                this.productField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string productVersion
        {
            get
            {
                return this.productVersionField;
            }
            set
            {
                this.productVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0008/3")]
    public partial class countryShortType
    {

        private string countryNameShortField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string countryNameShort
        {
            get
            {
                return this.countryNameShortField;
            }
            set
            {
                this.countryNameShortField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class personOnlyType
    {

        private personOnlyTypeIdentification identificationField;

        /// <remarks/>
        public personOnlyTypeIdentification identification
        {
            get
            {
                return this.identificationField;
            }
            set
            {
                this.identificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class personOnlyTypeIdentification
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("organisationIdentification", typeof(organisationIdentificationType))]
        [System.Xml.Serialization.XmlElementAttribute("personIdentification", typeof(personIdentificationLightType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0097/2")]
    public partial class organisationIdentificationType
    {

        private uidStructureType uidField;

        private namedOrganisationIdType localOrganisationIdField;

        private namedOrganisationIdType[] otherOrganisationIdField;

        private string organisationNameField;

        private string organisationLegalNameField;

        private string organisationAdditionalNameField;

        private string legalFormField;

        /// <remarks/>
        public uidStructureType uid
        {
            get
            {
                return this.uidField;
            }
            set
            {
                this.uidField = value;
            }
        }

        /// <remarks/>
        public namedOrganisationIdType localOrganisationId
        {
            get
            {
                return this.localOrganisationIdField;
            }
            set
            {
                this.localOrganisationIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OtherOrganisationId")]
        public namedOrganisationIdType[] OtherOrganisationId
        {
            get
            {
                return this.otherOrganisationIdField;
            }
            set
            {
                this.otherOrganisationIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationName
        {
            get
            {
                return this.organisationNameField;
            }
            set
            {
                this.organisationNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationLegalName
        {
            get
            {
                return this.organisationLegalNameField;
            }
            set
            {
                this.organisationLegalNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationAdditionalName
        {
            get
            {
                return this.organisationAdditionalNameField;
            }
            set
            {
                this.organisationAdditionalNameField = value;
            }
        }

        /// <remarks/>
        public string legalForm
        {
            get
            {
                return this.legalFormField;
            }
            set
            {
                this.legalFormField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0097/2")]
    public partial class uidStructureType
    {

        private uidOrganisationIdCategorieType uidOrganisationIdCategorieField;

        private string uidOrganisationIdField;

        /// <remarks/>
        public uidOrganisationIdCategorieType uidOrganisationIdCategorie
        {
            get
            {
                return this.uidOrganisationIdCategorieField;
            }
            set
            {
                this.uidOrganisationIdCategorieField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string uidOrganisationId
        {
            get
            {
                return this.uidOrganisationIdField;
            }
            set
            {
                this.uidOrganisationIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0097/2")]
    public enum uidOrganisationIdCategorieType
    {

        /// <remarks/>
        CHE,

        /// <remarks/>
        ADM,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0097/2")]
    public partial class namedOrganisationIdType
    {

        private string organisationIdCategoryField;

        private string organisationIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationIdCategory
        {
            get
            {
                return this.organisationIdCategoryField;
            }
            set
            {
                this.organisationIdCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationId
        {
            get
            {
                return this.organisationIdField;
            }
            set
            {
                this.organisationIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4")]
    public partial class personIdentificationLightType
    {

        private ulong vnField;

        private bool vnFieldSpecified;

        private namedPersonIdType localPersonIdField;

        private namedPersonIdType[] otherPersonIdField;

        private string officialNameField;

        private string firstNameField;

        private string originalNameField;

        private sexType sexField;

        private bool sexFieldSpecified;

        private datePartiallyKnownType dateOfBirthField;

        /// <remarks/>
        public ulong vn
        {
            get
            {
                return this.vnField;
            }
            set
            {
                this.vnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool vnSpecified
        {
            get
            {
                return this.vnFieldSpecified;
            }
            set
            {
                this.vnFieldSpecified = value;
            }
        }

        /// <remarks/>
        public namedPersonIdType localPersonId
        {
            get
            {
                return this.localPersonIdField;
            }
            set
            {
                this.localPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("otherPersonId")]
        public namedPersonIdType[] otherPersonId
        {
            get
            {
                return this.otherPersonIdField;
            }
            set
            {
                this.otherPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string officialName
        {
            get
            {
                return this.officialNameField;
            }
            set
            {
                this.officialNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string originalName
        {
            get
            {
                return this.originalNameField;
            }
            set
            {
                this.originalNameField = value;
            }
        }

        /// <remarks/>
        public sexType sex
        {
            get
            {
                return this.sexField;
            }
            set
            {
                this.sexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sexSpecified
        {
            get
            {
                return this.sexFieldSpecified;
            }
            set
            {
                this.sexFieldSpecified = value;
            }
        }

        /// <remarks/>
        public datePartiallyKnownType dateOfBirth
        {
            get
            {
                return this.dateOfBirthField;
            }
            set
            {
                this.dateOfBirthField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4")]
    public partial class namedPersonIdType
    {

        private string personIdCategoryField;

        private string personIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string personIdCategory
        {
            get
            {
                return this.personIdCategoryField;
            }
            set
            {
                this.personIdCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string personId
        {
            get
            {
                return this.personIdField;
            }
            set
            {
                this.personIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4")]
    public enum sexType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4")]
    public partial class datePartiallyKnownType
    {

        private object itemField;

        private ItemChoiceType itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("year", typeof(string), DataType = "gYear")]
        [System.Xml.Serialization.XmlElementAttribute("yearMonth", typeof(string), DataType = "gYearMonth")]
        [System.Xml.Serialization.XmlElementAttribute("yearMonthDay", typeof(System.DateTime), DataType = "date")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4", IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        year,

        /// <remarks/>
        yearMonth,

        /// <remarks/>
        yearMonthDay,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class personType
    {

        private personTypeIdentification identificationField;

        private addressInformationType addressField;

        /// <remarks/>
        public personTypeIdentification identification
        {
            get
            {
                return this.identificationField;
            }
            set
            {
                this.identificationField = value;
            }
        }

        /// <remarks/>
        public addressInformationType address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class personTypeIdentification
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("organisationIdentification", typeof(organisationIdentificationType))]
        [System.Xml.Serialization.XmlElementAttribute("personIdentification", typeof(personIdentificationLightType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    public partial class addressInformationType
    {

        private string addressLine1Field;

        private string addressLine2Field;

        private string streetField;

        private string houseNumberField;

        private string dwellingNumberField;

        private uint postOfficeBoxNumberField;

        private bool postOfficeBoxNumberFieldSpecified;

        private string postOfficeBoxTextField;

        private string localityField;

        private string townField;

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        private countryType countryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string addressLine1
        {
            get
            {
                return this.addressLine1Field;
            }
            set
            {
                this.addressLine1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string addressLine2
        {
            get
            {
                return this.addressLine2Field;
            }
            set
            {
                this.addressLine2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string houseNumber
        {
            get
            {
                return this.houseNumberField;
            }
            set
            {
                this.houseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string dwellingNumber
        {
            get
            {
                return this.dwellingNumberField;
            }
            set
            {
                this.dwellingNumberField = value;
            }
        }

        /// <remarks/>
        public uint postOfficeBoxNumber
        {
            get
            {
                return this.postOfficeBoxNumberField;
            }
            set
            {
                this.postOfficeBoxNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool postOfficeBoxNumberSpecified
        {
            get
            {
                return this.postOfficeBoxNumberFieldSpecified;
            }
            set
            {
                this.postOfficeBoxNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string postOfficeBoxText
        {
            get
            {
                return this.postOfficeBoxTextField;
            }
            set
            {
                this.postOfficeBoxTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string locality
        {
            get
            {
                return this.localityField;
            }
            set
            {
                this.localityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string town
        {
            get
            {
                return this.townField;
            }
            set
            {
                this.townField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("foreignZipCode", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("swissZipCode", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("swissZipCodeAddOn", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("swissZipCodeId", typeof(int))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        public countryType country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        foreignZipCode,

        /// <remarks/>
        swissZipCode,

        /// <remarks/>
        swissZipCodeAddOn,

        /// <remarks/>
        swissZipCodeId,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    public partial class countryType
    {

        private string countryIdField;

        private string countryIdISO2Field;

        private string countryNameShortField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string countryId
        {
            get
            {
                return this.countryIdField;
            }
            set
            {
                this.countryIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string countryIdISO2
        {
            get
            {
                return this.countryIdISO2Field;
            }
            set
            {
                this.countryIdISO2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string countryNameShort
        {
            get
            {
                return this.countryNameShortField;
            }
            set
            {
                this.countryNameShortField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class contactType
    {

        private string emailAddressField;

        private string phoneNumberField;

        private string faxNumberField;

        /// <remarks/>
        public string emailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }

        /// <remarks/>
        public string phoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }

        /// <remarks/>
        public string faxNumber
        {
            get
            {
                return this.faxNumberField;
            }
            set
            {
                this.faxNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(buildingAuthorityOnlyType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class buildingAuthorityType
    {

        private organisationIdentificationType buildingAuthorityIdentificationTypeField;

        private string descriptionField;

        private string shortDescriptionField;

        private buildingAuthorityTypeContactPerson contactPersonField;

        private contactType contactField;

        private addressInformationType addressField;

        /// <remarks/>
        public organisationIdentificationType buildingAuthorityIdentificationType
        {
            get
            {
                return this.buildingAuthorityIdentificationTypeField;
            }
            set
            {
                this.buildingAuthorityIdentificationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string shortDescription
        {
            get
            {
                return this.shortDescriptionField;
            }
            set
            {
                this.shortDescriptionField = value;
            }
        }

        /// <remarks/>
        public buildingAuthorityTypeContactPerson contactPerson
        {
            get
            {
                return this.contactPersonField;
            }
            set
            {
                this.contactPersonField = value;
            }
        }

        /// <remarks/>
        public contactType contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }

        /// <remarks/>
        public addressInformationType address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class buildingAuthorityTypeContactPerson
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("organisationIdentification", typeof(organisationIdentificationType))]
        [System.Xml.Serialization.XmlElementAttribute("personIdentification", typeof(personIdentificationLightType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class buildingAuthorityOnlyType : buildingAuthorityType
    {
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(estimationObjectOnlyType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("estimationObject", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class estimationObjectType
    {

        private namedIdType localIDField;

        private string volumeField;

        private string yearOfConstructionField;

        private string descriptionField;

        private System.DateTime validFromField;

        private bool validFromFieldSpecified;

        private string estimationReasonField;

        private estimationValueType[] estimationValueField;

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear")]
        public string yearOfConstruction
        {
            get
            {
                return this.yearOfConstructionField;
            }
            set
            {
                this.yearOfConstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime validFrom
        {
            get
            {
                return this.validFromField;
            }
            set
            {
                this.validFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool validFromSpecified
        {
            get
            {
                return this.validFromFieldSpecified;
            }
            set
            {
                this.validFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string estimationReason
        {
            get
            {
                return this.estimationReasonField;
            }
            set
            {
                this.estimationReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("estimationValue")]
        public estimationValueType[] estimationValue
        {
            get
            {
                return this.estimationValueField;
            }
            set
            {
                this.estimationValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class namedIdType
    {

        private string idCategoryField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string IdCategory
        {
            get
            {
                return this.idCategoryField;
            }
            set
            {
                this.idCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class estimationValueType
    {

        private namedIdType localIDField;

        private string baseYearField;

        private System.DateTime validFromField;

        private bool validFromFieldSpecified;

        private decimal indexValueField;

        private bool indexValueFieldSpecified;

        private valueType valueField;

        private typeOfvalueType typeOfvalueField;

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string baseYear
        {
            get
            {
                return this.baseYearField;
            }
            set
            {
                this.baseYearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime validFrom
        {
            get
            {
                return this.validFromField;
            }
            set
            {
                this.validFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool validFromSpecified
        {
            get
            {
                return this.validFromFieldSpecified;
            }
            set
            {
                this.validFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal indexValue
        {
            get
            {
                return this.indexValueField;
            }
            set
            {
                this.indexValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool indexValueSpecified
        {
            get
            {
                return this.indexValueFieldSpecified;
            }
            set
            {
                this.indexValueFieldSpecified = value;
            }
        }

        /// <remarks/>
        public valueType value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        public typeOfvalueType typeOfvalue
        {
            get
            {
                return this.typeOfvalueField;
            }
            set
            {
                this.typeOfvalueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class valueType
    {

        private decimal itemField;

        private ItemChoiceType4 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("amount", typeof(decimal))]
        [System.Xml.Serialization.XmlElementAttribute("percentage", typeof(decimal))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public decimal Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType4 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IncludeInSchema = false)]
    public enum ItemChoiceType4
    {

        /// <remarks/>
        amount,

        /// <remarks/>
        percentage,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum typeOfvalueType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1007")]
        Item1007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2001")]
        Item2001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2002")]
        Item2002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2003")]
        Item2003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2004")]
        Item2004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2005")]
        Item2005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2006")]
        Item2006,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2007")]
        Item2007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2008")]
        Item2008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2009")]
        Item2009,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2010")]
        Item2010,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2011")]
        Item2011,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2012")]
        Item2012,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2013")]
        Item2013,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2014")]
        Item2014,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2015")]
        Item2015,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2016")]
        Item2016,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class estimationObjectOnlyType : estimationObjectType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "kindOfConstructionWorkType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("kindOfConstructionWork", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class kindOfConstructionWorkType1
    {

        private kindOfWorkType kindOfWorkField;

        private bool energeticRestaurationField;

        private bool energeticRestaurationFieldSpecified;

        private bool renovationHeatingsystemField;

        private bool renovationHeatingsystemFieldSpecified;

        private bool innerConversionRenovationField;

        private bool innerConversionRenovationFieldSpecified;

        private bool conversionField;

        private bool conversionFieldSpecified;

        private bool extensionHeighteningHeatedField;

        private bool extensionHeighteningHeatedFieldSpecified;

        private bool extensionHeighteningNotHeatedField;

        private bool extensionHeighteningNotHeatedFieldSpecified;

        private bool thermicSolarFacilityField;

        private bool thermicSolarFacilityFieldSpecified;

        private bool photovoltaicSolarFacilityField;

        private bool photovoltaicSolarFacilityFieldSpecified;

        private bool otherWorksField;

        private bool otherWorksFieldSpecified;

        /// <remarks/>
        public kindOfWorkType kindOfWork
        {
            get
            {
                return this.kindOfWorkField;
            }
            set
            {
                this.kindOfWorkField = value;
            }
        }

        /// <remarks/>
        public bool energeticRestauration
        {
            get
            {
                return this.energeticRestaurationField;
            }
            set
            {
                this.energeticRestaurationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool energeticRestaurationSpecified
        {
            get
            {
                return this.energeticRestaurationFieldSpecified;
            }
            set
            {
                this.energeticRestaurationFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool renovationHeatingsystem
        {
            get
            {
                return this.renovationHeatingsystemField;
            }
            set
            {
                this.renovationHeatingsystemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool renovationHeatingsystemSpecified
        {
            get
            {
                return this.renovationHeatingsystemFieldSpecified;
            }
            set
            {
                this.renovationHeatingsystemFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool innerConversionRenovation
        {
            get
            {
                return this.innerConversionRenovationField;
            }
            set
            {
                this.innerConversionRenovationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool innerConversionRenovationSpecified
        {
            get
            {
                return this.innerConversionRenovationFieldSpecified;
            }
            set
            {
                this.innerConversionRenovationFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool conversion
        {
            get
            {
                return this.conversionField;
            }
            set
            {
                this.conversionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool conversionSpecified
        {
            get
            {
                return this.conversionFieldSpecified;
            }
            set
            {
                this.conversionFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool extensionHeighteningHeated
        {
            get
            {
                return this.extensionHeighteningHeatedField;
            }
            set
            {
                this.extensionHeighteningHeatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool extensionHeighteningHeatedSpecified
        {
            get
            {
                return this.extensionHeighteningHeatedFieldSpecified;
            }
            set
            {
                this.extensionHeighteningHeatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool extensionHeighteningNotHeated
        {
            get
            {
                return this.extensionHeighteningNotHeatedField;
            }
            set
            {
                this.extensionHeighteningNotHeatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool extensionHeighteningNotHeatedSpecified
        {
            get
            {
                return this.extensionHeighteningNotHeatedFieldSpecified;
            }
            set
            {
                this.extensionHeighteningNotHeatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool thermicSolarFacility
        {
            get
            {
                return this.thermicSolarFacilityField;
            }
            set
            {
                this.thermicSolarFacilityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool thermicSolarFacilitySpecified
        {
            get
            {
                return this.thermicSolarFacilityFieldSpecified;
            }
            set
            {
                this.thermicSolarFacilityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool photovoltaicSolarFacility
        {
            get
            {
                return this.photovoltaicSolarFacilityField;
            }
            set
            {
                this.photovoltaicSolarFacilityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool photovoltaicSolarFacilitySpecified
        {
            get
            {
                return this.photovoltaicSolarFacilityFieldSpecified;
            }
            set
            {
                this.photovoltaicSolarFacilityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool otherWorks
        {
            get
            {
                return this.otherWorksField;
            }
            set
            {
                this.otherWorksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool otherWorksSpecified
        {
            get
            {
                return this.otherWorksFieldSpecified;
            }
            set
            {
                this.otherWorksFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum kindOfWorkType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6001")]
        Item6001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6002")]
        Item6002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6007")]
        Item6007,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("partialAreaOfBuilding", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class partialAreaOfBuildingType
    {

        private decimal squareMeasureField;

        private bool squareMeasureFieldSpecified;

        /// <remarks/>
        public decimal squareMeasure
        {
            get
            {
                return this.squareMeasureField;
            }
            set
            {
                this.squareMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool squareMeasureSpecified
        {
            get
            {
                return this.squareMeasureFieldSpecified;
            }
            set
            {
                this.squareMeasureFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("coveringAreaOfSDR", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class coveringAreaOfSDRType
    {

        private decimal squareMeasureField;

        private realestateIdentificationType1 realestateIdentificationField;

        /// <remarks/>
        public decimal squareMeasure
        {
            get
            {
                return this.squareMeasureField;
            }
            set
            {
                this.squareMeasureField = value;
            }
        }

        /// <remarks/>
        public realestateIdentificationType1 realestateIdentification
        {
            get
            {
                return this.realestateIdentificationField;
            }
            set
            {
                this.realestateIdentificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "realestateIdentificationType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class realestateIdentificationType1
    {

        private string eGRIDField;

        private string numberField;

        private string numberSuffixField;

        private string subDistrictField;

        private string lotField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string EGRID
        {
            get
            {
                return this.eGRIDField;
            }
            set
            {
                this.eGRIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string numberSuffix
        {
            get
            {
                return this.numberSuffixField;
            }
            set
            {
                this.numberSuffixField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string subDistrict
        {
            get
            {
                return this.subDistrictField;
            }
            set
            {
                this.subDistrictField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string lot
        {
            get
            {
                return this.lotField;
            }
            set
            {
                this.lotField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("placeName", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class placeNameType
    {

        private placeNameTypeType placeNameType1Field;

        private string localGeographicalNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("placeNameType")]
        public placeNameTypeType placeNameType1
        {
            get
            {
                return this.placeNameType1Field;
            }
            set
            {
                this.placeNameType1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string localGeographicalName
        {
            get
            {
                return this.localGeographicalNameField;
            }
            set
            {
                this.localGeographicalNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum placeNameTypeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("cadastralSurveyorRemark", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class cadastralSurveyorRemarkType
    {

        private remarkTypeType remarkTypeField;

        private string remarkOtherTypeField;

        private string remarkTextField;

        private string objectIDField;

        /// <remarks/>
        public remarkTypeType remarkType
        {
            get
            {
                return this.remarkTypeField;
            }
            set
            {
                this.remarkTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string remarkOtherType
        {
            get
            {
                return this.remarkOtherTypeField;
            }
            set
            {
                this.remarkOtherTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string remarkText
        {
            get
            {
                return this.remarkTextField;
            }
            set
            {
                this.remarkTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string objectID
        {
            get
            {
                return this.objectIDField;
            }
            set
            {
                this.objectIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum remarkTypeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("cadastralMap", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class cadastralMapType
    {

        private string mapNumberField;

        private string identDNField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string mapNumber
        {
            get
            {
                return this.mapNumberField;
            }
            set
            {
                this.mapNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string identDN
        {
            get
            {
                return this.identDNField;
            }
            set
            {
                this.identDNField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("right", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class rightType
    {

        private string eREIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "normalizedString")]
        public string EREID
        {
            get
            {
                return this.eREIDField;
            }
            set
            {
                this.eREIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class streetDescriptionType
    {

        private streetLanguageType[] languageField;

        private string[] descriptionLongField;

        private string[] descriptionShortField;

        private string[] descriptionIndexField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("language")]
        public streetLanguageType[] language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("descriptionLong", DataType = "token")]
        public string[] descriptionLong
        {
            get
            {
                return this.descriptionLongField;
            }
            set
            {
                this.descriptionLongField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("descriptionShort", DataType = "token")]
        public string[] descriptionShort
        {
            get
            {
                return this.descriptionShortField;
            }
            set
            {
                this.descriptionShortField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("descriptionIndex", DataType = "token")]
        public string[] descriptionIndex
        {
            get
            {
                return this.descriptionIndexField;
            }
            set
            {
                this.descriptionIndexField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum streetLanguageType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9901")]
        Item9901,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9902")]
        Item9902,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9903")]
        Item9903,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9904")]
        Item9904,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "streetType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("street", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class streetType2
    {

        private string eSIDField;

        private bool isOfficialDescriptionField;

        private bool isOfficialDescriptionFieldSpecified;

        private string officialStreetNumberField;

        private namedIdType localIDField;

        private streetKindType streetKindField;

        private bool streetKindFieldSpecified;

        private streetDescriptionType descriptionField;

        private streetStatusType streetStatusField;

        private bool streetStatusFieldSpecified;

        private object streetGeometryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string ESID
        {
            get
            {
                return this.eSIDField;
            }
            set
            {
                this.eSIDField = value;
            }
        }

        /// <remarks/>
        public bool isOfficialDescription
        {
            get
            {
                return this.isOfficialDescriptionField;
            }
            set
            {
                this.isOfficialDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isOfficialDescriptionSpecified
        {
            get
            {
                return this.isOfficialDescriptionFieldSpecified;
            }
            set
            {
                this.isOfficialDescriptionFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string officialStreetNumber
        {
            get
            {
                return this.officialStreetNumberField;
            }
            set
            {
                this.officialStreetNumberField = value;
            }
        }

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        public streetKindType streetKind
        {
            get
            {
                return this.streetKindField;
            }
            set
            {
                this.streetKindField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool streetKindSpecified
        {
            get
            {
                return this.streetKindFieldSpecified;
            }
            set
            {
                this.streetKindFieldSpecified = value;
            }
        }

        /// <remarks/>
        public streetDescriptionType description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public streetStatusType streetStatus
        {
            get
            {
                return this.streetStatusField;
            }
            set
            {
                this.streetStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool streetStatusSpecified
        {
            get
            {
                return this.streetStatusFieldSpecified;
            }
            set
            {
                this.streetStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        public object streetGeometry
        {
            get
            {
                return this.streetGeometryField;
            }
            set
            {
                this.streetGeometryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum streetKindType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9801")]
        Item9801,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9802")]
        Item9802,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9803")]
        Item9803,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9809")]
        Item9809,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum streetStatusType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9811")]
        Item9811,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9812")]
        Item9812,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9813")]
        Item9813,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9814")]
        Item9814,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(insuranceObjectOnlyType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("insuranceObject", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class insuranceObjectType
    {

        private namedIdType localIDField;

        private System.DateTime startDateField;

        private bool startDateFieldSpecified;

        private System.DateTime endDateField;

        private bool endDateFieldSpecified;

        private string insuranceNumberField;

        private usageCodeType usageCodeField;

        private bool usageCodeFieldSpecified;

        private string usageDescriptionField;

        private insuranceValueType insuranceValueField;

        private insuranceVolumeType volumeField;

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime startDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool startDateSpecified
        {
            get
            {
                return this.startDateFieldSpecified;
            }
            set
            {
                this.startDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime endDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool endDateSpecified
        {
            get
            {
                return this.endDateFieldSpecified;
            }
            set
            {
                this.endDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string insuranceNumber
        {
            get
            {
                return this.insuranceNumberField;
            }
            set
            {
                this.insuranceNumberField = value;
            }
        }

        /// <remarks/>
        public usageCodeType usageCode
        {
            get
            {
                return this.usageCodeField;
            }
            set
            {
                this.usageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool usageCodeSpecified
        {
            get
            {
                return this.usageCodeFieldSpecified;
            }
            set
            {
                this.usageCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string usageDescription
        {
            get
            {
                return this.usageDescriptionField;
            }
            set
            {
                this.usageDescriptionField = value;
            }
        }

        /// <remarks/>
        public insuranceValueType insuranceValue
        {
            get
            {
                return this.insuranceValueField;
            }
            set
            {
                this.insuranceValueField = value;
            }
        }

        /// <remarks/>
        public insuranceVolumeType volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum usageCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1199")]
        Item1199,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1219")]
        Item1219,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1220")]
        Item1220,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1230")]
        Item1230,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1241")]
        Item1241,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1242")]
        Item1242,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1252")]
        Item1252,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1259")]
        Item1259,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1263")]
        Item1263,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1264")]
        Item1264,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1265")]
        Item1265,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1269")]
        Item1269,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1271")]
        Item1271,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1272")]
        Item1272,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1274")]
        Item1274,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class insuranceValueType
    {

        private namedIdType localIDField;

        private System.DateTime validFromField;

        private changeReasonType changeReasonField;

        private insuranceSumType insuranceSumField;

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime validFrom
        {
            get
            {
                return this.validFromField;
            }
            set
            {
                this.validFromField = value;
            }
        }

        /// <remarks/>
        public changeReasonType changeReason
        {
            get
            {
                return this.changeReasonField;
            }
            set
            {
                this.changeReasonField = value;
            }
        }

        /// <remarks/>
        public insuranceSumType insuranceSum
        {
            get
            {
                return this.insuranceSumField;
            }
            set
            {
                this.insuranceSumField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum changeReasonType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1001")]
        Item1001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1002")]
        Item1002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1003")]
        Item1003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1004")]
        Item1004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1005")]
        Item1005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1006")]
        Item1006,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class insuranceSumType
    {

        private decimal itemField;

        private ItemChoiceType3 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("amount", typeof(decimal))]
        [System.Xml.Serialization.XmlElementAttribute("percentage", typeof(decimal))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public decimal Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType3 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IncludeInSchema = false)]
    public enum ItemChoiceType3
    {

        /// <remarks/>
        amount,

        /// <remarks/>
        percentage,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class insuranceVolumeType
    {

        private string volumeField;

        private buildingVolumeNormType normField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        public buildingVolumeNormType norm
        {
            get
            {
                return this.normField;
            }
            set
            {
                this.normField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum buildingVolumeNormType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("961")]
        Item961,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("962")]
        Item962,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("969")]
        Item969,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class insuranceObjectOnlyType : insuranceObjectType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("area", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class areaType
    {

        private areaTypeType areaType1Field;

        private areaDescriptionCodeType areaDescriptionCodeField;

        private string areaDescriptionField;

        private decimal areaValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("areaType")]
        public areaTypeType areaType1
        {
            get
            {
                return this.areaType1Field;
            }
            set
            {
                this.areaType1Field = value;
            }
        }

        /// <remarks/>
        public areaDescriptionCodeType areaDescriptionCode
        {
            get
            {
                return this.areaDescriptionCodeField;
            }
            set
            {
                this.areaDescriptionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string areaDescription
        {
            get
            {
                return this.areaDescriptionField;
            }
            set
            {
                this.areaDescriptionField = value;
            }
        }

        /// <remarks/>
        public decimal areaValue
        {
            get
            {
                return this.areaValueField;
            }
            set
            {
                this.areaValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum areaTypeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum areaDescriptionCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        Item8,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("18")]
        Item18,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("19")]
        Item19,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20")]
        Item20,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22")]
        Item22,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        Item25,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("fiscalOwnership", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class fiscalOwnershipType
    {

        private System.DateTime accessionDateField;

        private fiscalOwnershipTypeFiscalRelationship fiscalRelationshipField;

        private System.DateTime validFromField;

        private bool validFromFieldSpecified;

        private System.DateTime validTillField;

        private bool validTillFieldSpecified;

        private decimal denominatorField;

        private bool denominatorFieldSpecified;

        private decimal tallyField;

        private bool tallyFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime accessionDate
        {
            get
            {
                return this.accessionDateField;
            }
            set
            {
                this.accessionDateField = value;
            }
        }

        /// <remarks/>
        public fiscalOwnershipTypeFiscalRelationship fiscalRelationship
        {
            get
            {
                return this.fiscalRelationshipField;
            }
            set
            {
                this.fiscalRelationshipField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime validFrom
        {
            get
            {
                return this.validFromField;
            }
            set
            {
                this.validFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool validFromSpecified
        {
            get
            {
                return this.validFromFieldSpecified;
            }
            set
            {
                this.validFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime validTill
        {
            get
            {
                return this.validTillField;
            }
            set
            {
                this.validTillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool validTillSpecified
        {
            get
            {
                return this.validTillFieldSpecified;
            }
            set
            {
                this.validTillFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal denominator
        {
            get
            {
                return this.denominatorField;
            }
            set
            {
                this.denominatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool denominatorSpecified
        {
            get
            {
                return this.denominatorFieldSpecified;
            }
            set
            {
                this.denominatorFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal tally
        {
            get
            {
                return this.tallyField;
            }
            set
            {
                this.tallyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tallySpecified
        {
            get
            {
                return this.tallyFieldSpecified;
            }
            set
            {
                this.tallyFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum fiscalOwnershipTypeFiscalRelationship
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "localityType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("locality", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class localityType1
    {

        private uint swissZipCodeField;

        private bool swissZipCodeFieldSpecified;

        private string swissZipCodeAddOnField;

        private localityNameType nameField;

        /// <remarks/>
        public uint swissZipCode
        {
            get
            {
                return this.swissZipCodeField;
            }
            set
            {
                this.swissZipCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool swissZipCodeSpecified
        {
            get
            {
                return this.swissZipCodeFieldSpecified;
            }
            set
            {
                this.swissZipCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string swissZipCodeAddOn
        {
            get
            {
                return this.swissZipCodeAddOnField;
            }
            set
            {
                this.swissZipCodeAddOnField = value;
            }
        }

        /// <remarks/>
        public localityNameType name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class localityNameType
    {

        private streetLanguageType[] languageField;

        private string[] nameLongField;

        private string[] nameShortField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("language")]
        public streetLanguageType[] language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("nameLong", DataType = "token")]
        public string[] nameLong
        {
            get
            {
                return this.nameLongField;
            }
            set
            {
                this.nameLongField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("nameShort", DataType = "token")]
        public string[] nameShort
        {
            get
            {
                return this.nameShortField;
            }
            set
            {
                this.nameShortField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("realestate", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class realestateType
    {

        private realestateIdentificationType1 realestateIdentificationField;

        private string authorityField;

        private System.DateTime dateField;

        private bool dateFieldSpecified;

        private realestateTypeType realestateType1Field;

        private string cantonalSubKindField;

        private realestateStatusType statusField;

        private bool statusFieldSpecified;

        private string mutnumberField;

        private string identDNField;

        private decimal squareMeasureField;

        private bool squareMeasureFieldSpecified;

        private bool realestateIncompleteField;

        private bool realestateIncompleteFieldSpecified;

        private coordinatesType coordinatesField;

        private namedMetaDataType1[] namedMetaDataField;

        /// <remarks/>
        public realestateIdentificationType1 realestateIdentification
        {
            get
            {
                return this.realestateIdentificationField;
            }
            set
            {
                this.realestateIdentificationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string authority
        {
            get
            {
                return this.authorityField;
            }
            set
            {
                this.authorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dateSpecified
        {
            get
            {
                return this.dateFieldSpecified;
            }
            set
            {
                this.dateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("realestateType")]
        public realestateTypeType realestateType1
        {
            get
            {
                return this.realestateType1Field;
            }
            set
            {
                this.realestateType1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string cantonalSubKind
        {
            get
            {
                return this.cantonalSubKindField;
            }
            set
            {
                this.cantonalSubKindField = value;
            }
        }

        /// <remarks/>
        public realestateStatusType status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool statusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }
            set
            {
                this.statusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string mutnumber
        {
            get
            {
                return this.mutnumberField;
            }
            set
            {
                this.mutnumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string identDN
        {
            get
            {
                return this.identDNField;
            }
            set
            {
                this.identDNField = value;
            }
        }

        /// <remarks/>
        public decimal squareMeasure
        {
            get
            {
                return this.squareMeasureField;
            }
            set
            {
                this.squareMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool squareMeasureSpecified
        {
            get
            {
                return this.squareMeasureFieldSpecified;
            }
            set
            {
                this.squareMeasureFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool realestateIncomplete
        {
            get
            {
                return this.realestateIncompleteField;
            }
            set
            {
                this.realestateIncompleteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool realestateIncompleteSpecified
        {
            get
            {
                return this.realestateIncompleteFieldSpecified;
            }
            set
            {
                this.realestateIncompleteFieldSpecified = value;
            }
        }

        /// <remarks/>
        public coordinatesType coordinates
        {
            get
            {
                return this.coordinatesField;
            }
            set
            {
                this.coordinatesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("namedMetaData")]
        public namedMetaDataType1[] namedMetaData
        {
            get
            {
                return this.namedMetaDataField;
            }
            set
            {
                this.namedMetaDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum realestateTypeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        Item8,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum realestateStatusType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class coordinatesType
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LV03", typeof(coordinatesTypeLV03))]
        [System.Xml.Serialization.XmlElementAttribute("LV95", typeof(coordinatesTypeLV95))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class coordinatesTypeLV03
    {

        private decimal yField;

        private decimal xField;

        private originOfCoordinatesType originOfCoordinatesField;

        /// <remarks/>
        public decimal Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        public decimal X
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        public originOfCoordinatesType originOfCoordinates
        {
            get
            {
                return this.originOfCoordinatesField;
            }
            set
            {
                this.originOfCoordinatesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum originOfCoordinatesType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("901")]
        Item901,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("902")]
        Item902,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("903")]
        Item903,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("904")]
        Item904,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("905")]
        Item905,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("906")]
        Item906,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("909")]
        Item909,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class coordinatesTypeLV95
    {

        private decimal eastField;

        private decimal northField;

        private originOfCoordinatesType originOfCoordinatesField;

        /// <remarks/>
        public decimal east
        {
            get
            {
                return this.eastField;
            }
            set
            {
                this.eastField = value;
            }
        }

        /// <remarks/>
        public decimal north
        {
            get
            {
                return this.northField;
            }
            set
            {
                this.northField = value;
            }
        }

        /// <remarks/>
        public originOfCoordinatesType originOfCoordinates
        {
            get
            {
                return this.originOfCoordinatesField;
            }
            set
            {
                this.originOfCoordinatesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "namedMetaDataType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class namedMetaDataType1
    {

        private string metaDataNameField;

        private string metaDataValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string metaDataName
        {
            get
            {
                return this.metaDataNameField;
            }
            set
            {
                this.metaDataNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string metaDataValue
        {
            get
            {
                return this.metaDataValueField;
            }
            set
            {
                this.metaDataValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "dwellingType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("dwelling", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class dwellingType1
    {

        private namedIdType[] localIDField;

        private string administrativeDwellingNoField;

        private string eWIDField;

        private string physicalDwellingNoField;

        private datePartiallyKnownType1 dateOfConstructionField;

        private datePartiallyKnownType1 dateOfDemolitionField;

        private string noOfHabitableRoomsField;

        private string floorField;

        private string locationOfDwellingOnFloorField;

        private bool multipleFloorField;

        private bool multipleFloorFieldSpecified;

        private usageLimitationType usageLimitationField;

        private bool usageLimitationFieldSpecified;

        private bool kitchenField;

        private bool kitchenFieldSpecified;

        private string surfaceAreaOfDwellingField;

        private dwellingStatusType statusField;

        private bool statusFieldSpecified;

        private dwellingUsageType dwellingUsageField;

        private string[] dwellingFreeTextField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("localID")]
        public namedIdType[] localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string administrativeDwellingNo
        {
            get
            {
                return this.administrativeDwellingNoField;
            }
            set
            {
                this.administrativeDwellingNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EWID
        {
            get
            {
                return this.eWIDField;
            }
            set
            {
                this.eWIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string physicalDwellingNo
        {
            get
            {
                return this.physicalDwellingNoField;
            }
            set
            {
                this.physicalDwellingNoField = value;
            }
        }

        /// <remarks/>
        public datePartiallyKnownType1 dateOfConstruction
        {
            get
            {
                return this.dateOfConstructionField;
            }
            set
            {
                this.dateOfConstructionField = value;
            }
        }

        /// <remarks/>
        public datePartiallyKnownType1 dateOfDemolition
        {
            get
            {
                return this.dateOfDemolitionField;
            }
            set
            {
                this.dateOfDemolitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string noOfHabitableRooms
        {
            get
            {
                return this.noOfHabitableRoomsField;
            }
            set
            {
                this.noOfHabitableRoomsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string floor
        {
            get
            {
                return this.floorField;
            }
            set
            {
                this.floorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string locationOfDwellingOnFloor
        {
            get
            {
                return this.locationOfDwellingOnFloorField;
            }
            set
            {
                this.locationOfDwellingOnFloorField = value;
            }
        }

        /// <remarks/>
        public bool multipleFloor
        {
            get
            {
                return this.multipleFloorField;
            }
            set
            {
                this.multipleFloorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool multipleFloorSpecified
        {
            get
            {
                return this.multipleFloorFieldSpecified;
            }
            set
            {
                this.multipleFloorFieldSpecified = value;
            }
        }

        /// <remarks/>
        public usageLimitationType usageLimitation
        {
            get
            {
                return this.usageLimitationField;
            }
            set
            {
                this.usageLimitationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool usageLimitationSpecified
        {
            get
            {
                return this.usageLimitationFieldSpecified;
            }
            set
            {
                this.usageLimitationFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool kitchen
        {
            get
            {
                return this.kitchenField;
            }
            set
            {
                this.kitchenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool kitchenSpecified
        {
            get
            {
                return this.kitchenFieldSpecified;
            }
            set
            {
                this.kitchenFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string surfaceAreaOfDwelling
        {
            get
            {
                return this.surfaceAreaOfDwellingField;
            }
            set
            {
                this.surfaceAreaOfDwellingField = value;
            }
        }

        /// <remarks/>
        public dwellingStatusType status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool statusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }
            set
            {
                this.statusFieldSpecified = value;
            }
        }

        /// <remarks/>
        public dwellingUsageType dwellingUsage
        {
            get
            {
                return this.dwellingUsageField;
            }
            set
            {
                this.dwellingUsageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dwellingFreeText", DataType = "token")]
        public string[] dwellingFreeText
        {
            get
            {
                return this.dwellingFreeTextField;
            }
            set
            {
                this.dwellingFreeTextField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "datePartiallyKnownType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class datePartiallyKnownType1
    {

        private object itemField;

        private ItemChoiceType1 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("year", typeof(string), DataType = "gYear")]
        [System.Xml.Serialization.XmlElementAttribute("yearMonth", typeof(string), DataType = "gYearMonth")]
        [System.Xml.Serialization.XmlElementAttribute("yearMonthDay", typeof(System.DateTime), DataType = "date")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IncludeInSchema = false)]
    public enum ItemChoiceType1
    {

        /// <remarks/>
        year,

        /// <remarks/>
        yearMonth,

        /// <remarks/>
        yearMonthDay,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum usageLimitationType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3401")]
        Item3401,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3402")]
        Item3402,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3403")]
        Item3403,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3404")]
        Item3404,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum dwellingStatusType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3001")]
        Item3001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3002")]
        Item3002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3003")]
        Item3003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3004")]
        Item3004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3005")]
        Item3005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3007")]
        Item3007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3008")]
        Item3008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3009")]
        Item3009,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class dwellingUsageType
    {

        private dwellingUsageCodeType usageCodeField;

        private bool usageCodeFieldSpecified;

        private dwellingInformationSourceType informationSourceField;

        private bool informationSourceFieldSpecified;

        private System.DateTime revisionDateField;

        private bool revisionDateFieldSpecified;

        private string remarkField;

        private bool personWithMainResidenceField;

        private bool personWithMainResidenceFieldSpecified;

        private bool personWithSecondaryResidenceField;

        private bool personWithSecondaryResidenceFieldSpecified;

        private System.DateTime dateFirstOccupancyField;

        private bool dateFirstOccupancyFieldSpecified;

        private System.DateTime dateLastOccupancyField;

        private bool dateLastOccupancyFieldSpecified;

        /// <remarks/>
        public dwellingUsageCodeType usageCode
        {
            get
            {
                return this.usageCodeField;
            }
            set
            {
                this.usageCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool usageCodeSpecified
        {
            get
            {
                return this.usageCodeFieldSpecified;
            }
            set
            {
                this.usageCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public dwellingInformationSourceType informationSource
        {
            get
            {
                return this.informationSourceField;
            }
            set
            {
                this.informationSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool informationSourceSpecified
        {
            get
            {
                return this.informationSourceFieldSpecified;
            }
            set
            {
                this.informationSourceFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime revisionDate
        {
            get
            {
                return this.revisionDateField;
            }
            set
            {
                this.revisionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool revisionDateSpecified
        {
            get
            {
                return this.revisionDateFieldSpecified;
            }
            set
            {
                this.revisionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string remark
        {
            get
            {
                return this.remarkField;
            }
            set
            {
                this.remarkField = value;
            }
        }

        /// <remarks/>
        public bool personWithMainResidence
        {
            get
            {
                return this.personWithMainResidenceField;
            }
            set
            {
                this.personWithMainResidenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool personWithMainResidenceSpecified
        {
            get
            {
                return this.personWithMainResidenceFieldSpecified;
            }
            set
            {
                this.personWithMainResidenceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool personWithSecondaryResidence
        {
            get
            {
                return this.personWithSecondaryResidenceField;
            }
            set
            {
                this.personWithSecondaryResidenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool personWithSecondaryResidenceSpecified
        {
            get
            {
                return this.personWithSecondaryResidenceFieldSpecified;
            }
            set
            {
                this.personWithSecondaryResidenceFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dateFirstOccupancy
        {
            get
            {
                return this.dateFirstOccupancyField;
            }
            set
            {
                this.dateFirstOccupancyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dateFirstOccupancySpecified
        {
            get
            {
                return this.dateFirstOccupancyFieldSpecified;
            }
            set
            {
                this.dateFirstOccupancyFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dateLastOccupancy
        {
            get
            {
                return this.dateLastOccupancyField;
            }
            set
            {
                this.dateLastOccupancyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dateLastOccupancySpecified
        {
            get
            {
                return this.dateLastOccupancyFieldSpecified;
            }
            set
            {
                this.dateLastOccupancyFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum dwellingUsageCodeType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3010")]
        Item3010,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3020")]
        Item3020,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3030")]
        Item3030,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3031")]
        Item3031,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3032")]
        Item3032,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3033")]
        Item3033,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3034")]
        Item3034,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3035")]
        Item3035,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3036")]
        Item3036,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3037")]
        Item3037,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3038")]
        Item3038,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3070")]
        Item3070,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum dwellingInformationSourceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3090")]
        Item3090,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3091")]
        Item3091,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3092")]
        Item3092,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3093")]
        Item3093,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class dwellingIdentificationType
    {

        private string eGIDField;

        private string eDIDField;

        private string eWIDField;

        private namedIdType localIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGID
        {
            get
            {
                return this.eGIDField;
            }
            set
            {
                this.eGIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EDID
        {
            get
            {
                return this.eDIDField;
            }
            set
            {
                this.eDIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EWID
        {
            get
            {
                return this.eWIDField;
            }
            set
            {
                this.eWIDField = value;
            }
        }

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("buildingEntranceOnly", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class buildingEntranceOnlyType
    {

        private string eGAIDField;

        private string eDIDField;

        private string buildingEntranceNoField;

        private coordinatesType coordinatesField;

        private namedIdType localIDField;

        private bool isOfficialAddressField;

        private bool isOfficialAddressFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGAID
        {
            get
            {
                return this.eGAIDField;
            }
            set
            {
                this.eGAIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EDID
        {
            get
            {
                return this.eDIDField;
            }
            set
            {
                this.eDIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string buildingEntranceNo
        {
            get
            {
                return this.buildingEntranceNoField;
            }
            set
            {
                this.buildingEntranceNoField = value;
            }
        }

        /// <remarks/>
        public coordinatesType coordinates
        {
            get
            {
                return this.coordinatesField;
            }
            set
            {
                this.coordinatesField = value;
            }
        }

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        public bool isOfficialAddress
        {
            get
            {
                return this.isOfficialAddressField;
            }
            set
            {
                this.isOfficialAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isOfficialAddressSpecified
        {
            get
            {
                return this.isOfficialAddressFieldSpecified;
            }
            set
            {
                this.isOfficialAddressFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class buildingEntranceIdentificationType
    {

        private string eGIDField;

        private string eGAIDField;

        private string eDIDField;

        private namedIdType localIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGID
        {
            get
            {
                return this.eGIDField;
            }
            set
            {
                this.eGIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGAID
        {
            get
            {
                return this.eGAIDField;
            }
            set
            {
                this.eGAIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EDID
        {
            get
            {
                return this.eDIDField;
            }
            set
            {
                this.eDIDField = value;
            }
        }

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class streetSectionType
    {

        private string eSIDField;

        private uint swissZipCodeField;

        private string swissZipCodeAddOnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string ESID
        {
            get
            {
                return this.eSIDField;
            }
            set
            {
                this.eSIDField = value;
            }
        }

        /// <remarks/>
        public uint swissZipCode
        {
            get
            {
                return this.swissZipCodeField;
            }
            set
            {
                this.swissZipCodeField = value;
            }
        }

        /// <remarks/>
        public string swissZipCodeAddOn
        {
            get
            {
                return this.swissZipCodeAddOnField;
            }
            set
            {
                this.swissZipCodeAddOnField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "buildingEntranceType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("buildingEntrance", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class buildingEntranceType1
    {

        private string eGAIDField;

        private string eDIDField;

        private string buildingEntranceNoField;

        private coordinatesType coordinatesField;

        private namedIdType localIDField;

        private bool isOfficialAddressField;

        private bool isOfficialAddressFieldSpecified;

        private streetSectionType steetSectionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGAID
        {
            get
            {
                return this.eGAIDField;
            }
            set
            {
                this.eGAIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EDID
        {
            get
            {
                return this.eDIDField;
            }
            set
            {
                this.eDIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string buildingEntranceNo
        {
            get
            {
                return this.buildingEntranceNoField;
            }
            set
            {
                this.buildingEntranceNoField = value;
            }
        }

        /// <remarks/>
        public coordinatesType coordinates
        {
            get
            {
                return this.coordinatesField;
            }
            set
            {
                this.coordinatesField = value;
            }
        }

        /// <remarks/>
        public namedIdType localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        public bool isOfficialAddress
        {
            get
            {
                return this.isOfficialAddressField;
            }
            set
            {
                this.isOfficialAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isOfficialAddressSpecified
        {
            get
            {
                return this.isOfficialAddressFieldSpecified;
            }
            set
            {
                this.isOfficialAddressFieldSpecified = value;
            }
        }

        /// <remarks/>
        public streetSectionType steetSection
        {
            get
            {
                return this.steetSectionField;
            }
            set
            {
                this.steetSectionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(buildingOnlyType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "buildingType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("building", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class buildingType1
    {

        private buildingIdentificationType buildingIdentificationField;

        private string eGIDField;

        private string officialBuildingNoField;

        private string nameField;

        private buildingDateType1 dateOfConstructionField;

        private buildingDateType1 dateOfRenovationField;

        private datePartiallyKnownType1 dateOfDemolitionField;

        private string numberOfFloorsField;

        private string numberOfSeparateHabitableRoomsField;

        private string surfaceAreaOfBuildingField;

        private string subSurfaceAreaOfBuildingField;

        private string surfaceAreaOfBuildingSignaleObjectField;

        private buildingCategoryType buildingCategoryField;

        private string buildingClassField;

        private buildingStatusType statusField;

        private bool statusFieldSpecified;

        private coordinatesType coordinatesField;

        private namedIdType[] otherIDField;

        private bool civilDefenseShelterField;

        private bool civilDefenseShelterFieldSpecified;

        private string neighbourhoodField;

        private string[] localCodeField;

        private string energyRelevantSurfaceField;

        private buildingVolumeType1 volumeField;

        private heatingType[] heatingField;

        private hotWaterType[] hotWaterField;

        private buildingEntranceType1[] buildingEntranceField;

        private namedMetaDataType1[] namedMetaDataField;

        private string[] buildingFreeTextField;

        /// <remarks/>
        public buildingIdentificationType buildingIdentification
        {
            get
            {
                return this.buildingIdentificationField;
            }
            set
            {
                this.buildingIdentificationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGID
        {
            get
            {
                return this.eGIDField;
            }
            set
            {
                this.eGIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string officialBuildingNo
        {
            get
            {
                return this.officialBuildingNoField;
            }
            set
            {
                this.officialBuildingNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public buildingDateType1 dateOfConstruction
        {
            get
            {
                return this.dateOfConstructionField;
            }
            set
            {
                this.dateOfConstructionField = value;
            }
        }

        /// <remarks/>
        public buildingDateType1 dateOfRenovation
        {
            get
            {
                return this.dateOfRenovationField;
            }
            set
            {
                this.dateOfRenovationField = value;
            }
        }

        /// <remarks/>
        public datePartiallyKnownType1 dateOfDemolition
        {
            get
            {
                return this.dateOfDemolitionField;
            }
            set
            {
                this.dateOfDemolitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfFloors
        {
            get
            {
                return this.numberOfFloorsField;
            }
            set
            {
                this.numberOfFloorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfSeparateHabitableRooms
        {
            get
            {
                return this.numberOfSeparateHabitableRoomsField;
            }
            set
            {
                this.numberOfSeparateHabitableRoomsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string surfaceAreaOfBuilding
        {
            get
            {
                return this.surfaceAreaOfBuildingField;
            }
            set
            {
                this.surfaceAreaOfBuildingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string subSurfaceAreaOfBuilding
        {
            get
            {
                return this.subSurfaceAreaOfBuildingField;
            }
            set
            {
                this.subSurfaceAreaOfBuildingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string surfaceAreaOfBuildingSignaleObject
        {
            get
            {
                return this.surfaceAreaOfBuildingSignaleObjectField;
            }
            set
            {
                this.surfaceAreaOfBuildingSignaleObjectField = value;
            }
        }

        /// <remarks/>
        public buildingCategoryType buildingCategory
        {
            get
            {
                return this.buildingCategoryField;
            }
            set
            {
                this.buildingCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string buildingClass
        {
            get
            {
                return this.buildingClassField;
            }
            set
            {
                this.buildingClassField = value;
            }
        }

        /// <remarks/>
        public buildingStatusType status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool statusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }
            set
            {
                this.statusFieldSpecified = value;
            }
        }

        /// <remarks/>
        public coordinatesType coordinates
        {
            get
            {
                return this.coordinatesField;
            }
            set
            {
                this.coordinatesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("otherID")]
        public namedIdType[] otherID
        {
            get
            {
                return this.otherIDField;
            }
            set
            {
                this.otherIDField = value;
            }
        }

        /// <remarks/>
        public bool civilDefenseShelter
        {
            get
            {
                return this.civilDefenseShelterField;
            }
            set
            {
                this.civilDefenseShelterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool civilDefenseShelterSpecified
        {
            get
            {
                return this.civilDefenseShelterFieldSpecified;
            }
            set
            {
                this.civilDefenseShelterFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string neighbourhood
        {
            get
            {
                return this.neighbourhoodField;
            }
            set
            {
                this.neighbourhoodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("localCode", DataType = "token")]
        public string[] localCode
        {
            get
            {
                return this.localCodeField;
            }
            set
            {
                this.localCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string energyRelevantSurface
        {
            get
            {
                return this.energyRelevantSurfaceField;
            }
            set
            {
                this.energyRelevantSurfaceField = value;
            }
        }

        /// <remarks/>
        public buildingVolumeType1 volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("heating")]
        public heatingType[] heating
        {
            get
            {
                return this.heatingField;
            }
            set
            {
                this.heatingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("hotWater")]
        public hotWaterType[] hotWater
        {
            get
            {
                return this.hotWaterField;
            }
            set
            {
                this.hotWaterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("buildingEntrance")]
        public buildingEntranceType1[] buildingEntrance
        {
            get
            {
                return this.buildingEntranceField;
            }
            set
            {
                this.buildingEntranceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("namedMetaData")]
        public namedMetaDataType1[] namedMetaData
        {
            get
            {
                return this.namedMetaDataField;
            }
            set
            {
                this.namedMetaDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("buildingFreeText", DataType = "token")]
        public string[] buildingFreeText
        {
            get
            {
                return this.buildingFreeTextField;
            }
            set
            {
                this.buildingFreeTextField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class buildingIdentificationType
    {

        private object[] itemsField;

        private ItemsChoiceType1[] itemsElementNameField;

        private namedIdType[] localIDField;

        private int municipalityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EGID", typeof(string), DataType = "nonNegativeInteger")]
        [System.Xml.Serialization.XmlElementAttribute("EGRID", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("cadasterAreaNumber", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("houseNumber", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("nameOfBuilding", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("number", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("officialBuildingNo", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("realestateType", typeof(realestateTypeType))]
        [System.Xml.Serialization.XmlElementAttribute("street", typeof(string), DataType = "token")]
        [System.Xml.Serialization.XmlElementAttribute("zipCode", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("localID")]
        public namedIdType[] localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        public int municipality
        {
            get
            {
                return this.municipalityField;
            }
            set
            {
                this.municipalityField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {

        /// <remarks/>
        EGID,

        /// <remarks/>
        EGRID,

        /// <remarks/>
        cadasterAreaNumber,

        /// <remarks/>
        houseNumber,

        /// <remarks/>
        nameOfBuilding,

        /// <remarks/>
        number,

        /// <remarks/>
        officialBuildingNo,

        /// <remarks/>
        realestateType,

        /// <remarks/>
        street,

        /// <remarks/>
        zipCode,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "buildingDateType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class buildingDateType1
    {

        private object itemField;

        private ItemChoiceType2 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("periodOfConstruction", typeof(periodOfConstructionType))]
        [System.Xml.Serialization.XmlElementAttribute("year", typeof(string), DataType = "gYear")]
        [System.Xml.Serialization.XmlElementAttribute("yearMonth", typeof(string), DataType = "gYearMonth")]
        [System.Xml.Serialization.XmlElementAttribute("yearMonthDay", typeof(System.DateTime), DataType = "date")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType2 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum periodOfConstructionType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8011")]
        Item8011,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8012")]
        Item8012,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8013")]
        Item8013,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8014")]
        Item8014,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8015")]
        Item8015,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8016")]
        Item8016,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8017")]
        Item8017,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8018")]
        Item8018,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8019")]
        Item8019,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8020")]
        Item8020,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8021")]
        Item8021,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8022")]
        Item8022,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8023")]
        Item8023,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IncludeInSchema = false)]
    public enum ItemChoiceType2
    {

        /// <remarks/>
        periodOfConstruction,

        /// <remarks/>
        year,

        /// <remarks/>
        yearMonth,

        /// <remarks/>
        yearMonthDay,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum buildingCategoryType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1010")]
        Item1010,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1020")]
        Item1020,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1030")]
        Item1030,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1040")]
        Item1040,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1060")]
        Item1060,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1080")]
        Item1080,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum buildingStatusType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1001")]
        Item1001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1002")]
        Item1002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1003")]
        Item1003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1004")]
        Item1004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1005")]
        Item1005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1007")]
        Item1007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1008")]
        Item1008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1009")]
        Item1009,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "buildingVolumeType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class buildingVolumeType1
    {

        private string volumeField;

        private buildingVolumeInformationSourceType informationSourceField;

        private bool informationSourceFieldSpecified;

        private buildingVolumeNormType normField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        public buildingVolumeInformationSourceType informationSource
        {
            get
            {
                return this.informationSourceField;
            }
            set
            {
                this.informationSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool informationSourceSpecified
        {
            get
            {
                return this.informationSourceFieldSpecified;
            }
            set
            {
                this.informationSourceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public buildingVolumeNormType norm
        {
            get
            {
                return this.normField;
            }
            set
            {
                this.normField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum buildingVolumeInformationSourceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("869")]
        Item869,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("858")]
        Item858,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("853")]
        Item853,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("852")]
        Item852,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("857")]
        Item857,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("851")]
        Item851,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("870")]
        Item870,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("878")]
        Item878,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("859")]
        Item859,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class heatingType
    {

        private heatGeneratorHeatingType heatGeneratorHeatingField;

        private energySourceType energySourceHeatingField;

        private bool energySourceHeatingFieldSpecified;

        private informationSourceType informationSourceHeatingField;

        private bool informationSourceHeatingFieldSpecified;

        private System.DateTime revisionDateField;

        private bool revisionDateFieldSpecified;

        /// <remarks/>
        public heatGeneratorHeatingType heatGeneratorHeating
        {
            get
            {
                return this.heatGeneratorHeatingField;
            }
            set
            {
                this.heatGeneratorHeatingField = value;
            }
        }

        /// <remarks/>
        public energySourceType energySourceHeating
        {
            get
            {
                return this.energySourceHeatingField;
            }
            set
            {
                this.energySourceHeatingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool energySourceHeatingSpecified
        {
            get
            {
                return this.energySourceHeatingFieldSpecified;
            }
            set
            {
                this.energySourceHeatingFieldSpecified = value;
            }
        }

        /// <remarks/>
        public informationSourceType informationSourceHeating
        {
            get
            {
                return this.informationSourceHeatingField;
            }
            set
            {
                this.informationSourceHeatingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool informationSourceHeatingSpecified
        {
            get
            {
                return this.informationSourceHeatingFieldSpecified;
            }
            set
            {
                this.informationSourceHeatingFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime revisionDate
        {
            get
            {
                return this.revisionDateField;
            }
            set
            {
                this.revisionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool revisionDateSpecified
        {
            get
            {
                return this.revisionDateFieldSpecified;
            }
            set
            {
                this.revisionDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum heatGeneratorHeatingType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7400")]
        Item7400,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7410")]
        Item7410,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7411")]
        Item7411,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7420")]
        Item7420,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7421")]
        Item7421,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7430")]
        Item7430,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7431")]
        Item7431,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7432")]
        Item7432,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7433")]
        Item7433,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7434")]
        Item7434,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7435")]
        Item7435,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7436")]
        Item7436,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7440")]
        Item7440,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7441")]
        Item7441,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7450")]
        Item7450,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7451")]
        Item7451,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7452")]
        Item7452,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7460")]
        Item7460,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7461")]
        Item7461,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7499")]
        Item7499,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum energySourceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7500")]
        Item7500,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7501")]
        Item7501,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7510")]
        Item7510,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7511")]
        Item7511,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7512")]
        Item7512,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7513")]
        Item7513,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7520")]
        Item7520,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7530")]
        Item7530,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7540")]
        Item7540,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7541")]
        Item7541,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7542")]
        Item7542,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7543")]
        Item7543,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7550")]
        Item7550,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7560")]
        Item7560,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7570")]
        Item7570,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7580")]
        Item7580,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7581")]
        Item7581,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7582")]
        Item7582,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7598")]
        Item7598,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7599")]
        Item7599,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum informationSourceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("852")]
        Item852,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("853")]
        Item853,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("855")]
        Item855,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("857")]
        Item857,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("858")]
        Item858,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("859")]
        Item859,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("860")]
        Item860,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("864")]
        Item864,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("865")]
        Item865,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("869")]
        Item869,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("870")]
        Item870,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("871")]
        Item871,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class hotWaterType
    {

        private heatGeneratorHotWaterType heatGeneratorHotWaterField;

        private energySourceType energySourceHeatingField;

        private bool energySourceHeatingFieldSpecified;

        private informationSourceType informationSourceHeatingField;

        private bool informationSourceHeatingFieldSpecified;

        private System.DateTime revisionDateField;

        private bool revisionDateFieldSpecified;

        /// <remarks/>
        public heatGeneratorHotWaterType heatGeneratorHotWater
        {
            get
            {
                return this.heatGeneratorHotWaterField;
            }
            set
            {
                this.heatGeneratorHotWaterField = value;
            }
        }

        /// <remarks/>
        public energySourceType energySourceHeating
        {
            get
            {
                return this.energySourceHeatingField;
            }
            set
            {
                this.energySourceHeatingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool energySourceHeatingSpecified
        {
            get
            {
                return this.energySourceHeatingFieldSpecified;
            }
            set
            {
                this.energySourceHeatingFieldSpecified = value;
            }
        }

        /// <remarks/>
        public informationSourceType informationSourceHeating
        {
            get
            {
                return this.informationSourceHeatingField;
            }
            set
            {
                this.informationSourceHeatingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool informationSourceHeatingSpecified
        {
            get
            {
                return this.informationSourceHeatingFieldSpecified;
            }
            set
            {
                this.informationSourceHeatingFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime revisionDate
        {
            get
            {
                return this.revisionDateField;
            }
            set
            {
                this.revisionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool revisionDateSpecified
        {
            get
            {
                return this.revisionDateFieldSpecified;
            }
            set
            {
                this.revisionDateFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum heatGeneratorHotWaterType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7600")]
        Item7600,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7610")]
        Item7610,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7620")]
        Item7620,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7630")]
        Item7630,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7632")]
        Item7632,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7634")]
        Item7634,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7640")]
        Item7640,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7650")]
        Item7650,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7651")]
        Item7651,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7660")]
        Item7660,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7699")]
        Item7699,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("buildingOnly", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class buildingOnlyType : buildingType1
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "constructionProjectType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    [System.Xml.Serialization.XmlRootAttribute("constructionProject", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5", IsNullable = false)]
    public partial class constructionProjectType1
    {

        private constructionProjectIdentificationType constructionProjectIdentificationField;

        private typeOfConstructionProjectType typeOfConstructionProjectField;

        private bool typeOfConstructionProjectFieldSpecified;

        private constructionLocalisationType1 constructionLocalisationField;

        private typeOfPermitType typeOfPermitField;

        private bool typeOfPermitFieldSpecified;

        private System.DateTime buildingPermitIssueDateField;

        private bool buildingPermitIssueDateFieldSpecified;

        private System.DateTime projectAnnouncementDateField;

        private bool projectAnnouncementDateFieldSpecified;

        private System.DateTime constructionAuthorisationDeniedDateField;

        private bool constructionAuthorisationDeniedDateFieldSpecified;

        private System.DateTime projectStartDateField;

        private bool projectStartDateFieldSpecified;

        private System.DateTime projectCompletionDateField;

        private bool projectCompletionDateFieldSpecified;

        private System.DateTime projectSuspensionDateField;

        private bool projectSuspensionDateFieldSpecified;

        private System.DateTime withdrawalDateField;

        private bool withdrawalDateFieldSpecified;

        private System.DateTime nonRealisationDateField;

        private bool nonRealisationDateFieldSpecified;

        private string totalCostsOfProjectField;

        private projectStatusType statusField;

        private typeOfClientType typeOfClientField;

        private bool typeOfClientFieldSpecified;

        private typeOfConstructionType typeOfConstructionField;

        private bool typeOfConstructionFieldSpecified;

        private string descriptionField;

        private string durationOfConstructionPhaseField;

        private string numberOfConcernedBuildingsField;

        private string numberOfConcernedDwellingsField;

        private string[] projectFreeTextField;

        private swissAndFlMunicipalityType[] municipalityField;

        /// <remarks/>
        public constructionProjectIdentificationType constructionProjectIdentification
        {
            get
            {
                return this.constructionProjectIdentificationField;
            }
            set
            {
                this.constructionProjectIdentificationField = value;
            }
        }

        /// <remarks/>
        public typeOfConstructionProjectType typeOfConstructionProject
        {
            get
            {
                return this.typeOfConstructionProjectField;
            }
            set
            {
                this.typeOfConstructionProjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfConstructionProjectSpecified
        {
            get
            {
                return this.typeOfConstructionProjectFieldSpecified;
            }
            set
            {
                this.typeOfConstructionProjectFieldSpecified = value;
            }
        }

        /// <remarks/>
        public constructionLocalisationType1 constructionLocalisation
        {
            get
            {
                return this.constructionLocalisationField;
            }
            set
            {
                this.constructionLocalisationField = value;
            }
        }

        /// <remarks/>
        public typeOfPermitType typeOfPermit
        {
            get
            {
                return this.typeOfPermitField;
            }
            set
            {
                this.typeOfPermitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfPermitSpecified
        {
            get
            {
                return this.typeOfPermitFieldSpecified;
            }
            set
            {
                this.typeOfPermitFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime buildingPermitIssueDate
        {
            get
            {
                return this.buildingPermitIssueDateField;
            }
            set
            {
                this.buildingPermitIssueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool buildingPermitIssueDateSpecified
        {
            get
            {
                return this.buildingPermitIssueDateFieldSpecified;
            }
            set
            {
                this.buildingPermitIssueDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectAnnouncementDate
        {
            get
            {
                return this.projectAnnouncementDateField;
            }
            set
            {
                this.projectAnnouncementDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectAnnouncementDateSpecified
        {
            get
            {
                return this.projectAnnouncementDateFieldSpecified;
            }
            set
            {
                this.projectAnnouncementDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime constructionAuthorisationDeniedDate
        {
            get
            {
                return this.constructionAuthorisationDeniedDateField;
            }
            set
            {
                this.constructionAuthorisationDeniedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool constructionAuthorisationDeniedDateSpecified
        {
            get
            {
                return this.constructionAuthorisationDeniedDateFieldSpecified;
            }
            set
            {
                this.constructionAuthorisationDeniedDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectStartDate
        {
            get
            {
                return this.projectStartDateField;
            }
            set
            {
                this.projectStartDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectStartDateSpecified
        {
            get
            {
                return this.projectStartDateFieldSpecified;
            }
            set
            {
                this.projectStartDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectCompletionDate
        {
            get
            {
                return this.projectCompletionDateField;
            }
            set
            {
                this.projectCompletionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectCompletionDateSpecified
        {
            get
            {
                return this.projectCompletionDateFieldSpecified;
            }
            set
            {
                this.projectCompletionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectSuspensionDate
        {
            get
            {
                return this.projectSuspensionDateField;
            }
            set
            {
                this.projectSuspensionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectSuspensionDateSpecified
        {
            get
            {
                return this.projectSuspensionDateFieldSpecified;
            }
            set
            {
                this.projectSuspensionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime withdrawalDate
        {
            get
            {
                return this.withdrawalDateField;
            }
            set
            {
                this.withdrawalDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool withdrawalDateSpecified
        {
            get
            {
                return this.withdrawalDateFieldSpecified;
            }
            set
            {
                this.withdrawalDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime nonRealisationDate
        {
            get
            {
                return this.nonRealisationDateField;
            }
            set
            {
                this.nonRealisationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool nonRealisationDateSpecified
        {
            get
            {
                return this.nonRealisationDateFieldSpecified;
            }
            set
            {
                this.nonRealisationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string totalCostsOfProject
        {
            get
            {
                return this.totalCostsOfProjectField;
            }
            set
            {
                this.totalCostsOfProjectField = value;
            }
        }

        /// <remarks/>
        public projectStatusType status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public typeOfClientType typeOfClient
        {
            get
            {
                return this.typeOfClientField;
            }
            set
            {
                this.typeOfClientField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfClientSpecified
        {
            get
            {
                return this.typeOfClientFieldSpecified;
            }
            set
            {
                this.typeOfClientFieldSpecified = value;
            }
        }

        /// <remarks/>
        public typeOfConstructionType typeOfConstruction
        {
            get
            {
                return this.typeOfConstructionField;
            }
            set
            {
                this.typeOfConstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfConstructionSpecified
        {
            get
            {
                return this.typeOfConstructionFieldSpecified;
            }
            set
            {
                this.typeOfConstructionFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string durationOfConstructionPhase
        {
            get
            {
                return this.durationOfConstructionPhaseField;
            }
            set
            {
                this.durationOfConstructionPhaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfConcernedBuildings
        {
            get
            {
                return this.numberOfConcernedBuildingsField;
            }
            set
            {
                this.numberOfConcernedBuildingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfConcernedDwellings
        {
            get
            {
                return this.numberOfConcernedDwellingsField;
            }
            set
            {
                this.numberOfConcernedDwellingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("projectFreeText", DataType = "token")]
        public string[] projectFreeText
        {
            get
            {
                return this.projectFreeTextField;
            }
            set
            {
                this.projectFreeTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("municipality")]
        public swissAndFlMunicipalityType[] municipality
        {
            get
            {
                return this.municipalityField;
            }
            set
            {
                this.municipalityField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class constructionProjectIdentificationType
    {

        private namedIdType[] localIDField;

        private string ePROIDField;

        private string officialConstructionProjectFileNoField;

        private string extensionOfOfficialConstructionProjectFileNoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("localID")]
        public namedIdType[] localID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EPROID
        {
            get
            {
                return this.ePROIDField;
            }
            set
            {
                this.ePROIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string officialConstructionProjectFileNo
        {
            get
            {
                return this.officialConstructionProjectFileNoField;
            }
            set
            {
                this.officialConstructionProjectFileNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string extensionOfOfficialConstructionProjectFileNo
        {
            get
            {
                return this.extensionOfOfficialConstructionProjectFileNoField;
            }
            set
            {
                this.extensionOfOfficialConstructionProjectFileNoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum typeOfConstructionProjectType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6010")]
        Item6010,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6011")]
        Item6011,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "constructionLocalisationType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class constructionLocalisationType1
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("canton", typeof(cantonAbbreviationType))]
        [System.Xml.Serialization.XmlElementAttribute("country", typeof(countryType1))]
        [System.Xml.Serialization.XmlElementAttribute("municipality", typeof(swissMunicipalityType1))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0007/6")]
    public enum cantonAbbreviationType
    {

        /// <remarks/>
        ZH,

        /// <remarks/>
        BE,

        /// <remarks/>
        LU,

        /// <remarks/>
        UR,

        /// <remarks/>
        SZ,

        /// <remarks/>
        OW,

        /// <remarks/>
        NW,

        /// <remarks/>
        GL,

        /// <remarks/>
        ZG,

        /// <remarks/>
        FR,

        /// <remarks/>
        SO,

        /// <remarks/>
        BS,

        /// <remarks/>
        BL,

        /// <remarks/>
        SH,

        /// <remarks/>
        AR,

        /// <remarks/>
        AI,

        /// <remarks/>
        SG,

        /// <remarks/>
        GR,

        /// <remarks/>
        AG,

        /// <remarks/>
        TG,

        /// <remarks/>
        TI,

        /// <remarks/>
        VD,

        /// <remarks/>
        VS,

        /// <remarks/>
        NE,

        /// <remarks/>
        GE,

        /// <remarks/>
        JU,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "countryType", Namespace = "http://www.ech.ch/xmlns/eCH-0008/3")]
    public partial class countryType1
    {

        private string countryIdField;

        private string countryIdISO2Field;

        private string countryNameShortField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string countryId
        {
            get
            {
                return this.countryIdField;
            }
            set
            {
                this.countryIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string countryIdISO2
        {
            get
            {
                return this.countryIdISO2Field;
            }
            set
            {
                this.countryIdISO2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string countryNameShort
        {
            get
            {
                return this.countryNameShortField;
            }
            set
            {
                this.countryNameShortField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "swissMunicipalityType", Namespace = "http://www.ech.ch/xmlns/eCH-0007/6")]
    public partial class swissMunicipalityType1
    {

        private int municipalityIdField;

        private bool municipalityIdFieldSpecified;

        private string municipalityNameField;

        private cantonAbbreviationType cantonAbbreviationField;

        private bool cantonAbbreviationFieldSpecified;

        private int historyMunicipalityIdField;

        private bool historyMunicipalityIdFieldSpecified;

        /// <remarks/>
        public int municipalityId
        {
            get
            {
                return this.municipalityIdField;
            }
            set
            {
                this.municipalityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool municipalityIdSpecified
        {
            get
            {
                return this.municipalityIdFieldSpecified;
            }
            set
            {
                this.municipalityIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string municipalityName
        {
            get
            {
                return this.municipalityNameField;
            }
            set
            {
                this.municipalityNameField = value;
            }
        }

        /// <remarks/>
        public cantonAbbreviationType cantonAbbreviation
        {
            get
            {
                return this.cantonAbbreviationField;
            }
            set
            {
                this.cantonAbbreviationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cantonAbbreviationSpecified
        {
            get
            {
                return this.cantonAbbreviationFieldSpecified;
            }
            set
            {
                this.cantonAbbreviationFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int historyMunicipalityId
        {
            get
            {
                return this.historyMunicipalityIdField;
            }
            set
            {
                this.historyMunicipalityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool historyMunicipalityIdSpecified
        {
            get
            {
                return this.historyMunicipalityIdFieldSpecified;
            }
            set
            {
                this.historyMunicipalityIdFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum typeOfPermitType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5000")]
        Item5000,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5001")]
        Item5001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5002")]
        Item5002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5003")]
        Item5003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5004")]
        Item5004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5005")]
        Item5005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5006")]
        Item5006,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5007")]
        Item5007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5008")]
        Item5008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5009")]
        Item5009,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5011")]
        Item5011,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5012")]
        Item5012,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5015")]
        Item5015,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5021")]
        Item5021,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5022")]
        Item5022,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5023")]
        Item5023,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5031")]
        Item5031,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5041")]
        Item5041,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5043")]
        Item5043,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5044")]
        Item5044,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5051")]
        Item5051,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5061")]
        Item5061,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5062")]
        Item5062,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5063")]
        Item5063,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5064")]
        Item5064,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5071")]
        Item5071,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum projectStatusType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6701")]
        Item6701,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6702")]
        Item6702,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6703")]
        Item6703,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6704")]
        Item6704,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6706")]
        Item6706,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6707")]
        Item6707,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6708")]
        Item6708,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6709")]
        Item6709,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum typeOfClientType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6101")]
        Item6101,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6103")]
        Item6103,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6104")]
        Item6104,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6107")]
        Item6107,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6108")]
        Item6108,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6110")]
        Item6110,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6111")]
        Item6111,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6115")]
        Item6115,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6116")]
        Item6116,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6121")]
        Item6121,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6122")]
        Item6122,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6123")]
        Item6123,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6124")]
        Item6124,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6131")]
        Item6131,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6132")]
        Item6132,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6133")]
        Item6133,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6141")]
        Item6141,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6142")]
        Item6142,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6143")]
        Item6143,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6161")]
        Item6161,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6151")]
        Item6151,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6152")]
        Item6152,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6162")]
        Item6162,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6163")]
        Item6163,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public enum typeOfConstructionType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6211")]
        Item6211,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6212")]
        Item6212,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6213")]
        Item6213,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6214")]
        Item6214,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6219")]
        Item6219,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6221")]
        Item6221,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6222")]
        Item6222,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6223")]
        Item6223,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6231")]
        Item6231,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6232")]
        Item6232,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6233")]
        Item6233,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6234")]
        Item6234,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6235")]
        Item6235,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6241")]
        Item6241,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6242")]
        Item6242,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6243")]
        Item6243,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6244")]
        Item6244,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6245")]
        Item6245,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6249")]
        Item6249,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6251")]
        Item6251,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6252")]
        Item6252,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6253")]
        Item6253,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6254")]
        Item6254,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6255")]
        Item6255,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6256")]
        Item6256,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6257")]
        Item6257,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6258")]
        Item6258,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6259")]
        Item6259,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6261")]
        Item6261,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6262")]
        Item6262,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6269")]
        Item6269,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6271")]
        Item6271,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6272")]
        Item6272,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6273")]
        Item6273,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6274")]
        Item6274,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6276")]
        Item6276,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6278")]
        Item6278,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6279")]
        Item6279,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6281")]
        Item6281,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6282")]
        Item6282,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6283")]
        Item6283,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6291")]
        Item6291,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6292")]
        Item6292,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6293")]
        Item6293,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6294")]
        Item6294,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6295")]
        Item6295,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6296")]
        Item6296,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6299")]
        Item6299,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0007/6")]
    public partial class swissAndFlMunicipalityType
    {

        private int municipalityIdField;

        private string municipalityNameField;

        private cantonFlAbbreviationType cantonFlAbbreviationField;

        private int historyMunicipalityIdField;

        private bool historyMunicipalityIdFieldSpecified;

        /// <remarks/>
        public int municipalityId
        {
            get
            {
                return this.municipalityIdField;
            }
            set
            {
                this.municipalityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string municipalityName
        {
            get
            {
                return this.municipalityNameField;
            }
            set
            {
                this.municipalityNameField = value;
            }
        }

        /// <remarks/>
        public cantonFlAbbreviationType cantonFlAbbreviation
        {
            get
            {
                return this.cantonFlAbbreviationField;
            }
            set
            {
                this.cantonFlAbbreviationField = value;
            }
        }

        /// <remarks/>
        public int historyMunicipalityId
        {
            get
            {
                return this.historyMunicipalityIdField;
            }
            set
            {
                this.historyMunicipalityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool historyMunicipalityIdSpecified
        {
            get
            {
                return this.historyMunicipalityIdFieldSpecified;
            }
            set
            {
                this.historyMunicipalityIdFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0007/6")]
    public enum cantonFlAbbreviationType
    {

        /// <remarks/>
        ZH,

        /// <remarks/>
        BE,

        /// <remarks/>
        LU,

        /// <remarks/>
        UR,

        /// <remarks/>
        SZ,

        /// <remarks/>
        OW,

        /// <remarks/>
        NW,

        /// <remarks/>
        GL,

        /// <remarks/>
        ZG,

        /// <remarks/>
        FR,

        /// <remarks/>
        SO,

        /// <remarks/>
        BS,

        /// <remarks/>
        BL,

        /// <remarks/>
        SH,

        /// <remarks/>
        AR,

        /// <remarks/>
        AI,

        /// <remarks/>
        SG,

        /// <remarks/>
        GR,

        /// <remarks/>
        AG,

        /// <remarks/>
        TG,

        /// <remarks/>
        TI,

        /// <remarks/>
        VD,

        /// <remarks/>
        VS,

        /// <remarks/>
        NE,

        /// <remarks/>
        GE,

        /// <remarks/>
        JU,

        /// <remarks/>
        FL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "personIdentificationType", Namespace = "http://www.ech.ch/xmlns/eCH-0129/5")]
    public partial class personIdentificationType1
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("individual", typeof(personIdentificationLightType))]
        [System.Xml.Serialization.XmlElementAttribute("organisation", typeof(organisationIdentificationType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    public partial class infoType
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("negativeReport", typeof(infoTypeNegativeReport))]
        [System.Xml.Serialization.XmlElementAttribute("positiveReport", typeof(infoTypePositiveReport))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    public partial class infoTypeNegativeReport
    {

        private object noticeField;

        private object dataField;

        /// <remarks/>
        public object notice
        {
            get
            {
                return this.noticeField;
            }
            set
            {
                this.noticeField = value;
            }
        }

        /// <remarks/>
        public object data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    public partial class infoTypePositiveReport
    {

        private object noticeField;

        private object dataField;

        /// <remarks/>
        public object notice
        {
            get
            {
                return this.noticeField;
            }
            set
            {
                this.noticeField = value;
            }
        }

        /// <remarks/>
        public object data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    public partial class partialDeliveryType
    {

        private string uniqueIdDeliveryField;

        private string totalNumberOfPackagesField;

        private string numberOfActualPackageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string uniqueIdDelivery
        {
            get
            {
                return this.uniqueIdDeliveryField;
            }
            set
            {
                this.uniqueIdDeliveryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string totalNumberOfPackages
        {
            get
            {
                return this.totalNumberOfPackagesField;
            }
            set
            {
                this.totalNumberOfPackagesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfActualPackage
        {
            get
            {
                return this.numberOfActualPackageField;
            }
            set
            {
                this.numberOfActualPackageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    [System.Xml.Serialization.XmlRootAttribute("header", Namespace = "http://www.ech.ch/xmlns/eCH-0058/5", IsNullable = false)]
    public partial class headerType
    {

        private string senderIdField;

        private string originalSenderIdField;

        private string declarationLocalReferenceField;

        private string[] recipientIdField;

        private string messageIdField;

        private string referenceMessageIdField;

        private string businessProcessIdField;

        private string ourBusinessReferenceIdField;

        private string yourBusinessReferenceIdField;

        private string uniqueIdBusinessTransactionField;

        private string messageTypeField;

        private string subMessageTypeField;

        private sendingApplicationType sendingApplicationField;

        private partialDeliveryType partialDeliveryField;

        private string subjectField;

        private string commentField;

        private System.DateTime messageDateField;

        private System.DateTime initialMessageDateField;

        private bool initialMessageDateFieldSpecified;

        private System.DateTime eventDateField;

        private bool eventDateFieldSpecified;

        private System.DateTime modificationDateField;

        private bool modificationDateFieldSpecified;

        private actionType actionField;

        private object[] attachmentField;

        private bool testDeliveryFlagField;

        private bool responseExpectedField;

        private bool responseExpectedFieldSpecified;

        private bool businessCaseClosedField;

        private bool businessCaseClosedFieldSpecified;

        private namedMetaDataType[] namedMetaDataField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string senderId
        {
            get
            {
                return this.senderIdField;
            }
            set
            {
                this.senderIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string originalSenderId
        {
            get
            {
                return this.originalSenderIdField;
            }
            set
            {
                this.originalSenderIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string declarationLocalReference
        {
            get
            {
                return this.declarationLocalReferenceField;
            }
            set
            {
                this.declarationLocalReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("recipientId", DataType = "anyURI")]
        public string[] recipientId
        {
            get
            {
                return this.recipientIdField;
            }
            set
            {
                this.recipientIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string messageId
        {
            get
            {
                return this.messageIdField;
            }
            set
            {
                this.messageIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string referenceMessageId
        {
            get
            {
                return this.referenceMessageIdField;
            }
            set
            {
                this.referenceMessageIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string businessProcessId
        {
            get
            {
                return this.businessProcessIdField;
            }
            set
            {
                this.businessProcessIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string ourBusinessReferenceId
        {
            get
            {
                return this.ourBusinessReferenceIdField;
            }
            set
            {
                this.ourBusinessReferenceIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string yourBusinessReferenceId
        {
            get
            {
                return this.yourBusinessReferenceIdField;
            }
            set
            {
                this.yourBusinessReferenceIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string uniqueIdBusinessTransaction
        {
            get
            {
                return this.uniqueIdBusinessTransactionField;
            }
            set
            {
                this.uniqueIdBusinessTransactionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string messageType
        {
            get
            {
                return this.messageTypeField;
            }
            set
            {
                this.messageTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string subMessageType
        {
            get
            {
                return this.subMessageTypeField;
            }
            set
            {
                this.subMessageTypeField = value;
            }
        }

        /// <remarks/>
        public sendingApplicationType sendingApplication
        {
            get
            {
                return this.sendingApplicationField;
            }
            set
            {
                this.sendingApplicationField = value;
            }
        }

        /// <remarks/>
        public partialDeliveryType partialDelivery
        {
            get
            {
                return this.partialDeliveryField;
            }
            set
            {
                this.partialDeliveryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }

        /// <remarks/>
        public System.DateTime messageDate
        {
            get
            {
                return this.messageDateField;
            }
            set
            {
                this.messageDateField = value;
            }
        }

        /// <remarks/>
        public System.DateTime initialMessageDate
        {
            get
            {
                return this.initialMessageDateField;
            }
            set
            {
                this.initialMessageDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool initialMessageDateSpecified
        {
            get
            {
                return this.initialMessageDateFieldSpecified;
            }
            set
            {
                this.initialMessageDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime eventDate
        {
            get
            {
                return this.eventDateField;
            }
            set
            {
                this.eventDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool eventDateSpecified
        {
            get
            {
                return this.eventDateFieldSpecified;
            }
            set
            {
                this.eventDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime modificationDate
        {
            get
            {
                return this.modificationDateField;
            }
            set
            {
                this.modificationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool modificationDateSpecified
        {
            get
            {
                return this.modificationDateFieldSpecified;
            }
            set
            {
                this.modificationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public actionType action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attachment")]
        public object[] attachment
        {
            get
            {
                return this.attachmentField;
            }
            set
            {
                this.attachmentField = value;
            }
        }

        /// <remarks/>
        public bool testDeliveryFlag
        {
            get
            {
                return this.testDeliveryFlagField;
            }
            set
            {
                this.testDeliveryFlagField = value;
            }
        }

        /// <remarks/>
        public bool responseExpected
        {
            get
            {
                return this.responseExpectedField;
            }
            set
            {
                this.responseExpectedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool responseExpectedSpecified
        {
            get
            {
                return this.responseExpectedFieldSpecified;
            }
            set
            {
                this.responseExpectedFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool businessCaseClosed
        {
            get
            {
                return this.businessCaseClosedField;
            }
            set
            {
                this.businessCaseClosedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool businessCaseClosedSpecified
        {
            get
            {
                return this.businessCaseClosedFieldSpecified;
            }
            set
            {
                this.businessCaseClosedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("namedMetaData")]
        public namedMetaDataType[] namedMetaData
        {
            get
            {
                return this.namedMetaDataField;
            }
            set
            {
                this.namedMetaDataField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    public enum actionType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        Item8,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    public partial class namedMetaDataType
    {

        private string metaDataNameField;

        private string metaDataValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string metaDataName
        {
            get
            {
                return this.metaDataNameField;
            }
            set
            {
                this.metaDataNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string metaDataValue
        {
            get
            {
                return this.metaDataValueField;
            }
            set
            {
                this.metaDataValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4")]
    public partial class personIdentificationKeyOnlyType
    {

        private ulong vnField;

        private bool vnFieldSpecified;

        private namedPersonIdType localPersonIdField;

        private namedPersonIdType[] otherPersonIdField;

        private namedPersonIdType[] euPersonIdField;

        /// <remarks/>
        public ulong vn
        {
            get
            {
                return this.vnField;
            }
            set
            {
                this.vnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool vnSpecified
        {
            get
            {
                return this.vnFieldSpecified;
            }
            set
            {
                this.vnFieldSpecified = value;
            }
        }

        /// <remarks/>
        public namedPersonIdType localPersonId
        {
            get
            {
                return this.localPersonIdField;
            }
            set
            {
                this.localPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("otherPersonId")]
        public namedPersonIdType[] otherPersonId
        {
            get
            {
                return this.otherPersonIdField;
            }
            set
            {
                this.otherPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("euPersonId")]
        public namedPersonIdType[] euPersonId
        {
            get
            {
                return this.euPersonIdField;
            }
            set
            {
                this.euPersonIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4")]
    public partial class personIdentificationType
    {

        private ulong vnField;

        private bool vnFieldSpecified;

        private namedPersonIdType localPersonIdField;

        private namedPersonIdType[] otherPersonIdField;

        private namedPersonIdType[] euPersonIdField;

        private string officialNameField;

        private string firstNameField;

        private string originalNameField;

        private sexType sexField;

        private datePartiallyKnownType dateOfBirthField;

        /// <remarks/>
        public ulong vn
        {
            get
            {
                return this.vnField;
            }
            set
            {
                this.vnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool vnSpecified
        {
            get
            {
                return this.vnFieldSpecified;
            }
            set
            {
                this.vnFieldSpecified = value;
            }
        }

        /// <remarks/>
        public namedPersonIdType localPersonId
        {
            get
            {
                return this.localPersonIdField;
            }
            set
            {
                this.localPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("otherPersonId")]
        public namedPersonIdType[] otherPersonId
        {
            get
            {
                return this.otherPersonIdField;
            }
            set
            {
                this.otherPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("euPersonId")]
        public namedPersonIdType[] euPersonId
        {
            get
            {
                return this.euPersonIdField;
            }
            set
            {
                this.euPersonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string officialName
        {
            get
            {
                return this.officialNameField;
            }
            set
            {
                this.officialNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string originalName
        {
            get
            {
                return this.originalNameField;
            }
            set
            {
                this.originalNameField = value;
            }
        }

        /// <remarks/>
        public sexType sex
        {
            get
            {
                return this.sexField;
            }
            set
            {
                this.sexField = value;
            }
        }

        /// <remarks/>
        public datePartiallyKnownType dateOfBirth
        {
            get
            {
                return this.dateOfBirthField;
            }
            set
            {
                this.dateOfBirthField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    public partial class swissAddressInformationType
    {

        private string addressLine1Field;

        private string addressLine2Field;

        private string streetField;

        private string houseNumberField;

        private string dwellingNumberField;

        private string localityField;

        private string townField;

        private uint swissZipCodeField;

        private string swissZipCodeAddOnField;

        private int swissZipCodeIdField;

        private bool swissZipCodeIdFieldSpecified;

        private countryType countryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string addressLine1
        {
            get
            {
                return this.addressLine1Field;
            }
            set
            {
                this.addressLine1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string addressLine2
        {
            get
            {
                return this.addressLine2Field;
            }
            set
            {
                this.addressLine2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string houseNumber
        {
            get
            {
                return this.houseNumberField;
            }
            set
            {
                this.houseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string dwellingNumber
        {
            get
            {
                return this.dwellingNumberField;
            }
            set
            {
                this.dwellingNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string locality
        {
            get
            {
                return this.localityField;
            }
            set
            {
                this.localityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string town
        {
            get
            {
                return this.townField;
            }
            set
            {
                this.townField = value;
            }
        }

        /// <remarks/>
        public uint swissZipCode
        {
            get
            {
                return this.swissZipCodeField;
            }
            set
            {
                this.swissZipCodeField = value;
            }
        }

        /// <remarks/>
        public string swissZipCodeAddOn
        {
            get
            {
                return this.swissZipCodeAddOnField;
            }
            set
            {
                this.swissZipCodeAddOnField = value;
            }
        }

        /// <remarks/>
        public int swissZipCodeId
        {
            get
            {
                return this.swissZipCodeIdField;
            }
            set
            {
                this.swissZipCodeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool swissZipCodeIdSpecified
        {
            get
            {
                return this.swissZipCodeIdFieldSpecified;
            }
            set
            {
                this.swissZipCodeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public countryType country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    [System.Xml.Serialization.XmlRootAttribute("organisationMailAdress", Namespace = "http://www.ech.ch/xmlns/eCH-0010/6", IsNullable = false)]
    public partial class organisationMailAddressType
    {

        private organisationMailAddressInfoType organisationField;

        private addressInformationType addressInformationField;

        /// <remarks/>
        public organisationMailAddressInfoType organisation
        {
            get
            {
                return this.organisationField;
            }
            set
            {
                this.organisationField = value;
            }
        }

        /// <remarks/>
        public addressInformationType addressInformation
        {
            get
            {
                return this.addressInformationField;
            }
            set
            {
                this.addressInformationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    public partial class organisationMailAddressInfoType
    {

        private string organisationNameField;

        private string organisationNameAddOn1Field;

        private string organisationNameAddOn2Field;

        private mrMrsType mrMrsField;

        private bool mrMrsFieldSpecified;

        private string titleField;

        private string firstNameField;

        private string lastNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationName
        {
            get
            {
                return this.organisationNameField;
            }
            set
            {
                this.organisationNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationNameAddOn1
        {
            get
            {
                return this.organisationNameAddOn1Field;
            }
            set
            {
                this.organisationNameAddOn1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string organisationNameAddOn2
        {
            get
            {
                return this.organisationNameAddOn2Field;
            }
            set
            {
                this.organisationNameAddOn2Field = value;
            }
        }

        /// <remarks/>
        public mrMrsType mrMrs
        {
            get
            {
                return this.mrMrsField;
            }
            set
            {
                this.mrMrsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool mrMrsSpecified
        {
            get
            {
                return this.mrMrsFieldSpecified;
            }
            set
            {
                this.mrMrsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string lastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    public enum mrMrsType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    [System.Xml.Serialization.XmlRootAttribute("personMailAddress", Namespace = "http://www.ech.ch/xmlns/eCH-0010/6", IsNullable = false)]
    public partial class personMailAddressType
    {

        private personMailAddressInfoType personField;

        private addressInformationType addressInformationField;

        /// <remarks/>
        public personMailAddressInfoType person
        {
            get
            {
                return this.personField;
            }
            set
            {
                this.personField = value;
            }
        }

        /// <remarks/>
        public addressInformationType addressInformation
        {
            get
            {
                return this.addressInformationField;
            }
            set
            {
                this.addressInformationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    public partial class personMailAddressInfoType
    {

        private mrMrsType mrMrsField;

        private bool mrMrsFieldSpecified;

        private string titleField;

        private string firstNameField;

        private string lastNameField;

        /// <remarks/>
        public mrMrsType mrMrs
        {
            get
            {
                return this.mrMrsField;
            }
            set
            {
                this.mrMrsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool mrMrsSpecified
        {
            get
            {
                return this.mrMrsFieldSpecified;
            }
            set
            {
                this.mrMrsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string lastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0010/6")]
    [System.Xml.Serialization.XmlRootAttribute("mailAddress", Namespace = "http://www.ech.ch/xmlns/eCH-0010/6", IsNullable = false)]
    public partial class mailAddressType
    {

        private object itemField;

        private addressInformationType addressInformationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("organisation", typeof(organisationMailAddressInfoType))]
        [System.Xml.Serialization.XmlElementAttribute("person", typeof(personMailAddressInfoType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        public addressInformationType addressInformation
        {
            get
            {
                return this.addressInformationField;
            }
            set
            {
                this.addressInformationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class constructionLocalisationType
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("municipality", typeof(swissMunicipalityType))]
        [System.Xml.Serialization.XmlElementAttribute("other", typeof(object))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class swissMunicipalityType
    {

        private int municipalityIdField;

        private string municipalityNameField;

        private cantonAbbreviationType cantonAbbreviationField;

        /// <remarks/>
        public int municipalityId
        {
            get
            {
                return this.municipalityIdField;
            }
            set
            {
                this.municipalityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string municipalityName
        {
            get
            {
                return this.municipalityNameField;
            }
            set
            {
                this.municipalityNameField = value;
            }
        }

        /// <remarks/>
        public cantonAbbreviationType cantonAbbreviation
        {
            get
            {
                return this.cantonAbbreviationField;
            }
            set
            {
                this.cantonAbbreviationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class constructionProjectType
    {

        private string officialConstructionProjectFileNoField;

        private string extensionOfOfficialConstructionProjectFileNoField;

        private string constructionProjectDescriptionField;

        private constructionLocalisationType constructionLocalisationField;

        private typeOfPermitType typeOfPermitField;

        private bool typeOfPermitFieldSpecified;

        private typeOfClientType typeOfClientField;

        private bool typeOfClientFieldSpecified;

        private typeOfConstructionProjectType typeOfConstructionProjectField;

        private bool typeOfConstructionProjectFieldSpecified;

        private typeOfConstructionType typeOfConstructionField;

        private bool typeOfConstructionFieldSpecified;

        private string totalCostsOfProjectField;

        private System.DateTime projectAnnouncementDateField;

        private bool projectAnnouncementDateFieldSpecified;

        private System.DateTime buildingPermitIssueDateField;

        private bool buildingPermitIssueDateFieldSpecified;

        private System.DateTime projectStartDateField;

        private bool projectStartDateFieldSpecified;

        private System.DateTime projectCompletionDateField;

        private bool projectCompletionDateFieldSpecified;

        private System.DateTime projectSuspensionDateField;

        private bool projectSuspensionDateFieldSpecified;

        private System.DateTime constructionAuthorisationDeniedDateField;

        private bool constructionAuthorisationDeniedDateFieldSpecified;

        private System.DateTime nonRealisationDateField;

        private bool nonRealisationDateFieldSpecified;

        private System.DateTime withdrawalDateField;

        private bool withdrawalDateFieldSpecified;

        private string durationOfConstructionPhaseField;

        private projectStatusType projectStatusField;

        private bool projectStatusFieldSpecified;

        private string numberOfConcernedBuildingsField;

        private string numberOfConcernedDwellingsField;

        private object extensionField;

        private recordModificationType recordModificationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string officialConstructionProjectFileNo
        {
            get
            {
                return this.officialConstructionProjectFileNoField;
            }
            set
            {
                this.officialConstructionProjectFileNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string extensionOfOfficialConstructionProjectFileNo
        {
            get
            {
                return this.extensionOfOfficialConstructionProjectFileNoField;
            }
            set
            {
                this.extensionOfOfficialConstructionProjectFileNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string constructionProjectDescription
        {
            get
            {
                return this.constructionProjectDescriptionField;
            }
            set
            {
                this.constructionProjectDescriptionField = value;
            }
        }

        /// <remarks/>
        public constructionLocalisationType constructionLocalisation
        {
            get
            {
                return this.constructionLocalisationField;
            }
            set
            {
                this.constructionLocalisationField = value;
            }
        }

        /// <remarks/>
        public typeOfPermitType typeOfPermit
        {
            get
            {
                return this.typeOfPermitField;
            }
            set
            {
                this.typeOfPermitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfPermitSpecified
        {
            get
            {
                return this.typeOfPermitFieldSpecified;
            }
            set
            {
                this.typeOfPermitFieldSpecified = value;
            }
        }

        /// <remarks/>
        public typeOfClientType typeOfClient
        {
            get
            {
                return this.typeOfClientField;
            }
            set
            {
                this.typeOfClientField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfClientSpecified
        {
            get
            {
                return this.typeOfClientFieldSpecified;
            }
            set
            {
                this.typeOfClientFieldSpecified = value;
            }
        }

        /// <remarks/>
        public typeOfConstructionProjectType typeOfConstructionProject
        {
            get
            {
                return this.typeOfConstructionProjectField;
            }
            set
            {
                this.typeOfConstructionProjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfConstructionProjectSpecified
        {
            get
            {
                return this.typeOfConstructionProjectFieldSpecified;
            }
            set
            {
                this.typeOfConstructionProjectFieldSpecified = value;
            }
        }

        /// <remarks/>
        public typeOfConstructionType typeOfConstruction
        {
            get
            {
                return this.typeOfConstructionField;
            }
            set
            {
                this.typeOfConstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool typeOfConstructionSpecified
        {
            get
            {
                return this.typeOfConstructionFieldSpecified;
            }
            set
            {
                this.typeOfConstructionFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string totalCostsOfProject
        {
            get
            {
                return this.totalCostsOfProjectField;
            }
            set
            {
                this.totalCostsOfProjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectAnnouncementDate
        {
            get
            {
                return this.projectAnnouncementDateField;
            }
            set
            {
                this.projectAnnouncementDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectAnnouncementDateSpecified
        {
            get
            {
                return this.projectAnnouncementDateFieldSpecified;
            }
            set
            {
                this.projectAnnouncementDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime buildingPermitIssueDate
        {
            get
            {
                return this.buildingPermitIssueDateField;
            }
            set
            {
                this.buildingPermitIssueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool buildingPermitIssueDateSpecified
        {
            get
            {
                return this.buildingPermitIssueDateFieldSpecified;
            }
            set
            {
                this.buildingPermitIssueDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectStartDate
        {
            get
            {
                return this.projectStartDateField;
            }
            set
            {
                this.projectStartDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectStartDateSpecified
        {
            get
            {
                return this.projectStartDateFieldSpecified;
            }
            set
            {
                this.projectStartDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectCompletionDate
        {
            get
            {
                return this.projectCompletionDateField;
            }
            set
            {
                this.projectCompletionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectCompletionDateSpecified
        {
            get
            {
                return this.projectCompletionDateFieldSpecified;
            }
            set
            {
                this.projectCompletionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime projectSuspensionDate
        {
            get
            {
                return this.projectSuspensionDateField;
            }
            set
            {
                this.projectSuspensionDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectSuspensionDateSpecified
        {
            get
            {
                return this.projectSuspensionDateFieldSpecified;
            }
            set
            {
                this.projectSuspensionDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime constructionAuthorisationDeniedDate
        {
            get
            {
                return this.constructionAuthorisationDeniedDateField;
            }
            set
            {
                this.constructionAuthorisationDeniedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool constructionAuthorisationDeniedDateSpecified
        {
            get
            {
                return this.constructionAuthorisationDeniedDateFieldSpecified;
            }
            set
            {
                this.constructionAuthorisationDeniedDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime nonRealisationDate
        {
            get
            {
                return this.nonRealisationDateField;
            }
            set
            {
                this.nonRealisationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool nonRealisationDateSpecified
        {
            get
            {
                return this.nonRealisationDateFieldSpecified;
            }
            set
            {
                this.nonRealisationDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime withdrawalDate
        {
            get
            {
                return this.withdrawalDateField;
            }
            set
            {
                this.withdrawalDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool withdrawalDateSpecified
        {
            get
            {
                return this.withdrawalDateFieldSpecified;
            }
            set
            {
                this.withdrawalDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string durationOfConstructionPhase
        {
            get
            {
                return this.durationOfConstructionPhaseField;
            }
            set
            {
                this.durationOfConstructionPhaseField = value;
            }
        }

        /// <remarks/>
        public projectStatusType projectStatus
        {
            get
            {
                return this.projectStatusField;
            }
            set
            {
                this.projectStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool projectStatusSpecified
        {
            get
            {
                return this.projectStatusFieldSpecified;
            }
            set
            {
                this.projectStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfConcernedBuildings
        {
            get
            {
                return this.numberOfConcernedBuildingsField;
            }
            set
            {
                this.numberOfConcernedBuildingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfConcernedDwellings
        {
            get
            {
                return this.numberOfConcernedDwellingsField;
            }
            set
            {
                this.numberOfConcernedDwellingsField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }

        /// <remarks/>
        public recordModificationType recordModification
        {
            get
            {
                return this.recordModificationField;
            }
            set
            {
                this.recordModificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class recordModificationType
    {

        private System.DateTime createDateField;

        private System.DateTime updateDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime createDate
        {
            get
            {
                return this.createDateField;
            }
            set
            {
                this.createDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime updateDate
        {
            get
            {
                return this.updateDateField;
            }
            set
            {
                this.updateDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class kindOfConstructionWorkType
    {

        private kindOfWorkType kindOfWorkField;

        private bool kindOfWorkFieldSpecified;

        private bool energeticRestaurationField;

        private bool energeticRestaurationFieldSpecified;

        private bool renovationHeatingSystemField;

        private bool renovationHeatingSystemFieldSpecified;

        private bool innerConversionRenovationField;

        private bool innerConversionRenovationFieldSpecified;

        private bool conversionField;

        private bool conversionFieldSpecified;

        private bool extensionHeighteningHeatedField;

        private bool extensionHeighteningHeatedFieldSpecified;

        private bool extensionHeighteningNotHeatedField;

        private bool extensionHeighteningNotHeatedFieldSpecified;

        private bool thermicSolarFacilityField;

        private bool thermicSolarFacilityFieldSpecified;

        private bool photovoltaicSolarFacilityField;

        private bool photovoltaicSolarFacilityFieldSpecified;

        private bool otherWorksField;

        private bool otherWorksFieldSpecified;

        private object extensionField;

        private recordModificationType recordModificationField;

        /// <remarks/>
        public kindOfWorkType kindOfWork
        {
            get
            {
                return this.kindOfWorkField;
            }
            set
            {
                this.kindOfWorkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool kindOfWorkSpecified
        {
            get
            {
                return this.kindOfWorkFieldSpecified;
            }
            set
            {
                this.kindOfWorkFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool energeticRestauration
        {
            get
            {
                return this.energeticRestaurationField;
            }
            set
            {
                this.energeticRestaurationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool energeticRestaurationSpecified
        {
            get
            {
                return this.energeticRestaurationFieldSpecified;
            }
            set
            {
                this.energeticRestaurationFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool renovationHeatingSystem
        {
            get
            {
                return this.renovationHeatingSystemField;
            }
            set
            {
                this.renovationHeatingSystemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool renovationHeatingSystemSpecified
        {
            get
            {
                return this.renovationHeatingSystemFieldSpecified;
            }
            set
            {
                this.renovationHeatingSystemFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool innerConversionRenovation
        {
            get
            {
                return this.innerConversionRenovationField;
            }
            set
            {
                this.innerConversionRenovationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool innerConversionRenovationSpecified
        {
            get
            {
                return this.innerConversionRenovationFieldSpecified;
            }
            set
            {
                this.innerConversionRenovationFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool conversion
        {
            get
            {
                return this.conversionField;
            }
            set
            {
                this.conversionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool conversionSpecified
        {
            get
            {
                return this.conversionFieldSpecified;
            }
            set
            {
                this.conversionFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool extensionHeighteningHeated
        {
            get
            {
                return this.extensionHeighteningHeatedField;
            }
            set
            {
                this.extensionHeighteningHeatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool extensionHeighteningHeatedSpecified
        {
            get
            {
                return this.extensionHeighteningHeatedFieldSpecified;
            }
            set
            {
                this.extensionHeighteningHeatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool extensionHeighteningNotHeated
        {
            get
            {
                return this.extensionHeighteningNotHeatedField;
            }
            set
            {
                this.extensionHeighteningNotHeatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool extensionHeighteningNotHeatedSpecified
        {
            get
            {
                return this.extensionHeighteningNotHeatedFieldSpecified;
            }
            set
            {
                this.extensionHeighteningNotHeatedFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool thermicSolarFacility
        {
            get
            {
                return this.thermicSolarFacilityField;
            }
            set
            {
                this.thermicSolarFacilityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool thermicSolarFacilitySpecified
        {
            get
            {
                return this.thermicSolarFacilityFieldSpecified;
            }
            set
            {
                this.thermicSolarFacilityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool photovoltaicSolarFacility
        {
            get
            {
                return this.photovoltaicSolarFacilityField;
            }
            set
            {
                this.photovoltaicSolarFacilityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool photovoltaicSolarFacilitySpecified
        {
            get
            {
                return this.photovoltaicSolarFacilityFieldSpecified;
            }
            set
            {
                this.photovoltaicSolarFacilityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool otherWorks
        {
            get
            {
                return this.otherWorksField;
            }
            set
            {
                this.otherWorksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool otherWorksSpecified
        {
            get
            {
                return this.otherWorksFieldSpecified;
            }
            set
            {
                this.otherWorksFieldSpecified = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }

        /// <remarks/>
        public recordModificationType recordModification
        {
            get
            {
                return this.recordModificationField;
            }
            set
            {
                this.recordModificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class dwellingType
    {

        private string administrativeDwellingNoField;

        private string physicalDwellingNoField;

        private string yearOfConstructionField;

        private string yearOfDemolitionField;

        private string noOfHabitableRoomsField;

        private string floorField;

        private bool multipleFloorField;

        private bool multipleFloorFieldSpecified;

        private string locationOfDwellingOnFloorField;

        private usageLimitationType usageLimitationField;

        private bool usageLimitationFieldSpecified;

        private bool kitchenField;

        private bool kitchenFieldSpecified;

        private string surfaceAreaOfDwellingField;

        private dwellingStatusType dwellingStatusField;

        private bool dwellingStatusFieldSpecified;

        private dwellingUsageType dwellingUsageField;

        private object extensionField;

        private recordModificationType recordModificationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string administrativeDwellingNo
        {
            get
            {
                return this.administrativeDwellingNoField;
            }
            set
            {
                this.administrativeDwellingNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string physicalDwellingNo
        {
            get
            {
                return this.physicalDwellingNoField;
            }
            set
            {
                this.physicalDwellingNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear")]
        public string yearOfConstruction
        {
            get
            {
                return this.yearOfConstructionField;
            }
            set
            {
                this.yearOfConstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear")]
        public string yearOfDemolition
        {
            get
            {
                return this.yearOfDemolitionField;
            }
            set
            {
                this.yearOfDemolitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string noOfHabitableRooms
        {
            get
            {
                return this.noOfHabitableRoomsField;
            }
            set
            {
                this.noOfHabitableRoomsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string floor
        {
            get
            {
                return this.floorField;
            }
            set
            {
                this.floorField = value;
            }
        }

        /// <remarks/>
        public bool multipleFloor
        {
            get
            {
                return this.multipleFloorField;
            }
            set
            {
                this.multipleFloorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool multipleFloorSpecified
        {
            get
            {
                return this.multipleFloorFieldSpecified;
            }
            set
            {
                this.multipleFloorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string locationOfDwellingOnFloor
        {
            get
            {
                return this.locationOfDwellingOnFloorField;
            }
            set
            {
                this.locationOfDwellingOnFloorField = value;
            }
        }

        /// <remarks/>
        public usageLimitationType usageLimitation
        {
            get
            {
                return this.usageLimitationField;
            }
            set
            {
                this.usageLimitationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool usageLimitationSpecified
        {
            get
            {
                return this.usageLimitationFieldSpecified;
            }
            set
            {
                this.usageLimitationFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool kitchen
        {
            get
            {
                return this.kitchenField;
            }
            set
            {
                this.kitchenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool kitchenSpecified
        {
            get
            {
                return this.kitchenFieldSpecified;
            }
            set
            {
                this.kitchenFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string surfaceAreaOfDwelling
        {
            get
            {
                return this.surfaceAreaOfDwellingField;
            }
            set
            {
                this.surfaceAreaOfDwellingField = value;
            }
        }

        /// <remarks/>
        public dwellingStatusType dwellingStatus
        {
            get
            {
                return this.dwellingStatusField;
            }
            set
            {
                this.dwellingStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dwellingStatusSpecified
        {
            get
            {
                return this.dwellingStatusFieldSpecified;
            }
            set
            {
                this.dwellingStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        public dwellingUsageType dwellingUsage
        {
            get
            {
                return this.dwellingUsageField;
            }
            set
            {
                this.dwellingUsageField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }

        /// <remarks/>
        public recordModificationType recordModification
        {
            get
            {
                return this.recordModificationField;
            }
            set
            {
                this.recordModificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class localityType
    {

        private uint swissZipCodeField;

        private string swissZipCodeAddOnField;

        private string placeNameField;

        /// <remarks/>
        public uint swissZipCode
        {
            get
            {
                return this.swissZipCodeField;
            }
            set
            {
                this.swissZipCodeField = value;
            }
        }

        /// <remarks/>
        public string swissZipCodeAddOn
        {
            get
            {
                return this.swissZipCodeAddOnField;
            }
            set
            {
                this.swissZipCodeAddOnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string placeName
        {
            get
            {
                return this.placeNameField;
            }
            set
            {
                this.placeNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class streetNameType
    {

        private streetLanguageType languageField;

        private string descriptionLongField;

        private string descriptionShortField;

        private string descriptionIndexField;

        /// <remarks/>
        public streetLanguageType language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string descriptionLong
        {
            get
            {
                return this.descriptionLongField;
            }
            set
            {
                this.descriptionLongField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string descriptionShort
        {
            get
            {
                return this.descriptionShortField;
            }
            set
            {
                this.descriptionShortField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string descriptionIndex
        {
            get
            {
                return this.descriptionIndexField;
            }
            set
            {
                this.descriptionIndexField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class streetType
    {

        private string eSIDField;

        private bool isOfficialDescriptionField;

        private bool isOfficialDescriptionFieldSpecified;

        private streetNameType[] streetNameListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string ESID
        {
            get
            {
                return this.eSIDField;
            }
            set
            {
                this.eSIDField = value;
            }
        }

        /// <remarks/>
        public bool isOfficialDescription
        {
            get
            {
                return this.isOfficialDescriptionField;
            }
            set
            {
                this.isOfficialDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isOfficialDescriptionSpecified
        {
            get
            {
                return this.isOfficialDescriptionFieldSpecified;
            }
            set
            {
                this.isOfficialDescriptionFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("streetNameItem", IsNullable = false)]
        public streetNameType[] streetNameList
        {
            get
            {
                return this.streetNameListField;
            }
            set
            {
                this.streetNameListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class coordinatesEntranceType
    {

        private decimal eastField;

        private decimal northField;

        /// <remarks/>
        public decimal east
        {
            get
            {
                return this.eastField;
            }
            set
            {
                this.eastField = value;
            }
        }

        /// <remarks/>
        public decimal north
        {
            get
            {
                return this.northField;
            }
            set
            {
                this.northField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class buildingEntranceType
    {

        private string eGAIDField;

        private string buildingEntranceNoField;

        private coordinatesEntranceType coordinatesField;

        private bool isOfficialAddressField;

        private bool isOfficialAddressFieldSpecified;

        private recordModificationType recordModificationField;

        private streetType streetField;

        private localityType localityField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGAID
        {
            get
            {
                return this.eGAIDField;
            }
            set
            {
                this.eGAIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string buildingEntranceNo
        {
            get
            {
                return this.buildingEntranceNoField;
            }
            set
            {
                this.buildingEntranceNoField = value;
            }
        }

        /// <remarks/>
        public coordinatesEntranceType coordinates
        {
            get
            {
                return this.coordinatesField;
            }
            set
            {
                this.coordinatesField = value;
            }
        }

        /// <remarks/>
        public bool isOfficialAddress
        {
            get
            {
                return this.isOfficialAddressField;
            }
            set
            {
                this.isOfficialAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool isOfficialAddressSpecified
        {
            get
            {
                return this.isOfficialAddressFieldSpecified;
            }
            set
            {
                this.isOfficialAddressFieldSpecified = value;
            }
        }

        /// <remarks/>
        public recordModificationType recordModification
        {
            get
            {
                return this.recordModificationField;
            }
            set
            {
                this.recordModificationField = value;
            }
        }

        /// <remarks/>
        public streetType street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        public localityType locality
        {
            get
            {
                return this.localityField;
            }
            set
            {
                this.localityField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class buildingVolumeType
    {

        private string volumeField;

        private buildingVolumeInformationSourceType informationSourceField;

        private bool informationSourceFieldSpecified;

        private buildingVolumeNormType normField;

        private bool normFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        public buildingVolumeInformationSourceType informationSource
        {
            get
            {
                return this.informationSourceField;
            }
            set
            {
                this.informationSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool informationSourceSpecified
        {
            get
            {
                return this.informationSourceFieldSpecified;
            }
            set
            {
                this.informationSourceFieldSpecified = value;
            }
        }

        /// <remarks/>
        public buildingVolumeNormType norm
        {
            get
            {
                return this.normField;
            }
            set
            {
                this.normField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool normSpecified
        {
            get
            {
                return this.normFieldSpecified;
            }
            set
            {
                this.normFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class buildingDateType
    {

        private string dateOfConstructionField;

        private periodOfConstructionType periodOfConstructionField;

        private bool periodOfConstructionFieldSpecified;

        /// <remarks/>
        public string dateOfConstruction
        {
            get
            {
                return this.dateOfConstructionField;
            }
            set
            {
                this.dateOfConstructionField = value;
            }
        }

        /// <remarks/>
        public periodOfConstructionType periodOfConstruction
        {
            get
            {
                return this.periodOfConstructionField;
            }
            set
            {
                this.periodOfConstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool periodOfConstructionSpecified
        {
            get
            {
                return this.periodOfConstructionFieldSpecified;
            }
            set
            {
                this.periodOfConstructionFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class coordinatesBuildingType
    {

        private decimal eastField;

        private decimal northField;

        private originOfCoordinatesType originOfCoordinatesField;

        /// <remarks/>
        public decimal east
        {
            get
            {
                return this.eastField;
            }
            set
            {
                this.eastField = value;
            }
        }

        /// <remarks/>
        public decimal north
        {
            get
            {
                return this.northField;
            }
            set
            {
                this.northField = value;
            }
        }

        /// <remarks/>
        public originOfCoordinatesType originOfCoordinates
        {
            get
            {
                return this.originOfCoordinatesField;
            }
            set
            {
                this.originOfCoordinatesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class buildingType
    {

        private string officialBuildingNoField;

        private string nameOfBuildingField;

        private coordinatesBuildingType coordinatesField;

        private string localCode1Field;

        private string localCode2Field;

        private string localCode3Field;

        private string localCode4Field;

        private string neighbourhoodField;

        private buildingStatusType buildingStatusField;

        private bool buildingStatusFieldSpecified;

        private buildingCategoryType buildingCategoryField;

        private bool buildingCategoryFieldSpecified;

        private string buildingClassField;

        private buildingDateType dateOfConstructionField;

        private string yearOfRenovationField;

        private string yearOfDemolitionField;

        private string surfaceAreaOfBuildingField;

        private buildingVolumeType volumeField;

        private string numberOfFloorsField;

        private string numberOfSeparateHabitableRoomsField;

        private bool civilDefenseShelterField;

        private bool civilDefenseShelterFieldSpecified;

        private string energyRelevantSurfaceField;

        private heatingType thermotechnicalDeviceForHeating1Field;

        private heatingType thermotechnicalDeviceForHeating2Field;

        private hotWaterType thermotechnicalDeviceForWarmWater1Field;

        private hotWaterType thermotechnicalDeviceForWarmWater2Field;

        private object extensionField;

        private recordModificationType recordModificationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string officialBuildingNo
        {
            get
            {
                return this.officialBuildingNoField;
            }
            set
            {
                this.officialBuildingNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string nameOfBuilding
        {
            get
            {
                return this.nameOfBuildingField;
            }
            set
            {
                this.nameOfBuildingField = value;
            }
        }

        /// <remarks/>
        public coordinatesBuildingType coordinates
        {
            get
            {
                return this.coordinatesField;
            }
            set
            {
                this.coordinatesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string localCode1
        {
            get
            {
                return this.localCode1Field;
            }
            set
            {
                this.localCode1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string localCode2
        {
            get
            {
                return this.localCode2Field;
            }
            set
            {
                this.localCode2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string localCode3
        {
            get
            {
                return this.localCode3Field;
            }
            set
            {
                this.localCode3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string localCode4
        {
            get
            {
                return this.localCode4Field;
            }
            set
            {
                this.localCode4Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string neighbourhood
        {
            get
            {
                return this.neighbourhoodField;
            }
            set
            {
                this.neighbourhoodField = value;
            }
        }

        /// <remarks/>
        public buildingStatusType buildingStatus
        {
            get
            {
                return this.buildingStatusField;
            }
            set
            {
                this.buildingStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool buildingStatusSpecified
        {
            get
            {
                return this.buildingStatusFieldSpecified;
            }
            set
            {
                this.buildingStatusFieldSpecified = value;
            }
        }

        /// <remarks/>
        public buildingCategoryType buildingCategory
        {
            get
            {
                return this.buildingCategoryField;
            }
            set
            {
                this.buildingCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool buildingCategorySpecified
        {
            get
            {
                return this.buildingCategoryFieldSpecified;
            }
            set
            {
                this.buildingCategoryFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string buildingClass
        {
            get
            {
                return this.buildingClassField;
            }
            set
            {
                this.buildingClassField = value;
            }
        }

        /// <remarks/>
        public buildingDateType dateOfConstruction
        {
            get
            {
                return this.dateOfConstructionField;
            }
            set
            {
                this.dateOfConstructionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear")]
        public string yearOfRenovation
        {
            get
            {
                return this.yearOfRenovationField;
            }
            set
            {
                this.yearOfRenovationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "gYear")]
        public string yearOfDemolition
        {
            get
            {
                return this.yearOfDemolitionField;
            }
            set
            {
                this.yearOfDemolitionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string surfaceAreaOfBuilding
        {
            get
            {
                return this.surfaceAreaOfBuildingField;
            }
            set
            {
                this.surfaceAreaOfBuildingField = value;
            }
        }

        /// <remarks/>
        public buildingVolumeType volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfFloors
        {
            get
            {
                return this.numberOfFloorsField;
            }
            set
            {
                this.numberOfFloorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string numberOfSeparateHabitableRooms
        {
            get
            {
                return this.numberOfSeparateHabitableRoomsField;
            }
            set
            {
                this.numberOfSeparateHabitableRoomsField = value;
            }
        }

        /// <remarks/>
        public bool civilDefenseShelter
        {
            get
            {
                return this.civilDefenseShelterField;
            }
            set
            {
                this.civilDefenseShelterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool civilDefenseShelterSpecified
        {
            get
            {
                return this.civilDefenseShelterFieldSpecified;
            }
            set
            {
                this.civilDefenseShelterFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string energyRelevantSurface
        {
            get
            {
                return this.energyRelevantSurfaceField;
            }
            set
            {
                this.energyRelevantSurfaceField = value;
            }
        }

        /// <remarks/>
        public heatingType thermotechnicalDeviceForHeating1
        {
            get
            {
                return this.thermotechnicalDeviceForHeating1Field;
            }
            set
            {
                this.thermotechnicalDeviceForHeating1Field = value;
            }
        }

        /// <remarks/>
        public heatingType thermotechnicalDeviceForHeating2
        {
            get
            {
                return this.thermotechnicalDeviceForHeating2Field;
            }
            set
            {
                this.thermotechnicalDeviceForHeating2Field = value;
            }
        }

        /// <remarks/>
        public hotWaterType thermotechnicalDeviceForWarmWater1
        {
            get
            {
                return this.thermotechnicalDeviceForWarmWater1Field;
            }
            set
            {
                this.thermotechnicalDeviceForWarmWater1Field = value;
            }
        }

        /// <remarks/>
        public hotWaterType thermotechnicalDeviceForWarmWater2
        {
            get
            {
                return this.thermotechnicalDeviceForWarmWater2Field;
            }
            set
            {
                this.thermotechnicalDeviceForWarmWater2Field = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }

        /// <remarks/>
        public recordModificationType recordModification
        {
            get
            {
                return this.recordModificationField;
            }
            set
            {
                this.recordModificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class responseHeaderType
    {

        private string messageIdField;

        private string requestMessageIdField;

        private string businessReferenceIdField;

        private sendingApplicationType respondingApplicationField;

        private string commentField;

        private System.DateTime responseDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string messageId
        {
            get
            {
                return this.messageIdField;
            }
            set
            {
                this.messageIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string requestMessageId
        {
            get
            {
                return this.requestMessageIdField;
            }
            set
            {
                this.requestMessageIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string businessReferenceId
        {
            get
            {
                return this.businessReferenceIdField;
            }
            set
            {
                this.businessReferenceIdField = value;
            }
        }

        /// <remarks/>
        public sendingApplicationType respondingApplication
        {
            get
            {
                return this.respondingApplicationField;
            }
            set
            {
                this.respondingApplicationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }

        /// <remarks/>
        public System.DateTime responseDate
        {
            get
            {
                return this.responseDateField;
            }
            set
            {
                this.responseDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class keyValuePairType
    {

        private string keyField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class realestateIdentificationType
    {

        private string eGRIDField;

        private string numberField;

        private string numberSuffixField;

        private string subDistrictField;

        private string lotField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string EGRID
        {
            get
            {
                return this.eGRIDField;
            }
            set
            {
                this.eGRIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string numberSuffix
        {
            get
            {
                return this.numberSuffixField;
            }
            set
            {
                this.numberSuffixField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string subDistrict
        {
            get
            {
                return this.subDistrictField;
            }
            set
            {
                this.subDistrictField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string lot
        {
            get
            {
                return this.lotField;
            }
            set
            {
                this.lotField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public enum maddRequestTypeRequestContext
    {

        /// <remarks/>
        building,

        /// <remarks/>
        constructionProject,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddRequestTypeRequestQuery
    {

        private string eGIDField;

        private string ePROIDField;

        private maddRequestTypeRequestQueryCondition[] conditionField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGID
        {
            get
            {
                return this.eGIDField;
            }
            set
            {
                this.eGIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EPROID
        {
            get
            {
                return this.ePROIDField;
            }
            set
            {
                this.ePROIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("condition")]
        public maddRequestTypeRequestQueryCondition[] condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddRequestTypeRequestQueryCondition
    {

        private string attributePathField;

        private maddRequestTypeRequestQueryConditionOperator operatorField;

        private string[] attributeValueField;

        /// <remarks/>
        public string attributePath
        {
            get
            {
                return this.attributePathField;
            }
            set
            {
                this.attributePathField = value;
            }
        }

        /// <remarks/>
        public maddRequestTypeRequestQueryConditionOperator @operator
        {
            get
            {
                return this.operatorField;
            }
            set
            {
                this.operatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("attributeValue", DataType = "token")]
        public string[] attributeValue
        {
            get
            {
                return this.attributeValueField;
            }
            set
            {
                this.attributeValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public enum maddRequestTypeRequestQueryConditionOperator
    {

        /// <remarks/>
        equalTo,

        /// <remarks/>
        greaterThan,

        /// <remarks/>
        lessThan,

        /// <remarks/>
        greaterThanOrEqualTo,

        /// <remarks/>
        lessThanOrEqualTo,

        /// <remarks/>
        notEqualTo,

        /// <remarks/>
        @in,

        /// <remarks/>
        notIn,

        /// <remarks/>
        isNull,

        /// <remarks/>
        isNotNull,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddRequestTypeOptions
    {

        private keyValuePairType[] parameterListField;

        private string flagsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("parameterItem", IsNullable = false)]
        public keyValuePairType[] parameterList
        {
            get
            {
                return this.parameterListField;
            }
            set
            {
                this.parameterListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "NMTOKENS")]
        public string flags
        {
            get
            {
                return this.flagsField;
            }
            set
            {
                this.flagsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    [System.Xml.Serialization.XmlRootAttribute("maddResponse", Namespace = "http://www.ech.ch/xmlns/eCH-0206/2", IsNullable = false)]
    public partial class maddResponseType
    {

        private maddResponseTypeStatus statusField;

        private maddResponseTypeResponseHeader responseHeaderField;

        private maddRequestType originalRequestField;

        private maddResponseTypeMaddAuthorization maddAuthorizationField;

        private object itemField;

        private maddResponseTypeResponseMetadata responseMetadataField;

        private object extensionField;

        /// <remarks/>
        public maddResponseTypeStatus status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public maddResponseTypeResponseHeader responseHeader
        {
            get
            {
                return this.responseHeaderField;
            }
            set
            {
                this.responseHeaderField = value;
            }
        }

        /// <remarks/>
        public maddRequestType originalRequest
        {
            get
            {
                return this.originalRequestField;
            }
            set
            {
                this.originalRequestField = value;
            }
        }

        /// <remarks/>
        public maddResponseTypeMaddAuthorization maddAuthorization
        {
            get
            {
                return this.maddAuthorizationField;
            }
            set
            {
                this.maddAuthorizationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("buildingList", typeof(maddResponseTypeBuildingList))]
        [System.Xml.Serialization.XmlElementAttribute("constructionProjectList", typeof(maddResponseTypeConstructionProjectList))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        public maddResponseTypeResponseMetadata responseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeStatus
    {

        private string codeField;

        private string messageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger")]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeResponseHeader : responseHeaderType
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeMaddAuthorization
    {

        private string maddIdField;

        private string maddDataSetField;

        /// <remarks/>
        public string maddId
        {
            get
            {
                return this.maddIdField;
            }
            set
            {
                this.maddIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger")]
        public string maddDataSet
        {
            get
            {
                return this.maddDataSetField;
            }
            set
            {
                this.maddDataSetField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeBuildingList
    {

        private maddResponseTypeBuildingListBuildingItem[] buildingItemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("buildingItem")]
        public maddResponseTypeBuildingListBuildingItem[] buildingItem
        {
            get
            {
                return this.buildingItemField;
            }
            set
            {
                this.buildingItemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeBuildingListBuildingItem
    {

        private string eGIDField;

        private buildingType buildingField;

        private maddResponseTypeBuildingListBuildingItemBuildingEntranceItem[] buildingEntranceListField;

        private swissMunicipalityType municipalityField;

        private constructionWorkListTypeConstructionWorkItem[] constructionWorkListField;

        private realestateIdentificationType[] realestateIdentificationListField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGID
        {
            get
            {
                return this.eGIDField;
            }
            set
            {
                this.eGIDField = value;
            }
        }

        /// <remarks/>
        public buildingType building
        {
            get
            {
                return this.buildingField;
            }
            set
            {
                this.buildingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("buildingEntranceItem", IsNullable = false)]
        public maddResponseTypeBuildingListBuildingItemBuildingEntranceItem[] buildingEntranceList
        {
            get
            {
                return this.buildingEntranceListField;
            }
            set
            {
                this.buildingEntranceListField = value;
            }
        }

        /// <remarks/>
        public swissMunicipalityType municipality
        {
            get
            {
                return this.municipalityField;
            }
            set
            {
                this.municipalityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("constructionWorkItem", IsNullable = false)]
        public constructionWorkListTypeConstructionWorkItem[] constructionWorkList
        {
            get
            {
                return this.constructionWorkListField;
            }
            set
            {
                this.constructionWorkListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("realestateIdentificationItem", IsNullable = false)]
        public realestateIdentificationType[] realestateIdentificationList
        {
            get
            {
                return this.realestateIdentificationListField;
            }
            set
            {
                this.realestateIdentificationListField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeBuildingListBuildingItemBuildingEntranceItem
    {

        private string eDIDField;

        private buildingEntranceType buildingEntranceField;

        private maddResponseTypeBuildingListBuildingItemBuildingEntranceItemDwellingItem[] dwellingListField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EDID
        {
            get
            {
                return this.eDIDField;
            }
            set
            {
                this.eDIDField = value;
            }
        }

        /// <remarks/>
        public buildingEntranceType buildingEntrance
        {
            get
            {
                return this.buildingEntranceField;
            }
            set
            {
                this.buildingEntranceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("dwellingItem", IsNullable = false)]
        public maddResponseTypeBuildingListBuildingItemBuildingEntranceItemDwellingItem[] dwellingList
        {
            get
            {
                return this.dwellingListField;
            }
            set
            {
                this.dwellingListField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeBuildingListBuildingItemBuildingEntranceItemDwellingItem
    {

        private string eWIDField;

        private dwellingType dwellingField;

        private realestateIdentificationType realestateIdentificationItemField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EWID
        {
            get
            {
                return this.eWIDField;
            }
            set
            {
                this.eWIDField = value;
            }
        }

        /// <remarks/>
        public dwellingType dwelling
        {
            get
            {
                return this.dwellingField;
            }
            set
            {
                this.dwellingField = value;
            }
        }

        /// <remarks/>
        public realestateIdentificationType realestateIdentificationItem
        {
            get
            {
                return this.realestateIdentificationItemField;
            }
            set
            {
                this.realestateIdentificationItemField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class constructionWorkListTypeConstructionWorkItem
    {

        private string ePROIDField;

        private string aRBIDField;

        private string eGIDField;

        private kindOfConstructionWorkType kindOfConstructionWorkField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EPROID
        {
            get
            {
                return this.ePROIDField;
            }
            set
            {
                this.ePROIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string ARBID
        {
            get
            {
                return this.aRBIDField;
            }
            set
            {
                this.aRBIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EGID
        {
            get
            {
                return this.eGIDField;
            }
            set
            {
                this.eGIDField = value;
            }
        }

        /// <remarks/>
        public kindOfConstructionWorkType kindOfConstructionWork
        {
            get
            {
                return this.kindOfConstructionWorkField;
            }
            set
            {
                this.kindOfConstructionWorkField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeConstructionProjectList
    {

        private maddResponseTypeConstructionProjectListConstructionProjectItem[] constructionProjectItemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("constructionProjectItem")]
        public maddResponseTypeConstructionProjectListConstructionProjectItem[] constructionProjectItem
        {
            get
            {
                return this.constructionProjectItemField;
            }
            set
            {
                this.constructionProjectItemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeConstructionProjectListConstructionProjectItem
    {

        private string ePROIDField;

        private constructionProjectType constructionProjectField;

        private constructionWorkListTypeConstructionWorkItem[] constructionWorkListField;

        private realestateIdentificationType[] realestateIdentificationListField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string EPROID
        {
            get
            {
                return this.ePROIDField;
            }
            set
            {
                this.ePROIDField = value;
            }
        }

        /// <remarks/>
        public constructionProjectType constructionProject
        {
            get
            {
                return this.constructionProjectField;
            }
            set
            {
                this.constructionProjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("constructionWorkItem", IsNullable = false)]
        public constructionWorkListTypeConstructionWorkItem[] constructionWorkList
        {
            get
            {
                return this.constructionWorkListField;
            }
            set
            {
                this.constructionWorkListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("realestateIdentificationItem", IsNullable = false)]
        public realestateIdentificationType[] realestateIdentificationList
        {
            get
            {
                return this.realestateIdentificationListField;
            }
            set
            {
                this.realestateIdentificationListField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeResponseMetadata
    {

        private maddResponseTypeResponseMetadataStatisticsItem[] statisticsListField;

        private System.DateTime lastUpdateDateField;

        private System.DateTime exportDateField;

        private string[] remarkListField;

        private object extensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("statisticsItem", IsNullable = false)]
        public maddResponseTypeResponseMetadataStatisticsItem[] statisticsList
        {
            get
            {
                return this.statisticsListField;
            }
            set
            {
                this.statisticsListField = value;
            }
        }

        /// <remarks/>
        public System.DateTime lastUpdateDate
        {
            get
            {
                return this.lastUpdateDateField;
            }
            set
            {
                this.lastUpdateDateField = value;
            }
        }

        /// <remarks/>
        public System.DateTime exportDate
        {
            get
            {
                return this.exportDateField;
            }
            set
            {
                this.exportDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("remarkItem", DataType = "token", IsNullable = false)]
        public string[] remarkList
        {
            get
            {
                return this.remarkListField;
            }
            set
            {
                this.remarkListField = value;
            }
        }

        /// <remarks/>
        public object extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0206/2")]
    public partial class maddResponseTypeResponseMetadataStatisticsItem
    {

        private string objectTypeField;

        private string objectCountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "NMTOKEN")]
        public string objectType
        {
            get
            {
                return this.objectTypeField;
            }
            set
            {
                this.objectTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string objectCount
        {
            get
            {
                return this.objectCountField;
            }
            set
            {
                this.objectCountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0007/6")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0007/6", IsNullable = false)]
    public partial class municipalityRoot
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("swissAndFlMunicipality", typeof(swissAndFlMunicipalityType))]
        [System.Xml.Serialization.XmlElementAttribute("swissMunicipality", typeof(swissMunicipalityType1))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0044/4")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0044/4", IsNullable = false)]
    public partial class personIdentificationRoot
    {

        private personIdentificationType personIdentificationField;

        /// <remarks/>
        public personIdentificationType personIdentification
        {
            get
            {
                return this.personIdentificationField;
            }
            set
            {
                this.personIdentificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0058/5")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0058/5", IsNullable = false)]
    public partial class eventReport
    {

        private headerType headerField;

        private infoType infoField;

        /// <remarks/>
        public headerType header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <remarks/>
        public infoType info
        {
            get
            {
                return this.infoField;
            }
            set
            {
                this.infoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0097/2")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0097/2", IsNullable = false)]
    public partial class organisationIdentificationRoot
    {

        private organisationIdentificationType organisationIdentificationField;

        /// <remarks/>
        public organisationIdentificationType organisationIdentification
        {
            get
            {
                return this.organisationIdentificationField;
            }
            set
            {
                this.organisationIdentificationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ech.ch/xmlns/eCH-0008/3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ech.ch/xmlns/eCH-0008/3", IsNullable = false)]
    public partial class countryRoot
    {

        private countryType1 countryField;

        /// <remarks/>
        public countryType1 country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }



}
