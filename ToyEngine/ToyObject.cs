using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyEngine.Render;

namespace ToyEngine
{
	public class ToyObject
	{
		public Transform[] Transforms { get; set; }
		public Model Model { get; set; }
		public Shader Shader { get; set; }
		public Texture Texture { get; set; }
	}
}
