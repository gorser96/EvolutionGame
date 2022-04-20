using Newtonsoft.Json.Linq;
using System.Globalization;

namespace EvolutionBack.Services.Hubs;

public class GameRequest
{
    public GameRequest(Guid roomUid, GameRequestType requestType, JObject requestData)
    {
        RoomUid = roomUid;
        RequestType = requestType;
        RequestData = requestData;
    }

    public Guid RoomUid { get; init; }

    public JObject RequestData { get; init; }

    public GameRequestType RequestType { get; init; }

    public T? GetFromData<T>(string propName)
    {
        if (RequestData is null)
        {
            return default;
        }

        var value = RequestData.GetValue(propName, StringComparison.OrdinalIgnoreCase);

        var type = typeof(T);
        if (value == null || value.Type == JTokenType.Null)
        {
            return default;
        }

        var innerType = Nullable.GetUnderlyingType(type);
        if (innerType != null)
        {
            type = innerType;
        }

        object? val;
        if (typeof(DateTime).Equals(type))
        {
            if (value.Type == JTokenType.Date)
            {
                val = value.ToObject<DateTime>();
            }
            else if (value.Type == JTokenType.String && DateTime.TryParse(value.ToString(),
                CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dtVal))
            {
                val = dtVal;
            }
            else
            {
                return default;
            }
        }
        else if (typeof(Guid).Equals(type))
        {
            if (value.Type == JTokenType.Guid)
            {
                val = value.Value<Guid>();
            }
            else if (value.Type == JTokenType.String && Guid.TryParse(value.ToString(), out var uidVal))
            {
                val = uidVal;
            }
            else
            {
                return default;
            }
        }
        else if (typeof(double).Equals(type))
        {
            if (value.Type == JTokenType.Float)
            {
                val = value.Value<double>();
            }
            else if (value.Type == JTokenType.Integer)
            {
                val = (double)value.Value<int>();
            }
            else if (value.Type == JTokenType.String && double.TryParse(value.ToString(), NumberStyles.Any,
                CultureInfo.InvariantCulture, out var dVal))
            {
                val = dVal;
            }
            else
            {
                return default;
            }
        }
        else if (typeof(bool).Equals(type))
        {
            if (value.Type == JTokenType.Boolean)
            {
                val = value.Value<bool>();
            }
            else if (value.Type == JTokenType.String && bool.TryParse(value.ToString(), out var bVal))
            {
                val = bVal;
            }
            else
            {
                return default;
            }
        }
        else if (typeof(string).Equals(type))
        {
            if (value.Type == JTokenType.String)
            {
                val = value.Value<string>();
            }
            else if (value.Type == JTokenType.Integer)
            {
                val = value.Value<long>().ToString();
            }
            else
            {
                return default;
            }
        }
        else if (type.IsEnum)
        {
            if (value.Type == JTokenType.Integer || value.Type == JTokenType.String)
            {
                var strValue = value.ToString();
                val = Enum.Parse(type, strValue);
                if (!Enum.IsDefined(type, val))
                {
                    throw new NotSupportedException($"Not supported value for enum [{type.Name}], [{strValue}]");
                }
            }
            else
            {
                return default;
            }
        }
        else
        {
            throw new NotSupportedException($"Not supported type: {type.Name}");
        }

        return (T?)val;
    }
}

public enum GameRequestType
{
    CreateAnimal = 1,
    AddProperty = 2,
    AddPairProperty = 3,
    GetFood = 4,
    Attack = 5,
    UseProperty = 6,
}