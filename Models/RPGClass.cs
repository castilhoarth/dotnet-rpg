using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))] //Permite a visualização do swagger em termos de valores e não de números
    public enum RPGClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3

    }
}