using System;
using System.Collections;
using System.Collections.Generic;

namespace sakura
{
    public class Generator
    {
        List<int> graph;
        Random rnd;
        int k = 0;
        int r = 0;
        int v0;
        int count;

        public const int countFlowersWidth = 4;
        public const int countFlowersHeight = 7;
        public const int countFlowers = countFlowersHeight * countFlowersWidth;


        public List<int> _graph
        {
            get
            {
                return graph;
            }
        }

        public Generator(int c)
        {
            rnd = new Random(c);
            v0 = rnd.Next(0, countFlowers);
            graph = new List<int>(countFlowers);
            graph.Add(v0);

            count = c;
            k = 0;
            Generate(v0);
        }

        public void Generate(int v)
        {
            if (k != count - 1)
            {
                r = rnd.Next(0, 4);

                // Идем от вершины вверх
                if (r == 0)
                {
                    if (graph.Count == 1)
                    {
                        if (v - 4 > 0)
                        {
                            Add(v - 4);
                        }
                        else
                        {
                            Add(v + 4);
                        }
                    }
                    else
                    if (v - 4 != graph[graph.Count - 2])  // Пришли не сверху?
                    {
                        if (v - 4 > -1) // Можно идти наверх?
                        {
                            Add(v - 4);
                        }
                        else
                        {
                            if (v + 4 != graph[graph.Count - 2])  // Пришли не снизу?
                            {
                                Add(v + 4);
                            }
                            else
                            {
                                if (v % 4 == 3) // Крайняя правая точка?
                                {
                                    Add(v - 1);
                                }
                                else if (v % 4 == 0) // Крайняя левая точка?
                                {
                                    Add(v + 1);
                                }
                                else
                                {
                                    if (rnd.Next(0, 2) == 0)
                                    {
                                        Add(v - 1);
                                    }
                                    else
                                    {
                                        Add(v + 1);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (v + 4 < 28) // Не крайняя нижняя точка?
                        {
                            Add(v + 4);
                        }
                        else
                        {
                            if (v % 4 == 0) // Крайняя левая точка?
                            {
                                Add(v + 1);
                            }
                            else if (v % 4 == 3)
                            {
                                Add(v - 1);
                            }
                            else
                            {
                                if (rnd.Next(0, 2) == 0)
                                {
                                    Add(v - 1);
                                }
                                else
                                {
                                    Add(v + 1);
                                }
                            }
                        }
                    }
                }


                // Вправо
                if (r == 1)
                {
                    if (graph.Count == 1)
                    {
                        if (v % 4 != 3)
                        {
                            Add(v + 1);
                        }
                        else
                        {
                            Add(v - 1);
                        }
                    }
                    else
                    if (v + 1 != graph[graph.Count - 2])  // Пришли не справа?
                    {
                        if (v % 4 != 3) // Можно идти направо?
                        {
                            Add(v + 1);
                        }
                        else
                        {
                            if (v - 1 != graph[graph.Count - 2])  // Пришли не слева?
                            {
                                Add(v - 1);
                            }
                            else
                            {
                                if (v - 4 < 0) // Крайняя верхняя точка?
                                {
                                    Add(v + 4);
                                }
                                else if (v + 4 > 28) // Крайняя нижняя точка?
                                {
                                    Add(v - 4);
                                }
                                else
                                {
                                    if (rnd.Next(0, 2) == 0)
                                    {
                                        Add(v - 4);
                                    }
                                    else
                                    {
                                        Add(v + 4);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (v % 4 != 0) // Не крайняя левая точка?
                        {
                            Add(v - 1);
                        }
                        else
                        {
                            if (v + 4 > 28) // Крайняя нижняя точка?
                            {
                                Add(v - 4);
                            }
                            else if (v - 4 < 0) // Крайняя верхняя точка?
                            {
                                Add(v + 4);
                            }
                            else
                            {
                                if (rnd.Next(0, 2) == 0)
                                {
                                    Add(v - 4);
                                }
                                else
                                {
                                    Add(v + 4);
                                }
                            }
                        }
                    }
                }

                // Вниз
                if (r == 2)
                {
                    if (graph.Count == 1)
                    {
                        if (v + 4 < 28)
                        {
                            Add(v + 4);
                        }
                        else
                        {
                            Add(v - 4);
                        }
                    }
                    else
                    if (v + 4 != graph[graph.Count - 2])  // Пришли не снизу?
                    {
                        if (v + 4 < 28) // Можно идти вниз?
                        {
                            Add(v + 4);
                        }
                        else
                        {
                            if (v - 4 != graph[graph.Count - 2])  // Пришли не сверху?
                            {
                                Add(v - 4);
                            }
                            else
                            {
                                if (v % 4 == 3) // Крайняя правая точка?
                                {
                                    Add(v - 1);
                                }
                                else if (v % 4 == 0) // Крайняя левая точка?
                                {
                                    Add(v + 1);
                                }
                                else
                                {
                                    if (rnd.Next(0, 2) == 0)
                                    {
                                        Add(v - 1);
                                    }
                                    else
                                    {
                                        Add(v + 1);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (v - 4 > -1) // Не крайняя верхняя точка?
                        {
                            Add(v - 4);
                        }
                        else
                        {
                            if (v % 4 == 0) // Крайняя левая точка?
                            {
                                Add(v + 1);
                            }
                            else if (v % 4 == 3)
                            {
                                Add(v - 1);
                            }
                            else
                            {
                                if (rnd.Next(0, 2) == 0)
                                {
                                    Add(v - 1);
                                }
                                else
                                {
                                    Add(v + 1);
                                }
                            }
                        }
                    }
                }


                // Влево
                if (r == 3)
                {
                    if (graph.Count == 1)
                    {
                        if (v % 4 != 0)
                        {
                            Add(v - 1);
                        }
                        else
                        {
                            Add(v + 1);
                        }
                    }
                    else
                    if (v - 1 != graph[graph.Count - 2])  // Пришли не слева?
                    {
                        if (v % 4 != 0) // Можно идти налево?
                        {
                            Add(v - 1);
                        }
                        else
                        {
                            if (v + 1 != graph[graph.Count - 2])  // Пришли не справа?
                            {
                                Add(v + 1);
                            }
                            else
                            {
                                if (v - 4 < 0) // Крайняя верхняя точка?
                                {
                                    Add(v + 4);
                                }
                                else if (v + 4 > 28) // Крайняя нижняя точка?
                                {
                                    Add(v - 4);
                                }
                                else
                                {
                                    if (rnd.Next(0, 2) == 0)
                                    {
                                        Add(v - 4);
                                    }
                                    else
                                    {
                                        Add(v + 4);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (v % 4 != 3) // Не крайняя правая точка?
                        {
                            Add(v + 1);
                        }
                        else
                        {
                            if (v + 4 > 28) // Крайняя нижняя точка?
                            {
                                Add(v - 4);
                            }
                            else if (v - 4 < 0) // Крайняя верхняя точка?
                            {
                                Add(v + 4);
                            }
                            else
                            {
                                if (rnd.Next(0, 2) == 0)
                                {
                                    Add(v - 4);
                                }
                                else
                                {
                                    Add(v + 4);
                                }
                            }
                        }
                    }
                }


            }
        }

            void Add(int l)
        {
                graph.Add(l);
                k++;
                Generate(l);
                r = -1;
            }

        }
    }
