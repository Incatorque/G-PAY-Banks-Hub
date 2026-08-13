import re

paths = [
    r"C:\Users\TinoMunhenga\AppData\Local\Temp\MIG__-_Payment_API_v1.7_(1).pdf.txt",
    r"C:\Users\TinoMunhenga\AppData\Local\Temp\Message_Implementation_Guide_-_Corporate_API_-_DebiCheck_V9.04_(2).pdf.txt",
]

for path in paths:
    print("====", path)
    t = open(path, encoding="utf-8", errors="ignore").read()
    for m in re.finditer(r"\{[^{}]{0,40}\"[A-Za-z]+\"[^{}]{10,1200}\}", t):
        s = m.group(0)
        if any(k.lower() in s.lower() for k in ["Username", "Password", "Session", "UserName"]):
            print("JSON:", " ".join(s.split())[:600])
            print("---")
    for pat in [r'"Username"\s*:\s*"[^"]+"', r'"Password"\s*:\s*"[^"]+"', r'"Session"\s*:\s*"[^"]+"', r'"Channel"[^,\n]{0,80}']:
        for m in re.finditer(pat, t, re.I):
            print("FIELD:", m.group(0)[:160])
