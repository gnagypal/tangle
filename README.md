# tangle

Gubanc játék megoldásának keresése backtrack algoritmus segítségével. 
A samples könyvtárban a példa keresés eredmények képei találhatóak. 

A képekhez magyarázat: keresés paramétere, hogy az első találatnál megálljon, vagy folytassa tovább a keresést. Utóbbi esetben egy talált konfigurációnak a 90/180/270 fokkal elforgatott eseteit is meg fogja találni az algoritmus, tehát egy helyett 2-4 azonos találat lehet.
- test3X3.png  4 tábla -> 1 valódi találat * 4 elfoghatási iránnyal
- test4X2.png  2 tábla -> 1 valódi találat * 2 elfoghatási iránnyal
- test4X4.png  4 tábla -> 1 valódi találat * 4 elfoghatási iránnyal
- test5X5.png  16 tábla-> 2 valódi találat * 4 elfoghatási iránnyal * 2 felcserélhető kártyák miatt

A legutolsónál a 9-es és 10-es kártyák színezése egyforma, a megoldásban felcserélhető a helyük, de ugyanazt a színezést adják.


