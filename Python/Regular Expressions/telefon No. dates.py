import re
#bez brojne validacije iskljucivo format(1 zadatak)
uzorak = r"\d{2}[/|\\.]\d{2}[/|\\.]\d{1,4}\.?"
s="12.12.2012. 13.13.2020. 01.01.203. 12/15/1 99/99/99 13/12/2015 12/02/2025abcd"
m = re.findall(uzorak,s)
print(m)
#Drugi zadatak
s= "sela@gmail.com pava@ea.pb jolo@ mario@eee.e joc@mx.ac.pp.cm"
uzorak = r"\b[^\s]+@+[^\s]+\.+[a-z]{2,4}"
p=re.findall(uzorak,s)
print(p)
#sve zemlje sveta(3 zadatak)
s= " +39165355766 +3335567895 061554332 060"
uzorak = r"\+[1-9]{7,9}|\b[0-9]{6,10}"
v=re.findall(uzorak,s)
print(v)
#samo za srbiju(3 zadatak)
s= "+38165123123 +381601231231 48+65 061123123 0623213213 060 01 adb"
uzorak = r"\+3816[0-9]{7,8} | \b06[0-9]{7,8}"
g=re.findall(uzorak,s)
print(g)
#cetvri zadatak
s= "a baba eofkoe 160-1234567891234-24 158-132456789555555-24 555-1234567891234-55"
uzorak= r"[0-9]{3}\-[0-9]{13}\-[0-9]{2}"
f=re.findall(uzorak,s)
print(f)
#odvojeno za americki i evropski sa brojnom validacijom(1 zadatak)
puzorak = r"((0[1-9]|[12]\d|3[01])\.(0[1-9]|1[0-2])\.\d{1,4}\.)"
auz=r"((0[1-9]|1[0-2])/(0[1-9]|[12]\d|3[01])/\d{1,4})"
p="12.12.2012. 1978-12-20 13.13.2020. 01.01.203. 12/15/1 99/99/99 13/12/2015 12/02/2025abcd"
lm = re.findall(puzorak,p)
am=re.findall(auz,p)
print("Evropski:")
for i in lm:
    print(i[0]);
print("Americki:")
for i in am:
    print(i[0])

