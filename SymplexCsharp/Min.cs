using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplexCsharp
{
   
    class Min
    {
        public void Minimizar()
        {



            bool zero=false;
            int rest, ni = 1, var, c;

            int i, j, k = 1, col, lin = 0, count, conta = 0;

            double maximo = 0, menor, aux, temp;

            //Numero maximo de restricoes aceita por este symplex

         


                Console.WriteLine("Programa para minimizar problema de otimizacao atraves do symplex");
                Console.Write("Funcao a ser otimizada \nNumero de variaveis: ");
                var = int.Parse(Console.ReadLine());


                Console.Write("\nNumero de restricoes: ");
                rest = int.Parse(Console.ReadLine());

            int aux2 = var;
            var = rest;
            rest = aux2;



                double[] funcao = new double[var];
                double[,] matrizRestricao = new double[rest + 1, rest + var + 1];

                double[] termIndependente = new double[rest];



                Console.WriteLine("\nExemplo: cn.Xn n de 1 ate numero de variaveis");

                for (c = 0; c < rest; c++)
                {
                    Console.Write(" c" + (c + 1) + ":");
                termIndependente[c] = double.Parse(Console.ReadLine());


                }






                for (i = 0; i < var; i++)
                {
                    Console.WriteLine("Restricao " + (i + 1));
                    k = 1;
                    for (c = 0; c < rest; c++)
                    {

                        Console.Write("X" + k + ":");
                        matrizRestricao[c, i] = float.Parse(Console.ReadLine());
                        k++;

                    }


                    Console.Write("Termo independente da restricao eg: cn.Xn = b\nb= ");
                   funcao[i] = float.Parse(Console.ReadLine());

                }
                for (i = 0; i < matrizRestricao.GetLength(0); i++)
                {
                    for (j = 0; j < matrizRestricao.GetLength(1); j++)
                        Console.Write("\t" + matrizRestricao[i, j]);
                    Console.WriteLine();
                }



                //cria tabela inicial do simplex
                for (i = 0; i < rest; i++)
                    for (j = 0; j < rest; j++)
                        if (i == j)
                            matrizRestricao[i, var + j] = 1;
                        else
                            matrizRestricao[i, var + j] = 0;
                for (i = 0; i < rest; i++)
                    matrizRestricao[i, var + rest] = termIndependente[i];

                for (j = 0; j < var; j++)
                    matrizRestricao[rest, j] = funcao[j] * -1;
                for (j = var; j < rest + var; j++)
                    matrizRestricao[rest, j] = 0;


                Console.WriteLine("\n---------------------");
                Console.WriteLine("\n ITERACAO: " + ni);
                Console.WriteLine("\n---------------------\n");
                ni++;

                for (i = 0; i < matrizRestricao.GetLength(0); i++)
                {
                    for (j = 0; j < matrizRestricao.GetLength(1); j++)
                        Console.Write("\t" + matrizRestricao[i, j].ToString("0.00"));
                    Console.WriteLine();
                }

                Console.WriteLine("\n");


                //Encontrar a variavel que entra e a variavel que sai da base

                do
                {
                    //Encontra a variavel que entrara na base
                    maximo = Math.Abs(matrizRestricao[rest, 0]);
                    col = 0;
                    for (j = 0; j < matrizRestricao.GetLength(1); j++)
                    {
                        if (matrizRestricao[rest, j] < 0)
                        {
                            temp = -1 * matrizRestricao[rest, j];
                            if (maximo < temp)//encontra coluna pivo
                            {
                                maximo = temp;
                                col = j;
                            }
                        }
                        else
                            continue;
                    }

                    count = 0;
                    aux = 1e20;
                    menor = 1e20;
                    for (i = 0; i < matrizRestricao.GetLength(0); i++)
                    {

                        if (matrizRestricao[i, col] > 0)
                            aux = matrizRestricao[i, rest + var] / matrizRestricao[i, col];//calculo do teta
                        else
                            count++;
                    if (matrizRestricao[i, col] == 0)
                    {
                        Console.WriteLine("Solução sem fronteiras");
                        Console.ReadLine();
                        zero = true;
                        break;



                    }
                    if (menor > aux)
                        {
                            menor = aux;
                            lin = i;
                        }
                    }

                    if(zero == true)
                {

                    break;
                }
                    if (count == rest)
                    {
                    //não calcula teta negativo ou  divisível por 0 -> solução ilimitada
                        Console.WriteLine("Solucao ilimitada");
                        Console.ReadLine();
                        break;

                    }
                    Console.WriteLine("\nNumero Pivo: " + matrizRestricao[lin, col] + ", linha " + lin + " coluna " + col);



                    //encontra os coeficientes da nova tabela
                    aux = matrizRestricao[lin, col];//numero pivo
                    for (j = 0; j <= rest + var; j++)
                        matrizRestricao[lin, j] = matrizRestricao[lin, j] / aux;


                    for (i = 0; i <= rest; i++)
                    {
                        if (i == lin)
                            continue;
                        else
                        {
                            aux = matrizRestricao[i, col] * -1;
                            for (k = 0; k <= rest + var; k++)
                            {
                                matrizRestricao[i, k] = (aux * matrizRestricao[lin, k]) + matrizRestricao[i, k];
                            }
                        }

                    }
                    Console.WriteLine("\n---------------------");
                    Console.WriteLine("\n ITERACAO:" + ni);
                    Console.WriteLine("\n--------------------\n");
                    ni++;
                    //imprime proxima matriz (rest = Numero de restrições)
                    for (i = 0; i < matrizRestricao.GetLength(0); i++)
                    {
                        for (j = 0; j < matrizRestricao.GetLength(1); j++)
                            Console.Write("\t" + matrizRestricao[i, j].ToString("0.00"));
                        Console.Write("\n");

                    }
                    conta = 0;
                    int end = rest + var + 1;
                    for (j = 0; j < end; j++)
                        if (matrizRestricao[rest, j] >= 0)
                            conta++;

                    if (conta == end)
                        break;
                } while (1 != 0);

            //imprime o Resultado
            if (zero == false)
            {

                Console.WriteLine("---->");
                Console.WriteLine("Resultado\n \tZ: " + matrizRestricao[i - 1, j - 1]);
                int x = rest;
                int v = 2;
                //impressão dos de X

                for (int z = 0; z < rest; z++)
                {
                    Console.WriteLine("\n \tX" + x + ": " + matrizRestricao[i - 1, j - v]);
                    x--;
                    v++;
                }

                Console.Read();
            }
            }

        }

    }

