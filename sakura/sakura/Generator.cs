using System;
using System.Collections;
using System.Collections.Generic;

namespace sakura
{
	public class Generator
	{
		List<int> graph;
		Random rnd = new Random(1);
		int k = 0;
		int r = 0;
		int v0;
		int count;

		public List<int> _graph 
		{
			get
			{
				return graph;
			}
		}

		public Generator (int c)
		{
			v0 = rnd.Next (0, 28);
			graph = new List<int> (28);
			graph.Add(v0);

			count = c;
			k = 0;
			Generate (v0);
		}

		public void Generate(int v) 
		{
			if (k != count) {
				r = rnd.Next (0, 4);

				if (r == 0) {
					if (v - 4 > -1) {
						graph.Add (v - 4);
						k++;
						Generate (v - 4);
					}
				} else if (r == 1) {
					if (v + 1 < 28) {
						graph.Add (v + 1);
						k++;
						Generate (v + 1);
					}
				} else if (r == 2) { 
					if (v + 4 < 28) {
						graph.Add (v + 4);
						k++;
						Generate (v + 4);
					}
				} else if (r == 3) {
					if (v - 1 > -1) {
						graph.Add (v - 1);
						k++;
						Generate (v - 1);
					}
				}


			}
	    }
}
}

