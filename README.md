# tangle

Gubanc játék megoldásának keresése backtrack algoritmus segítségével. 

A samples könyvtárban a példa keresés eredmények képei találhatóak. 

A keresés paramétere, hogy az első találatnál megálljon, vagy folytassa tovább a keresést. Utóbbi esetben egy talált konfigurációnak a 90/180/270 fokkal elforgatott eseteit is meg fogja találni az algoritmus, tehát az egy helyett 2-4 azonos találat lehet. 

A képeken látható megoldások/találatok ezek alapján:
- test3X3.png  - 4 megoldás = 1 valódi találat * 4 elforgatási iránnyal
- test4X2.png  - 2 megoldás = 1 valódi találat * 2 elforgatási iránnyal
- test4X4.png  - 4 megoldás = 1 valódi találat * 4 elforgatási iránnyal
- test5X5.png  - 16 megoldás = 2 valódi találat * 4 elforgatási iránnyal * 2 felcserélhető kártyák miatt

A legutolsónál a 9-es és 10-es kártyák színezése egyforma, így a megoldásban felcserélhető a helyük, mert ugyanazt a színezést adják. Emiatt van plusz dupla szorzó a megoldások számánál.

A program Visual Studio 2013-mal készült, de Visual Studio 2012 alól is megnyithatónak kell lennie.
A keresést Tangle.Core.Tests / GameTest.cs -ben található unit tesztekkel lehet elindítani. A teszt eredményként készülő kép a Tangle.Core.Tests\bin\Debug\ könyvtárba kerül.
