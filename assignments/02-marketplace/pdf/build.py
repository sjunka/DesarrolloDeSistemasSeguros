"""Imprime marketplace.html a PDF (Entrega2-Marketplace-Junca.pdf).
Uso: python3 build.py  (requiere Google Chrome)."""
import pathlib, subprocess

HERE = pathlib.Path(__file__).resolve().parent
PDF = HERE.parent / "Entrega2-Marketplace-Junca.pdf"
CHROME = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"

subprocess.run([CHROME, "--headless", "--disable-gpu", "--no-pdf-header-footer",
                f"--print-to-pdf={PDF}", (HERE / "marketplace.html").as_uri()],
               check=True, capture_output=True)
print(PDF)
