from pypdf import PdfReader
import os, re

docs = r"C:\GPay\GPay.Banking\Documentation\Banks Documentation\ABSA ZA\Corporate API (CAPI)"
out_dir = os.environ["TEMP"]

for name in os.listdir(docs):
    if not name.lower().endswith(".pdf"):
        continue
    path = os.path.join(docs, name)
    print("====", name, "====")
    try:
        r = PdfReader(path)
        print("pages", len(r.pages))
        text = []
        for page in r.pages:
            text.append(page.extract_text() or "")
        full = "\n".join(text)
        out = os.path.join(out_dir, name.replace(" ", "_") + ".txt")
        with open(out, "w", encoding="utf-8", errors="ignore") as f:
            f.write(full)
        print("wrote", out, "chars", len(full))
        for kw in [
            "Account Verif",
            "AVS",
            "AHV",
            "account-verification",
            "OAuth",
            "token",
            "mTLS",
            "client_credentials",
            "Base URL",
            "https://",
            "Account Holder",
        ]:
            if re.search(kw, full, re.I):
                print("FOUND", kw)
        count = 0
        for m in re.finditer(
            r".{0,60}(account.?verif|AVS|AHV|oauth|token|https://[^\s\)\"']+|Account Holder).{0,100}",
            full,
            re.I,
        ):
            print("SNIP:", " ".join(m.group(0).split())[:220])
            count += 1
            if count >= 40:
                break
    except Exception as e:
        print("ERR", e)
