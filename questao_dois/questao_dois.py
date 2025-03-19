import os
import threading

PASTA_CURSOS = "dados_cursos"


alunos_formandos = []
lock = threading.Lock()  


def processar_arquivo(caminho_arquivo):
    """Lê um arquivo e adiciona alunos formandos à lista global."""
    try:
        with open(caminho_arquivo, "r", encoding="utf-8") as arquivo:
            for linha in arquivo:
                dados = linha.strip().split(",")
                if len(dados) == 4:
                    matricula, nome, curso, status = dados
                    if status.strip().upper() == "CONCLUÍDO":
                        with lock:
                            alunos_formandos.append((matricula, nome, curso))
    except Exception as e:
        print(f"Erro ao processar {caminho_arquivo}: {e}")


def main():
    """Cria threads para processar arquivos simultaneamente."""
    threads = []
    arquivos = [f for f in os.listdir(PASTA_CURSOS) if f.endswith(".txt")]

    for arquivo in arquivos:
        caminho_completo = os.path.join(PASTA_CURSOS, arquivo)
        thread = threading.Thread(target=processar_arquivo, args=(caminho_completo,))
        threads.append(thread)
        thread.start()

    for thread in threads:
        thread.join()

    print("\nLista de alunos formandos:")
    for matricula, nome, curso in alunos_formandos:
        print(f"Matrícula: {matricula} | Nome: {nome} | Curso: {curso}")


if __name__ == "__main__":
    main()
