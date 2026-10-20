using MathNet.Numerics.LinearAlgebra;
using System.Globalization;

namespace Matrizes_Calculadora
{
    internal class Program
    {
        // Lê um número inteiro e positivo
        static Int32 LerInteiroPositivo(String mensagem)
        {
            Int32 num;

            Console.Write(mensagem);
            while (!Int32.TryParse(Console.ReadLine(), out num) || num <= 0)
            {
                Console.Write("Valor Inválido. Digite um número inteiro maior que zero: ");
            }

            return num;
        }

        // Lê um número double
        static Double LerDouble(String mensagem)
        {
            Double num;

            Console.Write(mensagem);
            while (!Double.TryParse((Console.ReadLine() ?? "").Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out num) || !Double.IsFinite(num))
            {
                Console.Write("Valor Inválido. Digite um número: ");
            }

            return num;
        }

        // Lê uma matriz
        static Double[,] LerMatriz(Int32 linhas, Int32 colunas, String nome)
        {
            Double[,] matriz = new Double[linhas, colunas];

            for (Int32 i = 0; i < linhas; i++)
            {
                for (Int32 j = 0; j < colunas; j++)
                {
                    matriz[i, j] = LerDouble($"{nome} | Elemento [{i + 1},{j + 1}]: ");
                }
            }

            return matriz;
        }

        // Imprime uma matriz
        static void ImprimirMatriz(Double[,] matriz)
        {
            for (Int32 i = 0; i < matriz.GetLength(0); i++)
            {
                for (Int32 j = 0; j < matriz.GetLength(1); j++)
                {
                    Double valor = Math.Round(matriz[i, j], 2) + 0.0;
                    Console.Write($"{valor:0.##}\t");
                }
                Console.WriteLine();
            }
        }

        // Lê um texto
        static String LerTexto()
        {
            return (Console.ReadLine() ?? "").Trim().ToUpper();
        }

        static void Main(string[] args)
        {
            Int32 i, j, k, ordem, L, C, L2, C2;
            Double det;
            String operacao, rep;
            rep = "S";

            Console.WriteLine("------ Bem-vindo à calculadora de matrizes ------");

            while (rep == "S")
            {
                // Escolha da operação

                Console.WriteLine("\nQual operação deseja realizar?");
                operacao = LerTexto();

                // Exibe uma mensagem de orientação e pede para responder novamente caso a resposta seja inválida

                while (!(operacao == "SOMA" || operacao == "ADIÇÃO" || operacao == "ADICAO" || operacao == "SUBTRAÇÃO" || operacao == "SUBTRACAO"
                || operacao == "MULTIPLICAÇÃO" || operacao == "MULTIPLICACAO" || operacao == "TRANSPOSTA" || operacao == "DETERMINANTE" || operacao == "INVERSA"))
                {
                    Console.WriteLine("\nNão é possível realizar essa operação");
                    Console.WriteLine("\nDigite uma operação válida (Soma | Subtração | Multiplicação | Transposta | Determinante | Inversa):");
                    operacao = LerTexto();
                }

                // Entrada de dados para soma ou subtração

                if (operacao == "SUBTRAÇÃO" || operacao == "SUBTRACAO" || operacao == "SOMA" || operacao == "ADIÇÃO" || operacao == "ADICAO")
                {
                    L = LerInteiroPositivo("\nDigite o número de linhas das matrizes: ");

                    C = LerInteiroPositivo("Digite o número de colunas das matrizes: ");

                    Console.WriteLine("\nDigite a primeira matriz:");

                    Double[,] mat1 = LerMatriz(L, C, "Matriz 1");

                    Console.WriteLine("\nDigite a segunda matriz:");

                    Double[,] mat2 = LerMatriz(L, C, "Matriz 2");

                    Double[,] matR = new Double[L, C];

                    // Processamento de dados para subtração

                    if (operacao == "SUBTRAÇÃO" || operacao == "SUBTRACAO")
                    {
                        for (i = 0; i < L; i++)
                        {
                            for (j = 0; j < C; j++)
                            {
                                matR[i, j] = mat1[i, j] - mat2[i, j];
                            }
                        }
                    }

                    // Processamento de dados para soma

                    else
                    {
                        for (i = 0; i < L; i++)
                        {
                            for (j = 0; j < C; j++)
                            {
                                matR[i, j] = mat1[i, j] + mat2[i, j];
                            }
                        }
                    }

                    // Saída de dados para soma ou subtração

                    Console.WriteLine("\nMatriz resultante: ");
                    ImprimirMatriz(matR);
                }

                // Entrada de dados para multiplicação

                else if (operacao == "MULTIPLICAÇÃO" || operacao == "MULTIPLICACAO")
                {
                    L = LerInteiroPositivo("\nDigite o número de linhas da primeira matriz: ");

                    C = LerInteiroPositivo("Digite o número de colunas da primeira matriz: ");

                    L2 = LerInteiroPositivo("\nDigite o número de linhas da segunda matriz: ");

                    C2 = LerInteiroPositivo("Digite o número de colunas da segunda matriz: ");

                    if (C == L2)
                    {
                        Console.WriteLine("\nDigite a primeira matriz:");

                        Double[,] mat1 = LerMatriz(L, C, "Matriz 1");

                        Console.WriteLine("\nDigite a segunda matriz:");

                        Double[,] mat2 = LerMatriz(L2, C2, "Matriz 2");

                        Double[,] matR = new Double[L, C2];

                        // Processamento de dados para multiplicação

                        for (i = 0; i < L; i++)
                        {
                            for (j = 0; j < C2; j++)
                            {
                                matR[i, j] = 0;

                                for (k = 0; k < C; k++)
                                {
                                    matR[i, j] += mat1[i, k] * mat2[k, j];
                                }
                            }
                        }

                        // Saída de dados para multiplicação

                        Console.WriteLine("\nMatriz resultante: ");
                        ImprimirMatriz(matR);
                    }

                    // Aviso caso a multiplicação não seja possível de ser realizada

                    else
                    {
                        Console.WriteLine("\nNão é possível realizar esta multiplicação");
                    }
                }

                // Entrada de dados para transposta

                else if (operacao == "TRANSPOSTA")
                {
                    L = LerInteiroPositivo("\nDigite o número de linhas da matriz: ");

                    C = LerInteiroPositivo("Digite o número de colunas da matriz: ");

                    Console.WriteLine("\nDigite a matriz:");

                    Double[,] mat1 = LerMatriz(L, C, "Matriz");

                    Double[,] matR = new Double[C, L];

                    // Processamento de dados para transposta

                    for (i = 0; i < C; i++)
                    {
                        for (j = 0; j < L; j++)
                        {
                            matR[i, j] = mat1[j, i];
                        }
                    }

                    // Saída de dados para transposta

                    Console.WriteLine("\nMatriz resultante: ");
                    ImprimirMatriz(matR);
                }

                // Entrada de dados para determinante ou inversa

                else if (operacao == "DETERMINANTE" || operacao == "INVERSA")
                {
                    ordem = LerInteiroPositivo("\nDigite a ordem da matriz: ");

                    Console.WriteLine("\nDigite a matriz:");

                    Double[,] mat1 = LerMatriz(ordem, ordem, "Matriz");

                    Matrix<Double> matriz = Matrix<Double>.Build.DenseOfArray(mat1);

                    // Processamento e saída de dados para determinante

                    if (operacao == "DETERMINANTE")
                    {
                        det = matriz.Determinant();

                        Console.WriteLine($"\nDeterminante = {Math.Round(det, 2) + 0.0:0.##}");
                    }
                    else
                    {
                        // Aviso caso não seja possível realizar a inversa

                        if (Math.Abs(matriz.Determinant()) < 1e-10)
                        {
                            Console.WriteLine("\nA matriz não possui inversa.");
                        }

                        // Processamento e saída de dados para inversa

                        else
                        {
                            Matrix<double> inversa = matriz.Inverse();

                            Console.WriteLine("\nMatriz inversa:");
                            ImprimirMatriz(inversa.ToArray());
                        }
                    }
                }

                // Perguntar se o usuário deseja realizar outro cálculo

                Console.Write("\nDeseja realizar outra operação? (s/n): ");
                rep = LerTexto();

                // Exibe uma mensagem de orientação e pede para responder novamente caso a resposta seja inválida

                while (!(rep == "S" || rep == "N"))
                {
                    Console.WriteLine("\nComando Inválido");
                    Console.Write("\nDigite (s) se quiser realizar outra operação e (n) se quiser encerrar: ");
                    rep = LerTexto();
                }
            }
        }
    }
}