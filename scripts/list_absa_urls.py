import re

path = r"C:\Users\TinoMunhenga\AppData\Local\Temp\MIG__-_Payment_API_v1.7_(1).pdf.txt"
t = open(path, encoding="utf-8", errors="ignore").read()

print("URLs:")
for m in re.finditer(r"https://capi[^\s\)\"']+", t):
    print(m.group(0))

print("\nAPI paths:")
for m in re.finditer(r"/api/[A-Za-z0-9_/\-]+", t):
    print(m.group(0))

path2 = r"C:\Users\TinoMunhenga\AppData\Local\Temp\Message_Implementation_Guide_-_Corporate_API_-_DebiCheck_V9.04_(2).pdf.txt"
t2 = open(path2, encoding="utf-8", errors="ignore").read()
print("\nDebiCheck URLs:")
for u in sorted(set(re.findall(r"https://capi[^\s\)\"']+", t2))):
    print(u)
print("\nDebiCheck paths:")
for u in sorted(set(re.findall(r"/api/[A-Za-z0-9_/\-]+", t2))):
    print(u)
