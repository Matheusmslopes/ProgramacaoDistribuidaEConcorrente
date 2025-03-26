import threading
import time
import random


cruzamento_lock = threading.Lock()


estatisticas = []

class Veiculo(threading.Thread):
    def __init__(self, id_veiculo):
        threading.Thread.__init__(self)
        self.id_veiculo = id_veiculo
        self.tempo_espera = 0
        self.tempo_no_cruzamento = 0

    def run(self):
        print(f"== Veículo {self.id_veiculo} esperando para entrar no cruzamento ==")


        espera = random.randint(1000, 5000)  # ms
        time.sleep(espera / 1000)
        self.tempo_espera = espera

        entrada = time.time()
        with cruzamento_lock:
            print(f"Veículo {self.id_veiculo} entrou no cruzamento.")
            tempo_dentro = random.randint(500, 1500)  # ms
            time.sleep(tempo_dentro / 1000)
            self.tempo_no_cruzamento = tempo_dentro
            print(f"Veículo {self.id_veiculo} saiu do cruzamento.")

        saida = time.time()

        estatisticas.append({
            "Veículo": self.id_veiculo,
            "Tempo de Espera (ms)": self.tempo_espera,
            "Tempo no Cruzamento (ms)": self.tempo_no_cruzamento,
            "Tempo Total (ms)": int(self.tempo_espera + self.tempo_no_cruzamento)
        })

def main():
    num_veiculos = 5
    threads = []

    for i in range(1, num_veiculos + 1):
        v = Veiculo(i)
        threads.append(v)
        v.start()

    for t in threads:
        t.join()

    print("\n=== Estatísticas da Simulação ===")
    for e in sorted(estatisticas, key=lambda x: x["Veículo"]):
        print(f"== Veículo {e['Veículo']} ==")
        print(f"Tempo esperando: {e['Tempo de Espera (ms)']} ms")
        print(f"Tempo no cruzamento: {e['Tempo no Cruzamento (ms)']} ms")
        print(f"Tempo total: {e['Tempo Total (ms)']} ms\n")

    print("Simulação finalizada.")

if __name__ == "__main__":
    main()
