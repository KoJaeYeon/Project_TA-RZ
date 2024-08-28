using System.Collections.Generic;

public class Map_Resource : Data
{ 
    public string ID { get; }
    public int Map_Total_Resources { get; }
    public List<float> Map_Resources_type_Ratio { get; }
    public List<int> Map_Panel_Resources_Ratio { get; }

    public Map_Resource() : this("R101",50, new List<float> {33,33,34 }, new List<int> {20,20,0,0,0,0,0,0,0 })
    {
    }

    public Map_Resource(
        string id,
        int map_Total_Resources,
        List<float> map_Resources_type_Ratio,
        List<int> map_Panel_Resources_Ratio)
    {
        ID = id;
        Map_Total_Resources = map_Total_Resources;
        Map_Resources_type_Ratio = map_Resources_type_Ratio;
        Map_Panel_Resources_Ratio = map_Panel_Resources_Ratio;
    }

    public override string ToString()
    {
        string Map_Resources_type_Ratio_String = Map_Resources_type_Ratio.Count > 0 ? string.Join(", ", Map_Resources_type_Ratio) : "None";
        string Map_Panel_Resources_Ratio_String = Map_Panel_Resources_Ratio.Count > 0 ? string.Join(", ", Map_Panel_Resources_Ratio) : "None";
        return $"ID: {ID}, map_Total_Resources: {Map_Total_Resources}, map_Resources_type_Ratio: {Map_Resources_type_Ratio_String}, map_Panel_Resources_Ratio: {Map_Panel_Resources_Ratio_String}";
    }
}
