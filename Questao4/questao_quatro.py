import numpy as np
import threading
from time import time
from multiprocessing import cpu_count

vetor = np.random.randint(1, 101, size=1_000_000)

def soma_sequencial(vetor):
    return np.sum(vetor)

class SomaThread(threading.Thread):
    def __init__(self, vetor, inicio, fim, resultado_parcial, indice):
        threading.Thread.__init__(self)
        self.vetor = vetor
        self.inicio = inicio
        self.fim = fim
        self.resultado_parcial = resultado_parcial
        self.indice = indice

    def run(self):
        self.resultado_parcial[self.indice] = np.sum(self.vetor[self.inicio:self.fim])

def soma_concorrente(vetor, partes=10):
    tamanho = len(vetor)
    intervalo = tamanho // partes
    threads = []
    resultado_parcial = [0] * partes

    for i in range(partes):
        inicio = i * intervalo
        fim = (i + 1) * intervalo if i < partes - 1 else tamanho
        thread = SomaThread(vetor, inicio, fim, resultado_parcial, i)
        threads.append(thread)
        thread.start()

    for thread in threads:
        thread.join()

    return sum(resultado_parcial)

inicio_seq = time()
resultado_seq = soma_sequencial(vetor)
fim_seq = time()

inicio_conc = time()
resultado_conc = soma_concorrente(vetor, partes=cpu_count())
fim_conc = time()

print("\n--- Comparação das Abordagens de Soma ---")
print(f"Sequencial -> Resultado: {resultado_seq}, Tempo: {fim_seq - inicio_seq:.6f} s")
print(f"Concorrente -> Resultado: {resultado_conc}, Tempo: {fim_conc - inicio_conc:.6f} s, Núcleos: {cpu_count()}")
print(f"Threads usadas: {threading.active_count() - 1}")
