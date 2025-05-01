using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace textdiffcore.DiffOutputGenerators;
public class JsonOutputGenerator : IDiffOutputGenerator
{
	/// <summary>
	/// Whether to ignore Equal
	/// </summary>
	public bool IsRemoveEqual { get; set; }

	public string GenerateOutput(List<Diffrence> diffrences)
	{
		var ja = new JsonArray();
		for (int i = 0; i < diffrences.Count; i++)
		{
			if (this.IsRemoveEqual && diffrences[i].action == TextDiffAction.Equal)
			{
				continue;
			}

			var node = this.GenerateNode(diffrences[i], i);
			ja.Add(node);
		}

		return ja.ToJsonString();
	}

	public string GenerateOutput(Diffrence diffrence)
	{
		return this.GenerateNode(diffrence).ToJsonString();
	}

	private JsonNode GenerateNode( Diffrence diff, int? index = null)
	{
		var node = new JsonObject();
		node["action"] = diff.action.ToString();
		node["value"] = diff.value;

		if (index != null)
		{
			node["index"] = index;
		}

		return node;
	}

}
