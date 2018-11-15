using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SymplexCsharp
{
    class Program
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------



        static void Main(string[] args)
        {

           
            
             

            int p;

            bool zero = false;


            int rest, ni=1, var, c;

            int i, j, k = 1, col, lin = 0, count, soma,conta=0;
            
            double maximo = 0, menor, aux, mult, temp;

            //Numero maximo de restricoes aceita por este symplex

            Console.WriteLine("Degite:\n 1 - Maximizar\n 2 - Minimizar");
            int verifica = int.Parse(Console.ReadLine());
            Console.Clear();

            if (verifica == 2)                                                              
            {

                //chama método de Minimizar
                Min call = new Min();

                call.Minimizar();
            }
            else if (verifica == 1)
            {

                Console.WriteLine("Programa para maximizar problema de otimizacao atraves do symplex");
                Console.Write("Funcao a ser otimizada \nNumero de variaveis: ");
                var = int.Parse(Console.ReadLine());


                Console.Write("\nNumero de restricoes: ");
                rest = int.Parse(Console.ReadLine());

                double[] funcao = new double[var]; //cj - zj
                double[,] matrizRestricao = new double[rest+1, rest + var+1];//matriz das restricoes , corpo da impressao

                double[] termIndependente = new double[rest];// coluna dos termos independentes



                Console.WriteLine("\nExemplo: cn.Xn n de 1 ate numero de variaveis");

                for (c = 0; c < var; c++)
                {
                    Console.Write(" c" + (c + 1) + ":");
                    funcao[c] = double.Parse(Console.ReadLine()); //Coeficientes das variaveis de lucro


                }

                
                

                

                for (i = 0; i < rest; i++)
                {
                    Console.WriteLine("Restricao " + (i + 1));
                    k = 1;
                    for (c = 0; c < var; c++)
                    {

                        Console.Write("X" + k + ":");
                        matrizRestricao[i, c] = float.Parse(Console.ReadLine()); //restricoes
                        k++;

                    }


                    Console.Write("Termo independente da restricao eg: cn.Xn = b\nb= ");
                    termIndependente[i] = float.Parse(Console.ReadLine());

                }

                //Imprimir matriz de dados inseridos
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
                            matrizRestricao[i,var + j] = 1; //Diagonal principal das variaveis de excesso = 1
                        else
                            matrizRestricao[i,var + j] = 0; // restante = 0
                for (i = 0; i < rest; i++)
                    matrizRestricao[i,var + rest] = termIndependente[i]; //coluna dos termos independentes

                for (j = 0; j < var; j++)
                    matrizRestricao[rest,j] = funcao[j] * -1;// coeficientes da funcao negados na ultima linha
                for (j = var; j < rest + var; j++)
                    matrizRestricao[rest,j] = 0; // completa com 0 as posições que faltam


                Console.WriteLine("\n---------------------"); 
                Console.WriteLine("\n ITERACAO:" + ni);
                Console.WriteLine("\n---------------------\n");
                ni++;

                for (i = 0; i < matrizRestricao.GetLength(0); i++)
                {
                    for (j = 0; j < matrizRestricao.GetLength(1); j++)
                        Console.Write("\t" + matrizRestricao[i, j].ToString("0.00")); // imprime a tabela inicial simplex
                    Console.WriteLine();
                }

                
                


                //Encontrar a variavel que entra e a variavel que sai da base

                do
                {
                    //Encontra a variavel que entrara na base
                    maximo = Math.Abs(matrizRestricao[rest, 0]);
                    col = 0;

                    //encontra coluna pivo
                    for (j = 0; j < matrizRestricao.GetLength(1); j++)
                    {
                        if (matrizRestricao[rest, j] < 0)
                        {
                            temp = -1 * matrizRestricao[rest, j];
                            if (maximo < temp)
                            {
                                maximo = temp;
                                col = j;
                            }
                        }//encontra coluna pivo - END
                        else
                            continue;
                    }

                    count = 0;
                    aux = 1e20;
                    menor = 1e20;
                    //encontra linha pivo
                    for (i = 0; i < matrizRestricao.GetLength(0); i++)
                    {


                        if (matrizRestricao[i, col] > 0)
                        {
                            aux = matrizRestricao[i, rest + var] / matrizRestricao[i, col];//calculo do teta

                        }
                        else
                        {
                            count++;
                        }
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
                        }//encontra linha pivo - END
                    }
                    if (zero == true)
                    {
                        break;
                    }
                    //não calcula teta negativo ou valor divisível por 0 -> solução ilimitada
                    if (count == rest)
                    {
                        Console.WriteLine("Solucao ilimitada");
                        Console.ReadLine();
                        break;

                    }
                    Console.WriteLine("\nNumero Pivo: " + matrizRestricao[lin, col] + ", linha " + lin + " coluna " + col);



                    //encontra os coeficientes da nova tabela
                    aux = matrizRestricao[lin, col];//numero pivo
                    for (j = 0; j <= rest + var; j++)
                        matrizRestricao[lin, j] = matrizRestricao[lin, j] / aux;// divisão de todos os valores da linha pelo pivo


                    for (i = 0; i <= rest; i++)
                    {
                        if (i == lin)
                            continue;// linha pivo não será alterada
                        else
                        {
                            aux = matrizRestricao[i, col] * -1;// valor que sera multiplicado o valor da linha para somar com a linha a ser zerada
                            for (k = 0; k <= rest + var; k++)
                            {
                                matrizRestricao[i, k] = (aux * matrizRestricao[lin, k]) + matrizRestricao[i, k]; //valores da nova linha
                            }
                        }

                    }
                    Console.WriteLine("\n---------------------");
                    Console.WriteLine("\n ITERACAO:" + ni);
                    Console.WriteLine("\n--------------------\n");
                    ni++;
                    //imprime proxima matriz 
                    for (i = 0; i < matrizRestricao.GetLength(0); i++)
                    {
                        for (j = 0; j < matrizRestricao.GetLength(1); j++)
                            Console.Write("\t" + matrizRestricao[i, j].ToString("0.00"));
                        Console.Write("\n");

                    }
                    conta = 0;
                    int end = rest + var+1;
                    for (j = 0; j < end ; j++)
                        if (matrizRestricao[rest, j] >= 0)// verifica que todos o valores de z são maiores que 0
                            conta++;
                    
                    if (conta == end) 
                        break;
                } while (1!=0);

                //imprime o Resultado

                if (zero == false)
                {
                    Console.WriteLine("---->");
                    Console.WriteLine("Resultado\n \tZ: " + matrizRestricao[i - 1, j - 1]);

                    int x = var;

                    int v = 1;

                    i = var;
                    j = var + rest;

                    //impressoes dos valores de X

                    for (int z = 0; z < var; z++)
                    {
                        Console.WriteLine("\n \tX" + x + ": " + matrizRestricao[i - v, j]);
                        x--;
                        v++;
                    }


                    Console.Read();
                }
            }
        }
    }
}

