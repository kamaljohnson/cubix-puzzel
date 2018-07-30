using System;
using System.IO;
using System.Xml.Serialization;

public static class Helper {
    //Serialize
    public static string Serialize(this SaveState toSerialize)
    {
        Type type = typeof(SaveState);
        XmlSerializer xml = new XmlSerializer(type);
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }

    //De-serialize
    public static SaveState Deserialize(this string toDeserialze)
    {
        Type type = typeof(SaveState);
        XmlSerializer xml = new XmlSerializer(type);
        StringReader reader = new StringReader(toDeserialze);
        return (SaveState)xml.Deserialize(reader);
    }

}
