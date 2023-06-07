using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.


namespace S.Configuration.Objects
{
	[JsonObject(memberSerialization: MemberSerialization.OptIn)]
	[DataContract]
	public class Setting
	{
	    [JsonConstructor]
	    public Setting(string name, string value)
	    {
	        Name = name;
	        _value = value;
	    }
	
	    public Setting(string name, string assetName, AssetSetting asset)
	    {
	        Name = name;
	        _value = assetName;
	        Asset = asset;
	    }
	
	    [JsonProperty("name")]
	    [DataMember]
	    public string Name { get; private set; }
	
	    [IgnoreDataMember]
	    [JsonIgnore]
	    public string Value
	    {
	        get
	        {
	            return Asset == null ? _value : Asset.Value;
	        }
	        internal set
	        {
	            _value = value;
	        }
	    }
	
	    [DataMember]
	    [JsonProperty(propertyName: "asset")]
	    public AssetSetting? Asset { get; set; }
	
	    [DataMember]
	    [JsonProperty("value")]
	    private string _value;
	
	    public string ToJson() { return JsonConvert.SerializeObject(this); }
	}	
}
